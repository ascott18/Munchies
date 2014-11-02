using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munchies
{
	internal class Butter : Treat
	{
		public Butter(Level levelInstance)
			: base(levelInstance)
		{
			ImageName = "Butter";
			PreloadImages(ImageName);
			SetSizeToImage(ImageName);
		}
	}
}
