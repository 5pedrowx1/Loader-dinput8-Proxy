#pragma once

#include "pch.h"
#include "Core.cpp"
#include "Logger.cpp"
#include "ModManager.cpp"
#include "PerformanceMonitor.cpp"
#include <thread>

class IPCServer {
public:
    static IPCServer& Instance() {
        static IPCServer instance;
        return instance;
    }

    void Start();
    void Stop();
    bool IsRunning() const { return m_Running; }

private:
    IPCServer() = default;
    ~IPCServer() { Stop(); }
    IPCServer(const IPCServer&) = delete;
    IPCServer& operator=(const IPCServer&) = delete;

    void ServerThread();
    void HandleCommand(const IPCMessage& msg, HANDLE hPipe);

    std::string GetStatusJSON();
    std::string HandleReloadAll();

    std::thread m_ServerThread;
    std::atomic<bool> m_Running{ false };
    HANDLE m_hPipe = INVALID_HANDLE_VALUE;
};

inline void IPCServer::Start() {
    if (m_Running) return;

    m_Running = true;
    m_ServerThread = std::thread(&IPCServer::ServerThread, this);
    LOG_SUCCESS("IPC Server started on pipe: \\\\.\\pipe\\GTAVModLoader");
}

inline void IPCServer::Stop() {
    if (!m_Running) return;

    m_Running = false;

    if (m_hPipe != INVALID_HANDLE_VALUE) {
        CloseHandle(m_hPipe);
        m_hPipe = INVALID_HANDLE_VALUE;
    }

    if (m_ServerThread.joinable()) {
        m_ServerThread.join();
    }

    LOG_INFO("IPC Server stopped");
}

inline void IPCServer::ServerThread() {
    const wchar_t* pipeName = L"\\\\.\\pipe\\GTAVModLoader";

    while (m_Running && !Globals::g_ShuttingDown) {
        m_hPipe = CreateNamedPipeW(
            pipeName,
            PIPE_ACCESS_DUPLEX,
            PIPE_TYPE_MESSAGE | PIPE_READMODE_MESSAGE | PIPE_WAIT,
            PIPE_UNLIMITED_INSTANCES,
            8192,
            8192,
            0,
            NULL
        );

        if (m_hPipe == INVALID_HANDLE_VALUE) {
            LOG_ERROR("Failed to create named pipe");
            Sleep(1000);
            continue;
        }

        if (ConnectNamedPipe(m_hPipe, NULL) || GetLastError() == ERROR_PIPE_CONNECTED) {
            LOG_DEBUG("Client connected to IPC pipe");

            while (m_Running && !Globals::g_ShuttingDown) {
                IPCMessage msg = { 0 };
                DWORD bytesRead;

                if (ReadFile(m_hPipe, &msg, sizeof(IPCMessage), &bytesRead, NULL) && bytesRead > 0) {
                    HandleCommand(msg, m_hPipe);
                }
                else {
                    break;
                }
            }

            DisconnectNamedPipe(m_hPipe);
            LOG_DEBUG("Client disconnected from IPC pipe");
        }

        CloseHandle(m_hPipe);
        m_hPipe = INVALID_HANDLE_VALUE;
    }
}

inline void IPCServer::HandleCommand(const IPCMessage& msg, HANDLE hPipe) {
    std::string cmd = msg.command;
    std::string data = msg.data;
    std::string response;

    LOG_DEBUG("IPC Command received: " + cmd);

    if (cmd == "GET_MODS") {
        response = ModManager::Instance().SerializeModsJSON();
    }
    else if (cmd == "LOAD_MOD") {
        std::wstring wpath = Utils::StringToWString(data);
        bool success = ModManager::Instance().LoadMod(wpath, "mod");
        response = success ? "SUCCESS" : "FAILED";
    }
    else if (cmd == "UNLOAD_MOD") {
        bool success = ModManager::Instance().UnloadMod(data);
        response = success ? "SUCCESS" : "FAILED";
    }
    else if (cmd == "RELOAD_MOD") {
        bool success = ModManager::Instance().ReloadMod(data);
        response = success ? "SUCCESS" : "FAILED";
    }
    else if (cmd == "GET_STATUS") {
        response = GetStatusJSON();
    }
    else if (cmd == "GET_LOGS") {
        response = Logger::Instance().SerializeLogsJSON();
    }
    else if (cmd == "GET_PERFORMANCE") {
        response = PerformanceMonitor::Instance().SerializeMetricsJSON();
    }
    else if (cmd == "SCAN_FOLDER") {
        ModManager::Instance().ScanAndLoadModsFolder();
        response = "SUCCESS";
    }
    else if (cmd == "RELOAD_ALL") {
        response = HandleReloadAll();
    }
    else if (cmd == "RESCAN_LOADED") {
        ModManager::Instance().ScanLoadedModules();
        response = "SUCCESS";
    }
    else if (cmd == "CLEAR_LOGS") {
        Logger::Instance().ClearHistory();
        response = "SUCCESS";
    }
    else {
        LOG_WARNING("Unknown IPC command: " + cmd);
        response = "UNKNOWN_COMMAND";
    }

    DWORD written;
    WriteFile(hPipe, response.c_str(), (DWORD)response.length(), &written, NULL);
}

inline std::string IPCServer::GetStatusJSON() {
    auto uptime = std::chrono::duration_cast<std::chrono::seconds>(
        std::chrono::steady_clock::now() - Globals::g_StartTime
    ).count();

    std::lock_guard<std::mutex> lock(Globals::g_ModsMutex);

    std::ostringstream json;
    json << "{\"version\":\"2.1.0\","
        << "\"uptime_seconds\":" << uptime << ","
        << "\"mods_loaded\":" << Globals::g_LoadedMods.size() << ","
        << "\"server_running\":" << (m_Running ? "true" : "false") << ","
        << "\"performance_monitor_active\":" << (Globals::g_Config.enablePerformanceMonitor ? "true" : "false") << ","
        << "\"game_detected\":" << (FindWindowA("grcWindow", nullptr) ? "true" : "false") << "}";

    return json.str();
}

inline std::string IPCServer::HandleReloadAll() {
    std::vector<std::string> modsToReload;
    std::vector<std::string> modTypes;
    std::vector<std::wstring> modPaths;

    {
        std::lock_guard<std::mutex> lock(Globals::g_ModsMutex);
        for (const auto& mod : Globals::g_LoadedMods) {
            if (mod.type != "scripthook" && mod.type != "dotnet") {
                modsToReload.push_back(mod.id);
                modTypes.push_back(mod.type);
                modPaths.push_back(mod.path);
            }
        }
    }

    int reloaded = 0;
    for (const auto& modId : modsToReload) {
        ModManager::Instance().UnloadMod(modId);
    }

    Sleep(1000);

    for (size_t i = 0; i < modPaths.size(); i++) {
        if (ModManager::Instance().LoadMod(modPaths[i], modTypes[i])) {
            reloaded++;
        }
    }

    return "SUCCESS:" + std::to_string(reloaded) + "/" + std::to_string(modsToReload.size());
}