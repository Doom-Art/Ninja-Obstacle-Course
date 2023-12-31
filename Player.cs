﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ninja_Obstacle_Course
{
    public class Player
    {
        private Texture2D _texture;
        private bool _isjump;
        private float _jumpStartTime, _jumpTime, _jumpSpeed, _gravity, _maxJumpTime, _sprintSpeed, _speed, _acceleration;
        private bool _standingOnGround, _isWalking, _isLeft, _isInAir;
        private readonly Rectangle[] _spriteSheetPos;
        private int _position;
        private float _timer;
        private int _prevState;
        private float _opacity, _elevatorSpeed, _maxGrav;
        private Keys _left, _right, _jump, _sprint;

        public Player(Texture2D texture, Rectangle[] spriteSheet)
        {
            _isjump = false;
            _jumpSpeed = 6;
            _maxJumpTime = 0.6f;
            _spriteSheetPos = spriteSheet;
            _prevState = 2;
            _position = 1;
            _gravity = 1;
            _sprintSpeed = 6;
            _isLeft = false;
            _isWalking = false;
            _opacity = 1;
            _left = Keys.A;
            _right = Keys.D;
            _jump = Keys.Space;
            _sprint = Keys.LeftShift;
            _texture = texture;
        }
        public CollectionMeter Meter
        {
            get;set;
        }
        public void SetKeys(Keys left, Keys right, Keys jump, Keys sprint)
        {
            _left = left;
            _right = right;
            _jump = jump;
            _sprint = sprint;
        }
        public bool AutoSprint
        {
            get; set;
        }
        public Vector2 Position { get; set; }
        public void SetLeft(Keys newKey)
        {
            _left = newKey;
        }
        public void SetRight(Keys newKey)
        {
            _right = newKey;
        }
        public void SetJump(Keys newKey)
        {
            _jump = newKey;
        }
        public void SetSprint(Keys newKey)
        {
            _sprint = newKey;
        }
        public float Opacity
        {
            get { return _opacity; }
        }
        public float MaxGrav
        {
            set { _maxGrav = value; }
        }
        public void Update(GameTime gameTime , List<Platform> platforms, KeyboardState currentState)
        {
            if (_opacity == 1)
            {
                var velocity = new Vector2();
                float speed = _speed;
                if ((currentState.IsKeyDown(_sprint) && !_isInAir)&& !AutoSprint)
                    speed = _sprintSpeed;
                if (_isWalking)
                {
                    if (AutoSprint)
                    {
                        if (_acceleration <= _sprintSpeed - 2)
                            _acceleration += _sprintSpeed/450f;
                        speed += _acceleration;
                    }
                    if (_timer > 200)
                    {
                        if (_position == 1)
                        {
                            if (_prevState == 2)
                            {
                                _position = 0;
                                _prevState++;
                            }
                            else
                            {
                                _prevState = 2;
                                _position = 2;
                            }
                        }
                        else
                            _position = 1;
                        _timer = 0;
                    }
                    else
                    {
                        _timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    }
                }
                else
                    _acceleration = 0;
                if (_isjump)
                {
                    _jumpTime = (float)gameTime.TotalGameTime.TotalSeconds - _jumpStartTime;
                }
                if (currentState.IsKeyDown(_jump) && _standingOnGround)
                {
                    _isjump = true; _standingOnGround = false;
                    _jumpStartTime = (float)gameTime.TotalGameTime.TotalSeconds;
                    _jumpTime = (float)gameTime.TotalGameTime.TotalSeconds - _jumpStartTime;
                    _gravity = 0;
                    velocity.Y = -3;
                }
                else if (_isjump)
                {
                    if (currentState.IsKeyUp(_jump))
                    {
                        _gravity = _maxGrav / 9;
                        velocity.Y = _gravity;
                        _isjump = false;
                    }
                    else if (_jumpTime < _maxJumpTime)
                    {
                        velocity.Y = -_jumpSpeed + _gravity;
                        if (_gravity < _maxGrav / 2)
                            _gravity += _maxGrav / 112;
                    }
                    else
                    {
                        _gravity = _maxGrav / 9;
                        velocity.Y = _gravity;
                        _isjump = false;
                    }
                }
                else if (!_standingOnGround)
                {
                    velocity.Y = _gravity;
                    if (_gravity <= _maxGrav)
                        _gravity += _maxGrav / 100;
                }

                foreach (Platform p in platforms)
                {
                    if (p.IsElevator)
                    {
                        if (p.Rectangle.Contains(Rectangle))
                        {
                            velocity.Y = -_elevatorSpeed;
                            _gravity = 1;
                        }
                    }
                }
                Rectangle newPosition = new((int)Position.X, (int)(Position.Y + velocity.Y - 1), Width, Height);
                if (velocity.Y < 0)
                {
                    for (int i = 0; i < platforms.Count; i++)
                    {
                        if (platforms[i].IsSolid())
                        {
                            if (platforms[i].DoesFade())
                            {
                                if (platforms[i].Intersects(newPosition))
                                {
                                    _isjump = false; velocity.Y = 0; _gravity = 1.3f;
                                    break;
                                }
                            }
                            else if (newPosition.Intersects(platforms[i].Rectangle))
                            {
                                _isjump = false; velocity.Y = 0; _gravity = 1.3f;
                                break;
                            }
                        }
                    }
                }
                else if (!_isjump)
                {
                    bool standing = false;
                    for (int i = 0; i < platforms.Count; i++)
                    {
                        if (platforms[i].IsSolid() && platforms[i].Intersects(newPosition))
                        {
                            standing = true;
                            velocity.Y = 0; _gravity = 1;
                            break;
                        }
                    }
                    _standingOnGround = standing;
                }


                if (currentState.IsKeyDown(_left))
                {
                    velocity.X = -speed;
                    _isLeft = true;
                }
                else if (currentState.IsKeyDown(_right))
                {
                    velocity.X = speed;
                    _isLeft = false;
                }
                newPosition = new Rectangle((int)(Position.X + velocity.X), (int)(Position.Y + velocity.Y - 1), Width, Height);
                for (int i = 0; i < platforms.Count; i++)
                {
                    if (platforms[i].IsSolid() && platforms[i].Intersects(newPosition))
                    {
                        if (platforms[i].OneWay)
                        {
                            if (!((velocity.X < 0 && platforms[i].WalkLeft) || (velocity.X > 0 && !platforms[i].WalkLeft)))
                            {
                                velocity.X = 0;
                            }
                        }
                        else
                        {
                            velocity.X = 0;
                            break;
                        }
                    }
                }
                _isInAir = velocity.Y != 0;
                _isWalking = velocity.X != 0 && !_isInAir;
                Position += velocity;
                Pet?.Update(Position, _isLeft, speed);
                Meter?.Update(gameTime, this);
            }
        }
        public void Reset()
        {
            _opacity = 1;
            _isWalking = false;
            _speed = 3;
            SecondLife = false;
            Meter = null;
        }
        public void BoostStats(Powerup powerup)
        {
            _maxJumpTime += powerup.JumpTimeIncrease;
            _jumpSpeed += powerup.JumpIncrease;
            _sprintSpeed += powerup.SprintIncrease;
            _speed += powerup.SpeedIncrease;
            _elevatorSpeed += powerup.ElevatorBoost;
            SecondLife = powerup.JSM;
            _maxGrav += powerup.GravityBoost;
        }
        public void FadingIn()
        {
            if (_opacity < 1)
            {
                _opacity += 0.01f;
            }
            else
            {
                _opacity = 1;
            }
        }
        public bool FadingOut()
        {
            if (_opacity > 0)
            {
                _opacity -= 0.01f;
                return true;
            }
            else
            {
                return false;
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Meter?.Draw(spriteBatch);
            Pet?.Draw(spriteBatch);
            if (_isInAir)
            {
                if (_isLeft)
                {
                    spriteBatch.Draw(_texture, Position, _spriteSheetPos[7], Color.White * _opacity);
                }
                else
                {
                    spriteBatch.Draw(_texture, Position, _spriteSheetPos[6], Color.White * _opacity);
                }
            }
            else if (_isLeft)
            {
                spriteBatch.Draw(_texture, Position, _spriteSheetPos[_position+3], Color.White * _opacity);

            }
            else
            {
                spriteBatch.Draw(_texture, Position, _spriteSheetPos[_position], Color.White * _opacity);
            }
        }
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            Meter?.Draw(spriteBatch);
            Pet?.Draw(spriteBatch);
            if (_isInAir)
            {
                if (_isLeft)
                {
                    spriteBatch.Draw(_texture, Position, _spriteSheetPos[7], color);
                }
                else
                {
                    spriteBatch.Draw(_texture, Position, _spriteSheetPos[6], color);
                }
            }
            else if (_isLeft)
            {
                spriteBatch.Draw(_texture, Position, _spriteSheetPos[_position + 3], color);

            }
            else
            {
                spriteBatch.Draw(_texture, Position, _spriteSheetPos[_position], color);
            }
        }
        public bool Touching(Rectangle rect)
        {
            return Rectangle.Intersects(rect);
        }
        public Rectangle Rectangle
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Width, Height); }
        }
        public int Width
        {
            get { return _spriteSheetPos[_position].Width; }
        }
        public int Height
        {
            get { return _spriteSheetPos[_position].Height; }
        }
        public void SetDifficulty(int difficulty)
        {
            switch (difficulty)
            {
                case 0:
                    this._speed = 4;
                    this._maxJumpTime = 2;
                    this._jumpSpeed = 8;
                    this._sprintSpeed = 9;
                    this._elevatorSpeed = 9;
                    break;
                case 1:
                    this._maxJumpTime = 0.8f;
                    this._jumpSpeed = 6;
                    this._sprintSpeed = 7;
                    this._elevatorSpeed = 6;
                    break;
                case 2:
                    this._maxJumpTime = 0.6f;
                    this._jumpSpeed = 6;
                    this._sprintSpeed = 6;
                    this._elevatorSpeed = 4;
                    break;
                case 3:
                    this._maxJumpTime = 0.5f;
                    this._jumpSpeed = 6;
                    this._sprintSpeed = 5;
                    this._elevatorSpeed = 3;
                    break;
            }
        }
        public void SetSkin(Texture2D newSkin)
        {
            _texture = newSkin;
        }
        public Pet Pet
        {
            get; set;
        }
        public bool SecondLife
        {
            get; set;
        }
    }
}
