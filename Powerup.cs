using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninja_Obstacle_Course
{
    public class Powerup
    {
        private string _powerupName, _powerupDescription;
        private int _price;
        public Powerup(string name, string description,int price)
        {
            _powerupName= name;
            _price = price;
            _powerupDescription= description;
        }
        public float ElevatorBoost { get; set; }
        public float GravityBoost { get; set; }
        public float JumpIncrease { get; set; }
        public float JumpTimeIncrease { get; set; }
        public float SprintIncrease {  get; set; }
        public float SpeedIncrease {  get; set; }
        public bool SpikeRemoval { get; set; }
        public bool JSM { get; set; }
        public string Name { get { return _powerupName + ":"; } }
        public string Description { get { return _powerupDescription; } }
        public int Price { get { return _price; } }

        
    }
}
