using Guna.UI2.WinForms;

namespace GTAVModManager.UserControlers
{
    partial class ModsControl
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();

            this.panelTop = new Guna.UI2.WinForms.Guna2Panel();
            this.btnReloadAll = new Guna.UI2.WinForms.Guna2Button();
            this.btnScanFolder = new Guna.UI2.WinForms.Guna2Button();
            this.btnUnload = new Guna.UI2.WinForms.Guna2Button();
            this.btnLoad = new Guna.UI2.WinForms.Guna2Button();
            this.txtSearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.dgvMods = new Guna.UI2.WinForms.Guna2DataGridView();
            this.colName = new DataGridViewTextBoxColumn();
            this.colType = new DataGridViewTextBoxColumn();
            this.colSize = new DataGridViewTextBoxColumn();
            this.colLoadTime = new DataGridViewTextBoxColumn();
            this.colStatus = new DataGridViewTextBoxColumn();
            this.colID = new DataGridViewTextBoxColumn();

            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMods)).BeginInit();
            this.SuspendLayout();

            // panelTop
            this.panelTop.Controls.Add(this.btnReloadAll);
            this.panelTop.Controls.Add(this.btnScanFolder);
            this.panelTop.Controls.Add(this.btnUnload);
            this.panelTop.Controls.Add(this.btnLoad);
            this.panelTop.Controls.Add(this.txtSearch);
            this.panelTop.CustomizableEdges = customizableEdges1;
            this.panelTop.Dock = DockStyle.Top;
            this.panelTop.Location = new Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.ShadowDecoration.CustomizableEdges = customizableEdges2;
            this.panelTop.Size = new Size(950, 80);
            this.panelTop.TabIndex = 0;

            // txtSearch
            this.txtSearch.BorderColor = Color.Green;
            this.txtSearch.BorderRadius = 8;
            this.txtSearch.CustomizableEdges = customizableEdges3;
            this.txtSearch.DefaultText = "";
            this.txtSearch.FillColor = Color.FromArgb(35, 35, 35);
            this.txtSearch.ForeColor = Color.White;
            this.txtSearch.Location = new Point(20, 20);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PasswordChar = '\0';
            this.txtSearch.PlaceholderForeColor = Color.Gray;
            this.txtSearch.PlaceholderText = "🔍 Buscar mod...";
            this.txtSearch.SelectedText = "";
            this.txtSearch.ShadowDecoration.CustomizableEdges = customizableEdges4;
            this.txtSearch.Size = new Size(300, 40);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new EventHandler(this.TxtSearch_TextChanged);

            // btnLoad
            this.btnLoad.BorderColor = Color.Green;
            this.btnLoad.BorderRadius = 8;
            this.btnLoad.BorderThickness = 1;
            this.btnLoad.CustomizableEdges = customizableEdges5;
            this.btnLoad.FillColor = Color.FromArgb(35, 35, 35);
            this.btnLoad.Font = new Font("Segoe UI", 10F);
            this.btnLoad.ForeColor = Color.White;
            this.btnLoad.HoverState.FillColor = Color.Green;
            this.btnLoad.Location = new Point(340, 20);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.ShadowDecoration.CustomizableEdges = customizableEdges6;
            this.btnLoad.Size = new Size(140, 40);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "➕ Carregar Mod";
            this.btnLoad.Click += new EventHandler(this.BtnLoad_Click);

            // btnUnload
            this.btnUnload.BorderColor = Color.Green;
            this.btnUnload.BorderRadius = 8;
            this.btnUnload.BorderThickness = 1;
            this.btnUnload.CustomizableEdges = customizableEdges7;
            this.btnUnload.FillColor = Color.FromArgb(35, 35, 35);
            this.btnUnload.Font = new Font("Segoe UI", 10F);
            this.btnUnload.ForeColor = Color.White;
            this.btnUnload.HoverState.FillColor = Color.DarkRed;
            this.btnUnload.Location = new Point(490, 20);
            this.btnUnload.Name = "btnUnload";
            this.btnUnload.ShadowDecoration.CustomizableEdges = customizableEdges8;
            this.btnUnload.Size = new Size(140, 40);
            this.btnUnload.TabIndex = 2;
            this.btnUnload.Text = "➖ Descarregar";
            this.btnUnload.Click += new EventHandler(this.BtnUnload_Click);

            // btnScanFolder
            this.btnScanFolder.BorderColor = Color.Green;
            this.btnScanFolder.BorderRadius = 8;
            this.btnScanFolder.BorderThickness = 1;
            this.btnScanFolder.CustomizableEdges = customizableEdges9;
            this.btnScanFolder.FillColor = Color.FromArgb(35, 35, 35);
            this.btnScanFolder.Font = new Font("Segoe UI", 10F);
            this.btnScanFolder.ForeColor = Color.White;
            this.btnScanFolder.HoverState.FillColor = Color.Green;
            this.btnScanFolder.Location = new Point(640, 20);
            this.btnScanFolder.Name = "btnScanFolder";
            this.btnScanFolder.ShadowDecoration.CustomizableEdges = customizableEdges10;
            this.btnScanFolder.Size = new Size(140, 40);
            this.btnScanFolder.TabIndex = 3;
            this.btnScanFolder.Text = "🔄 Escanear";
            this.btnScanFolder.Click += new EventHandler(this.BtnScanFolder_Click);

            // btnReloadAll
            this.btnReloadAll.BorderColor = Color.Green;
            this.btnReloadAll.BorderRadius = 8;
            this.btnReloadAll.BorderThickness = 1;
            this.btnReloadAll.CustomizableEdges = customizableEdges9;
            this.btnReloadAll.FillColor = Color.FromArgb(35, 35, 35);
            this.btnReloadAll.Font = new Font("Segoe UI", 10F);
            this.btnReloadAll.ForeColor = Color.White;
            this.btnReloadAll.HoverState.FillColor = Color.Orange;
            this.btnReloadAll.Location = new Point(790, 20);
            this.btnReloadAll.Name = "btnReloadAll";
            this.btnReloadAll.ShadowDecoration.CustomizableEdges = customizableEdges10;
            this.btnReloadAll.Size = new Size(140, 40);
            this.btnReloadAll.TabIndex = 4;
            this.btnReloadAll.Text = "🔃 Recarregar Tudo";
            this.btnReloadAll.Click += new EventHandler(this.BtnReloadAll_Click);

            // dgvMods
            this.dgvMods.AllowUserToAddRows = false;
            this.dgvMods.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(35, 35, 35);
            dataGridViewCellStyle1.ForeColor = Color.White;
            this.dgvMods.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMods.BackgroundColor = Color.FromArgb(25, 25, 25);
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(0, 100, 0);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(0, 100, 0);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            this.dgvMods.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMods.ColumnHeadersHeight = 40;
            this.dgvMods.Columns.AddRange(new DataGridViewColumn[] {
                this.colName,
                this.colType,
                this.colSize,
                this.colLoadTime,
                this.colStatus,
                this.colID});
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(25, 25, 25);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.White;
            dataGridViewCellStyle3.SelectionBackColor = Color.Green;
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            this.dgvMods.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvMods.Dock = DockStyle.Fill;
            this.dgvMods.GridColor = Color.FromArgb(50, 50, 50);
            this.dgvMods.Location = new Point(0, 80);
            this.dgvMods.Name = "dgvMods";
            this.dgvMods.ReadOnly = true;
            this.dgvMods.RowHeadersVisible = false;
            this.dgvMods.RowTemplate.Height = 35;
            this.dgvMods.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvMods.Size = new Size(950, 568);
            this.dgvMods.TabIndex = 1;
            this.dgvMods.ThemeStyle.AlternatingRowsStyle.BackColor = Color.FromArgb(35, 35, 35);
            this.dgvMods.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvMods.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            this.dgvMods.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            this.dgvMods.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            this.dgvMods.ThemeStyle.BackColor = Color.FromArgb(25, 25, 25);
            this.dgvMods.ThemeStyle.GridColor = Color.FromArgb(50, 50, 50);
            this.dgvMods.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(0, 100, 0);
            this.dgvMods.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dgvMods.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 10F);
            this.dgvMods.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            this.dgvMods.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvMods.ThemeStyle.HeaderStyle.Height = 40;
            this.dgvMods.ThemeStyle.ReadOnly = true;
            this.dgvMods.ThemeStyle.RowsStyle.BackColor = Color.FromArgb(25, 25, 25);
            this.dgvMods.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvMods.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            this.dgvMods.ThemeStyle.RowsStyle.ForeColor = Color.White;
            this.dgvMods.ThemeStyle.RowsStyle.Height = 35;
            this.dgvMods.ThemeStyle.RowsStyle.SelectionBackColor = Color.Green;
            this.dgvMods.ThemeStyle.RowsStyle.SelectionForeColor = Color.White;

            // colName
            this.colName.HeaderText = "Nome";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.Width = 250;

            // colType
            this.colType.HeaderText = "Tipo";
            this.colType.Name = "colType";
            this.colType.ReadOnly = true;
            this.colType.Width = 100;

            // colSize
            this.colSize.HeaderText = "Tamanho";
            this.colSize.Name = "colSize";
            this.colSize.ReadOnly = true;
            this.colSize.Width = 100;

            // colLoadTime
            this.colLoadTime.HeaderText = "Data/Hora";
            this.colLoadTime.Name = "colLoadTime";
            this.colLoadTime.ReadOnly = true;
            this.colLoadTime.Width = 180;

            // colStatus
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.Width = 150;

            // colID
            this.colID.HeaderText = "ID";
            this.colID.Name = "colID";
            this.colID.ReadOnly = true;
            this.colID.Visible = false;

            // ModsControl
            this.Controls.Add(this.dgvMods);
            this.Controls.Add(this.panelTop);
            this.Name = "ModsControl";
            this.Size = new Size(950, 648);
            this.panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMods)).EndInit();
            this.ResumeLayout(false);
        }

        private Guna2Panel panelTop;
        private Guna2TextBox txtSearch;
        private Guna2Button btnLoad;
        private Guna2Button btnUnload;
        private Guna2Button btnScanFolder;
        private Guna2Button btnReloadAll;
        private Guna2DataGridView dgvMods;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colType;
        private DataGridViewTextBoxColumn colSize;
        private DataGridViewTextBoxColumn colLoadTime;
        private DataGridViewTextBoxColumn colStatus;
        private DataGridViewTextBoxColumn colID;

        #endregion
    }
}
