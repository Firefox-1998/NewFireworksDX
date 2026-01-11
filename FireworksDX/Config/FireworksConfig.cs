namespace FireworksDX.Config
{
    public static class FireworksConfig
    {
        // Rockets
        public static int MinRocketIntervalFrames { get; set; }
        public static int MaxRocketIntervalFrames { get; set; }

        public static double RocketMinSpeed { get; set; }
        public static double RocketMaxSpeed { get; set; }
        public static double RocketGravity { get; set; }

        // Explosions
        public static int MinBursts { get; set; }
        public static int MaxBursts { get; set; }

        public static int MinParticles { get; set; }
        public static int MaxParticles { get; set; }
        public static double ParticleGravity { get; set; }

        // Trails
        public static int TrailLength { get; set; }

        // Smoke
        public static int MinSmoke { get; set; }
        public static int MaxSmoke { get; set; }

        // Effects
        public static bool EnableGlow { get; set; }
        public static bool EnableTwinkle { get; set; }
        public static bool EnableSmoke { get; set; }

        // Audio
        public static bool EnableAudio { get; set; } = true;

        /// <summary>
        /// Custom path for the explosion audio file.
        /// If null or empty, uses the default path (Audio\explosion.wav) relative to the DLL.
        /// </summary>
        public static string? ExplosionSoundPath { get; set; }

        static FireworksConfig()
        {
            LoadDefault();
        }

        // ---------------------------------------------------------
        // PROFILE 1 — DEFAULT
        // ---------------------------------------------------------
        public static void LoadDefault()
        {
            MinRocketIntervalFrames = 30;
            MaxRocketIntervalFrames = 100;

            RocketMinSpeed = 6;
            RocketMaxSpeed = 9;
            RocketGravity = 0.08;

            MinBursts = 2;
            MaxBursts = 4;

            MinParticles = 60;
            MaxParticles = 120;
            ParticleGravity = 0.05;

            TrailLength = 12;

            MinSmoke = 25;
            MaxSmoke = 45;

            EnableGlow = true;
            EnableTwinkle = true;
            EnableSmoke = true;
        }

        // ---------------------------------------------------------
        // PROFILE 2 — NEW YEAR SHOW
        // ---------------------------------------------------------
        public static void LoadNewYearShow()
        {
            MinRocketIntervalFrames = 15;
            MaxRocketIntervalFrames = 50;

            RocketMinSpeed = 7;
            RocketMaxSpeed = 11;
            RocketGravity = 0.09;

            MinBursts = 3;
            MaxBursts = 5;

            MinParticles = 90;
            MaxParticles = 160;
            ParticleGravity = 0.055;

            TrailLength = 14;

            MinSmoke = 35;
            MaxSmoke = 65;

            EnableGlow = true;
            EnableTwinkle = true;
            EnableSmoke = true;
        }

        // ---------------------------------------------------------
        // PROFILE 3 — JAPAN HANABI
        // ---------------------------------------------------------
        public static void LoadJapanHanabi()
        {
            MinRocketIntervalFrames = 40;
            MaxRocketIntervalFrames = 120;

            RocketMinSpeed = 5.5;
            RocketMaxSpeed = 8;
            RocketGravity = 0.07;

            MinBursts = 1;
            MaxBursts = 3;

            MinParticles = 70;
            MaxParticles = 130;
            ParticleGravity = 0.045;

            TrailLength = 18;

            MinSmoke = 15;
            MaxSmoke = 35;

            EnableGlow = true;
            EnableTwinkle = false;
            EnableSmoke = true;
        }

        // ---------------------------------------------------------
        // PROFILE 4 — MEGA SHOW (maximum intensity)
        // ---------------------------------------------------------
        public static void LoadMegaShow()
        {
            MinRocketIntervalFrames = 5;
            MaxRocketIntervalFrames = 20;

            RocketMinSpeed = 8;
            RocketMaxSpeed = 13;
            RocketGravity = 0.1;

            MinBursts = 4;
            MaxBursts = 7;

            MinParticles = 150;
            MaxParticles = 260;
            ParticleGravity = 0.06;

            TrailLength = 20;

            MinSmoke = 50;
            MaxSmoke = 90;

            EnableGlow = true;
            EnableTwinkle = true;
            EnableSmoke = true;
        }

        // ---------------------------------------------------------
        // PROFILE 5 — GOLDEN RAIN (elegant gold)
        // ---------------------------------------------------------
        public static void LoadGoldenRain()
        {
            MinRocketIntervalFrames = 35;
            MaxRocketIntervalFrames = 90;

            RocketMinSpeed = 6;
            RocketMaxSpeed = 8;
            RocketGravity = 0.075;

            MinBursts = 1;
            MaxBursts = 2;

            MinParticles = 80;
            MaxParticles = 140;
            ParticleGravity = 0.04;

            TrailLength = 22;

            MinSmoke = 20;
            MaxSmoke = 40;

            EnableGlow = true;
            EnableTwinkle = false;
            EnableSmoke = true;
        }

        // ---------------------------------------------------------
        // PROFILE 6 — CYBER NEON (futuristic)
        // ---------------------------------------------------------
        public static void LoadCyberNeon()
        {
            MinRocketIntervalFrames = 20;
            MaxRocketIntervalFrames = 60;

            RocketMinSpeed = 9;
            RocketMaxSpeed = 14;
            RocketGravity = 0.085;

            MinBursts = 2;
            MaxBursts = 4;

            MinParticles = 100;
            MaxParticles = 180;
            ParticleGravity = 0.05;

            TrailLength = 10;

            MinSmoke = 10;
            MaxSmoke = 25;

            EnableGlow = true;
            EnableTwinkle = true;
            EnableSmoke = false; // neon = clean
        }

        // ---------------------------------------------------------
        // PROFILE 7 — CALM FESTIVAL (relaxed)
        // ---------------------------------------------------------
        public static void LoadCalmFestival()
        {
            MinRocketIntervalFrames = 60;
            MaxRocketIntervalFrames = 150;

            RocketMinSpeed = 5;
            RocketMaxSpeed = 7;
            RocketGravity = 0.06;

            MinBursts = 1;
            MaxBursts = 2;

            MinParticles = 40;
            MaxParticles = 80;
            ParticleGravity = 0.035;

            TrailLength = 16;

            MinSmoke = 10;
            MaxSmoke = 20;

            EnableGlow = true;
            EnableTwinkle = false;
            EnableSmoke = true;
        }

        // ---------------------------------------------------------
        // PROFILE 8 — RED DRAGON (aggressive)
        // ---------------------------------------------------------
        public static void LoadRedDragon()
        {
            MinRocketIntervalFrames = 10;
            MaxRocketIntervalFrames = 40;

            RocketMinSpeed = 8;
            RocketMaxSpeed = 12;
            RocketGravity = 0.095;

            MinBursts = 3;
            MaxBursts = 5;

            MinParticles = 120;
            MaxParticles = 200;
            ParticleGravity = 0.06;

            TrailLength = 12;

            MinSmoke = 30;
            MaxSmoke = 60;

            EnableGlow = true;
            EnableTwinkle = true;
            EnableSmoke = true;
        }

        // ---------------------------------------------------------
        // PROFILE 9 — WinterFestival 
        // Ice effect, cold trails, slow and bright explosions
        // ---------------------------------------------------------		
        public static void LoadWinterFestival()
        {
            MinRocketIntervalFrames = 40;
            MaxRocketIntervalFrames = 110;

            RocketMinSpeed = 5.5;
            RocketMaxSpeed = 8;
            RocketGravity = 0.07;

            MinBursts = 1;
            MaxBursts = 3;

            MinParticles = 70;
            MaxParticles = 130;
            ParticleGravity = 0.04;

            TrailLength = 22;

            MinSmoke = 15;
            MaxSmoke = 30;

            EnableGlow = true;
            EnableTwinkle = true;
            EnableSmoke = true;
        }

        // ---------------------------------------------------------
        // PROFILE 10 — HalloweenSpirits
        // Orange/purple colors, irregular explosions, dense smoke.
        // ---------------------------------------------------------		
        public static void LoadHalloweenSpirits()
        {
            MinRocketIntervalFrames = 25;
            MaxRocketIntervalFrames = 80;

            RocketMinSpeed = 6;
            RocketMaxSpeed = 9;
            RocketGravity = 0.085;

            MinBursts = 2;
            MaxBursts = 4;

            MinParticles = 90;
            MaxParticles = 150;
            ParticleGravity = 0.055;

            TrailLength = 14;

            MinSmoke = 40;
            MaxSmoke = 70;

            EnableGlow = true;
            EnableTwinkle = false;
            EnableSmoke = true;
        }

        // ---------------------------------------------------------
        // PROFILE 11 — SummerBeachParty
        // Vibrant colors, wide explosions, short and clean trails.
        // ---------------------------------------------------------
        public static void LoadSummerBeachParty()
        {
            MinRocketIntervalFrames = 20;
            MaxRocketIntervalFrames = 60;

            RocketMinSpeed = 7;
            RocketMaxSpeed = 10;
            RocketGravity = 0.08;

            MinBursts = 2;
            MaxBursts = 4;

            MinParticles = 80;
            MaxParticles = 140;
            ParticleGravity = 0.05;

            TrailLength = 10;

            MinSmoke = 10;
            MaxSmoke = 25;

            EnableGlow = true;
            EnableTwinkle = true;
            EnableSmoke = false;
        }

        // ---------------------------------------------------------
        // PROFILE 12 — ChristmasMagic
        // Red/green/gold colors, long trails, soft explosions.
        // ---------------------------------------------------------
        public static void LoadChristmasMagic()
        {
            MinRocketIntervalFrames = 35;
            MaxRocketIntervalFrames = 100;

            RocketMinSpeed = 6;
            RocketMaxSpeed = 8.5;
            RocketGravity = 0.075;

            MinBursts = 2;
            MaxBursts = 3;

            MinParticles = 70;
            MaxParticles = 120;
            ParticleGravity = 0.045;

            TrailLength = 20;

            MinSmoke = 20;
            MaxSmoke = 40;

            EnableGlow = true;
            EnableTwinkle = true;
            EnableSmoke = true;
        }

        // ---------------------------------------------------------
        // PROFILE 13 — Chrysanthemum
        // Perfect spherical explosion, long trails, uniform decay.
        // ---------------------------------------------------------		
        public static void LoadChrysanthemum()
        {
            MinRocketIntervalFrames = 30;
            MaxRocketIntervalFrames = 90;

            RocketMinSpeed = 6;
            RocketMaxSpeed = 9;
            RocketGravity = 0.08;

            MinBursts = 1;
            MaxBursts = 1;

            MinParticles = 120;
            MaxParticles = 180;
            ParticleGravity = 0.045;

            TrailLength = 25;

            MinSmoke = 20;
            MaxSmoke = 40;

            EnableGlow = true;
            EnableTwinkle = false;
            EnableSmoke = true;
        }

        // ---------------------------------------------------------
        // PROFILE 14 — Peony
        // Round explosion without long trails, very common in real shows.
        // ---------------------------------------------------------			
        public static void LoadPeony()
        {
            MinRocketIntervalFrames = 25;
            MaxRocketIntervalFrames = 70;

            RocketMinSpeed = 6.5;
            RocketMaxSpeed = 9.5;
            RocketGravity = 0.08;

            MinBursts = 1;
            MaxBursts = 2;

            MinParticles = 100;
            MaxParticles = 160;
            ParticleGravity = 0.05;

            TrailLength = 8;

            MinSmoke = 15;
            MaxSmoke = 30;

            EnableGlow = true;
            EnableTwinkle = false;
            EnableSmoke = true;
        }

        // ---------------------------------------------------------
        // PROFILE 15 — Willow
        // Very long trails that "fall" slowly like a weeping willow.
        // --------------------------------------------------------
        public static void LoadWillow()
        {
            MinRocketIntervalFrames = 40;
            MaxRocketIntervalFrames = 120;

            RocketMinSpeed = 5.5;
            RocketMaxSpeed = 7.5;
            RocketGravity = 0.07;

            MinBursts = 1;
            MaxBursts = 2;

            MinParticles = 90;
            MaxParticles = 140;
            ParticleGravity = 0.03;

            TrailLength = 35;

            MinSmoke = 25;
            MaxSmoke = 45;

            EnableGlow = true;
            EnableTwinkle = false;
            EnableSmoke = true;
        }

        // ---------------------------------------------------------
        // PROFILE 16 — PalmTree
        // Explosion with central "trunk" and side branches.
        // --------------------------------------------------------
        public static void LoadPalmTree()
        {
            MinRocketIntervalFrames = 30;
            MaxRocketIntervalFrames = 80;

            RocketMinSpeed = 7;
            RocketMaxSpeed = 10;
            RocketGravity = 0.085;

            MinBursts = 1;
            MaxBursts = 2;

            MinParticles = 60;
            MaxParticles = 100;
            ParticleGravity = 0.06;

            TrailLength = 18;

            MinSmoke = 20;
            MaxSmoke = 35;

            EnableGlow = true;
            EnableTwinkle = false;
            EnableSmoke = true;
        }

        // ---------------------------------------------------------
        // PROFILE 17 — StrobeShell
        // Explosions with very strong intermittent flashing.
        // -------------------------------------------------------- 
        public static void LoadStrobeShell()
        {
            MinRocketIntervalFrames = 20;
            MaxRocketIntervalFrames = 60;

            RocketMinSpeed = 7;
            RocketMaxSpeed = 11;
            RocketGravity = 0.09;

            MinBursts = 1;
            MaxBursts = 2;

            MinParticles = 110;
            MaxParticles = 180;
            ParticleGravity = 0.055;

            TrailLength = 12;

            MinSmoke = 15;
            MaxSmoke = 30;

            EnableGlow = true;
            EnableTwinkle = true;
            EnableSmoke = true;
        }
    }
}