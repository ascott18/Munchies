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
	class AudioManager : IDisposable
	{
		public static readonly AudioManager Instance = new AudioManager();

		private readonly IWavePlayer outputDevice;
		private readonly MixingSampleProvider mixer;
		private readonly VolumeSampleProvider volumeProvider;

		private AudioManager()
		{
			// Wasapi is much lower latency than WaveOutEvent.
			outputDevice = new WasapiOut(NAudio.CoreAudioApi.AudioClientShareMode.Shared, true, 0);
			//outputDevice = new WaveOutEvent();
			mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(24000, 2));
			mixer.ReadFully = true;
            mixer.MixerInputEnded += Mixer_MixerInputEnded;
			volumeProvider = new VolumeSampleProvider(mixer);
			outputDevice.Init(volumeProvider);
			outputDevice.Play();

			Application.ApplicationExit += (s, e) => outputDevice.Dispose();
		}

        private void Mixer_MixerInputEnded(object sender, SampleProviderEventArgs e)
        {
            if (e.SampleProvider is CachedSoundSampleProvider csp && csp.CachedSound.Loop && !csp.CachedSound.Paused)
            {
				csp.Position = 0;
				AddMixerInput(e.SampleProvider);
			}
        }

        private static float volume;
		public static float Volume
		{
			get { return volume; }
			set
			{
				volume = value;
				Instance.volumeProvider.Volume = (float)Math.Pow(volume, 2);
			}
		}

		internal static void Initialize()
        {
            PreloadAllSounds();
        }

        private ISampleProvider ConvertToRightChannelCount(ISampleProvider input)
		{
			if (input.WaveFormat.Channels == mixer.WaveFormat.Channels)
			{
				return input;
			}
			if (input.WaveFormat.Channels == 1 && mixer.WaveFormat.Channels == 2)
			{
				return new MonoToStereoSampleProvider(input);
			}
			throw new NotImplementedException("Not yet implemented this channel count conversion");
		}

        public static CachedSound GetSound(string resourceName)
        {
            if (!Sounds.ContainsKey(resourceName))
                throw new Exception(string.Format("Sound {0} was not found. Was it not loaded?", resourceName));

            return Sounds[resourceName];
        }

		public static void StopAllSounds()
        {
			Instance.mixer.RemoveAllMixerInputs();
        }

		private IEnumerable<CachedSoundSampleProvider> InputsForSound(CachedSound sound)
		{
			return mixer.MixerInputs.OfType<CachedSoundSampleProvider>().Where(p => p.CachedSound == sound);
		}

		public static void PlaySound(CachedSound sound, bool singleInstance)
		{
			if (singleInstance && Instance.InputsForSound(sound).Any())
            {
				return;
            }

			Instance.AddMixerInput(new CachedSoundSampleProvider(sound));
		}

		public static void StopSound(CachedSound sound)
		{
			// Must tolist here to avoid breaking the enumerable.
			foreach (var input in Instance.InputsForSound(sound).ToList())
			{
				Instance.mixer.RemoveMixerInput(input);
			}
		}

		private void AddMixerInput(ISampleProvider input)
		{
			mixer.AddMixerInput(ConvertToRightChannelCount(input));
		}

		public void Dispose()
		{
			outputDevice.Dispose();
		}


        private static readonly Dictionary<string, CachedSound> Sounds = 
			new Dictionary<string, CachedSound>(StringComparer.OrdinalIgnoreCase);

        internal static void PreloadAllSounds()
        {
            Assembly thisExe = Assembly.GetExecutingAssembly();

            foreach (string resourceName in thisExe.GetManifestResourceNames())
            {
                if (resourceName.EndsWith(".ogg") && !Sounds.ContainsKey(resourceName))
                {
                    Sounds[resourceName] = new CachedSound(resourceName);
                }
            }
		}
    }
}
