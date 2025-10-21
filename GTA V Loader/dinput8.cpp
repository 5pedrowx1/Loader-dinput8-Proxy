// ============================================================================
// GTA V Mod Loader - dinput8.dll Proxy
// by 5pedrowx1
// ============================================================================

#include "pch.h"

#define _CRT_SECURE_NO_WARNINGS

#include <windows.h>
#include <string>
#include <vector>
#include <mutex>
#include <atomic>
#include <fstream>
#include <sstream>
#include <filesystem>
#include <chrono>
#include <thread>
#include <deque>
#include <metahost.h>

#pragma comment(lib, "mscoree.lib")

namespace fs = std::filesystem;

// ============================================================================
// ESTRUTURAS DE DADOS
// ============================================================================

struct LoaderConfig {
    bool enableUI = true;
    bool autoLaunchUI = true;
    std::string uiExecutableName = "GTAVModManager.exe";
    bool autoLoadMods = true;
    bool loadScriptHookV = true;
    bool loadScriptHookVDotNet = false;
    bool autoLoadScripts = true;
    int maxLogEntries = 500;
};

struct ModInfo {
    std::wstring name;
    std::wstring path;
    HMODULE handle;
    bool loaded;
    std::string type;
    std::chrono::system_clock::time_point loadTime;
    size_t size;
    std::string id;
};

struct LogEntry {
    std::string message;
    std::string timestamp;
    std::string level;
};

struct IPCMessage {
    char command[32];
    char data[512];
};

// ============================================================================
// VARIÁVEIS GLOBAIS
// ============================================================================

HMODULE g_hOriginalDInput = nullptr;
LoaderConfig g_Config;
std::vector<ModInfo> g_LoadedMods;
std::deque<LogEntry> g_LogHistory;
std::mutex g_ModsMutex;
std::mutex g_LogMutex;
std::atomic<bool> g_ShuttingDown{ false };
std::chrono::steady_clock::time_point g_StartTime;

HANDLE g_hPipe = INVALID_HANDLE_VALUE;
std::thread g_PipeThread;
std::atomic<bool> g_PipeRunning{ false };

PROCESS_INFORMATION g_UIProcessInfo = { 0 };
bool g_UILaunched = false;

ICLRMetaHost* g_pMetaHost = nullptr;
ICLRRuntimeInfo* g_pRuntimeInfo = nullptr;
ICLRRuntimeHost* g_pRuntimeHost = nullptr;

// ============================================================================
// UTILITÁRIOS
// ============================================================================

std::wstring StringToWString(const std::string& str) {
    if (str.empty()) return std::wstring();
    int size = MultiByteToWideChar(CP_UTF8, 0, str.c_str(), -1, nullptr, 0);
    std::wstring wstr(size, 0);
    MultiByteToWideChar(CP_UTF8, 0, str.c_str(), -1, &wstr[0], size);
    return wstr;
}

std::string WStringToString(const std::wstring& wstr) {
    if (wstr.empty()) return std::string();
    int size = WideCharToMultiByte(CP_UTF8, 0, wstr.c_str(), -1, nullptr, 0, nullptr, nullptr);
    std::string str(size, 0);
    WideCharToMultiByte(CP_UTF8, 0, wstr.c_str(), -1, &str[0], size, nullptr, nullptr);
    return str;
}

bool FileExists(const wchar_t* path) {
    DWORD attrib = GetFileAttributesW(path);
    return (attrib != INVALID_FILE_ATTRIBUTES && !(attrib & FILE_ATTRIBUTE_DIRECTORY));
}

bool DirectoryExists(const wchar_t* path) {
    DWORD attrib = GetFileAttributesW(path);
    return (attrib != INVALID_FILE_ATTRIBUTES && (attrib & FILE_ATTRIBUTE_DIRECTORY));
}

std::wstring GetGameDirectory() {
    wchar_t gamePath[MAX_PATH];
    GetModuleFileNameW(NULL, gamePath, MAX_PATH);
    std::wstring gameDir = gamePath;
    size_t pos = gameDir.find_last_of(L"\\/");
    if (pos != std::wstring::npos) gameDir = gameDir.substr(0, pos + 1);
    return gameDir;
}

