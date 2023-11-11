using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninja_Obstacle_Course
{
    public class Portal
    {
        private Texture2D _portalTex;
        private Rectangle _portalOneRect, _portalTwoRect;
        private Rectangle _portalOneSourceRect, _portalTwoSourceRect;
        private Color _color, _color2;
        public Portal(Texture2D portalTex, Rectangle portalOneRect, Rectangle portalTwoRect)
        {
            this._portalTex = portalTex;
            this._portalOneRect = portalOneRect;
            this._portalTwoRect = portalTwoRect;
            _portalOneSourceRect = new Rectangle(0, 0, portalTex.Width / 2, portalTex.Height);
            _portalTwoSourceRect = new Rectangle(portalTex.Width / 2, 0, portalTex.Width / 2, portalTex.Height);
            _color = Color.White;
            _color2 = Color.White;
        }
        public Portal(Texture2D portalTex, Rectangle portalOneRect, Rectangle portalTwoRect, Color color, Color color2)
        {
            this._portalTex = portalTex;
            this._portalOneRect = portalOneRect;
            this._portalTwoRect = portalTwoRect;
            _portalOneSourceRect = new Rectangle(0, 0, portalTex.Width / 2, portalTex.Height);
            _portalTwoSourceRect = new Rectangle(portalTex.Width / 2, 0, portalTex.Width / 2, portalTex.Height);
            _color = color;
            _color2 = color2;
        }
        public void Draw(SpriteBatch _spritebatch)
        {
            _spritebatch.Draw(_portalTex, _portalOneRect, _portalOneSourceRect, _color);
            _spritebatch.Draw(_portalTex, _portalTwoRect, _portalTwoSourceRect, _color2);
        }
        public Vector2 PortalExit()
        {
            return new Vector2(_portalTwoRect.Left, _portalTwoRect.Top);
        }
        public bool InPortal(Rectangle position)
        {
            return _portalOneRect.Contains(position);
        }
        public bool OutPortal(Rectangle position)
        {
            return _portalTwoRect.Contains(position);
        }
    }
}
