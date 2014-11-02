using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Management;
using AndrewScott.SettingsSerializer;
using AndrewScott.SimpleCommandManager;

namespace Munchies
{
	public partial class MainWindow : Form
	{
		# region Content Container Management

		private readonly List<ContentContainer> ContentContainers = new List<ContentContainer>
		{
			new GameContainer(),
			new TitleScreenContainer(),
			new ScoreContainer(),
		};

		private ContentContainer contentContainer;

		public ContentContainer ContentContainer
		{
			get { return contentContainer; }
			set
			{
				// Process the last container
				if (contentContainer != null)
				{
					contentContainer.Visible = false;
					Controls.Remove(contentContainer);
				}

				contentContainer = value;

				if (contentContainer != null)
				{
					contentContainer.Size = new Size(
						Math.Min(Program.ContentSizeSetting.Width, ClientSize.Width),
						Math.Min(Program.ContentSizeSetting.Height, ClientSize.Height)
						);


					// Set the location of the content container in the center of the window.
					contentContainer.Location = new Point(
						(ClientSize.Width - contentContainer.Size.Width) / 2,
						(ClientSize.Height - contentContainer.Size.Height) / 2);

					contentContainer.Anchor = AnchorStyles.None;

					contentContainer.Visible = true;

					Controls.Add(contentContainer);

					Focus();
				}
			}
		}

		#endregion


		#region Initialization

