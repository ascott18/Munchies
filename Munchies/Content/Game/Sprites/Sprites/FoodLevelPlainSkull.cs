using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munchies
{
	internal class FoodLevelPlainSkull : PlainSkull
	{
		private double ElapsedSinceLastVelocityRandomization;

		public FoodLevelPlainSkull(FoodLevel levelInstance)
			: base(levelInstance)
		{
			MaxVelocityX = 100;
			MaxVelocityY = 100;

			Velocity.X = Random.Next(-(int)MaxVelocityX, (int)MaxVelocityX);
			Velocity.Y = Random.Next(30, (int)MaxVelocityY);
		}

		public const double ChanceToChaseMelvinPerSecond = 1;

		public override void Update(double gameTime, double elapsedTime)
		{
			// Check if the skull has dropped completely below the top of the screen.
			if (IsSpawning && !TestEdgeCollision().HasFlag(EdgeCollisionTypes.Top))
				IsSpawning = false;


			// Velocity randomization
			ElapsedSinceLastVelocityRandomization += elapsedTime;

			if (ElapsedSinceLastVelocityRandomization > 0.1)
			{
				ElapsedSinceLastVelocityRandomization -= 0.1;


				// Don't randomized the velocity unless its below the top of the screen.
				// Skulls are spawned with a downward velocity so that they don't end up
				// endlessly wandering around above the top edge of the screen.
				if (!IsSpawning)
				{
					Update_RandomizeVelocity(gameTime, elapsedTime);
				}
			}

			if (!IsSpawning && Level.Game.GameMode.GameDifficulty == Game.GameDifficulty.Expert)
			{
				double rnd = Random.Next((int)(100 * 10e+5)) / 10e+5;

				double ChanceOfChaseNow = ChanceToChaseMelvinPerSecond * 100 * elapsedTime;

				if (rnd < ChanceOfChaseNow)
				{
					Update_VelocityTowardsMelvin(MaxVelocityX, MaxVelocityX);
				}
			}


			Update_MoveVelocity(elapsedTime);

			// Don't bind skulls within the game or bounce them if they aren't done spawning
			// because otherwise they will jump straight into the game.
			if (!IsSpawning)
				Update_TestEdgeCollisionAndBounce();
		}

		public const int VelocityRandAmount = 15;

		private void Update_RandomizeVelocity(double gameTime, double elapsedTime)
		{
			Velocity.X = Limit(Velocity.X + Random.Next(-VelocityRandAmount, VelocityRandAmount), -MaxVelocityX, MaxVelocityX);
			Velocity.Y = Limit(Velocity.Y + Random.Next(-VelocityRandAmount, VelocityRandAmount), -MaxVelocityY, MaxVelocityY);
		}
	}
}
