using GTAVModManager.Models;
using GTAVModManager.Services;
using System.Text;
using System.Text.Json;

namespace GTAVModManager.UserControls
{
    public partial class LogsControl : UserControl
    {
        private readonly ModLoaderClient _client;
        private LogsResponse? _currentLogs;
        private string _filterLevel = "ALL";
        private bool _autoScroll = true;

        public LogsControl()
        {
            InitializeComponent();
            _client = new ModLoaderClient();
        }

        private async void LogsControl_Load(object sender, EventArgs e)
        {
            await _client.ConnectAsync();
            await RefreshLogs();
            _refreshTimer.Start();
        }


        private async void _refreshTimer_Tick(object sender, EventArgs e)
        {
            await RefreshLogs();
        }

        private void SetFilter(string level)
        {
            _filterLevel = level;
            btnAll.FillColor = Color.FromArgb(35, 35, 35);
            btnInfo.FillColor = Color.FromArgb(35, 35, 35);
            btnWarning.FillColor = Color.FromArgb(35, 35, 35);
            btnError.FillColor = Color.FromArgb(35, 35, 35);
            btnAll.ForeColor = Color.FromArgb(180, 180, 180);
            btnInfo.ForeColor = Color.FromArgb(180, 180, 180);
            btnWarning.ForeColor = Color.FromArgb(180, 180, 180);
            btnError.ForeColor = Color.FromArgb(180, 180, 180);

            switch (level)
            {
                case "ALL":
                    btnAll.FillColor = Color.FromArgb(0, 255, 135);
                    btnAll.ForeColor = Color.Black;
                    break;
                case "INFO":
                    btnInfo.FillColor = Color.FromArgb(0, 204, 255);
                    btnInfo.ForeColor = Color.Black;
                    break;
                case "WARNING":
                    btnWarning.FillColor = Color.FromArgb(255, 204, 0);
                    btnWarning.ForeColor = Color.Black;
                    break;
                case "ERROR":
                    btnError.FillColor = Color.FromArgb(255, 77, 77);
                    btnError.ForeColor = Color.Black;
                    break;
            }

            _autoScroll = true;
            DisplayFilteredLogs();
        }

        private async Task RefreshLogs()
        {
            if (!_client.IsConnected)
            {
                await _client.ConnectAsync();
                return;
            }

            try
            {
                var json = await _client.GetLogsAsync();
                if (string.IsNullOrEmpty(json)) return;

                _currentLogs = JsonSerializer.Deserialize<LogsResponse>(json);
                DisplayFilteredLogs();
            }
            catch { }
        }

        private void DisplayFilteredLogs()
        {
            if (_currentLogs == null) return;

            var logsToDisplay = _filterLevel == "ALL"
                ? _currentLogs.Logs
                : _currentLogs.Logs.Where(l => l.Level == _filterLevel).ToList();

            var sb = new StringBuilder();
            sb.AppendLine($"=== GTA V Mod Loader - Logs ({_filterLevel}) ===");
            sb.AppendLine($"Total Logs: {logsToDisplay.Count} / {_currentLogs.Count}");
            sb.AppendLine($"Last Update: {DateTime.Now:HH:mm:ss}");
            sb.AppendLine(new string('=', 60));
            sb.AppendLine();

            foreach (var log in logsToDisplay)
            {
                string levelIcon = log.Level switch
                {
                    "ERROR" => "[✗]",
                    "WARNING" => "[⚠]",
                    "SUCCESS" => "[✓]",
                    "INFO" => "[ℹ]",
                    "DEBUG" => "[�]",
                    _ => "[ ]"
                };

                sb.AppendLine($"{levelIcon} [{log.Timestamp}] [{log.Level}] {log.Message}");
            }

            if (txtLogs.Text != sb.ToString())
            {
                int previousLength = txtLogs.Text.Length;
                txtLogs.Text = sb.ToString();

                if (_autoScroll || previousLength > 0)
                {
                    txtLogs.SelectionStart = txtLogs.Text.Length;
                    txtLogs.ScrollToCaret();
                }
            }
        }

