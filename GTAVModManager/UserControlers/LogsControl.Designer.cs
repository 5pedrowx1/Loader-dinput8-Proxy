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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();

            this.panelTop = new Guna.UI2.WinForms.Guna2Panel();
            this.btnClearLogs = new Guna.UI2.WinForms.Guna2Button();
            this.txtLogs = new Guna.UI2.WinForms.Guna2TextBox();

            this.panelTop.SuspendLayout();
            this.SuspendLayout();

            // panelTop
            this.panelTop.Controls.Add(this.btnClearLogs);
            this.panelTop.CustomizableEdges = customizableEdges1;
            this.panelTop.Dock = DockStyle.Top;
            this.panelTop.Location = new Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.ShadowDecoration.CustomizableEdges = customizableEdges2;
            this.panelTop.Size = new Size(950, 60);
            this.panelTop.TabIndex = 0;

            // btnClearLogs
            this.btnClearLogs.BorderColor = Color.Green;
            this.btnClearLogs.BorderRadius = 8;
            this.btnClearLogs.BorderThickness = 1;
            this.btnClearLogs.CustomizableEdges = customizableEdges3;
            this.btnClearLogs.FillColor = Color.FromArgb(35, 35, 35);
            this.btnClearLogs.Font = new Font("Segoe UI", 10F);
            this.btnClearLogs.ForeColor = Color.White;
            this.btnClearLogs.HoverState.FillColor = Color.DarkRed;
            this.btnClearLogs.Location = new Point(20, 10);
            this.btnClearLogs.Name = "btnClearLogs";
            this.btnClearLogs.ShadowDecoration.CustomizableEdges = customizableEdges4;
            this.btnClearLogs.Size = new Size(150, 40);
            this.btnClearLogs.TabIndex = 0;
            this.btnClearLogs.Text = "🗑️ Limpar Logs";
            this.btnClearLogs.Click += new EventHandler(this.BtnClearLogs_Click);

            // txtLogs
            this.txtLogs.BorderColor = Color.Green;
            this.txtLogs.BorderRadius = 8;
            this.txtLogs.CustomizableEdges = customizableEdges5;
            this.txtLogs.DefaultText = "";
            this.txtLogs.Dock = DockStyle.Fill;
            this.txtLogs.FillColor = Color.FromArgb(25, 25, 25);
            this.txtLogs.Font = new Font("Consolas", 9F);
            this.txtLogs.ForeColor = Color.White;
            this.txtLogs.Location = new Point(0, 60);
            this.txtLogs.Multiline = true;
            this.txtLogs.Name = "txtLogs";
            this.txtLogs.PasswordChar = '\0';
            this.txtLogs.PlaceholderText = "";
            this.txtLogs.ReadOnly = true;
            this.txtLogs.ScrollBars = ScrollBars.Vertical;
            this.txtLogs.SelectedText = "";
            this.txtLogs.ShadowDecoration.CustomizableEdges = customizableEdges6;
            this.txtLogs.Size = new Size(950, 588);
            this.txtLogs.TabIndex = 1;

            // LogsControl
            this.Controls.Add(this.txtLogs);
            this.Controls.Add(this.panelTop);
            this.Name = "LogsControl";
            this.Size = new Size(950, 648);
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private Guna2Panel panelTop;
        private Guna2Button btnClearLogs;
        private Guna2TextBox txtLogs;

        #endregion
    }
}
