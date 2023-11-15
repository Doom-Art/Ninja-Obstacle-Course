

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Ninja_Obstacle_Course
{
    public class MageSpell
    {
        private Texture2D _texture;
        private Vector2 _position;
        public Vector2 Center { get; set; }
        public float Radius { get; set; }

        public MageSpell(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            _position = position;
            Center = new Vector2(texture.Width/2, texture.Height/2);
            Radius = texture.Width/2;
        }
        public bool Intersects(Rectangle rect)
        {
            bool collide = false;
            if (Math.Abs(rect.Center.X - Center.X) < Radius+rect.Width/2)
                collide = true;
            else if (Math.Abs(rect.Center.Y - Center.Y) < Radius + rect.Height/ 2)
                collide = true;

            return collide;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}
