namespace GTAVModManager.UserControls
{
    partial class ModsControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _refreshTimer?.Stop();
                _refreshTimer?.Dispose();
                _client?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lblMods = new Guna.UI2.WinForms.Guna2HtmlLabel();
            modsTable = new Guna.UI2.WinForms.Guna2DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            btnAdd = new Guna.UI2.WinForms.Guna2Button();
            btnRemove = new Guna.UI2.WinForms.Guna2Button();
            btnReload = new Guna.UI2.WinForms.Guna2Button();
            lblTotalMods = new Guna.UI2.WinForms.Guna2HtmlLabel();
            panelMods = new Guna.UI2.WinForms.Guna2Panel();
            _refreshTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)modsTable).BeginInit();
            SuspendLayout();
            // 
            // lblMods
            // 
            lblMods.BackColor = Color.Transparent;
            lblMods.Font = new Font("Segoe UI Black", 16F, FontStyle.Bold);
            lblMods.ForeColor = Color.FromArgb(0, 255, 135);
            lblMods.Location = new Point(15, 15);
            lblMods.Name = "lblMods";
            lblMods.Size = new Size(68, 32);
            lblMods.TabIndex = 0;
            lblMods.Text = "MODS";
            // 
            // modsTable
            // 
            modsTable.AllowUserToAddRows = false;
            modsTable.AllowUserToDeleteRows = false;
            modsTable.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.White;
            modsTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            modsTable.BackgroundColor = Color.FromArgb(24, 24, 24);
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(18, 18, 18);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(0, 255, 135);
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            modsTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            modsTable.ColumnHeadersHeight = 30;
            modsTable.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4 });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(24, 24, 24);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(234, 234, 234);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(0, 255, 135);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(18, 18, 18);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            modsTable.DefaultCellStyle = dataGridViewCellStyle3;
            modsTable.GridColor = Color.FromArgb(42, 42, 42);
            modsTable.Location = new Point(18, 58);
            modsTable.MultiSelect = false;
            modsTable.Name = "modsTable";
            modsTable.ReadOnly = true;
            modsTable.RowHeadersVisible = false;
            modsTable.RowTemplate.Height = 30;
            modsTable.ScrollBars = ScrollBars.Vertical;
            modsTable.Size = new Size(542, 224);
            modsTable.TabIndex = 2;
            modsTable.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            modsTable.ThemeStyle.AlternatingRowsStyle.Font = null;
            modsTable.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            modsTable.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            modsTable.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            modsTable.ThemeStyle.BackColor = Color.FromArgb(24, 24, 24);
            modsTable.ThemeStyle.GridColor = Color.FromArgb(42, 42, 42);
            modsTable.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(18, 18, 18);
            modsTable.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            modsTable.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            modsTable.ThemeStyle.HeaderStyle.ForeColor = Color.FromArgb(0, 255, 135);
            modsTable.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            modsTable.ThemeStyle.HeaderStyle.Height = 30;
            modsTable.ThemeStyle.ReadOnly = true;
            modsTable.ThemeStyle.RowsStyle.BackColor = Color.FromArgb(24, 24, 24);
            modsTable.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            modsTable.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            modsTable.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(234, 234, 234);
            modsTable.ThemeStyle.RowsStyle.Height = 30;
            modsTable.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(0, 255, 135);
            modsTable.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(18, 18, 18);
            modsTable.CellFormatting += ModsTable_CellFormatting;
            modsTable.SelectionChanged += ModsTable_SelectionChanged;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.DataPropertyName = "Name";
            dataGridViewTextBoxColumn1.HeaderText = "Mod Name";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.DataPropertyName = "SizeFormatted";
            dataGridViewTextBoxColumn2.HeaderText = "Type";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Size";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "Status";
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // btnAdd
            // 
            btnAdd.BorderRadius = 6;
            btnAdd.CustomizableEdges = customizableEdges1;
            btnAdd.FillColor = Color.FromArgb(0, 255, 135);
            btnAdd.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAdd.ForeColor = Color.Black;
            btnAdd.Location = new Point(15, 305);
            btnAdd.Name = "btnAdd";
            btnAdd.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnAdd.Size = new Size(110, 30);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "📁 Add Mod";
            btnAdd.Click += AddMod;
            // 
            // btnRemove
            // 
            btnRemove.BorderRadius = 6;
            btnRemove.CustomizableEdges = customizableEdges3;
            btnRemove.FillColor = Color.FromArgb(255, 77, 77);
            btnRemove.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRemove.ForeColor = Color.Black;
            btnRemove.Location = new Point(135, 305);
            btnRemove.Name = "btnRemove";
            btnRemove.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnRemove.Size = new Size(100, 30);
            btnRemove.TabIndex = 4;
            btnRemove.Text = "🗑️ Remove";
            btnRemove.Click += RemoveMod;
            // 
            // btnReload
            // 
            btnReload.BorderRadius = 6;
            btnReload.CustomizableEdges = customizableEdges5;
            btnReload.FillColor = Color.FromArgb(0, 204, 106);
            btnReload.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnReload.ForeColor = Color.Black;
            btnReload.Location = new Point(245, 305);
            btnReload.Name = "btnReload";
            btnReload.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnReload.Size = new Size(105, 30);
            btnReload.TabIndex = 5;
            btnReload.Text = "🔄 Reload All";
            btnReload.Click += ReloadAll;
            // 
            // lblTotalMods
            // 
            lblTotalMods.BackColor = Color.Transparent;
            lblTotalMods.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotalMods.ForeColor = Color.FromArgb(156, 156, 156);
            lblTotalMods.Location = new Point(470, 312);
            lblTotalMods.Name = "lblTotalMods";
            lblTotalMods.Size = new Size(77, 17);
            lblTotalMods.TabIndex = 6;
            lblTotalMods.Text = "Total: 0 Mods";
            // 
            // panelMods
            // 
            panelMods.BackColor = Color.FromArgb(25, 25, 25);
            panelMods.BorderColor = Color.FromArgb(42, 42, 42);
            panelMods.BorderRadius = 10;
            panelMods.BorderThickness = 1;
            panelMods.CustomizableEdges = customizableEdges7;
            panelMods.Location = new Point(15, 55);
            panelMods.Name = "panelMods";
            panelMods.ShadowDecoration.CustomizableEdges = customizableEdges8;
            panelMods.Size = new Size(548, 230);
            panelMods.TabIndex = 1;
            // 
            // _refreshTimer
            // 
            _refreshTimer.Interval = 2000;
            _refreshTimer.Tick += RefreshModsList;
            // 
            // ModsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 18, 18);
            Controls.Add(lblMods);
            Controls.Add(modsTable);
            Controls.Add(panelMods);
            Controls.Add(btnAdd);
            Controls.Add(btnRemove);
            Controls.Add(btnReload);
            Controls.Add(lblTotalMods);
            Name = "ModsControl";
            Size = new Size(578, 353);
            Load += ModsControl_Load;
            ((System.ComponentModel.ISupportInitialize)modsTable).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel lblMods;
        private Guna.UI2.WinForms.Guna2Panel panelMods;
        private Guna.UI2.WinForms.Guna2DataGridView modsTable;
        private Guna.UI2.WinForms.Guna2Button btnAdd;
        private Guna.UI2.WinForms.Guna2Button btnRemove;
        private Guna.UI2.WinForms.Guna2Button btnReload;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTotalMods;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.Timer _refreshTimer;
    }
}