size_t GetFileSize(const std::wstring& path) {
    WIN32_FILE_ATTRIBUTE_DATA fad;
    if (GetFileAttributesExW(path.c_str(), GetFileExInfoStandard, &fad)) {
        LARGE_INTEGER size;
        size.HighPart = fad.nFileSizeHigh;
        size.LowPart = fad.nFileSizeLow;
        return static_cast<size_t>(size.QuadPart);
    }
    return 0;
}

std::string GenerateModID(const std::wstring& path) {
    std::string pathStr = WStringToString(path);
    std::hash<std::string> hasher;
    return std::to_string(hasher(pathStr));
}

// ============================================================================
// SISTEMA DE LOGGING
// ============================================================================

void LogMessage(const std::string& msg, const std::string& level = "INFO") {
    std::lock_guard<std::mutex> lock(g_LogMutex);

    auto now = std::chrono::system_clock::now();
    auto time_t_val = std::chrono::system_clock::to_time_t(now);

    struct tm timeinfo;
    localtime_s(&timeinfo, &time_t_val);

    char timeStr[32];
    strftime(timeStr, sizeof(timeStr), "%H:%M:%S", &timeinfo);

    LogEntry entry;
    entry.message = msg;
    entry.timestamp = timeStr;
    entry.level = level;

    g_LogHistory.push_back(entry);

    if (g_LogHistory.size() > g_Config.maxLogEntries) {
        g_LogHistory.pop_front();
    }

    std::string fullMsg = std::string("[") + timeStr + "] [" + level + "] " + msg;

    try {
        std::ofstream logFile("logs/loader.log", std::ios::app);
        if (logFile.is_open()) {
            logFile << fullMsg << std::endl;
        }
    }
    catch (...) {}

    OutputDebugStringA(fullMsg.c_str());
}

// ============================================================================
// DINPUT8 PROXY
// ============================================================================

void LoadOriginalDInput() {
    if (!g_hOriginalDInput) {
        char sysPath[MAX_PATH];
        GetSystemDirectoryA(sysPath, MAX_PATH);
        strcat_s(sysPath, "\\dinput8.dll");
        g_hOriginalDInput = LoadLibraryA(sysPath);

        if (!g_hOriginalDInput) {
            LogMessage("Nao foi possivel carregar dinput8.dll original!", "ERROR");
        }
        else {
            LogMessage("dinput8.dll original carregado", "SUCCESS");
        }
    }
}

extern "C" __declspec(dllexport)
HRESULT WINAPI DirectInput8Create(
    HINSTANCE hinst,
    DWORD dwVersion,
    REFIID riidltf,
    LPVOID* ppvOut,
    LPUNKNOWN punkOuter
) {
    LoadOriginalDInput();
    if (!g_hOriginalDInput) return E_FAIL;

    auto pFunc = (HRESULT(WINAPI*)(HINSTANCE, DWORD, REFIID, LPVOID*, LPUNKNOWN))
        GetProcAddress(g_hOriginalDInput, "DirectInput8Create");

    return pFunc ? pFunc(hinst, dwVersion, riidltf, ppvOut, punkOuter) : E_FAIL;
}

// ============================================================================
// GERENCIADOR DE MODS
// ============================================================================

