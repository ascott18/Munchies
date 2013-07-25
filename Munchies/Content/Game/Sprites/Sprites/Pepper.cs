using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munchies
{
    class Pepper : Treat
    {
        public Pepper(Level levelInstance)
            : base(levelInstance)
        {
            ImageName = "Pepper";
            PreloadImages(ImageName);
            SetSizeToImage(ImageName);
        }
    }
}
