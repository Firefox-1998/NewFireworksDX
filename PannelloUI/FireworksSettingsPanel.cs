using FireworksDX.Config;

namespace PannelloUI
{
    public partial class FireworksSettingsPanel : Form
    {
        private Dictionary<string, Action>? _profiles;

        public FireworksSettingsPanel()
        {
            InitializeComponent();
            InitializeProfiles();
            LoadProfiles();
            LoadCurrentConfig();
            HookEvents();
            RandomProfile();
        }

        private void InitializeProfiles()
        {
            _profiles = new Dictionary<string, Action>
            {
                ["Default"] = FireworksConfig.LoadDefault,
                ["New Year Show"] = FireworksConfig.LoadNewYearShow,
                ["Japan Hanabi"] = FireworksConfig.LoadJapanHanabi,
                ["Mega Show"] = FireworksConfig.LoadMegaShow,
                ["Golden Rain"] = FireworksConfig.LoadGoldenRain,
                ["Cyber Neon"] = FireworksConfig.LoadCyberNeon,
                ["Calm Festival"] = FireworksConfig.LoadCalmFestival,
                ["Red Dragon"] = FireworksConfig.LoadRedDragon,
                ["Winter Festival"] = FireworksConfig.LoadWinterFestival,
                ["Halloween Spirits"] = FireworksConfig.LoadHalloweenSpirits,
                ["Summer Beach Party"] = FireworksConfig.LoadSummerBeachParty,
                ["Christmas Magic"] = FireworksConfig.LoadChristmasMagic,
                ["Chrysanthemum"] = FireworksConfig.LoadChrysanthemum,
                ["Peony"] = FireworksConfig.LoadPeony,
                ["Willow"] = FireworksConfig.LoadWillow,
                ["Palm Tree"] = FireworksConfig.LoadPalmTree,
                ["Strobe Shell"] = FireworksConfig.LoadStrobeShell
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
            btnApply.Text = "Applicazione...";

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
                btnApply.Text = "Apply";
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
