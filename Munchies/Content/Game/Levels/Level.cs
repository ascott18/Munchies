using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Drawing;
using System.Windows.Forms;

namespace Munchies
{
	public abstract class Level
	{
		// Reference to the game that the level was created for
		public readonly Game Game;

		protected static readonly Random Random = new Random();

		public readonly List<Sprite> LevelSprites = new List<Sprite>();


		public readonly int LevelNumber;
		public readonly bool IsDesert;
		public readonly double LevelStartTime;

		public int FoodSpawned = 0;
		public int SkullsSpawned = 0;
		protected bool IsFinished;

		private LevelExit LevelExit;
		public bool PeaIsActive;
		public readonly Image BackgroundImage;

		public Level(Game game, int levelNumber, bool isDesert)
		{
			Game = game;
			LevelNumber = levelNumber;
			IsDesert = isDesert;

			LevelStartTime = game.GameTime;

			BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject(string.Format("BG{0}", Random.Next(16) + 1));
		}

		internal bool IsNextLevelDesert()
		{
			return (LevelNumber % 5 == 0 && !IsDesert);
		}

		internal virtual void Update(double gameTime, double elapsedTime)
		{
			if (IsFinished)
			{
				if (Game.Melvin.Location.X >= LevelExit.Location.X
				    && Game.Melvin.Location.Y >= LevelExit.Location.Y)
				{
					TriggerNextLevel();
				}
			}
		}

		private void TriggerNextLevel()
		{
			Game.TriggerNextLevelTransition();
		}


		public void ShowExitAndAllowEnding()
		{
			if (!IsFinished)
			{
				IsFinished = true;
				LevelExit = new LevelExit(this);
			}
		}

		internal void Draw(Graphics graphics)
		{
			// Draw miscellaneous (includes the exit door and peas)
			foreach (Sprite sprite in LevelSprites.Where(sprite => !(sprite is Edible || sprite is Enemy)))
			{
				sprite.Draw(graphics);
			}

			// Draw edibles(they should be above miscellaneous)
			foreach (Sprite sprite in LevelSprites.Where(sprite => sprite is Edible))
			{
				sprite.Draw(graphics);
			}

			// Draw enemies (they should be above everything else)
			foreach (Sprite sprite in LevelSprites.Where(sprite => sprite is Enemy))
			{
				sprite.Draw(graphics);
			}
		}
	}
}