		private void InitializeCommands()
		{
			// Common lambdas used by controls
			Func<bool> NoGameInProgress = () => CurrentGame == null || CurrentGame.HasEnded;


			#region File

			// New Game
			Program.CommandManager.Add(new Command(
				                           NewGame,
				                           Keys.Control | Keys.N,
				                           newGameToolStripMenuItem
				                           )
			{
				Enabled = NoGameInProgress,
			});

			// Pause
			Program.CommandManager.Add(new Command(
				                           Pause,
				                           Keys.Control | Keys.P,
				                           pauseGameToolStripMenuItem
				                           )
			{
				Enabled = () => CurrentGame != null && CurrentGame.Playing,
			});

			// Resume
			Program.CommandManager.Add(new Command(
				                           Play,
				                           Keys.Control | Keys.R,
				                           resumeGameToolStripMenuItem
				                           )
			{
				Enabled = () => CurrentGame != null && !CurrentGame.Playing && !CurrentGame.HasEnded,
			});

			// Toggle play/pause
			Program.CommandManager.Add(new Command(
				                           () => CurrentGame.TogglePlayPause(),
				                           Keys.Space
				                           )
			{
				Enabled = () => CurrentGame != null && CurrentGame.HasStarted && !CurrentGame.HasEnded,
			});
			Program.CommandManager.Add(new Command(
				                           () => CurrentGame.TogglePlayPause(),
				                           Keys.Escape
				                           )
			{
				Enabled = () => CurrentGame != null && CurrentGame.HasStarted && !CurrentGame.HasEnded,
			});

			// End Game
			Program.CommandManager.Add(new Command(
				                           () => CurrentGame.End(),
				                           Keys.Control | Keys.E,
				                           endGameToolStripMenuItem
				                           )
			{
				Enabled = () => CurrentGame != null && CurrentGame.HasStarted && !CurrentGame.HasEnded,
			});

			// Quit
			Program.CommandManager.Add(new Command(
				                           Application.Exit,
				                           Keys.Control | Keys.Q,
				                           quitToolStripMenuItem
				                           ));

			#endregion


			#region Options

			// Music
			Program.CommandManager.Add(new Command(
				                           () =>
				                           {
					                           Program.Settings.SetSetting("MusicEnabled",
					                                                       !(bool)Program.Settings.GetSetting("MusicEnabled"));
					                           CheckMusicPlayingStatus();
				                           },
				                           Keys.Control | Keys.M,
				                           musicToolStripMenuItem
				                           )
			{
				Checked = () => (bool)Program.Settings.GetSetting("MusicEnabled")
			});


			// Slow Food
			Program.CommandManager.Add(new Command(
				                           () => Program.Settings.SetSetting("FoodSpeed", Food.FoodSpeed.Slow),
				                           slowFoodToolStripMenuItem
				                           )
			{
				Enabled = NoGameInProgress,
				Checked = () => (Food.FoodSpeed)Program.Settings.GetSetting("FoodSpeed") == Food.FoodSpeed.Slow
			});
			// Fast Food
			Program.CommandManager.Add(new Command(
				                           () => Program.Settings.SetSetting("FoodSpeed", Food.FoodSpeed.Fast),
				                           fastFoodToolStripMenuItem
				                           )
			{
				Enabled = NoGameInProgress,
				Checked = () => (Food.FoodSpeed)Program.Settings.GetSetting("FoodSpeed") == Food.FoodSpeed.Fast
			});

			// Beginner
			Program.CommandManager.Add(new Command(
				                           () => GameMode.DifficultySetting = Game.GameDifficulty.Beginner,
				                           beginnerToolStripMenuItem
				                           )
			{
				Enabled = NoGameInProgress,
				Checked = () => GameMode.DifficultySetting == Game.GameDifficulty.Beginner
			});
			// Expert
			Program.CommandManager.Add(new Command(
				                           () => GameMode.DifficultySetting = Game.GameDifficulty.Expert,
				                           expertToolStripMenuItem
				                           )
			{
				Enabled = NoGameInProgress,
				Checked = () => GameMode.DifficultySetting == Game.GameDifficulty.Expert
			});

			#endregion


			#region Scores

			// Show Scores
			Program.CommandManager.Add(new Command(
				                           ShowScores,
				                           showScoresToolStripMenuItem
				                           )
			{
				Enabled = NoGameInProgress,
			});

			// Reset Scores
			Program.CommandManager.Add(new Command(
				                           () =>
				                           {
					                           var dialog = new ScoreResetConfirmDialog();

					                           if (dialog.ShowDialog(this) == DialogResult.OK)
						                           ShowScores();
				                           },
				                           resetScoresToolStripMenuItem
				                           )
			{
				Enabled = NoGameInProgress,
			});

			// A command that handles the entire Scores menu so that it will be disabled while in a game.
			Program.CommandManager.Add(new Command(
				                           () => { },
				                           scoreToolStripMenuItem
				                           )
			{
				Enabled = NoGameInProgress,
			});

			#endregion


			#region Level

			// Level selection menu
			for (int levelNumber = 1; levelNumber <= 41; levelNumber += 5)
			{
				// Define a local-scope reference to the level number
				// that will persist for the Command's lambdas.
				int levelNumberRef = levelNumber;


				// Create the menu item for the level
				ToolStripMenuItem menuItem = new ToolStripMenuItem();

				menuItem.Text = levelNumber.ToString();

				levelToolStripMenuItem.DropDownItems.Add(menuItem);


				// Create the command handler for the level
				Program.CommandManager.Add(new Command(
					                           () => GameMode.GameModeFromSettings.StartingLevel = levelNumberRef,
					                           menuItem
					                           )
				{
					Checked = () => GameMode.GameModeFromSettings.StartingLevel == levelNumberRef,
					Enabled = () => NoGameInProgress() && GameMode.GameModeFromSettings.HighestLevelAttained >= levelNumberRef,
				});
			}

			// A command that handles the entire Level menu so that it will be disabled while in a game.
			Program.CommandManager.Add(new Command(
				                           () => { },
				                           levelToolStripMenuItem
				                           )
			{
				Enabled = NoGameInProgress,
			});

			#endregion


			#region Window

			// Window menu (screen resolutions)
			{
				// A list that will hold all the available resolutions
				// Included are the four resolutions from the original game.
				List<Size> Resolutions = new List<Size>
				{
					new Size(512, 342),
					new Size(512, 384),
					new Size(640, 400),
					new Size(640, 480)
				};

				/*
                // Add all common resolutions to the list.
                // Source is http://stackoverflow.com/questions/1528266/list-of-valid-resolutions-for-a-given-screen
                var scope = new System.Management.ManagementScope();
                var query = new System.Management.ObjectQuery("SELECT * FROM CIM_VideoControllerResolution");

                using (var searcher = new System.Management.ManagementObjectSearcher(scope, query))
                {
                    foreach (var result in searcher.Get())
                    {
                        Size resolution = new Size((int)(uint)result["HorizontalResolution"], (int)(uint)result["VerticalResolution"]);

                        if (!Resolutions.Contains(resolution))
                            Resolutions.Add(resolution);
                    }
                }
                */

				// Process all of the resolutions in the list,
				// creating tooltip entries and Command objects for each.
				foreach (Size resolution in Resolutions)
				{
					// Accessing foreach variables within closures is bad.
					var res = resolution;

					// Create the tooltip items for each resolution
					ToolStripMenuItem menuItem = new ToolStripMenuItem();

					menuItem.Text = string.Format("{0}x{1}", resolution.Width, resolution.Height);

					windowToolStripMenuItem.DropDownItems.Add(menuItem);


					// Create the command handler for the resolution.
					Program.CommandManager.Add(new Command(
						                           () => Program.ContentSizeSetting = res,
						                           menuItem
						                           )
					{
						Checked = () => Program.ContentSizeSetting == res,
						Enabled = NoGameInProgress,
					});
				}

				// A command that handles the entire Window menu so that it will be disabled while in a game.
				Program.CommandManager.Add(new Command(
					                           () => { },
					                           windowToolStripMenuItem
					                           )
				{
					Enabled = NoGameInProgress,
				});
			}

			#endregion


			#region Help

			//Instructions
			Program.CommandManager.Add(new Command(
				                           () => new HelpDialogs.HelpInstructionsDialog().ShowDialog(this),
				                           instructionsToolStripMenuItem));

			//Enemies
			Program.CommandManager.Add(new Command(
				                           () => new HelpDialogs.HelpEnemiesDialog().ShowDialog(this),
				                           enemiesToolStripMenuItem));

			//Bonuses
			Program.CommandManager.Add(new Command(
				                           () => new HelpDialogs.HelpBonusesDialog().ShowDialog(this),
				                           bonusesToolStripMenuItem));

			//About
			Program.CommandManager.Add(new Command(
				                           () => new HelpDialogs.HelpAboutDialog().ShowDialog(this),
				                           aboutToolStripMenuItem));

			#endregion
		}

