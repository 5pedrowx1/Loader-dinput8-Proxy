using Guna.UI2.WinForms;

namespace GTAVModManager.UserControlers
{
    partial class ConfigControl
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigControl));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panelMain = new Guna2Panel();
            lblDarkMode = new Guna2HtmlLabel();
            swDarkMode = new Guna2ToggleSwitch();
            lblAutoRefresh = new Guna2HtmlLabel();
            swAutoRefresh = new Guna2ToggleSwitch();
            lblTitle = new Guna2HtmlLabel();
            txtLogs = new Guna2TextBox();
            panelMain.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.Controls.Add(txtLogs);
            panelMain.Controls.Add(lblDarkMode);
            panelMain.Controls.Add(swDarkMode);
            panelMain.Controls.Add(lblAutoRefresh);
            panelMain.Controls.Add(swAutoRefresh);
            panelMain.Controls.Add(lblTitle);
            panelMain.CustomizableEdges = customizableEdges7;
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(20);
            panelMain.ShadowDecoration.CustomizableEdges = customizableEdges8;
            panelMain.Size = new Size(950, 648);
            panelMain.TabIndex = 0;
            // 
            // lblDarkMode
            // 
            lblDarkMode.BackColor = Color.Transparent;
            lblDarkMode.Font = new Font("Segoe UI", 11F);
            lblDarkMode.ForeColor = Color.White;
            lblDarkMode.Location = new Point(80, 132);
            lblDarkMode.Name = "lblDarkMode";
            lblDarkMode.Size = new Size(139, 22);
            lblDarkMode.TabIndex = 4;
            lblDarkMode.Text = "Modo Escuro (Ativo)";
            // 
            // swDarkMode
            // 
            swDarkMode.Checked = true;
            swDarkMode.CheckedState.BorderColor = Color.Green;
            swDarkMode.CheckedState.FillColor = Color.Green;
            swDarkMode.CheckedState.InnerBorderColor = Color.White;
            swDarkMode.CheckedState.InnerColor = Color.White;
            swDarkMode.CustomizableEdges = customizableEdges3;
            swDarkMode.Location = new Point(20, 130);
            swDarkMode.Name = "swDarkMode";
            swDarkMode.ShadowDecoration.CustomizableEdges = customizableEdges4;
            swDarkMode.Size = new Size(50, 25);
            swDarkMode.TabIndex = 3;
            swDarkMode.UncheckedState.BorderColor = Color.FromArgb(125, 137, 149);
            swDarkMode.UncheckedState.FillColor = Color.FromArgb(125, 137, 149);
            swDarkMode.UncheckedState.InnerBorderColor = Color.White;
            swDarkMode.UncheckedState.InnerColor = Color.White;
            // 
            // lblAutoRefresh
            // 
            lblAutoRefresh.BackColor = Color.Transparent;
            lblAutoRefresh.Font = new Font("Segoe UI", 11F);
            lblAutoRefresh.ForeColor = Color.White;
            lblAutoRefresh.Location = new Point(80, 82);
            lblAutoRefresh.Name = "lblAutoRefresh";
            lblAutoRefresh.Size = new Size(251, 22);
            lblAutoRefresh.TabIndex = 2;
            lblAutoRefresh.Text = "Atualização Automática (5 segundos)";
            // 
            // swAutoRefresh
            // 
            swAutoRefresh.CheckedState.BorderColor = Color.Green;
            swAutoRefresh.CheckedState.FillColor = Color.Green;
            swAutoRefresh.CheckedState.InnerBorderColor = Color.White;
            swAutoRefresh.CheckedState.InnerColor = Color.White;
            swAutoRefresh.CustomizableEdges = customizableEdges5;
            swAutoRefresh.Location = new Point(20, 80);
            swAutoRefresh.Name = "swAutoRefresh";
            swAutoRefresh.ShadowDecoration.CustomizableEdges = customizableEdges6;
            swAutoRefresh.Size = new Size(50, 25);
            swAutoRefresh.TabIndex = 1;
            swAutoRefresh.UncheckedState.BorderColor = Color.FromArgb(125, 137, 149);
            swAutoRefresh.UncheckedState.FillColor = Color.FromArgb(125, 137, 149);
            swAutoRefresh.UncheckedState.InnerBorderColor = Color.White;
            swAutoRefresh.UncheckedState.InnerColor = Color.White;
            swAutoRefresh.CheckedChanged += SwAutoRefresh_CheckedChanged;
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.Lime;
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(180, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "⚙️ Configurações";
            // 
            // txtLogs
            // 
            txtLogs.BorderColor = Color.Green;
            txtLogs.BorderRadius = 8;
            txtLogs.CustomizableEdges = customizableEdges1;
            txtLogs.DefaultText = resources.GetString("txtLogs.DefaultText");
            txtLogs.FillColor = Color.FromArgb(25, 25, 25);
            txtLogs.Font = new Font("Consolas", 9F);
            txtLogs.ForeColor = Color.White;
            txtLogs.Location = new Point(23, 225);
            txtLogs.Multiline = true;
            txtLogs.Name = "txtLogs";
            txtLogs.PlaceholderText = "";
            txtLogs.ReadOnly = true;
            txtLogs.SelectedText = "";
            txtLogs.ShadowDecoration.CustomizableEdges = customizableEdges2;
            txtLogs.Size = new Size(386, 217);
            txtLogs.TabIndex = 7;
            // 
            // ConfigControl
            // 
            Controls.Add(panelMain);
            Name = "ConfigControl";
            Size = new Size(950, 648);
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            ResumeLayout(false);
        }

        private Guna2Panel panelMain;
        private Guna2HtmlLabel lblTitle;
        private Guna2ToggleSwitch swAutoRefresh;
        private Guna2HtmlLabel lblAutoRefresh;
        private Guna2ToggleSwitch swDarkMode;
        private Guna2HtmlLabel lblDarkMode;

        #endregion
        private Guna2TextBox txtLogs;
    }
}
