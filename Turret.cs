using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TestGame
{
    internal class Turret
    {

        public Vector2 position {get; set;}
        public List<Enemy> enemies = new List<Enemy>();
        private float turretRange {get; set;}
        List<double> allDistances = new List<double>();
        public float rotation;
        public int shootRate {get; set;}
        double timeSinceLast = 0;
        public double gt {get; set;}
        private double currentClosest = 10000;

        float lookDirX;
        float lookDirY;
        bool e = true;

        Enemy closestEnemy = null;
        
        public Turret(Vector2 position, List<Enemy> enemies, float turretRange, double gt, int shootRate){
            this.position = position;
            this.enemies = enemies;
            this.turretRange = turretRange;
            this.gt = gt;
            this.shootRate = shootRate;
        }
        
        public void SetGameTime(double _gt){
            gt = _gt;
        }
        public void EnemyUpdate(){
            GetClosest();
            GetRotation();
            
            if (gt > timeSinceLast + shootRate)
            {
                
                

                ShootEnemy();
                timeSinceLast = gt;
            }
        }


        void GetRotation(){
            if(closestEnemy != null){
                // Console.WriteLine(rotation);

                lookDirX = position.X - closestEnemy.Position.X;
                lookDirY = position.Y - closestEnemy.Position.Y;

                if(!e){
                    e = true;
                    Vector2 test = new Vector2(position.X + lookDirX/70, position.Y + lookDirY/70);
                    position = test;
                }else{
                    e = false;
                    Vector2 test = new Vector2(position.X - lookDirX/70, position.Y - lookDirY/70);
                    position = test;
                }
                

                rotation = (float)Math.Atan2(lookDirY, lookDirX);
            }
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

                // Console.WriteLine(closestEnemy.Position);    
            }
        }

        void ShootEnemy(){
            if(closestEnemy != null){
                // Console.WriteLine("Shooting at " + closestEnemy.Position);
                closestEnemy.TakeDamage(1);
            }

            
        }
    }
}