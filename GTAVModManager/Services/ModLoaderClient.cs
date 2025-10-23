using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Text;

namespace GTAVModManager.Services
{
    public class ModLoaderClient : IDisposable
    {
        private const string PipeName = "GTAVModLoader";
        private const int Timeout = 5000;
        private const int MaxRetries = 3;

        private NamedPipeClientStream? _pipeClient;
        private bool _disposed;
        private readonly object _lock = new object();
        private int _connectionAttempts = 0;

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

        public bool IsConnected
        {
            get
            {
                lock (_lock)
                {
                    return _pipeClient?.IsConnected ?? false;
                }
            }
        }

        public async Task<bool> ConnectAsync()
        {
            lock (_lock)
            {
                if (_disposed)
                    throw new ObjectDisposedException(nameof(ModLoaderClient));

                if (_pipeClient?.IsConnected == true)
                    return true;

                _pipeClient?.Dispose();
                _pipeClient = null;
            }

            try
            {
                var pipe = new NamedPipeClientStream(".", PipeName, PipeDirection.InOut);

                await pipe.ConnectAsync(Timeout);

                if (pipe.IsConnected)
                {
                    lock (_lock)
                    {
                        _pipeClient = pipe;
                        _connectionAttempts = 0;
                    }
                    return true;
                }

                pipe.Dispose();
                return false;
            }
            catch (TimeoutException)
            {
                _connectionAttempts++;
                return false;
            }
            catch (Exception)
            {
                _connectionAttempts++;
                return false;
            }
        }

        public async Task<string> SendCommandAsync(string command, string data = "")
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(ModLoaderClient));

            for (int retry = 0; retry < MaxRetries; retry++)
            {
                if (!IsConnected)
                {
                    if (!await ConnectAsync())
                    {
                        if (retry == MaxRetries - 1)
                            return string.Empty;

                        await Task.Delay(500 * (retry + 1));
                        continue;
                    }
                }

                try
                {
                    NamedPipeClientStream? pipe;
                    lock (_lock)
                    {
                        pipe = _pipeClient;
                    }

                    if (pipe == null || !pipe.IsConnected)
                    {
                        await ConnectAsync();
                        continue;
                    }

                    var msg = new IPCMessage(command, data);
                    byte[] buffer = StructToBytes(msg);

                    await pipe.WriteAsync(buffer, 0, buffer.Length);
                    await pipe.FlushAsync();

                    byte[] responseBuffer = new byte[8192];
                    int bytesRead = await pipe.ReadAsync(responseBuffer, 0, responseBuffer.Length);

                    return Encoding.UTF8.GetString(responseBuffer, 0, bytesRead);
                }
                catch (IOException)
                {
                    lock (_lock)
                    {
                        _pipeClient?.Dispose();
                        _pipeClient = null;
                    }

                    if (retry < MaxRetries - 1)
                    {
                        await Task.Delay(500 * (retry + 1));
                        continue;
                    }

                    return string.Empty;
                }
                catch (Exception)
                {
                    lock (_lock)
                    {
                        _pipeClient?.Dispose();
                        _pipeClient = null;
                    }
                    return string.Empty;
                }
            }

            return string.Empty;
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

            lock (_lock)
            {
                _disposed = true;
                _pipeClient?.Dispose();
                _pipeClient = null;
            }

            GC.SuppressFinalize(this);
        }

        ~ModLoaderClient()
        {
            Dispose();
        }
    }
}