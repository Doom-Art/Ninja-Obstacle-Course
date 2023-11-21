using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Ninja_Obstacle_Course
{
    public class Environment
    {
        private Texture2D _backgroundTex, _collectibleTex;
        private Rectangle _startPosition;
        private bool _hasCollectible;
        private CollectionMeter _meter;
        public Environment(Texture2D backgroundTex, Rectangle startLoc)
        {
            _backgroundTex = backgroundTex;
            _startPosition = startLoc;
            MaxGravity = 9.8f;
        }
        public Environment(Texture2D backgroundTex, Texture2D collectibleTex, Rectangle startPosition, bool hasCollectible, CollectionMeter collector)
        {
            _backgroundTex = backgroundTex;
            _collectibleTex = collectibleTex;
            _startPosition = startPosition;
            _hasCollectible = hasCollectible;
            _meter = collector;
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
