using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Ninja_Obstacle_Course
{
    public class Environment
    {
        private readonly Texture2D _backgroundTex;
        private Rectangle _startPosition;
        public Environment(Texture2D backgroundTex)
        {
            _backgroundTex = backgroundTex;
            _startPosition = new(-200, -3200, 4400, 4000);
            MaxGravity = 9.8f;
        }
        public Environment(Texture2D backgroundTex, Texture2D collectibleTex, CollectionMeter meter)
        {
            _backgroundTex = backgroundTex;
            CollectibleTex = collectibleTex;
            _startPosition = new(-200, -3200, 4400, 4000);
            HasCollectible = true;
            Meter = meter;
            MaxGravity = 9.8f;
        }
        public Texture2D CollectibleTex
        {
            get; private set;
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
