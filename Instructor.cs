using System;
using System.Formats.Asn1;
using static System.Net.Mime.MediaTypeNames;

namespace TestGame {
    internal class Instructor {

        
        public List<String> instructions = new List<string>();
        public Instructor(List<String> instructions){
            this.instructions = instructions;
        }

        public void createInstructions(){
            for (int i = 0; i < instructions.length; i++)
            {
                createSection(allInstructions[i]);
            }
        }

        public void createSection(string instruction){
            string firstLetter = instruction[0];
            int amount = 1;
            if(instruction.Length > 1){
                //multiple of the same instruction is declared
                amount = int.Parse(instruction.Remove(0, 1));
                amount++;
            }
            switch (firstLetter)
            {
                case "r":
                    printInstruction("Moving Right", amount);
                    break;
                case "l":
                    printInstruction("Moving Left", amount);
                    break;
                case "u":
                    printInstruction("Moving Up", amount);
                    break;
                case "d":
                    printInstruction("Moving Down", amount);
                    break;
                default:
            }

        }
        
        public string printInstruction(string text, int amount){
            for (int i = 0; i < amount; i++)
            {
                Console.WriteLine(text);
            }
        }

    }
}