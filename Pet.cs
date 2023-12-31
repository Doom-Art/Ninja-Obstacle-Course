﻿using Microsoft.Xna.Framework;
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
        private Vector2 _location, _targetLocation;
        private Rectangle[] _sourceRects;
        private float _animationTimer;
        private int _currentAnimation, _price;
        private bool _locked, _left;
        private readonly string _name;

        public Pet (Texture2D texture, int price, string name)
        {
            _texture = texture;
            _sourceRects = new Rectangle[4]
            {
                new (0,0,texture.Width/2,texture.Height/2),
                new (0,(texture.Height/2),texture.Width/2,texture.Height/2),
                new ((texture.Width / 2),0,texture.Width / 2,texture.Height/2),
                new ((texture.Width / 2),(texture.Height/2),texture.Width / 2,texture.Height/2),
            };
            _locked = true;
            _price = price;
            _name = name;
        }
        public Pet Copy
        {
            get { return new Pet(_texture, _price, _name); }
        }
        public int Price
        {
            get { return _price; }
        }
        public bool Locked
        {
            get { return _locked; }
        }
        public void Update(Vector2 position, bool left, float speed)
        {
            if (left)
            {
                _targetLocation = new Vector2(position.X + 50, position.Y);
            }
            else
            {
                _targetLocation = new Vector2(position.X - 50, position.Y);
            }
            float distanceX = Math.Abs(_targetLocation.X - _location.X);
            if (distanceX > 150)
            {
                _location = _targetLocation;
                _left = left;
            }
            else if (distanceX > speed)
            {
                if (_targetLocation.X > _location.X)
                {
                    _location.X += speed;
                    _left = false;
                }
                else
                {
                    _location.X -= speed;
                    _left = true;
                }
            }

            float distanceY = Math.Abs(_targetLocation.Y - _location.Y);
            if (distanceY > 150)
            {
                _location = _targetLocation;
                _left = left;
            }
            else if (distanceY > 5)
            {
                if (_targetLocation.Y > _location.Y)
                {
                    _location.Y += 4;
                }
                else
                {
                    _location.Y -= 4;
                }
            }

            _animationTimer++;
            if (_animationTimer > 20)
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
        public void DrawDisplay(SpriteBatch sprite)
        {
            sprite.Draw(_texture, new Rectangle(260, 200, 90, 90), _sourceRects[0], Color.White);
        }
        public void UnlockPet()
        {
            _locked = false;
        }
        private string Name
        {
            get { return _name; }
        }
        public void LockPet()
        {
            _locked = true;
        }
    }
}
