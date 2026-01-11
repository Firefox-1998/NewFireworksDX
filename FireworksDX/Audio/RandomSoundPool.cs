using NAudio.Wave;

namespace FireworksDX.Audio
{
    /// <summary>
    /// Manages a pool of audio files and plays them randomly.
    /// </summary>
    internal class RandomSoundPool
    {
        private readonly List<CachedSound> sounds = [];
        private readonly Random random = new();

        /// <summary>
        /// Gets the wave format of the loaded sounds (all must have the same format).
        /// </summary>
        public WaveFormat? WaveFormat { get; private set; }

        /// <summary>
        /// Gets whether any sounds are loaded.
        /// </summary>
        public bool HasSounds => sounds.Count > 0;

        /// <summary>
        /// Loads all WAV files from the specified directory.
        /// </summary>
        /// <param name="directoryPath">Directory containing audio files.</param>
        /// <param name="searchPattern">Search pattern (default: "*.wav").</param>
        public void LoadFromDirectory(string directoryPath, string searchPattern = "*.wav")
        {
            if (!Directory.Exists(directoryPath))
                return;

            var files = Directory.GetFiles(directoryPath, searchPattern);
            
            foreach (var file in files)
            {
                LoadSound(file);
            }
        }

        /// <summary>
        /// Loads a single audio file into the pool.
        /// </summary>
        /// <param name="filePath">Path to the audio file.</param>
        public void LoadSound(string filePath)
        {
            try
            {
                var sound = new CachedSound(filePath);

                // Ensure all sounds have the same wave format
                if (WaveFormat == null)
                {
                    WaveFormat = sound.WaveFormat;
                }
                else if (sound.WaveFormat.SampleRate != WaveFormat.SampleRate ||
                         sound.WaveFormat.Channels != WaveFormat.Channels)
                {
                    // Skip incompatible audio formats
                    return;
                }

                sounds.Add(sound);
            }
            catch (Exception)
            {
                // Ignore files that fail to load
            }
        }

        /// <summary>
        /// Gets a random sound from the pool.
        /// </summary>
        /// <returns>A random CachedSound, or null if no sounds are loaded.</returns>
        public CachedSound? GetRandomSound()
        {
            if (sounds.Count == 0)
                return null;

            int index = random.Next(sounds.Count);
            return sounds[index];
        }

        /// <summary>
        /// Clears all loaded sounds.
        /// </summary>
        public void Clear()
        {
            sounds.Clear();
            WaveFormat = null;
        }
    }
}