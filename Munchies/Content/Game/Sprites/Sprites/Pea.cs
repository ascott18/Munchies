using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munchies
{
    class Pea : Sprite
    {
        public float VelocityMagnitude = 380;

        public bool Salt;
        public bool Pepper;

        public Pea(Level levelInstance)
            : base(levelInstance)
        {
            Level.PeaIsActive = true;

            ImageName = "Pea";
            PreloadImages("Pea");
            SetSizeToImage("Pea");

            AudioManager.GetSound("Munchies.Resources.Sounds.beep.ogg").Play();

            Killed += Pea_Killed;
            Collide += Pea_Collide;
        }

        void Pea_Collide(Sprite sprite2)
        {
            if (sprite2 is Enemy)
                Game.ScorePoints += 50;

            if (sprite2 is Enemy || sprite2 is Edible)
            {
                Explosion explosion = new Explosion(Level);

                explosion.Location.X = sprite2.Location.X;
                explosion.Location.Y = sprite2.Location.Y;

                sprite2.Kill();

                if (!Pepper)
                    Kill();

            }
        }

        void Pea_Killed(object sender, EventArgs e)
        {
            Level.PeaIsActive = false;
        }

        public override void Update(double gameTime, double elapsedTime)
        {
            if (Salt)
                Update_SetYToMelvinY(Game.Melvin);

            Update_MoveVelocity(elapsedTime);

            Update_KillIfOffScreen();
        }

        public void Update_SetYToMelvinY(Melvin melvin)
        {
            Location.Y = melvin.Location.Y + (melvin.Size.Height / 2) - (Size.Height / 2);
        }
    }
}
