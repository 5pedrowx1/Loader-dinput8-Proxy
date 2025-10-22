// ============================================================================
//  GTA V Mod Loader - dinput8.dll Proxy Client API (C# Edition)
//  -------------------------------------------------------------
//  Author  : 5pedrowx1
//  Version : 2.1.0
//  License : MIT
//  Date    : October 2025
//
//  Description:
//  ---------------------------------------------------------------------------
//  This file provides a managed (.NET) client interface for communicating with
//  the GTA V Mod Loader via a named pipe (\\.\pipe\GTAVModLoader).
//
//  The ModLoaderClient class enables developers to integrate external tools,
//  editors, or launchers with the Mod Loader — allowing them to query, load,
//  unload, and manage mods through IPC (Inter-Process Communication).
//
//  Example usage:
//  ---------------------------------------------------------------------------
//      using GTAVModLoaderAPI;
//
//      var client = new ModLoaderClient();
//      if (client.Connect())
//      {
//          Console.WriteLine(client.GetStatus());
//          client.Disconnect();
//      }
//      else
//      {
//          Console.WriteLine($"Connection failed: {client.LastError}");
//      }
//
//  Notes:
//  - Requires .NET Framework 4.6+ or .NET 6+
//  - Works only on Windows (uses NamedPipeClientStream)
//
//  File: ModLoaderClient.cs
// ============================================================================

using System;
using System.IO;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Text;

namespace GTAVModLoaderAPI
{
    /// <summary>
    /// Provides a managed API for interacting with the GTA V Mod Loader server
    /// through a named pipe (\\.\pipe\GTAVModLoader).
    /// </summary>
    public class ModLoaderClient : IDisposable
    {
        private NamedPipeClientStream pipeClient;
        private bool disposed = false;

        /// <summary>
        /// Structure defining the IPC message format.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        private struct IPCMessage
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string command;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
            public string data;
        }

        /// <summary>
        /// Returns the last error message encountered during any operation.
        /// </summary>
        public string LastError { get; private set; }

        /// <summary>
        /// Connects to the GTA V Mod Loader IPC server.
        /// </summary>
        /// <param name="timeoutMs">Connection timeout in milliseconds (default: 5000)</param>
        /// <returns>True if the connection succeeded; otherwise false.</returns>
        public bool Connect(int timeoutMs = 5000)
        {
            try
            {
                pipeClient = new NamedPipeClientStream(
                    ".",
                    "GTAVModLoader",
                    PipeDirection.InOut,
                    PipeOptions.None
                );

                pipeClient.Connect(timeoutMs);
                pipeClient.ReadMode = PipeTransmissionMode.Message;

                LastError = null;
                return pipeClient.IsConnected;
            }
            catch (Exception ex)
            {
                LastError = $"Connection failed: {ex.Message}";
                return false;
            }
        }

        /// <summary>
        /// Disconnects from the Mod Loader IPC server.
        /// </summary>
        public void Disconnect()
        {
            pipeClient?.Dispose();
            pipeClient = null;
        }

        /// <summary>
        /// Indicates whether the client is currently connected.
        /// </summary>
        public bool IsConnected => pipeClient?.IsConnected ?? false;


        /// <summary>
        /// Sends a command to the Mod Loader server and returns its response.
        /// </summary>
        /// <param name="command">The command string (e.g., "GET_MODS").</param>
        /// <param name="data">Optional data or argument for the command.</param>
        /// <returns>
        /// The server’s response as a UTF-8 string, or null on error.
        /// </returns>
        public string SendCommand(string command, string data = "")
        {
            if (!IsConnected)
            {
                LastError = "Not connected to server";
                return null;
            }

            try
            {
                // Build message
                IPCMessage msg = new IPCMessage
                {
                    command = command ?? string.Empty,
                    data = data ?? string.Empty
                };

                // Marshal to byte array
                int size = Marshal.SizeOf(msg);
                byte[] buffer = new byte[size];
                IntPtr ptr = Marshal.AllocHGlobal(size);

                try
                {
                    Marshal.StructureToPtr(msg, ptr, false);
                    Marshal.Copy(ptr, buffer, 0, size);
                }
                finally
                {
                    Marshal.FreeHGlobal(ptr);
                }

                // Send
                pipeClient.Write(buffer, 0, size);
                pipeClient.Flush();

                // Receive
                byte[] responseBuffer = new byte[8192];
                int bytesRead = pipeClient.Read(responseBuffer, 0, responseBuffer.Length);

                LastError = null;
                return Encoding.UTF8.GetString(responseBuffer, 0, bytesRead).TrimEnd('\0');
            }
            catch (Exception ex)
            {
                LastError = $"Command failed: {ex.Message}";
                return null;
            }
        }

        /// <summary>Retrieve the list of mods (JSON format).</summary>
        public string GetMods() => SendCommand("GET_MODS");

        /// <summary>Load a mod from the specified path.</summary>
        public bool LoadMod(string path) => SendCommand("LOAD_MOD", path) == "SUCCESS";

        /// <summary>Unload a mod by its identifier.</summary>
        public bool UnloadMod(string modId) => SendCommand("UNLOAD_MOD", modId) == "SUCCESS";

        /// <summary>Reload a mod by its identifier.</summary>
        public bool ReloadMod(string modId) => SendCommand("RELOAD_MOD", modId) == "SUCCESS";

        /// <summary>Retrieve the loader’s current status (JSON).</summary>
        public string GetStatus() => SendCommand("GET_STATUS");

        /// <summary>Retrieve loader logs (JSON).</summary>
        public string GetLogs() => SendCommand("GET_LOGS");

        /// <summary>Retrieve performance metrics (JSON).</summary>
        public string GetPerformance() => SendCommand("GET_PERFORMANCE");

        /// <summary>Trigger a folder scan for mods.</summary>
        public bool ScanFolder() => SendCommand("SCAN_FOLDER") == "SUCCESS";

        /// <summary>Reload all mods currently loaded.</summary>
        public string ReloadAll() => SendCommand("RELOAD_ALL");

        /// <summary>Re-scan all currently loaded modules.</summary>
        public bool RescanLoaded() => SendCommand("RESCAN_LOADED") == "SUCCESS";

        /// <summary>Clear the loader’s logs.</summary>
        public bool ClearLogs() => SendCommand("CLEAR_LOGS") == "SUCCESS";

        /// <summary>
        /// Disposes of the current instance and releases all resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Disconnect();
                }
                disposed = true;
            }
        }

        ~ModLoaderClient()
        {
            Dispose(false);
        }
    }
}
