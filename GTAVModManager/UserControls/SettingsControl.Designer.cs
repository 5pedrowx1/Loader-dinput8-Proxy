using System.Drawing;
using System.Windows.Forms;

namespace GTAVModManager.UserControls
{
    partial class SettingsControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _client?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges23 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges24 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges21 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges22 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges25 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges26 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges27 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges28 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lblSettings = new Guna.UI2.WinForms.Guna2HtmlLabel();
            panelSettings = new Guna.UI2.WinForms.Guna2Panel();
            lblUIEnable = new Guna.UI2.WinForms.Guna2HtmlLabel();
            switchUIEnable = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            lblAutoLaunch = new Guna.UI2.WinForms.Guna2HtmlLabel();
            switchAutoLaunch = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            lblExecutable = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtExecutable = new Guna.UI2.WinForms.Guna2TextBox();
            lblScriptHookV = new Guna.UI2.WinForms.Guna2HtmlLabel();
            switchScriptHookV = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            lblScriptHookVDN = new Guna.UI2.WinForms.Guna2HtmlLabel();
            switchScriptHookVDN = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            lblAutoLoadMods = new Guna.UI2.WinForms.Guna2HtmlLabel();
            switchAutoLoadMods = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            lblAutoLoadScripts = new Guna.UI2.WinForms.Guna2HtmlLabel();
            switchAutoLoadScripts = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            lblMaxLogEntries = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtMaxLogEntries = new Guna.UI2.WinForms.Guna2TextBox();
            lblVerboseLogging = new Guna.UI2.WinForms.Guna2HtmlLabel();
            switchVerboseLogging = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            lblEnableMonitor = new Guna.UI2.WinForms.Guna2HtmlLabel();
            switchEnableMonitor = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            lblMonitorInterval = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtMonitorInterval = new Guna.UI2.WinForms.Guna2TextBox();
            btnSave = new Guna.UI2.WinForms.Guna2Button();
            btnReset = new Guna.UI2.WinForms.Guna2Button();
            panelSettings.SuspendLayout();
            SuspendLayout();
            // 
            // lblSettings
            // 
            lblSettings.BackColor = Color.Transparent;
            lblSettings.Font = new Font("Segoe UI Black", 16F, FontStyle.Bold);
            lblSettings.ForeColor = Color.FromArgb(0, 255, 135);
            lblSettings.Location = new Point(15, 0);
            lblSettings.Name = "lblSettings";
            lblSettings.Size = new Size(104, 32);
            lblSettings.TabIndex = 0;
            lblSettings.Text = "SETTINGS";
            // 
            // panelSettings
            // 
            panelSettings.BackColor = Color.FromArgb(25, 25, 25);
            panelSettings.BorderColor = Color.FromArgb(40, 40, 40);
            panelSettings.BorderRadius = 12;
            panelSettings.BorderThickness = 1;
            panelSettings.Controls.Add(lblUIEnable);
            panelSettings.Controls.Add(switchUIEnable);
            panelSettings.Controls.Add(lblAutoLaunch);
            panelSettings.Controls.Add(switchAutoLaunch);
            panelSettings.Controls.Add(lblExecutable);
            panelSettings.Controls.Add(txtExecutable);
            panelSettings.Controls.Add(lblScriptHookV);
            panelSettings.Controls.Add(switchScriptHookV);
            panelSettings.Controls.Add(lblScriptHookVDN);
            panelSettings.Controls.Add(switchScriptHookVDN);
            panelSettings.Controls.Add(lblAutoLoadMods);
            panelSettings.Controls.Add(switchAutoLoadMods);
            panelSettings.Controls.Add(lblAutoLoadScripts);
            panelSettings.Controls.Add(switchAutoLoadScripts);
            panelSettings.Controls.Add(lblMaxLogEntries);
            panelSettings.Controls.Add(txtMaxLogEntries);
            panelSettings.Controls.Add(lblVerboseLogging);
            panelSettings.Controls.Add(switchVerboseLogging);
            panelSettings.Controls.Add(lblEnableMonitor);
            panelSettings.Controls.Add(switchEnableMonitor);
            panelSettings.Controls.Add(lblMonitorInterval);
            panelSettings.Controls.Add(txtMonitorInterval);
            panelSettings.CustomizableEdges = customizableEdges23;
            panelSettings.Location = new Point(15, 40);
            panelSettings.Name = "panelSettings";
            panelSettings.ShadowDecoration.CustomizableEdges = customizableEdges24;
            panelSettings.Size = new Size(548, 288);
            panelSettings.TabIndex = 1;
            // 
            // lblUIEnable
            // 
            lblUIEnable.BackColor = Color.Transparent;
            lblUIEnable.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblUIEnable.ForeColor = Color.FromArgb(156, 156, 156);
            lblUIEnable.Location = new Point(20, 7);
            lblUIEnable.Name = "lblUIEnable";
            lblUIEnable.Size = new Size(58, 17);
            lblUIEnable.TabIndex = 0;
            lblUIEnable.Text = "Enable UI:";
            // 
            // switchUIEnable
            // 
            switchUIEnable.CheckedState.FillColor = Color.FromArgb(0, 255, 135);
            switchUIEnable.CustomizableEdges = customizableEdges1;
            switchUIEnable.Location = new Point(180, 5);
            switchUIEnable.Name = "switchUIEnable";
            switchUIEnable.ShadowDecoration.CustomizableEdges = customizableEdges2;
            switchUIEnable.Size = new Size(40, 20);
            switchUIEnable.TabIndex = 1;
            switchUIEnable.Click += OnSettingChanged;
            // 
            // lblAutoLaunch
            // 
            lblAutoLaunch.BackColor = Color.Transparent;
            lblAutoLaunch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAutoLaunch.ForeColor = Color.FromArgb(156, 156, 156);
            lblAutoLaunch.Location = new Point(20, 37);
            lblAutoLaunch.Name = "lblAutoLaunch";
            lblAutoLaunch.Size = new Size(75, 17);
            lblAutoLaunch.TabIndex = 2;
            lblAutoLaunch.Text = "Auto Launch:";
            // 
            // switchAutoLaunch
            // 
            switchAutoLaunch.CheckedState.FillColor = Color.FromArgb(0, 255, 135);
            switchAutoLaunch.CustomizableEdges = customizableEdges3;
            switchAutoLaunch.Location = new Point(180, 35);
            switchAutoLaunch.Name = "switchAutoLaunch";
            switchAutoLaunch.ShadowDecoration.CustomizableEdges = customizableEdges4;
            switchAutoLaunch.Size = new Size(40, 20);
            switchAutoLaunch.TabIndex = 3;
            switchAutoLaunch.Click += OnSettingChanged;
            // 
            // lblExecutable
            // 
            lblExecutable.BackColor = Color.Transparent;
            lblExecutable.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblExecutable.ForeColor = Color.FromArgb(156, 156, 156);
            lblExecutable.Location = new Point(20, 67);
            lblExecutable.Name = "lblExecutable";
            lblExecutable.Size = new Size(103, 17);
            lblExecutable.TabIndex = 4;
            lblExecutable.Text = "Executable Name:";
            // 
            // txtExecutable
            // 
            txtExecutable.BorderColor = Color.FromArgb(42, 42, 42);
            txtExecutable.BorderRadius = 5;
            txtExecutable.CustomizableEdges = customizableEdges5;
            txtExecutable.DefaultText = "";
            txtExecutable.FillColor = Color.FromArgb(24, 24, 24);
            txtExecutable.Font = new Font("Segoe UI", 9F);
            txtExecutable.ForeColor = Color.FromArgb(234, 234, 234);
            txtExecutable.Location = new Point(180, 64);
            txtExecutable.Name = "txtExecutable";
            txtExecutable.PlaceholderText = "";
            txtExecutable.SelectedText = "";
            txtExecutable.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtExecutable.Size = new Size(250, 27);
            txtExecutable.TabIndex = 5;
            txtExecutable.Click += OnSettingChanged;
            // 
            // lblScriptHookV
            // 
            lblScriptHookV.BackColor = Color.Transparent;
            lblScriptHookV.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblScriptHookV.ForeColor = Color.FromArgb(156, 156, 156);
            lblScriptHookV.Location = new Point(20, 107);
            lblScriptHookV.Name = "lblScriptHookV";
            lblScriptHookV.Size = new Size(106, 17);
            lblScriptHookV.TabIndex = 6;
            lblScriptHookV.Text = "Load ScriptHookV:";
            // 
            // switchScriptHookV
            // 
            switchScriptHookV.CheckedState.FillColor = Color.FromArgb(0, 255, 135);
            switchScriptHookV.CustomizableEdges = customizableEdges7;
            switchScriptHookV.Location = new Point(180, 105);
            switchScriptHookV.Name = "switchScriptHookV";
            switchScriptHookV.ShadowDecoration.CustomizableEdges = customizableEdges8;
            switchScriptHookV.Size = new Size(40, 20);
            switchScriptHookV.TabIndex = 7;
            switchScriptHookV.Click += OnSettingChanged;
            // 
            // lblScriptHookVDN
            // 
            lblScriptHookVDN.BackColor = Color.Transparent;
            lblScriptHookVDN.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblScriptHookVDN.ForeColor = Color.FromArgb(156, 156, 156);
            lblScriptHookVDN.Location = new Point(20, 137);
            lblScriptHookVDN.Name = "lblScriptHookVDN";
            lblScriptHookVDN.Size = new Size(148, 17);
            lblScriptHookVDN.TabIndex = 8;
            lblScriptHookVDN.Text = "Load ScriptHookVDotNet:";
            // 
            // switchScriptHookVDN
            // 
            switchScriptHookVDN.CheckedState.FillColor = Color.FromArgb(0, 255, 135);
            switchScriptHookVDN.CustomizableEdges = customizableEdges9;
            switchScriptHookVDN.Location = new Point(180, 135);
            switchScriptHookVDN.Name = "switchScriptHookVDN";
            switchScriptHookVDN.ShadowDecoration.CustomizableEdges = customizableEdges10;
            switchScriptHookVDN.Size = new Size(40, 20);
            switchScriptHookVDN.TabIndex = 9;
            switchScriptHookVDN.Click += OnSettingChanged;
            // 
            // lblAutoLoadMods
            // 
            lblAutoLoadMods.BackColor = Color.Transparent;
            lblAutoLoadMods.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAutoLoadMods.ForeColor = Color.FromArgb(156, 156, 156);
            lblAutoLoadMods.Location = new Point(20, 167);
            lblAutoLoadMods.Name = "lblAutoLoadMods";
            lblAutoLoadMods.Size = new Size(133, 17);
            lblAutoLoadMods.TabIndex = 10;
            lblAutoLoadMods.Text = "Auto Load Mods Folder:";
            // 
            // switchAutoLoadMods
            // 
            switchAutoLoadMods.CheckedState.FillColor = Color.FromArgb(0, 255, 135);
            switchAutoLoadMods.CustomizableEdges = customizableEdges11;
            switchAutoLoadMods.Location = new Point(180, 165);
            switchAutoLoadMods.Name = "switchAutoLoadMods";
            switchAutoLoadMods.ShadowDecoration.CustomizableEdges = customizableEdges12;
            switchAutoLoadMods.Size = new Size(40, 20);
            switchAutoLoadMods.TabIndex = 11;
            switchAutoLoadMods.Click += OnSettingChanged;
            // 
            // lblAutoLoadScripts
            // 
            lblAutoLoadScripts.BackColor = Color.Transparent;
            lblAutoLoadScripts.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAutoLoadScripts.ForeColor = Color.FromArgb(156, 156, 156);
            lblAutoLoadScripts.Location = new Point(20, 197);
            lblAutoLoadScripts.Name = "lblAutoLoadScripts";
            lblAutoLoadScripts.Size = new Size(141, 17);
            lblAutoLoadScripts.TabIndex = 12;
            lblAutoLoadScripts.Text = "Auto Load Scripts Folder:";
            // 
            // switchAutoLoadScripts
            // 
            switchAutoLoadScripts.CheckedState.FillColor = Color.FromArgb(0, 255, 135);
            switchAutoLoadScripts.CustomizableEdges = customizableEdges13;
            switchAutoLoadScripts.Location = new Point(180, 195);
            switchAutoLoadScripts.Name = "switchAutoLoadScripts";
            switchAutoLoadScripts.ShadowDecoration.CustomizableEdges = customizableEdges14;
            switchAutoLoadScripts.Size = new Size(40, 20);
            switchAutoLoadScripts.TabIndex = 13;
            switchAutoLoadScripts.Click += OnSettingChanged;
            // 
            // lblMaxLogEntries
            // 
            lblMaxLogEntries.BackColor = Color.Transparent;
            lblMaxLogEntries.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMaxLogEntries.ForeColor = Color.FromArgb(156, 156, 156);
            lblMaxLogEntries.Location = new Point(20, 227);
            lblMaxLogEntries.Name = "lblMaxLogEntries";
            lblMaxLogEntries.Size = new Size(94, 17);
            lblMaxLogEntries.TabIndex = 14;
            lblMaxLogEntries.Text = "Max Log Entries:";
            // 
            // txtMaxLogEntries
            // 
            txtMaxLogEntries.BorderColor = Color.FromArgb(42, 42, 42);
            txtMaxLogEntries.BorderRadius = 5;
            txtMaxLogEntries.CustomizableEdges = customizableEdges15;
            txtMaxLogEntries.DefaultText = "";
            txtMaxLogEntries.FillColor = Color.FromArgb(24, 24, 24);
            txtMaxLogEntries.Font = new Font("Segoe UI", 9F);
            txtMaxLogEntries.ForeColor = Color.FromArgb(234, 234, 234);
            txtMaxLogEntries.Location = new Point(180, 224);
            txtMaxLogEntries.Name = "txtMaxLogEntries";
            txtMaxLogEntries.PlaceholderText = "";
            txtMaxLogEntries.SelectedText = "";
            txtMaxLogEntries.ShadowDecoration.CustomizableEdges = customizableEdges16;
            txtMaxLogEntries.Size = new Size(80, 27);
            txtMaxLogEntries.TabIndex = 15;
            txtMaxLogEntries.Click += OnSettingChanged;
            // 
            // lblVerboseLogging
            // 
            lblVerboseLogging.BackColor = Color.Transparent;
            lblVerboseLogging.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblVerboseLogging.ForeColor = Color.FromArgb(156, 156, 156);
            lblVerboseLogging.Location = new Point(280, 227);
            lblVerboseLogging.Name = "lblVerboseLogging";
            lblVerboseLogging.Size = new Size(52, 17);
            lblVerboseLogging.TabIndex = 16;
            lblVerboseLogging.Text = "Verbose:";
            // 
            // switchVerboseLogging
            // 
            switchVerboseLogging.CheckedState.FillColor = Color.FromArgb(0, 255, 135);
            switchVerboseLogging.CustomizableEdges = customizableEdges17;
            switchVerboseLogging.Location = new Point(340, 225);
            switchVerboseLogging.Name = "switchVerboseLogging";
            switchVerboseLogging.ShadowDecoration.CustomizableEdges = customizableEdges18;
            switchVerboseLogging.Size = new Size(40, 20);
            switchVerboseLogging.TabIndex = 17;
            switchVerboseLogging.Click += OnSettingChanged;
            // 
            // lblEnableMonitor
            // 
            lblEnableMonitor.BackColor = Color.Transparent;
            lblEnableMonitor.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEnableMonitor.ForeColor = Color.FromArgb(156, 156, 156);
            lblEnableMonitor.Location = new Point(20, 257);
            lblEnableMonitor.Name = "lblEnableMonitor";
            lblEnableMonitor.Size = new Size(166, 17);
            lblEnableMonitor.TabIndex = 18;
            lblEnableMonitor.Text = "Enable Performance Monitor:";
            // 
            // switchEnableMonitor
            // 
            switchEnableMonitor.CheckedState.FillColor = Color.FromArgb(0, 255, 135);
            switchEnableMonitor.CustomizableEdges = customizableEdges19;
            switchEnableMonitor.Location = new Point(220, 255);
            switchEnableMonitor.Name = "switchEnableMonitor";
            switchEnableMonitor.ShadowDecoration.CustomizableEdges = customizableEdges20;
            switchEnableMonitor.Size = new Size(40, 20);
            switchEnableMonitor.TabIndex = 19;
            switchEnableMonitor.Click += OnSettingChanged;
            // 
            // lblMonitorInterval
            // 
            lblMonitorInterval.BackColor = Color.Transparent;
            lblMonitorInterval.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMonitorInterval.ForeColor = Color.FromArgb(156, 156, 156);
            lblMonitorInterval.Location = new Point(280, 257);
            lblMonitorInterval.Name = "lblMonitorInterval";
            lblMonitorInterval.Size = new Size(77, 17);
            lblMonitorInterval.TabIndex = 20;
            lblMonitorInterval.Text = "Interval (ms):";
            // 
            // txtMonitorInterval
            // 
            txtMonitorInterval.BorderColor = Color.FromArgb(42, 42, 42);
            txtMonitorInterval.BorderRadius = 5;
            txtMonitorInterval.CustomizableEdges = customizableEdges21;
            txtMonitorInterval.DefaultText = "";
            txtMonitorInterval.FillColor = Color.FromArgb(24, 24, 24);
            txtMonitorInterval.Font = new Font("Segoe UI", 9F);
            txtMonitorInterval.ForeColor = Color.FromArgb(234, 234, 234);
            txtMonitorInterval.Location = new Point(370, 254);
            txtMonitorInterval.Name = "txtMonitorInterval";
            txtMonitorInterval.PlaceholderText = "";
            txtMonitorInterval.SelectedText = "";
            txtMonitorInterval.ShadowDecoration.CustomizableEdges = customizableEdges22;
            txtMonitorInterval.Size = new Size(80, 27);
            txtMonitorInterval.TabIndex = 21;
            txtMonitorInterval.Click += OnSettingChanged;
            // 
            // btnSave
            // 
            btnSave.BorderRadius = 5;
            btnSave.CustomizableEdges = customizableEdges25;
            btnSave.FillColor = Color.FromArgb(0, 255, 135);
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSave.ForeColor = Color.Black;
            btnSave.Location = new Point(403, 334);
            btnSave.Name = "btnSave";
            btnSave.ShadowDecoration.CustomizableEdges = customizableEdges26;
            btnSave.Size = new Size(75, 28);
            btnSave.TabIndex = 2;
            btnSave.Text = "Save";
            btnSave.Click += SaveSettings;
            // 
            // btnReset
            // 
            btnReset.BorderRadius = 5;
            btnReset.CustomizableEdges = customizableEdges27;
            btnReset.FillColor = Color.FromArgb(255, 77, 77);
            btnReset.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnReset.ForeColor = Color.Black;
            btnReset.Location = new Point(488, 334);
            btnReset.Name = "btnReset";
            btnReset.ShadowDecoration.CustomizableEdges = customizableEdges28;
            btnReset.Size = new Size(75, 28);
            btnReset.TabIndex = 3;
            btnReset.Text = "Defaults";
            btnReset.Click += ResetToDefaults;
            // 
            // SettingsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 18, 18);
            Controls.Add(lblSettings);
            Controls.Add(panelSettings);
            Controls.Add(btnSave);
            Controls.Add(btnReset);
            Name = "SettingsControl";
            Size = new Size(578, 394);
            panelSettings.ResumeLayout(false);
            panelSettings.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel lblSettings;
        private Guna.UI2.WinForms.Guna2Panel panelSettings;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblUIEnable;
        private Guna.UI2.WinForms.Guna2ToggleSwitch switchUIEnable;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblAutoLaunch;
        private Guna.UI2.WinForms.Guna2ToggleSwitch switchAutoLaunch;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblExecutable;
        private Guna.UI2.WinForms.Guna2TextBox txtExecutable;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblScriptHookV;
        private Guna.UI2.WinForms.Guna2ToggleSwitch switchScriptHookV;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblScriptHookVDN;
        private Guna.UI2.WinForms.Guna2ToggleSwitch switchScriptHookVDN;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblAutoLoadMods;
        private Guna.UI2.WinForms.Guna2ToggleSwitch switchAutoLoadMods;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblAutoLoadScripts;
        private Guna.UI2.WinForms.Guna2ToggleSwitch switchAutoLoadScripts;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblMaxLogEntries;
        private Guna.UI2.WinForms.Guna2TextBox txtMaxLogEntries;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblVerboseLogging;
        private Guna.UI2.WinForms.Guna2ToggleSwitch switchVerboseLogging;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblEnableMonitor;
        private Guna.UI2.WinForms.Guna2ToggleSwitch switchEnableMonitor;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblMonitorInterval;
        private Guna.UI2.WinForms.Guna2TextBox txtMonitorInterval;
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private Guna.UI2.WinForms.Guna2Button btnReset;
    }
}
