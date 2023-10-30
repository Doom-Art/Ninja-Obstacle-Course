using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninja_Obstacle_Course
{
    public class Camera
    {
        public Matrix Transform { get; private set; }

        public void Follow(Player target)
        {
            var position = Matrix.CreateTranslation(
              -target.Position.X - (target.Width / 2),
              -target.Position.Y - (target.Height / 2),
              0);

            var offset = Matrix.CreateTranslation(
                600 / 2,
                700 / 2,
                0);

            Transform = position * offset;
        }
    }
}
