
namespace GTAVModManager.UserControls
{
    partial class LogsControl
    {
        private System.ComponentModel.IContainer components = null;


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _refreshTimer?.Stop();
                _refreshTimer?.Dispose();
                _client?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lblLogs = new Guna.UI2.WinForms.Guna2HtmlLabel();
            panelFilters = new Guna.UI2.WinForms.Guna2Panel();
            btnAll = new Guna.UI2.WinForms.Guna2Button();
            btnInfo = new Guna.UI2.WinForms.Guna2Button();
            btnWarning = new Guna.UI2.WinForms.Guna2Button();
            btnError = new Guna.UI2.WinForms.Guna2Button();
            txtLogs = new Guna.UI2.WinForms.Guna2TextBox();
            btnClear = new Guna.UI2.WinForms.Guna2Button();
            btnExport = new Guna.UI2.WinForms.Guna2Button();
            _refreshTimer = new System.Windows.Forms.Timer(components);
            panelFilters.SuspendLayout();
            SuspendLayout();
            // 
            // lblLogs
            // 
            lblLogs.BackColor = Color.Transparent;
            lblLogs.Font = new Font("Segoe UI Black", 16F, FontStyle.Bold);
            lblLogs.ForeColor = Color.FromArgb(0, 255, 135);
            lblLogs.Location = new Point(15, 15);
            lblLogs.Name = "lblLogs";
            lblLogs.Size = new Size(57, 32);
            lblLogs.TabIndex = 0;
            lblLogs.Text = "LOGS";
            // 
            // panelFilters
            // 
            panelFilters.BackColor = Color.FromArgb(25, 25, 25);
            panelFilters.BorderColor = Color.FromArgb(40, 40, 40);
            panelFilters.BorderRadius = 12;
            panelFilters.BorderThickness = 1;
            panelFilters.Controls.Add(btnAll);
            panelFilters.Controls.Add(btnInfo);
            panelFilters.Controls.Add(btnWarning);
            panelFilters.Controls.Add(btnError);
            panelFilters.CustomizableEdges = customizableEdges9;
            panelFilters.Location = new Point(15, 55);
            panelFilters.Name = "panelFilters";
            panelFilters.ShadowDecoration.CustomizableEdges = customizableEdges10;
            panelFilters.Size = new Size(548, 45);
            panelFilters.TabIndex = 1;
            // 
            // btnAll
            // 
            btnAll.BorderRadius = 6;
            btnAll.CustomizableEdges = customizableEdges1;
            btnAll.FillColor = Color.FromArgb(0, 255, 135);
            btnAll.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAll.ForeColor = Color.Black;
            btnAll.Location = new Point(10, 8);
            btnAll.Name = "btnAll";
            btnAll.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnAll.Size = new Size(70, 28);
            btnAll.TabIndex = 2;
            btnAll.Text = "All";
            btnAll.Click += btnAll_Click;
            // 
            // btnInfo
            // 
            btnInfo.BorderRadius = 6;
            btnInfo.CustomizableEdges = customizableEdges3;
            btnInfo.FillColor = Color.FromArgb(35, 35, 35);
            btnInfo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnInfo.ForeColor = Color.FromArgb(180, 180, 180);
            btnInfo.Location = new Point(90, 8);
            btnInfo.Name = "btnInfo";
            btnInfo.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnInfo.Size = new Size(70, 28);
            btnInfo.TabIndex = 3;
            btnInfo.Text = "Info";
            btnInfo.Click += btnInfo_Click;
            // 
            // btnWarning
            // 
            btnWarning.BorderRadius = 6;
            btnWarning.CustomizableEdges = customizableEdges5;
            btnWarning.FillColor = Color.FromArgb(35, 35, 35);
            btnWarning.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnWarning.ForeColor = Color.FromArgb(180, 180, 180);
            btnWarning.Location = new Point(170, 8);
            btnWarning.Name = "btnWarning";
            btnWarning.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnWarning.Size = new Size(85, 28);
            btnWarning.TabIndex = 4;
            btnWarning.Text = "Warning";
            btnWarning.Click += btnWarning_Click;
            // 
            // btnError
            // 
            btnError.BorderRadius = 6;
            btnError.CustomizableEdges = customizableEdges7;
            btnError.FillColor = Color.FromArgb(35, 35, 35);
            btnError.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnError.ForeColor = Color.FromArgb(180, 180, 180);
            btnError.Location = new Point(265, 8);
            btnError.Name = "btnError";
            btnError.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnError.Size = new Size(70, 28);
            btnError.TabIndex = 5;
            btnError.Text = "Error";
            btnError.Click += btnError_Click;
            // 
            // txtLogs
            // 
            txtLogs.BorderColor = Color.FromArgb(40, 40, 40);
            txtLogs.BorderRadius = 10;
            txtLogs.Cursor = Cursors.IBeam;
            txtLogs.CustomizableEdges = customizableEdges11;
            txtLogs.DefaultText = "";
            txtLogs.DisabledState.BorderColor = Color.FromArgb(42, 42, 42);
            txtLogs.DisabledState.FillColor = Color.FromArgb(24, 24, 24);
            txtLogs.DisabledState.ForeColor = Color.FromArgb(234, 234, 234);
            txtLogs.FillColor = Color.FromArgb(20, 20, 20);
            txtLogs.FocusedState.BorderColor = Color.FromArgb(0, 255, 135);
            txtLogs.Font = new Font("Consolas", 9.25F);
            txtLogs.ForeColor = Color.FromArgb(230, 230, 230);
            txtLogs.Location = new Point(15, 110);
            txtLogs.Multiline = true;
            txtLogs.Name = "txtLogs";
            txtLogs.PlaceholderText = "Log output...";
            txtLogs.ReadOnly = true;
            txtLogs.ScrollBars = ScrollBars.Vertical;
            txtLogs.SelectedText = "";
            txtLogs.ShadowDecoration.CustomizableEdges = customizableEdges12;
            txtLogs.Size = new Size(548, 200);
            txtLogs.TabIndex = 6;
            txtLogs.Scroll += txtLogs_Scroll;
            txtLogs.Click += txtLogs_Click;
            txtLogs.KeyDown += txtLogs_KeyDown;
            // 
            // btnClear
            // 
            btnClear.BorderRadius = 6;
            btnClear.CustomizableEdges = customizableEdges13;
            btnClear.FillColor = Color.FromArgb(255, 77, 77);
            btnClear.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClear.ForeColor = Color.Black;
            btnClear.Location = new Point(370, 320);
            btnClear.Name = "btnClear";
            btnClear.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnClear.Size = new Size(85, 28);
            btnClear.TabIndex = 7;
            btnClear.Text = "Clear";
            btnClear.Click += btnClear_Click;
            // 
            // btnExport
            // 
            btnExport.BorderRadius = 6;
            btnExport.CustomizableEdges = customizableEdges15;
            btnExport.FillColor = Color.FromArgb(0, 255, 135);
            btnExport.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnExport.ForeColor = Color.Black;
            btnExport.Location = new Point(465, 320);
            btnExport.Name = "btnExport";
            btnExport.ShadowDecoration.CustomizableEdges = customizableEdges16;
            btnExport.Size = new Size(98, 28);
            btnExport.TabIndex = 8;
            btnExport.Text = "Export Logs";
            btnExport.Click += btnExport_Click;
            // 
            // _refreshTimer
            // 
            _refreshTimer.Interval = 1000;
            _refreshTimer.Tick += _refreshTimer_Tick;
            // 
            // LogsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 18, 18);
            Controls.Add(lblLogs);
            Controls.Add(panelFilters);
            Controls.Add(txtLogs);
            Controls.Add(btnClear);
            Controls.Add(btnExport);
            Name = "LogsControl";
            Size = new Size(578, 353);
            Load += LogsControl_Load;
            panelFilters.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel lblLogs;
        private Guna.UI2.WinForms.Guna2Panel panelFilters;
        private Guna.UI2.WinForms.Guna2Button btnAll;
        private Guna.UI2.WinForms.Guna2Button btnInfo;
        private Guna.UI2.WinForms.Guna2Button btnWarning;
        private Guna.UI2.WinForms.Guna2Button btnError;
        private Guna.UI2.WinForms.Guna2TextBox txtLogs;
        private Guna.UI2.WinForms.Guna2Button btnClear;
        private Guna.UI2.WinForms.Guna2Button btnExport;
        private System.Windows.Forms.Timer _refreshTimer;
    }
}