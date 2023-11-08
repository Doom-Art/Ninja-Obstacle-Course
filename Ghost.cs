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
        private Rectangle _locRect;
        private float _opacity;
        private bool _left;
        public Ghost(Texture2D texture, Rectangle[] sourceRects, Rectangle location)
        {
            _texture = texture;
            _sourceRects = sourceRects;
            _locRect = location;
            _opacity = 1;
        }
    }
}
