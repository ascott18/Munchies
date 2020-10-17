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

	class CachedSoundSampleProvider : ISampleProvider
	{
		public CachedSound CachedSound { get; }
		public long Position { get; set;  }

		public CachedSoundSampleProvider(CachedSound cachedSound)
		{
			this.CachedSound = cachedSound;
		}

		public int Read(float[] buffer, int offset, int count)
		{
			if (CachedSound.Paused)
			{
				Array.Fill(buffer, 0, offset, count);
				return count;
			}

			var availableSamples = CachedSound.AudioData.Length - Position;
			var samplesToCopy = Math.Min(availableSamples, count);
			Array.Copy(CachedSound.AudioData, Position, buffer, offset, samplesToCopy);
			Position += samplesToCopy;
			return (int)samplesToCopy;
		}

		public WaveFormat WaveFormat { get { return CachedSound.WaveFormat; } }
	}

	//internal static class AudioManager
	//{
	//	public class Sound
	//	{
	//		internal ISampleProvider source;
	//		internal WaveOutEvent ISound;

	//		public bool SingleInstanceMode;


	//		#region Construction and Loading

	//		public Sound(string resourceName)
	//		{
	//			LoadSoundFile(resourceName);
	//		}

	//		public Sound(Stream file, string resourceName)
	//		{
	//			LoadSoundFile(file, resourceName);
	//		}

	//		public void LoadSoundFile(string resourceName)
	//		{
	//			Assembly thisExe = Assembly.GetExecutingAssembly();

	//			if (thisExe.GetManifestResourceNames().Contains(resourceName))
	//			{
	//				Stream file = thisExe.GetManifestResourceStream(resourceName);

	//				LoadSoundFile(file, resourceName);
	//			}
	//			else
	//			{
	//				throw new FileNotFoundException("Couldn't find the requested sound file in the assembly", resourceName);
	//			}
	//		}

	//		public void LoadSoundFile(Stream file, string resourceName)
	//		{
	//			source = new VorbisWaveReader(file, true);
	//		}

	//		public void Play()
	//		{
	//			Play(false);
	//		}

	//		#endregion


	//		#region Control

	//		public void Play(bool looping)
	//		{
	//			if (SingleInstanceMode)
	//				Stop();

	//			if (looping && !SingleInstanceMode)
	//				throw new InvalidOperationException("Cannot loop a sound that isn't in SingleInstanceMode");

	//			ISound = new WaveOutEvent();
	//			ISound.Init(source);
	//			ISound.Play();
	//		}

	//		public void Pause()
	//		{
	//			if (SingleInstanceMode && ISound != null)
	//			{
	//				ISound.Pause();
	//			}
	//		}

	//		public void Resume()
	//		{
	//			if (SingleInstanceMode && ISound != null)
	//			{
	//				ISound.Play();
	//			}
	//		}

	//		public void PlayOrResumeLoop()
	//		{
	//			if (SingleInstanceMode)
	//			{
	//				if (ISound == null)
	//					Play(true);
	//				else
	//					Resume();
	//			}
	//		}

	//		public void Stop()
	//		{
	//			if (ISound != null)
	//				//if (SingleInstanceMode && ISound != null)
	//			{
	//				ISound.Stop();
	//				ISound.Dispose();

	//				ISound = null;
	//			}
	//		}

	//		#endregion
	//	}


	//	internal static void Initialize(int maxVolume)
	//	{
	//		volumeLevelMax = maxVolume;

	//		Program.Settings.DeclareDefault("SoundVolume", (volumeLevelMax / 2) + 1);

	//		Volume = (int)Program.Settings.GetSetting("SoundVolume") / (float)volumeLevelMax;


	//		PreloadAllSounds();
	//	}


	//	public static void InitializeVolumeMenu(ToolStripMenuItem parentMenu)
	//	{
	//		for (int i = 0; i <= volumeLevelMax; i++)
	//		{
 //               ToolStripMenuItem Item = new ToolStripMenuItem
 //               {
 //                   Size = new System.Drawing.Size(91, 22),
 //                   Text = i.ToString(),
 //                   ShortcutKeyDisplayString = "Ctrl+" + i
 //               };

 //               parentMenu.DropDownItems.Add(Item);

	//			// Needed to pass a unique value to each lambda
	//			// (otherwise, all of them use the max value of the iteration)
	//			int thisSoundLevel = i;

	//			Program.CommandManager.Add(new Command(
	//				() => {
	//					Program.Settings.SetSetting("SoundVolume", thisSoundLevel);
	//					Volume = thisSoundLevel / (float)volumeLevelMax;
	//					GetSound("Munchies.Resources.Sounds.exitSound.ogg").Play();
	//				},
	//			    i < 10 ? Keys.Control | Keys.D0 + i : Keys.None,
	//			    Item
	//			)
	//			{
	//				Checked = () => (int)Program.Settings.GetSetting("SoundVolume") == thisSoundLevel
	//			});
	//		}

	//		// Special handling for the first volume setting.
	//		parentMenu.DropDownItems[0].Text = "Off";
	//	}

	//	private static readonly Dictionary<string, Sound> Sounds = new Dictionary<string, Sound>(StringComparer.OrdinalIgnoreCase);

	//	internal static void PreloadAllSounds()
	//	{
	//		Assembly thisExe = Assembly.GetExecutingAssembly();


	//		foreach (string resourceName in thisExe.GetManifestResourceNames())
	//		{
	//			if (resourceName.EndsWith(".ogg"))
	//				LoadSound(resourceName);
	//		}
	//	}

	//	private static int volumeLevelMax;
	//	private static float volume;

	//	public static float Volume
	//	{
	//		get { return volume; }
	//		set
	//		{
	//			volume = value;
	//			foreach (Sound sound in Sounds.Values)
	//			{
	//				if (sound.ISound != null)
 //                   {
	//					sound.ISound.Volume = value;
	//				}
	//			}
	//		}
	//	}

	//	internal static void LoadSound(string resourceName)
	//	{
	//		if (!Sounds.ContainsKey(resourceName))
	//			Sounds[resourceName] = new Sound(resourceName);
	//	}


	//	internal static void StopAllSounds()
	//	{
	//		foreach (Sound sound in Sounds.Values)
 //           {
 //               sound.Stop();
 //           }
 //       }


	//	public static Sound GetSound(string resourceName)
	//	{
	//		if (!Sounds.ContainsKey(resourceName))
	//			throw new Exception(string.Format("Sound {0} was not found. Was it not loaded?", resourceName));

	//		return Sounds[resourceName];
	//	}
	//}
}
