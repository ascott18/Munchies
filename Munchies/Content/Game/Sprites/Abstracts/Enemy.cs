using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munchies
{
    abstract class Enemy : Sprite
    {
        public Enemy(Level levelInstance)
            : base(levelInstance)
        {

        }
    }
}
