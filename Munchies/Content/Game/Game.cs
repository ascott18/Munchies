using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Media;

namespace Munchies
{
    public class Game : Panel, IDisposable
    {
        public Melvin Melvin;
        public GameContainer GameContainer;

        public GameMode GameMode;

        private Level currentLevel;
        public Level CurrentLevel {
            get 
            {
                return currentLevel;
            }
            set 
            {
                if (currentLevel != null)
                {
                    currentLevel.Dispose();
                }

                currentLevel = value;

                if (currentLevel != null)
                {
                    BackgroundImage = currentLevel.BackgroundImage;
                    GameContainer.toolStripStatus_Level.Text = currentLevel.LevelNumber.ToString();
                }
            }
        }

        public List<Sprite> AllSprites = new List<Sprite>();
        public List<Sprite> GameSprites = new List<Sprite>();

        public float ScaleFactor1DX = 1;
        public float ScaleFactor1DY = 1;
        public float ScaleFactor2D = 1;

        // ScorePoints
        private int scorePoints;
        public int ScorePoints
        {
            set
            {
                if (ScorePoints / 2500 < value / 2500)
                    Lives++;

                scorePoints = value;

                GameContainer.toolStripStatus_Points.Text = (scorePoints).ToString();
            }
            get
            {
                return scorePoints;
            }
        }

        // Lives
        private int lives;
        public int Lives
        {
            set
            {
                // Only play the life gained sound if we got exactly one life.
                // We get six lives when the level starts, but the sound shouldn't be played then.
                if (lives + 1 == value)
                    AudioManager.GetSound("Munchies.Resources.Sounds.extraLife.ogg").Play();

                lives = value;
                GameContainer.toolStripStatus_Lives.Text = Math.Max(0, value).ToString();
            }
            get
            {
                return lives;
            }
        }

        // Timing and frame control
        private Stopwatch timer = new Stopwatch();
        private double lastTime;
        public double GameTime;

        public enum GameDifficulty
        {
            Beginner,
            Expert,
        }

        public Game(GameContainer container, GameMode gameMode)
        {
            GameMode = gameMode;

            GameContainer = container;

            // Control setup and styling
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            
            // The game update engine
            Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);

            Click += Game_Click;

            // 6 lives to start out with because one is consumed instantly.
            Lives = 6;

            ScorePoints = 0; 
        }

        public static readonly Size BaseScaleSize = new Size(640, 456);

        public bool HasStarted;
        public event EventHandler Started;
        public void Start()
        {
            ScaleFactor1DX = (float)Size.Width / BaseScaleSize.Width;
            ScaleFactor1DY = (float)Size.Height / BaseScaleSize.Height;
            ScaleFactor2D = ScaleFactor1DX * ScaleFactor1DY;

            HasStarted = true;

            SpawnMelvin();

            // Create the first level
            CurrentLevel = new FoodLevel(this, GameMode.StartingLevel);

            Play();

            if (Started != null) Started(this, new EventArgs());
        }

        public bool HasEnded;
        public event EventHandler Ended;
        public void End()
        {
            HasEnded = true;

            Pause(false);

            GameMode.HighestLevelAttained = Math.Max(GameMode.HighestLevelAttained, CurrentLevel.LevelNumber);

            if (Ended != null) Ended(this, new EventArgs());
        }

        private void SpawnMelvin()
        {
            Lives--;

            if (Lives > 0)
                Melvin = new Melvin(this);
            else
                End();
        }

        void Game_Click(object sender, EventArgs e)
        {
            if (Playing)
                Melvin.ShootPea();
        }






        void DrawStatusDisplay(Graphics graphics, string Text)
        {
            
            // Set up font. 
            Font stringFont = new Font("Arial", 18);

            // Measure the string.
            SizeF stringSize = graphics.MeasureString(Text, stringFont);


            // Create the status that we are going to be drawing
            int Padding = 30;
            int Height = 42;
            int SideWidth = 18;
            Image result = new Bitmap((int)stringSize.Width + Padding, Height);

            // Compose the image that we will drawn
            using (Graphics g = Graphics.FromImage(result))
            {
                // Draw the background to the image
                using (Bitmap sourceImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("LevelTransition"))
                using (Bitmap source = new Bitmap(sourceImage))
                {

                    // Determine and create the images that make up the background
                    Image leftImage = (Image)source.Clone(
                        new Rectangle(0, 0, SideWidth, Height), source.PixelFormat);

                    Image rightImage = (Image)source.Clone(
                        new Rectangle(sourceImage.Size.Width - SideWidth, 0, SideWidth, Height), source.PixelFormat);

                    Image centerStrip = (Image)source.Clone(
                        new Rectangle(SideWidth + 1, 0, 1, Height), source.PixelFormat);

                    // Draw the left and right sides
                    g.DrawImage(leftImage, 0, 0);
                    g.DrawImage(rightImage, result.Size.Width - SideWidth, 0);

                    // Draw the middle
                    for (int i = SideWidth; i < result.Size.Width - SideWidth; i++)
                        g.DrawImage(centerStrip, i, 0);
                }


                // Draw string to the image.
                g.DrawString(Text, stringFont, Brushes.Black, new PointF(Padding / 2, (result.Size - stringSize).Height / 2 + 1));

            }

            // Draw the final image.
            graphics.DrawImage(result,
                (Size.Width - result.Size.Width) / 2,
                (Size.Height - result.Size.Height) / 2);

        }