		public MainWindow()
		{
			InitializeComponent();

			InitializeCommands();

			// Calculate the initial size and minimum size of the window.
			Size windowPadding = Size.Subtract(Size, ClientSize);
			MinimumSize = Size.Add(windowPadding, Program.ContentSizeSetting);
			Size = MinimumSize;

			// Show the title screen 
			ContentContainer = ContentContainers.First(c => c is TitleScreenContainer);

			Program.Settings.DeclareDefault("MusicEnabled", false);
			Program.Settings.DeclareDefault("FoodSpeed", Food.FoodSpeed.Fast);

			// Restore the maximized state of the game from the last time it was open.
			Program.Settings.DeclareDefault("Maximized", false);
			bool Maximized = (bool)Program.Settings.GetSetting("Maximized");
			WindowState = Maximized ? FormWindowState.Maximized : FormWindowState.Normal;
			FormClosing += MainWindow_FormClosing;

			// Initialize the audio manager
			AudioManager.Initialize(7);
			AudioManager.InitializeVolumeMenu(soundToolStripMenuItem);
		}

		private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Record the window state in the settings so that it may be restored
			// the next time that the program is opened.
			Program.Settings.SetSetting("Maximized", WindowState == FormWindowState.Maximized);
		}

		#endregion


		# region Game Management

		private Game currentGame;

		/// <summary>
		///     Holds the game that is currently in progress.
		/// </summary>
		public Game CurrentGame
		{
			get { return currentGame; }
			set
			{
				currentGame = value;

				// Handle the new game
				if (currentGame != null)
				{
					currentGame.OnPlay += currentGame_OnPlay;
					currentGame.OnPause += currentGame_OnPause;

					currentGame.Ended += currentGame_Ended;
				}
			}
		}

		/// <summary>
		///     Starts a new game.
		/// </summary>
		public void NewGame()
		{
			GameContainer container = (GameContainer)ContentContainers.First(c => c is GameContainer);

			GameMode mode = GameMode.GameModeFromSettings;

			Size windowPadding = Size.Subtract(Size, ClientSize);
			MinimumSize = Size.Add(windowPadding, mode.ContainerSize);

			CurrentGame = new Game(container, mode);

			container.Game = CurrentGame;

			ContentContainer = container;

			CurrentGame.Start();
		}

