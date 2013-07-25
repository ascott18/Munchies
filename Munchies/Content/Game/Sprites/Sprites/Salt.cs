using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munchies
{
    class Salt : Treat
    {
        public Salt(Level levelInstance)
            : base(levelInstance)
        {
            ImageName = "Salt";
            PreloadImages(ImageName);
            SetSizeToImage(ImageName);
        }
    }
}
