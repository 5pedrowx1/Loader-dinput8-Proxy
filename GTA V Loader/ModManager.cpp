#include "pch.h"
#include "ModManager.h"
#include "Logger.h"
#include "PerformanceMonitor.h"

ModManager& ModManager::Instance() {
    static ModManager instance;
    return instance;
}

ModManager::ModManager() = default;

ModManager::~ModManager() {
    StopModuleScanThread();
}

bool ModManager::LoadMod(const fs::path& modPath, const std::string& type) {
    std::lock_guard<std::mutex> lock(Globals::g_ModsMutex);

    std::string modId = Utils::GenerateModID(modPath.wstring());

    for (const auto& mod : Globals::g_LoadedMods) {
        if (mod.id == modId) {
            LOG_WARNING("Mod already loaded: " + modPath.filename().string());
            return true;
        }
    }

    HMODULE hMod = LoadLibraryW(modPath.c_str());
    if (!hMod) {
        DWORD err = GetLastError();
        LOG_ERROR("Failed to load: " + modPath.filename().string() + " (Error: " + std::to_string(err) + ")");
        return false;
    }

    CallModInitFunction(hMod, modPath.filename().string());

    ModInfo mod;
    mod.name = modPath.filename().wstring();
    mod.path = modPath.wstring();
    mod.handle = hMod;
    mod.loaded = true;
    mod.type = type;
    mod.loadTime = std::chrono::system_clock::now();
    mod.size = Utils::GetFileSize(modPath.wstring());
    mod.id = modId;

    Globals::g_LoadedMods.push_back(mod);
    LOG_SUCCESS("Loaded: " + modPath.filename().string() + " (" + std::to_string(mod.size / 1024) + " KB)");

    return true;
}

bool ModManager::CallModInitFunction(HMODULE hMod, const std::string& modName) {
    const char* initFuncs[] = { "Initialize", "Init", "Startup", "OnLoad" };

    for (const char* funcName : initFuncs) {
        auto initFunc = GetProcAddress(hMod, funcName);
        if (initFunc) {
            try {
                PerformanceMonitor::ScopedTimer timer(Utils::GenerateModID(std::wstring(modName.begin(), modName.end())));
                typedef void (*InitFuncType)();
                ((InitFuncType)initFunc)();
                LOG_DEBUG("Init function called: " + std::string(funcName));
                return true;
            }
            catch (...) {
                LOG_WARNING("Exception in init function: " + std::string(funcName));
            }
        }
    }
    return false;
}

bool ModManager::CallModCleanupFunction(HMODULE hMod) {
    const char* cleanupFuncs[] = { "Cleanup", "Shutdown", "OnUnload", "Dispose" };

    for (const char* funcName : cleanupFuncs) {
        auto cleanupFunc = GetProcAddress(hMod, funcName);
        if (cleanupFunc) {
            try {
                typedef void (*CleanupFuncType)();
                ((CleanupFuncType)cleanupFunc)();
                LOG_DEBUG("Cleanup function called: " + std::string(funcName));
                return true;
            }
            catch (...) {
                LOG_WARNING("Exception in cleanup function: " + std::string(funcName));
            }
        }
    }
    return false;
}

bool ModManager::UnloadMod(const std::string& modId) {
    std::lock_guard<std::mutex> lock(Globals::g_ModsMutex);

    for (auto it = Globals::g_LoadedMods.begin(); it != Globals::g_LoadedMods.end(); ++it) {
        if (it->id == modId && it->handle) {
            CallModCleanupFunction(it->handle);

            if (FreeLibrary(it->handle)) {
                LOG_SUCCESS("Unloaded: " + Utils::WStringToString(it->name));
                Globals::g_LoadedMods.erase(it);
                return true;
            }
            else {
                LOG_ERROR("Failed to unload: " + Utils::WStringToString(it->name));
                return false;
            }
        }
    }

    LOG_WARNING("Mod not found for unload: " + modId);
    return false;
}

bool ModManager::ReloadMod(const std::string& modId) {
    std::wstring modPath;
    std::string modType;

    {
        std::lock_guard<std::mutex> lock(Globals::g_ModsMutex);
        for (const auto& mod : Globals::g_LoadedMods) {
            if (mod.id == modId) {
                modPath = mod.path;
                modType = mod.type;
                break;
            }
        }
    }

    if (modPath.empty()) {
        LOG_ERROR("Mod not found for reload");
        return false;
    }

    if (!UnloadMod(modId)) {
        LOG_ERROR("Failed to unload mod for reload");
        return false;
    }

    Sleep(500);

    return LoadMod(modPath, modType);
}

