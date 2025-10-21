namespace GTAVModManager.Forms
{
    partial class GTAVModManager
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges29 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges30 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges21 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges22 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges23 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges24 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges25 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges26 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges27 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges28 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges31 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges32 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DragControl = new Guna.UI2.WinForms.Guna2DragControl(components);
            PanelTop = new Guna.UI2.WinForms.Guna2Panel();
            BtnClose = new Guna.UI2.WinForms.Guna2Button();
            LabelTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            ShadowForm = new Guna.UI2.WinForms.Guna2ShadowForm(components);
            Elipse = new Guna.UI2.WinForms.Guna2Elipse(components);
            SidebarPanel = new Guna.UI2.WinForms.Guna2Panel();
            BtnConfig = new Guna.UI2.WinForms.Guna2Button();
            BtnLogs = new Guna.UI2.WinForms.Guna2Button();
            BtnStatus = new Guna.UI2.WinForms.Guna2Button();
            BtnMods = new Guna.UI2.WinForms.Guna2Button();
            MainPanel = new Guna.UI2.WinForms.Guna2Panel();
            refreshTimer = new System.Windows.Forms.Timer(components);
            PanelTop.SuspendLayout();
            SidebarPanel.SuspendLayout();
            SuspendLayout();
            // 
            // DragControl
            // 
            DragControl.DockIndicatorColor = Color.Green;
            DragControl.DockIndicatorTransparencyValue = 0.6D;
            DragControl.TargetControl = PanelTop;
            DragControl.UseTransparentDrag = true;
            // 
            // PanelTop
            // 
            PanelTop.BackColor = Color.DarkGreen;
            PanelTop.Controls.Add(BtnClose);
            PanelTop.Controls.Add(LabelTitle);
            PanelTop.CustomizableEdges = customizableEdges19;
            PanelTop.Dock = DockStyle.Top;
            PanelTop.Location = new Point(0, 0);
            PanelTop.Name = "PanelTop";
            PanelTop.ShadowDecoration.CustomizableEdges = customizableEdges20;
            PanelTop.Size = new Size(1200, 52);
            PanelTop.TabIndex = 0;
            // 
            // BtnClose
            // 
            BtnClose.BorderColor = Color.Lime;
            BtnClose.BorderRadius = 5;
            BtnClose.BorderThickness = 1;
            BtnClose.CustomizableEdges = customizableEdges17;
            BtnClose.DisabledState.BorderColor = Color.DarkGray;
            BtnClose.DisabledState.CustomBorderColor = Color.DarkGray;
            BtnClose.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            BtnClose.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            BtnClose.FillColor = Color.Transparent;
            BtnClose.Font = new Font("Segoe UI", 9F);
            BtnClose.ForeColor = Color.White;
            BtnClose.HoverState.FillColor = Color.Green;
            BtnClose.Location = new Point(1148, 12);
            BtnClose.Name = "BtnClose";
            BtnClose.ShadowDecoration.CustomizableEdges = customizableEdges18;
            BtnClose.Size = new Size(40, 30);
            BtnClose.TabIndex = 1;
            BtnClose.Text = "✕";
            BtnClose.Click += BtnClose_Click;
            // 
            // LabelTitle
            // 
            LabelTitle.BackColor = Color.Transparent;
            LabelTitle.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LabelTitle.ForeColor = Color.Lime;
            LabelTitle.Location = new Point(20, 11);
            LabelTitle.Name = "LabelTitle";
            LabelTitle.Size = new Size(235, 32);
            LabelTitle.TabIndex = 0;
            LabelTitle.Text = "GTA V MOD MANAGER";
            // 
            // ShadowForm
            // 
            ShadowForm.ShadowColor = Color.Green;
            ShadowForm.TargetForm = this;
            // 
            // Elipse
            // 
            Elipse.BorderRadius = 12;
            Elipse.TargetControl = this;
            // 
            // SidebarPanel
            // 
            SidebarPanel.BackColor = Color.FromArgb(25, 25, 25);
            SidebarPanel.Controls.Add(BtnConfig);
            SidebarPanel.Controls.Add(BtnLogs);
            SidebarPanel.Controls.Add(BtnStatus);
            SidebarPanel.Controls.Add(BtnMods);
            SidebarPanel.CustomizableEdges = customizableEdges29;
            SidebarPanel.Dock = DockStyle.Left;
            SidebarPanel.Location = new Point(0, 52);
            SidebarPanel.Name = "SidebarPanel";
            SidebarPanel.ShadowDecoration.CustomizableEdges = customizableEdges30;
            SidebarPanel.Size = new Size(250, 648);
            SidebarPanel.TabIndex = 1;
            // 
            // BtnConfig
            // 
            BtnConfig.BorderColor = Color.Green;
            BtnConfig.BorderRadius = 8;
            BtnConfig.BorderThickness = 1;
            BtnConfig.CustomizableEdges = customizableEdges21;
            BtnConfig.DisabledState.BorderColor = Color.DarkGray;
            BtnConfig.DisabledState.CustomBorderColor = Color.DarkGray;
            BtnConfig.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            BtnConfig.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            BtnConfig.FillColor = Color.FromArgb(35, 35, 35);
            BtnConfig.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnConfig.ForeColor = Color.White;
            BtnConfig.HoverState.FillColor = Color.Green;
            BtnConfig.Location = new Point(10, 200);
            BtnConfig.Name = "BtnConfig";
            BtnConfig.ShadowDecoration.CustomizableEdges = customizableEdges22;
            BtnConfig.Size = new Size(230, 50);
            BtnConfig.TabIndex = 3;
            BtnConfig.Text = "⚙️ Config";
            BtnConfig.TextAlign = HorizontalAlignment.Left;
            BtnConfig.Click += BtnConfig_Click;
            // 
            // BtnLogs
            // 
            BtnLogs.BorderColor = Color.Green;
            BtnLogs.BorderRadius = 8;
            BtnLogs.BorderThickness = 1;
            BtnLogs.CustomizableEdges = customizableEdges23;
            BtnLogs.DisabledState.BorderColor = Color.DarkGray;
            BtnLogs.DisabledState.CustomBorderColor = Color.DarkGray;
            BtnLogs.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            BtnLogs.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            BtnLogs.FillColor = Color.FromArgb(35, 35, 35);
            BtnLogs.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnLogs.ForeColor = Color.White;
            BtnLogs.HoverState.FillColor = Color.Green;
            BtnLogs.Location = new Point(10, 140);
            BtnLogs.Name = "BtnLogs";
            BtnLogs.ShadowDecoration.CustomizableEdges = customizableEdges24;
            BtnLogs.Size = new Size(230, 50);
            BtnLogs.TabIndex = 2;
            BtnLogs.Text = "📝 Logs";
            BtnLogs.TextAlign = HorizontalAlignment.Left;
            BtnLogs.Click += BtnLogs_Click;
            // 
            // BtnStatus
            // 
            BtnStatus.BorderColor = Color.Green;
            BtnStatus.BorderRadius = 8;
            BtnStatus.BorderThickness = 1;
            BtnStatus.CustomizableEdges = customizableEdges25;
            BtnStatus.DisabledState.BorderColor = Color.DarkGray;
            BtnStatus.DisabledState.CustomBorderColor = Color.DarkGray;
            BtnStatus.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            BtnStatus.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            BtnStatus.FillColor = Color.FromArgb(35, 35, 35);
            BtnStatus.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnStatus.ForeColor = Color.White;
            BtnStatus.HoverState.FillColor = Color.Green;
            BtnStatus.Location = new Point(10, 80);
            BtnStatus.Name = "BtnStatus";
            BtnStatus.ShadowDecoration.CustomizableEdges = customizableEdges26;
            BtnStatus.Size = new Size(230, 50);
            BtnStatus.TabIndex = 1;
            BtnStatus.Text = "📊 Status";
            BtnStatus.TextAlign = HorizontalAlignment.Left;
            BtnStatus.Click += BtnStatus_Click;
            // 
            // BtnMods
            // 
            BtnMods.BorderColor = Color.Green;
            BtnMods.BorderRadius = 8;
            BtnMods.BorderThickness = 1;
            BtnMods.CustomizableEdges = customizableEdges27;
            BtnMods.DisabledState.BorderColor = Color.DarkGray;
            BtnMods.DisabledState.CustomBorderColor = Color.DarkGray;
            BtnMods.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            BtnMods.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            BtnMods.FillColor = Color.FromArgb(35, 35, 35);
            BtnMods.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnMods.ForeColor = Color.White;
            BtnMods.HoverState.FillColor = Color.Green;
            BtnMods.Location = new Point(10, 20);
            BtnMods.Name = "BtnMods";
            BtnMods.ShadowDecoration.CustomizableEdges = customizableEdges28;
            BtnMods.Size = new Size(230, 50);
            BtnMods.TabIndex = 0;
            BtnMods.Text = "📦 Mods";
            BtnMods.TextAlign = HorizontalAlignment.Left;
            BtnMods.Click += BtnMods_Click;
            // 
            // MainPanel
            // 
            MainPanel.CustomizableEdges = customizableEdges31;
            MainPanel.Dock = DockStyle.Fill;
            MainPanel.Location = new Point(250, 52);
            MainPanel.Name = "MainPanel";
            MainPanel.Padding = new Padding(20);
            MainPanel.ShadowDecoration.CustomizableEdges = customizableEdges32;
            MainPanel.Size = new Size(950, 648);
            MainPanel.TabIndex = 2;
            // 
            // refreshTimer
            // 
            refreshTimer.Interval = 5000;
            refreshTimer.Tick += RefreshTimer_Tick;
            // 
            // GTAVModManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(17, 17, 17);
            ClientSize = new Size(1200, 700);
            Controls.Add(MainPanel);
            Controls.Add(SidebarPanel);
            Controls.Add(PanelTop);
            FormBorderStyle = FormBorderStyle.None;
            Name = "GTAVModManager";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GTA V Mod Manager";
            FormClosing += GTAVModManager_FormClosing;
            KeyDown += GTAVModManager_KeyDown;
            PanelTop.ResumeLayout(false);
            PanelTop.PerformLayout();
            SidebarPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2DragControl DragControl;
        private Guna.UI2.WinForms.Guna2ShadowForm ShadowForm;
        private Guna.UI2.WinForms.Guna2Elipse Elipse;
        private Guna.UI2.WinForms.Guna2Panel PanelTop;
        private Guna.UI2.WinForms.Guna2HtmlLabel LabelTitle;
        private Guna.UI2.WinForms.Guna2Button BtnClose;
        private Guna.UI2.WinForms.Guna2Panel SidebarPanel;
        private Guna.UI2.WinForms.Guna2Button BtnMods;
        private Guna.UI2.WinForms.Guna2Button BtnConfig;
        private Guna.UI2.WinForms.Guna2Button BtnLogs;
        private Guna.UI2.WinForms.Guna2Button BtnStatus;
        private Guna.UI2.WinForms.Guna2Panel MainPanel;
        private System.Windows.Forms.Timer refreshTimer;
    }
}