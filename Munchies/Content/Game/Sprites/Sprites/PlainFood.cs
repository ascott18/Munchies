using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munchies
{
	internal class PlainFood : Food
	{
		public PlainFood(Level levelInstance, int foodID)
			: base(levelInstance, foodID)
		{
			FoodSpeed Speed = (FoodSpeed)Program.Settings.GetSetting("FoodSpeed");

			if (Speed == FoodSpeed.Slow)
			{
				MaxVelocityX = 75;
				MaxVelocityY = 75;
			}
			else if (Speed == FoodSpeed.Fast)
			{
				MaxVelocityX = 150;
				MaxVelocityY = 150;
			}

			Velocity.X = Random.Next((int)-MaxVelocityX, (int)MaxVelocityX);
			Velocity.Y = Random.Next((int)-MaxVelocityY, (int)MaxVelocityY);

			levelInstance.FoodSpawned++;
		}

		public override void Update(double gameTime, double elapsedTime)
		{
			Update_MoveVelocity(elapsedTime);

			Update_TestEdgeCollisionAndBounce();
		}
	}
}
