using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
// using System.Formats.Asn1;
using static System.Net.Mime.MediaTypeNames;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;


namespace TestGame
{
    public class Instructor
    {
        
        
        public List<String> instructions = new List<string>();
        // public Vector2 ob {get; set;}
        // public int obSpeed {get; set;}
        
        // public Vector2 ballPos { get; set; }
        public Instructor(List<String> instructions)
        {
            this.instructions = instructions;
            // this.ob = ob;
            // this.obSpeed = obSpeed;
        }

        public void createInstructions()
        {
            for (int i = 0; i < instructions.Count; i++)
            {    
                createSection(instructions[i]);  
                createInstructions(); 
            }
            
        }

        public void createSection(string instruction)
        {
            char firstLetter = instruction[0];
            int amount = 1;
            if (instruction.Length > 1)
            {
                //multiple of the same instruction is declared
                amount = int.Parse(instruction.Remove(0, 1));
                amount++;
            }
            
            switch (firstLetter)
            {
                case 'r':
                    printInstruction("Moving Right", amount);
                    Game1.changeDir('r');
                    break;
                case 'l':
                    printInstruction("Moving Left", amount);
                    Game1.changeDir('l');
                    break;
                case 'u':
                    printInstruction("Moving Up", amount);
                    Game1.changeDir('u');
                    break;
                case 'd':
                    printInstruction("Moving Down", amount);
                    Game1.changeDir('d');
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