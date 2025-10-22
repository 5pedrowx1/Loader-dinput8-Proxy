#pragma once

#include "pch.h"
#include <windows.h>
#include <string>
#include <chrono>
#include <vector>
#include <atomic>
#include <mutex>
#include <sstream>
#include <iomanip>
#include <fstream>
#include <tlhelp32.h>
#include <algorithm>

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
    LoaderConfig g_Config;
    std::vector<ModInfo> g_LoadedMods;
    std::mutex g_ModsMutex;
    std::atomic<bool> g_ShuttingDown{ false };
    std::chrono::steady_clock::time_point g_StartTime;
    PerformanceMetrics g_PerfMetrics;
    std::mutex g_PerfMutex;
}

namespace Utils {

    inline std::wstring StringToWString(const std::string& str) {
        if (str.empty()) return std::wstring();

        int size_needed = MultiByteToWideChar(CP_UTF8, 0, str.c_str(), (int)str.size(), NULL, 0);
        std::wstring wstr(size_needed, 0);
        MultiByteToWideChar(CP_UTF8, 0, str.c_str(), (int)str.size(), &wstr[0], size_needed);

        return wstr;
    }

    inline std::string WStringToString(const std::wstring& wstr) {
        if (wstr.empty()) return std::string();

        int size_needed = WideCharToMultiByte(CP_UTF8, 0, wstr.c_str(), (int)wstr.size(), NULL, 0, NULL, NULL);
        std::string str(size_needed, 0);
        WideCharToMultiByte(CP_UTF8, 0, wstr.c_str(), (int)wstr.size(), &str[0], size_needed, NULL, NULL);

        return str;
    }

    inline bool FileExists(const wchar_t* path) {
        DWORD attrib = GetFileAttributesW(path);
        return (attrib != INVALID_FILE_ATTRIBUTES && !(attrib & FILE_ATTRIBUTE_DIRECTORY));
    }

    inline bool DirectoryExists(const wchar_t* path) {
        DWORD attrib = GetFileAttributesW(path);
        return (attrib != INVALID_FILE_ATTRIBUTES && (attrib & FILE_ATTRIBUTE_DIRECTORY));
    }

    inline std::wstring GetGameDirectory() {
        wchar_t path[MAX_PATH];
        GetModuleFileNameW(NULL, path, MAX_PATH);

        std::wstring wpath(path);
        size_t pos = wpath.find_last_of(L"\\/");

        if (pos != std::wstring::npos) {
            return wpath.substr(0, pos + 1);
        }

        return L"";
    }

    inline size_t GetFileSize(const std::wstring& path) {
        WIN32_FILE_ATTRIBUTE_DATA fileInfo;
        if (GetFileAttributesExW(path.c_str(), GetFileExInfoStandard, &fileInfo)) {
            LARGE_INTEGER size;
            size.HighPart = fileInfo.nFileSizeHigh;
            size.LowPart = fileInfo.nFileSizeLow;
            return static_cast<size_t>(size.QuadPart);
        }
        return 0;
    }

    inline std::string GenerateModID(const std::wstring& path) {
        std::hash<std::wstring> hasher;
        size_t hash = hasher(path);

        std::ostringstream oss;
        oss << "mod_" << std::hex << hash;

        return oss.str();
    }

    inline std::string GetCurrentTimestamp() {
        auto now = std::chrono::system_clock::now();
        auto time = std::chrono::system_clock::to_time_t(now);

        struct tm timeinfo;
        localtime_s(&timeinfo, &time);

        std::ostringstream oss;
        oss << std::put_time(&timeinfo, "%Y-%m-%d %H:%M:%S");

        return oss.str();
    }

}