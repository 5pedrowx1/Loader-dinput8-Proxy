using System;
using System.Drawing;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using GTAVModManager.Models;
using GTAVModManager.Services;

namespace GTAVModManager.UserControls
{
    public partial class PerformanceControl : UserControl
    {
        private readonly ModLoaderClient _client;
        private PerformanceInfo? _currentPerformance;
        private ModsResponse? _currentMods;
        private System.Windows.Forms.Timer? _refreshTimer;

        public PerformanceControl()
        {
            InitializeComponent();
            _client = new ModLoaderClient();

            SetupEventHandlers();

            _refreshTimer = new System.Windows.Forms.Timer { Interval = 2000 };
            _refreshTimer.Tick += async (s, e) => await RefreshPerformance();

            Load += async (s, e) =>
            {
                await _client.ConnectAsync();
                await RefreshPerformance();
                _refreshTimer.Start();
            };
        }


        private void SetupEventHandlers()
        {
            btnReset.Click += async (s, e) => await ResetStatistics();

            performanceTable.CellFormatting += (s, e) =>
            {
                if (e.ColumnIndex == performanceTable.Columns["colStatus"]?.Index && e.RowIndex >= 0)
                {
                    var mod = performanceTable.Rows[e.RowIndex].DataBoundItem as ModInfo;
                    if (mod != null)
                    {
                        e.Value = mod.Loaded ? "✓" : "✗";
                        e.CellStyle.ForeColor = mod.Loaded
                            ? Color.FromArgb(0, 255, 135)
                            : Color.FromArgb(255, 77, 77);
                        e.CellStyle.Font = new Font(e.CellStyle.Font.FontFamily, 12, FontStyle.Bold);
                    }
                }

                if (e.ColumnIndex == performanceTable.Columns["colAvgTime"]?.Index && e.RowIndex >= 0)
                {
                    var mod = performanceTable.Rows[e.RowIndex].DataBoundItem as ModInfo;
                    if (mod != null && mod.AvgExecutionUs > 0)
                    {
                        if (mod.AvgExecutionUs > 1000) 
                            e.CellStyle.ForeColor = Color.FromArgb(255, 77, 77);
                        else if (mod.AvgExecutionUs > 500) 
                            e.CellStyle.ForeColor = Color.FromArgb(255, 204, 0);
                        else
                            e.CellStyle.ForeColor = Color.FromArgb(0, 255, 135);
                    }
                }
            };
        }

        private async Task RefreshPerformance()
        {
            if (!_client.IsConnected)
            {
                await _client.ConnectAsync();
                return;
            }

            try
            {
                var perfJson = await _client.GetPerformanceAsync();
                if (!string.IsNullOrEmpty(perfJson))
                {
                    _currentPerformance = JsonSerializer.Deserialize<PerformanceInfo>(perfJson);
                    UpdateSummary();
                }

                var modsJson = await _client.GetModsAsync();
                if (!string.IsNullOrEmpty(modsJson))
                {
                    _currentMods = JsonSerializer.Deserialize<ModsResponse>(modsJson);
                    UpdateModsTable();
                }
            }
            catch { }
        }

        private void UpdateSummary()
        {
            if (_currentPerformance == null) return;

            long totalCalls = 0;
            long totalTime = 0;
            long peakTime = 0;

            if (_currentMods != null)
            {
                foreach (var mod in _currentMods.Mods)
                {
                    totalCalls += mod.CallCount;
                    totalTime += mod.CallCount * mod.AvgExecutionUs;
                    if (mod.AvgExecutionUs > peakTime)
                        peakTime = mod.AvgExecutionUs;
                }
            }

            lblTotalCalls.Text = $"Total Calls: {totalCalls:N0}";

            double avgTimeMs = totalCalls > 0 ? (totalTime / (double)totalCalls / 1000.0) : 0;
            lblAvgTime.Text = $"Avg Time: {avgTimeMs:F2} ms";

            double peakTimeMs = peakTime / 1000.0;
            lblPeakTime.Text = $"Peak: {peakTimeMs:F2} ms";

            if (avgTimeMs > 5)
            {
                lblAvgTime.ForeColor = Color.FromArgb(255, 77, 77);
            }
            else if (avgTimeMs > 2)
            {
                lblAvgTime.ForeColor = Color.FromArgb(255, 204, 0);
            }
            else
            {
                lblAvgTime.ForeColor = Color.FromArgb(0, 255, 135);
            }
        }

        private void UpdateModsTable()
        {
            if (_currentMods == null) return;

            var sortedMods = _currentMods.Mods
                .Where(m => m.CallCount > 0)
                .OrderByDescending(m => m.AvgExecutionUs)
                .ToList();

            performanceTable.DataSource = null;
            performanceTable.DataSource = sortedMods;
        }

        private async Task ResetStatistics()
        {
            var confirmResult = MessageBox.Show(
                "This will reset all performance statistics. Continue?",
                "Confirm Reset",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    // Note: add reset command to IPCServer.cpp
                    MessageBox.Show(
                        "Statistics reset is not yet implemented.\n" +
                        "Please restart the game to reset statistics.",
                        "Not Implemented",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error resetting statistics: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}