#include "pch.h"
#include "Logger.h"

Logger& Logger::Instance() {
    static Logger instance;
    return instance;
}

void Logger::Log(const std::string& msg, const std::string& level) {
    std::string timestamp = Utils::GetCurrentTimestamp();

    if (level == "DEBUG" && !Globals::g_Config.verboseLogging) {
        return;
    }

    HANDLE hConsole = GetStdHandle(STD_OUTPUT_HANDLE);
    if (hConsole != INVALID_HANDLE_VALUE) {
        WORD color = FOREGROUND_RED | FOREGROUND_GREEN | FOREGROUND_BLUE;

        if (level == "ERROR") {
            color = FOREGROUND_RED | FOREGROUND_INTENSITY;
        }
        else if (level == "WARNING") {
            color = FOREGROUND_RED | FOREGROUND_GREEN | FOREGROUND_INTENSITY;
        }
        else if (level == "SUCCESS") {
            color = FOREGROUND_GREEN | FOREGROUND_INTENSITY;
        }
        else if (level == "DEBUG") {
            color = FOREGROUND_BLUE | FOREGROUND_INTENSITY;
        }
        else if (level == "INFO") {
            color = FOREGROUND_BLUE | FOREGROUND_GREEN | FOREGROUND_INTENSITY;
        }

        SetConsoleTextAttribute(hConsole, color);
        std::cout << "[" << timestamp << "] [" << level << "] " << msg << std::endl;
        SetConsoleTextAttribute(hConsole, FOREGROUND_RED | FOREGROUND_GREEN | FOREGROUND_BLUE);
    }

    WriteToFile(msg, level, timestamp);

    {
        std::lock_guard<std::mutex> lock(m_LogMutex);

        LogEntry entry;
        entry.message = msg;
        entry.timestamp = timestamp;
        entry.level = level;

        m_LogHistory.push_back(entry);

        if (m_LogHistory.size() > static_cast<size_t>(Globals::g_Config.maxLogEntries)) {
            m_LogHistory.pop_front();
        }
    }
}

void Logger::LogDebug(const std::string& msg) {
    Log(msg, "DEBUG");
}

void Logger::LogInfo(const std::string& msg) {
    Log(msg, "INFO");
}

void Logger::LogSuccess(const std::string& msg) {
    Log(msg, "SUCCESS");
}

void Logger::LogWarning(const std::string& msg) {
    Log(msg, "WARNING");
}

void Logger::LogError(const std::string& msg) {
    Log(msg, "ERROR");
}

std::string Logger::SerializeLogsJSON() {
    std::lock_guard<std::mutex> lock(m_LogMutex);

    std::ostringstream json;
    json << "{\"logs\":[";

    for (size_t i = 0; i < m_LogHistory.size(); i++) {
        const auto& log = m_LogHistory[i];

        std::string escapedMsg = log.message;
        size_t pos = 0;
        while ((pos = escapedMsg.find("\"", pos)) != std::string::npos) {
            escapedMsg.replace(pos, 1, "\\\"");
            pos += 2;
        }
        while ((pos = escapedMsg.find("\n", pos)) != std::string::npos) {
            escapedMsg.replace(pos, 1, "\\n");
            pos += 2;
        }
        while ((pos = escapedMsg.find("\r", pos)) != std::string::npos) {
            escapedMsg.replace(pos, 1, "\\r");
            pos += 2;
        }

        json << "{\"timestamp\":\"" << log.timestamp << "\","
            << "\"level\":\"" << log.level << "\","
            << "\"message\":\"" << escapedMsg << "\"}";

        if (i < m_LogHistory.size() - 1) {
            json << ",";
        }
    }

    json << "],\"count\":" << m_LogHistory.size() << "}";
    return json.str();
}

const std::deque<LogEntry>& Logger::GetHistory() const {
    return m_LogHistory;
}

void Logger::ClearHistory() {
    std::lock_guard<std::mutex> lock(m_LogMutex);
    m_LogHistory.clear();
}

Logger::Logger() {
    AllocConsole();

    FILE* fDummy;
    freopen_s(&fDummy, "CONOUT$", "w", stdout);
    freopen_s(&fDummy, "CONOUT$", "w", stderr);
    freopen_s(&fDummy, "CONIN$", "r", stdin);

    SetConsoleTitleA("GTA V Mod Loader - Console");

    HANDLE hConsole = GetStdHandle(STD_OUTPUT_HANDLE);
    if (hConsole != INVALID_HANDLE_VALUE) {
        COORD bufferSize;
        bufferSize.X = 120;
        bufferSize.Y = 9999;
        SetConsoleScreenBufferSize(hConsole, bufferSize);
    }
}

Logger::~Logger() {
    FreeConsole();
}

void Logger::WriteToFile(const std::string& message, const std::string& level, const std::string& timestamp) {
    try {
        std::wstring logPath = Utils::GetGameDirectory() + L"logs\\loader.log";

        std::ofstream logFile;
        logFile.open(logPath, std::ios::app);

        if (logFile.is_open()) {
            logFile << "[" << timestamp << "] [" << level << "] " << message << std::endl;
            logFile.close();
        }
    }
    catch (...) {
        // Silently fail if we can't write to log file
    }
}