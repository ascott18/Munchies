using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Munchies
{
    public partial class ContentContainer : UserControl
    {
        public ContentContainer()
        {
            InitializeComponent();

            SetStyle(ControlStyles.Selectable, false);
        }
    }
}
