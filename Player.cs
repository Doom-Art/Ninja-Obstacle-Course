using Microsoft.Xna.Framework;
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
    public class Player :Sprite
    {
        private bool _isjump;
        private float _jumpStartTime, _jumpTime, _jumpSpeed, _gravity, _maxJumpTime;
        private bool _standingOnGround, _isWalking, _isLeft, _isInAir;
        private Rectangle[] _spriteSheetPos;
        private int _position;
        private float timer;
        private int prevState;
        private float _opacity;
        private Keys _left, _right, _jump, _sprint;

        public Player(Texture2D texture, Rectangle[] spriteSheet) : base(texture)
        {
            _isjump = false;
            _jumpSpeed = 6;
            _maxJumpTime = 0.6f;
            _spriteSheetPos = spriteSheet;
            prevState = 2;
            _position = 1;
            _gravity = 1;
            _isLeft = false;
            _isWalking = false;
            _opacity = 1;
            _left = Keys.A;
            _right = Keys.D;
            _jump = Keys.Space;
            _sprint = Keys.LeftShift;
        }
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
        public void Update(GameTime gameTime , List<Platform> platforms)
        {
            if (_opacity == 1)
            {
                KeyboardState currentState = Keyboard.GetState();
                var velocity = new Vector2();
                var speed = 3f;
                if (currentState.IsKeyDown(_sprint))
                    speed = 6f;
                if (_isWalking)
                {
                    if (timer > 250 && _standingOnGround)
                    {
                        if (_position == 1)
                        {
                            if (prevState == 2)
                            {
                                _position = 0;
                                prevState++;
                            }
                            else
                            {
                                prevState = 2;
                                _position = 2;
                            }
                        }
                        else
                            _position = 1;
                        timer = 0;
                    }
                    else
                    {
                        timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    }
                }
                if (_isjump)
                {
                    _jumpTime = (float)gameTime.TotalGameTime.TotalSeconds - _jumpStartTime;
                }
                if (currentState.IsKeyDown(_jump)&& _standingOnGround)
                {
                    _isjump = true; _standingOnGround = false;
                    _jumpStartTime = (float)gameTime.TotalGameTime.TotalSeconds;
                    _jumpTime = (float)gameTime.TotalGameTime.TotalSeconds - _jumpStartTime;
                    _gravity = 0;
                    velocity.Y = -3;
                }
                else if (currentState.IsKeyUp(_jump) && _isjump)
                {
                    velocity.Y = 1f;
                    _gravity = 1;
                    _isjump = false;
                }
                else if (_jumpTime < _maxJumpTime && _isjump)
                {
                    velocity.Y = -_jumpSpeed + _gravity;
                    if (_gravity < 4)
                        _gravity += 0.09f;
                }
                else if (_isjump)
                {
                    velocity.Y = 0.1f;
                    _gravity = 1;
                    _isjump = false;
                }
                else if (!_standingOnGround)
                {
                    velocity.Y = _gravity;
                    if (_gravity <= 9.8)
                        _gravity += 0.08f;
                }

                Rectangle newPosition = new Rectangle((int)Position.X, (int)(Position.Y + velocity.Y - 1), Width, Height);
                if (!_isjump)
                {
                    bool standing = false;
                    for (int i = 0; i < platforms.Count; i++)
                    {
                        if (platforms[i].Intersects(newPosition))
                        {
                            standing = true;
                            velocity.Y = 0; _gravity = 1;
                            break;
                        }
                    }
                    _standingOnGround = standing;
                }
                else
                {
                    for (int i = 0; i < platforms.Count; i++)
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
                    if (platforms[i].Intersects(newPosition))
                    {
                        velocity.X = 0;
                        break;
                    }
                }
                _isWalking = velocity.X != 0;
                _isInAir = velocity.Y != 0;
                Position += velocity;
            }
            
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
        public override void Draw(SpriteBatch spriteBatch)
        {
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
            else if(!_isLeft)
            {
                spriteBatch.Draw(_texture, Position, _spriteSheetPos[_position], Color.White * _opacity);
            }

        }
        public bool Touching(Rectangle rect)
        {
            return Rectangle.Intersects(rect);
        }
        public override Rectangle Rectangle
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
    }
}
