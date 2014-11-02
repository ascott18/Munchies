using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munchies
{
	internal class DessertLevel : Level
	{
		private const double SpawnsPerSecond = 3;

		// Constructor
		public DessertLevel(Game game, int levelNumber)
			: base(game, levelNumber, true)
		{
		}

		internal override void Update(double gameTime, double elapsedTime)
		{
			Update_TryToSpawnTreat(gameTime, elapsedTime);

			Update_CheckIfShouldSpawnDoor(gameTime, elapsedTime);

			base.Update(gameTime, elapsedTime);
		}

		internal void Update_CheckIfShouldSpawnDoor(double gameTime, double elapsedTime)
		{
			if (gameTime - LevelStartTime > 25)
				ShowExitAndAllowEnding();
		}

		internal void Update_TryToSpawnTreat(double gameTime, double elapsedTime)
		{
			double rnd = Random.Next((int)(100 * 10e+5)) / 10e+5;

			double ChanceOfSpawnNow = SpawnsPerSecond * 100 * elapsedTime * Game.ScaleFactor1DX;

			if (rnd < ChanceOfSpawnNow)
			{
				Sprite spawn;

				switch (SpawnRandomizer.PickSpawn(new[]
				{
					Math.Max(0, 95 - (int)(gameTime - LevelStartTime)), // 0 Desert
					Math.Max(5, 20 - (int)(gameTime - LevelStartTime)), // 1 Peas
					10 + (int)(gameTime - LevelStartTime) // 2 PlainSkull
				}))
				{
					case 0:
						spawn = new Dessert(this);
						break;
					case 1:
						spawn = new Peas(this);
						break;
					case 2:
						spawn = new DessertLevelPlainSkull(this);
						break;
					default:
						throw new Exception("Unhandled switch case in determining spawn type");
				}


				spawn.Velocity.X = 0;
				spawn.Velocity.Y = Random.Next(80, (int)spawn.MaxVelocityY);

				spawn.Location.X = Random.Next((int)(Game.Size.Width - spawn.Size.Width));
				spawn.Location.Y = -spawn.Size.Height + 0.1f;
			}
		}
	}
}
