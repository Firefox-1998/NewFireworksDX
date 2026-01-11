using NAudio.Wave;

namespace FireworksDX.Audio
{
    /// <summary>
    /// Provider per riprodurre un CachedSound attraverso NAudio con supporto per velocità variabile.
    /// Ogni istanza rappresenta una singola riproduzione del suono.
    /// </summary>
    internal class CachedSoundSampleProvider : ISampleProvider
    {
        private readonly CachedSound cachedSound;
        private readonly float playbackSpeed;
        private float position;

        /// <summary>
        /// Crea un provider per riprodurre il suono.
        /// </summary>
        /// <param name="cachedSound">Il suono da riprodurre.</param>
        /// <param name="playbackSpeed">Velocità di riproduzione (1.0 = normale, 0.5 = metà velocità, 2.0 = doppia velocità). Default: 1.0</param>
        public CachedSoundSampleProvider(CachedSound cachedSound, float playbackSpeed = 1.0f)
        {
            this.cachedSound = cachedSound;
            this.playbackSpeed = Math.Max(0.1f, playbackSpeed); // Previeni velocità zero o negative
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
                    break; // Fine del suono
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