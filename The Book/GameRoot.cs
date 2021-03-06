﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Linq;

namespace The_Book
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameRoot : Game
    {
        // some helpful static properties
        public static GameRoot Instance { get; private set; }
        public static Viewport Viewport { get { return Instance.GraphicsDevice.Viewport; } }
        public static Vector2 ScreenSize { get { return new Vector2(Viewport.Width, Viewport.Height); } }
        public static GameTime GameTime { get; private set; }

        public static Texture2D Player { get; private set; }
        public static Texture2D Seeker { get; private set; }
        public static Texture2D Bullet { get; private set; }
        public static Texture2D Pointer { get; private set; }
        public static Texture2D Wanderer { get; private set; }
        public static Texture2D Boss { get; private set; }

        public static SpriteFont Font { get; private set; }
        public static SpriteFont RoundFont { get; private set; }

        public static Song Music { get; private set; }

        private static readonly Random rand = new Random();
        public static readonly Camera Camera = new Camera();

        private static SoundEffect[] explosions;

        public static DateTime current = new DateTime();
        // return a random explosion sound
        public static SoundEffect Explosion { get { return explosions[rand.Next(explosions.Length)]; } }

        private static SoundEffect[] shots;
        public static SoundEffect Shot { get { return shots[rand.Next(shots.Length)]; } }

        private static SoundEffect[] spawns;
        public static SoundEffect Spawn { get { return spawns[rand.Next(spawns.Length)]; } }

        public static Vector2 StartWorldPoint = new Vector2 (0,0);
        public static Vector2 EndWorldPoint = new Vector2(3000, 2000);
        public static Rectangle WorldSize = Extensions.toRect(StartWorldPoint, EndWorldPoint);

        GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;

        public GameRoot()
        {
            Instance = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = @"Content";

            graphics.PreferredBackBufferWidth = 1800;
            graphics.PreferredBackBufferHeight = 1000;
        }

        protected override void Initialize()
        {
            base.Initialize();

            EntityManager.Add(PlayerShip.Instance);

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.05f;
            MediaPlayer.Play(GameRoot.Music);

            Camera.ViewportWidth = graphics.GraphicsDevice.Viewport.Width;
            Camera.ViewportHeight = graphics.GraphicsDevice.Viewport.Height;
            Camera.MoveCamera(PlayerShip.Instance.Position);

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Player = Content.Load<Texture2D>("Art/Player");
            Seeker = Content.Load<Texture2D>("Art/Seeker");
            Bullet = Content.Load<Texture2D>("Art/Bullet");
            Pointer = Content.Load<Texture2D>("Art/Pointer");
            Wanderer = Content.Load<Texture2D>("Art/Wanderer");
            Boss = Content.Load<Texture2D>("Art/Boss");

            Font = Content.Load<SpriteFont>("Timer");
            RoundFont = Content.Load<SpriteFont>("Round");

            Music = Content.Load<Song>("Sound/Music");

            // These linq expressions are just a fancy way loading all sounds of each category into an array.
            explosions = Enumerable.Range(1, 8).Select(x => Content.Load<SoundEffect>("Sound/explosion-0" + x)).ToArray();
            shots = Enumerable.Range(1, 4).Select(x => Content.Load<SoundEffect>("Sound/shoot-0" + x)).ToArray();
            spawns = Enumerable.Range(1, 8).Select(x => Content.Load<SoundEffect>("Sound/spawn-0" + x)).ToArray();
        }

        protected override void Update(GameTime gameTime)
        {
            GameTime = gameTime;
            Input.Update();

            Camera.Update();

            // Allows the game to exit
            if (Input.WasButtonPressed(Buttons.Back) || Input.WasKeyPressed(Keys.Escape))
                this.Exit();
            Menu.Update();


            EntityManager.Update();
            EnemySpawner.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // Draw entities. Sort by texture for better batching.
            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.NonPremultiplied, null, null, null, null, Camera.TranslationMatrix);
            EntityManager.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.NonPremultiplied);
            // draw the custom mouse cursor
            spriteBatch.Draw(GameRoot.Pointer, Input.MousePosition, Color.White);

            spriteBatch.DrawString(GameRoot.Font, "Game Time: " + gameTime.TotalGameTime, new Vector2(15, 10), Color.GhostWhite);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawRightAlignedString(string text, float y)
        {
            var textWidth = GameRoot.Font.MeasureString(text).X;
            spriteBatch.DrawString(GameRoot.Font, text, new Vector2(ScreenSize.X - textWidth - 5, y), Color.White);
        }
    }
}
