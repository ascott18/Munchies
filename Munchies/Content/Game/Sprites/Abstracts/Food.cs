using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Munchies
{
	internal abstract class Food : Edible
	{
		private const int MAX_FOOD_ID = 45;

		public enum FoodSpeed
		{
			Slow,
			Fast,
		}

		protected Food(Level levelInstance, int foodID)
			: base(levelInstance)
		{
			ImageName = string.Format("Food{0}", ((foodID - 1) % MAX_FOOD_ID) + 1);

			PreloadImages(ImageName);
			SetSizeToImage(ImageName);
		}
	}
}
