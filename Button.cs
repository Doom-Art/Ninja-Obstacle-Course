using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninja_Obstacle_Course
{
    internal class Button
    {
        private SpriteFont _font;
        private Rectangle _location;
        private string _text;
        private Texture2D _texture;
        public Button(Texture2D texture, SpriteFont font, Rectangle location, string text)
        {
            this._font = font;
            this._location = location;
            this._text = text;
            this._texture = texture;
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(_texture, _location, Color.PaleVioletRed);
            sb.DrawString(_font, _text, new Vector2(_location.X+5,_location.Y+5), Color.Black);
        }
        public bool Clicked(MouseState ms)
        {
            return _location.Contains(ms.X, ms.Y);
        }
        public SpriteFont SpriteFont { get { return _font; } }
    }
}
