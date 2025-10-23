using GTAVModManager.Services;

namespace GTAVModManager.UserControls
{
    public partial class SettingsControl : UserControl
    {
        private readonly ModLoaderClient _client;
        private readonly string _configPath;
        private bool _isLoading;

        public SettingsControl()
        {
            InitializeComponent();
            _client = new ModLoaderClient();
            _configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dinput8_config.ini");

            SetupEventHandlers();

            Load += async (s, e) =>
            {
                await _client.ConnectAsync();
                LoadSettings();
            };
        }

        private void SetupEventHandlers()
        {
            btnSave.Click += async (s, e) => await SaveSettings();
            btnReset.Click += (s, e) => ResetToDefaults();
        }

        private void LoadSettings()
        {
            _isLoading = true;

            try
            {
                if (!File.Exists(_configPath))
                {
                    ResetToDefaults();
                    return;
                }

                switchUIEnable.Checked = ReadBool("UI", "Enable", true);
                switchAutoLaunch.Checked = ReadBool("UI", "AutoLaunch", true);
                txtExecutable.Text = ReadString("UI", "ExecutableName", "GTAVModManager.exe");
                switchScriptHookV.Checked = ReadBool("ScriptHook", "LoadScriptHookV", true);
                switchScriptHookVDN.Checked = ReadBool("ScriptHook", "LoadScriptHookVDotNet", false);
                switchAutoLoadMods.Checked = ReadBool("AutoLoad", "AutoLoadModsFolder", true);
                switchAutoLoadScripts.Checked = ReadBool("AutoLoad", "AutoLoadScriptsFolder", true);
                txtMaxLogEntries.Text = ReadInt("Logging", "MaxLogEntries", 500).ToString();
                switchVerboseLogging.Checked = ReadBool("Logging", "VerboseLogging", false);
                switchEnableMonitor.Checked = ReadBool("Performance", "EnableMonitor", true);
                txtMonitorInterval.Text = ReadInt("Performance", "MonitorIntervalMS", 5000).ToString();
            }
            finally
            {
                _isLoading = false;
            }
        }

        private async System.Threading.Tasks.Task SaveSettings()
        {
            if (_isLoading) return;

            try
            {
                btnSave.Enabled = false;
                btnSave.Text = "Saving...";

                if (!int.TryParse(txtMaxLogEntries.Text, out int maxLogs) || maxLogs < 10 || maxLogs > 10000)
                {
                    MessageBox.Show("Max Log Entries must be between 10 and 10000.",
                        "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtMonitorInterval.Text, out int interval) || interval < 100 || interval > 60000)
                {
                    MessageBox.Show("Monitor Interval must be between 100 and 60000 ms.",
                        "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                WriteValue("UI", "Enable", switchUIEnable.Checked ? "1" : "0");
                WriteValue("UI", "AutoLaunch", switchAutoLaunch.Checked ? "1" : "0");
                WriteValue("UI", "ExecutableName", txtExecutable.Text);
                WriteValue("ScriptHook", "LoadScriptHookV", switchScriptHookV.Checked ? "1" : "0");
                WriteValue("ScriptHook", "LoadScriptHookVDotNet", switchScriptHookVDN.Checked ? "1" : "0");
                WriteValue("AutoLoad", "AutoLoadModsFolder", switchAutoLoadMods.Checked ? "1" : "0");
                WriteValue("AutoLoad", "AutoLoadScriptsFolder", switchAutoLoadScripts.Checked ? "1" : "0");
                WriteValue("Logging", "MaxLogEntries", maxLogs.ToString());
                WriteValue("Logging", "VerboseLogging", switchVerboseLogging.Checked ? "1" : "0");
                WriteValue("Performance", "EnableMonitor", switchEnableMonitor.Checked ? "1" : "0");
                WriteValue("Performance", "MonitorIntervalMS", interval.ToString());

                MessageBox.Show(
                    "Settings saved successfully!\n\nNote: Some settings require restarting GTA V to take effect.",
                    "Settings Saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
                btnSave.Text = "Save";
            }
        }

        private void ResetToDefaults()
        {
            var confirmResult = MessageBox.Show(
                "Are you sure you want to reset all settings to default values?",
                "Confirm Reset",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                _isLoading = true;

                try
                {
                    switchUIEnable.Checked = true;
                    switchAutoLaunch.Checked = true;
                    txtExecutable.Text = "GTAVModManager.exe";
                    switchScriptHookV.Checked = true;
                    switchScriptHookVDN.Checked = false;
                    switchAutoLoadMods.Checked = true;
                    switchAutoLoadScripts.Checked = true;
                    txtMaxLogEntries.Text = "500";
                    switchVerboseLogging.Checked = false;
                    switchEnableMonitor.Checked = true;
                    txtMonitorInterval.Text = "5000";

                    MessageBox.Show("Settings reset to defaults.", "Reset Complete",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    _isLoading = false;
                }
            }
        }

        [System.Runtime.InteropServices.DllImport("kernel32", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section, string key, string defaultValue,
            System.Text.StringBuilder retVal, int size, string filePath);

        [System.Runtime.InteropServices.DllImport("kernel32", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section, string key, string value, string filePath);

        private string ReadString(string section, string key, string defaultValue)
        {
            var sb = new System.Text.StringBuilder(255);
            GetPrivateProfileString(section, key, defaultValue, sb, 255, _configPath);
            return sb.ToString();
        }

        private bool ReadBool(string section, string key, bool defaultValue)
        {
            string value = ReadString(section, key, defaultValue ? "1" : "0");
            return value == "1";
        }

        private int ReadInt(string section, string key, int defaultValue)
        {
            string value = ReadString(section, key, defaultValue.ToString());
            return int.TryParse(value, out int result) ? result : defaultValue;
        }

        private void WriteValue(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, _configPath);
        }
    }
}