namespace GTAVModManager.UserControlers
{
    public partial class ConfigControl : UserControl
    {
        public ConfigControl()
        {
            InitializeComponent();
        }
        public bool AutoRefreshEnabled
        {
            get { return swAutoRefresh.Checked; }
            set { swAutoRefresh.Checked = value; }
        }

        private void SwAutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            // Evento será conectado no form principal
        }

        public event EventHandler AutoRefreshChanged
        {
            add { swAutoRefresh.CheckedChanged += value; }
            remove { swAutoRefresh.CheckedChanged -= value; }
        }

    }
}
