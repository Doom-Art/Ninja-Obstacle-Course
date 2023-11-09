using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ninja_Obstacle_Course
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //User Input
        private MouseState _mouseState;
        private MouseState _prevMS;
        private KeyboardState _keyBoard;

        //Level 
        private List<Level> _levels;
        private int _cL;

        //Player Variables
        private Player _player;
        private float _playerRespawnTimer;
        private Camera _camera;
        private int _coins;

        //Skin Variables
        private List<Skin> _ninjaSkins;
        private int _deathCounter, _skinInLevel;

        //Multiplayer Variables
        private Player _player2;
        private float _playerRespawnTimer2;
        private bool _p1Death, _p2Death;
        private int _cL2, _currentSkin2;
        private Camera _camera2;
        private Viewport _viewPort1, _viewPort2, _viewPortDefault;

        //Multiplayer Menu
        private Button[] _arrowButtons2;

        //Shop Variables
        private Button[] _shopButtons;
        private int[] _shopSkins;
        private int _shopSelection, _numShopItems;
        private Texture2D _coinTex;

        //Menu Variables
        private Button[] _arrowButtons;
        private SpriteFont _ninjaFont;
        private List<Vector2> _menuPositions;
        private int _currentSkin;
        private Rectangle _skinRectangle;
        private int _difficulty;
        private Texture2D _lockTex;

        //Settings
        private Sprite _settingsOpener;
        private Button[] _settingsButtons;
        private bool _teacherMode;

        //Music
        private bool _soundOn;
        private List<SoundEffectInstance> _gameMusic;
        private SoundEffectInstance _deathSound;
        private int _cS;

        //Background Images
        private Texture2D _deathBG, _menuBG, _menuBG2, _shopBG;

        private Screen screen;
        private enum Screen
        {
            Menu,
            Multiplayer,
            MultiplayerMenu,
            Shop,
            Settings,
            Death,
            Game
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 600;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();

            _cL = 0;
            _deathCounter = 0;
            _difficulty = 2;
            _coins = 0;
            _numShopItems = -1;
            _soundOn = false;
            _levels = new List<Level>();
            screen = Screen.Menu;
            _camera = new Camera();
            _camera2 = new Camera();
            _teacherMode = false;

            base.Initialize();

            if (File.Exists("Save.txt"))
            {
                int counter = 0;
                foreach (string line in File.ReadLines(@"Save.txt", Encoding.UTF8))
                {
                    switch (counter)
                    {
                        case 0:
                            Int32.TryParse(line, out _coins);
                            break;
                        case 1:
                            for (int i = 0; i < _ninjaSkins.Count; i++)
                            {
                                if (line.Length == i)
                                    break;
                                if (line[i] == '1')
                                {
                                    _ninjaSkins[i].UnlockSkin();
                                }
                            }
                            break;
                        case 2:
                            Int32.TryParse(line, out _deathCounter);
                            break;
                        case 3:
                            if (line == "TeaCHER")
                                _teacherMode = true;
                            break;
                    }
                    counter++;
                }
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D rectangleTex = Content.Load<Texture2D>("Images/Rectangle");
            SpriteFont font = Content.Load<SpriteFont>("Fonts/Font");

            _lockTex = Content.Load<Texture2D>("Images/Lock");
            //Player Content
            _player = new Player(Content.Load<Texture2D>("Images/NinjaSkins/NinjaDarkBlue"), new Rectangle[8] { new Rectangle(31, 14, 38, 72), new Rectangle(131, 14, 38, 72), new Rectangle(231, 14, 38, 72), new Rectangle(31, 114, 38, 72), new Rectangle(131, 114, 38, 72), new Rectangle(231, 114, 38, 72), new Rectangle(31, 214, 38, 72), new Rectangle(131, 214, 38, 72) });
            _player2 = new Player(Content.Load<Texture2D>("Images/NinjaSkins/NinjaDarkBlue"), new Rectangle[8] { new Rectangle(31, 14, 38, 72), new Rectangle(131, 14, 38, 72), new Rectangle(231, 14, 38, 72), new Rectangle(31, 114, 38, 72), new Rectangle(131, 114, 38, 72), new Rectangle(231, 114, 38, 72), new Rectangle(31, 214, 38, 72), new Rectangle(131, 214, 38, 72) });

            _ninjaSkins = new List<Skin>
            {
                new Skin(Content.Load<Texture2D>("Images/NinjaSkins/NinjaDarkBlue")),
                new Skin(Content.Load<Texture2D>("Images/NinjaSkins/NinjaB")),
                new Skin(Content.Load<Texture2D>("Images/NinjaSkins/NinjaW")),
                new Skin(Content.Load<Texture2D>("Images/NinjaSkins/NinjaPink")),             
                //Skin 4 Rainbow Ninja
                new Skin(Content.Load<Texture2D>("Images/NinjaSkins/NinjaRainbow"), Content.Load<Texture2D>("Images/SkinIcons/Rainbow"), new Rectangle(2800, -260, 40, 40), 0),
                // Skin 5 Jester
                new Skin(Content.Load<Texture2D>("Images/NinjaSkins/Jester"), true),
                //Skin 6 Skeleton 
                new Skin(Content.Load<Texture2D>("Images/NinjaSkins/Skeleton"), true),
                //Skin 7 Reaper
                new Skin(Content.Load<Texture2D>("Images/NinjaSkins/Reaper"), true),
                //Skin 8 Aang
                new Skin(Content.Load<Texture2D>("Images/NinjaSkins/Aang"), Content.Load<Texture2D>("Images/SkinIcons/Aang"), new Rectangle(1760, -2520, 40, 40), 1),
                //Skin 9 Mario
                new Skin(Content.Load<Texture2D>("Images/NinjaSkins/Mario"), Content.Load<Texture2D>("Images/SkinIcons/Mario"), new Rectangle(3560, -460, 40, 40), 3),
                //Skin 10 Creeper
                new Skin(Content.Load<Texture2D>("Images/NinjaSkins/Creeper"), Content.Load<Texture2D>("Images/SkinIcons/Creeper"), new Rectangle(2000, -930, 40, 40), 2),
                //Skin 11 Suit
                new Skin(Content.Load<Texture2D>("Images/NinjaSkins/Suit"), true, 300),
                //Skin 12 Skul
                new Skin(Content.Load<Texture2D>("Images/NinjaSkins/Skul"), Content.Load<Texture2D>("Images/SkinIcons/Skul"), new Rectangle(280, -520, 40, 40), 5),
                //Skin 13 SteamBot
                new Skin(Content.Load<Texture2D>("Images/NinjaSkins/SteamBot"), true, 500),
                //Skin 14 Rectangle
                new Skin(rectangleTex, true)

            };
            _shopSkins = new int[2] { 11, 13 };
            _numShopItems += 2;

            //Menu Content
            _menuPositions = new() { new Vector2(70, 305), new Vector2(70, 365), new Vector2(70, 425) };
            _skinRectangle = new Rectangle(426, 310, 80, 140);
            _ninjaFont = font;
            _arrowButtons = new Button[12]
            {
                //Change Skin
                new Button(Content.Load<Texture2D>("Images/ArrowLeft"), new Rectangle(360, 360, 30, 40)),
                new Button(Content.Load<Texture2D>("Images/ArrowRight"), new Rectangle(540, 360, 30, 40)),
                //Change Level
                new Button(Content.Load<Texture2D>("Images/ArrowLeft"), new Rectangle(20, 300, 30, 40)),
                new Button(Content.Load<Texture2D>("Images/ArrowRight"), new Rectangle(220, 300, 30, 40)),
                //Change Music
                new Button(Content.Load<Texture2D>("Images/ArrowLeft"), new Rectangle(20, 360, 30, 40)),
                new Button(Content.Load<Texture2D>("Images/ArrowRight"), new Rectangle(220, 360, 30, 40)),
                //Change Difficulty
                new Button(Content.Load<Texture2D>("Images/ArrowLeft"), new Rectangle(20, 420, 30, 40)),
                new Button(Content.Load<Texture2D>("Images/ArrowRight"), new Rectangle(220, 420, 30, 40)),
                //Play Game
                new Button(Content.Load<Texture2D>("Images/Rectangle"), new Rectangle(95, 180, 100, 100), Color.White*0 ),
                //Multiplayer
                new Button(rectangleTex, font, new Rectangle(350,115,170,40), "Multiplayer", Color.DarkGreen),
                //Shop
                new Button(rectangleTex, font, new Rectangle(350,165,170,40), "Coin Shop", Color.DarkGreen),
                //Reset
                new Button(rectangleTex, font, new Rectangle(350,215,170,40), "Reset Game", Color.DarkGreen)
            };
            //Multiplayer Menu buttons
            _arrowButtons2 = new Button[6]
            {
                new Button(Content.Load<Texture2D>("Images/Rectangle"), new Rectangle(270, 200, 100, 100), Color.White*0 ),
                new Button(Content.Load<Texture2D>("Images/ArrowLeft"), new Rectangle(25, 300, 30, 40)),
                new Button(Content.Load<Texture2D>("Images/ArrowRight"), new Rectangle(235, 300, 30, 40)),
                new Button(Content.Load<Texture2D>("Images/ArrowLeft"), new Rectangle(330, 300, 30, 40)),
                new Button(Content.Load<Texture2D>("Images/ArrowRight"), new Rectangle(540, 300, 30, 40)),
                new Button(Content.Load<Texture2D>("Images/Escape"), new Rectangle(20,20,30,30))
            };

            //Shop
            _coinTex = Content.Load<Texture2D>("Images/Coin");
            _shopButtons = new Button[4]
            {
                new Button(Content.Load<Texture2D>("Images/Escape"), new Rectangle(20,50,30,30)), new Button(rectangleTex, font, new Rectangle(230,400,150,40), "Purchase", Color.Teal),
                new Button(Content.Load<Texture2D>("Images/ArrowLeft"), new Rectangle(140,240,40,40)), new Button(Content.Load<Texture2D>("Images/ArrowRight"), new Rectangle(420,240,40,40))
            };

            //Settings
            _settingsOpener = new Sprite(Content.Load<Texture2D>("Images/Gear"))
            {
                Position = new Vector2(860, 10)
            };
            _settingsButtons = new Button[10];
            string[] st = new string[10] { "Set Left Key", "Set Right Key", "Set Jump Key", "Set Sprint Key", "Resume Game", "Main Menu", "Quit Game", "Sound: On" ,"Restart","Prev Level"};
            int num = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 1; j < 3; j++)
                {
                    _settingsButtons[num] = new Button(rectangleTex, font, new Rectangle((240 * j) - 170, (i * 65 + 90), 230, 45), st[num]);
                    num++;
                }
            }
            _settingsButtons[7].AddSecondary("Sound: Off", true);

            //Music
            _gameMusic = new() { Content.Load<SoundEffect>("Sounds/GameMusicK").CreateInstance(), Content.Load<SoundEffect>("Sounds/GameMusicL").CreateInstance(), Content.Load<SoundEffect>("Sounds/GameMusic3").CreateInstance() };
            _deathSound = Content.Load<SoundEffect>("Sounds/Death").CreateInstance();

            //Background Pictures
            _deathBG = Content.Load<Texture2D>("Background Pictures/Death");
            _menuBG = Content.Load<Texture2D>("Background Pictures/Menu");
            _menuBG2 = Content.Load<Texture2D>("Background Pictures/MultiMenu");
            _shopBG = Content.Load<Texture2D>("Background Pictures/ShopBG");


            LevelCreator levelCreator = new LevelCreator(rectangleTex, Content.Load<Texture2D>("Images/Door2"), Content.Load<Texture2D>("Images/GhostPlatform"), Content.Load<Texture2D>("Images/RedWalker"), Content.Load<Texture2D>("Images/RedWalkerDoor"), Content.Load<Texture2D>("Images/Spike"), Content.Load<Texture2D>("Images/ExitPortalW"), Content.Load<Texture2D>("Images/Ghost"), Content.Load<SpriteFont>("Fonts/Small Font"));
            _levels.Add(levelCreator.Level0());
            _levels.Add(levelCreator.Level1());
            _levels.Add(levelCreator.Level2());
            _levels.Add(levelCreator.Level3());
            _levels.Add(levelCreator.DropLevel());
            _levels.Add(levelCreator.MazeOfRa());
            foreach (Level l in _levels){
                l.SetCoinDefaults(_coinTex);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            //Get User Inputs
            _prevMS = _mouseState;
            _mouseState = Mouse.GetState();
            _keyBoard = Keyboard.GetState();

            if (screen == Screen.Game)
            {
                if (_soundOn)
                    _gameMusic[_cS].Play();
                if (!_p1Death)
                {
                    if (_skinInLevel != 0)
                        _levels[_cL].Update(gameTime, _player, _ninjaSkins[_skinInLevel]);
                    else
                        _levels[_cL].Update(gameTime, _player);
                    if (_levels[_cL].PlayerCompleteLevel(_player))
                    {
                        _coins += _levels[_cL].CurrentCoins;
                        if (_levels[_cL].HasToken)
                        {
                            _ninjaSkins[_skinInLevel].UnlockSkin();
                            _player.SetSkin(_ninjaSkins[_skinInLevel].SkinTex);
                        }
                        if (_levels.Count > _cL + 1)
                        {
                            _cL++;
                            _skinInLevel = _levels[_cL].SetDefaults(_player, _difficulty, _ninjaSkins, _cL);
                        }
                        else
                        {
                            _graphics.PreferredBackBufferWidth = 600;
                            _graphics.ApplyChanges();
                            screen = Screen.Menu;
                            _gameMusic[_cS].Stop();
                        }
                        SaveGame(_coins, _deathCounter, _ninjaSkins, _teacherMode);
                    }
                    //Checks Death
                    else if (_difficulty != 0)
                    {
                        if (_levels[_cL].DidPlayerDie(_player))
                        {
                            _p1Death = true;
                        }
                    }
                    _camera.Follow(_player);
                    if (_settingsOpener.Rectangle.Contains(_mouseState.X, _mouseState.Y) && _mouseState.LeftButton == ButtonState.Pressed)
                    {
                        _gameMusic[_cS].Pause();
                        screen = Screen.Settings;
                        _graphics.PreferredBackBufferWidth = 600;
                        _graphics.ApplyChanges();
                    }
                }
                else
                {
                    _playerRespawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (_playerRespawnTimer > 1)
                    {
                        _gameMusic[_cS].Stop();
                        screen = Screen.Death;
                        _graphics.PreferredBackBufferWidth = 600;
                        _graphics.ApplyChanges();
                        _skinInLevel = _levels[_cL].SetDefaults(_player, _difficulty, _ninjaSkins, _cL);
                    }
                }

            }
            else if (screen == Screen.Multiplayer)
            {
                if (_soundOn)
                    _gameMusic[_cS].Play();
                if (!_p1Death && !_p2Death && _cL == _cL2)
                {
                    _levels[_cL].Update(gameTime, _player, _player2);
                    if (_levels[_cL].PlayerCompleteLevel(_player))
                    {
                        if (_levels.Count > _cL + 1)
                        {
                            _cL++;
                            _levels[_cL].SetDefaults(_player, _difficulty);
                        }
                    }
                    else if (_levels[_cL].DidPlayerDie(_player))
                    {
                        _p1Death = true;
                        _levels[_cL].SetDefaults(_player, _difficulty);
                    }
                    if (_levels[_cL2].PlayerCompleteLevel(_player2))
                    {
                        if (_levels.Count > _cL + 1)
                        {
                            _cL2++;
                            _levels[_cL2].SetDefaults(_player2, _difficulty);
                        }
                    }
                    else if (_levels[_cL2].DidPlayerDie(_player2))
                    {
                        _p2Death = true;
                        _levels[_cL2].SetDefaults(_player2, _difficulty);
                    }
                }
                else if (!_p1Death && !_p2Death)
                {
                    _levels[_cL].Update(gameTime, _player);
                    if (_levels[_cL].PlayerCompleteLevel(_player))
                    {
                        if (_levels.Count > _cL + 1)
                        {
                            _cL++;
                            _levels[_cL].SetDefaults(_player, _difficulty);
                        }
                    }
                    else if (_levels[_cL].DidPlayerDie(_player))
                    {
                        _p1Death = true;
                        _levels[_cL].SetDefaults(_player, _difficulty);
                    }

                    _levels[_cL2].Update(gameTime, _player2);
                    if (_levels[_cL2].PlayerCompleteLevel(_player2))
                    {
                        if (_levels.Count > _cL2 + 1)
                        {
                            _cL2++;
                            _levels[_cL2].SetDefaults(_player2, _difficulty);
                        }
                    }
                    else if (_levels[_cL2].DidPlayerDie(_player2))
                    {
                        _p2Death = true;
                        _levels[_cL2].SetDefaults(_player2, _difficulty);
                    }
                }
                else if (_p1Death && _p2Death)
                {
                    _playerRespawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (_playerRespawnTimer > 5)
                    {
                        _p1Death = false;
                        _playerRespawnTimer = 0;
                    }
                    _playerRespawnTimer2 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (_playerRespawnTimer2 > 5)
                    {
                        _p2Death = false;
                        _playerRespawnTimer2 = 0;
                    }
                }
                else if (_p1Death)
                {
                    _playerRespawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (_playerRespawnTimer > 5)
                    {
                        _p1Death = false;
                        _playerRespawnTimer = 0;
                    }
                    _levels[_cL2].Update(gameTime, _player2);
                    if (_levels[_cL2].PlayerCompleteLevel(_player2))
                    {
                        if (_levels.Count > _cL2 + 1)
                        {
                            _cL2++;
                            _levels[_cL2].SetDefaults(_player2, _difficulty);
                        }
                    }
                    else if (_levels[_cL2].DidPlayerDie(_player2))
                    {
                        _p2Death = true;
                        _levels[_cL2].SetDefaults(_player2, _difficulty);
                    }
                }
                else if (_p2Death)
                {
                    _playerRespawnTimer2 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (_playerRespawnTimer2 > 5)
                    {
                        _p2Death = false;
                        _playerRespawnTimer2 = 0;
                    }
                    _levels[_cL].Update(gameTime, _player);
                    if (_levels[_cL].PlayerCompleteLevel(_player))
                    {
                        if (_levels.Count > _cL + 1)
                        {
                            _cL++;
                            _levels[_cL].SetDefaults(_player, _difficulty);
                        }
                    }
                    else if (_levels[_cL].DidPlayerDie(_player))
                    {
                        _p1Death = true;
                        _levels[_cL].SetDefaults(_player, _difficulty);
                    }
                }
                _camera.Follow(_player);
                _camera2.Follow(_player2);
                if (_mouseState.LeftButton == ButtonState.Pressed)
                    if (_arrowButtons2[5].Clicked(_mouseState))
                    {
                        _graphics.PreferredBackBufferWidth = 600;
                        _graphics.PreferredBackBufferHeight = 500;
                        _graphics.ApplyChanges();
                        screen = Screen.Menu;
                        SaveGame(_coins, _deathCounter, _ninjaSkins, _teacherMode);
                    }
            }
            else if (screen == Screen.Menu)
            {
                if (_mouseState.LeftButton == ButtonState.Pressed && _prevMS.LeftButton == ButtonState.Released)
                {
                    if (_arrowButtons[0].Clicked(_mouseState))
                    {
                        if (_currentSkin == 0)
                            _currentSkin = _ninjaSkins.Count - 1;
                        else
                            _currentSkin--;
                    }
                    else if (_arrowButtons[1].Clicked(_mouseState))
                    {
                        if (_currentSkin + 1 < _ninjaSkins.Count)
                            _currentSkin++;
                        else
                            _currentSkin = 0;
                    }
                    else if (_arrowButtons[2].Clicked(_mouseState))
                    {
                        if (_cL == 0)
                            _cL = _levels.Count - 1;
                        else
                            _cL--;
                    }
                    else if (_arrowButtons[3].Clicked(_mouseState))
                    {
                        if (_cL + 1 < _levels.Count)
                            _cL++;
                        else
                            _cL = 0;
                    }
                    else if (_arrowButtons[4].Clicked(_mouseState))
                    {
                        if (_cS == 0)
                            _cS = _gameMusic.Count - 1;
                        else
                            _cS--;
                    }
                    else if (_arrowButtons[5].Clicked(_mouseState))
                    {
                        if (_cS + 1 < _gameMusic.Count)
                            _cS++;
                        else
                            _cS = 0;
                    }
                    else if (_arrowButtons[6].Clicked(_mouseState))
                    {
                        if ((_difficulty == 1 && !_teacherMode) || _difficulty == 0)
                            _difficulty = 3;
                        else if (_difficulty == 1)
                            _difficulty = 0;
                        else
                            _difficulty--;
                    }
                    else if (_arrowButtons[7].Clicked(_mouseState))
                    {
                        if (_difficulty == 3 && _teacherMode)
                            _difficulty = 0;
                        else if (_difficulty == 3)
                            _difficulty = 1;
                        else
                            _difficulty++;
                    }
                    else if (_arrowButtons[8].Clicked(_mouseState))
                    {
                        _skinInLevel = _levels[_cL].SetDefaults(_player, _difficulty, _ninjaSkins, _cL);
                        if (!_ninjaSkins[_currentSkin].Locked)
                            _player.SetSkin(_ninjaSkins[_currentSkin].SkinTex);
                        else
                            _player.SetSkin(_ninjaSkins[0].SkinTex);
                        screen = Screen.Game;
                        _graphics.PreferredBackBufferWidth = 900;
                        _graphics.ApplyChanges();
                    }
                    else if (_arrowButtons[9].Clicked(_mouseState))
                        screen = Screen.MultiplayerMenu;
                    else if (_arrowButtons[10].Clicked(_mouseState))
                        screen = Screen.Shop;
                    else if (_arrowButtons[11].Clicked(_mouseState))
                    {
                        _coins = 0;
                        _deathCounter = 0;
                        for (int i = 4; i < _ninjaSkins.Count; i++)
                        {
                            _ninjaSkins[i].LockSkin();
                        }
                        SaveGame(_coins, _deathCounter, _ninjaSkins, _teacherMode);
                    }
                }
            }
            else if (screen == Screen.MultiplayerMenu)
            {
                if (_mouseState.LeftButton == ButtonState.Pressed && _prevMS.LeftButton == ButtonState.Released)
                {
                    if (_arrowButtons2[0].Clicked(_mouseState))
                    {
                        _graphics.PreferredBackBufferWidth = 1200;
                        _graphics.PreferredBackBufferHeight = 500;
                        _graphics.ApplyChanges();

                        _cL2 = _cL;
                        _levels[_cL].SetDefaults(_player, _difficulty);
                        if (!_ninjaSkins[_currentSkin].Locked)
                            _player.SetSkin(_ninjaSkins[_currentSkin].SkinTex);
                        else
                            _player.SetSkin(_ninjaSkins[0].SkinTex);
                        _levels[_cL2].SetDefaults(_player2, _difficulty);
                        if (!_ninjaSkins[_currentSkin2].Locked)
                            _player2.SetSkin(_ninjaSkins[_currentSkin2].SkinTex);
                        else
                            _player2.SetSkin(_ninjaSkins[0].SkinTex);

                        _player.SetKeys(Keys.A, Keys.D, Keys.W, Keys.S);
                        _player2.SetKeys(Keys.Left, Keys.Right, Keys.Up, Keys.Down);

                        _viewPortDefault = GraphicsDevice.Viewport;
                        _viewPort1 = _viewPortDefault;
                        _viewPort2 = _viewPortDefault;

                        _viewPort1.Width = _viewPort1.Width / 2;
                        _viewPort2.Width = _viewPort2.Width / 2;
                        _viewPort2.X = _viewPort1.Width;
                        screen = Screen.Multiplayer;
                    }
                    else if (_arrowButtons2[1].Clicked(_mouseState))
                    {
                        if (_currentSkin == 0)
                            _currentSkin = _ninjaSkins.Count - 1;
                        else
                            _currentSkin--;
                    }
                    else if (_arrowButtons2[2].Clicked(_mouseState))
                    {
                        if (_currentSkin + 1 < _ninjaSkins.Count)
                            _currentSkin++;
                        else
                            _currentSkin = 0;
                    }
                    else if (_arrowButtons2[3].Clicked(_mouseState))
                    {
                        if (_currentSkin2 == 0)
                            _currentSkin2 = _ninjaSkins.Count - 1;
                        else
                            _currentSkin2--;
                    }
                    else if (_arrowButtons2[4].Clicked(_mouseState))
                    {
                        if (_currentSkin2 + 1 < _ninjaSkins.Count)
                            _currentSkin2++;
                        else
                            _currentSkin2 = 0;
                    }
                    else if (_arrowButtons2[5].Clicked(_mouseState))
                    {
                        screen = Screen.Menu;
                        SaveGame(_coins, _deathCounter, _ninjaSkins, _teacherMode);
                    }

                }
            }
            else if (screen == Screen.Settings)
            {
                if (_mouseState.LeftButton == ButtonState.Pressed && _prevMS.LeftButton == ButtonState.Released)
                {
                    if (_settingsButtons[0].Clicked(_mouseState))
                    {
                        if (Keyboard.GetState().GetPressedKeys().Length == 1)
                            _player.SetLeft(Keyboard.GetState().GetPressedKeys()[0]);
                    }
                    else if (_settingsButtons[1].Clicked(_mouseState))
                    {
                        if (Keyboard.GetState().GetPressedKeys().Length == 1)
                            _player.SetRight(Keyboard.GetState().GetPressedKeys()[0]);
                    }
                    else if (_settingsButtons[2].Clicked(_mouseState))
                    {
                        if (Keyboard.GetState().GetPressedKeys().Length == 1)
                            _player.SetJump(Keyboard.GetState().GetPressedKeys()[0]);
                    }
                    else if (_settingsButtons[3].Clicked(_mouseState))
                    {
                        if (Keyboard.GetState().GetPressedKeys().Length == 1)
                            _player.SetSprint(Keyboard.GetState().GetPressedKeys()[0]);
                    }
                    else if (_settingsButtons[4].Clicked(_mouseState))
                    {
                        _graphics.PreferredBackBufferWidth = 900;
                        _graphics.ApplyChanges();
                        screen = Screen.Game;
                    }
                    else if (_settingsButtons[5].Clicked(_mouseState))
                    {
                        SaveGame(_coins, _deathCounter, _ninjaSkins, _teacherMode);
                        screen = Screen.Menu;
                    }
                    else if (_settingsButtons[6].Clicked(_mouseState))
                    {
                        SaveGame(_coins, _deathCounter, _ninjaSkins, _teacherMode);
                        Exit();
                    }
                    else if (_settingsButtons[7].Clicked(_mouseState))
                    {
                        _settingsButtons[7].SwitchDisplay();
                        _soundOn = !_soundOn;
                    }
                    else if (_settingsButtons[8].Clicked(_mouseState))
                    {
                        _levels[_cL].SetDefaults(_player, _difficulty, _ninjaSkins, _cL);
                        _graphics.PreferredBackBufferWidth = 900;
                        _graphics.ApplyChanges();
                        screen = Screen.Game;
                    }
                    else if (_settingsButtons[9].Clicked(_mouseState))
                    {
                        if (_cL != 0)
                        {
                            _cL--;
                            _levels[_cL].SetDefaults(_player, _difficulty, _ninjaSkins, _cL);
                            _graphics.PreferredBackBufferWidth = 900;
                            _graphics.ApplyChanges();
                            screen = Screen.Game;
                        }
                    }
                }
            }
            else if (screen == Screen.Death)
            {
                if (_soundOn)
                    _deathSound.Play();
                _playerRespawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_playerRespawnTimer > 4)
                {
                    _deathCounter++;
                    screen = Screen.Game;
                    _p1Death = false;
                    _graphics.PreferredBackBufferWidth = 900;
                    _graphics.ApplyChanges();
                    _playerRespawnTimer = 0;
                    _deathSound.Stop();
                    if (_deathCounter == 5)
                    {
                        _ninjaSkins[5].UnlockSkin();
                        _player.SetSkin(_ninjaSkins[5].SkinTex);
                    }
                    else if (_deathCounter == 15)
                    {
                        _ninjaSkins[6].UnlockSkin();
                        _player.SetSkin(_ninjaSkins[6].SkinTex);
                    }
                    else if (_deathCounter == 30)
                    {
                        _ninjaSkins[7].UnlockSkin();
                        _player.SetSkin(_ninjaSkins[7].SkinTex);
                    }
                    else if (_deathCounter == 100)
                    {
                        _ninjaSkins[14].UnlockSkin();
                        _player.SetSkin(_ninjaSkins[14].SkinTex);
                    }
                }
            }
            else if (screen == Screen.Shop)
            {
                if (_mouseState.LeftButton == ButtonState.Pressed && _prevMS.LeftButton == ButtonState.Released)
                {
                    if (_shopButtons[0].Clicked(_mouseState))
                    {
                        SaveGame(_coins, _deathCounter, _ninjaSkins, _teacherMode);
                        screen = Screen.Menu;
                    }
                    else if (_shopButtons[1].Clicked(_mouseState))
                    {
                        switch (_shopSelection)
                        {
                            case < 2:
                                if (_coins >= _ninjaSkins[_shopSkins[_shopSelection]].Price && _ninjaSkins[_shopSkins[_shopSelection]].Locked)
                                {
                                    _coins -= _ninjaSkins[_shopSkins[_shopSelection]].Price;
                                    _ninjaSkins[_shopSkins[_shopSelection]].UnlockSkin();
                                }
                                break;
                        }
                    }
                    else if (_shopButtons[2].Clicked(_mouseState))
                    {
                        if (_shopSelection == 0)
                            _shopSelection = _numShopItems;
                        else
                            _shopSelection--;


                    }
                    else if (_shopButtons[3].Clicked(_mouseState))
                    {
                        if (_shopSelection == _numShopItems)
                            _shopSelection = 0;
                        else
                            _shopSelection++;
                    }

                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (screen == Screen.Game)
            {
                GraphicsDevice.Clear(Color.Coral);
                _spriteBatch.Begin(transformMatrix: _camera.Transform);
                if (_p1Death)
                {
                    if (_playerRespawnTimer < 0.3f)
                        _levels[_cL].DrawDeath(_spriteBatch, _player);
                    else if (_playerRespawnTimer < 0.6f)
                        _levels[_cL].Draw(_spriteBatch, _player);
                    else
                        _levels[_cL].DrawDeath(_spriteBatch, _player);
                }
                else if (_skinInLevel != 0)
                    _levels[_cL].Draw(_spriteBatch, _player, _ninjaSkins[_skinInLevel]);
                else
                    _levels[_cL].Draw(_spriteBatch, _player);
                _spriteBatch.End();
                _spriteBatch.Begin();
                _settingsOpener.Draw(_spriteBatch);
                _spriteBatch.Draw(_coinTex, new Rectangle(10,10,30,30), Color.White);
                _spriteBatch.DrawString(_ninjaFont, $"= {_levels[_cL].CurrentCoins}/{_levels[_cL].TotalCoins}", new Vector2(42, 10), Color.Black);
                _spriteBatch.DrawString(_ninjaFont, $"Level: {_cL}", new Vector2(10, 56), Color.Black);
                _spriteBatch.End();
            }
            else if (screen == Screen.Shop)
            {
                GraphicsDevice.Clear(Color.Coral);
                _spriteBatch.Begin();
                _spriteBatch.Draw(_shopBG, new Vector2(0, 0), Color.White);
                _spriteBatch.Draw(_coinTex, new Rectangle(10, 10, 30, 30), Color.White);
                _spriteBatch.DrawString(_ninjaFont, $"= {_coins}", new Vector2(42, 10), Color.Black);
                switch (_shopSelection)
                {
                    case < 2:
                        _spriteBatch.DrawString(_ninjaFont, $"Price: {_ninjaSkins[_shopSkins[_shopSelection]].Price} Coins", new Vector2(200,100), Color.Black);
                        _spriteBatch.Draw(_ninjaSkins[_shopSkins[_shopSelection]].SkinTex, new Rectangle(260,200,90,140),new Rectangle(31, 14, 38, 72), Color.White);
                        break;
                }
                foreach (Button b in _shopButtons)
                    b.Draw(_spriteBatch);
                _spriteBatch.End();
            }
            else if (screen == Screen.Menu)
            {
                GraphicsDevice.Clear(Color.White);
                _spriteBatch.Begin();
                _spriteBatch.Draw(_menuBG, new Vector2(0, 0), Color.White);
                for (int i = 0; i < _arrowButtons.Count(); i++)
                {
                    _arrowButtons[i].Draw(_spriteBatch);
                }
                _spriteBatch.Draw(_ninjaSkins[_currentSkin].SkinTex, _skinRectangle, new Rectangle(31, 14, 38, 72), Color.White);
                if (_ninjaSkins[_currentSkin].Locked)
                    _spriteBatch.Draw(_lockTex, _skinRectangle, Color.White* 0.8f);
                if (_cL == 0)
                    _spriteBatch.DrawString(_ninjaFont, "Tutorial", _menuPositions[0], Color.Black);
                else
                    _spriteBatch.DrawString(_ninjaFont, $"Level: {_cL}", _menuPositions[0], Color.Black);

                _spriteBatch.DrawString(_ninjaFont, $"Music: {_cS + 1}", _menuPositions[1], Color.Black);
                switch (_difficulty)
                {
                    case 0:
                        _spriteBatch.DrawString(_ninjaFont, "Teacher", _menuPositions[2], Color.Black);
                        break;
                    case 1:
                        _spriteBatch.DrawString(_ninjaFont, "Easy", _menuPositions[2], Color.Black);
                        break;
                    case 2:
                        _spriteBatch.DrawString(_ninjaFont, "Normal", _menuPositions[2], Color.Black);
                        break;
                    case 3:
                        _spriteBatch.DrawString(_ninjaFont, "Hard", _menuPositions[2], Color.Black);
                        break;
                }
                _spriteBatch.End();
            }
            else if (screen == Screen.Settings)
            {
                GraphicsDevice.Clear(Color.SkyBlue);
                _spriteBatch.Begin();
                _spriteBatch.DrawString(_ninjaFont, "To Change KeyBinds Hold the Key Down\n                and click the button", new Vector2(22, 10), Color.Black);
                foreach (Button b in _settingsButtons)
                    b.Draw(_spriteBatch);
                _spriteBatch.End();
            }
            else if (screen == Screen.Death)
            {
                GraphicsDevice.Clear(Color.Red);
                _spriteBatch.Begin();
                _spriteBatch.Draw(_deathBG, new Vector2(0, 0), Color.White);
                _spriteBatch.End();
            }
            else if (screen == Screen.Multiplayer)
            {
                GraphicsDevice.Clear(Color.Coral);
                GraphicsDevice.Viewport = _viewPort1;
                if (_p1Death)
                {
                    _spriteBatch.Begin();
                    _spriteBatch.Draw(_deathBG, new Vector2(0, 0), Color.White);
                    _spriteBatch.End();
                }
                else
                {
                    _spriteBatch.Begin(transformMatrix: _camera.Transform);
                    if (_cL == _cL2)
                        _levels[_cL].Draw(_spriteBatch, _player, _player2);
                    else
                        _levels[_cL].Draw(_spriteBatch, _player);
                    _spriteBatch.End();
                }
                GraphicsDevice.Viewport = _viewPort2;
                if (_p2Death)
                {
                    _spriteBatch.Begin();
                    _spriteBatch.Draw(_deathBG, new Vector2(0, 0), Color.White);
                    _spriteBatch.End();
                }
                else
                {
                    _spriteBatch.Begin(transformMatrix: _camera2.Transform);
                    if (_cL2 == _cL)
                        _levels[_cL2].Draw(_spriteBatch, _player2, _player);
                    else
                        _levels[_cL2].Draw(_spriteBatch, _player2);
                    _spriteBatch.End();
                }
                GraphicsDevice.Viewport = _viewPortDefault;
                _spriteBatch.Begin();
                _arrowButtons2[5].Draw(_spriteBatch);
                _spriteBatch.End();
            }
            else if (screen == Screen.MultiplayerMenu)
            {
                GraphicsDevice.Clear(Color.White);
                _spriteBatch.Begin();
                _spriteBatch.Draw(_menuBG2, new Vector2(0, 0), Color.White);
                _spriteBatch.DrawString(_ninjaFont, "    Player 1: \nW for Jump\nA for Left\nD for Right\nS for Sprint", new Vector2(30, 20), Color.Black);
                _spriteBatch.DrawString(_ninjaFont, "Player 2: \nUp for Jump\nLeft for Left\nRight for Right\nDown for Sprint", new Vector2(300, 20), Color.Black);
                _spriteBatch.Draw(_ninjaSkins[_currentSkin].SkinTex, new Rectangle(100, 250, 80, 140), new Rectangle(31, 14, 38, 72), Color.White);
                if (_ninjaSkins[_currentSkin].Locked)
                    _spriteBatch.Draw(_lockTex, new Rectangle(100, 250, 80, 140), Color.White * 0.8f);
                _spriteBatch.Draw(_ninjaSkins[_currentSkin2].SkinTex, new Rectangle(420, 250, 80, 140), new Rectangle(31, 14, 38, 72), Color.White);
                if (_ninjaSkins[_currentSkin2].Locked)
                    _spriteBatch.Draw(_lockTex, new Rectangle(420, 250, 80, 140), Color.White * 0.8f);
                foreach (Button b in _arrowButtons2)
                    b.Draw(_spriteBatch);
                _spriteBatch.End();
            }

            base.Draw(gameTime);
        }
        public static void SaveGame(int coins, int death, List<Skin> ninjaSkins, bool teacherMode)
        {
            StreamWriter save = new StreamWriter("Save.txt");
            save.WriteLine(coins);
            for (int i = 0; i < ninjaSkins.Count; i++)
            {
                if (ninjaSkins[i].Locked)
                {
                    save.Write(0);
                }
                else
                    save.Write(1);
            }
            save.WriteLine();
            save.WriteLine(death);
            if (teacherMode)
                save.WriteLine("TeaCHER");
            else
                save.WriteLine("false");
            save.Close();
        }
    }
}