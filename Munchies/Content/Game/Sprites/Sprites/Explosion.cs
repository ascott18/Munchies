using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munchies
{
	internal class Explosion : Sprite
	{
		private const int NumExplosionStates = 6;

		public Explosion(Level levelInstance)
			: base(levelInstance)
		{
			for (int i = 1; i <= NumExplosionStates; i++)
				PreloadImages(string.Format("Explosion{0}", i));

			ImageName = "Explosion1";
			SetSizeToImage(ImageName);

			AudioManager.GetSound("Munchies.Resources.Sounds.boom.ogg").Play();
		}

		private double Elapsed;

		public override void Update(double gameTime, double elapsedTime)
		{
			Elapsed += elapsedTime;

			int state = AnimationState.GetState(Elapsed, NumExplosionStates + 1, 50) + 1;

			if (state > NumExplosionStates)
				Kill();
			else
				ImageName = string.Format("Explosion{0}", state);
		}
	}
}
