using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munchies
{
	internal class Spoon : Utensil
	{
		private Poison poison;

		public new static int SpawnFrequency = 25;

		public new static int MinimumLevel = 11;

		private static readonly string[] spoonImageNames =
		{
			"Spoon1"
			//, "Spoon2", "Spoon3", "Spoon4", "Spoon5" 
		};

		private double LastPoisonKilledTime;

		public Spoon(Level levelInstance)
			: base(levelInstance)
		{
			PreloadImages(spoonImageNames);
			ImageName = "Spoon1";
			SetSizeToImage(ImageName);

			Location.X = Game.Size.Width - 0.1f;
			Location.Y = Random.Next(125, Game.Size.Height - 75);

			Velocity.X = -150;
			Velocity.Y = 0;

			LastPoisonKilledTime = Game.GameTime;
		}

		public override void Update(double gameTime, double elapsedTime)
		{
			if (LastPoisonKilledTime < gameTime - 0.2 &&
			    (poison == null || poison.IsDead))
			{
				SpawnPoison();
			}

			Update_MoveVelocity(elapsedTime);

			Update_KillIfOffScreen();
		}

		private void poison_Killed(object sender, EventArgs e)
		{
			LastPoisonKilledTime = Game.GameTime;

			((Poison)sender).Killed -= poison_Killed;
		}

		protected void SpawnPoison()
		{
			poison = new Poison(Level, this);
			poison.Killed += poison_Killed;
		}
	}
}
