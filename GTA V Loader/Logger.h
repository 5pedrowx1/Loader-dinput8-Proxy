#pragma once
#include "pch.h"
#include "Core.h"

class Logger {
public:
    static Logger& Instance();

    void Log(const std::string& msg, const std::string& level = "INFO");
    void LogDebug(const std::string& msg);
    void LogInfo(const std::string& msg);
    void LogSuccess(const std::string& msg);
    void LogWarning(const std::string& msg);
    void LogError(const std::string& msg);

    std::string SerializeLogsJSON();
    const std::deque<LogEntry>& GetHistory() const;
    void ClearHistory();

private:
    Logger();
    ~Logger();
    Logger(const Logger&) = delete;
    Logger& operator=(const Logger&) = delete;

    void WriteToFile(const std::string& message, const std::string& level, const std::string& timestamp);

    std::deque<LogEntry> m_LogHistory;
    std::mutex m_LogMutex;
};

#define LOG_DEBUG(msg) Logger::Instance().LogDebug(msg)
#define LOG_INFO(msg) Logger::Instance().LogInfo(msg)
#define LOG_SUCCESS(msg) Logger::Instance().LogSuccess(msg)
#define LOG_WARNING(msg) Logger::Instance().LogWarning(msg)
#define LOG_ERROR(msg) Logger::Instance().LogError(msg)