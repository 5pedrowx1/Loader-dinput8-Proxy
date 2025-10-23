using GTAVModManager.Models;
using GTAVModManager.Services;
using System.Text.Json;

namespace GTAVModManager.UserControls
{
    public partial class ModsControl : UserControl
    {
        private readonly ModLoaderClient _client;
        private ModsResponse? _currentMods;

        public ModsControl()
        {
            InitializeComponent();
            _client = new ModLoaderClient();
        }

        private async void ModsControl_Load(object sender, EventArgs e)
        {
            await ConnectAndRefresh();
            _refreshTimer.Start();
        }

        private void ModsTable_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var mod = modsTable.Rows[e.RowIndex].DataBoundItem as ModInfo;
            if (mod == null) return;

            if (e.ColumnIndex == modsTable.Columns["colStatus"]?.Index)
            {
                e.Value = mod.Loaded ? "✓ Loaded" : "✗ Unloaded";
                e.CellStyle.ForeColor = mod.Loaded
                    ? Color.FromArgb(0, 255, 135)
                    : Color.FromArgb(255, 77, 77);
                e.CellStyle.Font = new Font(e.CellStyle.Font?.FontFamily ?? FontFamily.GenericSansSerif, 9, FontStyle.Bold);
            }

            if (e.ColumnIndex == modsTable.Columns["colType"]?.Index)
            {
                switch (mod.Type.ToLower())
                {
                    case "scripthook":
                        e.CellStyle.ForeColor = Color.FromArgb(255, 204, 0);
                        break;
                    case "dotnet":
                        e.CellStyle.ForeColor = Color.FromArgb(0, 204, 255);
                        break;
                    case "mod":
                        e.CellStyle.ForeColor = Color.FromArgb(0, 255, 135);
                        break;
                    default:
                        e.CellStyle.ForeColor = Color.FromArgb(180, 180, 180);
                        break;
                }
            }
        }

        private void ModsTable_SelectionChanged(object? sender, EventArgs e)
        {
            bool hasSelection = modsTable.SelectedRows.Count > 0;
            btnRemove.Enabled = hasSelection;

            if (hasSelection)
            {
                var selectedMod = modsTable.SelectedRows[0].DataBoundItem as ModInfo;
                if (selectedMod != null &&
                    (selectedMod.Type == "scripthook" || selectedMod.Type == "dotnet"))
                {
                    btnRemove.Enabled = false;
                }
            }
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

        private async void RefreshModsList(object? sender, EventArgs e)
        {
            await RefreshModsList();
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
                    string? selectedModId = null;
                    if (modsTable.SelectedRows.Count > 0)
                    {
                        var selectedMod = modsTable.SelectedRows[0].DataBoundItem as ModInfo;
                        selectedModId = selectedMod?.Id;
                    }

                    modsTable.DataSource = null;
                    modsTable.DataSource = _currentMods.Mods;
                    lblTotalMods.Text = $"Total: {_currentMods.Count} Mods";

                    if (!string.IsNullOrEmpty(selectedModId))
                    {
                        foreach (DataGridViewRow row in modsTable.Rows)
                        {
                            if (row.DataBoundItem is ModInfo mod && mod.Id == selectedModId)
                            {
                                row.Selected = true;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error refreshing mod list: {ex.Message}");
            }
        }

        private async void AddMod(object? sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog
            {
                Filter = "Mod Files (*.dll;*.asi)|*.dll;*.asi|DLL Files (*.dll)|*.dll|ASI Files (*.asi)|*.asi|All Files (*.*)|*.*",
                Title = "Select Mod File",
                Multiselect = false,
                CheckFileExists = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    btnAdd.Enabled = false;
                    btnAdd.Text = "Loading...";

                    var result = await _client.LoadModAsync(openFileDialog.FileName);

                    if (result == "SUCCESS")
                    {
                        MessageBox.Show($"Mod loaded successfully!\n\nFile: {Path.GetFileName(openFileDialog.FileName)}",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await Task.Delay(500);
                        await RefreshModsList();
                    }
                    else
                    {
                        MessageBox.Show($"Failed to load mod.\n\nFile: {Path.GetFileName(openFileDialog.FileName)}\n\nCheck the logs for more details.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading mod: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    btnAdd.Enabled = true;
                    btnAdd.Text = "📁 Add Mod";
                }
            }
        }

        private async void RemoveMod(object? sender, EventArgs e)
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
                MessageBox.Show("Cannot unload ScriptHook modules.\n\nThese are critical components and must remain loaded.",
                    "Protected Mod", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show(
                $"Are you sure you want to unload the following mod?\n\n" +
                $"Name: {selectedMod.Name}\n" +
                $"Type: {selectedMod.Type}\n" +
                $"Size: {selectedMod.SizeFormatted}\n\n" +
                $"This action cannot be undone without reloading the mod.",
                "Confirm Unload",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    btnRemove.Enabled = false;
                    btnRemove.Text = "Unloading...";

                    var result = await _client.UnloadModAsync(selectedMod.Id);

                    if (result == "SUCCESS")
                    {
                        MessageBox.Show($"Mod unloaded successfully!\n\nName: {selectedMod.Name}",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await Task.Delay(500);
                        await RefreshModsList();
                    }
                    else
                    {
                        MessageBox.Show($"Failed to unload mod.\n\nName: {selectedMod.Name}\n\nThe mod may be in use or protected.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error unloading mod: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    btnRemove.Enabled = true;
                    btnRemove.Text = "🗑️ Remove";
                }
            }
        }

        private async void ReloadAll(object? sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(
                "This will reload all non-critical mods. Continue?\n\n" +
                "ScriptHook modules will not be affected.\n" +
                "This may take a few seconds.",
                "Confirm Reload All",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    btnReload.Enabled = false;
                    btnAdd.Enabled = false;
                    btnRemove.Enabled = false;
                    btnReload.Text = "Reloading...";

                    var result = await _client.ReloadAllAsync();

                    if (result.StartsWith("SUCCESS"))
                    {
                        var stats = result.Split(':').LastOrDefault() ?? "0/0";
                        MessageBox.Show(
                            $"Mods reloaded successfully!\n\n" +
                            $"Statistics: {stats}\n\n" +
                            $"Check the logs for detailed information.",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await Task.Delay(1000);
                        await RefreshModsList();
                    }
                    else
                    {
                        MessageBox.Show("Failed to reload mods.\n\nCheck the logs for more details.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    btnAdd.Enabled = true;
                    btnRemove.Enabled = true;
                    btnReload.Text = "🔄 Reload All";
                }
            }
        }
    }
}