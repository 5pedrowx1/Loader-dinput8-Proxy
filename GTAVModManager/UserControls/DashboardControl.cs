using System.Text.Json;
using GTAVModManager.Models;
using GTAVModManager.Services;

namespace GTAVModManager.UserControls
{
    public partial class DashboardControl : UserControl
    {
        private readonly ModLoaderClient _client;
        private StatusInfo? _currentStatus;
        private PerformanceInfo? _currentPerformance;

        public DashboardControl()
        {
            InitializeComponent();
            _client = new ModLoaderClient();

            updateTimer.Interval = 1500;
            updateTimer.Tick += async (s, e) => await UpdateDashboard();

            Load += async (s, e) =>
            {
                await ConnectToLoader();
                updateTimer.Start();
            };
        }

        private async Task ConnectToLoader()
        {
            try
            {
                bool connected = await _client.ConnectAsync();
                if (connected)
                {
                    statusIndicator.BackColor = Color.FromArgb(0, 255, 135);
                    lblStatus.Text = "Status: Connected";
                    lblStatus.ForeColor = Color.FromArgb(0, 255, 135);
                }
                else
                {
                    statusIndicator.BackColor = Color.FromArgb(255, 77, 77);
                    lblStatus.Text = "Status: Disconnected";
                    lblStatus.ForeColor = Color.FromArgb(255, 77, 77);
                }
            }
            catch
            {
                statusIndicator.BackColor = Color.FromArgb(255, 77, 77);
                lblStatus.Text = "Status: Error";
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
                var statusJson = await _client.GetStatusAsync();
                if (!string.IsNullOrEmpty(statusJson))
                {
                    _currentStatus = JsonSerializer.Deserialize<StatusInfo>(statusJson);
                    UpdateStatusDisplay();
                }

                var perfJson = await _client.GetPerformanceAsync();
                if (!string.IsNullOrEmpty(perfJson))
                {
                    _currentPerformance = JsonSerializer.Deserialize<PerformanceInfo>(perfJson);
                    UpdatePerformanceDisplay();
                }
            }
            catch
            {
                await ConnectToLoader();
            }
        }

        private void UpdateStatusDisplay()
        {
            if (_currentStatus == null) return;

            lblUptime.Text = $"Uptime: {_currentStatus.UptimeFormatted}";
            lblModsLoaded.Text = $"Mods Loaded: {_currentStatus.ModsLoaded}";

            statusIndicator.BackColor = _currentStatus.ServerRunning
                ? Color.FromArgb(0, 255, 135)
                : Color.FromArgb(255, 77, 77);
        }

        private void UpdatePerformanceDisplay()
        {
            if (_currentPerformance == null) return;

            int cpuValue = Math.Min(100, (int)_currentPerformance.CpuPercent);
            cpuUsageBar.Value = cpuValue;
            lblCPUUsage.Text = $"{_currentPerformance.CpuPercent:F1}%";

            if (cpuValue > 80)
                cpuUsageBar.ProgressColor = Color.FromArgb(255, 77, 77);
            else if (cpuValue > 50)
                cpuUsageBar.ProgressColor = Color.FromArgb(255, 204, 0);
            else
                cpuUsageBar.ProgressColor = Color.FromArgb(0, 255, 135);

            long totalRam = 16384; // Assume 16GB, could be dynamic
            int ramPercent = (int)((double)_currentPerformance.MemoryMb / totalRam * 100);
            ramUsageBar.Value = Math.Min(100, ramPercent);
            lblRAMUsage.Text = $"{_currentPerformance.MemoryMb} MB / {totalRam} MB";

            if (ramPercent > 80)
                ramUsageBar.ProgressColor = Color.FromArgb(255, 77, 77);
            else if (ramPercent > 50)
                ramUsageBar.ProgressColor = Color.FromArgb(255, 204, 0);
            else
                ramUsageBar.ProgressColor = Color.FromArgb(0, 255, 135);

            // FPS (simulated - would need game integration)
            lblFPSValue.Text = "60";
        }
    }
}