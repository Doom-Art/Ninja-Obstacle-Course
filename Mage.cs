

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Ninja_Obstacle_Course
{
    public class Mage
    {
        private Texture2D _mageTex, _spellTex;
        private Rectangle _locRect;
        private int _difficulty;
        private MageSpell _spell;
        private float _timer, _timeLimit, _maxSpeed;
        public Mage(Texture2D mageTex, Texture2D spellTex, Rectangle locRect)
        {
            _mageTex = mageTex;
            _spellTex = spellTex;
            _locRect = locRect;
            _timer = 100;
        }
        public void SetDifficulty(int difficulty)
        {
            _difficulty = difficulty;
            _timeLimit = 130 - (_difficulty * 30);
            _maxSpeed = 2 + (3 * difficulty);
            Hidden = false;
            _spell = null;
            LimiterRemoval = false;
        }
        public bool LimiterRemoval
        {
            private get; set;
        }
        public bool Hidden
        {
            get; set;
        }
        public void Update(List<Platform> platforms, Player player)
        {
            if (_spell == null)
            {
                if (_timer >= _timeLimit)
                {
                    var spellLoc = new Vector2(_locRect.X + 5, _locRect.Y + 20);
                    float distanceX = player.Rectangle.X - spellLoc.X;
                    float distanceY = player.Rectangle.Y - spellLoc.Y;
                    float xSpeed = distanceX / (120 - _difficulty*20);
                    float ySpeed = distanceY / (120 - _difficulty*20);
                    if ((Math.Abs(xSpeed) < _maxSpeed && Math.Abs(ySpeed) < _maxSpeed) || LimiterRemoval)
                    {
                        _spell = new MageSpell(_spellTex, spellLoc)
                        {
                            Speed = new Vector2(xSpeed, ySpeed)
                        };
                    }
                    _timer = 0;
                }
                else
                {
                    _timer += 1;
                }
            }
            else
            {
                _spell.Update();
                foreach (Platform platform in platforms)
                {
                    if (platform.Intersects(_spell.HitBox) && platform.IsSolid())
                    {
                        _spell = null;
                        break;
                    }
                }
            }
        }
        public void Update(List<Platform> platforms, Player player, Player player2)
        {
            if (_spell == null)
            {
                if (_timer >= _timeLimit)
                {
                    var spellLoc = new Vector2(_locRect.X + 5, _locRect.Y + 20);
                    float distanceX = player.Rectangle.X - spellLoc.X;
                    float distanceY = player.Rectangle.Y - spellLoc.Y; 
                    float distanceX2 = player2.Rectangle.X - spellLoc.X;
                    float distanceY2 = player2.Rectangle.Y - spellLoc.Y;
                    float xSpeed, ySpeed;
                    if (Math.Abs(distanceX) + Math.Abs(distanceY) < Math.Abs(distanceX2) + Math.Abs(distanceY2))
                    {
                        xSpeed = distanceX / (120 - _difficulty * 20);
                        ySpeed = distanceY / (120 - _difficulty * 20);
                    }
                    else
                    {
                        xSpeed = distanceX2 / (120 - _difficulty * 20);
                        ySpeed = distanceY2 / (120 - _difficulty * 20);
                    }
                    if (Math.Abs(xSpeed) < _maxSpeed && Math.Abs(ySpeed) < _maxSpeed)
                    {
                        _spell = new MageSpell(_spellTex, spellLoc)
                        {
                            Speed = new Vector2(xSpeed, ySpeed)
                        };
                    }
                    _timer = 0;
                }
                else
                {
                    _timer += 1;
                }
            }
            else
            {
                _spell.Update();
                foreach (Platform platform in platforms)
                {
                    if (platform.Intersects(_spell.HitBox) && platform.IsSolid())
                    {
                        _spell = null;
                        break;
                    }
                }
            }
        }
        public bool DidHit(Player player)
        {
            if (_spell != null && !Hidden)
                return player.Touching(_spell.HitBox);
            else
                return false;
        }
        public void Draw(SpriteBatch sprite)
        {
            if (!Hidden)
            {
                sprite.Draw(_mageTex, _locRect, Color.White);
                _spell?.Draw(sprite);
            }
        }
    }
}