bool LoadModFile(const fs::path& modPath, const std::string& type = "mod") {
    std::lock_guard<std::mutex> lock(g_ModsMutex);

    std::string modId = GenerateModID(modPath.wstring());

    for (const auto& mod : g_LoadedMods) {
        if (mod.id == modId) {
            LogMessage("Mod ja carregado: " + modPath.filename().string(), "WARNING");
            return true;
        }
    }

    HMODULE hMod = LoadLibraryW(modPath.c_str());
    if (!hMod) {
        DWORD err = GetLastError();
        LogMessage("Falha ao carregar: " + modPath.filename().string() +
            " (Erro: " + std::to_string(err) + ")", "ERROR");

        switch (err) {
        case 126: LogMessage("  -> Modulo ou dependencia nao encontrada", "ERROR"); break;
        case 127: LogMessage("  -> Procedimento nao encontrado", "ERROR"); break;
        case 193: LogMessage("  -> Nao e um aplicativo Win32/64 valido", "ERROR"); break;
        case 998: LogMessage("  -> Acesso invalido a memoria", "ERROR"); break;
        }
        return false;
    }

    auto initFunc = GetProcAddress(hMod, "Initialize");
    if (!initFunc) initFunc = GetProcAddress(hMod, "Init");
    if (!initFunc) initFunc = GetProcAddress(hMod, "Startup");
    if (!initFunc) initFunc = GetProcAddress(hMod, "OnLoad");

    if (initFunc) {
        try {
            typedef void (*InitFuncType)();
            ((InitFuncType)initFunc)();
            LogMessage("  -> Funcao de inicializacao chamada", "INFO");
        }
        catch (...) {
            LogMessage("  -> Excecao ao chamar inicializacao", "WARNING");
        }
    }

    ModInfo mod;
    mod.name = modPath.filename().wstring();
    mod.path = modPath.wstring();
    mod.handle = hMod;
    mod.loaded = true;
    mod.type = type;
    mod.loadTime = std::chrono::system_clock::now();
    mod.size = GetFileSize(modPath.wstring());
    mod.id = modId;

    g_LoadedMods.push_back(mod);
    LogMessage("Carregado: " + modPath.filename().string() +
        " (" + std::to_string(mod.size / 1024) + " KB)", "SUCCESS");

    return true;
}

bool UnloadMod(const std::string& modId) {
    std::lock_guard<std::mutex> lock(g_ModsMutex);

    for (auto it = g_LoadedMods.begin(); it != g_LoadedMods.end(); ++it) {
        if (it->id == modId) {
            if (it->handle) {
                auto cleanupFunc = GetProcAddress(it->handle, "Cleanup");
                if (!cleanupFunc) cleanupFunc = GetProcAddress(it->handle, "Shutdown");
                if (!cleanupFunc) cleanupFunc = GetProcAddress(it->handle, "OnUnload");

                if (cleanupFunc) {
                    try {
                        typedef void (*CleanupFuncType)();
                        ((CleanupFuncType)cleanupFunc)();
                        LogMessage("  -> Funcao de cleanup chamada", "INFO");
                    }
                    catch (...) {
                        LogMessage("  -> Excecao ao chamar cleanup", "WARNING");
                    }
                }

                if (FreeLibrary(it->handle)) {
                    LogMessage("Mod descarregado: " + WStringToString(it->name), "SUCCESS");
                    g_LoadedMods.erase(it);
                    return true;
                }
                else {
                    LogMessage("Falha ao descarregar mod: " + WStringToString(it->name), "ERROR");
                    return false;
                }
            }
        }
    }

    LogMessage("Mod nao encontrado para descarregar: " + modId, "WARNING");
    return false;
}

void ScanAndLoadMods() {
    LogMessage("Escaneando pasta mods/...", "INFO");

    if (!fs::exists("mods")) {
        fs::create_directories("mods");
        LogMessage("Pasta mods/ criada", "INFO");
        return;
    }

    int count = 0;
    for (const auto& entry : fs::directory_iterator("mods")) {
        if (entry.path().extension() == ".asi" || entry.path().extension() == ".dll") {
            if (LoadModFile(entry.path(), "mod")) {
                count++;
            }
        }
    }

    LogMessage("Total de mods carregados: " + std::to_string(count), "INFO");
}

void ScanScriptsFolder() {
    std::wstring scriptsPath = GetGameDirectory() + L"scripts\\";

    if (!DirectoryExists(scriptsPath.c_str())) {
        CreateDirectoryW(scriptsPath.c_str(), NULL);
        LogMessage("Pasta scripts/ criada", "INFO");
        return;
    }

    LogMessage("Escaneando pasta scripts/...", "INFO");

    std::wstring searchDLL = scriptsPath + L"*.dll";
    WIN32_FIND_DATAW findData;
    HANDLE hFind = FindFirstFileW(searchDLL.c_str(), &findData);
    int dllCount = 0;

    if (hFind != INVALID_HANDLE_VALUE) {
        do {
            if (!(findData.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY)) {
                LogMessage("  -> DLL: " + WStringToString(findData.cFileName), "INFO");
                dllCount++;
            }
        } while (FindNextFileW(hFind, &findData));
        FindClose(hFind);
    }

    std::wstring searchCS = scriptsPath + L"*.cs";
    hFind = FindFirstFileW(searchCS.c_str(), &findData);
    int csCount = 0;

    if (hFind != INVALID_HANDLE_VALUE) {
        do {
            if (!(findData.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY)) {
                csCount++;
            }
        } while (FindNextFileW(hFind, &findData));
        FindClose(hFind);
    }

    int total = dllCount + csCount;
    if (total > 0) {
        LogMessage("Scripts encontrados: " + std::to_string(dllCount) + " DLL, " +
            std::to_string(csCount) + " C#", "INFO");
    }
}

