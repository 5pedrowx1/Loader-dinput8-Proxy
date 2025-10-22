// ============================================================================
// GTA V Mod Loader - dinput8.dll Proxy
// by 5pedrowx1
// Version: 2.1.0
// ============================================================================

#include "pch.h"
#include <filesystem>
#include <thread>
#include <chrono>
#include "Core.h"
#include "Logger.h"
#include "PerformanceMonitor.h"
#include "ModManager.h"
#include "ConfigManager.h"
#include "IPCServer.h"

#pragma comment(lib, "psapi.lib")
#pragma comment(lib, "mscoree.lib")
#pragma comment(lib, "dinput8.lib")
#pragma comment(lib, "dxguid.lib")

namespace fs = std::filesystem;
static HMODULE g_hOriginalDInput = nullptr;

void LoadOriginalDInput() {
    if (!g_hOriginalDInput) {
        char sysPath[MAX_PATH];
        GetSystemDirectoryA(sysPath, MAX_PATH);
        strcat_s(sysPath, "\\dinput8.dll");
        g_hOriginalDInput = LoadLibraryA(sysPath);

        if (!g_hOriginalDInput) {
            LOG_ERROR("Failed to load original dinput8.dll");
        }
        else {
            LOG_SUCCESS("Original dinput8.dll loaded");
        }
    }
}

typedef HRESULT(WINAPI* DirectInput8CreateFunc)(HINSTANCE, DWORD, REFIID, LPVOID*, LPUNKNOWN);

extern "C" HRESULT WINAPI DirectInput8Create(
    HINSTANCE hinst,
    DWORD dwVersion,
    REFIID riidltf,
    LPVOID* ppvOut,
    LPUNKNOWN punkOuter
) {
    LoadOriginalDInput();
    if (!g_hOriginalDInput) return E_FAIL;

    DirectInput8CreateFunc pFunc = (DirectInput8CreateFunc)GetProcAddress(g_hOriginalDInput, "DirectInput8Create");

    return pFunc ? pFunc(hinst, dwVersion, riidltf, ppvOut, punkOuter) : E_FAIL;
}

class ScriptHookLoader {
public:
    static void LoadModules() {
        LOG_INFO("Loading ScriptHook modules...");

        if (Globals::g_Config.loadScriptHookV) {
            if (LoadModule(L"ScriptHookV.dll", "scripthook")) {
                LOG_SUCCESS("ScriptHookV loaded");
            }
        }

        if (Globals::g_Config.loadScriptHookVDotNet) {
            if (LoadModule(L"ScriptHookVDotNet.asi", "dotnet")) {
                LOG_SUCCESS("ScriptHookVDotNet loaded");

                if (Globals::g_Config.autoLoadScripts) {
                    ModManager::Instance().ScanScriptsFolder();
                }
            }
        }
    }

private:
    static bool LoadModule(const wchar_t* moduleName, const std::string& type) {
        std::wstring fullPath = Utils::GetGameDirectory() + moduleName;
        if (!Utils::FileExists(fullPath.c_str())) {
            LOG_ERROR("Not found: " + Utils::WStringToString(moduleName));
            return false;
        }
        return ModManager::Instance().LoadMod(fullPath, type);
    }
};

class UILauncher {
public:
    static UILauncher& Instance() {
        static UILauncher instance;
        return instance;
    }

    bool Launch() {
        if (!Globals::g_Config.enableUI || !Globals::g_Config.autoLaunchUI) {
            return false;
        }

        std::wstring uiPath = Utils::GetGameDirectory() +
            Utils::StringToWString(Globals::g_Config.uiExecutableName);

        if (!Utils::FileExists(uiPath.c_str())) {
            LOG_WARNING("UI not found: " + Globals::g_Config.uiExecutableName);
            return false;
        }

        STARTUPINFOW si = { 0 };
        si.cb = sizeof(si);

        if (CreateProcessW(
            uiPath.c_str(),
            NULL, NULL, NULL, FALSE, 0, NULL,
            Utils::GetGameDirectory().c_str(),
            &si, &m_ProcessInfo
        )) {
            LOG_SUCCESS("UI launched: " + Globals::g_Config.uiExecutableName);
            m_UILaunched = true;
            return true;
        }

        LOG_ERROR("Failed to launch UI");
        return false;
    }

    void Close() {
        if (m_UILaunched && m_ProcessInfo.hProcess) {
            DWORD exitCode;
            if (GetExitCodeProcess(m_ProcessInfo.hProcess, &exitCode) && exitCode == STILL_ACTIVE) {
                TerminateProcess(m_ProcessInfo.hProcess, 0);
                WaitForSingleObject(m_ProcessInfo.hProcess, 3000);
            }

            CloseHandle(m_ProcessInfo.hProcess);
            CloseHandle(m_ProcessInfo.hThread);
            m_UILaunched = false;
            LOG_INFO("UI closed");
        }
    }

    bool IsRunning() const {
        if (!m_UILaunched || !m_ProcessInfo.hProcess) return false;

        DWORD exitCode;
        if (GetExitCodeProcess(m_ProcessInfo.hProcess, &exitCode)) {
            return exitCode == STILL_ACTIVE;
        }
        return false;
    }

private:
    UILauncher() = default;
    ~UILauncher() { Close(); }

