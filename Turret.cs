using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace TestGame
{
    public class Turret
    {
        public Vector2 position {get; set;}

        public Turret(Vector2 position){
            this.position = position;
        }
    }
}