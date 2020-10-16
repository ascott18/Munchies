using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Munchies
{
	internal class Poison : Enemy
	{
		private const float Gravity = 550;

		public Poison(Level levelInstance, Spoon spoon)
			: base(levelInstance)
		{
			ImageName = "Poison";
			PreloadImages(ImageName);
			SetSizeToImage(ImageName);

			AudioManager.GetSound("Munchies.Resources.Sounds.drip.ogg").Play();

			Location.X = spoon.Location.X;
			Location.Y = spoon.Location.Y;


			Melvin melvin = levelInstance.Game.Melvin;

			Velocity.Y = Math.Min(-200, -200 - ((Location.Y - melvin.Location.Y) * 1.1f));
			Velocity.X = Limit(melvin.Location.X - Location.X, -250, 250);
		}


		public override void Update(double gameTime, double elapsedTime)
		{
			Velocity.Y += (float)elapsedTime * Gravity;

			Update_MoveVelocity(elapsedTime);

			Update_KillIfOffScreen();
		}
	}
}
