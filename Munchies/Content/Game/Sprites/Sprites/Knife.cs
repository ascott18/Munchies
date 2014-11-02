using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munchies
{
	internal class Knife : Utensil
	{
		public int Direction;

		private int NumShots;
		private int MaxNumShots = 5;

		public new static int SpawnFrequency = 20;

		public new static int MinimumLevel = 6;

		public Knife(Level levelInstance)
			: base(levelInstance)
		{
			// Pick a random direction
			Direction = Random.Next(2) == 0 ? -1 : 1;

			Velocity.X = 175 * Direction;
			Velocity.Y = 175;

			ImageName = string.Format("Knife{0}", Direction == -1 ? "_1" : "1");
			PreloadImages(ImageName);
			SetSizeToImage(ImageName);
		}

		protected void Shoot()
		{
			Location.Y = -Size.Height;

			if (Direction == 1)
			{
				Location.X = Game.Melvin.Location.X - Game.Melvin.Location.Y - Size.Width;

				while (Location.X < 0)
					Location.X += Game.Size.Width;
			}
			else if (Direction == -1)
			{
				Location.X = Game.Melvin.Location.X + Game.Melvin.Location.Y;

				while (Location.X > Game.Size.Width)
					Location.X -= Game.Size.Width;
			}

			AudioManager.GetSound("Munchies.Resources.Sounds.slice.ogg").Play();
			NumShots++;
		}

		public override void Update(double gameTime, double elapsedTime)
		{
			// Calculate a new trajectory for the knife if it has any shots left
			// and if it is sufficiently far below the screen
			// (we let it drop pretty low so we don't have an endless stream of knives -
			// its an easy way to throttle it);

			if (Location.Y > Game.Size.Height + 50)
			{
				if (NumShots < MaxNumShots)
					Shoot();
				else
					Kill();
			}

			Update_WrapAround_LeftRight();

			Update_MoveVelocity(elapsedTime);
		}
	}
}
