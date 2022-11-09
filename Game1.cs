using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
// using System.Formats.Asn1;
using static System.Net.Mime.MediaTypeNames;
using System.Collections;
using System.Collections.Generic;

namespace TestGame
{
    public class Game1 : Game
    {
        Texture2D ballTexture;
        Vector2 ballPosition;
        public static Vector2 dir;
        float ballSpeed;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Instructor instruction1;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            dir = new Vector2(0, 0);
            // TODO: Add your initialization logic here
            List<String> inst = new List<string>() { "l1", "w5", "u1", "d10", "u4" };

            instruction1 = new Instructor(inst);
            instruction1.createInstructions();

            ballPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            ballSpeed = 40f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ballTexture = Content.Load<Texture2D>("ball");
        }

        public static void changeDir (char direction){
           
            switch (direction)
            {
                case 'r':
                    dir = new Vector2(1, 0);
                    break;
                case 'l':
                    dir = new Vector2(-1, 0);  
                    break;
                case 'u':
                    dir = new Vector2(0, -1);
                    break;
                case 'd':
                    dir = new Vector2(0, 1); 
                    break;
            }
            
        }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //


            // ballPosition = instruction1.ob;

            ballPosition.Y += ballSpeed * dir.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
            ballPosition.X += ballSpeed * dir.X * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // TODO: Add your update logic here
            // var kstate = Keyboard.GetState();

            // if (kstate.IsKeyDown(Keys.Up))
            // {
            //     changeDir(true);
            // }

            // if (kstate.IsKeyDown(Keys.Right))
            // {
            //     changeDir(false);
            // }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(
                ballTexture,
                ballPosition,
                null,
                Color.White,
                0f,
                new Vector2(ballTexture.Width / 2, ballTexture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f
            );
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
