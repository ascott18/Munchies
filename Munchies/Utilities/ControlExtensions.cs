using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Munchies
{
    public static class ControlExtensions
    {
        public static void AutoCenterInParent(this Control control)
        {
            void Reposition()
            {
                var parent = control.Parent;
                control.Location = new Point((parent.Size.Width - control.Size.Width) / 2, (parent.Size.Height - control.Size.Height) / 2);
            }

            control.Parent.Resize += (s,e) => Reposition();
            Reposition();
        }
    }
}
