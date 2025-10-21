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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigControl));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panelMain = new Guna2Panel();
            lblInfo = new Guna2HtmlLabel();
            lblDarkMode = new Guna2HtmlLabel();
            swDarkMode = new Guna2ToggleSwitch();
            lblAutoRefresh = new Guna2HtmlLabel();
            swAutoRefresh = new Guna2ToggleSwitch();
            lblTitle = new Guna2HtmlLabel();
            panelMain.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.Controls.Add(lblInfo);
            panelMain.Controls.Add(lblDarkMode);
            panelMain.Controls.Add(swDarkMode);
            panelMain.Controls.Add(lblAutoRefresh);
            panelMain.Controls.Add(swAutoRefresh);
            panelMain.Controls.Add(lblTitle);
            panelMain.CustomizableEdges = customizableEdges5;
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(20);
            panelMain.ShadowDecoration.CustomizableEdges = customizableEdges6;
            panelMain.Size = new Size(950, 648);
            panelMain.TabIndex = 0;
            // 
            // lblInfo
            // 
            lblInfo.BackColor = Color.Transparent;
            lblInfo.Font = new Font("Segoe UI", 10F);
            lblInfo.ForeColor = Color.Gray;
            lblInfo.Location = new Point(20, 200);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(1950, 19);
            lblInfo.TabIndex = 5;
            lblInfo.Text = resources.GetString("lblInfo.Text");
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
            swDarkMode.CustomizableEdges = customizableEdges1;
            swDarkMode.Location = new Point(20, 130);
            swDarkMode.Name = "swDarkMode";
            swDarkMode.ShadowDecoration.CustomizableEdges = customizableEdges2;
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
            swAutoRefresh.CustomizableEdges = customizableEdges3;
            swAutoRefresh.Location = new Point(20, 80);
            swAutoRefresh.Name = "swAutoRefresh";
            swAutoRefresh.ShadowDecoration.CustomizableEdges = customizableEdges4;
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
        private Guna2HtmlLabel lblInfo;

        #endregion
    }
}
