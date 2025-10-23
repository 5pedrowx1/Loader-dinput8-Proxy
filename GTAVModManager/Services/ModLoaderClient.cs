using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Text;

namespace GTAVModManager.Services
{
    public class ModLoaderClient : IDisposable
    {
        private const string PipeName = "GTAVModLoader";
        private const int Timeout = 5000;
        private NamedPipeClientStream? _pipeClient;
        private bool _disposed;

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        private struct IPCMessage
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string Command;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
            public string Data;

            public IPCMessage(string command, string data = "")
            {
                Command = command ?? "";
                Data = data ?? "";
            }
        }

        public bool IsConnected => _pipeClient?.IsConnected ?? false;

        public async Task<bool> ConnectAsync()
        {
            try
            {
                _pipeClient?.Dispose();
                _pipeClient = new NamedPipeClientStream(".", PipeName, PipeDirection.InOut);

                await _pipeClient.ConnectAsync(Timeout);
                return _pipeClient.IsConnected;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> SendCommandAsync(string command, string data = "")
        {
            if (!IsConnected)
            {
                if (!await ConnectAsync())
                    return string.Empty;
            }

            try
            {
                var msg = new IPCMessage(command, data);
                byte[] buffer = StructToBytes(msg);

                await _pipeClient!.WriteAsync(buffer, 0, buffer.Length);
                await _pipeClient.FlushAsync();

                byte[] responseBuffer = new byte[8192];
                int bytesRead = await _pipeClient.ReadAsync(responseBuffer, 0, responseBuffer.Length);

                return Encoding.UTF8.GetString(responseBuffer, 0, bytesRead);
            }
            catch
            {
                _pipeClient?.Dispose();
                _pipeClient = null;
                return string.Empty;
            }
        }

        public Task<string> GetModsAsync() => SendCommandAsync("GET_MODS");
        public Task<string> GetStatusAsync() => SendCommandAsync("GET_STATUS");
        public Task<string> GetLogsAsync() => SendCommandAsync("GET_LOGS");
        public Task<string> GetPerformanceAsync() => SendCommandAsync("GET_PERFORMANCE");
        public Task<string> LoadModAsync(string path) => SendCommandAsync("LOAD_MOD", path);
        public Task<string> UnloadModAsync(string modId) => SendCommandAsync("UNLOAD_MOD", modId);
        public Task<string> ReloadModAsync(string modId) => SendCommandAsync("RELOAD_MOD", modId);
        public Task<string> ReloadAllAsync() => SendCommandAsync("RELOAD_ALL");
        public Task<string> ScanFolderAsync() => SendCommandAsync("SCAN_FOLDER");
        public Task<string> RescanLoadedAsync() => SendCommandAsync("RESCAN_LOADED");
        public Task<string> ClearLogsAsync() => SendCommandAsync("CLEAR_LOGS");

        private static byte[] StructToBytes<T>(T str) where T : struct
        {
            int size = Marshal.SizeOf(str);
            byte[] arr = new byte[size];
            IntPtr ptr = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(str, ptr, true);
                Marshal.Copy(ptr, arr, 0, size);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
            return arr;
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            _pipeClient?.Dispose();
        }
    }
}