using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Ninja_Obstacle_Course
{
    public class Environment
    {
        private Texture2D _backgroundTex;
        private Rectangle _startPosition;
        public Environment(Texture2D backgroundTex, Rectangle startLoc)
        {
            _backgroundTex = backgroundTex;
            _startPosition = startLoc;
            MaxGravity = 9.8f;
        }
        public float MaxGravity
        {
            get; set;
        }
        public void Draw(SpriteBatch sprite)
        {
            if (_backgroundTex != null)
            {
                sprite.Draw(_backgroundTex, _startPosition, Color.White);
            }
        }
    }
}