// ============================================================================
// SCRIPTHOOK
// ============================================================================

bool LoadModule(const wchar_t* moduleName, const std::string& type) {
    std::wstring fullPath = GetGameDirectory() + moduleName;

    if (!FileExists(fullPath.c_str())) {
        LogMessage("Nao encontrado: " + WStringToString(moduleName), "ERROR");
        return false;
    }

    return LoadModFile(fullPath, type);
}

void LoadScriptHookModules() {
    LogMessage("========================================", "INFO");
    LogMessage("Carregando modulos ScriptHook...", "INFO");
    LogMessage("========================================", "INFO");

    bool dotnetLoaded = false;

    if (g_Config.loadScriptHookVDotNet) {
        if (LoadModule(L"ScriptHookVDotNet.asi", "dotnet")) {
            LogMessage("ScriptHookVDotNet carregado!", "SUCCESS");
            dotnetLoaded = true;
        }
    }

    LogMessage("========================================", "INFO");

    if (dotnetLoaded && g_Config.autoLoadScripts) {
        ScanScriptsFolder();
    }
}

// ============================================================================
// CONFIGURAÇÃO
// ============================================================================

void LoadConfig() {
    try {
        std::wstring configPath = GetGameDirectory() + L"dinput8_config.ini";

        g_Config.enableUI = GetPrivateProfileIntW(L"UI", L"Enable", 1, configPath.c_str()) != 0;
        g_Config.autoLaunchUI = GetPrivateProfileIntW(L"UI", L"AutoLaunch", 1, configPath.c_str()) != 0;

        wchar_t exeName[256];
        GetPrivateProfileStringW(L"UI", L"ExecutableName", L"GTAVModManager.exe", exeName, 256, configPath.c_str());
        g_Config.uiExecutableName = WStringToString(exeName);

        g_Config.autoLoadMods = GetPrivateProfileIntW(L"AutoLoad", L"AutoLoadModsFolder", 1, configPath.c_str()) != 0;
        g_Config.autoLoadScripts = GetPrivateProfileIntW(L"AutoLoad", L"AutoLoadScriptsFolder", 1, configPath.c_str()) != 0;

        g_Config.loadScriptHookV = GetPrivateProfileIntW(L"ScriptHook", L"LoadScriptHookV", 1, configPath.c_str()) != 0;
        g_Config.loadScriptHookVDotNet = GetPrivateProfileIntW(L"ScriptHook", L"LoadScriptHookVDotNet", 0, configPath.c_str()) != 0;
        g_Config.maxLogEntries = GetPrivateProfileIntW(L"Logging", L"MaxLogEntries", 500, configPath.c_str());

        LogMessage("Configuracao carregada", "SUCCESS");
    }
    catch (...) {
        LogMessage("Usando configuracao padrao", "WARNING");
    }
}

void CreateDefaultConfig() {
    std::wstring configPath = GetGameDirectory() + L"dinput8_config.ini";

    if (!FileExists(configPath.c_str())) {
        WritePrivateProfileStringW(L"UI", L"Enable", L"1", configPath.c_str());
        WritePrivateProfileStringW(L"UI", L"AutoLaunch", L"1", configPath.c_str());
        WritePrivateProfileStringW(L"UI", L"ExecutableName", L"GTAVModManager.exe", configPath.c_str());
        WritePrivateProfileStringW(L"ScriptHook", L"LoadScriptHookV", L"0", configPath.c_str());
        WritePrivateProfileStringW(L"ScriptHook", L"LoadScriptHookVDotNet", L"0", configPath.c_str());
        WritePrivateProfileStringW(L"AutoLoad", L"AutoLoadModsFolder", L"1", configPath.c_str());
        WritePrivateProfileStringW(L"AutoLoad", L"AutoLoadScriptsFolder", L"1", configPath.c_str());
        WritePrivateProfileStringW(L"Logging", L"MaxLogEntries", L"500", configPath.c_str());

        LogMessage("Configuracao padrao criada!", "INFO");
    }
}

