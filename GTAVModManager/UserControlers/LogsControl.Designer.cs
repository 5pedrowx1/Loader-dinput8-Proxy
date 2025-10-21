using Guna.UI2.WinForms;

namespace GTAVModManager.UserControlers
{
    partial class LogsControl
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panelTop = new Guna2Panel();
            btnClearLogs = new Guna2Button();
            txtLogs = new Guna2TextBox();
            panelTop.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.Controls.Add(btnClearLogs);
            panelTop.CustomizableEdges = customizableEdges3;
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.ShadowDecoration.CustomizableEdges = customizableEdges4;
            panelTop.Size = new Size(950, 60);
            panelTop.TabIndex = 0;
            // 
            // btnClearLogs
            // 
            btnClearLogs.BorderColor = Color.Green;
            btnClearLogs.BorderRadius = 8;
            btnClearLogs.BorderThickness = 1;
            btnClearLogs.CustomizableEdges = customizableEdges1;
            btnClearLogs.FillColor = Color.FromArgb(35, 35, 35);
            btnClearLogs.Font = new Font("Segoe UI", 10F);
            btnClearLogs.ForeColor = Color.White;
            btnClearLogs.HoverState.FillColor = Color.DarkRed;
            btnClearLogs.Location = new Point(0, 11);
            btnClearLogs.Name = "btnClearLogs";
            btnClearLogs.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnClearLogs.Size = new Size(150, 40);
            btnClearLogs.TabIndex = 0;
            btnClearLogs.Text = "🗑️ Limpar Logs";
            btnClearLogs.Click += BtnClearLogs_Click;
            // 
            // txtLogs
            // 
            txtLogs.BorderColor = Color.Green;
            txtLogs.BorderRadius = 8;
            txtLogs.CustomizableEdges = customizableEdges5;
            txtLogs.DefaultText = "";
            txtLogs.Dock = DockStyle.Fill;
            txtLogs.FillColor = Color.FromArgb(25, 25, 25);
            txtLogs.Font = new Font("Consolas", 9F);
            txtLogs.ForeColor = Color.White;
            txtLogs.Location = new Point(0, 60);
            txtLogs.Multiline = true;
            txtLogs.Name = "txtLogs";
            txtLogs.PlaceholderText = "";
            txtLogs.ReadOnly = true;
            txtLogs.ScrollBars = ScrollBars.Vertical;
            txtLogs.SelectedText = "";
            txtLogs.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtLogs.Size = new Size(950, 588);
            txtLogs.TabIndex = 1;
            // 
            // LogsControl
            // 
            Controls.Add(txtLogs);
            Controls.Add(panelTop);
            Name = "LogsControl";
            Size = new Size(950, 648);
            panelTop.ResumeLayout(false);
            ResumeLayout(false);
        }

        private Guna2Panel panelTop;
        private Guna2Button btnClearLogs;
        private Guna2TextBox txtLogs;

        #endregion
    }
}
