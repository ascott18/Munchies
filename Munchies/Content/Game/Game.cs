﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Media;

namespace Munchies
{
	public class Game : Panel
	{
		public Melvin Melvin;
		public readonly GameContainer GameContainer;

		public readonly GameMode GameMode;

		private Level currentLevel;

		public Level CurrentLevel
		{
			get { return currentLevel; }
			set
			{
				currentLevel = value;

				if (currentLevel != null)
				{
					BackgroundImage = currentLevel.BackgroundImage;
					GameContainer.toolStripStatus_Level.Text = currentLevel.LevelNumber.ToString();
				}
			}
		}

		public readonly List<Sprite> AllSprites = new List<Sprite>();
		public readonly List<Sprite> GameSprites = new List<Sprite>();

		public float ScaleFactor1DX = 1;
		public float ScaleFactor1DY = 1;
		public float ScaleFactor2D = 1;

		// ScorePoints
		private int scorePoints;

		public int ScorePoints
		{
			set
			{
				if (ScorePoints / 2500 < value / 2500)
					Lives++;

				scorePoints = value;

				GameContainer.toolStripStatus_Points.Text = (scorePoints).ToString();
			}
			get { return scorePoints; }
		}

		// Lives
		private int lives;

		public int Lives
		{
			set
			{
				// Only play the life gained sound if we got exactly one life.
				// We get six lives when the level starts, but the sound shouldn't be played then.
				if (lives + 1 == value)
					AudioManager.GetSound("Munchies.Resources.Sounds.extraLife.ogg").Play();

				lives = value;
				GameContainer.toolStripStatus_Lives.Text = Math.Max(0, value).ToString();
			}
			get { return lives; }
		}

		// Timing and frame control
		private readonly Stopwatch timer = new Stopwatch();
		private double lastTime;
		public double GameTime;

		public enum GameDifficulty
		{
			Beginner,
			Expert,
		}

		public Game(GameContainer container, GameMode gameMode)
		{
			GameMode = gameMode;

			GameContainer = container;

			// Control setup and styling
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

			// The game update engine
			Paint += Engine;

			Click += Game_Click;

			// 6 lives to start out with because one is consumed instantly.
			Lives = 6;

			ScorePoints = 0;
		}

		public static readonly Size BaseScaleSize = new Size(640, 456);

		public bool HasStarted;
		public event EventHandler Started;

		public void Start()
		{
			ScaleFactor1DX = (float)Size.Width / BaseScaleSize.Width;
			ScaleFactor1DY = (float)Size.Height / BaseScaleSize.Height;
			ScaleFactor2D = ScaleFactor1DX * ScaleFactor1DY;

			HasStarted = true;

			SpawnMelvin();

			// Create the first level
			CurrentLevel = new FoodLevel(this, GameMode.StartingLevel);

			Play();

            Started?.Invoke(this, EventArgs.Empty);
        }

		public bool HasEnded;
		public event EventHandler Ended;

		public void End()
		{
			HasEnded = true;

			Pause(false);

			GameMode.HighestLevelAttained = Math.Max(GameMode.HighestLevelAttained, CurrentLevel.LevelNumber);

            Ended?.Invoke(this, EventArgs.Empty);
        }

		private void SpawnMelvin()
		{
			Lives--;

			if (Lives > 0)
				Melvin = new Melvin(this);
			else
				End();
		}

		private void Game_Click(object sender, EventArgs e)
		{
			if (Playing)
				Melvin.ShootPea();
		}


		private static readonly Dictionary<string, Image> statusDisplays = new Dictionary<string, Image>();

		private static Image GetStatusDisplay(Graphics graphics, string text)
		{
			Image result;

			// See if we've already made this status display.
			if (statusDisplays.TryGetValue(text, out result))
				return result;

			// Set up font. 
			Font stringFont = new Font("Arial", 18);

			// Measure the string.
			SizeF stringSize = graphics.MeasureString(text, stringFont);


			// Create the status that we are going to be drawing
			const int Padding = 30;
			const int Height = 42;
			const int SideWidth = 18;
			result = new Bitmap((int)stringSize.Width + Padding, Height);

			// Compose the image that we will drawn
			using (Graphics g = Graphics.FromImage(result))
			{
				// Draw the background to the image
				using (Bitmap sourceImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("LevelTransition"))
				using (Bitmap source = new Bitmap(sourceImage))
				{
					// Determine and create the images that make up the background
					Image leftImage = source.Clone(
						new Rectangle(0, 0, SideWidth, Height), source.PixelFormat);

					Image rightImage = source.Clone(
						new Rectangle(sourceImage.Size.Width - SideWidth, 0, SideWidth, Height),
						source.PixelFormat);

					Image centerStrip = source.Clone(
						new Rectangle(SideWidth + 1, 0, 1, Height), source.PixelFormat);

					// Draw the left and right sides
					g.DrawImage(leftImage, 0, 0);
					g.DrawImage(rightImage, result.Size.Width - SideWidth, 0);

					// Draw the middle
					for (int i = SideWidth; i < result.Size.Width - SideWidth; i++)
						g.DrawImage(centerStrip, i, 0);
				}


				// Draw string to the image.
				g.DrawString(text, stringFont, Brushes.Black, new PointF(Padding / 2, ((result.Size - stringSize).Height / 2) + 1));
			}

			statusDisplays[text] = result;
			return result;
		}

