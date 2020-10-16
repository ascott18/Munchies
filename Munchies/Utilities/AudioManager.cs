using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using IrrKlang;
using System.Windows.Forms;
using AndrewScott.SimpleCommandManager;

namespace Munchies
{
	internal static class AudioManager
	{
		public class Sound
		{
			internal ISoundSource source;
			internal ISound ISound;

			public bool SingleInstanceMode;


			#region Construction and Loading

			public Sound(string resourceName)
			{
				LoadSoundFile(resourceName);
			}

			public Sound(Stream file, string resourceName)
			{
				LoadSoundFile(file, resourceName);
			}

			public void LoadSoundFile(string resourceName)
			{
				Assembly thisExe = Assembly.GetExecutingAssembly();

				if (thisExe.GetManifestResourceNames().Contains(resourceName))
				{
					Stream file = thisExe.GetManifestResourceStream(resourceName);

					LoadSoundFile(file, resourceName);
				}
				else
				{
					throw new FileNotFoundException("Couldn't find the requested sound file in the assembly", resourceName);
				}
			}

			public void LoadSoundFile(Stream file, string resourceName)
			{
				source = engine.AddSoundSourceFromIOStream(file, resourceName);
			}

			public void Play()
			{
				Play(false);
			}

			#endregion


			#region Control

			public void Play(bool looping)
			{
				if (SingleInstanceMode)
					Stop();

				if (looping && !SingleInstanceMode)
					throw new InvalidOperationException("Cannot loop a sound that isn't in SingleInstanceMode");

				ISound = engine.Play2D(source, looping, false, false);
			}

			public void Pause()
			{
				if (SingleInstanceMode && ISound != null)
				{
					ISound.Paused = true;
				}
			}

			public void Resume()
			{
				if (SingleInstanceMode && ISound != null)
				{
					ISound.Paused = false;
				}
			}

			public void PlayOrResumeLoop()
			{
				if (SingleInstanceMode)
				{
					if (ISound == null)
						Play(true);
					else
						Resume();
				}
			}

			public void Stop()
			{
				if (ISound != null)
					//if (SingleInstanceMode && ISound != null)
				{
					ISound.Stop();
					ISound.Dispose();

					ISound = null;
				}
			}

			#endregion
		}

		private static ISoundEngine engine;


		internal static void Initialize(int maxVolume)
		{
			engine = new ISoundEngine();

			volumeLevelMax = maxVolume;

			Program.Settings.DeclareDefault("SoundVolume", (volumeLevelMax / 2) + 1);

			Volume = (int)Program.Settings.GetSetting("SoundVolume") / (float)volumeLevelMax;


			PreloadAllSounds();
		}


		public static void InitializeVolumeMenu(ToolStripMenuItem parentMenu)
		{
			for (int i = 0; i <= volumeLevelMax; i++)
			{
                ToolStripMenuItem Item = new ToolStripMenuItem
                {
                    Size = new System.Drawing.Size(91, 22),
                    Text = i.ToString(),
                    ShortcutKeyDisplayString = "Ctrl+" + i
                };

                parentMenu.DropDownItems.Add(Item);

				// Needed to pass a unique value to each lambda
				// (otherwise, all of them use the max value of the iteration)
				int thisSoundLevel = i;

				Command cmd = new Command(() =>
				{
					Program.Settings.SetSetting("SoundVolume", thisSoundLevel);
					Volume = thisSoundLevel / (float)volumeLevelMax;
					GetSound("Munchies.Resources.Sounds.exitSound.ogg").Play();
				},
				                          i < 10 ? Keys.Control | Keys.D0 + i : Keys.None,
				                          Item
					)
				{
					Checked = () => (int)Program.Settings.GetSetting("SoundVolume") == thisSoundLevel
				};

				Program.CommandManager.Add(cmd);
			}

			// Special handling for the first volume setting.
			parentMenu.DropDownItems[0].Text = "Off";
		}

		private static readonly Dictionary<String, Sound> Sounds = new Dictionary<String, Sound>(StringComparer.OrdinalIgnoreCase);

		internal static void PreloadAllSounds()
		{
			Assembly thisExe = Assembly.GetExecutingAssembly();


			foreach (string resourceName in thisExe.GetManifestResourceNames())
			{
				if (resourceName.EndsWith(".ogg") || resourceName.EndsWith(".mod"))
					LoadSound(resourceName);
			}
		}

		private static int volumeLevelMax;
		private static float volume;

		public static float Volume
		{
			get { return volume; }
			set
			{
				volume = value;
				engine.SoundVolume = value;
			}
		}

		internal static void LoadSound(string resourceName)
		{
			if (!Sounds.ContainsKey(resourceName))
				Sounds[resourceName] = new Sound(resourceName);
		}


		internal static void StopAllSounds()
		{
			foreach (Sound sound in Sounds.Values)
				sound.Stop();
		}


		public static Sound GetSound(string resourceName)
		{
			if (!Sounds.ContainsKey(resourceName))
				throw new Exception(string.Format("Sound {0} was not found. Was it not loaded?", resourceName));

			return Sounds[resourceName];
		}
	}
}