		/// <summary>
		///     Plays or resumes that current game.
		/// </summary>
		public void Play()
		{
			if (CurrentGame != null) CurrentGame.Play();
		}

		/// <summary>
		///     Pauses the current game.
		/// </summary>
		public void Pause()
		{
			if (CurrentGame != null) CurrentGame.Pause(true);
		}

		private void currentGame_OnPlay(object sender, EventArgs e)
		{
			menuStrip1.Visible = false;

			CheckMusicPlayingStatus();
		}

		private void currentGame_OnPause(object sender, EventArgs e)
		{
			menuStrip1.Visible = true;

			CheckMusicPlayingStatus();
		}

		private void currentGame_Ended(object sender, EventArgs e)
		{
			Game game = sender as Game;

			AudioManager.StopAllSounds();

			// Check to see if the score is a high score.
			// If it is, request the player's name and then show the high scores dialog.
			if (game.GameMode.Scores.IsScoreRankworthy(game.ScorePoints))
			{
				Score score = new Score
				{
					Points = game.ScorePoints,
					Level = game.CurrentLevel.LevelNumber,
				};

				AudioManager.GetSound("Munchies.Resources.Sounds.hiScore.ogg").Play();

				HighScoreDialog dialog = new HighScoreDialog(game.GameMode, score);
				dialog.Location = new Point(0, 0);

				dialog.ShowDialog(this);

				ShowScores(score);
			}

			// This is neede to make sure that all the menu items reflect the proper enabled state.
			menuStrip1.Refresh();
		}

		/// <summary>
		///     Automatically pauses the game when the main window loses focus.
		/// </summary>
		private void Form1_Deactivate(object sender, EventArgs e)
		{
			Pause();
		}

		#endregion


		/// <summary>
		///     Checks the current state of the background music.
		/// </summary>
		public void CheckMusicPlayingStatus()
		{
			AudioManager.Sound Music = AudioManager.GetSound("Munchies.Resources.Sounds.Music.mod");
			Music.SingleInstanceMode = true;

			if ((bool)Program.Settings.GetSetting("MusicEnabled")
			    && CurrentGame != null
			    && CurrentGame.HasStarted)
			{
				if (CurrentGame.Playing)
				{
					// Play the music if the user has enabled it and the current game is in progress.
					Music.PlayOrResumeLoop();
				}
				else
				{
					// Pauses the music if the game is paused
					Music.Pause();
				}
			}
			else
			{
				// Stops the music if the game is not in progress or the user has disabled music.
				Music.Stop();
			}
		}


		#region Scores

		/// <summary>
		///     Shows the high scores dialog.
		/// </summary>
		public void ShowScores()
		{
			ScoreContainer container = (ScoreContainer)ContentContainers.First(c => c is ScoreContainer);

			container.CurrentGameMode = GameMode.GameModeFromSettings;

			ContentContainer = container;

			container.HighlightedScore = null;
		}

		/// <summary>
		///     Shows thie high scores dialog and bolds the specified score.
		/// </summary>
		/// <param name="score">A Score that will be bolded in the dialog when it is shown.</param>
		public void ShowScores(Score score)
		{
			ShowScores();

			ScoreContainer container = (ScoreContainer)ContentContainer;

			container.HighlightedScore = score;
		}

		#endregion


		/// <summary>
		///     Implementation of CommandManager from the SimpleCommandManager project
		/// </summary>
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (Program.CommandManager.ProcessCmdKey(ref msg, keyData))
				return true;

			return base.ProcessCmdKey(ref msg, keyData);
		}
	}

	public static class SpawnRandomizer
	{
		private static readonly Random random = new Random();

		/// <summary>
		///     Given an array of integers where each integer represents the proportion to the sum of the array that that index
		///     should be chosen, pick an index and return it.
		/// </summary>
		/// <param name="chances">The integer array that contains the proportions at which each index should be chosen.</param>
		/// <returns>The index of the array that has been chosen.</returns>
		public static int PickSpawn(int[] chances)
		{
			int rnd = random.Next(chances.Sum());

			int runningSum = 0;
			for (int index = 0; index < chances.Length; index++)
			{
				runningSum += chances[index];
				if (runningSum > rnd)
					return index;
			}
			return 0;
		}
	}
}
