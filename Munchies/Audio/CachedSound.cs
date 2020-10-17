using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using NAudio.Wave;
using NAudio.Vorbis;
using NAudio.Wave.SampleProviders;

namespace Munchies
{

	class CachedSound
	{
		public float[] AudioData { get; private set; }
		public WaveFormat WaveFormat { get; private set; }

		public bool Loop { get; set; }

		public bool Paused { get; set; }

		public CachedSound(string resourceName)
        {
            Assembly thisExe = Assembly.GetExecutingAssembly();

            if (thisExe.GetManifestResourceNames().Contains(resourceName))
            {
                Stream file = thisExe.GetManifestResourceStream(resourceName);
                LoadSoundFile(file);
            }
            else
            {
                throw new FileNotFoundException("Couldn't find the requested sound file in the assembly", resourceName);
            }
        }

        private void LoadSoundFile(Stream stream)
		{
			using (var audioFileReader = new VorbisWaveReader(stream, true))
			{
				// TODO: could add resampling in here if required
				WaveFormat = audioFileReader.WaveFormat;
				var wholeFile = new List<float>((int)(audioFileReader.Length / 4));
				var readBuffer = new float[audioFileReader.WaveFormat.SampleRate * audioFileReader.WaveFormat.Channels];
				int samplesRead;
				while ((samplesRead = audioFileReader.Read(readBuffer, 0, readBuffer.Length)) > 0)
				{
					wholeFile.AddRange(readBuffer.Take(samplesRead));
				}
				AudioData = wholeFile.ToArray();
			}
		}

		public void Play(bool singleInstance = false)
		{
			Paused = false;
			AudioManager.PlaySound(this, singleInstance);
		}

		public void Pause()
		{
			Paused = true;
		}

		public void Stop()
		{
			AudioManager.StopSound(this);
		}
	}
}
