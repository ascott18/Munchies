using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munchies
{
	internal class DessertLevelPlainSkull : PlainSkull
	{
		public DessertLevelPlainSkull(DessertLevel levelInstance)
			: base(levelInstance)
		{
		}

		public override void Update(double gameTime, double elapsedTime)
		{
			if (IsSpawning && !TestEdgeCollision().HasFlag(EdgeCollisionTypes.Top))
				IsSpawning = false;

			Update_MoveVelocity(elapsedTime);
		}
	}
}
