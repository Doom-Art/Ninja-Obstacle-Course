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
        private string _text, _secondaryText;
        private Texture2D _texture;
        private bool _displaySecondary, _visible;
        private Color _color;
        public Button(Texture2D texture, SpriteFont font, Rectangle location, string text)
        {
            this._font = font;
            this._location = location;
            this._text = text;
            this._texture = texture;
            this._displaySecondary = false;
            this._color = Color.PaleVioletRed;
            this._visible = true;
        }
        public Button(Texture2D texture, SpriteFont font, Rectangle location, string text, Color color)
        {
            this._font = font;
            this._location = location;
            this._text = text;
            this._texture = texture;
            this._displaySecondary = false;
            this._color = color;
            this._visible = true;
        }
        public Button(Texture2D texture, Rectangle location)
        {
            this._location = location;
            this._texture = texture;
            this._displaySecondary = false;
            this._color = Color.White;
            this._visible = true;

        }
        public Button(Texture2D texture, Rectangle location, Color color)
        {
            this._location = location;
            this._texture = texture;
            this._displaySecondary = false;
            this._color = color;
            this._visible = true;

        }
        public void Draw(SpriteBatch sb)
        {
            if (_visible)
            {
                sb.Draw(_texture, _location, _color);
                if (_text != null)
                {
                    if (!_displaySecondary)
                        sb.DrawString(_font, _text, new Vector2(_location.X + 5, _location.Y + 5), Color.Black);
                    else
                        sb.DrawString(_font, _secondaryText, new Vector2(_location.X + 5, _location.Y + 5), Color.Black);
                }
            }
        }
        public void AddSecondary(string secondaryText, bool startTrue)
        {
            _displaySecondary = startTrue;
            _secondaryText = secondaryText;
        }
        public void SwitchDisplay()
        {
            if (_secondaryText != null)
            {
                _displaySecondary = !_displaySecondary;
            }
        }
        public void SwitchDisplay(bool switchTo)
        {
            if (_secondaryText != null)
            {
                _displaySecondary = switchTo;
            }
        }
        public bool Clicked(MouseState ms)
        {
            if (_visible)
                return _location.Contains(ms.X, ms.Y);
            else 
                return false;
        }
        public SpriteFont SpriteFont { get { return _font; } }
        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }
    }
}