    PROCESS_INFORMATION m_ProcessInfo = { 0 };
    bool m_UILaunched = false;
};

class GameDetector {
public:
    static bool IsGameFullyLoaded() {
        HWND hwnd = FindWindowW(L"grcWindow", NULL);
        if (!hwnd || !IsWindowVisible(hwnd) || IsIconic(hwnd))
            return false;

        wchar_t title[256] = { 0 };
        GetWindowTextW(hwnd, title, 256);
        return wcslen(title) > 0;
    }

    static bool WaitForGame(int maxWaitSeconds = 60) {
        LOG_INFO("Waiting for game to load...");

        int wait = 0;
        while (wait < maxWaitSeconds && !Globals::g_ShuttingDown) {
            if (IsGameFullyLoaded()) {
                LOG_SUCCESS("Game detected");
                Sleep(5000);
                if (IsGameFullyLoaded()) {
                    return true;
                }
            }
            Sleep(1000);
            wait++;
        }

        if (wait >= maxWaitSeconds) {
            LOG_WARNING("Game detection timeout");
        }
        return false;
    }
};

DWORD WINAPI InitThread(LPVOID) {
    Globals::g_StartTime = std::chrono::steady_clock::now();

    LOG_INFO("========================================");
    LOG_INFO("GTA V Mod Loader v2.1.0");
    LOG_INFO("by 5pedrowx1");
    LOG_INFO("========================================");

    try {
        fs::create_directories("logs");
        fs::create_directories("mods");
        fs::create_directories("scripts");
        LOG_SUCCESS("Directories verified");
    }
    catch (const std::exception& e) {
        LOG_ERROR("Failed to create directories: " + std::string(e.what()));
    }

    ConfigManager::Instance().CreateDefaultConfig();
    ConfigManager::Instance().LoadConfig();

    if (!GameDetector::WaitForGame(60)) {
        LOG_WARNING("Continuing without game detection...");
    }

    ScriptHookLoader::LoadModules();

    if (Globals::g_Config.autoLoadMods) {
        ModManager::Instance().ScanAndLoadModsFolder();
    }

    IPCServer::Instance().Start();
    Sleep(3000);
    ModManager::Instance().ScanLoadedModules();
    ModManager::Instance().StartModuleScanThread();

    if (Globals::g_Config.enablePerformanceMonitor) {
        PerformanceMonitor::Instance().Start();
    }

    if (Globals::g_Config.enableUI && Globals::g_Config.autoLaunchUI) {
        Sleep(2000);
        UILauncher::Instance().Launch();
    }

    LOG_SUCCESS("========================================");
    LOG_SUCCESS("Loader initialized successfully!");
    LOG_SUCCESS("Mods loaded: " + std::to_string(Globals::g_LoadedMods.size()));
    LOG_SUCCESS("========================================");

    return 0;
}

void Cleanup() {
    LOG_INFO("========================================");
    LOG_INFO("Shutting down GTA V Mod Loader...");
    LOG_INFO("========================================");

    Globals::g_ShuttingDown = true;

    ModManager::Instance().StopModuleScanThread();
    PerformanceMonitor::Instance().Stop();
    UILauncher::Instance().Close();
    IPCServer::Instance().Stop();

    {
        std::lock_guard<std::mutex> lock(Globals::g_ModsMutex);
        LOG_INFO("Unloading " + std::to_string(Globals::g_LoadedMods.size()) + " mods...");

        for (auto& mod : Globals::g_LoadedMods) {
            if (mod.handle && mod.type != "scripthook" && mod.type != "dotnet") {
                try {
                    FreeLibrary(mod.handle);
                    LOG_DEBUG("Unloaded: " + Utils::WStringToString(mod.name));
                }
                catch (...) {
                    LOG_WARNING("Exception unloading: " + Utils::WStringToString(mod.name));
                }
            }
        }
        Globals::g_LoadedMods.clear();
    }

    if (g_hOriginalDInput) {
        FreeLibrary(g_hOriginalDInput);
        g_hOriginalDInput = nullptr;
        LOG_DEBUG("Original dinput8.dll unloaded");
    }

    ConfigManager::Instance().SaveConfig();

    LOG_SUCCESS("Shutdown complete");
    LOG_INFO("========================================");
}

BOOL APIENTRY DllMain(HMODULE hModule, DWORD reason, LPVOID) {
    switch (reason) {
    case DLL_PROCESS_ATTACH:
    {
        DisableThreadLibraryCalls(hModule);

        HANDLE hThread = CreateThread(nullptr, 0, InitThread, nullptr, 0, nullptr);
        if (hThread) {
            CloseHandle(hThread);
        }
        else {
            MessageBoxA(NULL, "Failed to create initialization thread!", "GTA V Mod Loader", MB_ICONERROR);
        }
        break;
    }

    case DLL_PROCESS_DETACH:
        Cleanup();
        break;

    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
        // Nothing to do
        break;
    }

    return TRUE;
}