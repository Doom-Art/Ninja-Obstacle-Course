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
    /* Credits:
     * Devlopers: Kian Dufraimont (Doom), Louis Rouhani (Lou)
     * Coding: Kian Dufraimont
     * Sprite Art: Kian Dufraimont & Louis Rouhani (RedWalker, Spikes, GhostPlatforms)
     * Soundtracks: Kian Dufraimont & Louis Rouhani (Music Track 2)
     * Level Designs: Kian Dufraimont
     */
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //User Input
        private MouseState _mouseState, _prevMS;
        private KeyboardState _keyBoard, _prevKB;

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
        private int _shopSelection, _numShopItems, _equipedPet, _currentPowerUp, _bgColor;
        private Texture2D _coinTex, _rectTex;
        private List<Pet> _pets;
        private List<Powerup> _powerups;
        private SpriteFont _powerupFont;
        private List<Color> _colors;

        //Menu Variables
        private Button[] _arrowButtons;
        private SpriteFont _ninjaFont;
        private List<Vector2> _menuPositions;
        private int _currentSkin;
        private Rectangle _skinRectangle;
        private int _difficulty;
        private Texture2D _lockTex;

        //Settings
        private Button _settingsOpener;
        private Button[] _settingsButtons;
        private bool _teacherMode;

        //Music
        private bool _soundOn;
        private List<SoundEffectInstance> _gameMusic;
        private SoundEffectInstance _deathSound;
        private int _cS;

        //Info
        private Button[] _infoButtons;
        private int _infoPage;

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
            Game,
            MoreInfo,
            P1Win,
            P2Win
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
            _currentPowerUp = -1;
            _soundOn = false;
            _levels = new List<Level>();
            screen = Screen.Menu;
            _camera = new Camera();
            _camera2 = new Camera();
            _teacherMode = false;
            _equipedPet = -1;
            _bgColor = 0;
            _colors = new()
            {
                Color.LightGoldenrodYellow,
                Color.Coral,
                Color.Indigo,
                Color.SkyBlue,
                Color.Magenta,
                Color.LimeGreen,
                Color.White,
                Color.Black,
            };
            _numShopItems = _colors.Count - 1;

            base.Initialize();

            //Load Save File
            if (File.Exists("Save.txt"))
            {
                int counter = 0;
                foreach (string line in File.ReadLines(@"Save.txt", Encoding.UTF8))
                {
                    switch (counter)
                    {
                        case 0:
                            _ = int.TryParse(line, out _coins);
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
                            _ = int.TryParse(line, out _deathCounter);
                            break;
                        case 3:
                            if (line == "TeaCHER")
                                _teacherMode = true;
                            break;
                        case 4:
                            if (Int32.TryParse(line, out _equipedPet))
                                if (_equipedPet != -1)
                                {
                                    _player.Pet = (_pets[_equipedPet]);
                                }
                            break;
                        case 5:
                            for (int i = 0; i < _pets.Count; i++)
                            {
                                if (line.Length == i)
                                    break;
                                if (line[i] == '1')
                                {
                                    _pets[i].UnlockPet();
                                }
                            }
                            break;
                        case 6:
                            _ = int.TryParse(line, out _currentPowerUp);
                            break;
                        case 7:
                            _ = int.TryParse(line, out _bgColor);
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
            _player = new Player(Content.Load<Texture2D>("Images/NinjaSkins/NinjaDarkBlue"), new Rectangle[8] { new (31, 14, 38, 72), new (131, 14, 38, 72), new (231, 14, 38, 72), new (31, 114, 38, 72), new (131, 114, 38, 72), new (231, 114, 38, 72), new (31, 214, 38, 72), new (131, 214, 38, 72) });
            _player2 = new Player(Content.Load<Texture2D>("Images/NinjaSkins/NinjaDarkBlue"), new Rectangle[8] { new (31, 14, 38, 72), new (131, 14, 38, 72), new (231, 14, 38, 72), new (31, 114, 38, 72), new (131, 114, 38, 72), new (231, 114, 38, 72), new (31, 214, 38, 72), new (131, 214, 38, 72) });

            //Menu Content
            _menuPositions = new() { new Vector2(70, 305), new Vector2(70, 365), new Vector2(70, 425) };
            _skinRectangle = new Rectangle(426, 310, 80, 140);
            _ninjaFont = font;
            _arrowButtons = new Button[13]
            {
                //Change Skin
                new (Content.Load<Texture2D>("Images/ArrowLeft"), new Rectangle(360, 360, 30, 40)),
                new (Content.Load<Texture2D>("Images/ArrowRight"), new Rectangle(540, 360, 30, 40)),
                //Change Level
                new (Content.Load<Texture2D>("Images/ArrowLeft"), new Rectangle(20, 300, 30, 40)),
                new (Content.Load<Texture2D>("Images/ArrowRight"), new Rectangle(220, 300, 30, 40)),
                //Change Music
                new (Content.Load<Texture2D>("Images/ArrowLeft"), new Rectangle(20, 360, 30, 40)),
                new (Content.Load<Texture2D>("Images/ArrowRight"), new Rectangle(220, 360, 30, 40)),
                //Change Difficulty
                new (Content.Load<Texture2D>("Images/ArrowLeft"), new Rectangle(20, 420, 30, 40)),
                new (Content.Load<Texture2D>("Images/ArrowRight"), new Rectangle(220, 420, 30, 40)),
                //Play Game
                new (Content.Load<Texture2D>("Images/Rectangle"), new Rectangle(95, 180, 100, 100), Color.White*0 ),
                //Multiplayer
                new (rectangleTex, font, new Rectangle(350,115,170,40), "Multiplayer", Color.DarkGreen),
                //Shop
                new (rectangleTex, font, new Rectangle(350,165,170,40), "Coin Shop", Color.DarkGreen),
                //Reset
                new(rectangleTex, font, new Rectangle(350,215,170,40), "Reset Game", Color.DarkGreen),
                new(Content.Load<Texture2D>("Images/Info"), new Rectangle(540,115,40,40), Color.RoyalBlue)
            };

            //Multiplayer Menu buttons
            _arrowButtons2 = new Button[6]
            {
                new (rectangleTex, new Rectangle(270, 200, 60, 60), Color.White*0 ),
                new (Content.Load<Texture2D>("Images/ArrowLeft"), new Rectangle(25, 300, 30, 40)),
                new (Content.Load<Texture2D>("Images/ArrowRight"), new Rectangle(235, 300, 30, 40)),
                new (Content.Load<Texture2D>("Images/ArrowLeft"), new Rectangle(330, 300, 30, 40)),
                new (Content.Load<Texture2D>("Images/ArrowRight"), new Rectangle(540, 300, 30, 40)),
                new (Content.Load<Texture2D>("Images/Escape"), new Rectangle(20,20,30,30))
            };

            //Info Page Buttons
            _infoButtons = new Button[3]
            {
                new(rectangleTex, font, new Rectangle(10,440,150,45), "Main Menu"),
                new(rectangleTex, font, new Rectangle(250,440,150,45), "Prev Page"),
                new(rectangleTex, font, new Rectangle(420,440,150,45), "Next Page")
            };

            //Settings
            _settingsOpener = new(Content.Load<Texture2D>("Images/Gear"), new Rectangle(860, 10, 30, 30));
            _settingsButtons = new Button[10];
            string[] st = new string[10] { "Set Left Key", "Set Right Key", "Set Jump Key", "Set Sprint Key", "Auto Sprint: On", "Sound: On", "Resume Game", "Restart", "Main Menu", "Quit Game" };
            int num = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 1; j < 3; j++)
                {
                    _settingsButtons[num] = new Button(rectangleTex, font, new Rectangle((240 * j) - 170, (i * 65 + 90), 230, 45), st[num]);
                    num++;
                }
            }
            _settingsButtons[5].AddSecondary("Sound: Off", !_soundOn);
            _settingsButtons[4].AddSecondary("Auto Sprint: Off", !_player.AutoSprint);

            //Shop
            _coinTex = Content.Load<Texture2D>("Images/Coin");
            _shopButtons = new Button[5]
            {
                new (Content.Load<Texture2D>("Images/Escape"), new Rectangle(20,50,30,30)), new (rectangleTex, font, new Rectangle(230,400,150,40), "Purchase", Color.Teal),
                new (Content.Load<Texture2D>("Images/ArrowLeft"), new Rectangle(140,240,40,40)), new (Content.Load<Texture2D>("Images/ArrowRight"), new Rectangle(420,240,40,40)),
                new (rectangleTex, font, new Rectangle(80,400,120,40), "Eqiup", Color.Teal)
            };
            _shopButtons[4].AddSecondary("Unequip", false);
            
            //Skins
            _ninjaSkins = new List<Skin>
            {
                new (Content.Load<Texture2D>("Images/NinjaSkins/NinjaDarkBlue")),
                new (Content.Load<Texture2D>("Images/NinjaSkins/NinjaB")),
                new (Content.Load<Texture2D>("Images/NinjaSkins/NinjaW")),
                new (Content.Load<Texture2D>("Images/NinjaSkins/NinjaPink")),             
                //Skin 4 Rainbow Ninja
                new (Content.Load<Texture2D>("Images/NinjaSkins/NinjaRainbow"), Content.Load<Texture2D>("Images/SkinIcons/Rainbow"), new Rectangle(2500, -260, 40, 40), 0),
                // Skin 5 Jester
                new (Content.Load<Texture2D>("Images/NinjaSkins/Jester"), true),
                //Skin 6 Skeleton 
                new (Content.Load<Texture2D>("Images/NinjaSkins/Skeleton"), true),
                //Skin 7 Reaper
                new (Content.Load<Texture2D>("Images/NinjaSkins/Reaper"), true),
                //Skin 8 Aang
                new (Content.Load<Texture2D>("Images/NinjaSkins/Aang"), Content.Load<Texture2D>("Images/SkinIcons/Aang"), new Rectangle(1760, -2520, 40, 40), 1),
                //Skin 9 Mario
                new (Content.Load<Texture2D>("Images/NinjaSkins/Mario"), Content.Load<Texture2D>("Images/SkinIcons/Mario"), new Rectangle(3560, -460, 40, 40), 3),
                //Skin 10 Creeper
                new (Content.Load<Texture2D>("Images/NinjaSkins/Creeper"), Content.Load<Texture2D>("Images/SkinIcons/Creeper"), new Rectangle(2000, -930, 40, 40), 2),
                //Skin 11 Suit
                new (Content.Load<Texture2D>("Images/NinjaSkins/Suit"), true, 300),
                //Skin 12 Skul
                new (Content.Load<Texture2D>("Images/NinjaSkins/Skul"), Content.Load<Texture2D>("Images/SkinIcons/Skul"), new Rectangle(280, -520, 40, 40), 5),
                //Skin 13 SteamBot
                new (Content.Load<Texture2D>("Images/NinjaSkins/SteamBot"), true, 500),
                //Skin 14 Rectangle
                new (rectangleTex, true),
                //Skin 15 Clone Trooper
                new (Content.Load<Texture2D>("Images/NinjaSkins/Clone"), Content.Load<Texture2D>("Images/SkinIcons/Clone"),new Rectangle(1120,-1500,40,40), 6),
                //Skin 16 Mummy
                new (Content.Load<Texture2D>("Images/NinjaSkins/Mummy"), Content.Load<Texture2D>("Images/SkinIcons/Mummy"), new Rectangle(1680,-1660,40,40), 11),
                //Skin 17 Anubis
                new Skin(Content.Load<Texture2D>("Images/NinjaSkins/Anubis"), true)
            };
            _shopSkins = new int[] { 11, 13 };
            _numShopItems += _shopSkins.Length;

            //Pets
            _pets = new()
            {
                new Pet(Content.Load<Texture2D>("Images/Pets/Boo"), 400, "Boo"),
                new Pet(Content.Load<Texture2D>("Images/Pets/Duck"), 100, "Duck"),
                new Pet(Content.Load<Texture2D>("Images/Pets/Ten"), 10, "Bill"),
                new Pet(Content.Load<Texture2D>("Images/Pets/WindowXP"), 201, "Computer"),
                new Pet(Content.Load<Texture2D>("Images/Pets/Parroty"), 345, "Parroty"),
                new Pet(Content.Load<Texture2D>("Images/Pets/Fish"), 390, "Fishy"),
                new Pet(Content.Load<Texture2D>("Images/Pets/FlyingPiggy"), 150, "Flying Piggy"),
            };
            _numShopItems += _pets.Count;

            //PowerUps
            _powerups = new()
            {
                new Powerup("Fastest Man Alive","Increases your \nsprint speed by +2 \nand your walking \nspeed by +1", 150)
                {
                    SprintIncrease = 2,
                    SpeedIncrease = 1
                },
                new Powerup("Spike No More", "Cuts all \nSpikes in half", 170)
                {
                    SpikeRemoval = true,
                },
                new Powerup("Elevator Booster","Increases Elevator \nlift by +3",50)
                {
                    ElevatorBoost = 3f
                },
                new Powerup("Spring Shoes","Increases your \ninitial jump \nheight by +1", 160)
                {
                    JumpIncrease = 1
                },
                new Powerup("Revive (JSM)", "A Machine \nDeveloped by \nDarian to save \nJosiah when he \ndies too quickly", 250)
                {
                    JSM = true
                },
                new Powerup("Steel Shoes", "Increases Max \nGravity by +3", 100)
                {
                    GravityBoost = 3
                },
                new Powerup("GOD MODE", "Buffs all stats \nand shrinks spikes\nobtain a JSM", 1000)
                {
                    SpikeRemoval = true,
                    JSM = true,
                    SpeedIncrease = 1,
                    SprintIncrease = 2,
                    JumpIncrease = 1,
                    JumpTimeIncrease = 0.1f,
                    ElevatorBoost = 3f
                },
                new Powerup("Hell Debuff", "Reduces \nSprint speed by -2\nSpeed by -1\nElevator lift by -2", 5)
                {
                    ElevatorBoost = -2,
                    SpeedIncrease = -1,
                    SprintIncrease = -2
                }
            };
            _numShopItems += _powerups.Count;
            _powerupFont = Content.Load<SpriteFont>("Fonts/PowerupFont");

            //Music
            _gameMusic = new() { Content.Load<SoundEffect>("Sounds/GameMusicK").CreateInstance(), Content.Load<SoundEffect>("Sounds/GameMusicL").CreateInstance(), Content.Load<SoundEffect>("Sounds/GameMusic3").CreateInstance() };
            _deathSound = Content.Load<SoundEffect>("Sounds/Death").CreateInstance();

            //Background Pictures
            _deathBG = Content.Load<Texture2D>("Background Pictures/Death");
            _menuBG = Content.Load<Texture2D>("Background Pictures/Menu");
            _menuBG2 = Content.Load<Texture2D>("Background Pictures/MultiMenu");
            _shopBG = Content.Load<Texture2D>("Background Pictures/ShopBG");

            //Add Levels
            LevelCreator _levelCreator = new(rectangleTex, Content.Load<Texture2D>("Images/Door2"), Content.Load<Texture2D>("Images/GhostPlatform"), Content.Load<Texture2D>("Images/RedWalker"), Content.Load<Texture2D>("Images/RedWalkerDoor"), Content.Load<Texture2D>("Images/Spike"), Content.Load<Texture2D>("Images/Ghost"), Content.Load<Texture2D>("Images/Elevator"), Content.Load<SpriteFont>("Fonts/Small Font"))
            {
                MageSpell = Content.Load<Texture2D>("Images/MagicBolt"),
                MageTex = Content.Load<Texture2D>("Images/Mage"),
                ExitPortal1 = Content.Load<Texture2D>("Images/ExitPortalB"),
                ExitPortal2 = Content.Load<Texture2D>("Images/ExitPortalW"),
                SpaceSpike = Content.Load<Texture2D>("Images/SpaceSpike"),
            };
            Environment normalLand = new(null);
            Environment space = new(Content.Load<Texture2D>("Background Pictures/Space"))
            {
                MaxGravity = 3f
            };
            Environment desert = new(Content.Load<Texture2D>("Background Pictures/Desert"), Content.Load<Texture2D>("Images/WaterBottle"), new CollectionMeter(rectangleTex, Color.Blue, 100))
            {
                MaxGravity = 10f
            };
            _levels.Add(_levelCreator.Level0(normalLand));
            _levels.Add(_levelCreator.Level1(normalLand));
            _levels.Add(_levelCreator.Level2(normalLand));
            _levels.Add(_levelCreator.Level3(normalLand));
            _levels.Add(_levelCreator.DropLevel(normalLand));
            _levels.Add(_levelCreator.MazeOfRa(normalLand));
            _levels.Add(_levelCreator.Level6(normalLand));
            _levels.Add(_levelCreator.LuaLevel(space));
            _levels.Add(_levelCreator.Level8(space));
            _levels.Add(_levelCreator.Level9(space));
            _levels.Add(_levelCreator.Level10(space));
            _levels.Add(_levelCreator.Desert1(desert));
            foreach (Level l in _levels)
            {
                l.SetCoinDefaults(_coinTex);
            }
            _rectTex = rectangleTex;
        }

        protected override void Update(GameTime gameTime)
        {
            //Get User Inputs
            _prevMS = _mouseState;
            _mouseState = Mouse.GetState();
            _prevKB = _keyBoard;
            _keyBoard = Keyboard.GetState();
            if (screen == Screen.Game)
            {
                if (_soundOn)
                    _gameMusic[_cS].Play();
                if (!_p1Death)
                {
                    if (_skinInLevel != 0)
                        _levels[_cL].Update(gameTime, _player, _ninjaSkins[_skinInLevel], _keyBoard);
                    else
                        _levels[_cL].Update(gameTime, _player,_keyBoard);
                    if (_levels[_cL].PlayerCompleteLevel(_player))
                    {
                        _coins += _levels[_cL].CurrentCoins;
                        _currentPowerUp = -1;
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
                        SaveGame(_coins, _deathCounter, _ninjaSkins, _teacherMode, _equipedPet, _pets, _currentPowerUp, _bgColor);
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
                    if ((_settingsOpener.Clicked(_mouseState)) || (_keyBoard.IsKeyDown(Keys.Escape) && _prevKB.IsKeyUp(Keys.Escape)))
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
                        if (_currentPowerUp == -1)
                            _skinInLevel = _levels[_cL].SetDefaults(_player, _difficulty, _ninjaSkins, _cL);
                        else
                            _skinInLevel = _levels[_cL].SetDefaults(_player, _difficulty, _ninjaSkins, _cL, _powerups[_currentPowerUp]);
                    }
                }

            }
            else if (screen == Screen.Multiplayer)
            {
                if (_soundOn)
                    _gameMusic[_cS].Play();
                if (!_p1Death && !_p2Death && _cL == _cL2)
                {
                    _levels[_cL].Update(gameTime, _player, _player2, _keyBoard);
                    if (_levels[_cL].PlayerCompleteLevel(_player))
                    {
                        if (_levels.Count > _cL + 1)
                        {
                            _cL++;
                            _levels[_cL].SetDefaults(_player, _difficulty);
                        }
                        else
                            screen = Screen.P1Win;
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
                        else
                            screen = Screen.P2Win;
                    }
                    else if (_levels[_cL2].DidPlayerDie(_player2))
                    {
                        _p2Death = true;
                        _levels[_cL2].SetDefaults(_player2, _difficulty);
                    }
                }
                else if (!_p1Death && !_p2Death)
                {
                    _levels[_cL].Update(gameTime, _player, _keyBoard);
                    if (_levels[_cL].PlayerCompleteLevel(_player))
                    {
                        if (_levels.Count > _cL + 1)
                        {
                            _cL++;
                            _levels[_cL].SetDefaults(_player, _difficulty);
                        }
                        else
                            screen = Screen.P1Win;
                    }
                    else if (_levels[_cL].DidPlayerDie(_player))
                    {
                        _p1Death = true;
                        _levels[_cL].SetDefaults(_player, _difficulty);
                    }

                    _levels[_cL2].Update(gameTime, _player2, _keyBoard);
                    if (_levels[_cL2].PlayerCompleteLevel(_player2))
                    {
                        if (_levels.Count > _cL2 + 1)
                        {
                            _cL2++;
                            _levels[_cL2].SetDefaults(_player2, _difficulty);
                        }
                        else
                            screen = Screen.P2Win;
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
                    _levels[_cL2].Update(gameTime, _player2, _keyBoard);
                    if (_levels[_cL2].PlayerCompleteLevel(_player2))
                    {
                        if (_levels.Count > _cL2 + 1)
                        {
                            _cL2++;
                            _levels[_cL2].SetDefaults(_player2, _difficulty);
                        }
                        else
                            screen = Screen.P2Win;
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
                    _levels[_cL].Update(gameTime, _player, _keyBoard);
                    if (_levels[_cL].PlayerCompleteLevel(_player))
                    {
                        if (_levels.Count > _cL + 1)
                        {
                            _cL++;
                            _levels[_cL].SetDefaults(_player, _difficulty);
                        }
                        else
                            screen = Screen.P1Win;
                    }
                    else if (_levels[_cL].DidPlayerDie(_player))
                    {
                        _p1Death = true;
                        _levels[_cL].SetDefaults(_player, _difficulty);
                    }
                }
                _camera.Follow(_player);
                _camera2.Follow(_player2);
                if (_arrowButtons2[5].Update(_mouseState))
                {
                    _graphics.PreferredBackBufferWidth = 600;
                    _graphics.PreferredBackBufferHeight = 500;
                    _graphics.ApplyChanges();
                    screen = Screen.Menu;
                    SaveGame(_coins, _deathCounter, _ninjaSkins, _teacherMode, _equipedPet, _pets, _currentPowerUp, _bgColor);
                }
            }
            else if (screen == Screen.Menu)
            {
                if (_prevMS.LeftButton == ButtonState.Released)
                {
                    if (_arrowButtons[0].Update(_mouseState))
                    {
                        if (_currentSkin == 0)
                            _currentSkin = _ninjaSkins.Count - 1;
                        else
                            _currentSkin--;
                    }
                    else if (_arrowButtons[1].Update(_mouseState))
                    {
                        if (_currentSkin + 1 < _ninjaSkins.Count)
                            _currentSkin++;
                        else
                            _currentSkin = 0;
                    }
                    else if (_arrowButtons[2].Update(_mouseState))
                    {
                        if (_cL == 0)
                            _cL = _levels.Count - 1;
                        else
                            _cL--;
                    }
                    else if (_arrowButtons[3].Update(_mouseState))
                    {
                        if (_cL + 1 < _levels.Count)
                            _cL++;
                        else
                            _cL = 0;
                    }
                    else if (_arrowButtons[4].Update(_mouseState))
                    {
                        if (_cS == 0)
                            _cS = _gameMusic.Count - 1;
                        else
                            _cS--;
                    }
                    else if (_arrowButtons[5].Update(_mouseState))
                    {
                        if (_cS + 1 < _gameMusic.Count)
                            _cS++;
                        else
                            _cS = 0;
                    }
                    else if (_arrowButtons[6].Update(_mouseState))
                    {
                        if ((_difficulty == 1 && !_teacherMode) || _difficulty == 0)
                            _difficulty = 3;
                        else if (_difficulty == 1)
                            _difficulty = 0;
                        else
                            _difficulty--;
                    }
                    else if (_arrowButtons[7].Update(_mouseState))
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
                        if (_currentPowerUp == -1)
                            _skinInLevel = _levels[_cL].SetDefaults(_player, _difficulty, _ninjaSkins, _cL);
                        else
                            _skinInLevel = _levels[_cL].SetDefaults(_player, _difficulty, _ninjaSkins, _cL, _powerups[_currentPowerUp]);
                        if (!_ninjaSkins[_currentSkin].Locked)
                            _player.SetSkin(_ninjaSkins[_currentSkin].SkinTex);
                        else
                            _player.SetSkin(_ninjaSkins[0].SkinTex);
                        screen = Screen.Game;
                        _graphics.PreferredBackBufferWidth = 900;
                        _graphics.ApplyChanges();
                    }
                    else if (_arrowButtons[9].Update(_mouseState))
                        screen = Screen.MultiplayerMenu;
                    else if (_arrowButtons[10].Update(_mouseState))
                    {
                        screen = Screen.Shop;
                        if (_shopSelection < _shopSkins.Length)
                        {
                            _shopButtons[1].Visible = _ninjaSkins[_shopSkins[_shopSelection]].Locked;
                            _shopButtons[4].Visible = false;
                        }
                        else if (_shopSelection < _pets.Count + _shopSkins.Length)
                        {
                            if (_pets[_shopSelection - _shopSkins.Length].Locked)
                            {
                                _shopButtons[1].Visible = true;
                                _shopButtons[4].Visible = false;
                            }
                            else
                            {
                                _shopButtons[4].Visible = true;
                                _shopButtons[1].Visible = false;
                                _shopButtons[4].SwitchDisplay(_equipedPet == _shopSelection - _shopSkins.Length);
                            }
                        }
                        else if (_shopSelection < _pets.Count + _shopSkins.Length + _powerups.Count)
                        {
                            _shopButtons[4].Visible = false;
                            _shopButtons[1].Visible = _currentPowerUp == -1;
                        }
                        else
                        {
                            _shopButtons[1].Visible = true;
                        }
                    }
                    else if (_arrowButtons[11].Update(_mouseState))
                    {
                        _coins = 0;
                        _deathCounter = 0;
                        for (int i = 4; i < _ninjaSkins.Count; i++)
                        {
                            _ninjaSkins[i].LockSkin();
                        }
                        for (int i = 0; i < _pets.Count; i++)
                            _pets[i].LockPet();
                        _equipedPet = -1;
                        _player.Pet = null;
                        _currentPowerUp = -1;
                        SaveGame(_coins, _deathCounter, _ninjaSkins, _teacherMode, _equipedPet, _pets, _currentPowerUp, _bgColor);
                    }
                    else if (_arrowButtons[12].Update(_mouseState))
                    {
                        screen = Screen.MoreInfo;
                        _infoPage = 1;
                    }
                }
            }
            else if (screen == Screen.MultiplayerMenu)
            {
                if (_prevMS.LeftButton == ButtonState.Released)
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

                        _viewPort1.Width /= 2;
                        _viewPort2.Width /= 2;
                        _viewPort2.X = _viewPort1.Width;
                        screen = Screen.Multiplayer;
                        _player2.Pet = _player.Pet.Copy;
                        _player2.AutoSprint = _player.AutoSprint;
                    }
                    else if (_arrowButtons2[1].Update(_mouseState))
                    {
                        if (_currentSkin == 0)
                            _currentSkin = _ninjaSkins.Count - 1;
                        else
                            _currentSkin--;
                    }
                    else if (_arrowButtons2[2].Update(_mouseState))
                    {
                        if (_currentSkin + 1 < _ninjaSkins.Count)
                            _currentSkin++;
                        else
                            _currentSkin = 0;
                    }
                    else if (_arrowButtons2[3].Update(_mouseState))
                    {
                        if (_currentSkin2 == 0)
                            _currentSkin2 = _ninjaSkins.Count - 1;
                        else
                            _currentSkin2--;
                    }
                    else if (_arrowButtons2[4].Update(_mouseState))
                    {
                        if (_currentSkin2 + 1 < _ninjaSkins.Count)
                            _currentSkin2++;
                        else
                            _currentSkin2 = 0;
                    }
                    else if (_arrowButtons2[5].Update(_mouseState))
                    {
                        screen = Screen.Menu;
                        SaveGame(_coins, _deathCounter, _ninjaSkins, _teacherMode, _equipedPet, _pets, _currentPowerUp, _bgColor);
                    }

                }
            }
            else if (screen == Screen.Settings)
            {
                if (_prevMS.LeftButton == ButtonState.Released)
                {
                    if (_settingsButtons[0].Update(_mouseState))
                    {
                        if (Keyboard.GetState().GetPressedKeys().Length == 1)
                            _player.SetLeft(Keyboard.GetState().GetPressedKeys()[0]);
                    }
                    else if (_settingsButtons[1].Update(_mouseState))
                    {
                        if (Keyboard.GetState().GetPressedKeys().Length == 1)
                            _player.SetRight(Keyboard.GetState().GetPressedKeys()[0]);
                    }
                    else if (_settingsButtons[2].Update(_mouseState))
                    {
                        if (Keyboard.GetState().GetPressedKeys().Length == 1)
                            _player.SetJump(Keyboard.GetState().GetPressedKeys()[0]);
                    }
                    else if (_settingsButtons[3].Update(_mouseState))
                    {
                        if (Keyboard.GetState().GetPressedKeys().Length == 1)
                            _player.SetSprint(Keyboard.GetState().GetPressedKeys()[0]);
                    }
                    //Resume Game
                    else if (_settingsButtons[6].Update(_mouseState) || (_keyBoard.IsKeyDown(Keys.Escape) && _prevKB.IsKeyUp(Keys.Escape)))
                    {
                        _graphics.PreferredBackBufferWidth = 900;
                        _graphics.ApplyChanges();
                        screen = Screen.Game;
                    }
                    //Main Menu
                    else if (_settingsButtons[8].Update(_mouseState))
                    {
                        SaveGame(_coins, _deathCounter, _ninjaSkins, _teacherMode, _equipedPet, _pets, _currentPowerUp, _bgColor);
                        screen = Screen.Menu;
                    }
                    //Quit Game
                    else if (_settingsButtons[9].Update(_mouseState))
                    {
                        SaveGame(_coins, _deathCounter, _ninjaSkins, _teacherMode, _equipedPet, _pets, _currentPowerUp, _bgColor);
                        Exit();
                    }
                    //Sound
                    else if (_settingsButtons[5].Update(_mouseState))
                    {
                        _settingsButtons[5].SwitchDisplay();
                        _soundOn = !_soundOn;
                    }
                    //Restart Level
                    else if (_settingsButtons[7].Update(_mouseState))
                    {
                        if (_currentPowerUp == -1)
                            _skinInLevel = _levels[_cL].SetDefaults(_player, _difficulty, _ninjaSkins, _cL);
                        else
                            _skinInLevel = _levels[_cL].SetDefaults(_player, _difficulty, _ninjaSkins, _cL, _powerups[_currentPowerUp]);
                        _graphics.PreferredBackBufferWidth = 900;
                        _graphics.ApplyChanges();
                        screen = Screen.Game;
                    }
                    //Auto Sprint
                    else if (_settingsButtons[4].Update(_mouseState))
                    {
                        _player.AutoSprint = !_player.AutoSprint;
                        _settingsButtons[4].SwitchDisplay();
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
                    _camera.Follow(_player);
                    if (_deathCounter == 5)
                    {
                        _ninjaSkins[5].UnlockSkin();
                        _player.SetSkin(_ninjaSkins[5].SkinTex);
                        SaveGame(_coins, _deathCounter, _ninjaSkins, _teacherMode, _equipedPet, _pets, _currentPowerUp, _bgColor);
                    }
                    else if (_deathCounter == 15)
                    {
                        _ninjaSkins[6].UnlockSkin();
                        _player.SetSkin(_ninjaSkins[6].SkinTex);
                        SaveGame(_coins, _deathCounter, _ninjaSkins, _teacherMode, _equipedPet, _pets, _currentPowerUp, _bgColor);
                    }
                    else if (_deathCounter == 30)
                    {
                        _ninjaSkins[7].UnlockSkin();
                        _player.SetSkin(_ninjaSkins[7].SkinTex);
                        SaveGame(_coins, _deathCounter, _ninjaSkins, _teacherMode, _equipedPet, _pets, _currentPowerUp, _bgColor);
                    }
                    else if (_deathCounter == 50)
                    {
                        _ninjaSkins[17].UnlockSkin();
                        _player.SetSkin(_ninjaSkins[17].SkinTex);
                        SaveGame(_coins, _deathCounter, _ninjaSkins, _teacherMode, _equipedPet, _pets, _currentPowerUp, _bgColor);
                    }
                    else if (_deathCounter >= 100)
                    {
                        _ninjaSkins[14].UnlockSkin();
                        _player.SetSkin(_ninjaSkins[14].SkinTex);
                        SaveGame(_coins, _deathCounter, _ninjaSkins, _teacherMode, _equipedPet, _pets, _currentPowerUp, _bgColor);
                    }
                }
                else if (_keyBoard.IsKeyDown(Keys.K)&& _keyBoard.IsKeyDown(Keys.L))
                {
                    _playerRespawnTimer = 5;
                }
            }
            else if (screen == Screen.Shop)
            {
                if (_prevMS.LeftButton == ButtonState.Released)
                {
                    if (_shopButtons[0].Update(_mouseState))
                    {
                        if (_equipedPet == -1)
                            _player.Pet = null;
                        else
                            _player.Pet =(_pets[_equipedPet]);
                        SaveGame(_coins, _deathCounter, _ninjaSkins, _teacherMode, _equipedPet, _pets, _currentPowerUp, _bgColor);
                        screen = Screen.Menu;
                    }
                    else if (_shopButtons[1].Update(_mouseState))
                    {
                        if (_shopSelection < _shopSkins.Length)
                        {
                            if (_coins >= _ninjaSkins[_shopSkins[_shopSelection]].Price)
                            {
                                _coins -= _ninjaSkins[_shopSkins[_shopSelection]].Price;
                                _ninjaSkins[_shopSkins[_shopSelection]].UnlockSkin();
                                _shopButtons[1].Visible = false;
                            }
                        }
                        else if (_shopSelection < _pets.Count + _shopSkins.Length)
                        {
                            if (_coins >= _pets[_shopSelection - _shopSkins.Length].Price)
                            {
                                _coins -= _pets[_shopSelection - _shopSkins.Length].Price;
                                _pets[_shopSelection - _shopSkins.Length].UnlockPet();
                                _shopButtons[4].Visible = true;
                                _shopButtons[4].SwitchDisplay(false);
                                _shopButtons[1].Visible = false;
                            }
                        }
                        else if (_shopSelection < _pets.Count + _shopSkins.Length + _powerups.Count)
                        {
                            if (_coins >= _powerups[_shopSelection - _shopSkins.Length - _pets.Count].Price)
                            {
                                _currentPowerUp = _shopSelection - _shopSkins.Length - _pets.Count;
                                _shopButtons[1].Visible = false;
                                _coins -= _powerups[_shopSelection - _shopSkins.Length - _pets.Count].Price;
                            }
                        }
                        else
                        {
                            if (_coins >= 50 && _bgColor != (_shopSelection - _shopSkins.Length - _pets.Count - _powerups.Count))
                            {
                                _bgColor = _shopSelection - _shopSkins.Length - _pets.Count - _powerups.Count;
                                _coins -= 50;
                            }
                        }
                    }
                    else if (_shopButtons[2].Update(_mouseState))
                    {
                        if (_shopSelection == 0)
                            _shopSelection = _numShopItems;
                        else
                            _shopSelection--;
                        if (_shopSelection < _shopSkins.Length)
                        {
                            _shopButtons[1].Visible = _ninjaSkins[_shopSkins[_shopSelection]].Locked;
                            _shopButtons[4].Visible = false;
                        }
                        else if (_shopSelection < _pets.Count + _shopSkins.Length)
                        {
                            if (_pets[_shopSelection - _shopSkins.Length].Locked)
                            {
                                _shopButtons[1].Visible = true;
                                _shopButtons[4].Visible = false;
                            }
                            else
                            {
                                _shopButtons[4].Visible = true;
                                _shopButtons[1].Visible = false;
                                _shopButtons[4].SwitchDisplay(_equipedPet == _shopSelection - _shopSkins.Length);
                            }
                        }
                        else if (_shopSelection < _pets.Count + _shopSkins.Length + _powerups.Count)
                        {
                            _shopButtons[4].Visible = false;
                            _shopButtons[1].Visible = _currentPowerUp == -1;
                        }
                        else
                        {
                            _shopButtons[1].Visible = true;
                        }
                    }
                    else if (_shopButtons[3].Update(_mouseState))
                    {
                        if (_shopSelection == _numShopItems)
                            _shopSelection = 0;
                        else
                            _shopSelection++;
                        if (_shopSelection < _shopSkins.Length)
                        {
                            _shopButtons[1].Visible = _ninjaSkins[_shopSkins[_shopSelection]].Locked;
                            _shopButtons[4].Visible = false;
                        }
                        else if (_shopSelection < _pets.Count + _shopSkins.Length)
                        {
                            if (_pets[_shopSelection - _shopSkins.Length].Locked)
                            {
                                _shopButtons[1].Visible = true;
                                _shopButtons[4].Visible = false;
                            }
                            else
                            {
                                _shopButtons[4].Visible = true;
                                _shopButtons[1].Visible = false;
                                _shopButtons[4].SwitchDisplay(_equipedPet == _shopSelection - _shopSkins.Length);
                            }
                        }
                        else if (_shopSelection < _pets.Count + _shopSkins.Length + _powerups.Count)
                        {
                            _shopButtons[4].Visible = false;
                            _shopButtons[1].Visible = _currentPowerUp == -1;
                        }
                        else
                        {
                            _shopButtons[1].Visible = true;
                        }
                    }
                    else if (_shopButtons[4].Update(_mouseState))
                    {
                        if (_shopSelection - _shopSkins.Length == _equipedPet)
                        {
                            _equipedPet = -1;
                            _shopButtons[4].SwitchDisplay(false);
                        }
                        else
                        {
                            _equipedPet = _shopSelection - _shopSkins.Length;
                            _shopButtons[4].SwitchDisplay(true);
                        }
                    }
                }
            }
            else if (screen == Screen.MoreInfo)
            {
                if (_prevMS.LeftButton == ButtonState.Released)
                {
                    if (_infoButtons[0].Update(_mouseState))
                        screen = Screen.Menu;
                    else if (_infoButtons[1].Update(_mouseState) && _infoPage != 1)
                        _infoPage--;
                    else if (_infoButtons[2].Update(_mouseState) && _infoPage != 3)
                        _infoPage++;
                }
            }
            else if (screen == Screen.P1Win || screen == Screen.P2Win)
            {
                if (_graphics.PreferredBackBufferWidth != 600)
                {
                    _graphics.PreferredBackBufferWidth = 600;
                    _graphics.PreferredBackBufferHeight = 500;
                    _graphics.ApplyChanges();
                    SaveGame(_coins, _deathCounter, _ninjaSkins, _teacherMode, _equipedPet, _pets, _currentPowerUp, _bgColor);
                }
                if (_arrowButtons2[5].Update(_mouseState))
                    screen = Screen.Menu;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (screen == Screen.Game)
            {
                GraphicsDevice.Clear(_colors[_bgColor]);
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
                _spriteBatch.Draw(_rectTex, new Rectangle(0, 0, 160, 130), Color.White*0.7f);
                _spriteBatch.Draw(_coinTex, new Rectangle(10, 10, 30, 30), Color.White);
                _spriteBatch.DrawString(_ninjaFont, $"= {_levels[_cL].CurrentCoins}/{_levels[_cL].TotalCoins}", new Vector2(42, 10), Color.Black);
                _spriteBatch.DrawString(_ninjaFont, $"Level: {_cL}\nDeaths: {_deathCounter}", new Vector2(10, 56), Color.Black);
                _spriteBatch.End();
            }
            else if (screen == Screen.Shop)
            {
                GraphicsDevice.Clear(Color.Coral);
                _spriteBatch.Begin();
                _spriteBatch.Draw(_shopBG, new Vector2(0, 0), Color.White);
                _spriteBatch.Draw(_coinTex, new Rectangle(10, 10, 30, 30), Color.White);
                _spriteBatch.DrawString(_ninjaFont, $"= {_coins}", new Vector2(42, 10), Color.Black);
                if (_shopSelection < _shopSkins.Length)
                {
                    _spriteBatch.DrawString(_ninjaFont, $"Price: {_ninjaSkins[_shopSkins[_shopSelection]].Price} Coins", new Vector2(200, 100), Color.Black);
                    _spriteBatch.Draw(_ninjaSkins[_shopSkins[_shopSelection]].SkinTex, new Rectangle(260, 200, 90, 140), new Rectangle(31, 14, 38, 72), Color.White);
                }
                else if (_shopSelection < _shopSkins.Length + _pets.Count)
                {
                    _spriteBatch.DrawString(_ninjaFont, $"Price: {_pets[_shopSelection - _shopSkins.Length].Price} Coins", new Vector2(200, 100), Color.Black);
                    _pets[_shopSelection - _shopSkins.Length].DrawDisplay(_spriteBatch);
                }
                else if (_shopSelection < _shopSkins.Length + _pets.Count + _powerups.Count)
                {
                    _spriteBatch.DrawString(_ninjaFont, $"Price: {_powerups[_shopSelection - _shopSkins.Length - _pets.Count].Price} Coins", new Vector2(200, 100), Color.Black);
                    _spriteBatch.DrawString(_powerupFont, _powerups[_shopSelection - _shopSkins.Length - _pets.Count].Name, new Vector2(210, 160), Color.Black);
                    _spriteBatch.DrawString(_powerupFont, _powerups[_shopSelection - _shopSkins.Length - _pets.Count].Description, new Vector2(210, 200), Color.Black);
                }
                else
                {
                    _spriteBatch.DrawString(_ninjaFont, "BG Color Price: 50 Coins", new Vector2(170, 100), Color.Black);
                    _spriteBatch.Draw(_rectTex, new Rectangle(210, 160, 180, 180), _colors[_shopSelection - _shopSkins.Length - _pets.Count - _powerups.Count]);
                }
                for (int i = 0; i < (_shopButtons.Length); i++)
                    _shopButtons[i].Draw(_spriteBatch);
                _spriteBatch.End();
            }
            else if (screen == Screen.Menu)
            {
                GraphicsDevice.Clear(Color.White);
                _spriteBatch.Begin();
                _spriteBatch.Draw(_menuBG, new Vector2(0, 0), Color.White);
                for (int i = 0; i < _arrowButtons.Length; i++)
                {
                    _arrowButtons[i].Draw(_spriteBatch);
                }
                _spriteBatch.Draw(_ninjaSkins[_currentSkin].SkinTex, _skinRectangle, new Rectangle(31, 14, 38, 72), Color.White);
                if (_ninjaSkins[_currentSkin].Locked)
                    _spriteBatch.Draw(_lockTex, _skinRectangle, Color.White * 0.8f);
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
                GraphicsDevice.Clear(_colors[_bgColor]);
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
            else if (screen == Screen.MoreInfo)
            {
                GraphicsDevice.Clear(Color.Aqua);
                _spriteBatch.Begin();
                switch (_infoPage)
                {
                    case 1:
                        _spriteBatch.DrawString(_ninjaFont, "Skins:", new Vector2(10, 10), Color.Black);
                        _spriteBatch.DrawString(_powerupFont, "There are three ways to unlock Skins. First, you can buy \nsome from the shop. Second, you can collect Skin Tokens from \nLevels while playing Hard difficulty. Third, you can Die a lot.", new Vector2(10, 55), Color.Black);
                        _spriteBatch.DrawString(_ninjaFont, "Coins:", new Vector2(10, 150), Color.Black);
                        _spriteBatch.DrawString(_powerupFont, "Coins can be collected in any level, however they only count \nif you complete the level. Temporary coins will get reset \nif you die.", new Vector2(10, 195), Color.Black);
                        _spriteBatch.DrawString(_ninjaFont, "Power-Ups:", new Vector2(10, 290), Color.Black);
                        _spriteBatch.DrawString(_powerupFont, "You can purchase Power-Ups from the Shop. Only a single \npower-up can be active at a time and they last until you \ncomplete a single Level.", new Vector2(10, 335), Color.Black);
                        break;
                    case 2:
                        _spriteBatch.DrawString(_ninjaFont, "Auto Sprint", new Vector2(10, 10), Color.Black);
                        _spriteBatch.DrawString(_powerupFont, "Auto Sprint can be toggled on/off in settings during \nsingle player. When toggled on you will gradually gain speed \nwhile walking on the ground without stopping. Pressing the \nsprint key while auto sprint is on will not do anything. \nAuto sprint is able to go slightly faster than normal \nsprint if you gain enough speed.", new Vector2(10, 55), Color.Black);
                        _spriteBatch.DrawString(_ninjaFont, "Multiplayer:", new Vector2(10, 220), Color.Black);
                        _spriteBatch.DrawString(_powerupFont, "Multiplayer supports 2 players. It functions the same as the \nrest of the game except for a few changes. Targetting \nEnemies will target whichever player is closer. Power Ups \ncan not be used. Coins and skin tokens can not be collected. \nAuto Sprint must be toggled beforehand in single \nplayer settings and will affect both players", new Vector2(10, 265), Color.Black);
                        break;
                    case 3:
                        _spriteBatch.DrawString(_ninjaFont, "Credits:", new Vector2(10, 10), Color.Black);
                        _spriteBatch.DrawString(_powerupFont, "Devlopers: Kian Dufraimont (Doom), Louis Rouhani (Lou)\r\n\nCoding: Kian Dufraimont\r\n\nSprite Art: Kian Dufraimont (Skins, Mages, Pets) & \nLouis Rouhani (RedWalker, Spikes, GhostPlatforms)\r\n\nSoundtracks: Kian Dufraimont (Music Tracks 1,3) & \nLouis Rouhani (Music Track 2)\r\n\nLevel Designs: Kian Dufraimont", new Vector2(10, 55), Color.Black);
                        break;

                }
                foreach (Button b in _infoButtons)
                    b.Draw(_spriteBatch);
                _spriteBatch.End();
            }
            else if (screen == Screen.P1Win)
            {
                GraphicsDevice.Clear(Color.Aqua);
                _spriteBatch.Begin();
                _arrowButtons2[5].Draw(_spriteBatch);
                _spriteBatch.DrawString(_ninjaFont, "  Congrats!!!\nPlayer 1 Wins", new Vector2(200, 200), Color.Black);
                _spriteBatch.End();
            }
            else if (screen == Screen.P2Win)
            {
                GraphicsDevice.Clear(Color.Aqua);
                _spriteBatch.Begin();
                _arrowButtons2[5].Draw(_spriteBatch);
                _spriteBatch.DrawString(_ninjaFont, "  Congrats!!!\nPlayer 2 Wins", new Vector2(200, 200), Color.Black);
                _spriteBatch.End();
            }

            base.Draw(gameTime);
        }
        public static void SaveGame(int coins, int death, List<Skin> ninjaSkins, bool teacherMode, int equippedPet, List<Pet> pets, int currentPU, int bgColor)
        {
            StreamWriter save = new ("Save.txt");
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
            save.WriteLine(equippedPet);
            for (int i = 0; i < pets.Count; i++)
            {
                if (pets[i].Locked)
                {
                    save.Write(0);
                }
                else
                    save.Write(1);
            }
            save.WriteLine();
            save.WriteLine(currentPU);
            save.WriteLine(bgColor);
            save.Close();
        }
    }
}