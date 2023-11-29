using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        private readonly List<Portal> _portals;
        private readonly List<Platform> _platforms;
        private List<Platform> _spikes;
        private readonly List<RedWalker> _redWalkers;
        private List<Ghost> _ghosts;
        private readonly List<Vector2> _signLocations;
        private List<Mage> _mages;
        private readonly List<String> _signText;
        private SpriteFont _signFont;
        private Texture2D _exitPortalTex, _coinTex;
        private Rectangle _exitPortalRect;
        private Vector2 _playerStartingPosition;
        private bool _hasToken;
        private readonly List<Coin> _coins;
        private int _currentCoins, _totalCoins;
        private Environment _environment;

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
        public void SetFont(SpriteFont signFont)
        {
            _signFont = signFont;
        }
        public Environment Environment
        {
            set { _environment = value; }
        }
        public void ClearSigns()
        {
            _signLocations.Clear();
            _signText.Clear();
        }
        public void AddPlatform(Platform platform)
        {
            _platforms.Add(platform);
        }
        public void AddGhost(Ghost ghost)
        {
            _ghosts ??= new();
            _ghosts.Add(ghost);
        }
        public void AddMage(Mage mage)
        {
            _mages ??= new();
            _mages.Add(mage);
        }
        public void AddSpike(Platform spike)
        {
            _spikes ??= new();
            _spikes.Add(spike);
        }
        public List<Collectible> Collectibles
        {
            private get; set;
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
            _mages ??= new();
            foreach (Mage m in _mages)
                m.SetDifficulty(difficulty);
        }
        public void DrawDeath(SpriteBatch sprite, Player player)
        {
            _environment?.Draw(sprite);
            foreach (Coin c in _coins) { c.Draw(sprite); }
            foreach (Portal portal in _portals)
                portal.Draw(sprite);
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
            foreach(Mage m in _mages)
                m.Draw(sprite);
        }
        public void Draw(SpriteBatch sprite, Player player)
        {
            _environment?.Draw(sprite);
            if (_environment.HasCollectible && Collectibles != null)
            {
                foreach (Collectible c in Collectibles)
                    c.Draw(_environment.CollectibleTex, sprite);
            }
            foreach (Coin c in _coins) { c.Draw(sprite); }
            foreach (Portal portal in _portals)
                portal.Draw(sprite);
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
            foreach (Mage m in _mages)
                m.Draw(sprite);
        }
        public void Draw(SpriteBatch sprite, Player player, Skin skin)
        {
            Draw(sprite, player);
            if (!_hasToken)
                skin.DrawIcon(sprite);
        }
        public void Draw(SpriteBatch sprite, Player player, Player player2)
        {
            _environment?.Draw(sprite);
            if (_environment.HasCollectible && Collectibles != null)
                foreach (Collectible c in Collectibles)
                    c.Draw(_environment.CollectibleTex, sprite);
            foreach (Portal portal in _portals)
                portal.Draw(sprite);
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
            foreach (Mage m in _mages)
                m.Draw(sprite);
        }
        public void Update(GameTime gameTime, Player player, KeyboardState keyboard)
        {
            foreach (Platform p in _platforms)
                p.Update();
            player.Update(gameTime, _platforms, keyboard);
            if (_redWalkers != null)
                for (int i = 0; i < _redWalkers.Count; i++)
                {
                    _redWalkers[i].Update(gameTime);
                }
            if (_ghosts != null && player.Opacity == 1)
                foreach (Ghost g in _ghosts)
                    g.Update(player);
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

            for (int i = 0; i < _coins.Count; i++)
            {
                if (player.Touching(_coins[i].CoinRect))
                {
                    _currentCoins++;
                    _coins.RemoveAt(i);
                    break;
                }
            }
            if (player.Opacity == 1)
                foreach (Mage m in _mages)
                    m.Update(_platforms, player);
            if (_environment.HasCollectible && Collectibles != null)
                for (int i = 0; i < Collectibles.Count; i++)
                    if (Collectibles[i].Collected(player))
                    {
                        player.Meter.Gain();
                        Collectible c = Collectibles[i];
                        c.Visible = false;
                        Collectibles[i] = c;
                    }
        }
        public void Update(GameTime gameTime, Player player, Player player2, KeyboardState keyboard)
        {
            foreach (Platform p in _platforms)
                p.Update();
            player.Update(gameTime, _platforms, keyboard);
            player2.Update(gameTime, _platforms, keyboard);
            if (_redWalkers != null)
                for (int i = 0; i < _redWalkers.Count; i++)
                {
                    _redWalkers[i].Update(gameTime);
                }
            if (_ghosts != null && player.Opacity == 1 && player2.Opacity == 1)
            {
                foreach (Ghost g in _ghosts)
                    g.Update(player, player2);
            }
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
                }
                if (p.InPortal(player2.Rectangle))
                {
                    if (!player2.FadingOut())
                        player2.Position = p.PortalExit();
                }
                else if (p.OutPortal(player2.Rectangle))
                {
                    player2.FadingIn();
                }
            }
            if (player.Opacity == 1 && player2.Opacity == 1)
                foreach (Mage m in _mages)
                    m.Update(_platforms, player, player2);
            if (_environment.HasCollectible && Collectibles != null)
            {
                for (int i = 0; i < Collectibles.Count; i++)
                {
                    if (Collectibles[i].Collected(player))
                    {
                        player.Meter.Gain();
                        Collectible c = Collectibles[i];
                        c.Visible = false;
                        Collectibles[i] = c;
                    }
                    else if (Collectibles[i].Collected(player2))
                    {
                        player2.Meter.Gain();
                        Collectible c = Collectibles[i];
                        c.Visible = false;
                        Collectibles[i] = c;
                    }
                }
                    
            }
        }
        public void Update(GameTime gameTime, Player player, Skin skin, KeyboardState keyboard)
        {
            if (player.Touching(skin.IconLocation))
                _hasToken = true;
            Update(gameTime, player, keyboard);
        }
        public bool DidPlayerDie(Player player)
        {
            bool death = false;
            if (!player.SecondLife)
            {
                if (_redWalkers != null)
                {
                    foreach (RedWalker r in _redWalkers)
                    {
                        if (player.Touching(r.Position) && r.Visible)
                        {
                            death = true;
                        }
                    }
                }
                if (_spikes != null && !death)
                {
                    foreach (Platform p in _spikes)
                        if (player.Touching(p.Rectangle) && !p.Hidden)
                        {
                            death = true;
                        }
                }
                if (_ghosts != null && !death)
                {
                    foreach (Ghost g in _ghosts)
                    {
                        if (player.Touching(g.Rectangle)&& !g.Hidden)
                        {
                            death = true;
                        }
                    }
                }
                if (!death)
                {
                    foreach (Mage m in _mages)
                        if (m.DidHit(player))
                            death = true;
                }
                if (!death && _environment.HasCollectible)
                {
                    if (player.Meter.Size <= 0)
                        death = true;
                }
            }
            else
            {
                if (_redWalkers != null)
                {
                    foreach (RedWalker r in _redWalkers)
                    {
                        if (player.Touching(r.Position))
                        {
                            r.Hide();
                            player.SecondLife = false;
                        }
                    }
                }
                if (_spikes != null && player.SecondLife)
                {
                    foreach (Platform p in _spikes)
                        if (player.Touching(p.Rectangle))
                        {
                            p.Hidden = true;
                            player.SecondLife = false;
                        }
                }
                if (_ghosts != null && player.SecondLife)
                {
                    foreach (Ghost g in _ghosts)
                    {
                        if (player.Touching(g.Rectangle))
                        {
                            g.Hidden = true;
                            player.SecondLife = false;
                        }
                    }
                }
                if (player.SecondLife)
                {
                    foreach (Mage m in _mages)
                        if (m.DidHit(player))
                        {
                            player.SecondLife = false;
                            m.Hidden = true;
                        }
                }
                if (player.SecondLife && _environment.HasCollectible)
                {
                    if (player.Meter.Size <= 0)
                    {
                        player.SecondLife = false;
                        player.Meter.Reset();
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
                        Rectangle coinR = new(_platforms[j].Rectangle.X + i, _platforms[j].Rectangle.Y - 40, 30, 30);
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
            player.Reset();
            if (_environment.HasCollectible)
            {
                player.Meter = _environment.Meter.Clone(difficulty);
                if (Collectibles != null)
                {
                    for (int i = 0; i < Collectibles.Count; i++)
                        if (!Collectibles[i].Visible)
                        {
                            Collectible c = Collectibles[i];
                            c.Visible = true;
                            Collectibles[i] = c;
                        }
                }
            }
            player.MaxGrav = _environment.MaxGravity;
            int skinInLevel = 0;
            player.Position = _playerStartingPosition;
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
            foreach (Portal p in _portals)
            {
                p.SetWidth(50);
            }
            if (_ghosts != null)
                foreach (Ghost g in _ghosts)
                    g.Reset();
            ResetCoins();
            return skinInLevel;
        }
        public int SetDefaults(Player player, int difficulty, List<Skin> skins, int currentLevel, Powerup powerup)
        {
            player.Reset();
            int skinInLevel = 0;
            if (_environment.HasCollectible)
            {
                player.Meter = _environment.Meter.Clone(difficulty);
                if (Collectibles != null)
                {
                    for (int i = 0; i < Collectibles.Count; i++)
                        if (!Collectibles[i].Visible)
                        {
                            Collectible c = Collectibles[i];
                            c.Visible = true;
                            Collectibles[i] = c;
                        }
                }
            }
            player.MaxGrav = _environment.MaxGravity;
            player.Position = _playerStartingPosition;
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

            player.BoostStats(powerup);

            if (powerup.SpikeRemoval)
                foreach (Platform spike in _spikes)
                    spike.SpikeShrink(difficulty);
            if (powerup.DoorBooster)
                foreach (Portal p in _portals)
                    p.SetWidth(90);
            else if (powerup.LimiterRemoval && _mages != null)
                foreach (Mage m in _mages)
                    m.LimiterRemoval = true;
            return skinInLevel;
        }
        public void SetDefaults(Player player, int difficulty)
        {
            player.Reset();
            if (_environment.HasCollectible)
            {
                player.Meter = _environment.Meter.Clone(difficulty);
                if (Collectibles != null)
                {
                    for (int i = 0; i < Collectibles.Count; i++)
                        if (!Collectibles[i].Visible)
                        {
                            Collectible c = Collectibles[i];
                            c.Visible = true;
                            Collectibles[i] = c;
                        }
                }
            }
            foreach (Portal p in _portals)
            {
                p.SetWidth(50);
            }
            player.MaxGrav = _environment.MaxGravity;
            player.Position = _playerStartingPosition;
            _hasToken = false;
            SetDifficulty(player, difficulty);
            if (_ghosts != null)
                foreach (Ghost g in _ghosts)
                    g.Reset();
            _coins.Clear();
        }
        public bool PlayerCompleteLevel(Player player)
        {
            if (_exitPortalTex == null)
                return false;
            else
                return _exitPortalRect.Contains(player.Rectangle);
        }
        public bool HasToken { get { return _hasToken; } }
        public int CurrentCoins
        {
            get { return _currentCoins; }
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
