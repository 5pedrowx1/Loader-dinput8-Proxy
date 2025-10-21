using GTAVModManager.Logics;

namespace GTAVModManager.UserControlers
{
    public partial class StatusControl : UserControl
    {
        public StatusControl()
        {
            InitializeComponent();
        }

        public void UpdateStatus(StatusResponse status)
        {
            progressStatus.Value = Math.Min((status.ModsLoaded * 10), 100);

            lblStatus.Text = $"📊 Estatísticas do Sistema\n\n" +
                $"Versão: {status.Version}\n" +
                $"Tempo ativo: {TimeSpan.FromSeconds(status.UptimeSeconds):hh\\:mm\\:ss}\n" +
                $"Mods carregados: {status.ModsLoaded}\n" +
                $"Servidor: {(status.ServerRunning ? "✅ Online" : "❌ Offline")}\n" +
                $"Jogo detectado: {(status.GameDetected ? "✅ Sim" : "❌ Não")}";
        }
    }
}
