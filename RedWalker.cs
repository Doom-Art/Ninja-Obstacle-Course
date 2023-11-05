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
        private Texture2D _spriteSheetTex, _rectangleTex;
        private Rectangle _positionRect, _startLoc, _endLoc, _hitBox;
        private Rectangle[] _sourceRects;
        private int _speed, _state, _prevState;
        private bool _left;
        private float _timer;

        public RedWalker(Texture2D spriteSheet, Rectangle[] sourceRects, Rectangle location, int startArea, int endArea, Texture2D rectangleTex)
        {
            _spriteSheetTex = spriteSheet;
            _positionRect = location;
            _sourceRects = sourceRects;
            _startLoc = new Rectangle(startArea, _positionRect.Y, _positionRect.Width, _positionRect.Height);
            _endLoc = new Rectangle(endArea, _positionRect.Y, _positionRect.Width, _positionRect.Height);
            _speed = 4;
            _left = false;
            _state = 1;
            _prevState = 2;
            _rectangleTex = rectangleTex;
        }
        public RedWalker(Texture2D spriteSheet, Rectangle[] sourceRects, Rectangle location, int startArea, int endArea, Texture2D rectangleTex, bool startLeft)
        {
            _spriteSheetTex = spriteSheet;
            _positionRect = location;
            _sourceRects = sourceRects;
            _startLoc = new Rectangle(startArea, _positionRect.Y, _positionRect.Width, _positionRect.Height);
            _endLoc = new Rectangle(endArea, _positionRect.Y, _positionRect.Width, _positionRect.Height);
            _speed = 4;
            _left = startLeft;
            _state = 1;
            _prevState = 2;
            _rectangleTex = rectangleTex;
        }
        public void SetDifficulty(int difficulty)
        {
            _speed = 2 * difficulty;
        }
        public void Update(GameTime gameTime)
        {
            if (_timer > 200){
                if (_state == 0){
                    if (_prevState == 2){
                        _state = 1;
                        _prevState++;
                    }
                    else{
                        _prevState = 2;
                        _state = 2;
                    }
                }
                else
                    _state = 0;
                _timer = 0;
            }
            else
            {
                _timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            //Movement
            if ( _left ){
                if (_positionRect.X > _startLoc.X)
                    _positionRect.X -= _speed;
                else
                    _left = false;
            }
            else{
                if (_positionRect.X < _endLoc.X)
                    _positionRect.X += _speed;
                else
                    _left = true;
            }
            _hitBox = new Rectangle(_positionRect.X + 10, _positionRect.Y +10, _positionRect.Width - 20, _positionRect.Height-10);
        }
        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(_rectangleTex, _startLoc, Color.Orange * 0.7f);
            sprite.Draw(_rectangleTex, _endLoc, Color.Orange * 0.7f);
            if (_left)
                sprite.Draw(_spriteSheetTex, _positionRect, _sourceRects[_state], Color.White);
            else
                sprite.Draw(_spriteSheetTex, _positionRect, _sourceRects[_state+3], Color.White);
        }
        public Rectangle Position
        {
            get { return _hitBox; }
        }
    }
}
