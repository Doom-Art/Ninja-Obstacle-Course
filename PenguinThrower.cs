using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ninja_Obstacle_Course
{
    public class PenguinThrower
    {
        private Texture2D _penguinTex, _iceTex;
        private Rectangle _animation1, _animation2;
        private Vector2 _penguinLoc;
        private int _difficulty;
        private float _timer, _timeLimit, _gravity;
        private Ice _thrownIce;

        public PenguinThrower(Texture2D penguinTex, Texture2D iceTex, Vector2 penguinLoc)
        {
            this._penguinTex = penguinTex;
            this._iceTex = iceTex;
            this._penguinLoc = penguinLoc;
            _animation1 = new Rectangle(0, 0, _penguinTex.Width / 2, _penguinTex.Height);
            _animation2 = new Rectangle(_penguinTex.Width / 2, 0, _penguinTex.Width / 2, _penguinTex.Height);
        }
        public void SetDifficulty(int difficulty, float gravity)
        {
            _difficulty = difficulty;
            _timer = 0;
            _timeLimit = 160 - (_difficulty * 40);
            Hidden = false;
            _thrownIce = null;
            _gravity = gravity;
        }
        public void Draw(SpriteBatch sprite)
        {
            if (_thrownIce != null)
            {
                sprite.Draw(_penguinTex, _penguinLoc, _animation2, Color.White);
                _thrownIce.Draw(sprite);
            }
            else
            {
                sprite.Draw(_penguinTex, _penguinLoc, _animation1, Color.White);
            }
            
        }
        public void Update(List<Platform> platforms, Player player)
        {
            if (_thrownIce == null)
            {
                if (_timer > _timeLimit)
                {
                    _thrownIce = new Ice(_iceTex, new Vector2(_penguinLoc.X - 20, _penguinLoc.Y + 10), player.Position.X, _gravity, _difficulty);
                    _timer = 0;
                }
                else
                    _timer++;
            }
            else
            {
                _thrownIce.Update();
                foreach (Platform platform in platforms)
                {
                    if (platform.Intersects(_thrownIce.HitBox))
                    {
                        _thrownIce = null; break;
                    }
                }
            }
        }
        public void Update(List<Platform> platforms, Player player, Player player2)
        {
            if (_thrownIce == null)
            {
                if (_timer > _timeLimit)
                {
                    if (Math.Abs(player.Position.X - _penguinLoc.X) < Math.Abs(player2.Position.X - _penguinLoc.X))
                        _thrownIce = new Ice(_iceTex, new Vector2(_penguinLoc.X - 20, _penguinLoc.Y + 10), player.Position.X, _gravity, _difficulty);
                    else
                        _thrownIce = new Ice(_iceTex, new Vector2(_penguinLoc.X - 20, _penguinLoc.Y + 10), player2.Position.X, _gravity, _difficulty);
                    _timer = 0;
                }
                else
                    _timer++;
            }
            else
            {
                _thrownIce.Update();
                foreach (Platform platform in platforms)
                {
                    if (platform.Intersects(_thrownIce.HitBox))
                    {
                        _thrownIce = null; break;
                    }
                }
            }
        }
        public bool DidHit(Player player)
        {
            if (_thrownIce != null && !Hidden)
                return player.Touching(_thrownIce.HitBox);
            else
                return false;
        }
        public bool Hidden { get; set; }
    }
}
