using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Media;


namespace Munchies
{
	/// <summary>
	/// Sprite represents a 2d shape that has a location, velocity
	/// </summary>
	public abstract class Sprite
	{
		public PointF Velocity;
		public PointF Location;
		public SizeF Size;

		public float MaxVelocityX = 250;
		public float MaxVelocityY = 200;

		public Game Game;
		public Level Level;

		public double SpawnTime;

		public static Random Random = new Random();

		/// <summary>
		/// A dictonary table that links short file names (E.graphics. "MelvinS") to their System.Drawing.Image representation that has been loaded through PreloadImages().
		/// </summary>
		protected static Dictionary<string, Image> Images = new Dictionary<string, Image>();


		/// <summary>
		/// Constructs a sprite, attaching it to both a level and a game.
		/// 
		/// This ctor should only be used for sprites that are tied to a specific level. (Don't create Melvin using it).
		/// </summary>
		/// 
		/// <param name="levelInstance">The level that the sprite is being created for</param>
		public Sprite(Level levelInstance)
		{
			Level = levelInstance;
			levelInstance.LevelSprites.Add(this);

			Game = Level.Game;

			Game.AllSprites.Add(this);

			SpawnTime = Game.GameTime;
		}

		/// <summary>
		/// Constructs a sprite, attaching it to a game.
		/// 
		/// This ctor should only be used for sprites that should persist through multiple levels within a single game (Melvin).
		/// </summary>
		/// <param name="gameInstance">The game that the sprite is being created for</param>
		public Sprite(Game gameInstance)
		{
			Game = gameInstance;

			gameInstance.AllSprites.Add(this);
			gameInstance.GameSprites.Add(this);

			SpawnTime = Game.GameTime;
		}


		/// <summary>
		/// A base update method. This should be overridden by all sprites.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="elapsedTime"></param>
		public virtual void Update(double gameTime, double elapsedTime)
		{
			Update_MoveVelocity(elapsedTime);
		}


		/// <summary>
		/// The name of the image that should be drawn for the sprite.
		/// </summary>
		public string ImageName;

		/// <summary>
		/// A base draw method.
		/// </summary>
		/// <param name="graphics"></param>
		public virtual void Draw(Graphics graphics)
		{
			graphics.DrawImage(Images[ImageName], (int)Location.X, (int)Location.Y, Size.Width, Size.Height);
		}

		public bool IsDead;
		public event EventHandler Killed;

		public void Kill()
		{
			RemoveFromCollections();

			if (Killed != null)
				Killed(this, new EventArgs());

			IsDead = true;
		}

		/// <summary>
		/// Enum representing edges of the game frame that the sprite may be colliding with.
		/// </summary>
		[Flags]
		public enum EdgeCollisionTypes : byte
		{
			None = 0x0,
			Top = 0x1,
			Bottom = 0x2,
			Left = 0x4,
			Right = 0x8,
		}

		/// <summary>
		/// Tests what edges of the game frame the sprite is colliding with.
		/// </summary>
		/// <returns>Returns a EdgeCollisionTypes representing the edges that the sprite is intersecting.</returns>
		public EdgeCollisionTypes TestEdgeCollision()
		{
			EdgeCollisionTypes collisions = EdgeCollisionTypes.None;

			if (Location.X < 0)
				collisions |= EdgeCollisionTypes.Left;
			else if (Location.X > (float)Game.Size.Width - Size.Width)
				collisions |= EdgeCollisionTypes.Right;

			if (Location.Y < 0)
				collisions |= EdgeCollisionTypes.Top;
			else if (Location.Y > (float)Game.Size.Height - Size.Height)
				collisions |= EdgeCollisionTypes.Bottom;

			return collisions;
		}


		// Update submethods
		/// <summary>
		/// Move the sprite based on its velocity.
		/// </summary>
		/// <param name="elapsedTime">The elapsedTime variable from the base Update method.</param>
		public void Update_MoveVelocity(double elapsedTime)
		{
#if SCALEWITHSIZE
            Location.X += Velocity.X * (float)elapsedTime * game.ScaleFactor1DX;
            Location.Y += Velocity.Y * (float)elapsedTime * game.ScaleFactor1DY;
#else
			Location.X += Velocity.X * (float)elapsedTime;
			Location.Y += Velocity.Y * (float)elapsedTime;
#endif
		}

