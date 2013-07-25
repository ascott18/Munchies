using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munchies
{
    abstract class Edible : Sprite
    {
        public Edible(Level levelInstance)
            : base(levelInstance)
        {

        }
    }
}
