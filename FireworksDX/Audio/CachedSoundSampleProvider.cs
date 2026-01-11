using NAudio.Wave;

namespace FireworksDX.Audio
{
    /// <summary>
    /// Provider to play a CachedSound through NAudio with variable speed support.
    /// Each instance represents a single playback of the sound.
    /// </summary>
    internal class CachedSoundSampleProvider : ISampleProvider
    {
        private readonly CachedSound cachedSound;
        private readonly float playbackSpeed;
        private float position;

        /// <summary>
        /// Creates a provider to play the sound.
        /// </summary>
        /// <param name="cachedSound">The sound to play.</param>
        /// <param name="playbackSpeed">Playback speed (1.0 = normal, 0.5 = half speed, 2.0 = double speed). Default: 1.0</param>
        public CachedSoundSampleProvider(CachedSound cachedSound, float playbackSpeed = 1.0f)
        {
            this.cachedSound = cachedSound;
            this.playbackSpeed = Math.Max(0.1f, playbackSpeed); // Prevent zero or negative speeds
            position = 0;
        }

        public int Read(float[] buffer, int offset, int count)
        {
            int samplesWritten = 0;

            for (int i = 0; i < count; i++)
            {
                int sampleIndex = (int)position;

                if (sampleIndex >= cachedSound.AudioData.Length)
                {
                    break; // End of sound
                }

                buffer[offset + i] = cachedSound.AudioData[sampleIndex];
                position += playbackSpeed;
                samplesWritten++;
            }

            return samplesWritten;
        }

        public WaveFormat WaveFormat => cachedSound.WaveFormat;
    }
}