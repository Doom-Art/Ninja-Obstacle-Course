using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninja_Obstacle_Course
{
    public class RedWalker
    {
        private Texture2D _spriteSheetTex;
        private Rectangle _positionRect;
        private Rectangle[] _sourceRects;
        private Vector2 _startLoc, _endLoc;
        private int _speed, _state, _prevState;
        private bool _left;
        private float _timer;

        public RedWalker(Texture2D spriteSheet, Rectangle[] sourceRects, Rectangle location, Vector2 startArea, Vector2 endArea)
        {
            _spriteSheetTex = spriteSheet;
            _positionRect = location;
            _sourceRects = sourceRects;
            _startLoc = startArea; 
            _endLoc = endArea;
            _speed = 4;
            _left = false;
            _prevState = 2;
        }
        public void Update(GameTime gameTime)
        {
            if (_timer > 250)
            {
                if (_state == 1)
                {
                    if (_prevState == 2)
                    {
                        _state = 0;
                        _prevState++;
                    }
                    else
                    {
                        _prevState = 2;
                        _state = 2;
                    }
                }
                else
                    _state = 1;
                _timer = 0;
            }
            else
            {
                _timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            //Movement
            if ( _left )
            {
                if (_positionRect.X > _startLoc.X)
                    _positionRect.X -= _speed;

                else
                {
                    _left = false;
                }
            }
            else
            {
                if (_positionRect.X < _endLoc.X)
                    _positionRect.X += _speed;
                else
                    _left = true;
            }
        }
        public void Draw(SpriteBatch sprite)
        {
            if (_left)
                sprite.Draw(_spriteSheetTex, _positionRect, _sourceRects[_state], Color.White);
            else
                sprite.Draw(_spriteSheetTex, _positionRect, _sourceRects[_state+3], Color.White);
        }
    }
}
