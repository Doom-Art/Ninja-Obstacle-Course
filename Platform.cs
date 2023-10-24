using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninja_Obstacle_Course
{
    public class Platform
    {
        private Rectangle _locRect;
        private Color _color;
        private Texture2D _texture;
        private float _opacity;
        private bool _fadingIn;
        public Platform(Texture2D tex, Rectangle rect, Color color)
        {
            _texture = tex;
            _locRect = rect;
            _color = color;
            _opacity = 1;
        }
        public Platform(Texture2D tex, Rectangle rect, Color color, float opacity)
        {
            _texture = tex;
            _locRect = rect;
            _color = color;
            _opacity = opacity;
            _fadingIn = false;
        }
        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(_texture, _locRect, _color*_opacity);
        }
        public float Opacity
        {
            get { return this._opacity; }
        }
        public void fade()
        {
            if (_fadingIn)
            {
                if (_opacity < 1)
                {
                    _opacity += 0.005f;
                }
                else
                {
                    _opacity = 1;
                    _fadingIn = false;
                }
            }
            else if (_opacity > 0)
            {
                _opacity -= 0.005f;
            }
            else
            {
                _opacity = 0;
                _fadingIn = true;
            }
        }
        public Rectangle Rectangle
        {
            get { return _locRect; }
        }
        public bool Intersects(Rectangle rect)
        {
            if (_opacity < 0.65f)
            {
                return false;
            }
            else 
            {
                return rect.Intersects(_locRect); 
            }
        }
        
    }
}
