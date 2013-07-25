using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Munchies.HelpDialogs
{
    public partial class SpriteDescription : UserControl
    {

        private string text;

        [DisplayName("Sprite Description")]
        [Description("Description of the sprite")]
        public string Description
        {
            get { return text; }
            set 
            { 
                text = value;
                label1.Text = value;
            }
        }

        private Image image;

        [DisplayName("Sprite Image")]
        [Description("Image of the sprite")]
        public Image Image
        {
            get { return image; }
            set 
            { 
                image = value;
                pictureBox1.Image = value;
            }
        }

        public SpriteDescription()
        {
            InitializeComponent();
        }
    }
}
