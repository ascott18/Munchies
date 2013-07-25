using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Munchies
{
    abstract class Skull : Enemy
    {
        public string SkullImageName = "";
        protected bool IsSpawning = true;

        public Skull(Level levelInstance)
            : base(levelInstance)
        {
        }

        internal void SetLoc_SomewhereAboveTop()
        {
            float max_x = Level.Game.Size.Width - Size.Width;

            Location.X = Random.Next((int)max_x);
            Location.Y = -Size.Height + 0.1f;
        }

        public override void Draw(Graphics graphics)
        {
            int State = AnimationState.GetState(Game.GameTime, 4, 50) + 1;

            graphics.DrawImage(Images[string.Format(SkullImageName, State)], 
                (int)Location.X, (int)Location.Y, Size.Width, Size.Height);
        }
    }
}
