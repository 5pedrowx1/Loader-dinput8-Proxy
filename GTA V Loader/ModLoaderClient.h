// ============================================================================
//  GTA V Mod Loader - dinput8.dll Proxy Client API
//  ------------------------------------------------
//  Author : 5pedrowx1
//  Version: 2.1.0
//  License: MIT 
//  Date   : October 2025
//
//  Description:
//  ---------------------------------------------------------------------------
//  This header provides the client-side interface for communicating with the
//  GTA V Mod Loader through a named pipe (\\.\pipe\GTAVModLoader).
//  It allows external tools, debuggers, or custom launchers to send IPC
//  commands such as loading, unloading, or querying mods from the loader.
//
//  Usage Example:
//      ModLoaderClient client;
//      if (client.Connect()) {
//          std::cout << client.GetStatus() << std::endl;
//          client.Disconnect();
//      }
//
//  Notes:
//  - Requires Windows (Win32 API).
//  - Compatible with C++17 or newer.
//
//  File: ModLoaderClient.h
// ============================================================================
#pragma once

#include <windows.h>
#include <string>
#include <vector>
#include <stdexcept>
#include <cstring>

// ============================================================================
//  IPCMessage Structure
// ============================================================================
// This structure defines the data format used in the IPC (Inter-Process
// Communication) channel between the Mod Loader client and server.
#pragma pack(push, 1)
struct IPCMessage {
    char command[32];  // Command name (e.g., "GET_MODS", "LOAD_MOD")
    char data[512];    // Optional command data
};
#pragma pack(pop)


// ============================================================================
//  ModLoaderClient Class
// ============================================================================
// Provides a C++ API for communicating with the GTAV Mod Loader server
// via a named pipe (\\.\pipe\GTAVModLoader).
//
// The ModLoaderClient class handles connection, message formatting,
// and convenience wrappers for common commands (GET_MODS, LOAD_MOD, etc.).
//
// Example usage:
// ----------------------------------------------------------------------------
//     ModLoaderClient client;
//     if (client.Connect()) {
//         std::string modsJson = client.GetMods();
//         std::cout << "Loaded mods: " << modsJson << std::endl;
//         client.Disconnect();
//     } else {
//         std::cerr << "Failed to connect: " << client.GetLastErrorString() << std::endl;
//     }
// ----------------------------------------------------------------------------
//
class ModLoaderClient {
public:
    ModLoaderClient() : m_hPipe(INVALID_HANDLE_VALUE) {}

    ~ModLoaderClient() {
        Disconnect();
    }

    // ------------------------------------------------------------------------
    // Connect
    // ------------------------------------------------------------------------
    // Establishes a connection with the Mod Loader IPC server.
    // 
    // @param timeoutMilliseconds   Maximum time to wait for the pipe (default: 5000 ms)
    // @return                      true if connection succeeded, false otherwise
    //
    bool Connect(int timeoutMilliseconds = 5000) {
        const wchar_t* pipeName = L"\\\\.\\pipe\\GTAVModLoader";

        // Attempt to open the named pipe
        m_hPipe = CreateFileW(
            pipeName,
            GENERIC_READ | GENERIC_WRITE,
            0,
            nullptr,
            OPEN_EXISTING,
            0,
            nullptr
        );

        if (m_hPipe == INVALID_HANDLE_VALUE) {
            DWORD error = GetLastError();

            // If the pipe is busy, wait for it to become available
            if (error == ERROR_PIPE_BUSY) {
                if (WaitNamedPipeW(pipeName, static_cast<DWORD>(timeoutMilliseconds))) {
                    // Try connecting again
                    return Connect(0);
                }
            }

            m_lastError = "Failed to connect to pipe. Error: " + std::to_string(error);
            return false;
        }

        // Configure pipe mode (message-based communication)
        DWORD mode = PIPE_READMODE_MESSAGE;
        if (!SetNamedPipeHandleState(m_hPipe, &mode, nullptr, nullptr)) {
            m_lastError = "Failed to set pipe mode. Error: " + std::to_string(GetLastError());
            CloseHandle(m_hPipe);
            m_hPipe = INVALID_HANDLE_VALUE;
            return false;
        }

        m_lastError.clear();
        return true;
    }

    // ------------------------------------------------------------------------
    // Disconnect
    // ------------------------------------------------------------------------
    // Closes the connection with the Mod Loader server.
    //
    void Disconnect() {
        if (m_hPipe != INVALID_HANDLE_VALUE) {
            CloseHandle(m_hPipe);
            m_hPipe = INVALID_HANDLE_VALUE;
        }
    }

