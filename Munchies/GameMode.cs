using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using AndrewScott.SimpleCommandManager;

namespace Munchies
{
    [Serializable]
    public class GameMode
    {
        public static Game.GameDifficulty DifficultySetting
        {
            set
            {
                Program.Settings.SetSetting("GameDifficulty", value);
            }
            get
            {
                Program.Settings.DeclareDefault("GameDifficulty", Game.GameDifficulty.Beginner);

                return (Game.GameDifficulty)Program.Settings.GetSetting("GameDifficulty");
            }
        }

        public static GameMode GameModeFromSettings
        {
            get
            {
                return GetGameMode(DifficultySetting, Program.ContentSizeSetting);
            }
        }


        public Scores Scores = new Scores();

        public readonly Game.GameDifficulty GameDifficulty;
        public readonly Size ContainerSize;

        public readonly string SettingIdentifier;

        public int StartingLevel = 1;
        public int HighestLevelAttained = 1;

        private GameMode(Game.GameDifficulty gameDifficulty, Size containerSize)
        {
            GameDifficulty = gameDifficulty;
            ContainerSize = containerSize;

            SettingIdentifier = GetSettingIdentifier(gameDifficulty, containerSize);

            AllModes.Add(this);
        }


        private static List<GameMode> AllModes = new List<GameMode>();
        public static GameMode GetGameMode(Game.GameDifficulty gameDifficulty, Size containerSize)
        {
            var matches = AllModes
                   .Where(gm => gm.GameDifficulty == gameDifficulty && gm.ContainerSize == containerSize);

            if (matches.Count() == 0)
            {
                string SettingIdentifier = GetSettingIdentifier(gameDifficulty, containerSize);

                Program.Settings.DeclareDefault(SettingIdentifier, () => new GameMode(gameDifficulty, containerSize));

                return (GameMode)Program.Settings.GetSetting(SettingIdentifier);
            }

            return matches.First();
        }

        public static string GetSettingIdentifier(Game.GameDifficulty gameDifficulty, Size containerSize)
        {
            return string.Format("GameMode.{0}.{1}", gameDifficulty, containerSize);
        }
    }
}
