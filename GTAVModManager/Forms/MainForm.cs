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
    }
}
