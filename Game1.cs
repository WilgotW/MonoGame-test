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
        List<String> path1 = new List<string>() { "r6.7", "u3.3", "r6.45", "d6.5", "r6.6", "u7.55", "r1" };
        static List<Enemy> enemyList = new List<Enemy>();
        static List<Turret> turretList = new List<Turret>();

        MouseController mouseController = new MouseController();
        double gt = 0;
        static double staticGt = 0;
        double timeSinceLast = 0;
        int enemiesKilled = 0;

        private SpriteFont font;
        public static int score = 900;
        Texture2D backgroundPath1Texture;
        Texture2D background1Texture;
        Texture2D monster1Texture;
        Texture2D turretBaseTexture;
        Texture2D turretShooterTexture;
        Texture2D moneyCounterTexture;

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
            _graphics.PreferredBackBufferWidth = 1920;  
            _graphics.PreferredBackBufferHeight = 1080;   
            _graphics.ApplyChanges();
        }
        protected override void LoadContent()
        {
            
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Score");
            background1Texture = Content.Load<Texture2D>("background1");
            backgroundPath1Texture = Content.Load<Texture2D>("Level1");
            monster1Texture = Content.Load<Texture2D>("evilFlesh");
            turretBaseTexture = Content.Load<Texture2D>("TurretBase");
            turretShooterTexture = Content.Load<Texture2D>("TurretShooter");
            moneyCounterTexture = Content.Load<Texture2D>("MoneyCounter");

            this.IsMouseVisible = true;
        }
        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mouseController.MouseUpdate();

            gt = gameTime.TotalGameTime.TotalMilliseconds;
            staticGt = gt;
            if (gt > timeSinceLast + 1500)
            {
                if(enemyList.Count + enemiesKilled < 20){
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
                for (int i = 0; i < enemyList.Count; i++)
                {
                    if(enemyList[i].Health <= 0){
                        EnemyDie(i);
                    }
                }
            }

            foreach(Turret turret in turretList){
                turret.EnemyUpdate();
                turret.SetGameTime(staticGt);
            }

            base.Update(gameTime);
        }
        void AddEnemy()
        {
            Vector2 pos = new Vector2(-monster1Texture.Width / 2, (_graphics.PreferredBackBufferHeight / 2));
            Enemy enemy = new Enemy(path1, pos, 60f, gt, 35);
            enemyList.Add(enemy);
            enemy.Start();
        }
        public static void AddTurret(Vector2 position){
            if(score >= 500){
                score -= 500;
                Vector2 pos = position;
                Turret turret = new Turret(position, enemyList, 250f, staticGt, 100);
                turretList.Add(turret);
            }
            
        }

        void EnemyDie(int index){
            enemiesKilled++;
            score += 50;
            enemyList.RemoveAt(index);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            DrawTexture(background1Texture, new Vector2(32, 32), 0);
            DrawTexture(backgroundPath1Texture, new Vector2(32, 32), 0);
            DrawTexture(moneyCounterTexture, new Vector2(_graphics.PreferredBackBufferWidth/2, _graphics.PreferredBackBufferHeight - moneyCounterTexture.Height/2), 0);

            DrawText(font, new Vector2(_graphics.PreferredBackBufferWidth/2, _graphics.PreferredBackBufferHeight - moneyCounterTexture.Height + 25), score.ToString());
            
            foreach(Enemy enemy in enemyList)
            {
                DrawTexture(monster1Texture, enemy.Position, 0);
            }
            foreach(Turret turret in turretList){
                DrawTexture(turretBaseTexture, turret.position, 0);
                DrawTexture(turretShooterTexture, turret.position, turret.rotation);
            }
            

            base.Draw(gameTime);
        }

        void DrawTexture(Texture2D texture, Vector2 position, float rotation)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(
                texture,
                position,
                null,
                Color.White,
                rotation,
                new Vector2(monster1Texture.Width / 2, monster1Texture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f
            );
            _spriteBatch.End();
        }

        void DrawText(SpriteFont spriteFont, Vector2 position, String content){
            _spriteBatch.Begin();
            _spriteBatch.DrawString(
                font, 
                content, 
                position, 
                Color.Black
            );
            _spriteBatch.End();
        } 
    }
}
