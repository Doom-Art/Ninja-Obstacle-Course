using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninja_Obstacle_Course
{
    public class Coin
    {
        Texture2D _coinTex;
        Rectangle _coinRect;
        public Coin(Texture2D coinTex, Rectangle coinRect)
        {
            _coinTex = coinTex;
            _coinRect = coinRect;
        }
        public Rectangle CoinRect { get { return _coinRect; } }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_coinTex, _coinRect, Color.White);
        }
    }
}