// ============================================================================
// NAMED PIPE SERVER
// ============================================================================

std::string SerializeModsJSON() {
    std::lock_guard<std::mutex> lock(g_ModsMutex);

    std::ostringstream json;
    json << "{\"mods\":[";

    for (size_t i = 0; i < g_LoadedMods.size(); i++) {
        const auto& mod = g_LoadedMods[i];

        auto time_t_val = std::chrono::system_clock::to_time_t(mod.loadTime);
        struct tm timeinfo;
        localtime_s(&timeinfo, &time_t_val);
        char timeStr[64];
        strftime(timeStr, sizeof(timeStr), "%Y-%m-%d %H:%M:%S", &timeinfo);

        json << "{";
        json << "\"id\":\"" << mod.id << "\",";
        json << "\"name\":\"" << WStringToString(mod.name) << "\",";
        json << "\"path\":\"" << WStringToString(mod.path) << "\",";
        json << "\"loaded\":" << (mod.loaded ? "true" : "false") << ",";
        json << "\"type\":\"" << mod.type << "\",";
        json << "\"size\":" << mod.size << ",";
        json << "\"load_time\":\"" << timeStr << "\"";
        json << "}";

        if (i < g_LoadedMods.size() - 1) json << ",";
    }

    json << "],\"count\":" << g_LoadedMods.size() << "}";
    return json.str();
}

std::string SerializeStatusJSON() {
    auto uptime = std::chrono::duration_cast<std::chrono::seconds>(
        std::chrono::steady_clock::now() - g_StartTime
    ).count();

    std::lock_guard<std::mutex> lock(g_ModsMutex);

    std::ostringstream json;
    json << "{";
    json << "\"version\":\"2.0.0\",";
    json << "\"uptime_seconds\":" << uptime << ",";
    json << "\"mods_loaded\":" << g_LoadedMods.size() << ",";
    json << "\"server_running\":" << (g_PipeRunning ? "true" : "false") << ",";
    json << "\"game_detected\":" << (FindWindowA("grcWindow", nullptr) ? "true" : "false");
    json << "}";

    return json.str();
}

std::string SerializeLogsJSON() {
    std::lock_guard<std::mutex> lock(g_LogMutex);

    std::ostringstream json;
    json << "{\"logs\":[";

    for (size_t i = 0; i < g_LogHistory.size(); i++) {
        const auto& log = g_LogHistory[i];
        json << "{";
        json << "\"message\":\"" << log.message << "\",";
        json << "\"timestamp\":\"" << log.timestamp << "\",";
        json << "\"level\":\"" << log.level << "\"";
        json << "}";

        if (i < g_LogHistory.size() - 1) json << ",";
    }

    json << "],\"count\":" << g_LogHistory.size() << "}";
    return json.str();
}

