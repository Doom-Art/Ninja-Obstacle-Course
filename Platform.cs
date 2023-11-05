﻿using Microsoft.Xna.Framework;
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
        private bool _fadingIn;
        private bool _doesFade;
        private int _originalY;
        public Platform(Texture2D tex, Rectangle rect, Color color)
        {
            _texture = tex;
            _locRect = rect;
            _color = color;
            _opacity = 1;
            _doesFade = false;
            _fadeTime = 0.65f;
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
        public void fade()
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
