using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
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
            TrainStation,
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
        Texture2D skyBackgroundTexture;
        Rectangle skyBackgroundRect;
        Rectangle introAnimationRect;
        Rectangle trainAnimationRect;
        Rectangle backButtonCollisionRect;
        int animationNum;
        int animation2Num;
        Texture2D trainStationBackgroundTexture;
        Rectangle trainStationBackgroundRect;
        Texture2D rectangleTexture;
        //CollisionRectangle rect1;
        float seconds;
        float startTime;

        List<Texture2D> introAnimation;
        List<Texture2D> trainAnimation;

        bool walking;      
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

            introAnimation = new List<Texture2D>();
            trainAnimation = new List<Texture2D>();

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
            skyBackgroundRect = new Rectangle(0, 0, 800, 600);
            introAnimationRect = new Rectangle(0, 0, 800, 600);
            trainAnimationRect = new Rectangle(0, 0, 800, 600);
            trainStationBackgroundRect = new Rectangle(-400, -600, 1500, 1300);
            backButtonCollisionRect = new Rectangle(745, 3, 50, 50);
            walking = false;
            animationNum = 0;

            base.Initialize();
            //rect1 = new CollisionRectangle(rectangleTexture, 10, 10);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            for (int i  = 0; i < 53; i++)
            {
                introAnimation.Add(Content.Load<Texture2D>("IntroAnimation/animation " + i));
            }

            for (int i = 0; i < 41; i++)
            {
                trainAnimation.Add(Content.Load<Texture2D>("TrainAnimation/animation " + i));
            }


            introScreenTexture = Content.Load<Texture2D>("introscreen");
            titleTexture = Content.Load<Texture2D>("title");
            arcadeClassicFont = Content.Load<SpriteFont>("pixelFont");
            paperBackgroundTexture = Content.Load<Texture2D>("paper");
            optionsBackgroundTexture = Content.Load<Texture2D>("optionsScreen");
            buttonTexture = Content.Load<Texture2D>("buttonTexture");
            mainCharacterSpritesheet = Content.Load<Texture2D>("spritesheet");
            bedroomTexture = Content.Load<Texture2D>("bedroom");
            skyBackgroundTexture = Content.Load<Texture2D>("skyBackground");
            trainStationBackgroundTexture = Content.Load<Texture2D>("trainStation");
            rectangleTexture = Content.Load<Texture2D>("rectangle");
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
            seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
            
            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (startButtonCollisionRect.Contains(mouseState.X, mouseState.Y))
                    {
                        screen = Screen.OpeningAnimation;
                        startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                    }
                    else if (optionsButtonCollisionRect.Contains(mouseState.X, mouseState.Y))
                    {
                        screen = Screen.Options;
                    }
                }
            }
            else if (screen == Screen.Options)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    if (backButtonCollisionRect.Contains(mouseState.X, mouseState.Y))
                        screen = Screen.Intro;                
            }
            else if (screen == Screen.OpeningAnimation)
            {               
                if (seconds >= 0.2)
                {
                    startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                    animationNum += 1;
                }
                if (animationNum >= 52)
                {
                    screen = Screen.TrainStation;
                    startTime = (float)gameTime.TotalGameTime.TotalSeconds;

                }
            }
            else if (screen == Screen.TrainStation)
            {                
                if (seconds >= 0.2)
                {
                    startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                    animation2Num += 1;
                }
                else if (animation2Num >= 41)
                {                   
                    startTime = (float)gameTime.TotalGameTime.TotalSeconds;               
                }
                if (mainCharacterRect.Left <= 95 && Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    mainCharacterRect.X = 95;
                    trainStationBackgroundRect.X += 2;
                    if (trainStationBackgroundRect.Left >= 0)
                    {
                        trainStationBackgroundRect.X = 0;
                    }
                }              
                if (mainCharacterRect.Top <= 95 && Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    mainCharacterRect.Y = 95;
                    trainStationBackgroundRect.Y += 2;
                    if (trainStationBackgroundRect.Top >= 0)
                    {
                        trainStationBackgroundRect.Y = 0;
                    }                 
                }              
                if (mainCharacterRect.Right >= 700 && Keyboard.GetState().IsKeyDown(Keys.Right))
                {                  
                    mainCharacterRect.X = 652;
                    trainStationBackgroundRect.X -= 2;
                    if (trainStationBackgroundRect.Right <= 800)
                    {                     
                        trainStationBackgroundRect.X = -700;
                    }
                }
                if (mainCharacterRect.Bottom >= 505 && Keyboard.GetState().IsKeyDown(Keys.Down))
                {                  
                    mainCharacterRect.Y = 400;
                    trainStationBackgroundRect.Y -= 2;
                    if (trainStationBackgroundRect.Bottom <= 600)
                    {
                        trainStationBackgroundRect.Y = -700;
                    }
                }
                   
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
                _spriteBatch.Draw(buttonTexture, backButtonCollisionRect, Color.White);
                _spriteBatch.DrawString(arcadeClassicFont, "X", new Vector2(760, 8), Color.White);
                _spriteBatch.DrawString(arcadeClassicFont, "Instructions", new Vector2(265, 130), Color.Black);
                _spriteBatch.DrawString(arcadeClassicFont, "Up Arrow     Up", new Vector2(200, 200), Color.Black);
                _spriteBatch.DrawString(arcadeClassicFont, "Left Arrow     Left", new Vector2(200, 250), Color.Black);
                _spriteBatch.DrawString(arcadeClassicFont, "Right Arrow     Right", new Vector2(200, 300), Color.Black);
                _spriteBatch.DrawString(arcadeClassicFont, "Down Arrow     Down", new Vector2(200, 350), Color.Black);
                _spriteBatch.DrawString(arcadeClassicFont, "I     Interact", new Vector2(200, 400), Color.Black);
            }
            else if (screen == Screen.OpeningAnimation)
            {              
                if (animationNum < 52)
                {
                    _spriteBatch.Draw(introAnimation[animationNum], introAnimationRect, Color.White);
                }
                else if (animationNum == 52)
                {
                    _spriteBatch.Draw(introAnimation[52], introAnimationRect, Color.White);
                }
                
            }
            else if (screen == Screen.TrainStation)
            {           
                _spriteBatch.Draw(trainStationBackgroundTexture, trainStationBackgroundRect, Color.White);
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
                    _spriteBatch.Draw(mainCharacterTextures[0], mainCharacterRect, Color.White);
                    if (walking)
                    {
                    _spriteBatch.Draw(mainCharacterTextures[1], mainCharacterRect, Color.White);
                    _spriteBatch.Draw(mainCharacterTextures[2], mainCharacterRect, Color.White);  
                    }
                }

                if (animation2Num < 41)
                {
                _spriteBatch.Draw(trainAnimation[animation2Num], trainAnimationRect, Color.White);
                }
                
            }
            _spriteBatch.End();
        
            base.Draw(gameTime);           
        }
    }

    //To Do:
    //add collison rects
    //_spriteBatch.Draw(skyBackgroundTexture, skyBackgroundRect, Color.White);
    //_spriteBatch.Draw(bedroomTexture, bedroomRect, Color.White);
    
    
}