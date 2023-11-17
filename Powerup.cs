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
        private float _jumpIncrease, _jumpTimeIncrease, _sprintIncrease, _speedIncrease, _elevatorBoost;
        private bool _spikeRemoval;
        private string _powerupName, _powerupDescription;
        private int _price;
        public Powerup(string name, string description,int price)
        {
            _powerupName= name;
            _price = price;
            _powerupDescription= description;
        }
        public float ElevatorBoost { get { return _elevatorBoost; } set { _elevatorBoost = value; } }
        public float JumpIncrease { get { return _jumpIncrease; } set { _jumpIncrease = value; } }
        public float JumpTimeIncrease { get { return _jumpTimeIncrease; } set { _jumpTimeIncrease = value; } }
        public float SprintIncrease {  get { return _sprintIncrease; } set { _sprintIncrease = value; } }
        public float SpeedIncrease {  get { return _speedIncrease; } set { _speedIncrease = value; } }
        public bool SpikeRemoval { get { return _spikeRemoval; } set { _spikeRemoval = value; } }
        public string Name { get { return _powerupName + ":"; } }
        public string Description { get { return _powerupDescription; } }
        public int Price { get { return _price; } }

        
    }
}
