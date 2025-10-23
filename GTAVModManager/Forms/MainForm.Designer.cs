using System;
using System.Drawing;
using System.Windows.Forms;

namespace GTAVModManager.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Elipse = new Guna.UI2.WinForms.Guna2Elipse(components);
            ShadowForm = new Guna.UI2.WinForms.Guna2ShadowForm(components);
            DragForm = new Guna.UI2.WinForms.Guna2DragControl(components);
            PanelButtons = new Guna.UI2.WinForms.Guna2Panel();
            BtnSettings = new Guna.UI2.WinForms.Guna2Button();
            BtnLogs = new Guna.UI2.WinForms.Guna2Button();
            BtnPerformance = new Guna.UI2.WinForms.Guna2Button();
            BtnMods = new Guna.UI2.WinForms.Guna2Button();
            BtnDashboard = new Guna.UI2.WinForms.Guna2Button();
            lblModManager = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblVersion = new Guna.UI2.WinForms.Guna2HtmlLabel();
            PanelTabs = new Guna.UI2.WinForms.Guna2Panel();
            BtnMinimize = new Guna.UI2.WinForms.Guna2Button();
            BtnClose = new Guna.UI2.WinForms.Guna2Button();
            PanelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // Elipse
            // 
            Elipse.BorderRadius = 20;
            Elipse.TargetControl = this;
            // 
            // ShadowForm
            // 
            ShadowForm.BorderRadius = 20;
            ShadowForm.ShadowColor = Color.FromArgb(0, 255, 135);
            ShadowForm.TargetForm = this;
            // 
            // DragForm
            // 
            DragForm.DockIndicatorTransparencyValue = 0.6D;
            DragForm.TargetControl = this;
            DragForm.UseTransparentDrag = true;
            // 
            // PanelButtons
            // 
            PanelButtons.BackColor = Color.FromArgb(22, 22, 22);
            PanelButtons.BorderColor = Color.FromArgb(40, 40, 40);
            PanelButtons.BorderRadius = 12;
            PanelButtons.BorderThickness = 1;
            PanelButtons.Controls.Add(BtnSettings);
            PanelButtons.Controls.Add(BtnLogs);
            PanelButtons.Controls.Add(BtnPerformance);
            PanelButtons.Controls.Add(BtnMods);
            PanelButtons.Controls.Add(BtnDashboard);
            PanelButtons.CustomizableEdges = customizableEdges17;
            PanelButtons.Location = new Point(22, 70);
            PanelButtons.Name = "PanelButtons";
            PanelButtons.ShadowDecoration.CustomizableEdges = customizableEdges18;
            PanelButtons.Size = new Size(170, 280);
            PanelButtons.TabIndex = 0;
            // 
            // BtnSettings
            // 
            BtnSettings.BorderRadius = 6;
            BtnSettings.CustomizableEdges = customizableEdges7;
            BtnSettings.FillColor = Color.FromArgb(35, 35, 35);
            BtnSettings.Font = new Font("Segoe UI Semibold", 9.75F);
            BtnSettings.ForeColor = Color.White;
            BtnSettings.HoverState.FillColor = Color.FromArgb(0, 255, 135);
            BtnSettings.HoverState.ForeColor = Color.Black;
            BtnSettings.Location = new Point(5, 224);
            BtnSettings.Name = "BtnSettings";
            BtnSettings.ShadowDecoration.CustomizableEdges = customizableEdges8;
            BtnSettings.Size = new Size(160, 45);
            BtnSettings.TabIndex = 4;
            BtnSettings.Text = "⚙ Settings";
            BtnSettings.Click += BtnSettings_Click;
            // 
            // BtnLogs
            // 
            BtnLogs.BorderRadius = 6;
            BtnLogs.CustomizableEdges = customizableEdges9;
            BtnLogs.FillColor = Color.FromArgb(35, 35, 35);
            BtnLogs.Font = new Font("Segoe UI Semibold", 9.75F);
            BtnLogs.ForeColor = Color.White;
            BtnLogs.HoverState.FillColor = Color.FromArgb(0, 255, 135);
            BtnLogs.HoverState.ForeColor = Color.Black;
            BtnLogs.Location = new Point(5, 172);
            BtnLogs.Name = "BtnLogs";
            BtnLogs.ShadowDecoration.CustomizableEdges = customizableEdges10;
            BtnLogs.Size = new Size(160, 45);
            BtnLogs.TabIndex = 3;
            BtnLogs.Text = "\U0001f9fe Logs";
            BtnLogs.Click += BtnLogs_Click;
            // 
            // BtnPerformance
            // 
            BtnPerformance.BorderRadius = 6;
            BtnPerformance.CustomizableEdges = customizableEdges11;
            BtnPerformance.FillColor = Color.FromArgb(35, 35, 35);
            BtnPerformance.Font = new Font("Segoe UI Semibold", 9.75F);
            BtnPerformance.ForeColor = Color.White;
            BtnPerformance.HoverState.FillColor = Color.FromArgb(0, 255, 135);
            BtnPerformance.HoverState.ForeColor = Color.Black;
            BtnPerformance.Location = new Point(5, 120);
            BtnPerformance.Name = "BtnPerformance";
            BtnPerformance.ShadowDecoration.CustomizableEdges = customizableEdges12;
            BtnPerformance.Size = new Size(160, 45);
            BtnPerformance.TabIndex = 2;
            BtnPerformance.Text = "📊 Performance";
            BtnPerformance.Click += BtnPerformance_Click;
            // 
            // BtnMods
            // 
            BtnMods.BorderRadius = 6;
            BtnMods.CustomizableEdges = customizableEdges13;
            BtnMods.FillColor = Color.FromArgb(35, 35, 35);
            BtnMods.Font = new Font("Segoe UI", 9F);
            BtnMods.ForeColor = Color.White;
            BtnMods.HoverState.FillColor = Color.FromArgb(0, 255, 135);
            BtnMods.HoverState.ForeColor = Color.Black;
            BtnMods.Location = new Point(5, 68);
            BtnMods.Name = "BtnMods";
            BtnMods.ShadowDecoration.CustomizableEdges = customizableEdges14;
            BtnMods.Size = new Size(160, 45);
            BtnMods.TabIndex = 1;
            BtnMods.Text = "\U0001f9e9 Mods";
            BtnMods.Click += BtnMods_Click;
            // 
            // BtnDashboard
            // 
            BtnDashboard.BorderRadius = 6;
            BtnDashboard.CustomizableEdges = customizableEdges15;
            BtnDashboard.FillColor = Color.FromArgb(35, 35, 35);
            BtnDashboard.Font = new Font("Segoe UI Semibold", 9.75F);
            BtnDashboard.ForeColor = Color.White;
            BtnDashboard.HoverState.FillColor = Color.FromArgb(0, 255, 135);
            BtnDashboard.HoverState.ForeColor = Color.Black;
            BtnDashboard.Location = new Point(5, 16);
            BtnDashboard.Name = "BtnDashboard";
            BtnDashboard.ShadowDecoration.CustomizableEdges = customizableEdges16;
            BtnDashboard.Size = new Size(160, 45);
            BtnDashboard.TabIndex = 0;
            BtnDashboard.Text = "📋 Dashboard";
            BtnDashboard.Click += BtnDashboard_Click;
            // 
            // lblModManager
            // 
            lblModManager.BackColor = Color.Transparent;
            lblModManager.Font = new Font("Segoe UI Black", 15F, FontStyle.Bold);
            lblModManager.ForeColor = Color.FromArgb(0, 255, 135);
            lblModManager.Location = new Point(22, 17);
            lblModManager.Name = "lblModManager";
            lblModManager.Size = new Size(234, 30);
            lblModManager.TabIndex = 1;
            lblModManager.Text = "GTA V MOD MANAGER";
            // 
            // lblVersion
            // 
            lblVersion.BackColor = Color.Transparent;
            lblVersion.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblVersion.ForeColor = Color.FromArgb(120, 120, 120);
            lblVersion.Location = new Point(84, 421);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(37, 17);
            lblVersion.TabIndex = 2;
            lblVersion.Text = "v2.1.0";
            // 
            // PanelTabs
            // 
            PanelTabs.BackColor = Color.FromArgb(15, 15, 15);
            PanelTabs.BorderColor = Color.FromArgb(40, 40, 40);
            PanelTabs.BorderRadius = 12;
            PanelTabs.BorderThickness = 1;
            PanelTabs.CustomizableEdges = customizableEdges5;
            PanelTabs.Location = new Point(210, 70);
            PanelTabs.Name = "PanelTabs";
            PanelTabs.ShadowDecoration.CustomizableEdges = customizableEdges6;
            PanelTabs.Size = new Size(578, 368);
            PanelTabs.TabIndex = 3;
            // 
            // BtnMinimize
            // 
            BtnMinimize.BorderRadius = 5;
            BtnMinimize.CustomizableEdges = customizableEdges1;
            BtnMinimize.FillColor = Color.FromArgb(35, 35, 35);
            BtnMinimize.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            BtnMinimize.ForeColor = Color.White;
            BtnMinimize.HoverState.FillColor = Color.FromArgb(0, 255, 135);
            BtnMinimize.HoverState.ForeColor = Color.Black;
            BtnMinimize.Location = new Point(720, 15);
            BtnMinimize.Name = "BtnMinimize";
            BtnMinimize.ShadowDecoration.CustomizableEdges = customizableEdges2;
            BtnMinimize.Size = new Size(35, 28);
            BtnMinimize.TabIndex = 0;
            BtnMinimize.Text = "–";
            BtnMinimize.Click += BtnMinimize_Click;
            // 
            // BtnClose
            // 
            BtnClose.BorderRadius = 5;
            BtnClose.CustomizableEdges = customizableEdges3;
            BtnClose.FillColor = Color.FromArgb(35, 35, 35);
            BtnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            BtnClose.ForeColor = Color.White;
            BtnClose.HoverState.FillColor = Color.FromArgb(255, 77, 77);
            BtnClose.HoverState.ForeColor = Color.Black;
            BtnClose.Location = new Point(760, 15);
            BtnClose.Name = "BtnClose";
            BtnClose.ShadowDecoration.CustomizableEdges = customizableEdges4;
            BtnClose.Size = new Size(35, 28);
            BtnClose.TabIndex = 1;
            BtnClose.Text = "X";
            BtnClose.Click += BtnClose_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 18, 18);
            ClientSize = new Size(800, 450);
            Controls.Add(BtnMinimize);
            Controls.Add(BtnClose);
            Controls.Add(lblVersion);
            Controls.Add(lblModManager);
            Controls.Add(PanelTabs);
            Controls.Add(PanelButtons);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MainForm";
            Text = "GTAV Mod Manager";
            PanelButtons.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse Elipse;
        private Guna.UI2.WinForms.Guna2ShadowForm ShadowForm;
        private Guna.UI2.WinForms.Guna2DragControl DragForm;
        private Guna.UI2.WinForms.Guna2Panel PanelButtons;
        private Guna.UI2.WinForms.Guna2Panel PanelTabs;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblModManager;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblVersion;
        private Guna.UI2.WinForms.Guna2Button BtnDashboard;
        private Guna.UI2.WinForms.Guna2Button BtnMods;
        private Guna.UI2.WinForms.Guna2Button BtnPerformance;
        private Guna.UI2.WinForms.Guna2Button BtnLogs;
        private Guna.UI2.WinForms.Guna2Button BtnSettings;
        private Guna.UI2.WinForms.Guna2Button BtnClose;
        private Guna.UI2.WinForms.Guna2Button BtnMinimize;
    }
}