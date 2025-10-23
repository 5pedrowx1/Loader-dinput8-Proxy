using GTAVModManager.UserControls;

namespace GTAVModManager.Forms
{
    public partial class MainForm : Form
    {
        private DashboardControl? dashboardControl;
        private ModsControl? modsControl;
        private PerformanceControl? performanceControl;
        private LogsControl? logsControl;
        private SettingsControl? settingsControl;

        public MainForm()
        {
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            dashboardControl = new DashboardControl();
            modsControl = new ModsControl();
            performanceControl = new PerformanceControl();
            logsControl = new LogsControl();
            settingsControl = new SettingsControl();

            dashboardControl.Dock = DockStyle.Fill;
            modsControl.Dock = DockStyle.Fill;
            performanceControl.Dock = DockStyle.Fill;
            logsControl.Dock = DockStyle.Fill;
            settingsControl.Dock = DockStyle.Fill;

            PanelTabs.Controls.Add(dashboardControl);
            PanelTabs.Controls.Add(modsControl);
            PanelTabs.Controls.Add(performanceControl);
            PanelTabs.Controls.Add(logsControl);
            PanelTabs.Controls.Add(settingsControl);

            dashboardControl.Visible = true;
            modsControl.Visible = false;
            performanceControl.Visible = false;
            logsControl.Visible = false;
            settingsControl.Visible = false;
        }

        private void ShowControl(int index)
        {
            dashboardControl!.Visible = index == 0;
            modsControl!.Visible = index == 1;
            performanceControl!.Visible = index == 2;
            logsControl!.Visible = index == 3;
            settingsControl!.Visible = index == 4;
        }

        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            ShowControl(0);
        }

        private void BtnMods_Click(object sender, EventArgs e)
        {
            ShowControl(1);
        }

        private void BtnPerformance_Click(object sender, EventArgs e)
        {
            ShowControl(2);
        }

        private void BtnLogs_Click(object sender, EventArgs e)
        {
            ShowControl(3);
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            ShowControl(4);
        }

        private async void BtnMinimize_Click(object sender, EventArgs e)
        {
            var originalSize = this.Size;
            var originalLocation = this.Location;

            for (int step = 0; step < 6; step++)
            {
                this.Size = new Size(this.Width - 20, this.Height - 10);
                this.Location = new Point(this.Location.X + 10, this.Location.Y + 5);
                this.Opacity -= 0.1;
                await Task.Delay(20);
            }

            WindowState = FormWindowState.Minimized;
            this.Size = originalSize;
            this.Location = originalLocation;
            this.Opacity = 1;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (settingsControl.HasUnsavedChanges)
            {
                var result = MessageBox.Show(
                    "You have unsaved changes in the settings.\n\n" +
                    "Do you want to save before closing?",
                    "Unsaved Changes",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        settingsControl.SaveSettingsSync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving settings:\n\n{ex.Message}",
                            "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

    }
}
