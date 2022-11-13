using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TestGame
{
    internal class MouseController
    {
        public Vector2 mousePos = new Vector2();
        public MouseState mouseState = Mouse.GetState();
        private MouseState oldState;
        public List<Turret> _turretList = new List<Turret>();
        float turretWidth {get; set;}
        float turretHeight {get; set;}
        int hoveringTurretIndex = 0;
        bool hovering = false;
        public MouseController(List<Turret> _turretList, float turretWidth, float turretHeight){
            this._turretList = _turretList;
            this.turretWidth = turretWidth;
            this.turretHeight = turretHeight;
        }
        public void MouseUpdate(){
            var mousePosition = Mouse.GetState().Position;
            
            mousePos = new Vector2(mousePosition.X, mousePosition.Y);

            CheckTurretCollision();

            MouseState newState = Mouse.GetState(); 
            if(newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                if(mousePosition.Y < 900){
                    
                    Game1.AddTurret(mousePos);
                }
            }
            if(newState.RightButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                if(hovering){
                    _turretList.RemoveAt(hoveringTurretIndex);
                    Game1.DeleteTurret(hoveringTurretIndex);
                }
            }
            oldState = newState; 
            
        }

        void CheckTurretCollision(){
            

            var mousePosition = Mouse.GetState().Position;
            mousePos = new Vector2(mousePosition.X, mousePosition.Y);

            for (int i = 0; i < _turretList.Count; i++)
            {
                if(mousePos.X > _turretList[i].position.X - turretWidth/2 && mousePos.X < _turretList[i].position.X + turretWidth/2){
                    if(mousePos.Y > _turretList[i].position.Y - turretHeight/2 && mousePos.Y < _turretList[i].position.Y + turretHeight/2){
                        hovering = true;
                       
                        hoveringTurretIndex = i;
                    }else{
                        hovering = false;
                    }
                    
                }else{
                    hovering = false;
                }
                
            }
           
        }

    }
}