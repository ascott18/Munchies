using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Drawing;
using System.Windows.Forms;

namespace Munchies
{
    public abstract class Level : IDisposable
    {
        // Reference to the game that the level was created for
        public Game Game;

        public static Random Random = new Random();

        public List<Sprite> LevelSprites = new List<Sprite>();


        // The number of the level
        public int LevelNumber;
        public bool IsDesert { private set; get; }
        public double LevelStartTime;

        public int FoodSpawned = 0;
        public int SkullsSpawned = 0;
        public bool IsFinished;

        LevelExit LevelExit;
        public bool PeaIsActive;
        public Image BackgroundImage;

        public Level(Game game, int levelNumber, bool isDesert)
        {
            Game = game;
            LevelNumber = levelNumber;
            IsDesert = isDesert;

            LevelStartTime = game.GameTime;

            BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject(string.Format("BG{0}", Random.Next(16) + 1));
            
        }

        internal bool IsNextLevelDesert()
        {
            return (LevelNumber % 5 == 0 && !IsDesert);
        }

        internal virtual void Update(double gameTime, double elapsedTime)
        {
            if (IsFinished)
            {
                if (Game.Melvin.Location.X >= LevelExit.Location.X
                    && Game.Melvin.Location.Y >= LevelExit.Location.Y)
                {
                    TriggerNextLevel();
                }
            }
        }

        private void TriggerNextLevel()
        {
            Game.TriggerNextLevelTransition();
        }



        public void ShowExitAndAllowEnding()
        {
            if (!IsFinished)
            {
                IsFinished = true;
                LevelExit = new LevelExit(this);
            }
        }

        internal void Draw(Graphics graphics)
        {


            // Draw miscellaneous (includes the exit door and peas)
            foreach (Sprite sprite in LevelSprites.Where(sprite => !(sprite is Edible || sprite is Enemy)))
            {
                sprite.Draw(graphics);
            }

            // Draw edibles(they should be above miscellaneous)
            foreach (Sprite sprite in LevelSprites.Where(sprite => sprite is Edible))
            {
                sprite.Draw(graphics);
            }

            // Draw enemies (they should be above everything else)
            foreach (Sprite sprite in LevelSprites.Where(sprite => sprite is Enemy))
            {
                sprite.Draw(graphics);
            }
        }




        private bool disposed;
        public bool IsDisposed { get { return disposed; } }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    LevelExit = null;
                    Game = null;

                    BackgroundImage = null;

                    // Iteration done in reverse because sprites are removed from the
                    // list as they are disposed which causes the list to shift.
                    foreach (Sprite sprite in LevelSprites.Reverse<Sprite>())
                        sprite.Dispose();

                }

                // There are no unmanaged resources to release, but
                // if we add them, they need to be released here.
            }
            disposed = true;
        }
    }
}