        private async Task ClearLogs()
        {
            var confirmResult = MessageBox.Show(
                "Are you sure you want to clear all logs?\n\n" +
                "This action will clear the log history from the loader.\n" +
                "Log files on disk will not be affected.",
                "Confirm Clear",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    btnClear.Enabled = false;
                    btnClear.Text = "Clearing...";

                    var result = await _client.ClearLogsAsync();

                    if (result == "SUCCESS")
                    {
                        txtLogs.Clear();
                        MessageBox.Show("Logs cleared successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await Task.Delay(500);
                        await RefreshLogs();
                    }
                    else
                    {
                        MessageBox.Show("Failed to clear logs.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error clearing logs: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    btnClear.Enabled = true;
                    btnClear.Text = "Clear";
                }
            }
        }

        private async Task ExportLogs()
        {
            if (_currentLogs == null || _currentLogs.Logs.Count == 0)
            {
                MessageBox.Show("No logs to export.", "No Logs",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var saveFileDialog = new SaveFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|Log Files (*.log)|*.log|All Files (*.*)|*.*",
                Title = "Export Logs",
                FileName = $"GTAVModLoader_Logs_{DateTime.Now:yyyyMMdd_HHmmss}.txt",
                DefaultExt = "txt"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    btnExport.Enabled = false;
                    btnExport.Text = "Exporting...";

                    var sb = new StringBuilder();
                    sb.AppendLine("=".PadRight(80, '='));
                    sb.AppendLine($"GTA V Mod Loader - Logs Export");
                    sb.AppendLine($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    sb.AppendLine($"Filter: {_filterLevel}");
                    sb.AppendLine($"Total Logs: {_currentLogs.Count}");
                    sb.AppendLine("=".PadRight(80, '='));
                    sb.AppendLine();

                    var logsToExport = _filterLevel == "ALL"
                        ? _currentLogs.Logs
                        : _currentLogs.Logs.Where(l => l.Level == _filterLevel).ToList();

                    sb.AppendLine($"Exported Logs: {logsToExport.Count}");
                    sb.AppendLine("-".PadRight(80, '-'));
                    sb.AppendLine();

                    var errorCount = logsToExport.Count(l => l.Level == "ERROR");
                    var warningCount = logsToExport.Count(l => l.Level == "WARNING");
                    var infoCount = logsToExport.Count(l => l.Level == "INFO");
                    var successCount = logsToExport.Count(l => l.Level == "SUCCESS");
                    var debugCount = logsToExport.Count(l => l.Level == "DEBUG");

                    sb.AppendLine("Summary:");
                    sb.AppendLine($"  Errors:   {errorCount}");
                    sb.AppendLine($"  Warnings: {warningCount}");
                    sb.AppendLine($"  Info:     {infoCount}");
                    sb.AppendLine($"  Success:  {successCount}");
                    sb.AppendLine($"  Debug:    {debugCount}");
                    sb.AppendLine();
                    sb.AppendLine("-".PadRight(80, '-'));
                    sb.AppendLine();

                    foreach (var log in logsToExport)
                    {
                        sb.AppendLine($"[{log.Timestamp}] [{log.Level.PadRight(8)}] {log.Message}");
                    }

                    sb.AppendLine();
                    sb.AppendLine("=".PadRight(80, '='));
                    sb.AppendLine($"End of Export - {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    sb.AppendLine("=".PadRight(80, '='));

                    await File.WriteAllTextAsync(saveFileDialog.FileName, sb.ToString());

                    var result = MessageBox.Show(
                        $"Logs exported successfully!\n\n" +
                        $"File: {Path.GetFileName(saveFileDialog.FileName)}\n" +
                        $"Location: {Path.GetDirectoryName(saveFileDialog.FileName)}\n" +
                        $"Size: {new FileInfo(saveFileDialog.FileName).Length / 1024.0:F2} KB\n\n" +
                        $"Do you want to open the file location?",
                        "Export Complete",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start("explorer.exe", $"/select,\"{saveFileDialog.FileName}\"");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting logs: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    btnExport.Enabled = true;
                    btnExport.Text = "Export Logs";
                }
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            SetFilter("ALL");
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            SetFilter("INFO");
        }

        private void btnWarning_Click(object sender, EventArgs e)
        {
            SetFilter("WARNING");
        }

        private void btnError_Click(object sender, EventArgs e)
        {
            SetFilter("ERROR");
        }

        private async void btnClear_Click(object sender, EventArgs e)
        {
            await ClearLogs();
        }

        private async void btnExport_Click(object sender, EventArgs e)
        {
            await ExportLogs();
        }

        private void txtLogs_Click(object sender, EventArgs e)
        {
            _autoScroll = false;
        }

        private void txtLogs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End && e.Control)
                _autoScroll = true;
        }

        private void txtLogs_Scroll(object sender, ScrollEventArgs e)
        {
            _autoScroll = true;
        }
    }
}