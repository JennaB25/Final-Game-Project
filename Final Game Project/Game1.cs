using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;

namespace Final_Game_Project
{
    public class Game1 : Game
    {      
        enum Screen
        {
            Intro,
            Options,
            OpeningAnimation,
            House,
            End
        }
        Screen screen;
        MouseState mouseState;
        KeyboardState keyboardState;
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
        Texture2D mainCharacterSpritesheet;
        List<Texture2D> mainCharacterTextures;
        Rectangle mainCharacterRect;
        Texture2D cropTexture;
        Rectangle sourceRect;
        Texture2D bedroomTexture;
        Rectangle bedroomRect;
        bool walking;
        int walkingValue;       
        bool up;
        bool down;
        bool left;
        bool right;
            
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
            mainCharacterRect = new Rectangle(300, 300, 49, 105);
            mainCharacterTextures = new List<Texture2D>();
            bedroomRect = new Rectangle(-60, 0, 900, 600);
            walking = false;
            walkingValue = 1;        

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
            mainCharacterSpritesheet = Content.Load<Texture2D>("spritesheet");
            bedroomTexture = Content.Load<Texture2D>("bedroom");
            int width = mainCharacterSpritesheet.Width / 12;
            int height = mainCharacterSpritesheet.Height;
            for (int y = 0; y < 1; y++) 
                for (int x = 0; x < 12; x++) 
                {
                    sourceRect = new Rectangle(x * width, y * height, width, height);
                    cropTexture = new Texture2D(GraphicsDevice, width, height);
                    Color[] data = new Color[width * height];
                    mainCharacterSpritesheet.GetData(0, sourceRect, data, 0, data.Length);
                    cropTexture.SetData(data);
                    if (mainCharacterTextures.Count < 12)
                        mainCharacterTextures.Add(cropTexture);
                }                 
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();          

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
                if (keyboardState.IsKeyDown(Keys.M))
                {
                    screen = Screen.House;
                }
            }
            if (screen == Screen.House)
            {
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    up = true;
                    mainCharacterRect.Y -= 2;
                }
                else
                    up = false;
                    down = true;
                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    down = true;
                    walking = true;
                    if (walking)
                    {
                        mainCharacterRect.Y += 2;
                    }   
                }
                else                   
                    walking = false;
                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    right = true;
                    mainCharacterRect.X += 2;                 
                }
                else
                    right = false;
                    down = true;
                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    left = true;
                    mainCharacterRect.X -= 2;                 
                }
                else
                    left = false;
                    down = true;

            }
                base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightBlue);

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

            }
            else if (screen == Screen.House)
            {
                _spriteBatch.Draw(bedroomTexture, bedroomRect, Color.White);
                if (up)
                {
                    _spriteBatch.Draw(mainCharacterTextures[3], mainCharacterRect, Color.White);
                }
                else if (right)
                {
                    _spriteBatch.Draw(mainCharacterTextures[6], mainCharacterRect, Color.White);
                }
                else if (left)
                {
                    _spriteBatch.Draw(mainCharacterTextures[9], mainCharacterRect, Color.White);
                }
                else if (down)
                {
                    if (walking)
                    {
                        if (walkingValue == 1)
                        {
                            _spriteBatch.Draw(mainCharacterTextures[1], mainCharacterRect, Color.White);
                            walkingValue *= -1;
                        }
                        else if (walkingValue == -1)
                        {
                            _spriteBatch.Draw(mainCharacterTextures[2], mainCharacterRect, Color.White);
                            walkingValue *= -1;
                        }
                    }
                    else
                    {
                        _spriteBatch.Draw(mainCharacterTextures[0], mainCharacterRect, Color.White);
                    }
                }
                

            }
            _spriteBatch.End();
        
            base.Draw(gameTime);
        }
    }

    //To Do:
    //add custom icon to monogame file
    //add a pixel sky background to bedroom
    
}