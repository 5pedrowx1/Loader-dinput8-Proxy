using GTAVModManager.Logics;
using GTAVModManager.UserControlers;
using Newtonsoft.Json;
using System.IO.Pipes;
using System.Text;

namespace GTAVModManager.Forms
{
    public partial class GTAVModManager : Form
    {
        private NamedPipeClientStream pipeClient;
        private const string PIPE_NAME = "GTAVModLoader";

        private ModsControl modsControl;
        private StatusControl statusControl;
        private LogsControl logsControl;
        private ConfigControl configControl;
        private bool isVisible = true;

        public GTAVModManager()
        {
            InitializeComponent();
            InitializeControls();
            ConnectToPipe();
            refreshTimer.Start();
            this.KeyPreview = true;
        }

        private void InitializeControls()
        {
            modsControl = new ModsControl(this);
            statusControl = new StatusControl();
            logsControl = new LogsControl();
            configControl = new ConfigControl();

            modsControl.Dock = DockStyle.Fill;
            statusControl.Dock = DockStyle.Fill;
            logsControl.Dock = DockStyle.Fill;
            configControl.Dock = DockStyle.Fill;

            MainPanel.Controls.Add(modsControl);
            MainPanel.Controls.Add(statusControl);
            MainPanel.Controls.Add(logsControl);
            MainPanel.Controls.Add(configControl);

            modsControl.Visible = true;
            statusControl.Visible = false;
            logsControl.Visible = false;
            configControl.Visible = false;

            configControl.AutoRefreshChanged += ConfigControl_AutoRefreshChanged;
        }

        private void ConfigControl_AutoRefreshChanged(object sender, EventArgs e)
        {
            if (configControl.AutoRefreshEnabled)
            {
                refreshTimer.Start();
            }
            else
            {
                refreshTimer.Stop();
            }
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            RefreshDataAsync();
        }

        private async void ConnectToPipe()
        {
            try
            {
                pipeClient = new NamedPipeClientStream(".", PIPE_NAME, PipeDirection.InOut);
                await pipeClient.ConnectAsync(5000);
                logsControl.AddLog("Conectado ao loader", "SUCCESS");
                await RefreshData();
            }
            catch (Exception ex)
            {
                logsControl.AddLog($"Erro ao conectar: {ex.Message}", "ERROR");
                MessageBox.Show("Não foi possível conectar ao loader. Certifique-se que o jogo está rodando.",
                    "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<string> SendCommand(string command, string data = "")
        {
            try
            {
                if (pipeClient == null || !pipeClient.IsConnected)
                {
                    pipeClient = new NamedPipeClientStream(".", PIPE_NAME, PipeDirection.InOut);
                    await pipeClient.ConnectAsync(5000);
                }

                byte[] buffer = new byte[544];
                Encoding.UTF8.GetBytes(command).CopyTo(buffer, 0);
                Encoding.UTF8.GetBytes(data).CopyTo(buffer, 32);

                await pipeClient.WriteAsync(buffer, 0, buffer.Length);

                byte[] responseBuffer = new byte[8192];
                int bytesRead = await pipeClient.ReadAsync(responseBuffer, 0, responseBuffer.Length);

                return Encoding.UTF8.GetString(responseBuffer, 0, bytesRead);
            }
            catch (Exception ex)
            {
                logsControl.AddLog($"Erro ao enviar comando: {ex.Message}", "ERROR");
                return null;
            }
        }

        private async void RefreshDataAsync()
        {
            await RefreshData();
        }

        private async Task RefreshData()
        {
            await LoadModsAsync();
            await LoadStatusAsync();
            await LoadLogsAsync();
        }

        private async Task LoadModsAsync()
        {
            string response = await SendCommand("GET_MODS");
            if (string.IsNullOrEmpty(response)) return;

            try
            {
                var modsData = JsonConvert.DeserializeObject<ModsResponse>(response);
                modsControl.UpdateModsList(modsData);
            }
            catch (Exception ex)
            {
                logsControl.AddLog($"Erro ao processar mods: {ex.Message}", "ERROR");
            }
        }

        private async Task LoadStatusAsync()
        {
            string response = await SendCommand("GET_STATUS");
            if (string.IsNullOrEmpty(response)) return;

            try
            {
                var status = JsonConvert.DeserializeObject<StatusResponse>(response);
                statusControl.UpdateStatus(status);
            }
            catch { }
        }

        private async Task LoadLogsAsync()
        {
            string response = await SendCommand("GET_LOGS");
            if (string.IsNullOrEmpty(response)) return;

            try
            {
                var logsData = JsonConvert.DeserializeObject<LogsResponse>(response);
                logsControl.UpdateLogs(logsData);
            }
            catch { }
        }

        public async void LoadModFromFile()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Mod Files|*.dll;*.asi";
                ofd.Title = "Selecione um mod para carregar";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string result = await SendCommand("LOAD_MOD", ofd.FileName);
                    MessageBox.Show(
                        result == "SUCCESS" ? "Mod carregado com sucesso!" : "Falha ao carregar mod",
                        "Resultado",
                        MessageBoxButtons.OK,
                        result == "SUCCESS" ? MessageBoxIcon.Information : MessageBoxIcon.Error
                    );
                    await RefreshData();
                }
            }
        }

