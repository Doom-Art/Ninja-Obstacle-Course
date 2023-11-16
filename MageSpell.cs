

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Ninja_Obstacle_Course
{
    public class MageSpell
    {
        private Texture2D _texture;
        private Vector2 _position, _speed;
        private Rectangle _hitBox;
        public MageSpell(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            _position = position;
        }
        public Vector2 Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        public Rectangle HitBox { get { return _hitBox; } }
        public void Update()
        {
            _position += _speed;
            _hitBox = new Rectangle((int)_position.X + 10, (int)_position.Y + 10, _texture.Width - 20, _texture.Height - 20);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
        public Vector2 Position
        {
            get { return _position; }
        }
    }
}
