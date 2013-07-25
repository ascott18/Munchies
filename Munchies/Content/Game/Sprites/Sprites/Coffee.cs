using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munchies
{
    class Coffee : Treat
    {
        const int NumCoffeeStates = 3;

        public Coffee(Level levelInstance)
            : base(levelInstance)
        {
            for (int i = 1; i <= NumCoffeeStates; i++)
                PreloadImages(string.Format("Coffee{0}", i));

            ImageName = "Coffee1";
            SetSizeToImage(ImageName);
        }

        public override void Update(double gameTime, double elapsedTime)
        {
            int state = AnimationState.GetState(gameTime, NumCoffeeStates, 20) + 1;

            ImageName = string.Format("Coffee{0}", state);
        }

    }
}
