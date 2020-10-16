using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munchies
{
	internal abstract class PlainSkull : Skull
	{
		protected PlainSkull(Level levelInstance)
			: base(levelInstance)
		{
			SkullImageName = "Skull{0}";

			for (int i = 1; i <= 4; i++)
				PreloadImages(string.Format(SkullImageName, i));

			SetSizeToImage(string.Format(SkullImageName, 1));

			levelInstance.SkullsSpawned++;
		}
	}
}
