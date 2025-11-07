using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using System.Collections.Generic;

namespace topic_2_mono_game
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        KeyboardState keyboardstate;
        Texture2D mowertexture;
        Texture2D longgrasstexture;

        Rectangle window;
        Vector2 mowerspeed;

        bool keypress;
        bool soundplaying;



        Rectangle mowerrectangle;
        SoundEffect mowerSound;
        SoundEffectInstance mowerSoundInstance;

        List<Rectangle> grassTiles;
        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            window = new Rectangle(0, 0, 600, 500);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();

            mowerrectangle = new Rectangle(100, 100, 30, 30);

            grassTiles = new List<Rectangle>();
            for (int x = 0; x < window.Width; x += 5)
                for (int y = 0; y < window.Height; y += 5)
                    grassTiles.Add(new Rectangle(x, y, 5, 5));
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            mowertexture = Content.Load<Texture2D>("images/mower");
            longgrasstexture = Content.Load<Texture2D>("images/long_grass");
            mowerSound = Content.Load<SoundEffect>("sounds/mower_sound");
            mowerSoundInstance = mowerSound.CreateInstance();
            mowerSoundInstance.IsLooped = true;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            keyboardstate = Keyboard.GetState();
            mowerspeed = Vector2.Zero;
            if ( keyboardstate.IsKeyDown(Keys.Space))
            {
                keypress = true;
            }
           if (keypress == true)
            {
                if (keyboardstate.IsKeyDown(Keys.W))
                {
                    mowerspeed.Y += -4;
                    mowerSoundInstance.Volume = 1.0f;
                }
                if (keyboardstate.IsKeyDown(Keys.A))
                {
                    mowerspeed.X += -4;
                    mowerSoundInstance.Volume = 1.0f;
                }
                if (keyboardstate.IsKeyDown(Keys.S))
                {
                    mowerspeed.Y += 4;
                    mowerSoundInstance.Volume = 1.0f;

                }
                if (keyboardstate.IsKeyDown(Keys.D))
                {
                    mowerspeed.X += 4;
                    mowerSoundInstance.Volume = 1.0f;
                }
                mowerrectangle.X += (int)mowerspeed.X;
                mowerrectangle.Y += (int)mowerspeed.Y;

            }
           
            if (mowerspeed != Vector2.Zero)
            {
                if (mowerSoundInstance.State != SoundState.Playing)
                {
                    mowerSoundInstance.Play();
                }
            }
            else
            {
                mowerSoundInstance.Pause();
            }
            for (int i = 0; i < grassTiles.Count; i++)
                if (mowerrectangle.Contains(grassTiles[i]))
                {
                    grassTiles.RemoveAt(i);
                    i--;

                }
            if (mowerspeed == Vector2.Zero)
            {
               
                mowerSoundInstance.Volume = 0.2f;
                mowerSoundInstance.Play();
            }
            if (keyboardstate.IsKeyDown(Keys.LeftShift))
            {
                keypress = false;
                mowerSoundInstance.Stop();
            }
            if (keyboardstate.IsKeyDown(Keys.Space))
            {
                soundplaying = true;

            }
            if (soundplaying == true)
            {
               
                mowerSoundInstance.Play();
            }
            if (mowerspeed != Vector2.Zero)
            {
                soundplaying = false;
               
            }
           



            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            foreach(Rectangle grass in grassTiles)
            {
                _spriteBatch.Draw(longgrasstexture, grass, Color.White);
            }
           
            _spriteBatch.Draw(mowertexture, mowerrectangle, Color.White);




            _spriteBatch.End();

            // TODO: Add your drawing code here


            base.Draw(gameTime);
        }
    }
}