void ModManager::ScanAndLoadModsFolder() {
    if (!fs::exists("mods")) {
        fs::create_directories("mods");
        LOG_INFO("Created mods folder");
        return;
    }

    int count = 0;
    for (const auto& entry : fs::directory_iterator("mods")) {
        if (entry.path().extension() == ".asi" || entry.path().extension() == ".dll") {
            if (LoadMod(entry.path(), "mod")) {
                count++;
            }
        }
    }

    if (count > 0) {
        LOG_SUCCESS("Loaded " + std::to_string(count) + " mods from folder");
    }
}

void ModManager::ScanScriptsFolder() {
    std::wstring scriptsPath = Utils::GetGameDirectory() + L"scripts\\";

    if (!Utils::DirectoryExists(scriptsPath.c_str())) {
        CreateDirectoryW(scriptsPath.c_str(), NULL);
        LOG_INFO("Created scripts folder");
        return;
    }

    int dllCount = 0, csCount = 0;

    WIN32_FIND_DATAW findData;
    HANDLE hFind = FindFirstFileW((scriptsPath + L"*.dll").c_str(), &findData);
    if (hFind != INVALID_HANDLE_VALUE) {
        do {
            if (!(findData.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY)) dllCount++;
        } while (FindNextFileW(hFind, &findData));
        FindClose(hFind);
    }

    hFind = FindFirstFileW((scriptsPath + L"*.cs").c_str(), &findData);
    if (hFind != INVALID_HANDLE_VALUE) {
        do {
            if (!(findData.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY)) csCount++;
        } while (FindNextFileW(hFind, &findData));
        FindClose(hFind);
    }

    if (dllCount + csCount > 0) {
        LOG_INFO("Scripts found: " + std::to_string(dllCount) + " DLL, " + std::to_string(csCount) + " C#");
    }
}

bool ModManager::IsRelevantModule(const std::wstring& modPath) const {
    static const std::wstring systemDirs[] = { L"\\windows\\", L"\\system32\\", L"\\syswow64\\" };
    static const std::wstring ignorePrefixes[] = { L"msvc", L"d3d", L"xinput", L"vcruntime" };

    std::wstring lowerPath = modPath;
    std::transform(lowerPath.begin(), lowerPath.end(), lowerPath.begin(), ::tolower);

    for (const auto& sysDir : systemDirs) {
        if (lowerPath.find(sysDir) != std::wstring::npos) return false;
    }

    for (const auto& prefix : ignorePrefixes) {
        if (lowerPath.find(prefix) != std::wstring::npos) return false;
    }

    return (lowerPath.find(L".asi") != std::wstring::npos ||
        lowerPath.find(L"\\scripts\\") != std::wstring::npos ||
        lowerPath.find(L"\\mods\\") != std::wstring::npos ||
        (lowerPath.find(Utils::GetGameDirectory()) != std::wstring::npos &&
            lowerPath.find(L".dll") != std::wstring::npos));
}

std::string ModManager::DetermineModType(const std::wstring& modPath) const {
    std::wstring lowerPath = modPath;
    std::transform(lowerPath.begin(), lowerPath.end(), lowerPath.begin(), ::tolower);

    if (lowerPath.find(L"scripthookv.dll") != std::wstring::npos) return "scripthook";
    if (lowerPath.find(L"scripthookvdotnet") != std::wstring::npos) return "dotnet";
    if (lowerPath.find(L"\\scripts\\") != std::wstring::npos) return "script";
    if (lowerPath.find(L".asi") != std::wstring::npos) return "asi_external";
    if (lowerPath.find(L"\\mods\\") != std::wstring::npos) return "mod";

    return "external";
}

bool ModManager::IsModuleAlreadyTracked(HMODULE hModule) const {
    for (const auto& mod : Globals::g_LoadedMods) {
        if (mod.handle == hModule) return true;
    }
    return false;
}

