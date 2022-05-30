using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Time_and_Sound_MonoGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        MouseState mouseState;
        Texture2D bombTexture;
        Rectangle bombRect;
        Texture2D boomTexture;
        Rectangle boomRect;
        SpriteFont timeFont;
        Texture2D rectangleTexture;
        Rectangle rectangleRect;
        Rectangle defusedRect;
        Texture2D defusedTexture;
        float seconds;
        float startTime;
        SoundEffect explode;
        bool bang;
        bool defused;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 500;
            // TODO: Add your initialization logic here
            bombRect = new Rectangle(50, 50, 700, 400);
            boomRect = new Rectangle(0, 0, 800, 500);
            rectangleRect = new Rectangle(487, 155, 190, 50);
            defusedRect = new Rectangle(0, 0, 800, 500);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            bombTexture = Content.Load<Texture2D>("bomb");
            timeFont = Content.Load<SpriteFont>("Time");
            explode = Content.Load<SoundEffect>("explosion");
            boomTexture = Content.Load<Texture2D>("boom");
            rectangleTexture = Content.Load<Texture2D>("rectangle");
            defusedTexture = Content.Load<Texture2D>("defused");
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
            if (rectangleRect.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
            {
                seconds = 0;
                defused = true;
            }
            if (seconds >= 15 && defused == false)
            {
                explode.Play();
                startTime = (float)gameTime.TotalGameTime.TotalSeconds;
            }
            if (defused == true)
                bang = false;
            if (seconds >= 15)
                bang = true;
            else if (bang == true && seconds >= explode.Duration.TotalSeconds)
                Exit();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
           
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            if (bang == false && defused == false)
                _spriteBatch.Draw(bombTexture, bombRect, Color.White);
            else if (bang == true)
                _spriteBatch.Draw(boomTexture, boomRect, Color.White);
            if (bang == false && defused == false)
                _spriteBatch.DrawString(timeFont, (15 - seconds).ToString("0:00"), new Vector2(270, 200), Color.Black);
            else if (defused == true)
                _spriteBatch.Draw(defusedTexture, defusedRect, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
