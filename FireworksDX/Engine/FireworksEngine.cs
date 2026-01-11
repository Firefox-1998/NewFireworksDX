using FireworksDX.Audio;
using FireworksDX.Config;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace FireworksDX.Engine
{
    public class FireworksEngine
    {
        private readonly Random rnd = new();
        private WaveOutEvent? outputDevice;
        private MixingSampleProvider? mixer;
        private RandomSoundPool? explosionSoundPool;

        // Total duration of the audio file in seconds
        private const double AudioDurationSeconds = 1.0;

        // Estimated FPS (60 fps)
        private const double FrameRate = 60.0;

        public List<Rocket> Rockets { get; } = [];
        public List<Particle> Particles { get; } = [];
        public List<SmokeParticle> Smoke { get; } = [];

        public int NextRocketIn { get; private set; }
        public bool EnableSound { get; set; } = true;

        public FireworksEngine()
        {
            ResetRocketTimer();
            InitializeSoundPool();
        }

        private void InitializeSoundPool()
        {
            try
            {
                string audioPath = GetExplosionSoundPath();

                // Create sound pool
                explosionSoundPool = new RandomSoundPool();

                // Check if path is a directory or a file
                if (Directory.Exists(audioPath))
                {
                    // Load all WAV files from directory
                    explosionSoundPool.LoadFromDirectory(audioPath, "*.wav");
                }
                else if (File.Exists(audioPath))
                {
                    // Load single file
                    explosionSoundPool.LoadSound(audioPath);
                }

                // If no sounds were loaded, clean up
                if (!explosionSoundPool.HasSounds)
                {
                    explosionSoundPool = null;
                    return;
                }

                // Initialize output device
                outputDevice = new WaveOutEvent();

                // Create mixer to handle simultaneous audio
                mixer = new MixingSampleProvider(explosionSoundPool.WaveFormat)
                {
                    ReadFully = true
                };

                outputDevice.Init(mixer);
                outputDevice.Play();
            }
            catch (Exception)
            {
                // If loading fails, clean up everything
                explosionSoundPool = null;
                outputDevice?.Dispose();
                outputDevice = null;
                mixer = null;
            }
        }

        /// <summary>
        /// Gets the path of the explosion audio file or directory.
        /// If FireworksConfig.ExplosionSoundPath is set, uses that.
        /// Otherwise uses the default path: [DllDirectory]\Audio\
        /// </summary>
        private string GetExplosionSoundPath()
        {
            // If a custom path is set, use it
            if (!string.IsNullOrWhiteSpace(FireworksConfig.ExplosionSoundPath))
            {
                return FireworksConfig.ExplosionSoundPath;
            }

            // Otherwise use the default directory relative to the DLL
            string assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string assemblyDirectory = Path.GetDirectoryName(assemblyLocation) ?? AppDomain.CurrentDomain.BaseDirectory;

            return Path.Combine(assemblyDirectory, "Audio");
        }

        /// <summary>
        /// Reloads the audio file (useful if the path changes during execution).
        /// </summary>
        public void ReloadSound()
        {
            // Stop and dispose of existing resources
            outputDevice?.Stop();
            outputDevice?.Dispose();
            outputDevice = null;
            mixer = null;
            explosionSoundPool?.Clear();
            explosionSoundPool = null;

            // Reinitialize
            InitializeSoundPool();
        }

        private void ResetRocketTimer()
        {
            NextRocketIn = rnd.Next(
                FireworksConfig.MinRocketIntervalFrames,
                FireworksConfig.MaxRocketIntervalFrames
            );
        }

        public void Update(int width, int height)
        {
            NextRocketIn--;
            if (NextRocketIn <= 0)
            {
                LaunchRocket(width, height);
                ResetRocketTimer();
            }

            UpdateRockets();
            UpdateParticles();
            UpdateSmoke();
        }

        private void LaunchRocket(int width, int height)
        {
            double x = rnd.NextDouble() * width;
            double y = height;

            double targetHeight = height * (0.25 + rnd.NextDouble() * 0.35);

            double velocityY = -(FireworksConfig.RocketMinSpeed +
                       rnd.NextDouble() * (FireworksConfig.RocketMaxSpeed - FireworksConfig.RocketMinSpeed));

            // Calculate flight time in frames using kinematics
            // When the rocket reaches its peak, velocityY becomes 0
            // Formula: t = -v₀ / g (number of frames needed)
            double flightTimeFrames = Math.Abs(velocityY / FireworksConfig.RocketGravity);

            // Convert from frames to seconds
            double flightTimeSeconds = flightTimeFrames / FrameRate;

            var rocket = new Rocket
            {
                Position = new Vec2(x, y),
                Velocity = new Vec2(
                    (rnd.NextDouble() - 0.5) * 1.0,
                    velocityY
                ),
                TargetY = targetHeight,
                Color = RandomBrightColor(),
                EstimatedFlightTime = flightTimeSeconds
            };

            Rockets.Add(rocket);

            // Play audio AT LAUNCH with speed adapted to flight time
            PlayRocketSound(rocket.EstimatedFlightTime);
        }

        /// <summary>
        /// Plays a random rocket audio with speed adapted to flight time.
        /// </summary>
        /// <param name="flightTimeSeconds">Estimated flight time in seconds.</param>
        private void PlayRocketSound(double flightTimeSeconds)
        {
            if (!EnableSound || !FireworksConfig.EnableAudio || explosionSoundPool == null || mixer == null)
                return;

            try
            {
                // Get a random sound from the pool
                var randomSound = explosionSoundPool.GetRandomSound();
                if (randomSound == null)
                    return;

                // Calculate playback speed to synchronize audio with flight
                // If flight lasts 0.8 seconds and audio 1.0 seconds, speed = 1.0/0.8 = 1.25x
                float playbackSpeed = (float)(AudioDurationSeconds / flightTimeSeconds);

                // Limit speed between 0.5x and 2.0x to avoid overly distorted sounds
                playbackSpeed = Math.Clamp(playbackSpeed, 0.5f, 2.0f);

                // Add audio to mixer with calculated speed
                mixer.AddMixerInput(new CachedSoundSampleProvider(randomSound, playbackSpeed));
            }
            catch (Exception)
            {
                // Ignore error
            }
        }

        /// <summary>
        /// Launches n rockets simultaneously (for burst effects with synchronized audio).
        /// </summary>
        public void LaunchImmediateBurstRocket(int width, int height)
        {
            LaunchRocket(width, height);
        }

        /// <summary>
        /// Plays only the explosion part (about half of the accelerated audio).
        /// </summary>
        private void PlayExplosionSoundOnly()
        {
            if (!EnableSound || !FireworksConfig.EnableAudio || explosionSoundPool == null || mixer == null)
                return;

            try
            {
                // Get a random sound from the pool
                var randomSound = explosionSoundPool.GetRandomSound();
                if (randomSound == null)
                    return;

                // Play audio at double speed to get a faster explosion
                mixer.AddMixerInput(new CachedSoundSampleProvider(randomSound, 2.0f));
            }
            catch (Exception)
            {
                // Ignore error
            }
        }

        private Color RandomBrightColor()
        {
            return Color.FromArgb(
                255,
                rnd.Next(150, 256),
                rnd.Next(150, 256),
                rnd.Next(150, 256)
            );
        }

        private void UpdateRockets()
        {
            for (int i = Rockets.Count - 1; i >= 0; i--)
            {
                var r = Rockets[i];

                // Create new Vec2 instances with updated values
                r.Position = new Vec2(
                    r.Position.X + r.Velocity.X,
                    r.Position.Y + r.Velocity.Y
                );

                r.Velocity = new Vec2(
                    r.Velocity.X,
                    r.Velocity.Y + FireworksConfig.RocketGravity
                );

                if (r.Velocity.Y >= 0 || r.Position.Y <= r.TargetY)
                {
                    Explode(r);
                    Rockets.RemoveAt(i);
                }
            }
        }

        private void Explode(Rocket r)
        {
            // DO NOT play audio here because it was already played at launch
            // The audio synchronizes automatically with the visual explosion

            int bursts = rnd.Next(FireworksConfig.MinBursts, FireworksConfig.MaxBursts);

            for (int b = 0; b < bursts; b++)
            {
                int count = rnd.Next(FireworksConfig.MinParticles, FireworksConfig.MaxParticles);
                double baseSpeed = 3 + b * 1.5;

                for (int i = 0; i < count; i++)
                {
                    double angle = rnd.NextDouble() * Math.PI * 2;
                    double speed = baseSpeed + rnd.NextDouble() * 3;

                    Color c = (rnd.NextDouble() < 0.5) ? r.Color : RandomBrightColor();

                    Particles.Add(new Particle
                    {
                        Position = r.Position,
                        Velocity = new Vec2(Math.Cos(angle) * speed, Math.Sin(angle) * speed),
                        Life = rnd.Next(60, 110),
                        BaseColor = c,
                        TwinklePhase = rnd.NextDouble() * Math.PI * 2
                    });
                }
            }

            if (FireworksConfig.EnableSmoke)
            {
                int smokeCount = rnd.Next(FireworksConfig.MinSmoke, FireworksConfig.MaxSmoke);
                for (int i = 0; i < smokeCount; i++)
                {
                    double angle = rnd.NextDouble() * Math.PI * 2;
                    double speed = rnd.NextDouble() * 1.5;

                    Smoke.Add(new SmokeParticle
                    {
                        Position = r.Position,
                        Velocity = new Vec2(Math.Cos(angle) * speed,
                                            Math.Sin(angle) * speed - 0.5),
                        Life = rnd.Next(80, 140),
                        Size = rnd.NextDouble() * 18 + 10
                    });
                }
            }
        }

        private void UpdateParticles()
        {
            for (int i = Particles.Count - 1; i >= 0; i--)
            {
                var p = Particles[i];

                // Update position
                p.Position = new Vec2(
                    p.Position.X + p.Velocity.X,
                    p.Position.Y + p.Velocity.Y
                );

                // Update velocity (gravity)
                p.Velocity = new Vec2(
                    p.Velocity.X,
                    p.Velocity.Y + FireworksConfig.ParticleGravity
                );

                p.Life--;

                p.Trail.Enqueue(p.Position);
                if (p.Trail.Count > FireworksConfig.TrailLength)
                    p.Trail.Dequeue();

                if (p.Life <= 0)
                    Particles.RemoveAt(i);
            }
        }

        private void UpdateSmoke()
        {
            for (int i = Smoke.Count - 1; i >= 0; i--)
            {
                var s = Smoke[i];

                // Update position
                s.Position = new Vec2(
                    s.Position.X + s.Velocity.X,
                    s.Position.Y + s.Velocity.Y
                );

                s.Life--;

                if (s.Life <= 0)
                    Smoke.RemoveAt(i);
            }
        }

        /// <summary>
        /// Releases audio resources when the engine is destroyed.
        /// </summary>
        ~FireworksEngine()
        {
            outputDevice?.Stop();
            outputDevice?.Dispose();
        }
    }
}