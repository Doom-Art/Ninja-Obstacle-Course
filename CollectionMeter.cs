using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninja_Obstacle_Course
{
    public class CollectionMeter
    {
        readonly Texture2D _texture;
        Rectangle _location;
        float _timer;
        Color _color;
        readonly int _maxSize;

        public CollectionMeter(Texture2D texture, Color color, int maxSize)
        {
            _texture = texture;
            _color = color;
            _maxSize = maxSize;
            _location = new Rectangle(1, 1, maxSize, 20);
            _timer = 0;
        }
        public CollectionMeter Clone()
        {
            return new CollectionMeter(_texture, _color, _maxSize);
        }
        public void Update(GameTime gameTime, Player player)
        {
            _location.X = (int)player.Position.X - 20;
            _location.Y = (int)player.Position.Y - 20;

            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_timer > 5)
            {
                _location.Width -= 1;
            }
        }
        public void Gain()
        {
            _location.Width += 10;
            if (_location.Width > _maxSize)
            {
                _location.Width = _maxSize;
            }
        }
        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(_texture, _location, _color);
        }
    }
}