        internal delegate void GameUpdateEvent(Graphics graphics, double gameTime, double elapsedTime);
        internal event GameUpdateEvent Updated;

        const int BaseSleepTimeMS = 5;

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Timing control
            long StartTimeMS = timer.ElapsedMilliseconds;

            GameTime = StartTimeMS / 1000.0;

            double elapsedTime = GameTime - lastTime;
            lastTime = GameTime;


            // Game state update
            if (Playing && !HasEnded)
                Update(GameTime, elapsedTime);


            // Draw everything
            Draw(e.Graphics);


            // Let interested listerners know that we just updated,
            // and give them access to our Graphics object
            // so that they can draw things if they need to.
            if (Updated != null)
                Updated(e.Graphics, GameTime, elapsedTime);


            // Update throttling
            int ElapsedMS = (int)(timer.ElapsedMilliseconds - StartTimeMS + 0.5);

            if (ElapsedMS < BaseSleepTimeMS)
                System.Threading.Thread.Sleep(BaseSleepTimeMS - ElapsedMS);


            //Force the next Paint()
            this.Invalidate();
        }

        #region Play/Pause Handling
        
        public bool Playing { get; private set; }

        public event EventHandler OnPlay;
        public void Play()
        {
            if (Playing) return;

            Playing = true;

            Updated -= Game_Updated_Paused;

            timer.Start();

            Cursor.Hide();

            this.Invalidate();

            if (OnPlay != null)
                OnPlay(this, new EventArgs());
        }

        public event EventHandler OnPause;
        public void Pause(bool ShowGraphic)
        {
            if (!Playing) return;

            Playing = false;

            timer.Stop();

            Cursor.Show();

            if (OnPause != null)
                OnPause(this, new EventArgs());

            if (ShowGraphic)
                Updated += Game_Updated_Paused;
        }

        public void TogglePlayPause()
        {
            if (Playing)
                Pause(true);
            else
                Play();
        }

        void Game_Updated_Paused(Graphics graphics, double gameTime, double elapsedTime)
        {
            DrawStatusDisplay(graphics, "Paused");
        }

        #endregion

        public virtual void Update(double gameTime, double elapsedTime)
        {
            if (NextLevel == null && Melvin.IsDead)
            {
                SpawnMelvin();
            }


            foreach (Sprite sprite in AllSprites.Reverse<Sprite>())
            {
                if (!sprite.IsDisposed)
                {
                    sprite.Update(gameTime, elapsedTime);

                    foreach (Sprite sprite2 in AllSprites.Reverse<Sprite>())
                    {
                        if (!sprite.IsDisposed && sprite.TestCollision(sprite2))
                        {
                            sprite.OnCollide(sprite2);
                        }
                    }
                }
            }

            CurrentLevel.Update(gameTime, elapsedTime);
        }

        public virtual void Draw(Graphics graphics)
        {
            foreach (Sprite sprite in GameSprites)
            {
                sprite.Draw(graphics);
            }

            CurrentLevel.Draw(graphics);
        }

        #region Level Management

        private Level NextLevel;
        internal void TriggerNextLevelTransition()
        {
            if (NextLevel == null)
            {
                int oldLevelNumber = CurrentLevel.LevelNumber;

                if (CurrentLevel.IsNextLevelDesert())
                    NextLevel = new DessertLevel(this, oldLevelNumber);
                else
                    NextLevel = new FoodLevel(this, oldLevelNumber + 1);

                Updated += LevelTriggering_Stage1;
            }
        }

        internal void LevelTriggering_Stage1(Graphics graphics, double gameTime, double elapsedTime)
        {
            Updated -= LevelTriggering_Stage1;
            Updated += LevelTriggering_Stage2;

            AudioManager.GetSound("Munchies.Resources.Sounds.levelsound.ogg").Play();

            if (NextLevel is DessertLevel)
                DrawStatusDisplay(graphics, string.Format("Prepare for Dessert!"));
            else
                DrawStatusDisplay(graphics, string.Format("Prepare for Level {0}", NextLevel.LevelNumber));

            AllSprites.RemoveAll(CurrentLevel.LevelSprites.Contains);
        }

        internal void LevelTriggering_Stage2(Graphics graphics, double gameTime, double elapsedTime)
        {
            Updated -= LevelTriggering_Stage2;

            timer.Stop();

            System.Threading.Thread.Sleep(1500);

            timer.Start();

            CurrentLevel = NextLevel;
            NextLevel = null;

            Melvin.SetLoc_AtBottomOfGame();
            Melvin.ResetVelocity();
        }

        #endregion



        private bool disposed;
        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (CurrentLevel != null)
                    {
                        CurrentLevel.Dispose();
                    }

                    if (NextLevel != null)
                    {
                        NextLevel.Dispose();
                    }

                    timer = null;

                    GameMode = null;
                    Melvin = null;

                    BackgroundImage = null;

                    // Iteration done in reverse because sprites are removed from the
                    // list as they are disposed which causes the list to shift.
                    foreach (Sprite sprite in GameSprites.Reverse<Sprite>())
                        sprite.Dispose();


                }

                // There are no unmanaged resources to release, but
                // if we add them, they need to be released here.
            }

            disposed = true;

            base.Dispose(disposing);
        }
    }
}
