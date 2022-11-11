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
        List<String> path1 = new List<string>() { "l5", "u5", "r3", "d2", "r7", "u1", "l1" };
        Texture2D ballTexture;
        public Vector2 ballPosition;
        double gt = 0;
        double timeSinceLast = 0;
        static List<Enemy> enemyList = new List<Enemy>();
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public Instructor instruction1;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ballTexture = Content.Load<Texture2D>("ball");
        }
        void AddEnemy()
        {
            Vector2 pos = new Vector2(400, 400);
            Enemy enemy = new Enemy(path1, pos, 40f, gt);
            enemyList.Add(enemy);
            enemy.Start();
        }
        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            gt = gameTime.TotalGameTime.TotalMilliseconds;
            if (gt > timeSinceLast + 3000)
            {
                if(enemyList.Count < 5){
                    AddEnemy();
                    timeSinceLast = gt;
                }
            }
            if(enemyList.Count > 0)
            {
                foreach (Enemy enemy in enemyList)
                {
                    enemy.GameT = gt;
                    enemy.changeDir();
                    float enemyX = enemy.Position.X;
                    float enemyY = enemy.Position.Y;
                    enemyX += enemy.Speed * enemy.DirX * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    enemyY += enemy.Speed * enemy.DirY * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    enemy.Position = new Vector2(enemyX, enemyY);
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach(Enemy enemy in enemyList)
            {
                drawEnemy(enemy.Position);
            }

            base.Draw(gameTime);
        }

        void drawEnemy(Vector2 position)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(
                ballTexture,
                position,
                null,
                Color.White,
                0f,
                new Vector2(ballTexture.Width / 2, ballTexture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f
            );
            _spriteBatch.End();
        }
    }
}
