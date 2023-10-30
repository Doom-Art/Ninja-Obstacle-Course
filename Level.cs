using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
        public Level(List<Platform> platforms, List<RedWalker> redWalkers)
        {
            this._platforms = platforms;
            this._playerStartingPosition = new Vector2(280, -280);
            this._redWalkers = redWalkers;
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
        public void SetDifficulty(Player player, int difficulty)
        {
            player.SetDifficulty(difficulty);
            if (_redWalkers != null){
                foreach (RedWalker r in _redWalkers)
                    r.SetDifficulty(difficulty);
            }
        }
        public void Draw(SpriteBatch sprite, Player player)
        {
            if (_portals != null) {
                foreach (Portal portal in _portals)
                    portal.Draw(sprite);
            }
            for (int i = 0; i < _signLocations.Count; i++){
                sprite.DrawString(_signFont, _signText[i], _signLocations[i], Color.Blue);
            }
            if (_exitPortalTex != null){
                sprite.Draw(_exitPortalTex, _exitPortalRect, Color.White);
            }
            if (_redWalkers != null)
                for (int i = 0; i < _redWalkers.Count; i++)
                {
                    _redWalkers[i].Draw(sprite);
                }
            player.Draw(sprite);
            foreach (Platform p in _platforms){
                p.Draw(sprite);
            }
            if (_spikes != null)
                foreach (Platform p in _spikes)
                    p.Draw(sprite);
        }
        public void Draw(SpriteBatch sprite, Player player, Player player2)
        {
            if (_portals != null)
            {
                foreach (Portal portal in _portals)
                    portal.Draw(sprite);
            }
            for (int i = 0; i < _signLocations.Count; i++)
            {
                sprite.DrawString(_signFont, _signText[i], _signLocations[i], Color.Blue);
            }
            if (_exitPortalTex != null)
            {
                sprite.Draw(_exitPortalTex, _exitPortalRect, Color.White);
            }
            if (_redWalkers != null)
                for (int i = 0; i < _redWalkers.Count; i++)
                {
                    _redWalkers[i].Draw(sprite);
                }
            player2.Draw(sprite);
            player.Draw(sprite);
            foreach (Platform p in _platforms)
            {
                p.Draw(sprite);
            }
            if (_spikes != null)
                foreach (Platform p in _spikes)
                    p.Draw(sprite);
        }
        public void Update2(GameTime gameTime, Player player)
        {
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
                    {
                        player.FadingIn();
                        break;
                    }
                }
            }
        }
        public void Update(GameTime gameTime, Player player)
        {
            foreach (Platform p in _platforms)
                p.fade();
            player.Update(gameTime, _platforms);
            if (_redWalkers != null)
                for (int i = 0; i < _redWalkers.Count; i++)
                {
                    _redWalkers[i].Update(gameTime);
                }
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
                    {
                        player.FadingIn();
                        break;
                    }
                }
            }
        }
        public bool DidPlayerDie(Player player)
        {
            bool death = false;
            if (_redWalkers != null)
            {
                foreach (RedWalker r in _redWalkers)
                {
                    if (player.Touching(r.Position))
                    {
                        death = true;
                        break;
                    }
                }
            }
            if (_spikes != null)
            {
                if (!death)
                    foreach(Platform p in _spikes)
                        if (player.Touching(p.Rectangle))
                        {
                            death = true;
                            break;
                        }
            }
            return death;
        }
        public void SetDefaults(Player player, int difficulty)
        {
            player.Position = _playerStartingPosition;
            player.Reset();
            SetDifficulty(player, difficulty);
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
