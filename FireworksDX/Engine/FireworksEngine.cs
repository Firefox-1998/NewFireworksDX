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
        private CachedSound? explosionSound;
        
        // Durata totale del file audio in secondi
        private const double AudioDurationSeconds = 1.0;
        
        // FPS stimato (60 fps)
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

                if (!string.IsNullOrEmpty(audioPath) && File.Exists(audioPath))
                {
                    // Carica il suono in memoria (una sola volta)
                    explosionSound = new CachedSound(audioPath);
                    
                    // Inizializza il dispositivo di output
                    outputDevice = new WaveOutEvent();
                    
                    // Crea il mixer per gestire audio simultanei
                    mixer = new MixingSampleProvider(explosionSound.WaveFormat)
                    {
                        ReadFully = true
                    };
                    
                    outputDevice.Init(mixer);
                    outputDevice.Play();
                }
                else
                {

                }
            }
            catch (Exception)
            {
                // Se il caricamento fallisce, pulisci tutto
                explosionSound = null;
                outputDevice?.Dispose();
                outputDevice = null;
                mixer = null;
            }
        }

        /// <summary>
        /// Ottiene il percorso del file audio di esplosione.
        /// Se FireworksConfig.ExplosionSoundPath è impostato, usa quello.
        /// Altrimenti usa il path di default: [DllDirectory]\Audio\explosion.wav
        /// </summary>
        private string GetExplosionSoundPath()
        {
            // Se è impostato un path personalizzato, usalo
            if (!string.IsNullOrWhiteSpace(FireworksConfig.ExplosionSoundPath))
            {
                return FireworksConfig.ExplosionSoundPath;
            }

            // Altrimenti usa il path di default relativo alla DLL
            string assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string assemblyDirectory = Path.GetDirectoryName(assemblyLocation) ?? AppDomain.CurrentDomain.BaseDirectory;
            
            return Path.Combine(assemblyDirectory, "Audio", "explosion.wav");
        }

        /// <summary>
        /// Ricarica il file audio (utile se cambia il path durante l'esecuzione).
        /// </summary>
        public void ReloadSound()
        {
            // Ferma e disponi delle risorse esistenti
            outputDevice?.Stop();
            outputDevice?.Dispose();
            outputDevice = null;
            mixer = null;
            explosionSound = null;

            // Reinizializza
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

            // Calcola il tempo di volo in frame usando cinematica
            // Quando il razzo raggiunge il picco, velocityY diventa 0
            // Formula: t = -v₀ / g (numero di frame necessari)
            double flightTimeFrames = Math.Abs(velocityY / FireworksConfig.RocketGravity);
            
            // Converti da frame a secondi
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

            // Riproduci l'audio AL LANCIO con velocità adattata al tempo di volo
            PlayRocketSound(rocket.EstimatedFlightTime);
        }

        /// <summary>
        /// Riproduce l'audio del razzo con velocità adattata al tempo di volo.
        /// </summary>
        /// <param name="flightTimeSeconds">Tempo di volo stimato in secondi.</param>
        private void PlayRocketSound(double flightTimeSeconds)
        {
            if (!EnableSound || !FireworksConfig.EnableAudio || explosionSound == null || mixer == null)
                return;

            try
            {
                // Calcola la velocità di riproduzione per sincronizzare audio e volo
                // Se il volo dura 0.8 secondi e l'audio 1.0 secondi, velocità = 1.0/0.8 = 1.25x
                float playbackSpeed = (float)(AudioDurationSeconds / flightTimeSeconds);
                
                // Limita la velocità tra 0.5x e 2.0x per evitare suoni troppo distorti
                playbackSpeed = Math.Clamp(playbackSpeed, 0.5f, 2.0f);

                // Aggiungi l'audio al mixer con la velocità calcolata
                mixer.AddMixerInput(new CachedSoundSampleProvider(explosionSound, playbackSpeed));
            }
            catch (Exception)
            {
                // Ignora l'errore
            }
        }

        /// <summary>
        /// Lancia n razzi contemporaneamente (per effetti burst con audio sincronizzato).
        /// </summary>
        public void LaunchImmediateBurstRocket(int width, int height)
        {
            LaunchRocket(width, height);
        }

        /// <summary>
        /// Riproduce solo la parte dell'esplosione (circa metà dell'audio accelerato).
        /// </summary>
        private void PlayExplosionSoundOnly()
        {
            if (!EnableSound || !FireworksConfig.EnableAudio || explosionSound == null || mixer == null)
                return;

            try
            {
                // Riproduci l'audio a velocità doppia per ottenere un'esplosione più rapida
                mixer.AddMixerInput(new CachedSoundSampleProvider(explosionSound, 2.0f));
            }
            catch (Exception)
            {
                // Ignora l'errore
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

                // Crea nuove istanze di Vec2 con i valori aggiornati
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
            // NON riprodurre audio qui perché è già stato riprodotto al lancio
            // L'audio si sincronizza automaticamente con l'esplosione visiva

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

                // Aggiorna posizione
                p.Position = new Vec2(
                    p.Position.X + p.Velocity.X,
                    p.Position.Y + p.Velocity.Y
                );
                
                // Aggiorna velocità (gravità)
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

                // Aggiorna posizione
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
        /// Rilascia le risorse audio quando l'engine viene distrutto.
        /// </summary>
        ~FireworksEngine()
        {
            outputDevice?.Stop();
            outputDevice?.Dispose();
        }
    }
}