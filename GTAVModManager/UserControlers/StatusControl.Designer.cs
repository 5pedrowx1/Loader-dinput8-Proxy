using Guna.UI2.WinForms;

namespace GTAVModManager.UserControlers
{
    partial class StatusControl
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

            this.lblStatus = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.progressStatus = new Guna.UI2.WinForms.Guna2ProgressBar();
            this.panelMain = new Guna.UI2.WinForms.Guna2Panel();

            this.panelMain.SuspendLayout();
            this.SuspendLayout();

            // panelMain
            this.panelMain.Controls.Add(this.lblStatus);
            this.panelMain.Controls.Add(this.progressStatus);
            this.panelMain.CustomizableEdges = customizableEdges1;
            this.panelMain.Dock = DockStyle.Fill;
            this.panelMain.Location = new Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new Padding(20);
            this.panelMain.ShadowDecoration.CustomizableEdges = customizableEdges2;
            this.panelMain.Size = new Size(950, 648);
            this.panelMain.TabIndex = 0;

            // progressStatus
            this.progressStatus.BorderRadius = 8;
            this.progressStatus.CustomizableEdges = customizableEdges3;
            this.progressStatus.FillColor = Color.FromArgb(35, 35, 35);
            this.progressStatus.Location = new Point(20, 20);
            this.progressStatus.Name = "progressStatus";
            this.progressStatus.ProgressColor = Color.Green;
            this.progressStatus.ProgressColor2 = Color.Lime;
            this.progressStatus.ShadowDecoration.CustomizableEdges = customizableEdges4;
            this.progressStatus.Size = new Size(910, 30);
            this.progressStatus.TabIndex = 0;

            // lblStatus
            this.lblStatus.BackColor = Color.Transparent;
            this.lblStatus.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblStatus.ForeColor = Color.White;
            this.lblStatus.Location = new Point(20, 70);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(910, 550);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "📊 Estatísticas do Sistema\n\nCarregando...";

            // StatusControl
            this.Controls.Add(this.panelMain);
            this.Name = "StatusControl";
            this.Size = new Size(950, 648);
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private Guna2Panel panelMain;
        private Guna2HtmlLabel lblStatus;
        private Guna2ProgressBar progressStatus;

        #endregion
    }
}
