#include "pch.h"
#include "Core.h"

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
    std::wstring StringToWString(const std::string& str) {
        if (str.empty()) return std::wstring();
        int size_needed = MultiByteToWideChar(CP_UTF8, 0, str.c_str(), (int)str.size(), NULL, 0);
        std::wstring wstr(size_needed, 0);
        MultiByteToWideChar(CP_UTF8, 0, str.c_str(), (int)str.size(), &wstr[0], size_needed);
        return wstr;
    }

    std::string WStringToString(const std::wstring& wstr) {
        if (wstr.empty()) return std::string();
        int size_needed = WideCharToMultiByte(CP_UTF8, 0, wstr.c_str(), (int)wstr.size(), NULL, 0, NULL, NULL);
        std::string str(size_needed, 0);
        WideCharToMultiByte(CP_UTF8, 0, wstr.c_str(), (int)wstr.size(), &str[0], size_needed, NULL, NULL);
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
        wchar_t path[MAX_PATH];
        GetModuleFileNameW(NULL, path, MAX_PATH);
        std::wstring wpath(path);
        size_t pos = wpath.find_last_of(L"\\/");
        if (pos != std::wstring::npos) {
            return wpath.substr(0, pos + 1);
        }
        return L"";
    }

    size_t GetFileSize(const std::wstring& path) {
        WIN32_FILE_ATTRIBUTE_DATA fileInfo;
        if (GetFileAttributesExW(path.c_str(), GetFileExInfoStandard, &fileInfo)) {
            LARGE_INTEGER size;
            size.HighPart = fileInfo.nFileSizeHigh;
            size.LowPart = fileInfo.nFileSizeLow;
            return static_cast<size_t>(size.QuadPart);
        }
        return 0;
    }

    std::string GenerateModID(const std::wstring& path) {
        std::hash<std::wstring> hasher;
        size_t hash = hasher(path);
        std::ostringstream oss;
        oss << "mod_" << std::hex << hash;
        return oss.str();
    }

    std::string GetCurrentTimestamp() {
        auto now = std::chrono::system_clock::now();
        auto time = std::chrono::system_clock::to_time_t(now);
        struct tm timeinfo;
        localtime_s(&timeinfo, &time);
        std::ostringstream oss;
        oss << std::put_time(&timeinfo, "%Y-%m-%d %H:%M:%S");
        return oss.str();
    }
}