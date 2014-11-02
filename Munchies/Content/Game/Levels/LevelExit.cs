using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Munchies
{
	internal class LevelExit : Sprite
	{
		private int State;

		private static readonly string[] exitImageNames =
		{
			"Exit1", "Exit2", "Exit3", "Exit4", "Exit5"
		};

		public LevelExit(Level levelInstance)
			: base(levelInstance)
		{
			Game gameInstance = levelInstance.Game;

			PreloadImages(exitImageNames);
			SetSizeToImage("Exit1");

			AudioManager.GetSound("Munchies.Resources.Sounds.exitSound.ogg").Play();

			Location.X = gameInstance.Size.Width - Size.Width;
			Location.Y = gameInstance.Size.Height - Size.Height;
		}

		public override void Update(double gameTime, double elapsedTime)
		{
			if (State != 5)
			{
				State = (int)(((gameTime - SpawnTime) * 15) % 5) + 1;
			}
		}

		public override void Draw(Graphics graphics)
		{
			if (State > 0)
			{
				graphics.DrawImage(Images[string.Format("Exit{0}", State)],
				                   (int)Location.X, (int)Location.Y, Size.Width, Size.Height);
			}
		}
	}
}
