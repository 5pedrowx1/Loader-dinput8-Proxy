#pragma once
#include "pch.h"
#include "Core.h"

class PerformanceMonitor {
public:
    static PerformanceMonitor& Instance();

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
    PerformanceMonitor();
    ~PerformanceMonitor();
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

    ULONGLONG m_LastCPU = 0;
    ULONGLONG m_LastSysCPU = 0;
    ULONGLONG m_LastUserCPU = 0;
    HANDLE m_ProcessHandle = nullptr;
    int m_NumProcessors = 0;
};