		private void DrawStatusDisplay(Graphics graphics, string text)
		{
			var statusDisplay = GetStatusDisplay(graphics, text);

			// Draw the final image.
			graphics.DrawImage(statusDisplay,
			                   (Size.Width - statusDisplay.Size.Width) / 2,
			                   (Size.Height - statusDisplay.Size.Height) / 2);
		}

		internal delegate void GameUpdateEvent(Graphics graphics, double gameTime, double elapsedTime);

		internal event GameUpdateEvent Updated;

		private const int BaseSleepTimeMS = 5;

		private void Engine(object sender, PaintEventArgs e)
		{
			// Timing control
			long StartTimeMS = timer.ElapsedMilliseconds;

			GameTime = StartTimeMS / 1000.0;

			double elapsedTime = GameTime - lastTime;
			lastTime = GameTime;


			// Game state update
			if (Playing && !HasEnded)
				Update(GameTime, elapsedTime);


			// Draw everything
			Draw(e.Graphics);


            // Let interested listerners know that we just updated,
            // and give them access to our Graphics object
            // so that they can draw things if they need to.
            Updated?.Invoke(e.Graphics, GameTime, elapsedTime);


            // Update throttling
            int ElapsedMS = (int)(timer.ElapsedMilliseconds - StartTimeMS + 0.5);

			if (ElapsedMS < BaseSleepTimeMS)
				System.Threading.Thread.Sleep(BaseSleepTimeMS - ElapsedMS);


			//Force the next Paint()
			Invalidate();
		}


		#region Play/Pause Handling

		public bool Playing { get; private set; }

		public event EventHandler OnPlay;

		public void Play()
		{
			if (Playing) return;

			Playing = true;

			Updated -= Game_Updated_Paused;

			timer.Start();

			Cursor.Hide();

			Invalidate();

            OnPlay?.Invoke(this, EventArgs.Empty);
        }

		public event EventHandler OnPause;

		public void Pause(bool ShowGraphic)
		{
			if (!Playing) return;

			Playing = false;

			timer.Stop();

			Cursor.Show();

            OnPause?.Invoke(this, EventArgs.Empty);

            if (ShowGraphic)
				Updated += Game_Updated_Paused;
		}

		public void TogglePlayPause()
		{
			if (Playing)
				Pause(true);
			else
				Play();
		}

		private void Game_Updated_Paused(Graphics graphics, double gameTime, double elapsedTime)
		{
			DrawStatusDisplay(graphics, "Paused");
		}

		#endregion


		public virtual void Update(double gameTime, double elapsedTime)
		{
			if (NextLevel == null && Melvin.IsDead)
			{
				SpawnMelvin();
			}


			foreach (Sprite sprite in AllSprites.Reverse<Sprite>())
			{
				sprite.Update(gameTime, elapsedTime);

				foreach (Sprite sprite2 in AllSprites.Reverse<Sprite>())
				{
					if (sprite.TestCollision(sprite2))
					{
						sprite.OnCollide(sprite2);
					}
				}
			}

			CurrentLevel.Update(gameTime, elapsedTime);
		}

		public virtual void Draw(Graphics graphics)
		{
			foreach (Sprite sprite in GameSprites)
			{
				sprite.Draw(graphics);
			}

			CurrentLevel.Draw(graphics);
		}


		#region Level Management

		private Level NextLevel;

		internal void TriggerNextLevelTransition()
		{
			if (NextLevel == null)
			{
				int oldLevelNumber = CurrentLevel.LevelNumber;

				if (CurrentLevel.IsNextLevelDesert())
					NextLevel = new DessertLevel(this, oldLevelNumber);
				else
					NextLevel = new FoodLevel(this, oldLevelNumber + 1);

				Updated += LevelTriggering_Stage1;
			}
		}

		internal void LevelTriggering_Stage1(Graphics graphics, double gameTime, double elapsedTime)
		{
			Updated -= LevelTriggering_Stage1;
			Updated += LevelTriggering_Stage2;

			AudioManager.GetSound("Munchies.Resources.Sounds.levelsound.ogg").Play();

			if (NextLevel is DessertLevel)
				DrawStatusDisplay(graphics, string.Format("Prepare for Dessert!"));
			else
				DrawStatusDisplay(graphics, string.Format("Prepare for Level {0}", NextLevel.LevelNumber));

			AllSprites.RemoveAll(CurrentLevel.LevelSprites.Contains);
		}

		internal void LevelTriggering_Stage2(Graphics graphics, double gameTime, double elapsedTime)
		{
			Updated -= LevelTriggering_Stage2;

			timer.Stop();

			System.Threading.Thread.Sleep(1500);

			timer.Start();

			CurrentLevel = NextLevel;
			NextLevel = null;

			Melvin.SetLoc_AtBottomOfGame();
			Melvin.ResetVelocity();
		}

		#endregion
	}
}
