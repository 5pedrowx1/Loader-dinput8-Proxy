using System.Text.Json;
using GTAVModManager.Models;
using GTAVModManager.Services;

namespace GTAVModManager.UserControls
{
    public partial class PerformanceControl : UserControl
    {
        private readonly ModLoaderClient _client;
        private PerformanceInfo? _currentPerformance;
        private ModsResponse? _currentMods;

        public PerformanceControl()
        {
            InitializeComponent();
            _client = new ModLoaderClient();
        }

        private async void _refreshTimer_Tick(object sender, EventArgs e)
        {
            await RefreshPerformance();
        }

        private async void PerformanceControl_Load(object sender, EventArgs e)
        {
            await _client.ConnectAsync();
            await RefreshPerformance();
            _refreshTimer.Start();
        }

        private void PerformanceTable_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var mod = performanceTable.Rows[e.RowIndex].DataBoundItem as ModInfo;
            if (mod == null) return;

            if (e.ColumnIndex == performanceTable.Columns["colStatus"]?.Index)
            {
                e.Value = mod.Loaded ? "✓" : "✗";
                e.CellStyle.ForeColor = mod.Loaded
                    ? Color.FromArgb(0, 255, 135)
                    : Color.FromArgb(255, 77, 77);
                e.CellStyle.Font = new Font(e.CellStyle.Font.FontFamily, 12, FontStyle.Bold);
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (e.ColumnIndex == performanceTable.Columns["colAvgTime"]?.Index)
            {
                if (mod.AvgExecutionUs > 0)
                {
                    double avgMs = mod.AvgExecutionUs / 1000.0;
                    e.Value = $"{avgMs:F3}";

                    if (mod.AvgExecutionUs > 1000)
                        e.CellStyle.ForeColor = Color.FromArgb(255, 77, 77);
                    else if (mod.AvgExecutionUs > 500)
                        e.CellStyle.ForeColor = Color.FromArgb(255, 204, 0);
                    else
                        e.CellStyle.ForeColor = Color.FromArgb(0, 255, 135);
                }
                else
                {
                    e.Value = "N/A";
                    e.CellStyle.ForeColor = Color.FromArgb(120, 120, 120);
                }
            }

            if (e.ColumnIndex == performanceTable.Columns["colType"]?.Index)
            {
                switch (mod.Type.ToLower())
                {
                    case "scripthook":
                        e.CellStyle.ForeColor = Color.FromArgb(255, 204, 0);
                        break;
                    case "dotnet":
                        e.CellStyle.ForeColor = Color.FromArgb(0, 204, 255);
                        break;
                    case "mod":
                        e.CellStyle.ForeColor = Color.FromArgb(0, 255, 135);
                        break;
                    default:
                        e.CellStyle.ForeColor = Color.FromArgb(180, 180, 180);
                        break;
                }
            }
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
            lblAvgTime.Text = $"Avg Time: {avgTimeMs:F3} ms";

            double peakTimeMs = peakTime / 1000.0;
            lblPeakTime.Text = $"Peak: {peakTimeMs:F3} ms";

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

            if (peakTimeMs > 10)
            {
                lblPeakTime.ForeColor = Color.FromArgb(255, 77, 77);
            }
            else if (peakTimeMs > 5)
            {
                lblPeakTime.ForeColor = Color.FromArgb(255, 204, 0);
            }
            else
            {
                lblPeakTime.ForeColor = Color.FromArgb(0, 255, 135);
            }
        }

        private void UpdateModsTable()
        {
            if (_currentMods == null) return;

            var sortedMods = _currentMods.Mods
                .Where(m => m.CallCount > 0)
                .OrderByDescending(m => m.AvgExecutionUs)
                .ToList();

            int firstDisplayedScrollingRowIndex = -1;
            if (performanceTable.FirstDisplayedScrollingRowIndex >= 0)
            {
                firstDisplayedScrollingRowIndex = performanceTable.FirstDisplayedScrollingRowIndex;
            }

            performanceTable.DataSource = null;
            performanceTable.DataSource = sortedMods;

            if (firstDisplayedScrollingRowIndex >= 0 &&
                firstDisplayedScrollingRowIndex < performanceTable.RowCount)
            {
                performanceTable.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex;
            }
        }

        private async void ResetStatistics(object? sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(
                "This will reset all performance statistics. Continue?\n\n" +
                "Note: The reset functionality requires restarting the game.\n" +
                "Statistics will be cleared on the next game launch.",
                "Confirm Reset",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    btnReset.Enabled = false;
                    btnReset.Text = "Resetting...";

                    // Note: The RESET_STATS command needs to be implemented in IPCServer.cpp
                    // For now, just inform the user
                    await Task.Delay(500);

                    MessageBox.Show(
                        "Performance statistics reset feature is not yet fully implemented.\n\n" +
                        "To reset statistics, please restart GTA V.\n\n" +
                        "This feature will be added in a future update.",
                        "Reset Statistics",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    if (_currentMods != null)
                    {
                        foreach (var mod in _currentMods.Mods)
                        {
                            // Local reset for view only
                        }
                    }

                    await RefreshPerformance();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error resetting statistics: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    btnReset.Enabled = true;
                    btnReset.Text = "Reset";
                }
            }
        }
    }
}