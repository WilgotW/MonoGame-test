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
        List<String> path1 = new List<string>() { "r6", "u2", "r3", "d5", "r3", "u6", "r1" };
        static List<Enemy> enemyList = new List<Enemy>();
        static List<Turret> turretList = new List<Turret>();

        MouseController mouseController = new MouseController();
        double gt = 0;
        double timeSinceLast = 0;
        
        Texture2D ballTexture;
        Texture2D turretTexture;
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
            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ballTexture = Content.Load<Texture2D>("ball");
            turretTexture = Content.Load<Texture2D>("Turret");
            this.IsMouseVisible = true;
        }
        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mouseController.MouseUpdate();

            gt = gameTime.TotalGameTime.TotalMilliseconds;
            if (gt > timeSinceLast + 7000)
            {
                if(enemyList.Count < 2){
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

            foreach(Turret turret in turretList){
                turret.EnemyUpdate();
            }

            base.Update(gameTime);
        }
        void AddEnemy()
        {
            Vector2 pos = new Vector2(-ballTexture.Width / 2, _graphics.PreferredBackBufferHeight / 2);
            Enemy enemy = new Enemy(path1, pos, 50f, gt);
            enemyList.Add(enemy);
            enemy.Start();
        }
        public static void AddTurret(Vector2 position){
            Vector2 pos = position;
            Turret turret = new Turret(position, enemyList, 250f);
            turretList.Add(turret);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach(Enemy enemy in enemyList)
            {
                drawTexture(ballTexture, enemy.Position);
            }
            foreach(Turret turret in turretList){
                drawTexture(turretTexture, turret.position);
            }

            base.Draw(gameTime);
        }

        void drawTexture(Texture2D texture, Vector2 position)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(
                texture,
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
