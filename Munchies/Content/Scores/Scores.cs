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

	public class ScoreManager
	{
		public Scores Scores;

		public readonly Game.GameDifficulty Difficulty;
		public readonly Size GameSize;

		public readonly string SettingIdentifier;


		#region Construction

		private static List<ScoreManager> AllManagers = new List<ScoreManager>();

		//internal static ScoreManager GetScoreManager(Game.GameDifficulty difficulty, Size gameSize)
		//{
		//    var matches = AllManagers
		//        .Where(sm => sm.Difficulty == difficulty && sm.GameSize == gameSize);

		//    if (matches.Count() == 0)
		//       return new ScoreManager(difficulty, gameSize);

		//    return matches.First();
		//}

		//private ScoreManager(Game.GameDifficulty difficulty, Size gameSize)
		//{
		//    Difficulty = difficulty;
		//    GameSize = gameSize;

		//    SettingIdentifier = string.Format("Scores.{0}.{1}", difficulty, gameSize);

		//    Program.Settings.DeclareDefault(SettingIdentifier, () => new Scores(difficulty, gameSize));

		//    Scores = (Scores)Program.Settings.GetSetting(SettingIdentifier);

		//  //  Program.Settings.SetSetting(SettingIdentifier, Scores);

		//    AllManagers.Add(this);
		//}

		#endregion
	}
}
