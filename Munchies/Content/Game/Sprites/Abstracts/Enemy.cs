using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munchies
{
	internal abstract class Enemy : Sprite
	{
		protected Enemy(Level levelInstance)
			: base(levelInstance)
		{
		}
	}
}
