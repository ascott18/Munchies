using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Drawing;

namespace Munchies
{
	[Serializable]
	public class Scores : List<Score>
	{
		[NonSerialized] public const int MaxNumScores = 10;

		public Scores()
			: base(MaxNumScores)
		{
			for (int i = 0; i < MaxNumScores; i++)
				Add(new Score());
		}

		public bool IsScoreRankworthy(int points)
		{
			return points > this.Last().Points;
		}

		public void AddScore(Score score)
		{
			if (!IsScoreRankworthy(score.Points))
				return;

			Remove(this.Last());

			Add(score);

			var Ordered = this.OrderByDescending(s => s.Points).ToList();

			Clear();

			AddRange(Ordered);

			// Save();
		}
	}

	[Serializable]
	public class Score
	{
		public int Level { get; set; }
		public int Points { get; set; }
		public string Name { get; set; }

		public Score()
		{
			Level = 1;
			Points = 100;
			Name = "Melvin";
		}
	}
}