void ModManager::ScanLoadedModules() {
    HANDLE hSnapshot = CreateToolhelp32Snapshot(TH32CS_SNAPMODULE | TH32CS_SNAPMODULE32, GetCurrentProcessId());
    if (hSnapshot == INVALID_HANDLE_VALUE) return;

    MODULEENTRY32W me32;
    me32.dwSize = sizeof(MODULEENTRY32W);

    int newModsFound = 0;

    if (Module32FirstW(hSnapshot, &me32)) {
        do {
            std::wstring modPath = me32.szExePath;
            HMODULE hModule = me32.hModule;

            {
                std::lock_guard<std::mutex> cacheLock(m_ScanCacheMutex);
                if (m_ScannedModules.find(modPath) != m_ScannedModules.end()) {
                    continue;
                }
            }

            if (!IsRelevantModule(modPath)) {
                std::lock_guard<std::mutex> cacheLock(m_ScanCacheMutex);
                m_ScannedModules.insert(modPath);
                continue;
            }

            std::lock_guard<std::mutex> lock(Globals::g_ModsMutex);

            if (IsModuleAlreadyTracked(hModule)) continue;

            std::string modId = Utils::GenerateModID(modPath);
            std::string modType = DetermineModType(modPath);

            ModInfo mod;
            mod.name = me32.szModule;
            mod.path = modPath;
            mod.handle = hModule;
            mod.loaded = true;
            mod.type = modType;
            mod.loadTime = std::chrono::system_clock::now();
            mod.size = me32.modBaseSize;
            mod.id = modId;

            Globals::g_LoadedMods.push_back(mod);
            newModsFound++;

            LOG_INFO("Detected: " + Utils::WStringToString(me32.szModule) + " (" + modType + ")");

            {
                std::lock_guard<std::mutex> cacheLock(m_ScanCacheMutex);
                m_ScannedModules.insert(modPath);
            }

        } while (Module32NextW(hSnapshot, &me32));
    }

    CloseHandle(hSnapshot);

    if (newModsFound > 0) {
        LOG_DEBUG("Scan complete: " + std::to_string(newModsFound) + " new modules");
    }
}

void ModManager::ModuleScanThread() {
    Sleep(10000);

    while (m_ScanRunning && !Globals::g_ShuttingDown) {
        ScanLoadedModules();

        for (int i = 0; i < 150 && m_ScanRunning && !Globals::g_ShuttingDown; i++) {
            Sleep(100);
        }
    }
}

void ModManager::StartModuleScanThread() {
    if (m_ScanRunning) return;

    m_ScanRunning = true;
    m_ScanThread = std::thread(&ModManager::ModuleScanThread, this);
    LOG_INFO("Module scanner started");
}

void ModManager::StopModuleScanThread() {
    if (!m_ScanRunning) return;

    m_ScanRunning = false;
    if (m_ScanThread.joinable()) {
        m_ScanThread.join();
    }
    LOG_INFO("Module scanner stopped");
}

const std::vector<ModInfo>& ModManager::GetLoadedMods() const {
    return Globals::g_LoadedMods;
}

std::string ModManager::SerializeModsJSON() const {
    std::lock_guard<std::mutex> lock(Globals::g_ModsMutex);
    std::ostringstream json;
    json << "{\"mods\":[";

    for (size_t i = 0; i < Globals::g_LoadedMods.size(); i++) {
        const auto& mod = Globals::g_LoadedMods[i];

        auto time_t_val = std::chrono::system_clock::to_time_t(mod.loadTime);
        struct tm timeinfo;
        localtime_s(&timeinfo, &time_t_val);
        char timeStr[64];
        strftime(timeStr, sizeof(timeStr), "%Y-%m-%d %H:%M:%S", &timeinfo);

        json << "{\"id\":\"" << mod.id << "\","
            << "\"name\":\"" << Utils::WStringToString(mod.name) << "\","
            << "\"path\":\"" << Utils::WStringToString(mod.path) << "\","
            << "\"loaded\":" << (mod.loaded ? "true" : "false") << ","
            << "\"type\":\"" << mod.type << "\","
            << "\"size\":" << mod.size << ","
            << "\"call_count\":" << mod.callCount << ","
            << "\"avg_execution_us\":" << (mod.callCount > 0 ? mod.totalExecutionTime.count() / mod.callCount : 0) << ","
            << "\"load_time\":\"" << timeStr << "\"}";

        if (i < Globals::g_LoadedMods.size() - 1) json << ",";
    }

    json << "],\"count\":" << Globals::g_LoadedMods.size() << "}";
    return json.str();
}