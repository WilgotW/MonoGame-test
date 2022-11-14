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

        
        double gt = 0;
        static double staticGt = 0;
        double timeSinceLast = 0;
        int enemiesKilled = 0;

        private SpriteFont font;
        public static int score = 2000;
        Texture2D backgroundPath1Texture;
        Texture2D background1Texture;

        
        Texture2D monster1Texture;
        static public Texture2D monster1Idle;
        static public Texture2D monster1Hit;
        
        Texture2D turretBaseTexture;
        static Texture2D basicTurretIdle;
        static Texture2D basicTurretShoot;
        
        Texture2D moneyCounterTexture;

        Texture2D cardBasicTurret;
        static Texture2D shootSpeedUpgrade;
        static Texture2D rangeUpgrade;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        MouseController mouseController;
        public static GraphicsDeviceManager static_graphics;

        

        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            static_graphics = _graphics;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            
        }
        protected override void Initialize()
        {
            base.Initialize();
            _graphics.PreferredBackBufferWidth = 1840;  
            _graphics.PreferredBackBufferHeight = 1000;   
            _graphics.ApplyChanges();
        }
        protected override void LoadContent()
        {
            

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Score");
            background1Texture = Content.Load<Texture2D>("background1");
            backgroundPath1Texture = Content.Load<Texture2D>("Level1");

            cardBasicTurret = Content.Load<Texture2D>("CardBasicTurret");
            shootSpeedUpgrade = Content.Load<Texture2D>("ShootSpeedUpgrade");
            rangeUpgrade = Content.Load<Texture2D>("RangeUpgrade");

            monster1Idle = Content.Load<Texture2D>("evilFlesh");
            monster1Hit = Content.Load<Texture2D>("evilFleshHit");

            turretBaseTexture = Content.Load<Texture2D>("TurretBaseV2");
            moneyCounterTexture = Content.Load<Texture2D>("MoneyCounter");

            basicTurretIdle = Content.Load<Texture2D>("BasicTurret");
            basicTurretShoot = Content.Load<Texture2D>("BasicTurretShoot");

            mouseController = new MouseController(turretList, turretBaseTexture.Width, turretBaseTexture.Height, gt);

            this.IsMouseVisible = true;
        }
        public static Texture2D changeMonster1Texture(Texture2D current){
            if(current == monster1Idle){
                return monster1Hit;
            }else{
                return monster1Idle;
            }
        }
        public static Texture2D changeTurretTexture(Texture2D current){
            if(current == basicTurretIdle){
                return basicTurretShoot;
            }else{
                return basicTurretIdle;
            }
        }
        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mouseController.MouseUpdate();

            mouseController.gt = gt;

            


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
                turret.enemies = enemyList;
                turret.EnemyUpdate();
                turret.SetGameTime(staticGt);
            }

            base.Update(gameTime);
        }
        void AddEnemy()
        {
            Vector2 pos = new Vector2(-64 / 2, (_graphics.PreferredBackBufferHeight / 2)+40);
            Enemy enemy = new Enemy(path1, pos, 60f, gt, 35, monster1Idle);
            enemyList.Add(enemy);
            enemy.Start();
        }
        public static void AddTurret(Vector2 position){
            if(score >= 500){
                score -= 500;
                Vector2 pos = position;
                UpgradeCard shootUppgradeCard = new UpgradeCard(shootSpeedUpgrade, new Vector2(static_graphics.PreferredBackBufferWidth - 232, static_graphics.PreferredBackBufferHeight/2 + 55), 0f, new Vector2(32, 32));
                UpgradeCard rangeUppgradeCard = new UpgradeCard(rangeUpgrade, new Vector2(static_graphics.PreferredBackBufferWidth - 232, static_graphics.PreferredBackBufferHeight/2 + 170), 0f, new Vector2(32, 32));
                Turret turret = new Turret(position, enemyList, 250f, staticGt, 60, basicTurretIdle, shootUppgradeCard, rangeUppgradeCard);

                turretList.Add(turret);
            }
            
        }
        public static void DeleteTurret(int turretIndex){
            turretList.RemoveAt(turretIndex);
            score += 200;
        }

        void EnemyDie(int index){
            enemiesKilled++;
            score += 50;
            enemyList.RemoveAt(index);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            DrawTexture(background1Texture, new Vector2(32, 32), 0, new Vector2(32, 32));
            DrawTexture(backgroundPath1Texture, new Vector2(32, 32), 0, new Vector2(32, 32));
            DrawTexture(moneyCounterTexture, new Vector2(_graphics.PreferredBackBufferWidth/2, _graphics.PreferredBackBufferHeight - moneyCounterTexture.Height/2), 0, new Vector2(32, 32));
            DrawText(font, new Vector2(_graphics.PreferredBackBufferWidth/2, _graphics.PreferredBackBufferHeight - moneyCounterTexture.Height + 25), score.ToString());
            
            
            // DrawTexture(shootSpeedUpgrade, new Vector2(_graphics.PreferredBackBufferWidth - 232,_graphics.PreferredBackBufferHeight/2 + 55), 0f, new Vector2(32, 32));
            // DrawTexture(rangeUpgrade, new Vector2(_graphics.PreferredBackBufferWidth - 232,_graphics.PreferredBackBufferHeight/2 + 170), 0f, new Vector2(32, 32));
            


            foreach(Enemy enemy in enemyList)
            {
                DrawTexture(enemy.monster1Texture, enemy.Position, 0, new Vector2(32, 32));
            }
            foreach(Turret turret in turretList){
                DrawTexture(turretBaseTexture, turret.position, 0, new Vector2(32, 32));
                DrawTexture(turret.basicTurretTexture, turret.position, turret.rotation, new Vector2(32, 32));

                if(turret.showUpgrades){
                    DrawTexture(cardBasicTurret, new Vector2(_graphics.PreferredBackBufferWidth - cardBasicTurret.Width, -cardBasicTurret.Height/2 + _graphics.PreferredBackBufferHeight/2), 0f, new Vector2(32, 32));
                    DrawTexture(turret.shootUppgrade.texture, turret.shootUppgrade.position, turret.shootUppgrade.rotation, turret.shootUppgrade.offset);
                    DrawTexture(turret.rangeUppgrade.texture, turret.rangeUppgrade.position, turret.rangeUppgrade.rotation, turret.rangeUppgrade.offset);
                }
            }
            

            base.Draw(gameTime);
        }

        void DrawTexture(Texture2D texture, Vector2 position, float rotation, Vector2 offset)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(
                texture,
                position,
                null,
                Color.White,
                rotation,
                offset,
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
