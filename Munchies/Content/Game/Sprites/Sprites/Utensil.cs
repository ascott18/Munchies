﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munchies
{
	internal abstract class Utensil : Enemy
	{
		public static int SpawnFrequency = 10;

		public static int MinimumLevel = 1;

		protected Utensil(Level levelInstance)
			: base(levelInstance)
		{
			AudioManager.GetSound("Munchies.Resources.Sounds.RedAlert.ogg").Play();
		}
	}
}
