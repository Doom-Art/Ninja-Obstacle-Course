using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Ninja_Obstacle_Course
{
    public class Environment
    {
        private Texture2D _backgroundTex, _collectibleTex;
        private Rectangle _startPosition;
        public Environment(Texture2D backgroundTex, Rectangle startLoc)
        {
            _backgroundTex = backgroundTex;
            _startPosition = startLoc;
            MaxGravity = 9.8f;
        }
        public Environment(Texture2D backgroundTex, Texture2D collectibleTex, Rectangle startPosition, CollectionMeter meter)
        {
            _backgroundTex = backgroundTex;
            _collectibleTex = collectibleTex;
            _startPosition = startPosition;
            HasCollectible = true;
            Meter = meter;
            MaxGravity = 9.8f;
        }
        public CollectionMeter Meter
        {
            get; private set;
        }
        public bool HasCollectible
        {
            get; private set;
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
