#pragma once
#include "pch.h"

struct LoaderConfig {
    bool enableUI = true;
    bool autoLaunchUI = true;
    std::string uiExecutableName = "GTAVModManager.exe";
    bool autoLoadMods = true;
    bool loadScriptHookV = true;
    bool loadScriptHookVDotNet = false;
    bool autoLoadScripts = true;
    int maxLogEntries = 500;
    bool verboseLogging = false;
    bool enablePerformanceMonitor = true;
    int performanceMonitorInterval = 5000;
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
    size_t callCount = 0;
    std::chrono::microseconds totalExecutionTime{ 0 };
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

struct PerformanceMetrics {
    size_t totalMemoryUsage = 0;
    size_t peakMemoryUsage = 0;
    double cpuUsagePercent = 0.0;
    size_t threadCount = 0;
    size_t handleCount = 0;
    std::chrono::milliseconds averageFrameTime{ 0 };
    size_t totalModsLoaded = 0;
    std::chrono::seconds uptime{ 0 };
    std::string slowestMod;
    std::chrono::microseconds slowestModTime{ 0 };
};

namespace Globals {
    extern LoaderConfig g_Config;
    extern std::vector<ModInfo> g_LoadedMods;
    extern std::mutex g_ModsMutex;
    extern std::atomic<bool> g_ShuttingDown;
    extern std::chrono::steady_clock::time_point g_StartTime;
    extern PerformanceMetrics g_PerfMetrics;
    extern std::mutex g_PerfMutex;
}

namespace Utils {
    std::wstring StringToWString(const std::string& str);
    std::string WStringToString(const std::wstring& wstr);
    bool FileExists(const wchar_t* path);
    bool DirectoryExists(const wchar_t* path);
    std::wstring GetGameDirectory();
    size_t GetFileSize(const std::wstring& path);
    std::string GenerateModID(const std::wstring& path);
    std::string GetCurrentTimestamp();
}