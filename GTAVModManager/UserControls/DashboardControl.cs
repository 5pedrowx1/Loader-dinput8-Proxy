using System.Diagnostics;
using System.Text.Json;
using Microsoft.VisualBasic.Devices;
using GTAVModManager.Models;
using GTAVModManager.Services;

namespace GTAVModManager.UserControls
{
    public partial class DashboardControl : UserControl
    {
        private readonly ModLoaderClient _client;
        private StatusInfo? _currentStatus;
        private PerformanceInfo? _currentPerformance;
        private readonly ComputerInfo _computerInfo = new ComputerInfo();
        private readonly Process _currentProcess;

        // Para cálculo preciso de CPU
        private DateTime _lastCpuCheck = DateTime.UtcNow;
        private TimeSpan _lastTotalProcessorTime = TimeSpan.Zero;
        private double _currentCpuUsage = 0;

        // Para FPS da UI
        private Stopwatch _fpsStopwatch = new Stopwatch();
        private int _frameCount = 0;
        private double _lastFps = 0;
        private bool _isInitialized = false;

        public DashboardControl()
        {
            InitializeComponent();
            _client = new ModLoaderClient();
            _currentProcess = Process.GetCurrentProcess();
        }

        private async void DashboardControl_Load(object? sender, EventArgs e)
        {
            if (_isInitialized) return;
            _isInitialized = true;

            // Inicializar primeira leitura de CPU
            _lastTotalProcessorTime = _currentProcess.TotalProcessorTime;
            _lastCpuCheck = DateTime.UtcNow;

            await ConnectToLoader();
            updateTimer.Start();
            _fpsStopwatch.Start();
        }

