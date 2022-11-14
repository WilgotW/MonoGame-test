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
        bool hasBeenPressed = false;
        public double gt {get; set;}
        public double timeSinceLast = 0;
        
        public MouseController(List<Turret> _turretList, float turretWidth, float turretHeight, double gt){
            this._turretList = _turretList;
            this.turretWidth = turretWidth;
            this.turretHeight = turretHeight;
            this.gt = gt;
        }
        public void MouseUpdate(){
            var mousePosition = Mouse.GetState().Position;
            mousePos = new Vector2(mousePosition.X, mousePosition.Y);

            CheckTurretCollision(mousePos);
            CheckUpgradeCollision(mousePos);
            
            if (gt > timeSinceLast + 1000)
            {
                hasBeenPressed = false;
                timeSinceLast = gt;
            }

            MouseState newState = Mouse.GetState(); 
            if(newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {   
                if(mousePosition.Y < 900){
                    if(_turretList.Count > 0){
                        if(_turretList[hoveringTurretIndex].shootUppgrade.hovering == true){
                            // if(_turretList[hoveringTurretIndex].damage < 2){
                                _turretList[hoveringTurretIndex].damage++;
                                Console.WriteLine("+1 damage");
                            // }
                        }
                        else if(_turretList[hoveringTurretIndex].mouseIsHovering){
                            Console.WriteLine("selecting");
                            
                            _turretList[hoveringTurretIndex].showUpgrades = true;
                        }else{
                            Game1.AddTurret(mousePos);

                            _turretList[hoveringTurretIndex].showUpgrades = false;
                        }
                    }else{
                        Game1.AddTurret(mousePos);
                    }
                }
                
            }
            if(newState.RightButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                if(!hasBeenPressed){
                    hasBeenPressed = true;
                    if(_turretList[hoveringTurretIndex].mouseIsHovering == true){
                        _turretList[hoveringTurretIndex].beenPressed = true;
                        Game1.DeleteTurret(hoveringTurretIndex);
                    }
                }
                
            }
            oldState = newState; 
            
        }
        void CheckUpgradeCollision(Vector2 mousePos){
            
            // Console.WriteLine(hovering);

            if(_turretList.Count > 0){
                if(mousePos.X > _turretList[hoveringTurretIndex].shootUppgrade.position.X - _turretList[hoveringTurretIndex].shootUppgrade.texture.Width/4 && mousePos.X < _turretList[hoveringTurretIndex].shootUppgrade.position.X + _turretList[hoveringTurretIndex].shootUppgrade.texture.Width){
                    if(mousePos.Y > _turretList[hoveringTurretIndex].shootUppgrade.position.Y - _turretList[hoveringTurretIndex].shootUppgrade.texture.Height/2 && mousePos.Y < _turretList[hoveringTurretIndex].shootUppgrade.position.Y + _turretList[hoveringTurretIndex].shootUppgrade.texture.Height/2){
                        // hasBeenPressed = false;
                        _turretList[hoveringTurretIndex].shootUppgrade.hovering = true;
                        

                    }else{
                        _turretList[hoveringTurretIndex].shootUppgrade.hovering = false;
                    }
                    
                }else{
                    _turretList[hoveringTurretIndex].shootUppgrade.hovering = false;
                }
            }
                
                
            
        }
        void CheckTurretCollision(Vector2 mousePos){
            // Console.WriteLine(hovering);

            for (int i = 0; i < _turretList.Count; i++)
            {
                if(mousePos.X > _turretList[i].position.X - turretWidth/2 && mousePos.X < _turretList[i].position.X + turretWidth/2){
                    if(mousePos.Y > _turretList[i].position.Y - turretHeight/2 && mousePos.Y < _turretList[i].position.Y + turretHeight/2){
                        // hasBeenPressed = false;
                        _turretList[i].mouseIsHovering = true;
                        
                        

                        hoveringTurretIndex = i;
                    }else{
                        _turretList[i].mouseIsHovering = false;
                    }
                    
                }else{
                    _turretList[i].mouseIsHovering = false;
                }
                
            }
           
        }

    }
}