namespace GTAVModManager.UserControls
{
    partial class LogsControl
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
            lblLogs = new Guna.UI2.WinForms.Guna2HtmlLabel();
            panelFilters = new Guna.UI2.WinForms.Guna2Panel();
            btnAll = new Guna.UI2.WinForms.Guna2Button();
            btnInfo = new Guna.UI2.WinForms.Guna2Button();
            btnWarning = new Guna.UI2.WinForms.Guna2Button();
            btnError = new Guna.UI2.WinForms.Guna2Button();
            txtLogs = new Guna.UI2.WinForms.Guna2TextBox();
            btnClear = new Guna.UI2.WinForms.Guna2Button();
            btnExport = new Guna.UI2.WinForms.Guna2Button();
            SuspendLayout();
            // 
            // lblLogs
            // 
            lblLogs.BackColor = Color.Transparent;
            lblLogs.Font = new Font("Segoe UI Black", 16F, FontStyle.Bold);
            lblLogs.ForeColor = Color.FromArgb(0, 255, 135);
            lblLogs.Location = new Point(15, 15);
            lblLogs.Name = "lblLogs";
            lblLogs.Size = new Size(70, 32);
            lblLogs.TabIndex = 0;
            lblLogs.Text = "LOGS";
            // 
            // panelFilters
            // 
            panelFilters.BackColor = Color.FromArgb(25, 25, 25);
            panelFilters.BorderColor = Color.FromArgb(40, 40, 40);
            panelFilters.BorderRadius = 12;
            panelFilters.BorderThickness = 1;
            panelFilters.CustomizableEdges = edges1;
            panelFilters.Location = new Point(15, 55);
            panelFilters.Name = "panelFilters";
            panelFilters.ShadowDecoration.CustomizableEdges = edges2;
            panelFilters.Size = new Size(548, 45);
            panelFilters.TabIndex = 1;
            // 
            // btnAll
            // 
            btnAll.BorderRadius = 6;
            btnAll.FillColor = Color.FromArgb(0, 255, 135);
            btnAll.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAll.ForeColor = Color.Black;
            btnAll.Location = new Point(25, 63);
            btnAll.Name = "btnAll";
            btnAll.Size = new Size(70, 28);
            btnAll.TabIndex = 2;
            btnAll.Text = "All";
            // 
            // btnInfo
            // 
            btnInfo.BorderRadius = 6;
            btnInfo.FillColor = Color.FromArgb(0, 204, 255);
            btnInfo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnInfo.ForeColor = Color.Black;
            btnInfo.Location = new Point(105, 63);
            btnInfo.Name = "btnInfo";
            btnInfo.Size = new Size(70, 28);
            btnInfo.TabIndex = 3;
            btnInfo.Text = "Info";
            // 
            // btnWarning
            // 
            btnWarning.BorderRadius = 6;
            btnWarning.FillColor = Color.FromArgb(255, 204, 0);
            btnWarning.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnWarning.ForeColor = Color.Black;
            btnWarning.Location = new Point(185, 63);
            btnWarning.Name = "btnWarning";
            btnWarning.Size = new Size(85, 28);
            btnWarning.TabIndex = 4;
            btnWarning.Text = "Warning";
            // 
            // btnError
            // 
            btnError.BorderRadius = 6;
            btnError.FillColor = Color.FromArgb(255, 77, 77);
            btnError.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnError.ForeColor = Color.Black;
            btnError.Location = new Point(280, 63);
            btnError.Name = "btnError";
            btnError.Size = new Size(70, 28);
            btnError.TabIndex = 5;
            btnError.Text = "Error";
            // 
            // txtLogs
            // 
            txtLogs.BorderColor = Color.FromArgb(40, 40, 40);
            txtLogs.BorderRadius = 10;
            txtLogs.Cursor = Cursors.IBeam;
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
            txtLogs.PasswordChar = '\0';
            txtLogs.PlaceholderText = "Log output...";
            txtLogs.ReadOnly = true;
            txtLogs.ScrollBars = ScrollBars.Vertical;
            txtLogs.Size = new Size(548, 200);
            txtLogs.TabIndex = 6;
            // 
            // btnClear
            // 
            btnClear.BorderRadius = 6;
            btnClear.FillColor = Color.FromArgb(255, 77, 77);
            btnClear.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClear.ForeColor = Color.Black;
            btnClear.Location = new Point(370, 320);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(85, 28);
            btnClear.TabIndex = 7;
            btnClear.Text = "Clear";
            // 
            // btnExport
            // 
            btnExport.BorderRadius = 6;
            btnExport.FillColor = Color.FromArgb(0, 255, 135);
            btnExport.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnExport.ForeColor = Color.Black;
            btnExport.Location = new Point(465, 320);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(98, 28);
            btnExport.TabIndex = 8;
            btnExport.Text = "Export Logs";
            // 
            // LogsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 18, 18);
            Controls.Add(lblLogs);
            Controls.Add(panelFilters);
            Controls.Add(btnAll);
            Controls.Add(btnInfo);
            Controls.Add(btnWarning);
            Controls.Add(btnError);
            Controls.Add(txtLogs);
            Controls.Add(btnClear);
            Controls.Add(btnExport);
            Name = "LogsControl";
            Size = new Size(578, 353);
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
    }
}