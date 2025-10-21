using GTAVModManager.Logics;

namespace GTAVModManager.UserControlers
{
    public partial class ModsControl : UserControl
    {
        private GTAVModManager.Forms.GTAVModManager mainForm;

        public ModsControl(GTAVModManager.Forms.GTAVModManager form)
        {
            InitializeComponent();
            mainForm = form;
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string filter = txtSearch.Text.ToLower();
            foreach (DataGridViewRow row in dgvMods.Rows)
            {
                if (row.Cells["colName"].Value != null)
                {
                    row.Visible = string.IsNullOrEmpty(filter) ||
                        row.Cells["colName"].Value.ToString().ToLower().Contains(filter);
                }
            }
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            mainForm.LoadModFromFile();
        }

        private void BtnUnload_Click(object sender, EventArgs e)
        {
            mainForm.UnloadSelectedMod();
        }

        private void BtnScanFolder_Click(object sender, EventArgs e)
        {
            mainForm.ScanModsFolder();
        }

        private void BtnReloadAll_Click(object sender, EventArgs e)
        {
            mainForm.ReloadAllMods();
        }

        public void UpdateModsList(ModsResponse modsData)
        {
            dgvMods.Rows.Clear();
            foreach (var mod in modsData.Mods)
            {
                dgvMods.Rows.Add(
                    mod.Name,
                    mod.Type,
                    $"{mod.Size / 1024} KB",
                    mod.LoadTime,
                    mod.Loaded ? "✅ Carregado" : "❌ Erro",
                    mod.ID
                );
            }
        }

        public string GetSelectedModID()
        {
            if (dgvMods.SelectedRows.Count > 0)
            {
                return dgvMods.SelectedRows[0].Cells["colID"].Value?.ToString();
            }
            return null;
        }
    }
}
