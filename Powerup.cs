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
        private float _jumpIncrease, _sprintIncrease, _speedIncrease;
        private bool _spikeRemoval;
        private Texture2D _powerupTex;
        private int _price;
        public Powerup(Texture2D texture, int price)
        {
            _powerupTex = texture;
            _price = price;
        }
        public float JumpIncrease { get { return _jumpIncrease; } set { _jumpIncrease = value; } }
        public float SprintIncrease {  get { return _sprintIncrease; } set { _sprintIncrease = value; } }
        public float SpeedIncrease {  get { return _speedIncrease; } set { _speedIncrease = value; } }
        public bool SpikeRemoval { get { return _spikeRemoval; } set { _spikeRemoval = value; } }
        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(_powerupTex, new Rectangle(), Color.White);
        }
        public int Price { get { return _price; } }

        
    }
}