        private async Task ConnectToLoader()
        {
            try
            {
                bool connected = await _client.ConnectAsync();
                UpdateConnectionStatus(connected);

                if (connected)
                {
                    await UpdateDashboard();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao conectar: {ex.Message}");
                UpdateConnectionStatus(false);
            }
        }

        private void UpdateConnectionStatus(bool connected)
        {
            if (InvokeRequired)
            {
                Invoke(() => UpdateConnectionStatus(connected));
                return;
            }

            if (connected)
            {
                statusIndicator.BackColor = Color.FromArgb(0, 255, 135);
                statusIndicator.FillColor = Color.FromArgb(0, 255, 135);
                lblStatus.Text = "Status: Connected";
                lblStatus.ForeColor = Color.FromArgb(0, 255, 135);
            }
            else
            {
                statusIndicator.BackColor = Color.FromArgb(255, 77, 77);
                statusIndicator.FillColor = Color.FromArgb(255, 77, 77);
                lblStatus.Text = "Status: Disconnected";
                lblStatus.ForeColor = Color.FromArgb(255, 77, 77);
            }
        }

        private async Task UpdateDashboard()
        {
            if (!_client.IsConnected)
            {
                await ConnectToLoader();
                return;
            }

            try
            {
                // Buscar status do loader
                var statusJson = await _client.GetStatusAsync();
                if (!string.IsNullOrEmpty(statusJson))
                {
                    _currentStatus = JsonSerializer.Deserialize<StatusInfo>(statusJson);
                    UpdateStatusDisplay();
                }

                // Buscar performance do loader
                var perfJson = await _client.GetPerformanceAsync();
                if (!string.IsNullOrEmpty(perfJson))
                {
                    _currentPerformance = JsonSerializer.Deserialize<PerformanceInfo>(perfJson);
                }

                // Atualizar displays com dados reais do sistema
                UpdatePerformanceDisplay();
                UpdateConnectionStatus(true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao atualizar dashboard: {ex.Message}");
                UpdateConnectionStatus(false);
            }
        }

        private void UpdateStatusDisplay()
        {
            if (InvokeRequired)
            {
                Invoke(UpdateStatusDisplay);
                return;
            }

            if (_currentStatus == null) return;

            lblUptime.Text = $"Uptime: {_currentStatus.UptimeFormatted}";
            lblModsLoaded.Text = $"Mods Loaded: {_currentStatus.ModsLoaded}";

            if (_currentStatus.ServerRunning && _currentStatus.GameDetected)
            {
                statusIndicator.BackColor = Color.FromArgb(0, 255, 135);
                statusIndicator.FillColor = Color.FromArgb(0, 255, 135);
            }
            else if (_currentStatus.ServerRunning)
            {
                statusIndicator.BackColor = Color.FromArgb(255, 204, 0);
                statusIndicator.FillColor = Color.FromArgb(255, 204, 0);
            }
            else
            {
                statusIndicator.BackColor = Color.FromArgb(255, 77, 77);
                statusIndicator.FillColor = Color.FromArgb(255, 77, 77);
            }
        }

        private void UpdatePerformanceDisplay()
        {
            if (InvokeRequired)
            {
                Invoke(UpdatePerformanceDisplay);
                return;
            }

            // Obter estatísticas reais do sistema
            float cpuUsage = GetSystemCpuUsage();
            double usedMemoryMb = GetSystemMemoryUsage();

            // Atualizar displays
            UpdateCpuDisplay(cpuUsage);
            UpdateRamDisplay(usedMemoryMb);
            UpdateFPSCounter();
        }

        private float GetSystemCpuUsage()
        {
            try
            {
                // Calcular CPU usage total do sistema usando PerformanceCounter
                using (var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total"))
                {
                    // Primeira leitura sempre retorna 0, então fazemos duas
                    cpuCounter.NextValue();
                    System.Threading.Thread.Sleep(100); // Pequena pausa para medição
                    float cpuUsage = cpuCounter.NextValue();

                    return Math.Min(100f, Math.Max(0f, cpuUsage));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao ler CPU (método 1): {ex.Message}");

                // Fallback: usar método alternativo baseado no processo
                try
                {
                    var currentTime = DateTime.UtcNow;
                    var currentTotalProcessorTime = _currentProcess.TotalProcessorTime;

                    var cpuUsedMs = (currentTotalProcessorTime - _lastTotalProcessorTime).TotalMilliseconds;
                    var totalMsPassed = (currentTime - _lastCpuCheck).TotalMilliseconds;
                    var cpuUsageTotal = cpuUsedMs / (Environment.ProcessorCount * totalMsPassed);

                    _currentCpuUsage = cpuUsageTotal * 100;
                    _lastCpuCheck = currentTime;
                    _lastTotalProcessorTime = currentTotalProcessorTime;

                    return (float)Math.Min(100.0, Math.Max(0.0, _currentCpuUsage));
                }
                catch (Exception ex2)
                {
                    Debug.WriteLine($"Erro ao ler CPU (método 2): {ex2.Message}");
                    return 0f;
                }
            }
        }

        private double GetSystemMemoryUsage()
        {
            try
            {
                // Obter memória disponível usando PerformanceCounter
                using (var ramCounter = new PerformanceCounter("Memory", "Available MBytes"))
                {
                    double availableMB = ramCounter.NextValue();
                    double totalMB = _computerInfo.TotalPhysicalMemory / (1024.0 * 1024.0);
                    double usedMB = totalMB - availableMB;

                    return Math.Max(0, usedMB);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao ler RAM: {ex.Message}");

                // Fallback: usar apenas o total menos o disponível reportado pelo sistema
                try
                {
                    double totalMB = _computerInfo.TotalPhysicalMemory / (1024.0 * 1024.0);
                    double availableMB = _computerInfo.AvailablePhysicalMemory / (1024.0 * 1024.0);
                    return Math.Max(0, totalMB - availableMB);
                }
                catch
                {
                    return 0;
                }
            }
        }

        private void UpdateCpuDisplay(float cpuUsage)
        {
            int cpuValue = Math.Min(100, Math.Max(0, (int)cpuUsage));
            cpuUsageBar.Value = cpuValue;
            lblCPUUsage.Text = $"{cpuUsage:F1}%";

            if (cpuValue > 80)
            {
                cpuUsageBar.ProgressColor = Color.FromArgb(255, 77, 77);
                cpuUsageBar.ProgressColor2 = Color.FromArgb(255, 120, 120);
                lblCPUUsage.ForeColor = Color.FromArgb(255, 77, 77);
            }
            else if (cpuValue > 50)
            {
                cpuUsageBar.ProgressColor = Color.FromArgb(255, 204, 0);
                cpuUsageBar.ProgressColor2 = Color.FromArgb(255, 220, 100);
                lblCPUUsage.ForeColor = Color.FromArgb(255, 204, 0);
            }
            else
            {
                cpuUsageBar.ProgressColor = Color.FromArgb(0, 255, 135);
                cpuUsageBar.ProgressColor2 = Color.FromArgb(0, 204, 255);
                lblCPUUsage.ForeColor = Color.FromArgb(234, 234, 234);
            }
        }

        private void UpdateRamDisplay(double usedMemoryMb)
        {
            double totalRamMB = _computerInfo.TotalPhysicalMemory / (1024.0 * 1024.0);
            int ramPercent = (int)((usedMemoryMb / totalRamMB) * 100);
            ramPercent = Math.Min(100, Math.Max(0, ramPercent));

            ramUsageBar.Value = ramPercent;
            lblRAMUsage.Text = $"{usedMemoryMb / 1024.0:F2} GB / {totalRamMB / 1024.0:F0} GB";

            if (ramPercent > 80)
            {
                ramUsageBar.ProgressColor = Color.FromArgb(255, 77, 77);
                ramUsageBar.ProgressColor2 = Color.FromArgb(255, 120, 120);
                lblRAMUsage.ForeColor = Color.FromArgb(255, 77, 77);
            }
            else if (ramPercent > 50)
            {
                ramUsageBar.ProgressColor = Color.FromArgb(255, 204, 0);
                ramUsageBar.ProgressColor2 = Color.FromArgb(255, 220, 100);
                lblRAMUsage.ForeColor = Color.FromArgb(255, 204, 0);
            }
            else
            {
                ramUsageBar.ProgressColor = Color.FromArgb(0, 255, 135);
                ramUsageBar.ProgressColor2 = Color.FromArgb(0, 204, 255);
                lblRAMUsage.ForeColor = Color.FromArgb(234, 234, 234);
            }
        }

        private void UpdateFPSCounter()
        {
            _frameCount++;

            if (_fpsStopwatch.ElapsedMilliseconds >= 1000)
            {
                _lastFps = _frameCount / (_fpsStopwatch.ElapsedMilliseconds / 1000.0);
                _frameCount = 0;
                _fpsStopwatch.Restart();
            }

            lblFPSValue.Text = $"{_lastFps:F0}";

            if (_lastFps >= 60)
                lblFPSValue.ForeColor = Color.FromArgb(0, 255, 135);
            else if (_lastFps >= 30)
                lblFPSValue.ForeColor = Color.FromArgb(255, 204, 0);
            else
                lblFPSValue.ForeColor = Color.FromArgb(255, 77, 77);
        }

        private async void updateTimer_Tick_1(object sender, EventArgs e)
        {
            await UpdateDashboard();
        }
    }
}