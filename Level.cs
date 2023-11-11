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
        private List<Ghost> _ghosts;
        private List<Vector2> _signLocations;
        private List<String> _signText;
        private SpriteFont _signFont;
        private Texture2D _exitPortalTex, _coinTex;
        private Rectangle _exitPortalRect;
        private Vector2 _playerStartingPosition;
        private bool _hasToken;
        private List<Coin> _coins;
        private int _currentCoins, _totalCoins;

        public Level(List<Platform> platforms, List<Portal> portals)
        {
            this._platforms = platforms;
            this._portals = portals;
            this._playerStartingPosition = new Vector2(280, -280);
            this._spikes = new();
            _signLocations = new List<Vector2>();
            _signText = new List<String>();
            _coins = new();
            _currentCoins = 0;
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
            _coins = new();
            _currentCoins = 0;
        }
        public Level(List<Platform> platforms, List<RedWalker> redWalkers)
        {
            this._platforms = platforms;
            this._playerStartingPosition = new Vector2(280, -280);
            this._redWalkers = redWalkers;
            this._spikes = new();
            _signLocations = new List<Vector2>();
            _signText = new List<String>();
            _coins = new();
            _currentCoins = 0;
        }
        public Level(List<Platform> platforms, List<Portal> portals, Vector2 startingPosition)
        {
            this._platforms = platforms;
            this._portals = portals;
            this._playerStartingPosition = startingPosition;
            this._spikes = new();
            this._signLocations = new List<Vector2>();
            this._signText = new List<String>();
            _coins = new();
            _currentCoins = 0;
        }
        public void SetFont(SpriteFont signFont)
        {
            _signFont = signFont;
        }
        public void AddGhost(Ghost ghost)
        {
            _ghosts ??= new();
            _ghosts.Add(ghost);
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
            foreach (Platform p in _platforms)
                if (p.DoesFade())
                    p.SetFadeDifficulty(difficulty);
            foreach (Platform p in _spikes)
                p.SetSpikeSize(difficulty);
            if (_ghosts != null)
                foreach (Ghost ghost in _ghosts)
                    ghost.SetDifficulty(difficulty);
        }
        public void DrawDeath(SpriteBatch sprite, Player player)
        {
            foreach (Coin c in _coins) { c.Draw(sprite); }
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
            player.Draw(sprite, Color.Black);
            foreach (Platform p in _platforms)
            {
                p.Draw(sprite);
            }
            if (_spikes != null)
                foreach (Platform p in _spikes)
                    p.Draw(sprite);
            if (_ghosts != null)
                for (int i = 0; i < _ghosts.Count; i++)
                {
                    _ghosts[i].Draw(sprite);
                }
        }
        public void Draw(SpriteBatch sprite, Player player)
        {
            foreach (Coin c in _coins) { c.Draw(sprite); }
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
            if (_ghosts != null)
                for (int i = 0; i < _ghosts.Count; i++)
                {
                    _ghosts[i].Draw(sprite);
                }
        }
        public void Draw(SpriteBatch sprite, Player player, Skin skin)
        {
            foreach (Coin c in _coins) { c.Draw(sprite); }
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
            player.Draw(sprite);
            foreach (Platform p in _platforms)
            {
                p.Draw(sprite);
            }
            if (_spikes != null)
                foreach (Platform p in _spikes)
                    p.Draw(sprite);
            if (!_hasToken)
                skin.DrawIcon(sprite);
            if (_ghosts != null)
                for (int i = 0; i < _ghosts.Count; i++)
                {
                    _ghosts[i].Draw(sprite);
                }
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
            if (_ghosts != null)
                for (int i = 0; i < _ghosts.Count; i++)
                {
                    _ghosts[i].Draw(sprite);
                }
        }
        public void Update(GameTime gameTime, Player player)
        {
            foreach (Platform p in _platforms)
                p.Update();
            player.Update(gameTime, _platforms);
            if (_redWalkers != null)
                for (int i = 0; i < _redWalkers.Count; i++)
                {
                    _redWalkers[i].Update(gameTime);
                }
            if (_ghosts != null && player.Opacity == 1)
            {
                foreach (Ghost g in _ghosts)
                    g.Update(player);
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
            for (int i = 0; i < _coins.Count; i++)
            {
                if (player.Touching(_coins[i].CoinRect))
                {
                    _currentCoins++;
                    _coins.RemoveAt(i);
                    break;
                }
            }
        }
        public void Update(GameTime gameTime, Player player, Player player2)
        {
            foreach (Platform p in _platforms)
                p.Update();
            player.Update(gameTime, _platforms);
            player2.Update(gameTime, _platforms);
            if (_redWalkers != null)
                for (int i = 0; i < _redWalkers.Count; i++)
                {
                    _redWalkers[i].Update(gameTime);
                }
            if (_ghosts != null && player.Opacity == 1)
            {
                foreach (Ghost g in _ghosts)
                    g.Update(player, player2);
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
                    if (p.InPortal(player2.Rectangle))
                    {
                        if (!player2.FadingOut())
                            player2.Position = p.PortalExit();
                    }
                    else if (p.OutPortal(player2.Rectangle))
                    {
                        player2.FadingIn();
                        break;
                    }
                }
            }
        }

        public void Update(GameTime gameTime, Player player, Skin skin)
        {
            foreach (Platform p in _platforms)
                p.Update();
            player.Update(gameTime, _platforms);
            if (player.Touching(skin.IconLocation))
                _hasToken = true;
            if (_redWalkers != null)
                for (int i = 0; i < _redWalkers.Count; i++)
                {
                    _redWalkers[i].Update(gameTime);
                }
            if (_ghosts != null && player.Opacity == 1)
            {
                foreach (Ghost g in _ghosts)
                    g.Update(player);
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
            for (int i = 0; i < _coins.Count; i++)
            {
                if (player.Touching(_coins[i].CoinRect))
                {
                    _currentCoins++;
                    _coins.RemoveAt(i);
                    break;
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
            if (_ghosts != null && !death)
            {
                foreach (Ghost g in _ghosts)
                {
                    if (player.Touching(g.Rectangle))
                    {
                        death = true;
                        break;
                    }
                }
            }
            if (death)
                _currentCoins = 0;
            return death;
        }
        public void SetSpawn(Vector2 newSpawn)
        {
            _playerStartingPosition = newSpawn;
        }
        public void SetCoinDefaults(Texture2D coinTex)
        {
            _coinTex = coinTex;
        }
        public void ResetCoins()
        {
            _currentCoins = 0;
            _coins.Clear();
            _totalCoins = 0;
            for (int j = 4; j<_platforms.Count; j++)
            {
                if (_platforms[j].Opacity != 0.5f && _platforms[j].Rectangle.Width > 40 && _platforms[j].IsSolid())
                    for (int i = 0; i < _platforms[j].Rectangle.Width; i += 50)
                    {
                        bool touch = false;
                        Rectangle coinR = new Rectangle(_platforms[j].Rectangle.X + i, _platforms[j].Rectangle.Y - 40, 30, 30);
                        foreach (Platform p in _platforms)
                            if (p.Rectangle.Intersects(coinR))
                            {
                                touch = true;
                                break;
                            }
                        if (!touch && _spikes !=null)
                            foreach (Platform p in _spikes)
                                if (p.Rectangle.Intersects(coinR))
                                {
                                    touch = true;
                                    break;
                                }
                        if (!touch)
                        {
                            _coins.Add(new Coin(_coinTex, coinR));
                            _totalCoins++;
                        }
                        
                    }
            }
        }
        public int SetDefaults(Player player, int difficulty, List<Skin> skins, int currentLevel)
        {
            int skinInLevel = 0;
            player.Position = _playerStartingPosition;
            player.Reset();
            _hasToken = false;
            SetDifficulty(player, difficulty);
            if (difficulty == 3)
            {
                for (int i = 4; i < skins.Count; i++)
                {
                    if (skins[i].UnlockLevel == currentLevel)
                    {
                        if (skins[i].Locked)
                            skinInLevel = i;
                        break;
                    }
                }
            }
            if (_ghosts != null)
                foreach (Ghost g in _ghosts)
                    g.Reset();
            ResetCoins();
            return skinInLevel;
        }
        public void SetDefaults(Player player, int difficulty)
        {
            player.Position = _playerStartingPosition;
            player.Reset();
            _hasToken = false;
            SetDifficulty(player, difficulty);
            if (_ghosts != null)
                foreach (Ghost g in _ghosts)
                    g.Reset();
            _coins.Clear();
        }
        public bool PlayerCompleteLevel(Player player)
        {
            return PlayerCompleteLevel(player.Rectangle);
        }
        public bool HasToken { get { return _hasToken; } }
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
        public int CurrentCoins
        {
            get { return _currentCoins; }
        }
        public int CoinsCount()
        {
            return _coins.Count;
        }
        public int TotalCoins
        {
            get { return _totalCoins; }
        }
        public Texture2D CoinTex
        {
            get { return _coinTex; }
        }
    }
}
