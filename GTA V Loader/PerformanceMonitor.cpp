#pragma once

#include "pch.h"
#include "Core.cpp"
#include <thread>
#include <psapi.h>
#include <Logger.cpp>

class PerformanceMonitor {
public:
    static PerformanceMonitor& Instance() {
        static PerformanceMonitor instance;
        return instance;
    }

    void Start();
    void Stop();
    void Update();

    PerformanceMetrics GetMetrics() const;
    std::string SerializeMetricsJSON() const;

    class ScopedTimer {
    public:
        ScopedTimer(const std::string& modId);
        ~ScopedTimer();
    private:
        std::string m_ModId;
        std::chrono::high_resolution_clock::time_point m_Start;
    };

private:
    PerformanceMonitor() = default;
    ~PerformanceMonitor() { Stop(); }
    PerformanceMonitor(const PerformanceMonitor&) = delete;
    PerformanceMonitor& operator=(const PerformanceMonitor&) = delete;

    void MonitorThread();
    void UpdateMemoryMetrics();
    void UpdateCPUMetrics();
    void UpdateThreadMetrics();
    void UpdateModMetrics();

    size_t GetProcessMemoryUsage();
    double CalculateCPUUsage();

    std::thread m_MonitorThread;
    std::atomic<bool> m_Running{ false };

    ULARGE_INTEGER m_LastCPU{ 0 };
    ULARGE_INTEGER m_LastSysCPU{ 0 };
    ULARGE_INTEGER m_LastUserCPU{ 0 };
    HANDLE m_ProcessHandle = nullptr;
    int m_NumProcessors = 0;
};

inline PerformanceMonitor::ScopedTimer::ScopedTimer(const std::string& modId)
    : m_ModId(modId)
    , m_Start(std::chrono::high_resolution_clock::now()) {
}

inline PerformanceMonitor::ScopedTimer::~ScopedTimer() {
    auto end = std::chrono::high_resolution_clock::now();
    auto duration = std::chrono::duration_cast<std::chrono::microseconds>(end - m_Start);

    std::lock_guard<std::mutex> lock(Globals::g_ModsMutex);
    for (auto& mod : Globals::g_LoadedMods) {
        if (mod.id == m_ModId) {
            mod.callCount++;
            mod.totalExecutionTime += duration;
            break;
        }
    }
}

inline void PerformanceMonitor::Start() {
    if (m_Running) return;

    m_Running = true;

    SYSTEM_INFO sysInfo;
    GetSystemInfo(&sysInfo);
    m_NumProcessors = sysInfo.dwNumberOfProcessors;

    FILETIME ftime, fsys, fuser;
    GetSystemTimeAsFileTime(&ftime);
    memcpy(&m_LastCPU, &ftime, sizeof(FILETIME));

    m_ProcessHandle = GetCurrentProcess();
    GetProcessTimes(m_ProcessHandle, &ftime, &ftime, &fsys, &fuser);
    memcpy(&m_LastSysCPU, &fsys, sizeof(FILETIME));
    memcpy(&m_LastUserCPU, &fuser, sizeof(FILETIME));

    m_MonitorThread = std::thread(&PerformanceMonitor::MonitorThread, this);
    LOG_INFO("Performance monitor started");
}

inline void PerformanceMonitor::Stop() {
    if (!m_Running) return;

    m_Running = false;
    if (m_MonitorThread.joinable()) {
        m_MonitorThread.join();
    }
    LOG_INFO("Performance monitor stopped");
}

inline void PerformanceMonitor::MonitorThread() {
    while (m_Running && !Globals::g_ShuttingDown) {
        Update();

        int interval = Globals::g_Config.performanceMonitorInterval;
        for (int i = 0; i < interval / 100 && m_Running; i++) {
            Sleep(100);
        }
    }
}

inline void PerformanceMonitor::Update() {
    UpdateMemoryMetrics();
    UpdateCPUMetrics();
    UpdateThreadMetrics();
    UpdateModMetrics();

    auto uptime = std::chrono::duration_cast<std::chrono::seconds>(
        std::chrono::steady_clock::now() - Globals::g_StartTime
    );

    std::lock_guard<std::mutex> lock(Globals::g_PerfMutex);
    Globals::g_PerfMetrics.uptime = uptime;
    Globals::g_PerfMetrics.totalModsLoaded = Globals::g_LoadedMods.size();
}

