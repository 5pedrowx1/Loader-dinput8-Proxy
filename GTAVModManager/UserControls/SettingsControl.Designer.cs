using System.Drawing;
using System.Windows.Forms;

namespace GTAVModManager.UserControls
{
    partial class SettingsControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges edges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges edges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lblSettings = new Guna.UI2.WinForms.Guna2HtmlLabel();
            panelSettings = new Guna.UI2.WinForms.Guna2Panel();
            lblGamePath = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtGamePath = new Guna.UI2.WinForms.Guna2TextBox();
            btnBrowseGame = new Guna.UI2.WinForms.Guna2Button();
            lblModsPath = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtModsPath = new Guna.UI2.WinForms.Guna2TextBox();
            btnBrowseMods = new Guna.UI2.WinForms.Guna2Button();
            lblStartup = new Guna.UI2.WinForms.Guna2HtmlLabel();
            switchStartup = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            lblLogs = new Guna.UI2.WinForms.Guna2HtmlLabel();
            switchLogs = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            lblLanguage = new Guna.UI2.WinForms.Guna2HtmlLabel();
            comboLanguage = new Guna.UI2.WinForms.Guna2ComboBox();
            btnSave = new Guna.UI2.WinForms.Guna2Button();
            btnReset = new Guna.UI2.WinForms.Guna2Button();
            SuspendLayout();
            //
            // lblSettings
            //
            lblSettings.BackColor = Color.Transparent;
            lblSettings.Font = new Font("Segoe UI Black", 16F, FontStyle.Bold);
            lblSettings.ForeColor = Color.FromArgb(0, 255, 135);
            lblSettings.Location = new Point(15, 15);
            lblSettings.Name = "lblSettings";
            lblSettings.Text = "SETTINGS";
            //
            // panelSettings
            //
            panelSettings.BackColor = Color.FromArgb(25, 25, 25);
            panelSettings.BorderColor = Color.FromArgb(40, 40, 40);
            panelSettings.BorderRadius = 12;
            panelSettings.BorderThickness = 1;
            panelSettings.CustomizableEdges = edges1;
            panelSettings.ShadowDecoration.CustomizableEdges = edges2;
            panelSettings.Location = new Point(15, 55);
            panelSettings.Name = "panelSettings";
            panelSettings.Size = new Size(548, 250);
            //
            // lblGamePath
            //
            lblGamePath.BackColor = Color.Transparent;
            lblGamePath.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblGamePath.ForeColor = Color.FromArgb(156, 156, 156);
            lblGamePath.Location = new Point(20, 25);
            lblGamePath.Text = "GTA V Directory:";
            //
            // txtGamePath
            //
            txtGamePath.BorderColor = Color.FromArgb(42, 42, 42);
            txtGamePath.BorderRadius = 5;
            txtGamePath.FillColor = Color.FromArgb(24, 24, 24);
            txtGamePath.Font = new Font("Segoe UI", 9F);
            txtGamePath.ForeColor = Color.FromArgb(234, 234, 234);
            txtGamePath.Location = new Point(130, 22);
            txtGamePath.Size = new Size(340, 27);
            //
            // btnBrowseGame
            //
            btnBrowseGame.BorderRadius = 5;
            btnBrowseGame.FillColor = Color.FromArgb(0, 255, 135);
            btnBrowseGame.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnBrowseGame.ForeColor = Color.Black;
            btnBrowseGame.Location = new Point(480, 22);
            btnBrowseGame.Size = new Size(45, 27);
            btnBrowseGame.Text = "...";
            //
            // lblModsPath
            //
            lblModsPath.BackColor = Color.Transparent;
            lblModsPath.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblModsPath.ForeColor = Color.FromArgb(156, 156, 156);
            lblModsPath.Location = new Point(20, 65);
            lblModsPath.Text = "Mods Folder:";
            //
            // txtModsPath
            //
            txtModsPath.BorderColor = Color.FromArgb(42, 42, 42);
            txtModsPath.BorderRadius = 5;
            txtModsPath.FillColor = Color.FromArgb(24, 24, 24);
            txtModsPath.Font = new Font("Segoe UI", 9F);
            txtModsPath.ForeColor = Color.FromArgb(234, 234, 234);
            txtModsPath.Location = new Point(130, 62);
            txtModsPath.Size = new Size(340, 27);
            //
            // btnBrowseMods
            //
            btnBrowseMods.BorderRadius = 5;
            btnBrowseMods.FillColor = Color.FromArgb(0, 255, 135);
            btnBrowseMods.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnBrowseMods.ForeColor = Color.Black;
            btnBrowseMods.Location = new Point(480, 62);
            btnBrowseMods.Size = new Size(45, 27);
            btnBrowseMods.Text = "...";
            //
            // lblStartup
            //
            lblStartup.BackColor = Color.Transparent;
            lblStartup.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStartup.ForeColor = Color.FromArgb(156, 156, 156);
            lblStartup.Location = new Point(20, 105);
            lblStartup.Text = "Launch on Boot:";
            //
            // switchStartup
            //
            switchStartup.CheckedState.FillColor = Color.FromArgb(0, 255, 135);
            switchStartup.Location = new Point(130, 103);
            switchStartup.Size = new Size(40, 20);
            //
            // lblLogs
            //
            lblLogs.BackColor = Color.Transparent;
            lblLogs.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLogs.ForeColor = Color.FromArgb(156, 156, 156);
            lblLogs.Location = new Point(20, 140);
            lblLogs.Text = "Enable Full Logs:";
            //
            // switchLogs
            //
            switchLogs.CheckedState.FillColor = Color.FromArgb(0, 255, 135);
            switchLogs.Location = new Point(130, 138);
            switchLogs.Size = new Size(40, 20);
            //
            // lblLanguage
            //
            lblLanguage.BackColor = Color.Transparent;
            lblLanguage.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLanguage.ForeColor = Color.FromArgb(156, 156, 156);
            lblLanguage.Location = new Point(20, 180);
            lblLanguage.Text = "Language:";
            //
            // comboLanguage
            //
            comboLanguage.BackColor = Color.Transparent;
            comboLanguage.BorderColor = Color.FromArgb(42, 42, 42);
            comboLanguage.BorderRadius = 5;
            comboLanguage.DrawMode = DrawMode.OwnerDrawFixed;
            comboLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            comboLanguage.FillColor = Color.FromArgb(24, 24, 24);
            comboLanguage.FocusedColor = Color.FromArgb(0, 255, 135);
            comboLanguage.Font = new Font("Segoe UI", 9F);
            comboLanguage.ForeColor = Color.FromArgb(234, 234, 234);
            comboLanguage.ItemHeight = 24;
            comboLanguage.Items.AddRange(new object[] { "English", "Português", "Español" });
            comboLanguage.Location = new Point(130, 175);
            comboLanguage.Size = new Size(160, 30);
            //
            // btnSave
            //
            btnSave.BorderRadius = 5;
            btnSave.FillColor = Color.FromArgb(0, 255, 135);
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSave.ForeColor = Color.Black;
            btnSave.Location = new Point(400, 315);
            btnSave.Size = new Size(75, 28);
            btnSave.Text = "Save";
            //
            // btnReset
            //
            btnReset.BorderRadius = 5;
            btnReset.FillColor = Color.FromArgb(255, 77, 77);
            btnReset.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnReset.ForeColor = Color.Black;
            btnReset.Location = new Point(485, 315);
            btnReset.Size = new Size(75, 28);
            btnReset.Text = "Defaults";
            panelSettings.Controls.Add(lblGamePath);
            panelSettings.Controls.Add(txtGamePath);
            panelSettings.Controls.Add(btnBrowseGame);
            panelSettings.Controls.Add(lblModsPath);
            panelSettings.Controls.Add(txtModsPath);
            panelSettings.Controls.Add(btnBrowseMods);
            panelSettings.Controls.Add(lblStartup);
            panelSettings.Controls.Add(switchStartup);
            panelSettings.Controls.Add(lblLogs);
            panelSettings.Controls.Add(switchLogs);
            panelSettings.Controls.Add(lblLanguage);
            panelSettings.Controls.Add(comboLanguage);
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
            Size = new Size(578, 353);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel lblSettings;
        private Guna.UI2.WinForms.Guna2Panel panelSettings;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblGamePath;
        private Guna.UI2.WinForms.Guna2TextBox txtGamePath;
        private Guna.UI2.WinForms.Guna2Button btnBrowseGame;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblModsPath;
        private Guna.UI2.WinForms.Guna2TextBox txtModsPath;
        private Guna.UI2.WinForms.Guna2Button btnBrowseMods;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblStartup;
        private Guna.UI2.WinForms.Guna2ToggleSwitch switchStartup;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblLogs;
        private Guna.UI2.WinForms.Guna2ToggleSwitch switchLogs;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblLanguage;
        private Guna.UI2.WinForms.Guna2ComboBox comboLanguage;
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private Guna.UI2.WinForms.Guna2Button btnReset;
    }
}