        public async void UnloadSelectedMod()
        {
            string modId = modsControl.GetSelectedModID();

            if (string.IsNullOrEmpty(modId))
            {
                MessageBox.Show("Selecione um mod para descarregar", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Deseja realmente descarregar este mod?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                string result = await SendCommand("UNLOAD_MOD", modId);
                MessageBox.Show(
                    result == "SUCCESS" ? "Mod descarregado com sucesso!" : "Falha ao descarregar mod",
                    "Resultado",
                    MessageBoxButtons.OK,
                    result == "SUCCESS" ? MessageBoxIcon.Information : MessageBoxIcon.Error
                );
                await RefreshData();
            }
        }

        public async void ScanModsFolder()
        {
            await SendCommand("SCAN_FOLDER");
            await Task.Delay(1000);
            await RefreshData();
            MessageBox.Show("Pasta de mods escaneada!", "Sucesso",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public async void ReloadAllMods()
        {
            DialogResult confirm = MessageBox.Show(
                "Deseja recarregar todos os mods? Isso pode causar instabilidade.",
                "Confirmar Recarga",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm == DialogResult.Yes)
            {
                string result = await SendCommand("RELOAD_ALL");
                await Task.Delay(2000);
                await RefreshData();

                MessageBox.Show(
                    result.StartsWith("SUCCESS") ? $"Mods recarregados!\n{result.Split(':')[1]}" : "Falha ao recarregar",
                    "Resultado",
                    MessageBoxButtons.OK,
                    result.StartsWith("SUCCESS") ? MessageBoxIcon.Information : MessageBoxIcon.Error
                );
            }
        }

        private void GTAVModManager_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                ToggleVisibility();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }

        private void ToggleVisibility()
        {
            if (isVisible)
            {
                this.Hide();
                isVisible = false;
            }
            else
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.BringToFront();
                isVisible = true;
            }
        }

        private void BtnMods_Click(object sender, EventArgs e)
        {
            ShowControl(0);
        }

        private void BtnStatus_Click(object sender, EventArgs e)
        {
            ShowControl(1);
        }

        private void BtnLogs_Click(object sender, EventArgs e)
        {
            ShowControl(2);
        }

        private void BtnConfig_Click(object sender, EventArgs e)
        {
            ShowControl(3);
        }

        private void ShowControl(int index)
        {
            modsControl.Visible = index == 0;
            statusControl.Visible = index == 1;
            logsControl.Visible = index == 2;
            configControl.Visible = index == 3;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Deseja realmente fechar o Mod Manager?",
                "Confirmar Saída",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void GTAVModManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            refreshTimer?.Stop();
            refreshTimer?.Dispose();

            if (pipeClient != null)
            {
                pipeClient.Close();
                pipeClient.Dispose();
            }
        }
    }
}