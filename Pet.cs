using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninja_Obstacle_Course
{
    public class Pet
    {
        private Texture2D _texture;
        private Rectangle _location;
        private Rectangle[] _sourceRects;
        private float _animationTimer;
        private int _currentAnimation;
        private bool _locked, _left;

        public Pet (Texture2D texture)
        {
            _texture = texture;
            _sourceRects = new Rectangle[4]
            {
                new Rectangle(0,0,texture.Width/2,texture.Height/2),
                new Rectangle(0,texture.Height/2,texture.Width/2,texture.Height/2),
                new Rectangle(texture.Width / 2,0,texture.Width / 2,texture.Height/2),
                new Rectangle(texture.Width / 2,texture.Height/2,texture.Width / 2,texture.Height/2),
            };
        }
        public void Update(Rectangle rect, bool left)
        {
            _left = left;
            if (left)
            {
                _location = new Rectangle(rect.X + 70, rect.Y, 30, 30);
            }
            else
            {
                _location = new Rectangle(rect.X - 40, rect.Y, 30, 30);
            }
            _animationTimer++;
            if (_animationTimer > 50)
            {
                _animationTimer = 0;
                if (_currentAnimation == 0)
                {
                    _currentAnimation = 1;
                }
                else
                    _currentAnimation = 0;
            }
        }
        public void Draw(SpriteBatch sprite)
        {
            if (_left)
            {
                sprite.Draw(_texture, _location, _sourceRects[_currentAnimation], Color.White);

            }
            else
            {
                sprite.Draw(_texture, _location, _sourceRects[_currentAnimation+2], Color.White);
            }
        }
    }
}
