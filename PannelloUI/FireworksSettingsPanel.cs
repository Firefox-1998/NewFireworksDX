using FireworksDX.Config;
using System.Reflection;
using System.Resources;

namespace AboutFormWithUIPanel
{
    public partial class FireworksSettingsPanel : Form
    {
        private Dictionary<string, Action>? _profiles;
        private readonly ResourceManager _resources;

        public FireworksSettingsPanel()
        {
            _resources = new ResourceManager(typeof(FireworksSettingsPanel));
            InitializeComponent();
            LoadLocalizedStrings();
            InitializeProfiles();
            LoadProfiles();
            LoadCurrentConfig();
            HookEvents();
            RandomProfile();
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
            btnApply.Text = GetResource("btnApply");
            btnRandom.Text = GetResource("btnRandom");
            chkAudio.Text = GetResource("chkAudio");
            chkGlow.Text = GetResource("chkGlow");
            chkSmoke.Text = GetResource("chkSmoke");
            chkTwinkle.Text = GetResource("chkTwinkle");
            lblIntensity.Text = GetResource("lblIntensity");
            lblProfile.Text = GetResource("lblProfile");
        }

        private void InitializeProfiles()
        {
            _profiles = new Dictionary<string, Action>
            {
                [GetResource("FireworksProfileName_1")] = FireworksConfig.LoadDefault,
                [GetResource("FireworksProfileName_2")] = FireworksConfig.LoadNewYearShow,
                [GetResource("FireworksProfileName_3")] = FireworksConfig.LoadJapanHanabi,
                [GetResource("FireworksProfileName_4")] = FireworksConfig.LoadMegaShow,
                [GetResource("FireworksProfileName_5")] = FireworksConfig.LoadGoldenRain,
                [GetResource("FireworksProfileName_6")] = FireworksConfig.LoadCyberNeon,
                [GetResource("FireworksProfileName_7")] = FireworksConfig.LoadCalmFestival,
                [GetResource("FireworksProfileName_8")] = FireworksConfig.LoadRedDragon,
                [GetResource("FireworksProfileName_9")] = FireworksConfig.LoadWinterFestival,
                [GetResource("FireworksProfileName_10")] = FireworksConfig.LoadHalloweenSpirits,
                [GetResource("FireworksProfileName_11")] = FireworksConfig.LoadSummerBeachParty,
                [GetResource("FireworksProfileName_12")] = FireworksConfig.LoadChristmasMagic,
                [GetResource("FireworksProfileName_13")] = FireworksConfig.LoadChrysanthemum,
                [GetResource("FireworksProfileName_14")] = FireworksConfig.LoadPeony,
                [GetResource("FireworksProfileName_15")] = FireworksConfig.LoadWillow,
                [GetResource("FireworksProfileName_16")] = FireworksConfig.LoadPalmTree,
                [GetResource("FireworksProfileName_17")] = FireworksConfig.LoadStrobeShell
            };
        }

        private void LoadProfiles()
        {
            if (_profiles is null)
            {
                return;
            }                
            comboProfiles.Items.AddRange([.. _profiles.Keys]);
            comboProfiles.SelectedIndex = 0;
        }

        private void LoadCurrentConfig()
        {
            chkGlow.Checked = FireworksConfig.EnableGlow;
            chkTwinkle.Checked = FireworksConfig.EnableTwinkle;
            chkSmoke.Checked = FireworksConfig.EnableSmoke;
            chkAudio.Checked = FireworksConfig.EnableAudio;
            trackIntensity.Value = 100;
        }

        private void HookEvents()
        {
            btnApply.Click += (s, e) => ApplyProfile();
            btnRandom.Click += (s, e) => RandomProfile();
        }

        private void ApplyProfile()
        {
            btnApply.Enabled = false;
            btnApply.Text = GetResource("btnApply_To");

            try
            {
                string? selectedProfile = comboProfiles?.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedProfile) && _profiles is not null && _profiles.TryGetValue(selectedProfile, out Action? loadAction))
                {
                    loadAction();
                }

                FireworksConfig.EnableGlow = chkGlow.Checked;
                FireworksConfig.EnableTwinkle = chkTwinkle.Checked;
                FireworksConfig.EnableSmoke = chkSmoke.Checked;
                FireworksConfig.EnableAudio = chkAudio.Checked;

                double factor = trackIntensity.Value / 100.0;
                FireworksConfig.MinParticles = (int)(FireworksConfig.MinParticles * factor);
                FireworksConfig.MaxParticles = (int)(FireworksConfig.MaxParticles * factor);
            }
            finally
            {
                btnApply.Text = GetResource("btnApply");
                btnApply.Enabled = true;
            }
        }

        private void RandomProfile()
        {
            var rnd = new Random();
            comboProfiles.SelectedIndex = rnd.Next(comboProfiles.Items.Count);
            ApplyProfile();
        }
    }
}