		/// <summary>
		/// Tests the sprite to ensure that it within the bounds of the game frame,
		/// and if it has passed outside, set it back inside and bounce off the wall it protruded from.
		/// </summary>
		public void Update_TestEdgeCollisionAndBounce()
		{
			EdgeCollisionTypes collisions = TestEdgeCollision();

			bool bind = false;

			if (collisions.HasFlag(EdgeCollisionTypes.Top) || collisions.HasFlag(EdgeCollisionTypes.Bottom))
			{
				Velocity.Y = -Velocity.Y;
				bind = true;
			}
			if (collisions.HasFlag(EdgeCollisionTypes.Left) || collisions.HasFlag(EdgeCollisionTypes.Right))
			{
				Velocity.X = -Velocity.X;
				bind = true;
			}

			if (bind)
				Update_BindWithinGame();
		}

		/// <summary>
		/// Checks and modifies a sprite's position to make sure that it lies within its Munchies.Game panel.
		/// </summary>
		public void Update_BindWithinGame()
		{
			Location.X = Math.Max(0, Location.X);
			Location.Y = Math.Max(0, Location.Y);

			Location.X = Math.Min(Location.X, Game.Size.Width - (int)Size.Width);
			Location.Y = Math.Min(Location.Y, Game.Size.Height - (int)Size.Height);
		}

		/// <summary>
		/// Update method that will position the sprite so that it will wrap around the screen.
		/// </summary>
		public void Update_WrapAround()
		{
			Update_WrapAround_LeftRight();

			Update_WrapAround_TopBottom();
		}

		public void Update_WrapAround_LeftRight()
		{
			if (Location.X + Size.Width < 0)
				// Left edge wraparound
				Location.X = Game.Size.Width - 0.1f;
			else if (Location.X > (float)Game.Size.Width)
				// Right edge wraparound
				Location.X = -Size.Width + 0.1f;
		}

		public void Update_WrapAround_TopBottom()
		{
			if (Location.Y + Size.Height < 0)
				// Top edge wraparound
				Location.Y = Game.Size.Height - 0.1f;
			else if (Location.Y > (float)Game.Size.Height)
				// Bottom edge wraparound
				Location.Y = -Size.Height + 0.1f;
		}

		/// <summary>
		/// Update method that causes the sprite to veer away from Melvin when it is near him.
		/// </summary>
		/// <param name="elapsedTime">The elapsedTime variable from the base Update method.</param>
		/// <param name="proximity">The distance (in pixels) at which the sprite will begin to avoid Melvin.</param>
		/// <param name="multiplier">A multipler that determines how quickly this sprite will change velocity. It should probably be in the order of 10^3</param>
		public void Update_AvoidMelvin(double elapsedTime, float proximity, float multiplier)
		{
			float Distance = (float)Math.Sqrt(
				Math.Pow(
					(Game.Melvin.Location.X + (Game.Melvin.Size.Width / 2)) -
					(Location.X + (Size.Width / 2)), 2)
				+
				Math.Pow(
					(Game.Melvin.Location.Y + (Game.Melvin.Size.Height / 2)) -
					(Location.Y + (Size.Height / 2)), 2)
				);
			float Delta = (float)elapsedTime * ((proximity - Distance) / proximity) * multiplier;

			if (Distance < proximity)
			{
				if (Game.Melvin.Location.X + Game.Melvin.Size.Width > Location.X + Size.Width)
					// The fast food is to the left of Melvin
					Velocity.X = Limit(Velocity.X - Delta, -MaxVelocityX, MaxVelocityX);
				else
					Velocity.X = Limit(Velocity.X + Delta, -MaxVelocityX, MaxVelocityX);

				if (Game.Melvin.Location.Y + Game.Melvin.Size.Height > Location.Y + Size.Height)
					// The fast food is to the left of Melvin
					Velocity.Y = Limit(Velocity.Y - Delta, -MaxVelocityY, MaxVelocityY);
				else
					Velocity.Y = Limit(Velocity.Y + Delta, -MaxVelocityY, MaxVelocityY);
			}
		}

		public void Update_VelocityTowardsMelvin(float MaxVelocity, float maxVariation)
		{
			Melvin melvin = Level.Game.Melvin;

			if (melvin != null)
			{
				float dist_x = -(Location.X - melvin.Location.X);
				float dist_y = -(Location.Y - melvin.Location.Y);


				float MaxAbs = Math.Max(Math.Abs(dist_x), Math.Abs(dist_y));
				float Ratio = MaxVelocity / MaxAbs;

				Velocity.X = Limit(dist_x * Ratio, Velocity.X - maxVariation, Velocity.X + maxVariation);
				Velocity.Y = Limit(dist_y * Ratio, Velocity.Y - maxVariation, Velocity.Y + maxVariation);
			}
		}