    // ------------------------------------------------------------------------
    // IsConnected
    // ------------------------------------------------------------------------
    // Checks whether the client is currently connected to the server.
    //
    // @return true if connected, false otherwise
    //
    [[nodiscard]] bool IsConnected() const noexcept {
        return m_hPipe != INVALID_HANDLE_VALUE;
    }

    // ------------------------------------------------------------------------
    // SendCommand
    // ------------------------------------------------------------------------
    // Sends a command to the Mod Loader server and returns its response.
    //
    // @param command   Command name (e.g., "GET_MODS", "LOAD_MOD")
    // @param data      Optional data or argument for the command
    // @return          The server’s response as a JSON
    //
    std::string SendCommand(const std::string& command, const std::string& data = "") {
        if (!IsConnected()) {
            m_lastError = "Not connected to server";
            return {};
        }

        // Build IPC message
        IPCMessage msg{};
        strncpy_s(msg.command, sizeof(msg.command), command.c_str(), _TRUNCATE);
        strncpy_s(msg.data, sizeof(msg.data), data.c_str(), _TRUNCATE);

        // Send the message
        DWORD written = 0;
        if (!WriteFile(m_hPipe, &msg, sizeof(IPCMessage), &written, nullptr)) {
            m_lastError = "Failed to write to pipe. Error: " + std::to_string(GetLastError());
            return {};
        }

        if (!FlushFileBuffers(m_hPipe)) {
            m_lastError = "Failed to flush pipe. Error: " + std::to_string(GetLastError());
            return {};
        }

        // Receive the response
        char buffer[8192]{};
        DWORD bytesRead = 0;

        if (!ReadFile(m_hPipe, buffer, sizeof(buffer) - 1, &bytesRead, nullptr)) {
            DWORD error = GetLastError();
            if (error == ERROR_MORE_DATA) {
                buffer[sizeof(buffer) - 1] = '\0';
                m_lastError.clear();
                return std::string(buffer, bytesRead);
            }

            m_lastError = "Failed to read from pipe. Error: " + std::to_string(error);
            return {};
        }

        m_lastError.clear();
        return std::string(buffer, bytesRead);
    }

    // ------------------------------------------------------------------------
    // GetLastErrorString
    // ------------------------------------------------------------------------
    // Returns the last recorded error message.
    //
    // @return A string describing the last error encountered.
    //
    [[nodiscard]] std::string GetLastErrorString() const noexcept {
        return m_lastError;
    }

    // ========================= Convenience Methods ==========================

    // Retrieve the list of loaded mods (JSON format)
    std::string GetMods() {
        return SendCommand("GET_MODS");
    }

    // Load a mod from a specified path
    bool LoadMod(const std::string& path) {
        return SendCommand("LOAD_MOD", path) == "SUCCESS";
    }

    // Unload a mod by its identifier
    bool UnloadMod(const std::string& modId) {
        return SendCommand("UNLOAD_MOD", modId) == "SUCCESS";
    }

    // Reload a mod by its identifier
    bool ReloadMod(const std::string& modId) {
        return SendCommand("RELOAD_MOD", modId) == "SUCCESS";
    }

    // Query the loader’s current status
    std::string GetStatus() {
        return SendCommand("GET_STATUS");
    }

    // Retrieve loader logs
    std::string GetLogs() {
        return SendCommand("GET_LOGS");
    }

    // Retrieve performance metrics (e.g., load time, memory usage)
    std::string GetPerformance() {
        return SendCommand("GET_PERFORMANCE");
    }

    // Trigger a rescan of the mods folder
    bool ScanFolder() {
        return SendCommand("SCAN_FOLDER") == "SUCCESS";
    }

    // Reload all mods currently active
    std::string ReloadAll() {
        return SendCommand("RELOAD_ALL");
    }

    // Re-scan all loaded modules (for dependency updates)
    bool RescanLoaded() {
        return SendCommand("RESCAN_LOADED") == "SUCCESS";
    }

    // Clear all loader logs
    bool ClearLogs() {
        return SendCommand("CLEAR_LOGS") == "SUCCESS";
    }

private:
    HANDLE m_hPipe;            // Handle to the named pipe connection
    std::string m_lastError;   // Stores the last error message
};
