using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munchies
{
	internal class Dessert : Treat
	{
		public Dessert(Level levelInstance)
			: base(levelInstance)
		{
			ImageName = string.Format("Desert{0}", Random.Next(4) + 1);
			PreloadImages(ImageName);
			SetSizeToImage(ImageName);
		}
	}
}
