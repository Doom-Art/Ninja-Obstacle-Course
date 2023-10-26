using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninja_Obstacle_Course
{
    internal class Level
    {
        private List<Portal> _portals;
        private List<Platform> _platforms;
        private List<Vector2> _signLocations;
        private List<String> _signText;
        private SpriteFont _signFont;

        public Level(List<Platform> platforms, List<Portal> portals)
        {
            this._platforms = platforms;
            this._portals = portals;
            _signLocations = new List<Vector2>();
            _signText = new List<String>();
        }
        public Level(List<Platform> platforms)
        {
            this._platforms = platforms;
            _signLocations = new List<Vector2>();
            _signText = new List<String>();
        }
        public void SetFont(SpriteFont signFont)
        {
            _signFont = signFont;
        }
        public void AddSign(Vector2 location, String signText)
        {
            _signLocations.Add(location);
            _signText.Add(signText);
        }

        public void Draw(SpriteBatch _spriteBatch, Player player)
        {
            if (_portals != null) {
                foreach (Portal portal in _portals)
                    portal.Draw(_spriteBatch);
            }
            for (int i = 0; i < _signLocations.Count; i++)
            {
                _spriteBatch.DrawString(_signFont, _signText[i], _signLocations[i], Color.Blue);
            }
            player.Draw(_spriteBatch);
            foreach (Platform p in _platforms)
            {
                p.Draw(_spriteBatch);
            }
        }
        public void Update(GameTime gameTime, Player player)
        {
            foreach (Platform p in _platforms)
                p.fade();
            player.Update(gameTime, _platforms);
            if (_portals != null)
            {
                foreach (Portal p in _portals)
                {
                    if (p.InPortal(player.Rectangle))
                    {
                        if (!player.FadingOut())
                            player.Position = p.PortalExit();
                    }
                    else if (p.OutPortal(player.Rectangle))
                        player.FadingIn();
                }
            }
        }
    }
}
