using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Munchies
{
	public partial class HighScoreDialog : DialogBase
	{
		private readonly Score score;
		private readonly GameMode gameMode;

		public HighScoreDialog(GameMode gameMode, Score score)
		{
			InitializeComponent();

			Program.Settings.DeclareDefault("LastScoreName", "Melvin");

			this.score = score;
			this.gameMode = gameMode;

			textBox1.Text = (string)Program.Settings.GetSetting("LastScoreName");
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Program.Settings.SetSetting("LastScoreName", textBox1.Text);

			score.Name = textBox1.Text;

			gameMode.Scores.AddScore(score);

			Close();
		}
	}
}
