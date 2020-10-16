using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Munchies
{
	internal class FoodLevel : Level
	{
		// Constructor
		public FoodLevel(Game game, int levelNumber)
			: base(game, levelNumber, false)
		{
			// Added 0.5 to these values for nearest-int rounding

			totalNumFoodToSpawn = (int)(20 + ((levelNumber / 2) * game.ScaleFactor2D) + 0.5);

			if (LevelNumber <= 4)
				totalNumSkullsToSpawn = 2 + LevelNumber;
			else
				totalNumSkullsToSpawn = 6 + (LevelNumber / 5);

			totalNumSkullsToSpawn = (int)((totalNumSkullsToSpawn * game.ScaleFactor2D) + 0.5);

			maxNumSimultaneousSkulls = (int)((6 * game.ScaleFactor2D) + 0.5);
			maxNumSimultaneousFood = (int)((7 * game.ScaleFactor2D) + 0.5);

			for (int i = 0; i < maxNumSimultaneousFood; i++)
			{
				PlainFood food = new PlainFood(this, LevelNumber);
				food.SetLoc_Anywhere();
			}
		}

		internal override void Update(double gameTime, double elapsedTime)
		{
			Update_TryToSpawnTreat(gameTime, elapsedTime);

			Update_TryToSpawnUtensils(gameTime, elapsedTime);

			Update_CheckFoodSpawns(gameTime, elapsedTime);

			Update_CheckSkullSpawns(gameTime, elapsedTime);

			base.Update(gameTime, elapsedTime);
		}


		#region Utensil Spawning

		private readonly Dictionary<Type, double> UtensilKilledTimes = new Dictionary<Type, double>();
		private double LastUtensilSpawnedTime;

		private const int UtensilSpawnFrequency = 1;

		private void Update_TryToSpawnUtensil<T>(Func<Level, T> ctor, double gameTime) where T : Utensil
		{
			// LevelNumber 1-20 : 1
			// LevelNumber 21-25 : 2
			// LevelNumber 26+ : 3

			int MaxNumUtensils = Sprite.Limit(((LevelNumber - 16) / 5) + 1, 1, 3);

			if (LevelSprites.OfType<Utensil>().Count() < MaxNumUtensils
			    && LastUtensilSpawnedTime + UtensilSpawnFrequency < gameTime)
			{
				if (!UtensilKilledTimes.ContainsKey(typeof(T)))
					UtensilKilledTimes[typeof(T)] = gameTime - 7;

				int SpawnFrequency = (int)typeof(T).GetField("SpawnFrequency").GetValue(null);

				if (!LevelSprites.OfType<T>().Any()
				    && UtensilKilledTimes[typeof(T)] + SpawnFrequency < gameTime)
				{
					T utensil = ctor(this);
					utensil.Killed += utensil_Killed;
					LastUtensilSpawnedTime = gameTime;
				}
			}
		}

		private void utensil_Killed(object sender, EventArgs e)
		{
			UtensilKilledTimes[sender.GetType()] = Game.GameTime;

			((Sprite)sender).Killed -= utensil_Killed;
		}

		private void Update_TryToSpawnUtensils(double gameTime, double elapsedTime)
		{
			if (LevelNumber >= Fork.MinimumLevel)
				Update_TryToSpawnUtensil(_ => new Fork(this), gameTime);

			if (LevelNumber >= Knife.MinimumLevel)
				Update_TryToSpawnUtensil(_ => new Knife(this), gameTime);

			if (LevelNumber >= Spoon.MinimumLevel)
				Update_TryToSpawnUtensil(_ => new Spoon(this), gameTime);
		}

		#endregion


		#region Skull Spawning

		private readonly int totalNumSkullsToSpawn;
		private readonly int maxNumSimultaneousSkulls;
		private double lastSmartSkullKillTime = -100;

		internal void Update_CheckSkullSpawns(double gameTime, double elapsedTime)
		{
			// Plain skulls
			while (LevelSprites.OfType<FoodLevelPlainSkull>().Count() < maxNumSimultaneousSkulls
			       && SkullsSpawned < totalNumSkullsToSpawn)
			{
				var skull = new FoodLevelPlainSkull(this);
				skull.SetLoc_SomewhereAboveTop();
			}

			// Smart skulls
			if (LevelNumber > 15
			    && !LevelSprites.OfType<SmartSkull>().Any()
			    && lastSmartSkullKillTime + 10 < gameTime)
			{
				var skull = new SmartSkull(this);
				skull.SetLoc_SomewhereAboveTop();
				skull.Killed += skull_Killed;
			}
		}

		private void skull_Killed(object sender, EventArgs e)
		{
			lastSmartSkullKillTime = Game.GameTime;
		}

		#endregion


		#region Food & Treat Spawning

		private readonly int totalNumFoodToSpawn;
		private readonly int maxNumSimultaneousFood;
		private double lastFastFoodKillTime;

		internal void Update_CheckFoodSpawns(double gameTime, double elapsedTime)
		{
			while (LevelSprites.OfType<PlainFood>().Count() < maxNumSimultaneousFood
			       && FoodSpawned < totalNumFoodToSpawn)
			{
				PlainFood food = new PlainFood(this, LevelNumber);
				food.SetLoc_OnRandomEdge();
			}

			if (LevelNumber >= 4
			    && LevelSprites.OfType<PlainFood>().Count() > 2
			    && !LevelSprites.OfType<FastFood>().Any()
			    && lastFastFoodKillTime + 5 < gameTime)
			{
				FastFood food = new FastFood(this, LevelNumber);
				food.SetLoc_OnRandomEdge();
				food.Killed += FastFood_Killed;
			}


			if (!LevelSprites.OfType<Food>().Any())
				ShowExitAndAllowEnding();
		}

		private void FastFood_Killed(object sender, EventArgs e)
		{
			lastFastFoodKillTime = Game.GameTime;
		}


		private const double SpawnsPerSecond = 0.15;

		internal void Update_TryToSpawnTreat(double gameTime, double elapsedTime)
		{
			if (IsFinished)
				return;

			double rnd = Random.Next((int)(100 * 10e+5)) / 10e+5;

			double ChanceOfSpawnNow = SpawnsPerSecond * 100 * elapsedTime * Game.ScaleFactor1DY;

			if (rnd < ChanceOfSpawnNow)
			{
                Treat spawn = (SpawnRandomizer.PickSpawn(new[] {
					// 0 Desert
					60,

					// 1 Peas (spawn chance increased below 10 peas)
					LevelSprites.OfType<Peas>().Any() ? 0 : 30 + Math.Max(0, 30 - (Game.Melvin.Peas * 3)),

					// 2 Butter
					Game.Melvin.ButterStage > 0 || LevelSprites.OfType<Butter>().Any() ? 0 : 15,

					// 3 Salt
					Game.Melvin.Salt || LevelSprites.OfType<Salt>().Any() ? 0 : 10,

					// 4 Pepper
					Game.Melvin.Pepper || LevelSprites.OfType<Pepper>().Any() ? 0 : 10,

					// 5 Coffee
					3
                })) switch
                {
                    0 => new Dessert(this),
                    1 => new Peas(this),
                    2 => new Butter(this),
                    3 => new Salt(this),
                    4 => new Pepper(this),
                    5 => new Coffee(this),
                    _ => throw new Exception("Unhandled switch case in determining spawn type"),
                };
                spawn.Velocity.Y = 0;
				spawn.Location.Y = Random.Next((int)(Game.Size.Height - spawn.Size.Height));

				if (Random.Next(2) == 0)
				{
					// Spawn on the left side of the screen and move to the right
					spawn.Velocity.X = Random.Next(100, (int)spawn.MaxVelocityX);
					spawn.Location.X = -spawn.Size.Width + 0.1f;
				}
				else
				{
					// Spawn on the right side of the screen and move to the left
					spawn.Velocity.X = -Random.Next(100, (int)spawn.MaxVelocityX);
					spawn.Location.X = Game.Size.Width - 0.1f;
				}

				AudioManager.GetSound("Munchies.Resources.Sounds.piu.ogg").Play();
			}
		}

		#endregion
	}
}
