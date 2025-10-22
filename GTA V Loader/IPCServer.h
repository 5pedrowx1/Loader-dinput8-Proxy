#pragma once
#include "pch.h"
#include "Core.h"

class IPCServer {
public:
    static IPCServer& Instance();

    void Start();
    void Stop();
    bool IsRunning() const;

private:
    IPCServer() = default;
    ~IPCServer();
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