using System.Diagnostics;
using System.Reflection;
using System.Resources;


namespace PannelloUI
{
    public partial class AboutFormFireDX : Form
    {
        private FireworksSettingsPanel? _settingsForm;
        private readonly ResourceManager _resources;

        public AboutFormFireDX()
        {
            _resources = new ResourceManager(typeof(AboutFormFireDX));
            InitializeComponent();
            LoadLocalizedStrings();
            InitializeRuntimeHosts();
        }

        /// <summary>
        /// Ottiene una stringa dal ResourceManager, lancia eccezione se non trovata.
        /// </summary>
        private string GetResource(string key)
        {
            return _resources.GetString(key)
                ?? throw new InvalidOperationException($"Missing resource: {key}");
        }

        private void LoadLocalizedStrings()
        {
            var version = Assembly.GetEntryAssembly()?.GetName().Version?.ToString()
                        ?? Assembly.GetExecutingAssembly().GetName().Version?.ToString()
                        ?? "0.0.0";
            // Carica le stringhe dal RESX
            lblVersion.Text = string.Format(GetResource("VersionLabel"), version);
            lblTitle.Text = GetResource("Title");
            lnkRepo.Text = GetResource("OpenRepository");
            btnBurst.Text = GetResource("BurstButton");
            btnGpuInfo.Text = GetResource("GpuInfoButton");
        }

        private void InitializeRuntimeHosts()
        {
            // Host FireworksSettingsPanel nel pannello laterale
            _settingsForm = new FireworksSettingsPanel
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            panelSettingsHost.Controls.Add(_settingsForm);
            _settingsForm.Show();
        }

        private void LnkRepo_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://example.com/fireworksdx",
                    UseShellExecute = true
                });
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                MessageBox.Show(
                    $"Impossibile aprire il browser: {ex.Message}",
                    "Errore",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private void BtnBurst_Click(object? sender, EventArgs e)
        {
            fireworksPanel.LaunchBurst(5);
        }

        private void BtnGpuInfo_Click(object? sender, EventArgs e)
        {
            if (fireworksPanel != null)
            {
                string info = fireworksPanel.GetGpuInfo();
                string gpuStatus = fireworksPanel.IsUsingGpu ? GetResource("GpuAccelerationActive") : GetResource("SoftwareRendering");
                
                MessageBox.Show(
                    $"{info}\n\n{gpuStatus}", 
                    GetResource("GpuInfoTitle"), 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information
                );
            }
        }
    }
}