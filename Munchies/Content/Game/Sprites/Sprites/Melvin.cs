using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Munchies
{
	public class Melvin : Sprite
	{
		// Peas
		private int peas;

		public int Peas
		{
			set
			{
				peas = value;
				UpdatePeasDisplayText();
			}
			get { return peas; }
		}

		// Salt
		private bool salt;

		public bool Salt
		{
			set
			{
				salt = value;
				UpdatePeasDisplayText();
			}
			get { return salt; }
		}

		// Pepper
		private bool pepper;

		public bool Pepper
		{
			set
			{
				pepper = value;
				UpdatePeasDisplayText();
			}
			get { return pepper; }
		}

		private void UpdatePeasDisplayText()
		{
			string text = Peas.ToString();

			if (Salt || Pepper)
			{
				text += " ";
				text += Salt ? "S" : "";
				text += Pepper ? "P" : "";
			}

			Game.GameContainer.toolStripStatus_Peas.Text = text;
		}

		private static readonly string[] butterImageNames =
		{
			"Shield1", "Shield2", "Shield3", "Shield4"
		};

		private static readonly string[] melvinImageNames =
		{
			"MelvinS",
			"MelvinL1", "MelvinL2", "MelvinL3", "MelvinL4",
			"MelvinR1", "MelvinR2", "MelvinR3", "MelvinR4",
			"MelvinD1", "MelvinD2", "MelvinD3", "MelvinD4",
			"MelvinD5", "MelvinD6", "MelvinD7"
		};

		public Melvin(Game gameInstance) : base(gameInstance)
		{
			MaxVelocityX = 300;
			MaxVelocityY = 275;

			PreloadImages(melvinImageNames);
			PreloadImages(butterImageNames);

			SetSizeToImage("MelvinS");

			SetLoc_AtBottomOfGame();
			ResetCursorPosToCenter();
			SetButter(1);

			UpdatePeasDisplayText();

			Collide += Melvin_Collide;

			gameInstance.OnPlay += gameInstance_OnPlay;
			gameInstance.OnPause += gameInstance_OnPause;
		}


		private void gameInstance_OnPause(object sender, EventArgs e)
		{
			Cursor.Position = Game.GameContainer.PointToScreen(
				new Point((int)Location.X, (int)Location.Y));
		}

		private void gameInstance_OnPlay(object sender, EventArgs e)
		{
			ResetCursorPosToCenter();
		}


		private void Melvin_Collide(Sprite sprite2)
		{
			if (sprite2 is Edible)
			{
				if (sprite2 is PlainFood)
					Game.ScorePoints += 10;
				else if (sprite2 is FastFood)
					Game.ScorePoints += 100;
				else if (sprite2 is Dessert)
					Game.ScorePoints += 50;

				else if (sprite2 is Pepper)
					Pepper = true;
				else if (sprite2 is Salt)
					Salt = true;
				else if (sprite2 is Peas)
					Peas += 5;
				else if (sprite2 is Coffee)
					Game.Lives++;
				else if (sprite2 is Butter)
					SetButter(2);

				AudioManager.GetSound("Munchies.Resources.Sounds.Dunk.ogg").Play();
				sprite2.Kill();
			}

			if (sprite2 is Enemy && ButterStage == 0)
			{
				Die();
				sprite2.Kill();
			}
		}

		internal void ShootPea()
		{
			Level level = Game.CurrentLevel;

			if (!IsDying
			    && Peas > 0
			    && !level.PeaIsActive
			    && FacingDirection != "S")
			{
				Peas--;

                Pea pea = new Pea(level)
                {
                    Pepper = Pepper,
                    Salt = Salt
                };

                pea.Update_SetYToMelvinY(this);

				if (FacingDirection == "L")
				{
					pea.Velocity.X = -pea.VelocityMagnitude;
					pea.Location.X = Location.X + (pea.Size.Width / 2);
				}
				else if (FacingDirection == "R")
				{
					pea.Velocity.X = pea.VelocityMagnitude;
					pea.Location.X = Location.X + Size.Width - (pea.Size.Width / 2);
				}
			}
		}

		private double DeathStartTime;
		private bool IsDying;

		internal void Die()
		{
			if (!IsDying)
			{
				AudioManager.GetSound("Munchies.Resources.Sounds.OhNo.ogg").Play();

				DeathStartTime = Game.GameTime;
				IsDying = true;

				Collide -= Melvin_Collide;
			}
		}

		public int ButterStage { get; private set; }
		private double TimeAtlastButterStageChange;

		private void SetButter(int stage)
		{
			TimeAtlastButterStageChange = Game.GameTime;
			ButterStage = stage;
		}

		private void ResetCursorPosToCenter()
		{
			int w_mid = Game.GameContainer.Width / 2;
			int h_mid = Game.GameContainer.Height / 2;

			Cursor.Position = Game.GameContainer.PointToScreen(
				new Point(w_mid, h_mid));
		}

		private string MelvinImageName = "MelvinS";
		private string FacingDirection;

		public override void Update(double gameTime, double elapsedTime)
		{
			if (IsDying)
			{
				int state = AnimationState.GetState(DeathStartTime, gameTime, 7 + 1, 70) + 1;


                if (state <= 7)
                {
                    MelvinImageName = $"MelvinD{state}";
                }
                else
                {
                    if (Game.CurrentLevel is DessertLevel)
                        Game.TriggerNextLevelTransition();
                    Kill();
                }
            }
			else
			{
				// Adjust Melvin's velocity based on cursor movement
				float MiddleOfScreen_X = Game.GameContainer.Width / 2;
				float MiddleOfScreen_Y = Game.GameContainer.Height / 2;

				float DeltaX = Game.GameContainer.PointToClient(Cursor.Position).X - MiddleOfScreen_X;
				float DeltaY = Game.GameContainer.PointToClient(Cursor.Position).Y - MiddleOfScreen_Y;

				Velocity.X = Limit(Velocity.X + DeltaX, -MaxVelocityX, MaxVelocityX);
				Velocity.Y = Limit(Velocity.Y + DeltaY, -MaxVelocityY, MaxVelocityY);

				if (CanMove())
				{
					Update_MoveVelocity(elapsedTime);

					// Check if Melvin has hit an edge, and zero his velocity if he has
					// Update_ZeroVelocityAtEdges();
				}

				Update_DetermineFacingDirection();

				// Determine Melvin image to be used
				if (FacingDirection == "S")
				{
					MelvinImageName = "MelvinS";
				}
				else
				{
					int MelvinState = AnimationState.GetState(gameTime, 4, 40) + 1;

					MelvinImageName = $"Melvin{FacingDirection}{MelvinState}";
				}
			}


			Update_BindWithinGame();

			ResetCursorPosToCenter();


			// Butter updating
			if (ButterStage == 2 && TimeAtlastButterStageChange < gameTime - 13)
				SetButter(1);
			else if (ButterStage == 1 && TimeAtlastButterStageChange < gameTime - 3)
				SetButter(0);
		}

		protected void Update_ZeroVelocityAtEdges()
		{
			EdgeCollisionTypes collisions = TestEdgeCollision();

			if (collisions.HasFlag(EdgeCollisionTypes.Left)
			    || collisions.HasFlag(EdgeCollisionTypes.Right))
			{
				Velocity.X = 0;
			}


			if (collisions.HasFlag(EdgeCollisionTypes.Top)
			    || collisions.HasFlag(EdgeCollisionTypes.Bottom))
			{
				Velocity.Y = 0;
			}
		}

		protected bool CanMove()
		{
			const float MinVelocity = 14;

			return Math.Abs(Velocity.X) > MinVelocity || Math.Abs(Velocity.Y) > MinVelocity;
		}

		protected void Update_DetermineFacingDirection()
		{
			FacingDirection = "S";

			if (CanMove())
				FacingDirection = Velocity.X < 0 ? "L" : "R";
		}

		public override void Draw(Graphics graphics)
		{
			// Melvin
			graphics.DrawImage(Images[MelvinImageName], (int)Location.X, (int)Location.Y, Size.Width, Size.Height);

			if (ButterStage > 0)
			{
				// Butter Shield
				int shieldFrame = AnimationState.GetState(Game.GameTime, 2, 70) + 1;
				string shieldImage = string.Format("Shield{0}", shieldFrame + ((ButterStage - 1) * 2));

				Image image = Images[shieldImage];

				if (shieldFrame == 1)
					image.RotateFlip(RotateFlipType.Rotate90FlipNone);
				else
					image.RotateFlip(RotateFlipType.RotateNoneFlipNone);

#if SCALEWITHSIZE
                graphics.DrawImage(Images[shieldImage], (int)Location.X - 4, (int)Location.Y - 4, 
                    40 * game.ScaleFactor1DX, 40 * game.ScaleFactor1DY);
#else
				graphics.DrawImage(Images[shieldImage], (int)Location.X - 4, (int)Location.Y - 4, 40, 40);
#endif
			}
		}
	}
}