inline void PerformanceMonitor::UpdateMemoryMetrics() {
    PROCESS_MEMORY_COUNTERS_EX pmc;
    if (GetProcessMemoryInfo(GetCurrentProcess(), (PROCESS_MEMORY_COUNTERS*)&pmc, sizeof(pmc))) {
        std::lock_guard<std::mutex> lock(Globals::g_PerfMutex);
        Globals::g_PerfMetrics.totalMemoryUsage = pmc.WorkingSetSize;
        Globals::g_PerfMetrics.peakMemoryUsage = std::max(
            Globals::g_PerfMetrics.peakMemoryUsage,
            pmc.PeakWorkingSetSize
        );
    }
}

inline void PerformanceMonitor::UpdateCPUMetrics() {
    FILETIME ftime, fsys, fuser;
    ULARGE_INTEGER now, sys, user;

    GetSystemTimeAsFileTime(&ftime);
    memcpy(&now, &ftime, sizeof(FILETIME));

    GetProcessTimes(m_ProcessHandle, &ftime, &ftime, &fsys, &fuser);
    memcpy(&sys, &fsys, sizeof(FILETIME));
    memcpy(&user, &fuser, sizeof(FILETIME));

    double percent = (sys.QuadPart - m_LastSysCPU.QuadPart) +
        (user.QuadPart - m_LastUserCPU.QuadPart);
    percent /= (now.QuadPart - m_LastCPU.QuadPart);
    percent /= m_NumProcessors;

    std::lock_guard<std::mutex> lock(Globals::g_PerfMutex);
    Globals::g_PerfMetrics.cpuUsagePercent = percent * 100.0;

    m_LastCPU = now;
    m_LastUserCPU = user;
    m_LastSysCPU = sys;
}

inline void PerformanceMonitor::UpdateThreadMetrics() {
    DWORD threadCount = 0;
    DWORD handleCount = 0;

    GetProcessHandleCount(GetCurrentProcess(), &handleCount);

    HANDLE snapshot = CreateToolhelp32Snapshot(TH32CS_SNAPTHREAD, 0);
    if (snapshot != INVALID_HANDLE_VALUE) {
        THREADENTRY32 te;
        te.dwSize = sizeof(te);

        DWORD pid = GetCurrentProcessId();
        if (Thread32First(snapshot, &te)) {
            do {
                if (te.th32OwnerProcessID == pid) {
                    threadCount++;
                }
            } while (Thread32Next(snapshot, &te));
        }
        CloseHandle(snapshot);
    }

    std::lock_guard<std::mutex> lock(Globals::g_PerfMutex);
    Globals::g_PerfMetrics.threadCount = threadCount;
    Globals::g_PerfMetrics.handleCount = handleCount;
}

inline void PerformanceMonitor::UpdateModMetrics() {
    std::lock_guard<std::mutex> lock(Globals::g_ModsMutex);

    std::string slowestMod;
    std::chrono::microseconds slowestTime{ 0 };

    for (const auto& mod : Globals::g_LoadedMods) {
        if (mod.callCount > 0) {
            auto avgTime = mod.totalExecutionTime / mod.callCount;
            if (avgTime > slowestTime) {
                slowestTime = avgTime;
                slowestMod = Utils::WStringToString(mod.name);
            }
        }
    }

    std::lock_guard<std::mutex> perfLock(Globals::g_PerfMutex);
    Globals::g_PerfMetrics.slowestMod = slowestMod;
    Globals::g_PerfMetrics.slowestModTime = slowestTime;
}

inline PerformanceMetrics PerformanceMonitor::GetMetrics() const {
    std::lock_guard<std::mutex> lock(Globals::g_PerfMutex);
    return Globals::g_PerfMetrics;
}

inline std::string PerformanceMonitor::SerializeMetricsJSON() const {
    auto metrics = GetMetrics();

    std::ostringstream json;
    json << "{"
        << "\"memory_mb\":" << (metrics.totalMemoryUsage / (1024 * 1024)) << ","
        << "\"peak_memory_mb\":" << (metrics.peakMemoryUsage / (1024 * 1024)) << ","
        << "\"cpu_percent\":" << std::fixed << std::setprecision(2) << metrics.cpuUsagePercent << ","
        << "\"thread_count\":" << metrics.threadCount << ","
        << "\"handle_count\":" << metrics.handleCount << ","
        << "\"total_mods\":" << metrics.totalModsLoaded << ","
        << "\"uptime_seconds\":" << metrics.uptime.count() << ","
        << "\"slowest_mod\":\"" << metrics.slowestMod << "\","
        << "\"slowest_mod_time_us\":" << metrics.slowestModTime.count()
        << "}";

    return json.str();
}