		public void Update_KillIfOffScreen()
		{
			if (Location.X + Size.Width < 0
			    || Location.X > (float)Game.Size.Width
			    || Location.Y + Size.Height < 0
			    || Location.Y > (float)Game.Size.Height
				)
				Kill();
		}

		// Miscellaneous positioning/velocity
		public void SetLoc_CenterOfGame()
		{
			Location.X = (Game.GameContainer.Size.Width - Size.Width) / 2;
			Location.Y = (Game.GameContainer.Size.Height - Size.Height) / 2;
		}


		public void SetLoc_Anywhere()
		{
			Location.X = Random.Next((int)(Game.Size.Width - Size.Width));
			Location.Y = Random.Next((int)(Game.Size.Height - Size.Height));
		}


		public void SetLoc_OnRandomEdge()
		{
			float max_x = Game.Size.Width - Size.Width;
			float max_y = Game.Size.Height - Size.Height;

			Location.X = Random.Next((int)max_x);
			Location.Y = Random.Next((int)max_y);

			switch (Random.Next(4))
			{
				case 0: // Set to the top edge
					Location.Y = 0;
					break;
				case 1: // Set to the bottom edge
					Location.Y = max_y;
					break;
				case 2: // Set to the left edge
					Location.X = 0;
					break;
				case 3: // Set to the right edge
					Location.X = max_x;
					break;
			}
		}

		public void SetLoc_AtBottomOfGame()
		{
			Location.X = (Game.Size.Width - Size.Width) / 2;
			Location.Y = Game.Size.Height - Size.Height;
		}

		public void ResetVelocity()
		{
			Velocity.X = 0;
			Velocity.Y = 0;
		}

		public static T Limit<T>(T value, T minValue, T maxValue)
			where T : IComparable
		{
			if (minValue.CompareTo(maxValue) == 1)
				throw new ArgumentException("minValue cannot be creater than maxValue");
			if (value.CompareTo(minValue) == -1)
				return minValue;
			if (value.CompareTo(maxValue) == 1)
				return maxValue;
			return value;
		}

		// Collision handling
		/// <summary>
		/// Tests if the sprite is colliding with another sprite
		/// </summary>
		/// <param name="spriteToTest">The sprite to check collision with</param>
		/// <returns>True if the sprites are colliding, otherwise false</returns>
		public bool TestCollision(Sprite spriteToTest)
		{
			return !(Location.X > spriteToTest.Location.X + spriteToTest.Size.Width
			         || Location.X + Size.Width < spriteToTest.Location.X
			         || Location.Y > spriteToTest.Location.Y + spriteToTest.Size.Height
			         || Location.Y + Size.Height < spriteToTest.Location.Y);
		}

		/// <summary>
		/// Delegate used to handle the Collide event.
		/// </summary>
		/// <param name="sprite2">The sprite that (this) has intersected with.</param>
		public delegate void CollideEventHandler(Sprite sprite2);

		/// <summary>
		/// Event that will be fired when any two sprites collide with one another.
		/// </summary>
		public event CollideEventHandler Collide;

		internal void OnCollide(Sprite sprite2)
		{
			if (Collide != null)
				Collide(sprite2);
		}


		// Image handling
		/// <summary>
		/// Preloads the image files that the sprite will use. Call SetImage(string fileName) to load one of the images.
		/// </summary>
		/// <param name="fileName">A string of the file name to load.
		/// 
		/// The file name should just be the name of the file as it exists in Munchies.Images, minus extension. (E.G. "MelvinS")</param>
		public void PreloadImages(string fileName)
		{
			if (!Images.ContainsKey(fileName))
				Images[fileName] = (Image)Properties.Resources.ResourceManager.GetObject(fileName);
		}

		/// <summary>
		/// Preloads the image files that the sprite will use. Call SetImage(string fileName) to load one of the images.
		/// </summary>
		/// <param name="fileNames">A string array of file names to load.
		/// 
		/// The file names should just be the names of the files as they exists in Munchies.Images, minus extension. (E.G. "MelvinS")</param>
		public void PreloadImages(string[] fileNames)
		{
			foreach (string fileName in fileNames)
				PreloadImages(fileName);
		}


		public void SetSizeToImage(string fileName)
		{
			Image image = Images[fileName];

#if SCALEWITHSIZE
            Size.Width = image.Size.Width * game.ScaleFactor1DX;
            Size.Height = image.Size.Height * game.ScaleFactor1DY;
#else
			Size.Width = image.Size.Width;
			Size.Height = image.Size.Height;
#endif
		}


		private void RemoveFromCollections()
		{
			Game.AllSprites.Remove(this);
			Game.GameSprites.Remove(this);

			if (Level != null)
				Level.LevelSprites.Remove(this);
		}
	}
}
