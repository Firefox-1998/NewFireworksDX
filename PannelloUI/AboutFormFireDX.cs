using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices.Marshalling;


namespace AboutFormWithUIPanel
{
    public partial class AboutFormFireDX : Form
    {
        private FireworksSettingsPanel? _settingsForm;
        private readonly ResourceManager _resources;

        public AboutFormFireDX()
        {
            _resources = new ResourceManager(typeof(AboutFormFireDX));
            InitializeComponent();
            InitializeLanguageComboBox();
            LoadLocalizedStrings();
            InitializeRuntimeHosts();
        }

        private void InitializeLanguageComboBox()
        {
            // Define available languages
            var languages = new Dictionary<string, CultureInfo>
            {
                ["English (US)"] = new CultureInfo("en-US"),
                ["Italiano"] = new CultureInfo("it-IT")
            };

            cmbLanguage.DataSource = new BindingSource(languages, string.Empty);
            cmbLanguage.DisplayMember = "Key";
            cmbLanguage.ValueMember = "Value";

            // Set English US as default
            cmbLanguage.SelectedValue = new CultureInfo("en-US");

            // Set default culture
            var defaultCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = defaultCulture;
            Thread.CurrentThread.CurrentCulture = defaultCulture;
            CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;
            CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
        }

        private void CmbLanguage_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cmbLanguage.SelectedValue is CultureInfo selectedCulture)
            {
                ChangeCulture(selectedCulture);
            }
        }

        private void ChangeCulture(CultureInfo culture)
        {
            // Set culture for the current thread
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;

            // Reload localized strings
            LoadLocalizedStrings();

            // Reload settings panel to update its strings as well
            if (_settingsForm != null)
            {
                panelSettingsHost.Controls.Remove(_settingsForm);
                _settingsForm.Dispose();
                _settingsForm = null;
            }
            InitializeRuntimeHosts();
        }

        /// <summary>
        /// Gets a string from the ResourceManager, throws exception if not found.
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
            // Load strings from RESX
            lblVersion.Text = string.Format(GetResource("VersionLabel"), version);
            lblTitle.Text = GetResource("Title");
            lnkRepo.Text = GetResource("OpenRepository");
            btnBurst.Text = GetResource("BurstButton");
            btnGpuInfo.Text = GetResource("GpuInfoButton");
        }

        private void InitializeRuntimeHosts()
        {
            // Host FireworksSettingsPanel in the side panel
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
                    FileName = "https://github.com/Firefox-1998/NewFireworksDX",
                    UseShellExecute = true
                });
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                MessageBox.Show(
                    string.Format(GetResource("MessageOpenBrowserError"), ex.Message),
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private void BtnBurst_Click(object? sender, EventArgs e)
        {
            Random iBurstNumRocket = new();            
            fireworksPanel.LaunchBurst(iBurstNumRocket.Next(3, 8));
        }

        private void BtnGpuInfo_Click(object? sender, EventArgs e)
        {
            if (fireworksPanel != null)
            {
                string info = fireworksPanel.GetGpuInfo();
                string gpuStatus = fireworksPanel.IsUsingGpu ? GetResource("GpuAccelerationActive") : GetResource("SoftwareRendering");

                MessageBox.Show(
                    $"{info}\n\n{gpuStatus}",
                    GetResource("GpuInfoTitleMessage"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }
    }
}