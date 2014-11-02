using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munchies
{
	internal abstract class Treat : Edible
	{
		public Treat(Level levelInstance)
			: base(levelInstance)
		{
			MaxVelocityX = 175;
			MaxVelocityY = 175;
		}

		public override void Update(double gameTime, double elapsedTime)
		{
			Update_MoveVelocity(elapsedTime);

			Update_KillIfOffScreen();
		}
	}
}
