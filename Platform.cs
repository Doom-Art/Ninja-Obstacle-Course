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
        private float _opacity, _fadeTime;
        private bool _fadingIn, _doesFade;
        private bool _doesGrowY,_doesGrowX, _growing;
        private int _originalY, _maxLength;
        public Platform(Texture2D tex, Rectangle rect, Color color)
        {
            _texture = tex;
            _locRect = rect;
            _color = color;
            _opacity = 1;
            _doesFade = false;
            _fadeTime = 0.65f;
        }
        public Platform(Texture2D tex, Rectangle rect, Color color, bool growX, int maxLength)
        {
            _texture = tex;
            _locRect = rect;
            _color = color;
            _opacity = 1;
            _doesGrowX = growX;
            _doesGrowY = !growX;
            _maxLength = maxLength;
        }
        public Platform(Texture2D tex, Rectangle rect, int originY)
        {
            _texture = tex;
            _locRect = rect;
            _color = Color.White;
            _opacity = 1;
            _doesFade = false;
            _originalY = originY;
            _fadeTime = 0.65f;
        }
        public Platform(Texture2D tex, Rectangle rect, Color color, float opacity)
        {
            _texture = tex;
            _locRect = rect;
            _color = color;
            _opacity = opacity;
            _fadingIn = false;
            _doesFade = false;
            _fadeTime = 0.65f;
        }
        public Platform(Texture2D tex, Rectangle rect, Color color, float opacity, bool doesFade)
        {
            _texture = tex;
            _locRect = rect;
            _color = color;
            _opacity = opacity;
            _fadingIn = false;
            _doesFade = doesFade;
            _fadeTime = 0.65f;
        }
        public Platform(Texture2D tex, Rectangle rect, Color color, float opacity, bool doesFade, bool startFade)
        {
            _texture = tex;
            _locRect = rect;
            _color = color;
            _opacity = opacity;
            _fadingIn = startFade;
            _doesFade = doesFade;
            _fadeTime = 0.65f;
        }
        public void SetSpikeSize(int difficulty)
        {
            _locRect = new Rectangle(_locRect.X, _originalY - (10 * difficulty), 10 * difficulty, 10 * difficulty);
        }
        public void SetFadeDifficulty(int difficulty)
        {
            if (difficulty == 1)
            {
                _fadeTime = 0.5f;
            }
            else
                _fadeTime = 0.65f;
        }
        public void Draw(SpriteBatch sprite)
        {
            //if (_locRect.Width > 40 && _opacity == 1 && _locRect.Height< 100)
                //sprite.Draw(_texture, new Rectangle(_locRect.X - 5, _locRect.Y + 5, _locRect.Width, _locRect.Height), Color.Black*0.4f);
            if (!_doesFade)
                sprite.Draw(_texture, _locRect, _color*_opacity);
            else
            {
                if(_opacity >= _fadeTime)
                    sprite.Draw(_texture, _locRect, _color * _opacity);
                else
                    sprite.Draw(_texture, _locRect, Color.LightGreen * _opacity);
            }
        }
        public float Opacity
        {
            get { return this._opacity; }
        }
        public void SetGrow(bool growX, int maxLength)
        {
            if (growX)
                _doesGrowX = true;
            else
                _doesGrowY = true;
            _maxLength = maxLength;
        }
        public void Update()
        {
            if (_doesFade){
                if (_fadingIn){
                    if (_opacity < 1){
                        _opacity += 0.005f;
                    }
                    else{
                        _opacity = 1;
                        _fadingIn = false;
                    }
                }
                else if (_opacity > 0)
                    _opacity -= 0.005f;
                else{
                    _opacity = 0;
                    _fadingIn = true;
                }
            }
            else if (_doesGrowY)
            {
                if (_growing)
                {
                    if (_locRect.Height < _maxLength)
                        _locRect.Height += 1;
                    else
                        _growing = false;
                }
                else
                {
                    if (_locRect.Height > 0)
                        _locRect.Height -= 1;
                    else
                        _growing = true;
                }
            }
            else if (_doesGrowX)
            {
                if (_growing)
                {
                    if (_locRect.Width < _maxLength)
                        _locRect.Width += 1;
                    else
                        _growing = false;
                }
                else
                {
                    if (_locRect.Width > 0)
                        _locRect.Width -= 1;
                    else
                        _growing = true;
                }
            }
        }
        public Rectangle Rectangle
        {
            get { return _locRect; }
        }
        public bool DoesFade()
        {
            return _doesFade;
        }
        public bool Intersects(Rectangle rect)
        {
            if (_opacity < _fadeTime)
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
