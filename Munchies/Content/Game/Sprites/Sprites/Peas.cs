using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munchies
{
	internal class Peas : Treat
	{
		public Peas(Level levelInstance)
			: base(levelInstance)
		{
			ImageName = "Peas";
			PreloadImages(ImageName);
			SetSizeToImage(ImageName);
		}
	}
}
