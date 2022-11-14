using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestGame
{
    internal class Enemy
    {
        public Texture2D monster1Texture {get; set;}
        public List<String> instructions = new List<string>();
        public Instructor instruction1;
        public double GameT;
        public Vector2 Position { get; set; }
        public float Speed { get; set; }
        public int Health {get; set;}
        public int DirX { get; set; }
        public int DirY { get; set; }
        public Vector2 Dir;

        private double timeSinceLast = 0;
        private double gt;

        public Enemy(List<String> instructions, Vector2 position, float speed, double gameT, int Health, Texture2D monster1Texture)
        {
            this.instructions = instructions;
            this.Position = position;
            this.Speed = speed;
            this.GameT = gameT;
            this.Health = Health;
            this.monster1Texture = monster1Texture;
        }

        public void Start()
        {
            instruction1 = new Instructor(instructions, GameT);
            instruction1.getTime();
        }
        public void UpdateEnemy(){
            if (GameT > timeSinceLast + 300)
            {
                if(monster1Texture == Game1.monster1Hit){
                    monster1Texture = Game1.changeMonster1Texture(monster1Texture);
                }
                
                timeSinceLast = GameT;
            }
        }

        public void TakeDamage(int dmg){
            Health -= dmg;
            monster1Texture = Game1.changeMonster1Texture(monster1Texture);
        }
        public void changeDir()
        {
            UpdateEnemy();
            instruction1.SetGameTime(GameT);
            DirX = instruction1.dx;
            DirY = instruction1.dy;
            Dir = new Vector2(DirX, DirY);
            instruction1.CreateInstructions(GameT);
        }
    }
}
