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
        private readonly Texture2D _texture;
        private readonly Color _color;
        private readonly int _maxSize, _gainAmount;
        private readonly float _loseTime;
        private float _timer;
        private Rectangle _location;

        public CollectionMeter(Texture2D texture, Color color, int maxSize)
        {
            _texture = texture;
            _color = color;
            _maxSize = maxSize;
            _location = new Rectangle(1, 1, _maxSize, 20);
            _timer = 0;
        }
        private CollectionMeter(Texture2D texture, Color color, int maxSize, int difficulty)
        {
            _texture = texture;
            _color = color;
            _maxSize = maxSize;
            _location = new Rectangle(1, 1, maxSize, 20);
            _timer = 0;
            _gainAmount = 15;
            _loseTime = 4.2f - (difficulty);
        }
        public CollectionMeter Clone(int difficulty)
        {
            return new CollectionMeter(_texture, _color, _maxSize, difficulty);
        }
        public void Update(GameTime gameTime, Player player)
        {
            _location.X = (int)player.Position.X - 20;
            _location.Y = (int)player.Position.Y - 30;

            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_timer > _loseTime)
            {
                _location.Width -= 5;
                _timer = 0;
            }
        }
        public void Reset()
        {
            _timer = 0;
            _location = new Rectangle(1, 1, _maxSize, 20);
        }
        public void Gain()
        {
            _location.Width += _gainAmount;
            _timer = 0;
            if (_location.Width > _maxSize)
            {
                _location.Width = _maxSize;
            }
        }
        public int Size
        {
            get { return _location.Width; }
        }
        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(_texture, _location, _color*0.7f);
        }
    }
}
