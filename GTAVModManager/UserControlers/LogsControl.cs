using GTAVModManager.Logics;

namespace GTAVModManager.UserControlers
{
    public partial class LogsControl : UserControl
    {
        public LogsControl()
        {
            InitializeComponent();
        }

        private void BtnClearLogs_Click(object sender, EventArgs e)
        {
            txtLogs.Clear();
        }

        public void UpdateLogs(LogsResponse logsData)
        {
            txtLogs.Clear();
            foreach (var log in logsData.Logs)
            {
                string icon = log.Level switch
                {
                    "SUCCESS" => "✅",
                    "ERROR" => "❌",
                    "WARNING" => "⚠️",
                    _ => "ℹ️"
                };
                txtLogs.AppendText($"[{log.Timestamp}] {icon} {log.Message}\r\n");
            }
        }

        public void AddLog(string message, string level)
        {
            if (txtLogs.InvokeRequired)
            {
                txtLogs.Invoke(new Action<string, string>(AddLog), message, level);
                return;
            }
            string icon = level switch
            {
                "SUCCESS" => "✅",
                "ERROR" => "❌",
                "WARNING" => "⚠️",
                _ => "ℹ️"
            };
            txtLogs.AppendText($"[{DateTime.Now:HH:mm:ss}] {icon} {message}\r\n");
        }
    }
}
