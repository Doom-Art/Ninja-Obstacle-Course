using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninja_Obstacle_Course
{
    public class Ghost
    {
        private Texture2D _texture;
        private Rectangle[] _sourceRects;
        private Rectangle _locRect, _originalLoc;
        private float _opacity;
        private int _speed;
        private bool _left, _increasing;
        public Ghost(Texture2D texture, Rectangle location)
        {
            _texture = texture;
            _sourceRects = new Rectangle[4]
            {
                new Rectangle(0,0,texture.Width/2,texture.Height/2),
                new Rectangle(texture.Width/2,0,texture.Width/2,texture.Height/2),
                new Rectangle(0,texture.Height/2,texture.Width/2,texture.Height/2),
                new Rectangle(texture.Width/2,texture.Height/2,texture.Width/2,texture.Height / 2)
            };
            _locRect = location;
            _originalLoc = location;
            _opacity = 1;
            _speed = 3;
        }
        public Rectangle Rectangle
        {
            get { return _locRect; }
        }
        public bool Hidden
        {
            get; set;
        }
        public void SetDifficulty(int difficulty)
        {
            _speed = difficulty;
        }
        public void Update(Player player)
        {
            float fadeAmount = 0.002f;
            Vector2 movement = new();
            if (_increasing)
            {
                if (_opacity < 1)
                    _opacity += fadeAmount;
                else
                    _increasing = false;
            }
            else
            {
                if (_opacity > 0)
                {
                    _opacity -= fadeAmount;
                }
                else
                {
                    _increasing = true;
                }
            }
            if (_opacity >= 0.6f)
            {
                if (player.Rectangle.X > _locRect.X)
                {
                    movement.X = _speed;
                    _left = false;
                }
                else
                {
                    movement.X = -_speed;
                    _left = true;
                }
                if (player.Rectangle.Y > _locRect.Y)
                    movement.Y = _speed-1;
                else 
                    movement.Y = -(_speed-1);
            }
            _locRect.X += (int)movement.X;
            _locRect.Y += (int)movement.Y;
        }
        public void Update(Player player, Player player2)
        {
            float fadeAmount = 0.005f;
            Vector2 movement = new();
            if (_increasing)
            {
                if (_opacity < 1)
                    _opacity += fadeAmount;
                else
                    _increasing = false;
            }
            else
            {
                if (_opacity > 0)
                {
                    _opacity -= fadeAmount;
                }
                else
                    _increasing = true;
                
            }
            if (_opacity >= 0.6f)
            {
                int distance1 = Math.Abs(player.Rectangle.X - _locRect.X);
                int distance2 = Math.Abs(player2.Rectangle.X - _locRect.X);
                if (distance1 < distance2)
                {
                    if (player.Rectangle.X > _locRect.X)
                        movement.X = _speed;
                    else
                        movement.X = -_speed;
                    if (player.Rectangle.Y > _locRect.Y)
                        movement.Y = _speed - 1;
                    else
                        movement.Y = -(_speed - 1);
                }
                else
                {
                    if (player2.Rectangle.X > _locRect.X)
                        movement.X = _speed;
                    else
                        movement.X = -_speed;
                    if (player2.Rectangle.Y > _locRect.Y)
                        movement.Y = _speed - 1;
                    else
                        movement.Y = -(_speed - 1);
                }                
            }
            _locRect.X += (int)movement.X;
            _locRect.Y += (int)movement.Y;
        }
        public void Draw(SpriteBatch sprite)
        {
            if (!Hidden)
            {
                if (_opacity >= 0.6f){
                    if (_left)
                        sprite.Draw(_texture, _locRect, _sourceRects[0], Color.White * _opacity);
                    else
                        sprite.Draw(_texture, _locRect, _sourceRects[1], Color.White * _opacity);
                }
                else{
                    if (_left)
                        sprite.Draw(_texture, _locRect, _sourceRects[2], Color.White * (_opacity + 0.1f));
                    else
                        sprite.Draw(_texture, _locRect, _sourceRects[3], Color.White * (_opacity + 0.1f));
                }
            }
        }
        public void Reset()
        {
            _locRect = _originalLoc;
            _opacity = 1;
            Hidden = false;
        }
    }
}
