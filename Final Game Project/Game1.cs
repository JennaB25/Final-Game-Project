using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;

namespace Final_Game_Project
{
    public class Game1 : Game
    {      
        enum Screen
        {
            Intro,
            Options,
            OpeningAnimation,
            End
        }
        Screen screen;
        MouseState mouseState;
        Texture2D introScreenTexture;
        Rectangle introScreenRect;
        Texture2D titleTexture;
        Rectangle titleRect;
        SpriteFont arcadeClassicFont;
        Texture2D paperBackgroundTexture;
        Rectangle paperBackgroundRect;
        Rectangle paperBackgroundRect2;
        Texture2D optionsBackgroundTexture;
        Rectangle optionsBackgroundRect;
        Texture2D buttonTexture;     
        Rectangle startButtonCollisionRect;
        Rectangle optionsButtonCollisionRect;
        //VideoPlayer videoPlayer;
        //Video openingAnimation;     

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();
            this.Window.Title = "Kovari Mail";

            screen = Screen.Intro;
            introScreenRect = new Rectangle(0, 0, 800, 600);
            titleRect = new Rectangle(130, 100, 550, 100);
            paperBackgroundRect = new Rectangle(130, 250, 550, 300);
            optionsBackgroundRect = new Rectangle(0, 0, 800, 600);
            paperBackgroundRect2 = new Rectangle(100, 50, 600, 500);           
            startButtonCollisionRect = new Rectangle(200, 320, 410, 70);
            optionsButtonCollisionRect = new Rectangle(200, 400, 410, 70);
            //videoPlayer = new VideoPlayer();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            introScreenTexture = Content.Load<Texture2D>("introscreen");
            titleTexture = Content.Load<Texture2D>("title");
            arcadeClassicFont = Content.Load<SpriteFont>("pixelFont");
            paperBackgroundTexture = Content.Load<Texture2D>("paper");
            optionsBackgroundTexture = Content.Load<Texture2D>("optionsScreen");
            buttonTexture = Content.Load<Texture2D>("buttonTexture");
            //openingAnimation = Content.Load<Video>("openingAnimation")
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mouseState = Mouse.GetState();

            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (startButtonCollisionRect.Contains(mouseState.X, mouseState.Y))
                    {
                        screen = Screen.OpeningAnimation;
                    }
                    else if (optionsButtonCollisionRect.Contains(mouseState.X, mouseState.Y))
                    {
                        screen = Screen.Options;
                    }
                }
            }
            if (screen == Screen.Options)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.B))
                    screen = Screen.Intro;
            }
            if (screen == Screen.OpeningAnimation)
            {
                //videoPlayer.Play(openingAnimation);
            }

                base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Bisque);

            _spriteBatch.Begin();
            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(introScreenTexture, introScreenRect, Color.White);
                _spriteBatch.Draw(titleTexture, titleRect, Color.White);
                _spriteBatch.Draw(paperBackgroundTexture, paperBackgroundRect, Color.White);
                _spriteBatch.Draw(buttonTexture, startButtonCollisionRect, Color.White);
                _spriteBatch.Draw(buttonTexture, optionsButtonCollisionRect, Color.White);
                _spriteBatch.DrawString(arcadeClassicFont, "Start", new Vector2(350, 335), Color.Black);
                _spriteBatch.DrawString(arcadeClassicFont, "Options", new Vector2(330, 415), Color.Black);
            }
            else if (screen == Screen.Options)
            {
                _spriteBatch.Draw(optionsBackgroundTexture, optionsBackgroundRect, Color.White);
                _spriteBatch.Draw(paperBackgroundTexture, paperBackgroundRect2, Color.White);
                _spriteBatch.DrawString(arcadeClassicFont, "Instructions", new Vector2(265, 130), Color.Black);
                _spriteBatch.DrawString(arcadeClassicFont, "P     Pause  Screen", new Vector2(200, 200), Color.Black);
            }
            else if (screen == Screen.OpeningAnimation)
            {
                //_spriteBatch.Draw(videoPlayer.GetTexture, new Rectangle(0, 0, 800, 600), Color.White);
            }
            _spriteBatch.End();

            //To Do:
            //add custom icon to monogame file

            base.Draw(gameTime);
        }
    }
}