void HandleIPCCommand(const IPCMessage& msg) {
    std::string cmd = msg.command;
    std::string data = msg.data;

    std::string response;

    if (cmd == "GET_MODS") {
        response = SerializeModsJSON();
    }
    else if (cmd == "LOAD_MOD") {
        std::wstring wpath = StringToWString(data);
        bool success = LoadModFile(wpath, "mod");
        response = success ? "SUCCESS" : "FAILED";
    }
    else if (cmd == "UNLOAD_MOD") {
        bool success = UnloadMod(data);
        response = success ? "SUCCESS" : "FAILED";
    }
    else if (cmd == "GET_STATUS") {
        response = SerializeStatusJSON();
    }
    else if (cmd == "GET_LOGS") {
        response = SerializeLogsJSON();
    }
    else if (cmd == "SCAN_FOLDER") {
        ScanAndLoadMods();
        response = "SUCCESS";
    }
    else if (cmd == "RELOAD_ALL") {
        std::vector<std::string> modsToReload;
        std::vector<std::string> modTypes;
        std::vector<std::wstring> modPaths;

        {
            std::lock_guard<std::mutex> lock(g_ModsMutex);
            for (const auto& mod : g_LoadedMods) {
                if (mod.type != "scripthook" && mod.type != "dotnet") {
                    modsToReload.push_back(mod.id);
                    modTypes.push_back(mod.type);
                    modPaths.push_back(mod.path);
                }
            }
        }

        int unloaded = 0;
        for (const auto& modId : modsToReload) {
            if (UnloadMod(modId)) unloaded++;
        }

        Sleep(1000);

        int reloaded = 0;
        for (size_t i = 0; i < modPaths.size(); i++) {
            if (LoadModFile(modPaths[i], modTypes[i])) reloaded++;
        }

        response = "SUCCESS:" + std::to_string(reloaded) + "/" + std::to_string(modsToReload.size());
    }
    else {
        response = "UNKNOWN_COMMAND";
    }

    DWORD written;
    WriteFile(g_hPipe, response.c_str(), (DWORD)response.length(), &written, NULL);
}

void NamedPipeServerThread() {
    const wchar_t* pipeName = L"\\\\.\\pipe\\GTAVModLoader";

    while (g_PipeRunning) {
        g_hPipe = CreateNamedPipeW(
            pipeName,
            PIPE_ACCESS_DUPLEX,
            PIPE_TYPE_MESSAGE | PIPE_READMODE_MESSAGE | PIPE_WAIT,
            PIPE_UNLIMITED_INSTANCES,
            8192,
            8192,
            0,
            NULL
        );

        if (g_hPipe == INVALID_HANDLE_VALUE) {
            LogMessage("Erro ao criar Named Pipe", "ERROR");
            Sleep(1000);
            continue;
        }

        LogMessage("Named Pipe aguardando conexao...", "INFO");

        if (ConnectNamedPipe(g_hPipe, NULL) || GetLastError() == ERROR_PIPE_CONNECTED) {
            LogMessage("Cliente conectado ao Named Pipe", "SUCCESS");

            while (g_PipeRunning) {
                IPCMessage msg = { 0 };
                DWORD bytesRead;

                BOOL success = ReadFile(
                    g_hPipe,
                    &msg,
                    sizeof(IPCMessage),
                    &bytesRead,
                    NULL
                );

                if (success && bytesRead > 0) {
                    HandleIPCCommand(msg);
                }
                else {
                    break;
                }
            }

            DisconnectNamedPipe(g_hPipe);
        }

        CloseHandle(g_hPipe);
        g_hPipe = INVALID_HANDLE_VALUE;
    }
}

void StartNamedPipeServer() {
    if (g_PipeRunning) return;

    LogMessage("Iniciando Named Pipe Server...", "INFO");
    g_PipeRunning = true;
    g_PipeThread = std::thread(NamedPipeServerThread);
    LogMessage("Named Pipe Server iniciado", "SUCCESS");
}

void StopNamedPipeServer() {
    if (!g_PipeRunning) return;

    LogMessage("Parando Named Pipe Server...", "INFO");
    g_PipeRunning = false;

    if (g_hPipe != INVALID_HANDLE_VALUE) {
        CloseHandle(g_hPipe);
        g_hPipe = INVALID_HANDLE_VALUE;
    }

    if (g_PipeThread.joinable()) {
        g_PipeThread.join();
    }

    LogMessage("Named Pipe Server parado", "SUCCESS");
}

// ============================================================================
// UI LAUNCHER
// ============================================================================

bool LaunchUI() {
    if (!g_Config.enableUI || !g_Config.autoLaunchUI) {
        LogMessage("UI desabilitada nas configuracoes", "INFO");
        return false;
    }

    std::wstring uiPath = GetGameDirectory() + StringToWString(g_Config.uiExecutableName);

    if (!FileExists(uiPath.c_str())) {
        LogMessage("UI nao encontrada: " + g_Config.uiExecutableName, "WARNING");
        return false;
    }

    STARTUPINFOW si = { 0 };
    si.cb = sizeof(si);

    LogMessage("Lancando UI: " + g_Config.uiExecutableName, "INFO");

    if (CreateProcessW(
        uiPath.c_str(),
        NULL,
        NULL,
        NULL,
        FALSE,
        0,
        NULL,
        GetGameDirectory().c_str(),
        &si,
        &g_UIProcessInfo
    )) {
        LogMessage("UI lancada com sucesso!", "SUCCESS");
        g_UILaunched = true;
        return true;
    }
    else {
        DWORD err = GetLastError();
        LogMessage("Falha ao lancar UI (Erro: " + std::to_string(err) + ")", "ERROR");
        return false;
    }
}

