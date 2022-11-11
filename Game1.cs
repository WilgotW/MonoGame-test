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
        List<String> path1 = new List<string>() { "r5", "d5", "r3", "u10", "r7", "d5", "r1" };

        Texture2D ballTexture;
        public Vector2 ballPosition;
        double gt = 0;
        double timeSinceLast = 0;
        static List<Enemy> enemyList;
        static Vector2 dir;
        float ballSpeed;
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
/*            dir = new Vector2(0, 0);
*/            // TODO: Add your initialization logic here
            /*List<String> inst = new List<string>() {"r5", "d5", "r3", "u10", "r7", "d5", "r1"};
            instruction1 = new Instructor(inst);*/
            

/*            ballPosition = new Vector2(0, _graphics.PreferredBackBufferHeight / 2);
*/
/*            ballSpeed = 40f;
*/           base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ballTexture = Content.Load<Texture2D>("ball");
/*            instruction1.getTime();
*/        }


        //var static: 
        public static void changeDir(int x, int y)
        {
            if(enemyList.Count > 0)
            {

                foreach(Enemy enemy in enemyList)
                {
                    enemy.Dir = new Vector2(x, y);
                }
            }
/*            dir = new Vector2(x, y);*/
        }

        void AddEnemy()
        {
            Vector2 pos = new Vector2(400, 400);
            Enemy enemy = new Enemy(path1, pos, 100f, gt);
            enemyList.Add(enemy);
            enemy.Start();
        }
        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            gt = gameTime.TotalGameTime.TotalMilliseconds;
            /*instruction1.CreateInstructions(gt);*/

            if (gt > timeSinceLast + 1000)
            {

                AddEnemy();
            }


            // ballPosition = instruction1.ob;
            if(enemyList.Count > 0)
            {
                foreach (Enemy enemy in enemyList)
                {
                    float enemyX = enemy.Position.X;
                    float enemyY = enemy.Position.Y;
                    enemyX += enemy.Speed * dir.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    enemyY += enemy.Speed * dir.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    enemy.Position = new Vector2(enemyX, enemyY);
                }
            }
            

            /*ballPosition.Y += ballSpeed * dir.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
            ballPosition.X += ballSpeed * dir.X * (float)gameTime.ElapsedGameTime.TotalSeconds;*/

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

            foreach(Enemy enemy in enemyList)
            {
                drawEnemy(enemy.Position);
            }

            /*drawEnemy(ballPosition);*/

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
