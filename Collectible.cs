using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninja_Obstacle_Course
{
    public struct Collectible
    {
        bool _visible;
        public Collectible(Rectangle rect)
        {
            Rectangle = rect;
            _visible = true;
        }
        public bool Collected(Player player)
        {
            return _visible && player.Touching(Rectangle);
        }
        public void Reset()
        {
            _visible = true;
        }
        public Rectangle Rectangle
        {
            get; set;
        }
        public bool Visible
        {
            get { return _visible; }
            set {  _visible = value; }
        }
        public readonly void Draw(Texture2D tex, SpriteBatch sprite)
        {
            if (_visible)
            {
                sprite.Draw(tex, Rectangle, Color.White);
            }
        }
    }
}
