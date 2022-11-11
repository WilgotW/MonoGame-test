using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace TestGame
{
    public class Instructor
    {

        public GameTime gameTime;
        public double timeSinceLast;
        public int x = 0;
        int amount = 0;
        public List<String> instructions = new List<string>();
        // public Vector2 ob {get; set;}
        // public int obSpeed {get; set;}

        // public Vector2 ballPos { get; set; }
        public Instructor(List<String> inst)
        {
            this.instructions = inst;
            // this.ob = ob;
            // this.obSpeed = obSpeed;
        }

        public void CreateInstructions(double gameTime)
        {
            if (gameTime > timeSinceLast + (amount * 1000) || gameTime == 0)
            {
                
                CreateSection(instructions[x]);
                Console.WriteLine(instructions[x]);
                if (instructions.Count - 1 > x) 
                {
                    if (x > 0)
                    {
                        getTime();
                    }
                    x++;
                    
                }
                timeSinceLast = gameTime;
            }
        }

        public void getTime()
        {
            Console.WriteLine(instructions[x].Remove(0, 1));
            amount = int.Parse(instructions[x].Remove(0, 1));
        }


        public void CreateSection(string instruction)
        {

            char firstLetter = instruction[0];
            

            switch (firstLetter)
            {
                case 'r':
                    // printInstruction("Moving Right", amount);
                    Game1.changeDir(1, 0);
                    break;
                case 'l':
                    // printInstruction("Moving Left", amount);
                    Game1.changeDir(-1, 0);
                    break;
                case 'u':
                    // printInstruction("Moving Up", amount);
                    Game1.changeDir(0, -1);
                    break;
                case 'd':
                    // printInstruction("Moving Down", amount);
                    Game1.changeDir(0, 1);
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