using System.Text.Json;
using GTAVModManager.Models;
using GTAVModManager.Services;

namespace GTAVModManager.UserControls
{
    public partial class ModsControl : UserControl
    {
        private readonly ModLoaderClient _client;
        private ModsResponse? _currentMods;
        private System.Windows.Forms.Timer? _refreshTimer;

        public ModsControl()
        {
            InitializeComponent();
            _client = new ModLoaderClient();

            SetupEventHandlers();

            _refreshTimer = new System.Windows.Forms.Timer { Interval = 2000 };
            _refreshTimer.Tick += async (s, e) => await RefreshModsList();

            Load += async (s, e) =>
            {
                await ConnectAndRefresh();
                _refreshTimer.Start();
            };
        }

        private void SetupEventHandlers()
        {
            btnAdd.Click += async (s, e) => await AddMod();
            btnRemove.Click += async (s, e) => await RemoveMod();
            btnReload.Click += async (s, e) => await ReloadAll();

            modsTable.CellFormatting += (s, e) =>
            {
                if (e.ColumnIndex == modsTable.Columns["colStatus"]?.Index && e.RowIndex >= 0)
                {
                    var mod = modsTable.Rows[e.RowIndex].DataBoundItem as ModInfo;
                    if (mod != null)
                    {
                        e.Value = mod.Loaded ? "✓ Loaded" : "✗ Unloaded";
                        e.CellStyle.ForeColor = mod.Loaded
                            ? Color.FromArgb(0, 255, 135)
                            : Color.FromArgb(255, 77, 77);
                    }
                }
            };
        }

        private async Task ConnectAndRefresh()
        {
            try
            {
                await _client.ConnectAsync();
                await RefreshModsList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to connect to mod loader: {ex.Message}",
                    "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task RefreshModsList()
        {
            if (!_client.IsConnected)
            {
                await _client.ConnectAsync();
                return;
            }

            try
            {
                var json = await _client.GetModsAsync();
                if (string.IsNullOrEmpty(json)) return;

                _currentMods = JsonSerializer.Deserialize<ModsResponse>(json);

                if (_currentMods != null)
                {
                    modsTable.DataSource = null;
                    modsTable.DataSource = _currentMods.Mods;
                    lblTotalMods.Text = $"Total: {_currentMods.Count} Mods";
                }
            }
            catch { }
        }

        private async Task AddMod()
        {
            using var openFileDialog = new OpenFileDialog
            {
                Filter = "Mod Files (*.dll;*.asi)|*.dll;*.asi|All Files (*.*)|*.*",
                Title = "Select Mod File",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var result = await _client.LoadModAsync(openFileDialog.FileName);

                    if (result == "SUCCESS")
                    {
                        MessageBox.Show("Mod loaded successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await RefreshModsList();
                    }
                    else
                    {
                        MessageBox.Show("Failed to load mod.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading mod: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task RemoveMod()
        {
            if (modsTable.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a mod to remove.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedMod = modsTable.SelectedRows[0].DataBoundItem as ModInfo;
            if (selectedMod == null) return;

            if (selectedMod.Type == "scripthook" || selectedMod.Type == "dotnet")
            {
                MessageBox.Show("Cannot unload ScriptHook modules.", "Protected Mod",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show(
                $"Are you sure you want to unload '{selectedMod.Name}'?",
                "Confirm Unload",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    var result = await _client.UnloadModAsync(selectedMod.Id);

                    if (result == "SUCCESS")
                    {
                        MessageBox.Show("Mod unloaded successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await RefreshModsList();
                    }
                    else
                    {
                        MessageBox.Show("Failed to unload mod.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error unloading mod: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task ReloadAll()
        {
            var confirmResult = MessageBox.Show(
                "This will reload all non-critical mods. Continue?",
                "Confirm Reload All",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    btnReload.Enabled = false;
                    btnReload.Text = "Reloading...";

                    var result = await _client.ReloadAllAsync();

                    if (result.StartsWith("SUCCESS"))
                    {
                        MessageBox.Show($"Mods reloaded: {result.Split(':').LastOrDefault()}",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await Task.Delay(1000);
                        await RefreshModsList();
                    }
                    else
                    {
                        MessageBox.Show("Failed to reload mods.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error reloading mods: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    btnReload.Enabled = true;
                    btnReload.Text = "🔄 Reload All";
                }
            }
        }
    }
}