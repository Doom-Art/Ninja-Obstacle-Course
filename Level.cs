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
        private List<Platform> _platforms, _spikes;
        private List<RedWalker> _redWalkers;
        private List<Vector2> _signLocations;
        private List<String> _signText;
        private SpriteFont _signFont;
        private Texture2D _exitPortalTex;
        private Rectangle _exitPortalRect;
        private Vector2 _playerStartingPosition;

        public Level(List<Platform> platforms, List<Portal> portals)
        {
            this._platforms = platforms;
            this._portals = portals;
            this._playerStartingPosition = new Vector2(280, -280);
            _signLocations = new List<Vector2>();
            _signText = new List<String>();
        }
        public Level(List<Platform> platforms, List<Portal> portals, List<RedWalker> redWalkers, List<Platform> spikes)
        {
            this._platforms = platforms;
            this._portals = portals;
            this._playerStartingPosition = new Vector2(280, -280);
            this._redWalkers = redWalkers;
            this._spikes = spikes;
            _signLocations = new List<Vector2>();
            _signText = new List<String>();
        }
        public Level(List<Platform> platforms, List<Portal> portals, Vector2 startingPosition)
        {
            this._platforms = platforms;
            this._portals = portals;
            this._playerStartingPosition = startingPosition;
            this._signLocations = new List<Vector2>();
            this._signText = new List<String>();
        }
        public void SetFont(SpriteFont signFont)
        {
            _signFont = signFont;
        }
        public void SetExit(Texture2D exitTex, Rectangle exitRect)
        {
            _exitPortalRect = exitRect;
            _exitPortalTex = exitTex;
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
            for (int i = 0; i < _signLocations.Count; i++){
                _spriteBatch.DrawString(_signFont, _signText[i], _signLocations[i], Color.Blue);
            }
            if (_exitPortalTex != null){
                _spriteBatch.Draw(_exitPortalTex, _exitPortalRect, Color.White);
            }
            player.Draw(_spriteBatch);
            foreach (Platform p in _platforms){
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
        public void SetDefaults(Player player)
        {
            player.Position = _playerStartingPosition;
            player.Reset();
        }
        public bool PlayerCompleteLevel(Player player)
        {
            return PlayerCompleteLevel(player.Rectangle);
        }
        public bool PlayerCompleteLevel(Rectangle rect)
        {
            if (_exitPortalTex == null)
            {
                return false;
            }
            else if (_exitPortalRect.Contains(rect))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
