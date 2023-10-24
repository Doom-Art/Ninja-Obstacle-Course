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
        private Texture2D portalTex;
        private Rectangle portalOneRect, portalTwoRect;
        private Rectangle portalOneSourceRect, portalTwoSourceRect;
        public Portal(Texture2D portalTex, Rectangle portalOneRect, Rectangle portalTwoRect)
        {
            this.portalTex = portalTex;
            this.portalOneRect = portalOneRect;
            this.portalTwoRect = portalTwoRect;
            portalOneSourceRect = new Rectangle(0, 0, portalTex.Width / 2, portalTex.Height);
            portalTwoSourceRect = new Rectangle(portalTex.Width / 2, 0, portalTex.Width / 2, portalTex.Height);
        }
        public Portal(Texture2D portalTex, Rectangle portalOneRect, Rectangle portalTwoRect, Rectangle portalOneSourceRect, Rectangle portalTwoSourceRect)
        {
            this.portalTex = portalTex;
            this.portalOneRect = portalOneRect;
            this.portalTwoRect = portalTwoRect;
            this.portalOneSourceRect = portalOneSourceRect;
            this.portalTwoSourceRect = portalTwoSourceRect;
        }
        public void Draw(SpriteBatch _spritebatch)
        {
            _spritebatch.Draw(portalTex, portalOneRect, portalOneSourceRect, Color.White);
            _spritebatch.Draw(portalTex, portalTwoRect, portalTwoSourceRect, Color.White);
        }
        public Vector2 PortalExit()
        {
            return new Vector2(portalTwoRect.Left, portalTwoRect.Top);
        }
        public bool InPortal(Rectangle position)
        {
            return portalOneRect.Contains(position);
        }
        public bool OutPortal(Rectangle position)
        {
            return portalTwoRect.Contains(position);
        }
    }
}
