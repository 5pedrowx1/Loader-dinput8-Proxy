using System.Runtime.InteropServices;
using System.Text;
using GTAVModManager.Services;

namespace GTAVModManager.UserControls
{
    public partial class SettingsControl : UserControl
    {
        private readonly ModLoaderClient _client;
        private readonly string _configPath;
        private bool _isLoading;
        private bool _hasUnsavedChanges;

        public bool HasUnsavedChanges => _hasUnsavedChanges;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(
            string section, string key, string defaultValue,
            StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(
            string section, string key, string value, string filePath);

        public SettingsControl()
        {
            InitializeComponent();
            _client = new ModLoaderClient();

            _configPath = Path.Combine(
                Path.GetDirectoryName(Application.ExecutablePath) ?? string.Empty,
                "dinput8_config.ini");

            Load += async (s, e) => await ConnectAndLoadAsync();
        }

        private async Task ConnectAndLoadAsync()
        {
            try
            {
                await _client.ConnectAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to connect to Mod Loader:\n\n{ex.Message}",
                    "Connection Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            LoadSettings();
        }

        private void OnSettingChanged(object? sender, EventArgs e)
        {
            if (_isLoading) return;

            _hasUnsavedChanges = true;
            UpdateSaveButtonState();
        }

        private void UpdateSaveButtonState()
        {
            btnSave.FillColor = _hasUnsavedChanges
                ? Color.FromArgb(255, 204, 0)
                : Color.FromArgb(0, 255, 135);

            btnSave.Text = _hasUnsavedChanges ? "Save *" : "Save";
        }

        private void LoadSettings()
        {
            _isLoading = true;

            try
            {
                if (!File.Exists(_configPath))
                {
                    MessageBox.Show(
                        $"Configuration file not found!\n\n" +
                        $"Expected: {_configPath}\n\n" +
                        $"Creating default configuration...",
                        "Config Missing",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    ResetToDefaults(null, EventArgs.Empty);
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

                _hasUnsavedChanges = false;
                UpdateSaveButtonState();
            }
            finally
            {
                _isLoading = false;
            }
        }

        private async void SaveSettings(object? sender, EventArgs e)
        {
            if (_isLoading || !_hasUnsavedChanges) return;
            if (!ValidateSettings()) return;

            btnSave.Enabled = false;
            btnSave.Text = "Saving...";

            try
            {
                SaveSettingsSync();
                await Task.Delay(300);

                MessageBox.Show(
                    "Settings saved successfully!\n\n" +
                    "⚠ Some settings require restarting GTA V to take effect.\n\n" +
                    $"Config file: {Path.GetFileName(_configPath)}",
                    "Settings Saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error saving settings:\n\n{ex.Message}\n\n" +
                    $"Check write permissions for:\n{_configPath}",
                    "Save Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
                btnSave.Text = "Save";
                UpdateSaveButtonState();
            }
        }

        public void SaveSettingsSync()
        {
            if (!ValidateSettings()) return;

            int maxLogs = int.Parse(txtMaxLogEntries.Text);
            int interval = int.Parse(txtMonitorInterval.Text);

            WriteValue("UI", "Enable", switchUIEnable.Checked ? "1" : "0");
            WriteValue("UI", "AutoLaunch", switchAutoLaunch.Checked ? "1" : "0");
            WriteValue("UI", "ExecutableName", txtExecutable.Text.Trim());
            WriteValue("ScriptHook", "LoadScriptHookV", switchScriptHookV.Checked ? "1" : "0");
            WriteValue("ScriptHook", "LoadScriptHookVDotNet", switchScriptHookVDN.Checked ? "1" : "0");
            WriteValue("AutoLoad", "AutoLoadModsFolder", switchAutoLoadMods.Checked ? "1" : "0");
            WriteValue("AutoLoad", "AutoLoadScriptsFolder", switchAutoLoadScripts.Checked ? "1" : "0");
            WriteValue("Logging", "MaxLogEntries", maxLogs.ToString());
            WriteValue("Logging", "VerboseLogging", switchVerboseLogging.Checked ? "1" : "0");
            WriteValue("Performance", "EnableMonitor", switchEnableMonitor.Checked ? "1" : "0");
            WriteValue("Performance", "MonitorIntervalMS", interval.ToString());

            _hasUnsavedChanges = false;
            UpdateSaveButtonState();
        }

        private bool ValidateSettings()
        {
            if (!int.TryParse(txtMaxLogEntries.Text, out int maxLogs) || maxLogs < 10 || maxLogs > 10000)
            {
                MessageBox.Show("Max Log Entries must be between 10 and 10000.", "Invalid Input",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaxLogEntries.Focus();
                return false;
            }

            if (!int.TryParse(txtMonitorInterval.Text, out int interval) || interval < 100 || interval > 60000)
            {
                MessageBox.Show("Monitor Interval must be between 100 and 60000 ms.", "Invalid Input",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonitorInterval.Focus();
                return false;
            }

            string exe = txtExecutable.Text.Trim();
            if (string.IsNullOrWhiteSpace(exe))
            {
                MessageBox.Show("Executable name cannot be empty.", "Invalid Input",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!exe.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
            {
                if (MessageBox.Show(
                    "The executable name does not end with '.exe'.\nAre you sure?",
                    "Confirm Executable",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.No)
                {
                    txtExecutable.Focus();
                    return false;
                }
            }

            return true;
        }

        private void ResetToDefaults(object? sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Reset all settings to default values?\n\nThis cannot be undone.",
                "Confirm Reset",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
                return;

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

                _hasUnsavedChanges = true;
                UpdateSaveButtonState();

                MessageBox.Show("Settings reset to defaults.\nClick Save to apply.",
                    "Reset Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                _isLoading = false;
            }
        }

        private string ReadString(string section, string key, string defaultValue)
        {
            var sb = new StringBuilder(255);
            GetPrivateProfileString(section, key, defaultValue, sb, 255, _configPath);
            return sb.ToString();
        }

        private bool ReadBool(string section, string key, bool defaultValue)
        {
            string value = ReadString(section, key, defaultValue ? "1" : "0");
            return value == "1" || value.Equals("true", StringComparison.OrdinalIgnoreCase);
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