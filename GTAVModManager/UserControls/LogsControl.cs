using System.Text;
using System.Text.Json;
using GTAVModManager.Models;
using GTAVModManager.Services;

namespace GTAVModManager.UserControls
{
    public partial class LogsControl : UserControl
    {
        private readonly ModLoaderClient _client;
        private LogsResponse? _currentLogs;
        private string _filterLevel = "ALL";
        private System.Windows.Forms.Timer? _refreshTimer;

        public LogsControl()
        {
            InitializeComponent();
            _client = new ModLoaderClient();

            SetupEventHandlers();

            _refreshTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            _refreshTimer.Tick += async (s, e) => await RefreshLogs();

            Load += async (s, e) =>
            {
                await _client.ConnectAsync();
                await RefreshLogs();
                _refreshTimer.Start();
            };
        }

        private void SetupEventHandlers()
        {
            btnAll.Click += (s, e) => SetFilter("ALL");
            btnInfo.Click += (s, e) => SetFilter("INFO");
            btnWarning.Click += (s, e) => SetFilter("WARNING");
            btnError.Click += (s, e) => SetFilter("ERROR");

            btnClear.Click += async (s, e) => await ClearLogs();
            btnExport.Click += async (s, e) => await ExportLogs();
        }

        private void SetFilter(string level)
        {
            _filterLevel = level;
            btnAll.FillColor = Color.FromArgb(35, 35, 35);
            btnInfo.FillColor = Color.FromArgb(35, 35, 35);
            btnWarning.FillColor = Color.FromArgb(35, 35, 35);
            btnError.FillColor = Color.FromArgb(35, 35, 35);

            switch (level)
            {
                case "ALL":
                    btnAll.FillColor = Color.FromArgb(0, 255, 135);
                    break;
                case "INFO":
                    btnInfo.FillColor = Color.FromArgb(0, 204, 255);
                    break;
                case "WARNING":
                    btnWarning.FillColor = Color.FromArgb(255, 204, 0);
                    break;
                case "ERROR":
                    btnError.FillColor = Color.FromArgb(255, 77, 77);
                    break;
            }

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

            foreach (var log in logsToDisplay)
            {
                sb.AppendLine($"[{log.Timestamp}] [{log.Level}] {log.Message}");
            }

            if (txtLogs.Text != sb.ToString())
            {
                int selectionStart = txtLogs.SelectionStart;
                txtLogs.Text = sb.ToString();

                if (selectionStart >= txtLogs.Text.Length - 100)
                {
                    txtLogs.SelectionStart = txtLogs.Text.Length;
                    txtLogs.ScrollToCaret();
                }
            }
        }

        private async Task ClearLogs()
        {
            var confirmResult = MessageBox.Show(
                "Are you sure you want to clear all logs?",
                "Confirm Clear",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    var result = await _client.ClearLogsAsync();

                    if (result == "SUCCESS")
                    {
                        txtLogs.Clear();
                        MessageBox.Show("Logs cleared successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await RefreshLogs();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error clearing logs: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                FileName = $"GTAVModLoader_Logs_{DateTime.Now:yyyyMMdd_HHmmss}.txt"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("=".PadRight(80, '='));
                    sb.AppendLine($"GTA V Mod Loader - Logs Export");
                    sb.AppendLine($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    sb.AppendLine($"Total Logs: {_currentLogs.Count}");
                    sb.AppendLine("=".PadRight(80, '='));
                    sb.AppendLine();

                    foreach (var log in _currentLogs.Logs)
                    {
                        sb.AppendLine($"[{log.Timestamp}] [{log.Level}] {log.Message}");
                    }

                    await File.WriteAllTextAsync(saveFileDialog.FileName, sb.ToString());

                    MessageBox.Show($"Logs exported successfully to:\n{saveFileDialog.FileName}",
                        "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting logs: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}