using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace TestGame
{
    internal class Enemy
    {
        public List<String> instructions = new List<string>();
        public Instructor instruction1;
        public double GameT;
        public Vector2 Position { get; set; }
        public float Speed { get; set; }
        public int DirX { get; set; }
        public int DirY { get; set; }
        public Vector2 Dir;

        public Enemy(List<String> instructions, Vector2 position, float speed, double gameT)
        {
            this.instructions = instructions;
            this.Position = position;
            this.Speed = speed;
            this.GameT = gameT;
        }

        public void Start()
        {
            instruction1 = new Instructor(instructions, GameT);
            instruction1.getTime();
        }

        //static: ?
        public void changeDir()
        {
            instruction1.SetGameTime(GameT);
            DirX = instruction1.dx;
            DirY = instruction1.dy;
            Dir = new Vector2(DirX, DirY);
            instruction1.CreateInstructions(GameT);
        }
    }
}
