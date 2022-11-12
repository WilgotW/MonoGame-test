using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace TestGame
{
    internal class Turret
    {

        public Vector2 position {get; set;}
        public List<Enemy> enemies = new List<Enemy>();
        private float turretRange {get; set;}
        List<double> allDistances = new List<double>();
        private double currentClosest = 10000;

        Enemy closestEnemy = null;
        
        public Turret(Vector2 position, List<Enemy> enemies, float turretRange){
            this.position = position;
            this.enemies = enemies;
            this.turretRange = turretRange;
        }

        public void CheckEnemyDistance(){
            GetClosest();
        }

        void GetClosest(){
            double distanceToClosestEnemy = 10000;
            closestEnemy = null;
            
            foreach(Enemy enemy in enemies){
                float yDistance = enemy.Position.Y - position.Y;
                float xDistance = enemy.Position.X - position.X;

                double pyt = (xDistance * xDistance) + (yDistance * yDistance);
                double distanceToEnemy = Math.Sqrt(pyt);

                if(distanceToEnemy < distanceToClosestEnemy && distanceToEnemy < turretRange){
                    distanceToClosestEnemy = distanceToEnemy;
                    closestEnemy = enemy;
                }
            }
            if(closestEnemy != null){

                Console.WriteLine(closestEnemy.Position);    
            }
        }

        void ShootEnemy(){
            
        }
    }
}