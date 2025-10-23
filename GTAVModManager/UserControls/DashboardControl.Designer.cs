namespace GTAVModManager.UserControls
{
    partial class DashboardControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _cpuCounter?.Dispose();
                _ramCounter?.Dispose();
                _currentProcess?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lblDashboard = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblStatus = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblUptime = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblModsLoaded = new Guna.UI2.WinForms.Guna2HtmlLabel();
            PanelStatus = new Guna.UI2.WinForms.Guna2Panel();
            lblRAMUsage = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblFPSValue = new Guna.UI2.WinForms.Guna2HtmlLabel();
            ramUsageBar = new Guna.UI2.WinForms.Guna2ProgressBar();
            lblFPS = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblCPUUsage = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblRAM = new Guna.UI2.WinForms.Guna2HtmlLabel();
            cpuUsageBar = new Guna.UI2.WinForms.Guna2ProgressBar();
            lblCPU = new Guna.UI2.WinForms.Guna2HtmlLabel();
            statusIndicator = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            updateTimer = new System.Windows.Forms.Timer(components);
            PanelStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)statusIndicator).BeginInit();
            SuspendLayout();
            // 
            // lblDashboard
            // 
            lblDashboard.BackColor = Color.Transparent;
            lblDashboard.Font = new Font("Segoe UI Black", 16F, FontStyle.Bold);
            lblDashboard.ForeColor = Color.FromArgb(0, 255, 135);
            lblDashboard.Location = new Point(15, 15);
            lblDashboard.Name = "lblDashboard";
            lblDashboard.Size = new Size(141, 32);
            lblDashboard.TabIndex = 0;
            lblDashboard.Text = "DASHBOARD";
            // 
            // lblStatus
            // 
            lblStatus.BackColor = Color.Transparent;
            lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStatus.ForeColor = Color.FromArgb(180, 180, 180);
            lblStatus.Location = new Point(20, 60);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(104, 17);
            lblStatus.TabIndex = 4;
            lblStatus.Text = "Status: Connected";
            // 
            // lblUptime
            // 
            lblUptime.BackColor = Color.Transparent;
            lblUptime.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblUptime.ForeColor = Color.FromArgb(180, 180, 180);
            lblUptime.Location = new Point(230, 60);
            lblUptime.Name = "lblUptime";
            lblUptime.Size = new Size(99, 17);
            lblUptime.TabIndex = 5;
            lblUptime.Text = "Uptime: 00:12:48";
            // 
            // lblModsLoaded
            // 
            lblModsLoaded.BackColor = Color.Transparent;
            lblModsLoaded.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblModsLoaded.ForeColor = Color.FromArgb(180, 180, 180);
            lblModsLoaded.Location = new Point(430, 60);
            lblModsLoaded.Name = "lblModsLoaded";
            lblModsLoaded.Size = new Size(89, 17);
            lblModsLoaded.TabIndex = 6;
            lblModsLoaded.Text = "Mods Loaded: 8";
            // 
            // PanelStatus
            // 
            PanelStatus.BackColor = Color.FromArgb(25, 25, 25);
            PanelStatus.BorderColor = Color.FromArgb(40, 40, 40);
            PanelStatus.BorderRadius = 15;
            PanelStatus.BorderThickness = 1;
            PanelStatus.Controls.Add(lblRAMUsage);
            PanelStatus.Controls.Add(lblFPSValue);
            PanelStatus.Controls.Add(ramUsageBar);
            PanelStatus.Controls.Add(lblFPS);
            PanelStatus.Controls.Add(lblCPUUsage);
            PanelStatus.Controls.Add(lblRAM);
            PanelStatus.Controls.Add(cpuUsageBar);
            PanelStatus.Controls.Add(lblCPU);
            PanelStatus.CustomizableEdges = customizableEdges5;
            PanelStatus.Location = new Point(20, 90);
            PanelStatus.Name = "PanelStatus";
            PanelStatus.ShadowDecoration.CustomizableEdges = customizableEdges6;
            PanelStatus.Size = new Size(540, 230);
            PanelStatus.TabIndex = 7;
            // 
            // lblRAMUsage
            // 
            lblRAMUsage.BackColor = Color.Transparent;
            lblRAMUsage.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRAMUsage.ForeColor = Color.FromArgb(234, 234, 234);
            lblRAMUsage.Location = new Point(195, 104);
            lblRAMUsage.Name = "lblRAMUsage";
            lblRAMUsage.Size = new Size(78, 17);
            lblRAMUsage.TabIndex = 13;
            lblRAMUsage.Text = "2.9 GB / 8 GB";
            // 
            // lblFPSValue
            // 
            lblFPSValue.BackColor = Color.Transparent;
            lblFPSValue.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblFPSValue.ForeColor = Color.FromArgb(0, 255, 135);
            lblFPSValue.Location = new Point(75, 135);
            lblFPSValue.Name = "lblFPSValue";
            lblFPSValue.Size = new Size(35, 39);
            lblFPSValue.TabIndex = 16;
            lblFPSValue.Text = "60";
            // 
            // ramUsageBar
            // 
            ramUsageBar.BorderRadius = 6;
            ramUsageBar.CustomizableEdges = customizableEdges1;
            ramUsageBar.FillColor = Color.FromArgb(35, 35, 35);
            ramUsageBar.Location = new Point(25, 109);
            ramUsageBar.Name = "ramUsageBar";
            ramUsageBar.ProgressColor = Color.FromArgb(0, 255, 135);
            ramUsageBar.ProgressColor2 = Color.FromArgb(0, 204, 255);
            ramUsageBar.ShadowDecoration.CustomizableEdges = customizableEdges2;
            ramUsageBar.Size = new Size(160, 10);
            ramUsageBar.TabIndex = 12;
            ramUsageBar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            ramUsageBar.Value = 45;
            // 
            // lblFPS
            // 
            lblFPS.BackColor = Color.Transparent;
            lblFPS.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFPS.ForeColor = Color.FromArgb(150, 150, 150);
            lblFPS.Location = new Point(25, 145);
            lblFPS.Name = "lblFPS";
            lblFPS.Size = new Size(23, 17);
            lblFPS.TabIndex = 15;
            lblFPS.Text = "FPS";
            // 
            // lblCPUUsage
            // 
            lblCPUUsage.BackColor = Color.Transparent;
            lblCPUUsage.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCPUUsage.ForeColor = Color.FromArgb(234, 234, 234);
            lblCPUUsage.Location = new Point(195, 43);
            lblCPUUsage.Name = "lblCPUUsage";
            lblCPUUsage.Size = new Size(27, 17);
            lblCPUUsage.TabIndex = 10;
            lblCPUUsage.Text = "32%";
            // 
            // lblRAM
            // 
            lblRAM.BackColor = Color.Transparent;
            lblRAM.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRAM.ForeColor = Color.FromArgb(150, 150, 150);
            lblRAM.Location = new Point(25, 85);
            lblRAM.Name = "lblRAM";
            lblRAM.Size = new Size(87, 17);
            lblRAM.TabIndex = 12;
            lblRAM.Text = "Memory Usage";
            // 
            // cpuUsageBar
            // 
            cpuUsageBar.BorderRadius = 6;
            cpuUsageBar.CustomizableEdges = customizableEdges3;
            cpuUsageBar.FillColor = Color.FromArgb(35, 35, 35);
            cpuUsageBar.Location = new Point(25, 48);
            cpuUsageBar.Name = "cpuUsageBar";
            cpuUsageBar.ProgressColor = Color.FromArgb(0, 255, 135);
            cpuUsageBar.ProgressColor2 = Color.FromArgb(0, 204, 255);
            cpuUsageBar.ShadowDecoration.CustomizableEdges = customizableEdges4;
            cpuUsageBar.Size = new Size(160, 10);
            cpuUsageBar.TabIndex = 9;
            cpuUsageBar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            cpuUsageBar.Value = 32;
            // 
            // lblCPU
            // 
            lblCPU.BackColor = Color.Transparent;
            lblCPU.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCPU.ForeColor = Color.FromArgb(150, 150, 150);
            lblCPU.Location = new Point(25, 25);
            lblCPU.Name = "lblCPU";
            lblCPU.Size = new Size(63, 17);
            lblCPU.TabIndex = 9;
            lblCPU.Text = "CPU Usage";
            // 
            // statusIndicator
            // 
            statusIndicator.BackColor = Color.FromArgb(0, 255, 135);
            statusIndicator.FillColor = Color.FromArgb(0, 255, 135);
            statusIndicator.ImageRotate = 0F;
            statusIndicator.Location = new Point(127, 63);
            statusIndicator.Name = "statusIndicator";
            statusIndicator.ShadowDecoration.CustomizableEdges = customizableEdges7;
            statusIndicator.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            statusIndicator.Size = new Size(10, 10);
            statusIndicator.TabIndex = 16;
            statusIndicator.TabStop = false;
            // 
            // updateTimer
            // 
            updateTimer.Interval = 1500;
            updateTimer.Tick += updateTimer_Tick_1;
            // 
            // DashboardControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 18, 18);
            Controls.Add(statusIndicator);
            Controls.Add(PanelStatus);
            Controls.Add(lblModsLoaded);
            Controls.Add(lblUptime);
            Controls.Add(lblStatus);
            Controls.Add(lblDashboard);
            Name = "DashboardControl";
            Size = new Size(578, 353);
            Load += DashboardControl_Load;
            PanelStatus.ResumeLayout(false);
            PanelStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)statusIndicator).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel lblDashboard;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblStatus;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblUptime;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblModsLoaded;
        private Guna.UI2.WinForms.Guna2Panel PanelStatus;
        private Guna.UI2.WinForms.Guna2CirclePictureBox statusIndicator;
        private Guna.UI2.WinForms.Guna2ProgressBar cpuUsageBar;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCPUUsage;
        private Guna.UI2.WinForms.Guna2ProgressBar ramUsageBar;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblRAMUsage;
        private System.Windows.Forms.Timer updateTimer;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblFPSValue;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblFPS;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblRAM;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCPU;
    }
}