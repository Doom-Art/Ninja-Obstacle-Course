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
        private int _speed;
        private bool _left;

        public RedWalker(Texture2D spriteSheet, Rectangle[] sourceRects, Rectangle location, Vector2 startArea, Vector2 endArea)
        {
            _spriteSheetTex = spriteSheet;
            _positionRect = location;
            _sourceRects = sourceRects;
            _startLoc = startArea; 
            _endLoc = endArea;
            _speed = 4;
            _left = false;
        }
        public void Update()
        {
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
    }
}
