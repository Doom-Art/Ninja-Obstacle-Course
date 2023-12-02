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
        public Ice(Texture2D tex, Vector2 position, Vector2 speed)
        {
            _tex = tex;
            _position = position;
            _speed = speed;
        }
        public void Update()
        {
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
