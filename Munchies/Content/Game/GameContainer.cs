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
	public partial class GameContainer : ContentContainer
	{
		public GameContainer()
		{
			InitializeComponent();
			gameOverPicture.AutoCenterInParent();
		}

        private Game game;

		public Game Game
		{
			get { return game; }
			set
			{
				if (game != null)
				{
					game.Visible = false;
					gameOverPicture.Visible = false;

					Controls.Remove(game);
				}

				game = value;

				if (game != null)
				{
					game.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);

					game.Size = new Size(Size.Width, Size.Height - statusStrip1.Size.Height);
					game.Location = new Point(0, 0);

					game.Ended += game_Ended;

					game.Visible = true;

					Controls.Add(game);

					toolStripStatus_TopScore.Text = game.GameMode.Scores[0].Points.ToString();
					toolStripStatus_TopMunchie.Text = game.GameMode.Scores[0].Name;
				}
			}
		}

		private void game_Ended(object sender, EventArgs e)
		{
			gameOverPicture.Visible = true;
		}

		internal static Image GetStatusBarImage(int Width, int Height)
		{
			// Create the status that we are going to be drawing
			const int SideWidth = 7;
			Image result = new Bitmap(Width, Height);

			// Compose the image that we will drawn
			using Graphics g = Graphics.FromImage(result);
            using Bitmap sourceImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("Status");
            using Bitmap source = new Bitmap(sourceImage);

			// Draw the background to the image:
			// Determine and create the images that make up the background
			Image leftImage = source.Clone(
                new Rectangle(0, 0, SideWidth, Height), source.PixelFormat);

            Image rightImage = source.Clone(
                new Rectangle(sourceImage.Size.Width - SideWidth, 0, SideWidth, Height),
                source.PixelFormat);

            Image centerStrip = source.Clone(
                new Rectangle(SideWidth + 1, 0, 1, Height), source.PixelFormat);

            // Draw the left and right sides
            g.DrawImage(leftImage, 0, 0);
            g.DrawImage(rightImage, result.Size.Width - SideWidth, 0);

            // Draw the middle
            for (int i = SideWidth; i < result.Size.Width - SideWidth; i++)
                g.DrawImage(centerStrip, i, 0);

			return result;
		}

		private void GameContainer_Layout(object sender, LayoutEventArgs e)
		{
			statusStrip1.Size = new Size(Size.Width, 24);
			statusStrip1.Location = new Point(0, Size.Height - statusStrip1.Size.Height);
			statusStrip1.BackgroundImage = GetStatusBarImage(statusStrip1.Size.Width, statusStrip1.Height);
		}
	}
}
