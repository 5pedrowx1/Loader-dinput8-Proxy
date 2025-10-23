namespace GTAVModManager.UserControls
{
    partial class PerformanceControl
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lblPerformance = new Guna.UI2.WinForms.Guna2HtmlLabel();
            panelSummary = new Guna.UI2.WinForms.Guna2Panel();
            lblTotalCalls = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblAvgTime = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblPeakTime = new Guna.UI2.WinForms.Guna2HtmlLabel();
            performanceTable = new Guna.UI2.WinForms.Guna2DataGridView();
            btnReset = new Guna.UI2.WinForms.Guna2Button();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)performanceTable).BeginInit();
            SuspendLayout();
            // 
            // lblPerformance
            // 
            lblPerformance.BackColor = Color.Transparent;
            lblPerformance.Font = new Font("Segoe UI Black", 16F, FontStyle.Bold);
            lblPerformance.ForeColor = Color.FromArgb(0, 255, 135);
            lblPerformance.Location = new Point(15, 15);
            lblPerformance.Name = "lblPerformance";
            lblPerformance.Size = new Size(163, 32);
            lblPerformance.TabIndex = 0;
            lblPerformance.Text = "PERFORMANCE";
            // 
            // panelSummary
            // 
            panelSummary.BackColor = Color.FromArgb(25, 25, 25);
            panelSummary.BorderColor = Color.FromArgb(40, 40, 40);
            panelSummary.BorderRadius = 12;
            panelSummary.BorderThickness = 1;
            panelSummary.CustomizableEdges = customizableEdges1;
            panelSummary.Location = new Point(15, 55);
            panelSummary.Name = "panelSummary";
            panelSummary.ShadowDecoration.CustomizableEdges = customizableEdges2;
            panelSummary.Size = new Size(548, 60);
            panelSummary.TabIndex = 1;
            // 
            // lblTotalCalls
            // 
            lblTotalCalls.BackColor = Color.Transparent;
            lblTotalCalls.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotalCalls.ForeColor = Color.FromArgb(156, 156, 156);
            lblTotalCalls.Location = new Point(30, 70);
            lblTotalCalls.Name = "lblTotalCalls";
            lblTotalCalls.Size = new Size(92, 17);
            lblTotalCalls.TabIndex = 2;
            lblTotalCalls.Text = "Total Calls: 1234";
            // 
            // lblAvgTime
            // 
            lblAvgTime.BackColor = Color.Transparent;
            lblAvgTime.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAvgTime.ForeColor = Color.FromArgb(156, 156, 156);
            lblAvgTime.Location = new Point(200, 70);
            lblAvgTime.Name = "lblAvgTime";
            lblAvgTime.Size = new Size(105, 17);
            lblAvgTime.TabIndex = 3;
            lblAvgTime.Text = "Avg Time: 1.23 ms";
            // 
            // lblPeakTime
            // 
            lblPeakTime.BackColor = Color.Transparent;
            lblPeakTime.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPeakTime.ForeColor = Color.FromArgb(156, 156, 156);
            lblPeakTime.Location = new Point(380, 70);
            lblPeakTime.Name = "lblPeakTime";
            lblPeakTime.Size = new Size(79, 17);
            lblPeakTime.TabIndex = 4;
            lblPeakTime.Text = "Peak: 5.67 ms";
            // 
            // performanceTable
            // 
            performanceTable.AllowUserToAddRows = false;
            performanceTable.AllowUserToDeleteRows = false;
            performanceTable.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(24, 24, 24);
            performanceTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            performanceTable.BackgroundColor = Color.FromArgb(20, 20, 20);
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(18, 18, 18);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(0, 255, 135);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            performanceTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            performanceTable.ColumnHeadersHeight = 30;
            performanceTable.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn6 });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(24, 24, 24);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(234, 234, 234);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(0, 255, 135);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(18, 18, 18);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            performanceTable.DefaultCellStyle = dataGridViewCellStyle3;
            performanceTable.GridColor = Color.FromArgb(42, 42, 42);
            performanceTable.Location = new Point(15, 125);
            performanceTable.MultiSelect = false;
            performanceTable.Name = "performanceTable";
            performanceTable.ReadOnly = true;
            performanceTable.RowHeadersVisible = false;
            performanceTable.RowTemplate.Height = 30;
            performanceTable.ScrollBars = ScrollBars.Vertical;
            performanceTable.Size = new Size(548, 200);
            performanceTable.TabIndex = 5;
            performanceTable.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            performanceTable.ThemeStyle.AlternatingRowsStyle.Font = null;
            performanceTable.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            performanceTable.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            performanceTable.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            performanceTable.ThemeStyle.BackColor = Color.FromArgb(20, 20, 20);
            performanceTable.ThemeStyle.GridColor = Color.FromArgb(42, 42, 42);
            performanceTable.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            performanceTable.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            performanceTable.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            performanceTable.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            performanceTable.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            performanceTable.ThemeStyle.HeaderStyle.Height = 30;
            performanceTable.ThemeStyle.ReadOnly = true;
            performanceTable.ThemeStyle.RowsStyle.BackColor = Color.White;
            performanceTable.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            performanceTable.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            performanceTable.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            performanceTable.ThemeStyle.RowsStyle.Height = 30;
            performanceTable.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            performanceTable.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            // 
            // btnReset
            // 
            btnReset.BorderRadius = 6;
            btnReset.CustomizableEdges = customizableEdges3;
            btnReset.FillColor = Color.FromArgb(255, 77, 77);
            btnReset.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnReset.ForeColor = Color.Black;
            btnReset.Location = new Point(465, 335);
            btnReset.Name = "btnReset";
            btnReset.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnReset.Size = new Size(98, 28);
            btnReset.TabIndex = 6;
            btnReset.Text = "Reset";
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "✔";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Mod";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Calls";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "Avg Time (ms)";
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.HeaderText = "Peak (ms)";
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewTextBoxColumn6.HeaderText = "Errors";
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // PerformanceControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 18, 18);
            Controls.Add(lblPerformance);
            Controls.Add(panelSummary);
            Controls.Add(lblTotalCalls);
            Controls.Add(lblAvgTime);
            Controls.Add(lblPeakTime);
            Controls.Add(performanceTable);
            Controls.Add(btnReset);
            Name = "PerformanceControl";
            Size = new Size(578, 380);
            ((System.ComponentModel.ISupportInitialize)performanceTable).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel lblPerformance;
        private Guna.UI2.WinForms.Guna2Panel panelSummary;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTotalCalls;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblAvgTime;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPeakTime;
        private Guna.UI2.WinForms.Guna2DataGridView performanceTable;
        private Guna.UI2.WinForms.Guna2Button btnReset;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    }
}