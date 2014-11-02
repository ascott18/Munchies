using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munchies
{
	internal class FastFood : Food
	{
		public FastFood(Level levelInstance, int foodID)
			: base(levelInstance, foodID)
		{
			MaxVelocityX = 200;
			MaxVelocityY = 200;

			Velocity.X = Random.Next((int)-MaxVelocityX, (int)MaxVelocityX);
			Velocity.Y = Random.Next((int)-MaxVelocityY, (int)MaxVelocityY);

			Killed += FastFood_Killed;
		}

		private void FastFood_Killed(object sender, EventArgs e)
		{
			AudioManager.GetSound("Munchies.Resources.Sounds.smartFood.ogg").Play();
		}

		public override void Update(double gameTime, double elapsedTime)
		{
			Update_MoveVelocity(elapsedTime);

			Update_WrapAround();

			Update_AvoidMelvin(elapsedTime, 85, 8500);
		}
	}
}
