using NAudio.Wave;

namespace FireworksDX.Audio
{
    /// <summary>
    /// Rappresenta un file audio caricato in memoria per riproduzione rapida e simultanea.
    /// </summary>
    internal class CachedSound
    {
        public float[] AudioData { get; private set; }
        public WaveFormat WaveFormat { get; private set; }

        public CachedSound(string audioFileName)
        {
            using var audioFileReader = new AudioFileReader(audioFileName);
            WaveFormat = audioFileReader.WaveFormat;
            
            var wholeFile = new List<float>((int)(audioFileReader.Length / 4));
            var readBuffer = new float[audioFileReader.WaveFormat.SampleRate * audioFileReader.WaveFormat.Channels];
            int samplesRead;
            
            while ((samplesRead = audioFileReader.Read(readBuffer, 0, readBuffer.Length)) > 0)
            {
                wholeFile.AddRange(readBuffer.Take(samplesRead));
            }
            
            AudioData = [.. wholeFile];
        }
    }
}