using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

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

        //Skin Variables
        private List<Skin> _ninjaSkins;
        private int _deathCounter, _skinInLevel;

        //Multiplayer Variables
        private Player _player2;
        private float _playerRespawnTimer2;
        private bool _p1Death, _p2Death;
        private int _cL2, _currentSkin2;
        private Camera _camera2;
        private Viewport _viewPort1, _viewPort2;

        //Menu Variables
        private Button[] _arrowButtons;
        private SpriteFont _ninjaFont;
        private List<Vector2> _menuPositions;
        private int _currentSkin;
        private Rectangle _skinRectangle;
        private int _difficulty;

        //Settings
        private Sprite _settingsOpener;
        private Button[] _settingsButtons;

        //Music
        private bool _soundOn;
        private List<SoundEffectInstance> _gameMusic;
        private SoundEffectInstance _deathSound;
        private int _cS;

        //Background Images
        private Texture2D _deathBG;
        private Texture2D _menuBG;
        private Screen screen;
        private enum Screen
        {
            Menu,
            Multiplayer,
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

            _cL = 1;
            _currentSkin = 0;
            _currentSkin2 = 4;
            _deathCounter = 0;
            _difficulty = 2;
            _cS = 0;
            _soundOn = false;
            _levels = new List<Level>();
            screen = Screen.Menu;
            _camera = new Camera();
            _camera2 = new Camera();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D rectangleTex = Content.Load<Texture2D>("Images/Rectangle");
            SpriteFont font = Content.Load<SpriteFont>("Fonts/Font");

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
                //Skin 6 Aang
                new Skin(Content.Load<Texture2D>("Images/NinjaSkins/Aang"), Content.Load<Texture2D>("Images/SkinIcons/Aang"), new Rectangle(1760, -2520, 40, 40), 1),
                //Skin 7 Reaper
                new Skin(Content.Load<Texture2D>("Images/NinjaSkins/Reaper"), true),
                //Skin 8 Mario
                new Skin(Content.Load<Texture2D>("Images/NinjaSkins/Mario"), Content.Load<Texture2D>("Images/SkinIcons/Mario"), new Rectangle(3560, -460, 40, 40), 3)
            };


            //Menu Content
            _menuPositions = new() { new Vector2(70, 305), new Vector2(70, 365), new Vector2(70, 425) };
            _skinRectangle = new Rectangle(426, 310, 80, 140);
            _ninjaFont = font;
            _arrowButtons = new Button[10]
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
                new Button(rectangleTex, font, new Rectangle(350,115,170,40), "Multiplayer", Color.DarkGreen)
            };
            _menuBG = Content.Load<Texture2D>("Background Pictures/Menu");



            //Settings
            _settingsOpener = new Sprite(Content.Load<Texture2D>("Images/Gear"));
            _settingsOpener.Position = new Vector2(560, 10);
            _settingsButtons = new Button[8];
            string[] st = new string[8] { "Set Left Key", "Set Right Key", "Set Jump Key", "Set Sprint Key", "Resume Game", "Main Menu", "Quit Game", "Sound: On" };
            int num = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 3; j++)
                {
                    _settingsButtons[num] = new Button(rectangleTex, font, new Rectangle((240 * j) - 170, (i * 65 + 90), 230, 45), st[num]);
                    num++;
                }
            }
            _settingsButtons[7].AddSecondary("Sound: Off", true);

            //Music
            _gameMusic = new() { Content.Load<SoundEffect>("Sounds/GameMusicL").CreateInstance(), Content.Load<SoundEffect>("Sounds/GameMusicK").CreateInstance(), Content.Load<SoundEffect>("Sounds/GameMusic3").CreateInstance() };
            _deathSound = Content.Load<SoundEffect>("Sounds/Death").CreateInstance();

            //Background Pictures
            _deathBG = Content.Load<Texture2D>("Background Pictures/Death");

            LevelCreator levelCreator = new LevelCreator();
            _levels.Add(levelCreator.Level0(rectangleTex, Content.Load<Texture2D>("Images/Door2"), Content.Load<Texture2D>("Images/GhostPlatform"), Content.Load<Texture2D>("Images/RedWalker"), Content.Load<Texture2D>("Images/RedWalkerDoor"), Content.Load<Texture2D>("Images/Spike"), Content.Load<Texture2D>("Images/ExitPortalW"), Content.Load<SpriteFont>("Fonts/Small Font")));
            _levels.Add(levelCreator.Level1(rectangleTex, Content.Load<Texture2D>("Images/Door2"), Content.Load<Texture2D>("Images/GhostPlatform"), Content.Load<Texture2D>("Images/ExitPortalW"), Content.Load<SpriteFont>("Fonts/Small Font")));
            _levels.Add(levelCreator.Level2(rectangleTex, Content.Load<Texture2D>("Images/Door2"), Content.Load<Texture2D>("Images/GhostPlatform"), Content.Load<Texture2D>("Images/RedWalker"), Content.Load<Texture2D>("Images/RedWalkerDoor"), Content.Load<Texture2D>("Images/Spike"), Content.Load<Texture2D>("Images/ExitPortalW")));
            _levels.Add(levelCreator.Level3(rectangleTex, Content.Load<Texture2D>("Images/Door2"), Content.Load<Texture2D>("Images/GhostPlatform"), Content.Load<Texture2D>("Images/RedWalker"), Content.Load<Texture2D>("Images/RedWalkerDoor"), Content.Load<Texture2D>("Images/Spike"), Content.Load<Texture2D>("Images/ExitPortalW"), Content.Load<SpriteFont>("Fonts/Small Font")));

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
                if (_skinInLevel != 0)
                    _levels[_cL].Update(gameTime, _player, _ninjaSkins[_skinInLevel]);
                else
                    _levels[_cL].Update(gameTime, _player);
                if (_levels[_cL].PlayerCompleteLevel(_player))
                {
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
                        screen = Screen.Menu;
                        _gameMusic[_cS].Stop();
                    }
                }
                //Checks Death
                else if (_difficulty != 0)
                {
                    if (_levels[_cL].DidPlayerDie(_player))
                    {
                        _gameMusic[_cS].Stop();
                        screen = Screen.Death;
                        _skinInLevel = _levels[_cL].SetDefaults(_player, _difficulty, _ninjaSkins, _cL);
                    }
                }
                _camera.Follow(_player);
                if (_settingsOpener.Rectangle.Contains(_mouseState.X, _mouseState.Y) && _mouseState.LeftButton == ButtonState.Pressed)
                {
                    _gameMusic[_cS].Pause();
                    screen = Screen.Settings;
                }
            }
            else if (screen == Screen.Multiplayer)
            {
                if (_soundOn)
                    _gameMusic[_cS].Play();
                if (_p1Death)
                {
                    _playerRespawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (_playerRespawnTimer > 5)
                    {
                        _p1Death = false;
                        _playerRespawnTimer = 0;
                    }
                }
                else
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
                }
                if (_p2Death)
                {
                    _playerRespawnTimer2 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (_playerRespawnTimer2 > 5)
                    {
                        _p2Death = false;
                        _playerRespawnTimer2 = 0;
                    }
                }
                else
                {
                    if (_cL != _cL2 || _p1Death)
                    {
                        _levels[_cL2].Update(gameTime, _player2);
                    }
                    else
                    {
                        _levels[_cL2].Update2(gameTime, _player2);
                    }
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
                _camera.Follow(_player);
                _camera2.Follow(_player2);
            }
            else if (screen == Screen.Menu)
            {

                this.Window.Title = $"Mouse X {_mouseState.X}, MouseY {_mouseState.Y}";

                if (_mouseState.LeftButton == ButtonState.Pressed && _prevMS.LeftButton == ButtonState.Released)
                {
                    if (_arrowButtons[0].Clicked(_mouseState))
                    {
                        if (_currentSkin == 0)
                            _currentSkin = _ninjaSkins.Count - 1;
                        else
                            _currentSkin--;
                        while (_ninjaSkins[_currentSkin].Locked)
                        {
                            if (_currentSkin == 0)
                                _currentSkin = _ninjaSkins.Count - 1;
                            else
                                _currentSkin--;
                        }
                    }
                    else if (_arrowButtons[1].Clicked(_mouseState))
                    {
                        if (_currentSkin + 1 < _ninjaSkins.Count)
                            _currentSkin++;
                        else
                            _currentSkin = 0;
                        while (_ninjaSkins[_currentSkin].Locked)
                        {
                            if (_currentSkin + 1 < _ninjaSkins.Count)
                                _currentSkin++;
                            else
                                _currentSkin = 0;
                        }
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
                        if (_difficulty == 0)
                            _difficulty = 3;
                        else
                            _difficulty--;
                    }
                    else if (_arrowButtons[7].Clicked(_mouseState))
                    {
                        if (_difficulty == 3)
                            _difficulty = 0;
                        else
                            _difficulty++;
                    }
                    else if (_arrowButtons[8].Clicked(_mouseState))
                    {
                        _skinInLevel = _levels[_cL].SetDefaults(_player, _difficulty, _ninjaSkins, _cL);
                        _player.SetSkin(_ninjaSkins[_currentSkin].SkinTex);
                        screen = Screen.Game;
                    }
                    else if (_arrowButtons[9].Clicked(_mouseState))
                    {
                        _graphics.PreferredBackBufferWidth = 1200;
                        _graphics.PreferredBackBufferHeight = 500;
                        _graphics.ApplyChanges();

                        _cL2 = _cL;
                        _levels[_cL].SetDefaults(_player, _difficulty);
                        _player.SetSkin(_ninjaSkins[_currentSkin].SkinTex);
                        _levels[_cL2].SetDefaults(_player2, _difficulty);
                        _player2.SetSkin(_ninjaSkins[_currentSkin2].SkinTex);

                        _player.SetKeys(Keys.A, Keys.D, Keys.W, Keys.S);
                        _player2.SetKeys(Keys.Left, Keys.Right, Keys.Up, Keys.Down);

                        Viewport defaultView = GraphicsDevice.Viewport;
                        _viewPort1 = defaultView;
                        _viewPort2 = defaultView;

                        _viewPort1.Width = _viewPort1.Width / 2;
                        _viewPort2.Width = _viewPort2.Width / 2;
                        _viewPort2.X = _viewPort1.Width;

                        screen = Screen.Multiplayer;
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
                        screen = Screen.Game;
                    else if (_settingsButtons[5].Clicked(_mouseState))
                        screen = Screen.Menu;
                    else if (_settingsButtons[6].Clicked(_mouseState))
                        Exit();
                    else if (_settingsButtons[7].Clicked(_mouseState))
                    {
                        _settingsButtons[7].SwitchDisplay();
                        _soundOn = !_soundOn;
                    }
                }
            }
            else if (screen == Screen.Death)
            {
                if (_soundOn)
                    _deathSound.Play();
                _playerRespawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_playerRespawnTimer > 3)
                {
                    _deathCounter++;
                    screen = Screen.Game;
                    _playerRespawnTimer = 0;
                    _deathSound.Stop();
                    if (_deathCounter == 5)
                    {
                        _ninjaSkins[5].UnlockSkin();
                        _player.SetSkin(_ninjaSkins[5].SkinTex);
                    }
                    else if (_deathCounter == 10)
                    {
                        _ninjaSkins[7].UnlockSkin();
                        _player.SetSkin(_ninjaSkins[7].SkinTex);
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
                if (_skinInLevel != 0)
                    _levels[_cL].Draw(_spriteBatch, _player, _ninjaSkins[_skinInLevel]);
                else
                    _levels[_cL].Draw(_spriteBatch, _player);
                _spriteBatch.End();
                _spriteBatch.Begin();
                _settingsOpener.Draw(_spriteBatch);
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
            }

            base.Draw(gameTime);
        }
    }
}