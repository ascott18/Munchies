using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munchies
{
	internal class Fork : Utensil
	{
		private const float Gravity = 550;
		private int PreviousHeight;

		public new static int SpawnFrequency = 10;

		public new static int MinimumLevel = 1;

		protected bool IsSpawning = true;

		public Fork(Level levelInstance)
			: base(levelInstance)
		{
			PreloadImages("Fork1");
			PreloadImages("Fork2");
			ImageName = "Fork1";
			SetSizeToImage(ImageName);

			Location.X = -Size.Width;

			PreviousHeight = Update_GetNewYMaxHeight(true);
			Location.Y = PreviousHeight;

			Velocity.X = 130;
			Velocity.Y = 0;
		}

		public override void Update(double gameTime, double elapsedTime)
		{
			// Wait half a second before we begin updating so that 
			// players have some warning from the spawn timer
			if (SpawnTime > gameTime - 0.5)
				return;

			// Check if the fork is completely within the client area.
			if (IsSpawning && TestEdgeCollision() == EdgeCollisionTypes.None)
				IsSpawning = false;

			Velocity.Y += (float)elapsedTime * Gravity;

			Update_MoveVelocity(elapsedTime);

			if (TestEdgeCollision().HasFlag(EdgeCollisionTypes.Bottom))
			{
				Update_PickNewYVelocity();

				// Play the boing sound
				if (!IsSpawning)
					AudioManager.GetSound("Munchies.Resources.Sounds.Boing.ogg").Play();
			}

			if (!IsSpawning)
				Update_TestEdgeCollisionAndBounce();

			ImageName = Velocity.X > 0 ? "Fork1" : "Fork2";
		}

		protected int Update_GetNewYMaxHeight(bool noLimit)
		{
			// Pick a random new height for the fork to bounce to
			int nextHeight = Random.Next(125, Math.Max(126, Game.Size.Height - 75));

			if (!noLimit)
				nextHeight = Limit(nextHeight, PreviousHeight - 50, PreviousHeight + 50);

			nextHeight = Limit(nextHeight, 0, Game.Size.Height);

			PreviousHeight = nextHeight;

			return nextHeight;
		}

		protected void Update_PickNewYVelocity()
		{
			int nextHeight = Update_GetNewYMaxHeight(false);

			// Determine the velocity needed to reach the given height.
			// This is a simplified version of the kinematic equation
			// V^2 = Vo^2 + 2a(D1 - D0)
			Velocity.Y = (float)-Math.Sqrt(2 * Gravity * nextHeight);

			// Bind above the bottom within the game so that
			// Update_TestEdgeCollisionAndBounce doesn't instantly bounce and counter our new velocity
			Location.Y = Math.Min(Location.Y, Game.Size.Height - (int)Size.Height);
		}
	}
}
