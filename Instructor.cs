using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace TestGame
{
    public class Instructor
    {
        public double timeSinceLast = 0;
        public int x = 0;
        int amount = 0;
        public List<String> instructions = new List<string>();
        public double GameT {get; set;}
        public int dx = 0;
        public int dy = 0;
        public Instructor(List<String> inst, double GameT)
        {
            this.instructions = inst;
            this.GameT = GameT;
        }
        public void SetGameTime(double gt){
            GameT = gt;
        }
        public void CreateInstructions(double gameTime)
        {
            if (GameT > timeSinceLast + (amount * 1000) || x == 0)
            {
                CreateSection(instructions[x]);
                if (instructions.Count - 1 > x) 
                {
                    if (x > 0)
                    {
                        getTime();
                    }
                    x++;
                }
                timeSinceLast = GameT;
            }
        }
        public void getTime()
        {
            amount = int.Parse(instructions[x].Remove(0, 1));
        }
        public void CreateSection(string instruction)
        {
            char firstLetter = instruction[0];
            switch (firstLetter)
            {
                case 'r':
                    // printInstruction("Moving Right", amount);
                    dx = 1;
                    dy = 0;
                    break;
                case 'l':
                    // printInstruction("Moving Left", amount);
                    dx = -1;
                    dy = 0;
                    break;
                case 'u':
                    // printInstruction("Moving Up", amount);
                    dx = 0;
                    dy = -1;
                    break;
                case 'd':
                    // printInstruction("Moving Down", amount);
                    dx = 0;
                    dy = 1;
                    break;
            }
        }
        public void printInstruction(string text, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Console.WriteLine(text);
            }
        }

    }
}