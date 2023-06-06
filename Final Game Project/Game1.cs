﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
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
            TrainStation,
            Town,
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
        SpriteFont textFont;
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
        Texture2D trainStationCharacterTexture;
        Rectangle trainStationCharacterRect;
        Texture2D cropTexture;
        Rectangle sourceRect;       
        Rectangle introAnimationRect;
        Rectangle trainAnimationRect;
        Rectangle backButtonCollisionRect;
        int animationNum;
        int animation2Num;
        Texture2D trainStationBackgroundTexture;
        Rectangle trainStationBackgroundRect;
        Texture2D bubbleTexture;
        Rectangle bubbleRect;
        Texture2D rectangleTexture;
        Vector2 textLocation;        
        int walkingValue;      
        float seconds;
        float startTime;
        SoundEffect introAnimationSound;
        SoundEffectInstance introAnimationSEI;
        SoundEffect baseMusic;
        SoundEffectInstance baseMusicSEI;
        List<CollisionRect> barriers;
        CollisionRect rect1;
        CollisionRect rect2;
        CollisionRect rect3;
        Texture2D interactButtonTexture;
        Rectangle interactButtonRect;
        bool sideCharcterProximity;
        bool sideCharcterText;
        Texture2D townBackgroundTexture;
        Rectangle townBackgroundRect;
        Texture2D townTopLayerTexture;
        Rectangle townTopLayerRect;
        Rectangle paperBackgroundRect3;

        List<Texture2D> introAnimation;
        List<Texture2D> trainAnimation;
            
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
            mainCharacterRect = new Rectangle(300, 300, 40, 80);            
            mainCharacterTextures = new List<Texture2D>();           
            introAnimationRect = new Rectangle(0, 0, 800, 600);
            trainAnimationRect = new Rectangle(0, 0, 800, 600);
            trainStationBackgroundRect = new Rectangle(-400, -600, 1500, 1300);
            backButtonCollisionRect = new Rectangle(745, 3, 50, 50); 
            trainStationCharacterRect = new Rectangle(650, 270, 40, 80);
            bubbleRect = new Rectangle(660, 230, 100, 60);
            textLocation = new Vector2(670, 245);           
            interactButtonRect = new Rectangle(0, 0, 20, 20);
            townBackgroundRect = new Rectangle(0, -1500, 2500, 2300);
            townTopLayerRect = new Rectangle(0, -1500, 2500, 2300);
            paperBackgroundRect3 = new Rectangle(100, 50, 600, 300);
            walkingValue = 1;
            animationNum = 0;
            sideCharcterProximity = false;
            sideCharcterText = false;
            base.Initialize();
            barriers = new List<CollisionRect>();
            barriers.Add(new CollisionRect(rectangleTexture, 46, 418, 441, 100));
            barriers.Add(new CollisionRect(rectangleTexture, 582, 403, 168, 50));
            barriers.Add(new CollisionRect(rectangleTexture, 50, 104, 610, 143));                                         
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
            textFont = Content.Load<SpriteFont>("textFont");
            paperBackgroundTexture = Content.Load<Texture2D>("paper");
            optionsBackgroundTexture = Content.Load<Texture2D>("optionsScreen");
            buttonTexture = Content.Load<Texture2D>("buttonTexture");
            mainCharacterSpritesheet = Content.Load<Texture2D>("spritesheet");
            trainStationBackgroundTexture = Content.Load<Texture2D>("trainStation");
            rectangleTexture = Content.Load<Texture2D>("rectangle");
            introAnimationSound = Content.Load<SoundEffect>("introAnimationSound");
            trainStationCharacterTexture = Content.Load<Texture2D>("trainStationCharcter");
            bubbleTexture = Content.Load<Texture2D>("bubble");
            interactButtonTexture = Content.Load<Texture2D>("interactButton");
            townBackgroundTexture = Content.Load<Texture2D>("villageMap");
            townTopLayerTexture = Content.Load<Texture2D>("townTopLayer");
            introAnimationSEI = introAnimationSound.CreateInstance();
            introAnimationSEI.IsLooped = false;
            baseMusic = Content.Load<SoundEffect>("baseMusic");
            baseMusicSEI = baseMusic.CreateInstance();
            baseMusicSEI.IsLooped = true;
            int width = mainCharacterSpritesheet.Width / 3;
            int height = mainCharacterSpritesheet.Height / 4;
            for (int y = 0; y < 4; y++) 
                for (int x = 0; x < 3; x++) 
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
                baseMusicSEI.Play();
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (startButtonCollisionRect.Contains(mouseState.X, mouseState.Y))
                    {
                        baseMusicSEI.Stop();                       
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
                introAnimationSEI.Play();
                if (seconds >= 0.4)
                {
                    startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                    animationNum += 1;
                }
                if (animationNum >= 52)
                {
                    introAnimationSEI.Stop();
                    screen = Screen.TrainStation;
                    startTime = (float)gameTime.TotalGameTime.TotalSeconds;

                }
            }
            else if (screen == Screen.TrainStation)
            {
                baseMusicSEI.Play();
                if (seconds >= 0.2)
                {
                startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                animation2Num += 1;
                }
                else if (animation2Num >= 41)
                {                   
                        //               
                }
                CollisionRect._speed = new Vector2();
                if (mainCharacterRect.Left <= 95 && Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    trainStationBackgroundRect.X += 2;
                    mainCharacterRect.X = 95;
                    if (trainStationBackgroundRect.Left <= 0)
                    {
                        CollisionRect._speed.X = 2;                      
                        trainStationCharacterRect.X += 2;
                        bubbleRect.X += 2;
                        textLocation.X += 2;
                    }
                    else if (trainStationBackgroundRect.Left >= 0)
                    {
                        trainStationBackgroundRect.X = 0;
                    }
                }
                if (mainCharacterRect.Top <= 95 && Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    mainCharacterRect.Y = 95;
                    trainStationBackgroundRect.Y += 2;
                    if (trainStationBackgroundRect.Top <= 0)
                    {
                        CollisionRect._speed.Y = 2;
                        trainStationCharacterRect.Y += 2;
                        bubbleRect.Y += 2;
                        textLocation.Y += 2;
                    }
                    else if (trainStationBackgroundRect.Top >= 0)
                    {
                        trainStationBackgroundRect.Y = 0;
                    }
                }
                if (mainCharacterRect.Right >= 700 && Keyboard.GetState().IsKeyDown(Keys.Right))
                {                   
                    mainCharacterRect.X = 660;
                    trainStationBackgroundRect.X -= 2;
                    if (trainStationBackgroundRect.Right <= 800)
                    {
                        trainStationBackgroundRect.X = -700;
                        screen = Screen.Town;
                        mainCharacterRect.X = 40;
                        mainCharacterRect.Y = 400;
                        animation2Num = 0;
                    }
                    else if (trainStationBackgroundRect.Right >= 0)
                    {
                        CollisionRect._speed.X = -2;
                        trainStationCharacterRect.X -= 2;
                        bubbleRect.X -= 2;
                        textLocation.X -= 2;
                    }
                }
                if (mainCharacterRect.Bottom >= 505 && Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    mainCharacterRect.Y = 425;
                    trainStationBackgroundRect.Y -= 2;
                    if (trainStationBackgroundRect.Bottom <= 600)
                    {
                        trainStationBackgroundRect.Y = -700;
                    }
                    else if (trainStationBackgroundRect.Bottom >= 0)
                    {
                        CollisionRect._speed.Y = -2;                       
                        trainStationCharacterRect.Y -= 2;
                        bubbleRect.Y -= 2;
                        textLocation.Y -= 2;
                    }
                }
                foreach (CollisionRect barrier in barriers)
                    barrier.Move();
                //Side Train Station Charcter//
                if (trainStationCharacterRect.Intersects(mainCharacterRect))
                    sideCharcterProximity = true;
                else
                    sideCharcterProximity = false;
                interactButtonRect.X = mainCharacterRect.X + 25;
                interactButtonRect.Y = mainCharacterRect.Y - 25;

                if (keyboardState.IsKeyDown(Keys.I) && sideCharcterProximity)                                    
                    if (sideCharcterText)
                        sideCharcterText = false;
                    else
                        sideCharcterText = true;

                //---------------------------//
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    up = true;
                    if (seconds >= 0.2)
                    {
                        walkingValue *= -1;
                        startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                    }
                    mainCharacterRect.Y -= 2;
                }
                else
                    up = false;
                down = true;
                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    if (seconds >= 0.2)
                    {
                        walkingValue *= -1;
                        startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                    }
                    mainCharacterRect.Y += 2;
                }
                else
                    down = true;
                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    right = true;
                    if (seconds >= 0.2)
                    {
                        walkingValue *= -1;
                        startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                    }
                    mainCharacterRect.X += 2;
                }
                else
                    right = false;
                down = true;
                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    left = true;
                    if (seconds >= 0.2)
                    {
                        walkingValue *= -1;
                        startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                    }
                    mainCharacterRect.X -= 2;
                }
                else
                    left = false;
                down = true;


                foreach (CollisionRect barrier in barriers)
                {
                    if (barrier.Collide(mainCharacterRect))
                    {
                        if (keyboardState.IsKeyDown(Keys.Up))
                            mainCharacterRect.Y += 2;
                        if (keyboardState.IsKeyDown(Keys.Down))
                            mainCharacterRect.Y -= 2;
                        if (keyboardState.IsKeyDown(Keys.Left))
                            mainCharacterRect.X += 2;
                        if (keyboardState.IsKeyDown(Keys.Right))
                            mainCharacterRect.X -= 2;
                    }
                }
                
               
            }
            else if (screen == Screen.Town)
            {
                if (seconds >= 0.2)
                {
                    startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                    animation2Num += 1;
                }
                else if (animation2Num >= 17)
                {
                    //               
                }
                CollisionRect._speed = new Vector2();
                if (mainCharacterRect.Left <= 95 && Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    townBackgroundRect.X += 2;
                    townTopLayerRect.X += 2;
                    mainCharacterRect.X = 95;
                    if (townBackgroundRect.Left <= 0)
                    {
                        CollisionRect._speed.X = 2;                       
                    }
                    else if (townBackgroundRect.Left >= 0)
                    {
                        townBackgroundRect.X = 0;
                        townTopLayerRect.X = 0;
                    }
                }
                if (mainCharacterRect.Top <= 95 && Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    mainCharacterRect.Y = 95;
                    townBackgroundRect.Y += 2;
                    townTopLayerRect.Y += 2;
                    if (townBackgroundRect.Top <= 0)
                    {
                        CollisionRect._speed.Y = 2;                      
                    }
                    else if (townBackgroundRect.Top >= 0)
                    {
                        townBackgroundRect.Y = 0;
                        townTopLayerRect.Y = 0;
                    }
                }
                if (mainCharacterRect.Right >= 700 && Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    mainCharacterRect.X = 660;
                    townBackgroundRect.X -= 2;
                    townTopLayerRect.X -= 2;
                    if (townBackgroundRect.Right <= 800)
                    {
                        townBackgroundRect.X = -1700;
                        townTopLayerRect.X = -1700;
                    }
                    else if (townBackgroundRect.Right >= 0)
                    {
                        CollisionRect._speed.X = -2;                       
                    }
                }
                if (mainCharacterRect.Bottom >= 505 && Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    mainCharacterRect.Y = 425;
                    townBackgroundRect.Y -= 2;
                    townTopLayerRect.Y -= 2;
                    if (townBackgroundRect.Bottom <= 600)
                    {
                        townBackgroundRect.Y = -1700;
                        townTopLayerRect.Y = -1700;
                    }
                    else if (townBackgroundRect.Bottom >= 0)
                    {
                        CollisionRect._speed.Y = -2;                       
                    }
                }
                foreach (CollisionRect barrier in barriers)
                    barrier.Move();
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    up = true;
                    if (seconds >= 0.2)
                    {
                        walkingValue *= -1;
                        startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                    }
                    mainCharacterRect.Y -= 2;
                }
                else
                    up = false;
                down = true;
                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    if (seconds >= 0.2)
                    {
                        walkingValue *= -1;
                        startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                    }
                    mainCharacterRect.Y += 2;
                }
                else
                    down = true;
                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    right = true;
                    if (seconds >= 0.2)
                    {
                        walkingValue *= -1;
                        startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                    }
                    mainCharacterRect.X += 2;
                }
                else
                    right = false;
                down = true;
                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    left = true;
                    if (seconds >= 0.2)
                    {
                        walkingValue *= -1;
                        startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                    }
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
                _spriteBatch.DrawString(arcadeClassicFont, "Up  Arrow     Up", new Vector2(200, 200), Color.Black);
                _spriteBatch.DrawString(arcadeClassicFont, "Left  Arrow     Left", new Vector2(200, 250), Color.Black);
                _spriteBatch.DrawString(arcadeClassicFont, "Right  Arrow     Right", new Vector2(200, 300), Color.Black);
                _spriteBatch.DrawString(arcadeClassicFont, "Down  Arrow     Down", new Vector2(200, 350), Color.Black);
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
                //Rects//
                foreach (CollisionRect barrier in barriers)
                    barrier.Draw(_spriteBatch);              
                //----//
                _spriteBatch.Draw(trainStationBackgroundTexture, trainStationBackgroundRect, Color.White);               
                _spriteBatch.Draw(trainStationCharacterTexture, trainStationCharacterRect, Color.White);
                if (seconds < 1)
                {
                    _spriteBatch.Draw(bubbleTexture, bubbleRect, Color.White);
                    _spriteBatch.DrawString(textFont, "Hey over here!", textLocation, Color.Black);
                }
                if (sideCharcterProximity)
                {
                    _spriteBatch.Draw(interactButtonTexture, interactButtonRect, Color.White);
                }      
                if (up)
                {
                    if (walkingValue == 1)
                        _spriteBatch.Draw(mainCharacterTextures[7], mainCharacterRect, Color.White);
                    else if (walkingValue == -1)
                        _spriteBatch.Draw(mainCharacterTextures[8], mainCharacterRect, Color.White);
                    else
                        _spriteBatch.Draw(mainCharacterTextures[6], mainCharacterRect, Color.White);

                }
                else if (right)
                {
                    if (walkingValue == 1)
                        _spriteBatch.Draw(mainCharacterTextures[10], mainCharacterRect, Color.White);
                    else if (walkingValue == -1)
                        _spriteBatch.Draw(mainCharacterTextures[11], mainCharacterRect, Color.White);
                    else
                        _spriteBatch.Draw(mainCharacterTextures[9], mainCharacterRect, Color.White);
                }
                else if (left)
                {
                    if (walkingValue == 1)
                        _spriteBatch.Draw(mainCharacterTextures[4], mainCharacterRect, Color.White);
                    else if (walkingValue == -1)
                        _spriteBatch.Draw(mainCharacterTextures[5], mainCharacterRect, Color.White);
                    else
                        _spriteBatch.Draw(mainCharacterTextures[3], mainCharacterRect, Color.White);
                }
                else if (down)
                {
                    if (walkingValue == 1)
                        _spriteBatch.Draw(mainCharacterTextures[1], mainCharacterRect, Color.White);
                    else if (walkingValue == -1)
                        _spriteBatch.Draw(mainCharacterTextures[2], mainCharacterRect, Color.White);
                    else
                        _spriteBatch.Draw(mainCharacterTextures[0], mainCharacterRect, Color.White);
                }
                if (sideCharcterText)
                {
                    _spriteBatch.Draw(paperBackgroundTexture, paperBackgroundRect3, Color.White);
                    _spriteBatch.DrawString(textFont, "Hi, my name is Max. I'm supposed to pick you up. So I hear you're gonna", new Vector2(200, 150), Color.Black);
                    _spriteBatch.DrawString(textFont, "be taking over the local mail delivery buissness. You have pretty big", new Vector2(200, 170), Color.Black);
                    _spriteBatch.DrawString(textFont, "shoes to fill, let me tell you. The first guy had been doing it for over", new Vector2(200, 190), Color.Black);
                    _spriteBatch.DrawString(textFont, "50 years. That's until he went missing--I mean he went into retirement.", new Vector2(200, 210), Color.Black);
                    _spriteBatch.DrawString(textFont, "Anyways head down to the right into town and I'll meet you there.", new Vector2(200, 230), Color.Black);
                }
                if (animation2Num < 41)
                {
                    _spriteBatch.Draw(trainAnimation[animation2Num], trainAnimationRect, Color.White);
                }              

            }
            else if (screen == Screen.Town)
            {
                _spriteBatch.Draw(townBackgroundTexture, townBackgroundRect, Color.White);
                if (up)
                {
                    if (walkingValue == 1)
                        _spriteBatch.Draw(mainCharacterTextures[7], mainCharacterRect, Color.White);
                    else if (walkingValue == -1)
                        _spriteBatch.Draw(mainCharacterTextures[8], mainCharacterRect, Color.White);
                    else
                        _spriteBatch.Draw(mainCharacterTextures[6], mainCharacterRect, Color.White);

                }
                else if (right)
                {
                    if (walkingValue == 1)
                        _spriteBatch.Draw(mainCharacterTextures[10], mainCharacterRect, Color.White);
                    else if (walkingValue == -1)
                        _spriteBatch.Draw(mainCharacterTextures[11], mainCharacterRect, Color.White);
                    else
                        _spriteBatch.Draw(mainCharacterTextures[9], mainCharacterRect, Color.White);
                }
                else if (left)
                {
                    if (walkingValue == 1)
                        _spriteBatch.Draw(mainCharacterTextures[4], mainCharacterRect, Color.White);
                    else if (walkingValue == -1)
                        _spriteBatch.Draw(mainCharacterTextures[5], mainCharacterRect, Color.White);
                    else
                        _spriteBatch.Draw(mainCharacterTextures[3], mainCharacterRect, Color.White);
                }
                else if (down)
                {
                    if (walkingValue == 1)
                        _spriteBatch.Draw(mainCharacterTextures[1], mainCharacterRect, Color.White);
                    else if (walkingValue == -1)
                        _spriteBatch.Draw(mainCharacterTextures[2], mainCharacterRect, Color.White);
                    else
                        _spriteBatch.Draw(mainCharacterTextures[0], mainCharacterRect, Color.White);
                }
                _spriteBatch.Draw(townTopLayerTexture, townTopLayerRect, Color.White);
                if (animation2Num < 17)
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
    //make a class for diffrent quests  
    //fix player moving to edge  
    //change base music
    //fix text loading screen
    //add a way to go between screens
    //add rects to change screens
    //rect1 = new CollisionRect(rectangleTexture, 46, 418, 441, 100);
    //rect2 = new CollisionRect(rectangleTexture, 582, 403, 168, 50);
    //rect3 = new CollisionRect(rectangleTexture, 50, 104, 610, 143);


}