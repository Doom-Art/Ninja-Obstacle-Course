using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninja_Obstacle_Course
{
    public class Ice
    {
        private Texture2D _tex;
        private Vector2 _position, _speed;
        private float _gravity, _maxGrav, _targetX, _fallSpeed;
        public Ice(Texture2D tex, Vector2 position, float targetX, float gravity, int difficulty)
        {
            _tex = tex;
            _position = position;
            if (targetX-position.X < 0)
                _speed = new Vector2(-3,-5);
            else 
                _speed = new Vector2(3,-5);
            _maxGrav = gravity;
            _targetX = targetX;
            _gravity = -5;
            _fallSpeed = 80 - (difficulty * 10);
        }
        public void Update()
        {
            if (_gravity<_maxGrav)
                _gravity += _maxGrav/(_fallSpeed);
            _speed.Y = _gravity;
            if ((int)_position.X == (int)_targetX || (int)_position.X == (int)_targetX + 1 || (int)_position.X == (int)_targetX + 2)
                _speed.X = 0;
            _position += _speed; 
            HitBox = new Rectangle((int)_position.X + 3, (int)_position.Y + 6, 18, 18);
        }
        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(_tex, _position, Color.White);
        }
        public Rectangle HitBox { get; private set; }
    }
}
