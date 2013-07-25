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
    public partial class ScoreEntry : UserControl
    {
        Font NormalFont = new Font("Microsoft Sans Serif", 9F);
        Font HighlightFont = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);

        private bool highlighted;
        public bool Highlighted
        {
            get { return highlighted; }
            set 
            { 
                highlighted = value;

                if (highlighted)
                {
                    name.Font = HighlightFont;
                    name.ForeColor = Color.Red;
                }
                else
                {
                    name.Font = NormalFont;
                    name.ForeColor = SystemColors.ControlText;
                }
            }
        }


        public ScoreEntry()
        {
            InitializeComponent();
        }
    }
}