void CloseUI() {
    if (g_UILaunched && g_UIProcessInfo.hProcess) {
        LogMessage("Fechando UI...", "INFO");

        DWORD exitCode;
        if (GetExitCodeProcess(g_UIProcessInfo.hProcess, &exitCode) && exitCode == STILL_ACTIVE) {
            TerminateProcess(g_UIProcessInfo.hProcess, 0);
            WaitForSingleObject(g_UIProcessInfo.hProcess, 3000);
        }

        CloseHandle(g_UIProcessInfo.hProcess);
        CloseHandle(g_UIProcessInfo.hThread);
        g_UILaunched = false;

        LogMessage("UI fechada", "SUCCESS");
    }
}

// ============================================================================
// DETECÇÃO DO JOGO
// ============================================================================

bool IsGameFullyLoaded() {
    HWND hwnd = FindWindowW(L"grcWindow", NULL);
    if (!hwnd || !IsWindowVisible(hwnd) || IsIconic(hwnd))
        return false;

    wchar_t title[256] = { 0 };
    GetWindowTextW(hwnd, title, 256);
    return wcslen(title) > 0;
}

// ============================================================================
// THREAD DE INICIALIZAÇÃO
// ============================================================================

DWORD WINAPI InitThread(LPVOID) {
    g_StartTime = std::chrono::steady_clock::now();

    LogMessage("========================================", "INFO");
    LogMessage("GTA V Ultimate Mod Loader v2.0", "INFO");
    LogMessage("========================================", "INFO");

    fs::create_directories("logs");
    fs::create_directories("mods");
    fs::create_directories("scripts");

    CreateDefaultConfig();
    LoadConfig();

    LogMessage("Aguardando jogo...", "INFO");
    int wait = 0;
    while (wait < 60 && !g_ShuttingDown) {
        if (IsGameFullyLoaded()) {
            LogMessage("Jogo detectado", "SUCCESS");
            Sleep(5000);
            if (IsGameFullyLoaded()) break;
        }
        Sleep(1000);
        wait++;
    }

    LoadScriptHookModules();

    if (g_Config.autoLoadMods) {
        ScanAndLoadMods();
    }

    StartNamedPipeServer();

    if (g_Config.enableUI && g_Config.autoLaunchUI) {
        Sleep(2000);
        LaunchUI();
    }

    LogMessage("========================================", "INFO");
    LogMessage(">>> LOADER PRONTO <<<", "SUCCESS");
    LogMessage("========================================", "INFO");

    Beep(600, 150);
    Sleep(100);
    Beep(800, 150);

    return 0;
}

// ============================================================================
// DLL ENTRY POINT
// ============================================================================

BOOL APIENTRY DllMain(HMODULE hModule, DWORD reason, LPVOID) {
    if (reason == DLL_PROCESS_ATTACH) {
        DisableThreadLibraryCalls(hModule);
        HANDLE hThread = CreateThread(nullptr, 0, InitThread, nullptr, 0, nullptr);
        if (hThread) CloseHandle(hThread);
    }
    else if (reason == DLL_PROCESS_DETACH) {
        g_ShuttingDown = true;

        LogMessage("Shutdown...", "INFO");
        CloseUI();
        StopNamedPipeServer();

        {
            std::lock_guard<std::mutex> lock(g_ModsMutex);
            for (auto& mod : g_LoadedMods) {
                if (mod.handle) {
                    FreeLibrary(mod.handle);
                }
            }
            g_LoadedMods.clear();
        }

        if (g_hOriginalDInput) {
            FreeLibrary(g_hOriginalDInput);
        }

        LogMessage("Completo", "SUCCESS");
    }

    return TRUE;
}