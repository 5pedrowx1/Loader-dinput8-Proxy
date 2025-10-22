#pragma once

#include "pch.h"
#include "Core.cpp"
#include "Logger.cpp"

class ConfigManager {
public:
    static ConfigManager& Instance() {
        static ConfigManager instance;
        return instance;
    }

    void LoadConfig();
    void SaveConfig();
    void CreateDefaultConfig();

    const LoaderConfig& GetConfig() const { return Globals::g_Config; }
    LoaderConfig& GetConfigMutable() { return Globals::g_Config; }

private:
    ConfigManager() = default;
    ~ConfigManager() = default;
    ConfigManager(const ConfigManager&) = delete;
    ConfigManager& operator=(const ConfigManager&) = delete;

    std::wstring GetConfigPath() const;
};

inline std::wstring ConfigManager::GetConfigPath() const {
    return Utils::GetGameDirectory() + L"dinput8_config.ini";
}

inline void ConfigManager::LoadConfig() {
    std::wstring configPath = GetConfigPath();

    try {
        Globals::g_Config.enableUI = GetPrivateProfileIntW(L"UI", L"Enable", 1, configPath.c_str()) != 0;
        Globals::g_Config.autoLaunchUI = GetPrivateProfileIntW(L"UI", L"AutoLaunch", 1, configPath.c_str()) != 0;

        wchar_t exeName[256];
        GetPrivateProfileStringW(L"UI", L"ExecutableName", L"GTAVModManager.exe", exeName, 256, configPath.c_str());
        Globals::g_Config.uiExecutableName = Utils::WStringToString(exeName);

        Globals::g_Config.autoLoadMods = GetPrivateProfileIntW(L"AutoLoad", L"AutoLoadModsFolder", 1, configPath.c_str()) != 0;
        Globals::g_Config.autoLoadScripts = GetPrivateProfileIntW(L"AutoLoad", L"AutoLoadScriptsFolder", 1, configPath.c_str()) != 0;

        Globals::g_Config.loadScriptHookV = GetPrivateProfileIntW(L"ScriptHook", L"LoadScriptHookV", 1, configPath.c_str()) != 0;
        Globals::g_Config.loadScriptHookVDotNet = GetPrivateProfileIntW(L"ScriptHook", L"LoadScriptHookVDotNet", 0, configPath.c_str()) != 0;

        Globals::g_Config.maxLogEntries = GetPrivateProfileIntW(L"Logging", L"MaxLogEntries", 500, configPath.c_str());
        Globals::g_Config.verboseLogging = GetPrivateProfileIntW(L"Logging", L"VerboseLogging", 0, configPath.c_str()) != 0;

        Globals::g_Config.enablePerformanceMonitor = GetPrivateProfileIntW(L"Performance", L"EnableMonitor", 1, configPath.c_str()) != 0;
        Globals::g_Config.performanceMonitorInterval = GetPrivateProfileIntW(L"Performance", L"MonitorIntervalMS", 5000, configPath.c_str());

        LOG_SUCCESS("Configuration loaded");
    }
    catch (...) {
        LOG_WARNING("Failed to load config, using defaults");
    }
}

inline void ConfigManager::SaveConfig() {
    std::wstring configPath = GetConfigPath();

    try {
        WritePrivateProfileStringW(L"UI", L"Enable",
            Globals::g_Config.enableUI ? L"1" : L"0", configPath.c_str());
        WritePrivateProfileStringW(L"UI", L"AutoLaunch",
            Globals::g_Config.autoLaunchUI ? L"1" : L"0", configPath.c_str());
        WritePrivateProfileStringW(L"UI", L"ExecutableName",
            Utils::StringToWString(Globals::g_Config.uiExecutableName).c_str(), configPath.c_str());

        WritePrivateProfileStringW(L"AutoLoad", L"AutoLoadModsFolder",
            Globals::g_Config.autoLoadMods ? L"1" : L"0", configPath.c_str());
        WritePrivateProfileStringW(L"AutoLoad", L"AutoLoadScriptsFolder",
            Globals::g_Config.autoLoadScripts ? L"1" : L"0", configPath.c_str());

        WritePrivateProfileStringW(L"ScriptHook", L"LoadScriptHookV",
            Globals::g_Config.loadScriptHookV ? L"1" : L"0", configPath.c_str());
        WritePrivateProfileStringW(L"ScriptHook", L"LoadScriptHookVDotNet",
            Globals::g_Config.loadScriptHookVDotNet ? L"1" : L"0", configPath.c_str());

        WritePrivateProfileStringW(L"Logging", L"MaxLogEntries",
            std::to_wstring(Globals::g_Config.maxLogEntries).c_str(), configPath.c_str());
        WritePrivateProfileStringW(L"Logging", L"VerboseLogging",
            Globals::g_Config.verboseLogging ? L"1" : L"0", configPath.c_str());

        WritePrivateProfileStringW(L"Performance", L"EnableMonitor",
            Globals::g_Config.enablePerformanceMonitor ? L"1" : L"0", configPath.c_str());
        WritePrivateProfileStringW(L"Performance", L"MonitorIntervalMS",
            std::to_wstring(Globals::g_Config.performanceMonitorInterval).c_str(), configPath.c_str());

        LOG_SUCCESS("Configuration saved");
    }
    catch (...) {
        LOG_ERROR("Failed to save configuration");
    }
}

inline void ConfigManager::CreateDefaultConfig() {
    std::wstring configPath = GetConfigPath();

    if (!Utils::FileExists(configPath.c_str())) {
        WritePrivateProfileStringW(L"UI", L"Enable", L"1", configPath.c_str());
        WritePrivateProfileStringW(L"UI", L"AutoLaunch", L"1", configPath.c_str());
        WritePrivateProfileStringW(L"UI", L"ExecutableName", L"GTAVModManager.exe", configPath.c_str());

        WritePrivateProfileStringW(L"ScriptHook", L"LoadScriptHookV", L"1", configPath.c_str());
        WritePrivateProfileStringW(L"ScriptHook", L"LoadScriptHookVDotNet", L"0", configPath.c_str());

        WritePrivateProfileStringW(L"AutoLoad", L"AutoLoadModsFolder", L"1", configPath.c_str());
        WritePrivateProfileStringW(L"AutoLoad", L"AutoLoadScriptsFolder", L"1", configPath.c_str());

        WritePrivateProfileStringW(L"Logging", L"MaxLogEntries", L"500", configPath.c_str());
        WritePrivateProfileStringW(L"Logging", L"VerboseLogging", L"0", configPath.c_str());

        WritePrivateProfileStringW(L"Performance", L"EnableMonitor", L"1", configPath.c_str());
        WritePrivateProfileStringW(L"Performance", L"MonitorIntervalMS", L"5000", configPath.c_str());

        LOG_SUCCESS("Default configuration created");
    }
}