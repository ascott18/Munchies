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
    public partial class ScoreContainer : ContentContainer
    {
        private GameMode currentGameMode;
        internal GameMode CurrentGameMode
        {
            get { return currentGameMode; }
            set
            {
                currentGameMode = value;

                title.Text = string.Format("High Scores: {0} x {1} {2}",
                    CurrentGameMode.ContainerSize.Width, CurrentGameMode.ContainerSize.Height, CurrentGameMode.GameDifficulty);

                var OrderedScores = CurrentGameMode.Scores.OrderByDescending(s => s.Points);

                for (int i = 0; i < Scores.MaxNumScores; i++)
                {
                    Score score = CurrentGameMode.Scores[i];

                    ScoreEntry entry = Entries[i];

                    entry.scoreBindingSource.DataSource = score;

                    if (HighlightedScore == score)
                        entry.Highlighted = true;

                }
            }
        }

        private Score highlightedScore;

        public Score HighlightedScore
        {
            get { return highlightedScore; }
            set 
            {
                // If the new score is null, or the old one was not,
                // Clear the highlight of all entries (we are either
                // changing the highlight, or removing it)
                if (value == null || highlightedScore != null)
                    foreach (ScoreEntry entry in Entries)
                        entry.Highlighted = false;

                highlightedScore = value;

                if (highlightedScore != null)
                {
                    foreach (ScoreEntry entry in Entries)
                    {
                        if (entry.scoreBindingSource.DataSource == highlightedScore)
                            entry.Highlighted = true;
                    }
                }
            }
        }

        private ScoreEntry[] Entries;

        public ScoreContainer()
        {
            InitializeComponent();

            scoreEntryTitleRow.rank.Text = "";
            scoreEntryTitleRow.level.Text = "Level";
            scoreEntryTitleRow.points.Text = "Score";
            scoreEntryTitleRow.name.Text = "Name";

            for (int i = 0; i < Scores.MaxNumScores; i++)
            {
                ScoreEntry entry = new ScoreEntry();
                entry.Location = new System.Drawing.Point(23, 60 + (entry.Size.Height * i));
                entry.rank.Text = string.Format("{0}.", i + 1);
                
                scorePanel.Controls.Add(entry);
            }

            Entries = scorePanel.Controls.OfType<ScoreEntry>().Skip(1).ToArray();
        }
    }
}
