using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munchies
{
    class SmartSkull : Skull
    {
        float MaxVelocity;

        public SmartSkull(Level levelInstance)
            : base(levelInstance)
        {
            SkullImageName = "SmartSkull{0}";

            for (int i = 1; i <= 4; i++)
                PreloadImages(string.Format(SkullImageName, i));

            SetSizeToImage(string.Format(SkullImageName, 1));


            MaxVelocity = 50 + levelInstance.LevelNumber;
        }


        public override void Update(double gameTime, double elapsedTime)
        {
            Update_VelocityTowardsMelvin(MaxVelocity, 10000);

            Update_MoveVelocity(elapsedTime);
        }
    }
}
