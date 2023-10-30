using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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
        private List<Texture2D> _ninjaSkins;

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

            _cL = 0;
            _cL2 = 0;
            _currentSkin = 0;
            _currentSkin2 = 0;
            _difficulty = 2;
            _cS = 0;
            _soundOn = true;
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

            _ninjaSkins = new List<Texture2D>() { Content.Load<Texture2D>("Images/NinjaSkins/NinjaDarkBlue"), Content.Load<Texture2D>("Images/NinjaSkins/NinjaB"), Content.Load<Texture2D>("Images/NinjaSkins/NinjaW"), Content.Load<Texture2D>("Images/NinjaSkins/NinjaPink"), Content.Load<Texture2D>("Images/NinjaSkins/Jester"), Content.Load<Texture2D>("Images/NinjaSkins/Aang") };

            //Menu Content
            _menuPositions = new() { new Vector2(70,305), new Vector2(70,365), new Vector2(70,425)};
            _skinRectangle = new Rectangle(426,310,80,140);
            _ninjaFont = font;
            _arrowButtons = new Button[10];
            //Change Skin
            _arrowButtons[0] = new Button(Content.Load<Texture2D>("Images/ArrowLeft"), new Rectangle(360, 360, 30, 40));
            _arrowButtons[1] = new Button(Content.Load<Texture2D>("Images/ArrowRight"), new Rectangle(540, 360, 30, 40));
            //Change Level
            _arrowButtons[2] = new Button(Content.Load<Texture2D>("Images/ArrowLeft"), new Rectangle(20, 300, 30, 40));
            _arrowButtons[3] = new Button(Content.Load<Texture2D>("Images/ArrowRight"), new Rectangle(220, 300, 30, 40));
            //Change Music
            _arrowButtons[4] = new Button(Content.Load<Texture2D>("Images/ArrowLeft"), new Rectangle(20, 360, 30, 40));
            _arrowButtons[5] = new Button(Content.Load<Texture2D>("Images/ArrowRight"), new Rectangle(220, 360, 30, 40));
            //Change Difficulty
            _arrowButtons[6] = new Button(Content.Load<Texture2D>("Images/ArrowLeft"), new Rectangle(20, 420, 30, 40));
            _arrowButtons[7] = new Button(Content.Load<Texture2D>("Images/ArrowRight"), new Rectangle(220, 420, 30, 40));
            //Multiplayer
            _arrowButtons[9] = new Button(rectangleTex, font, new Rectangle(350,115,170,40), "Multiplayer", Color.DarkGreen);

            //Play Game
            _arrowButtons[8] = new Button(Content.Load<Texture2D>("Images/Rectangle"), new Rectangle(95, 180, 100, 100), Color.White*0 );
            _menuBG = Content.Load<Texture2D>("Background Pictures/Menu");

            //Settings
            _settingsOpener = new Sprite(Content.Load<Texture2D>("Images/Gear"));
            _settingsOpener.Position = new Vector2(560, 10);
            _settingsButtons = new Button[8];
            string[] st = new string[8] { "Set Left Key", "Set Right Key", "Set Jump Key", "Set Sprint Key", "Resume Game", "Main Menu","Quit Game","Sound: On" };
            int num = 0;
            for (int i = 0; i< 4; i++)
            {
                for (int j = 1; j < 3; j++)
                {
                    _settingsButtons[num] = new Button(rectangleTex, font, new Rectangle((240*j)-170, (i * 65 + 90), 230, 45), st[num]);
                    num++;
                }
            }
            _settingsButtons[7].AddSecondary("Sound: Off", false);

            //Music
            _gameMusic = new() { Content.Load<SoundEffect>("Sounds/GameMusicL").CreateInstance(), Content.Load<SoundEffect>("Sounds/GameMusicK").CreateInstance(), Content.Load<SoundEffect>("Sounds/GameMusic3").CreateInstance() };
            _deathSound = Content.Load<SoundEffect>("Sounds/Death").CreateInstance();

            //Background Pictures
            _deathBG = Content.Load<Texture2D>("Background Pictures/Death");

            //Level 1 Content
            List<Platform> tempPlatforms = new List<Platform>();
            List<Portal> tempPortals = new List<Portal>();
            Texture2D portalTex = Content.Load<Texture2D>("Images/Door");
            Texture2D ghostPlat = Content.Load<Texture2D>("Images/GhostPlatform");
            //Four Borders
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(0, -200, 3800, 200), Color.DarkGray));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(0, -2800, 200, 2800), Color.DarkGray));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(0, -3000, 3800, 200), Color.DarkGray));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(3600, -2800, 200, 2800), Color.DarkGray));
            //First Area
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(360, -440, 40, 120), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(360, -320, 240, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(ghostPlat, new Rectangle(650, -320, 110, 40), Color.White,0.5f));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(440, -440, 280, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(560, -640, 40, 200), Color.Green, 0f, true,true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(720, -440, 40, 120), Color.Yellow));

            tempPortals.Add(new Portal(portalTex, new Rectangle(680,-520,50,80), new Rectangle(1000,-720,50,80)));

            //Second Area
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(960, -640, 120, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(ghostPlat, new Rectangle(1080, -840, 120, 240), Color.White,0.5f));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1200, -640, 40, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1240, -640, 40, 40), Color.Green, 0.5f, true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1280, -640, 40, 40), Color.Green,0.5f,true, true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1320, -640, 40, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1360, -640, 320, 40), Color.Green,0,true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1680, -640, 120, 40), Color.Yellow));

            tempPortals.Add(new Portal(portalTex, new Rectangle(1720, -720, 50, 80), new Rectangle(3360, -400, 50, 80)));

            //Third Area
            tempPlatforms.Add(new Platform(ghostPlat, new Rectangle(2680, -320, 240, 40), Color.White, 0.5f));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(2920, -320, 120, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(3040, -300, 280, 20), Color.Red, 0.1f));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(3040, -320, 280, 40), Color.Green, 0.86f, true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(3320, -320, 120, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(ghostPlat, new Rectangle(3440, -320, 160, 40), Color.White, 0.5f));

            tempPortals.Add(new Portal(portalTex, new Rectangle(2960, -400, 50, 80), new Rectangle(320, -2680, 50,80)));

            //Final Area
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(280, -2600, 160, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(440, -2600, 360, 40), Color.Green, 0.1f, true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(800, -2600, 40, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(840, -2600, 360, 40), Color.Green, 0.7f, true,true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1200, -2600, 40, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1240, -2600, 360, 40), Color.Green, 0.3f, true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1600, -2600, 120, 40), Color.Yellow));

            _levels.Add(new Level(tempPlatforms,tempPortals));
            //Level 1 Signs/Hints
            _levels[0].SetFont(font);
            _levels[0].AddSign(new Vector2(620,-600), "Jump To Enter\n The Portal");
            _levels[0].AddSign(new Vector2(800, -880), "Red Ghost Platforms \nStop Your Jumps\nAnd Make You Fall");
            _levels[0].AddSign(new Vector2(230, -2760), "Hold Sprint to move Faster \n  when walking on Ground");
            _levels[0].SetExit(Content.Load <Texture2D>("Images/ExitPortalW"), new Rectangle(1600, -2720, 120, 120));

            //Level 2
            tempPlatforms = new();
            tempPortals = new();
            List<Platform> tempSpikes = new();
            Texture2D redWalker = Content.Load<Texture2D>("Images/RedWalker");
            Rectangle[] redWalkerSourceRects = new Rectangle[6] { new Rectangle(22, 8, 56, 83), new Rectangle(122, 8, 56, 83), new Rectangle(222, 8, 56, 83), new Rectangle(22, 108, 56, 83), new Rectangle(122, 108, 56, 83), new Rectangle(222, 108, 56, 83) };
            List<RedWalker> tempRWalkers = new List<RedWalker>();
            Texture2D rWalkerDoorTex = Content.Load<Texture2D>("Images/RedWalkerDoor");
            Texture2D spikeTex = Content.Load<Texture2D>("Images/Spike");
            portalTex = Content.Load<Texture2D>("Images/Door2");

            //Four Borders
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(0, -200, 3800, 200), Color.DarkGray));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(0, -2800, 200, 2800), Color.DarkGray));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(0, -3000, 3800, 200), Color.DarkGray));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(3600, -2800, 200, 2800), Color.DarkGray));

            //First Area
            tempPlatforms.Add(new Platform(ghostPlat, new Rectangle(200, -280, 600, 80), Color.White, 0.5f));
            tempRWalkers.Add(new RedWalker(redWalker, redWalkerSourceRects, new Rectangle(900, -280, 60, 80), 800, 1040, rWalkerDoorTex));
            tempPlatforms.Add(new Platform(ghostPlat, new Rectangle(920, -400, 40, 80), Color.White, 0.5f));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(920, -320, 40, 40), Color.Yellow));
            tempRWalkers.Add(new RedWalker(redWalker, redWalkerSourceRects, new Rectangle(1200, -280, 60, 80), 1160, 1400, rWalkerDoorTex));
            tempPlatforms.Add(new Platform(ghostPlat, new Rectangle(1280, -400, 40, 80), Color.White, 0.5f));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1280, -320, 40, 40), Color.Yellow));
            tempPortals.Add(new Portal(portalTex, new Rectangle(1520, -280, 50, 80), new Rectangle(280, -800, 50, 80)));
            tempPortals.Add(new Portal(portalTex, new Rectangle(1600, -280, 50, 80), new Rectangle(280, -800, 50, 80)));
            tempPortals.Add(new Portal(portalTex, new Rectangle(1680, -280, 50, 80), new Rectangle(280, -800, 50, 80)));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(1880, -220, 20, 20), Color.White));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(2040, -220, 20, 20), Color.White));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(2280, -220, 20, 20), Color.White));
            //True Portal is 4th
            tempPortals.Add(new Portal(portalTex, new Rectangle(2400, -280, 50,80), new Rectangle(1840, -480,50,80)));

            tempSpikes.Add(new Platform(spikeTex, new Rectangle(2560, -220, 20, 20), Color.White));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(2920, -220, 20, 20), Color.White));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(3400, -220, 20, 20), Color.White));
            tempPortals.Add(new Portal(portalTex, new Rectangle(3550, -280, 50, 80), new Rectangle(280, -800, 50, 80)));

            //Second Area
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1760, -400, 240, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(2080, -400, 120, 40), Color.Green, 0.5f, true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(2280, -400, 300, 40), Color.Yellow));
            tempRWalkers.Add(new RedWalker(redWalker, redWalkerSourceRects, new Rectangle(2280, -480, 60, 80), 2280, 2520, rWalkerDoorTex));
            tempPlatforms.Add(new Platform(ghostPlat, new Rectangle(2400, -600, 40, 80), Color.White, 0.5f));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(2400, -520, 40, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(2680, -400, 840, 40), Color.Yellow));
            tempRWalkers.Add(new RedWalker(redWalker, redWalkerSourceRects, new Rectangle(3000, -480, 60, 80), 2680, 3400, rWalkerDoorTex, true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(3080, -520,40,40), Color.Yellow));

            //Third Area
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(3520, -520, 80, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(3480, -640, 120, 40), Color.Green, 0.1f, true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(2680, -640, 800, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1640, -680, 1040, 40), Color.Yellow));
            tempRWalkers.Add(new RedWalker(redWalker, redWalkerSourceRects, new Rectangle(2400, -760, 60, 80), 2240, 2620, rWalkerDoorTex));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(2440, -800, 40, 40), Color.Yellow));
            tempPlatforms.Add(new Platform(ghostPlat, new Rectangle(2440, -880, 40, 80), Color.White, 0.5f));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(2080, -700, 20, 20), Color.White));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(1960, -700, 20, 20), Color.White));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(1800, -700, 20, 20), Color.White));
            tempSpikes.Add(new Platform(spikeTex, new Rectangle(1640, -700, 20, 20), Color.White));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1480, -680, 80, 40), Color.Green, 0.6f, true,true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1080, -680, 160, 40), Color.Yellow));

            tempPortals.Add(new Portal(portalTex, new Rectangle(1120, -760, 50, 80), new Rectangle(400, -920, 50, 80)));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(240, -840, 240, 40), Color.Yellow));

            _levels.Add(new Level(tempPlatforms, tempPortals,tempRWalkers,tempSpikes));
            _levels[1].SetFont(font);
            _levels[1].AddSign(new Vector2(400, -500), "Beware of the Red Walkers \nThey kill anything they touch \nwhen walking between their \ndesignated squares");
            _levels[1].AddSign(new Vector2(1440, -400), "Pick a door any door\n4 are fake 1 is real");
            _levels[1].AddSign(new Vector2(1800, -330), "Watch out for \n  Spikes Ahead");
            _levels[1].SetExit(Content.Load<Texture2D>("Images/ExitPortalW"), new Rectangle(240, -960, 120, 120));            
        }

        protected override void Update(GameTime gameTime)
        {
            //Get User Inputs
            _prevMS = _mouseState;
            _mouseState = Mouse.GetState();
            _keyBoard = Keyboard.GetState();
            
            if (screen == Screen.Game){
                if (_soundOn)
                    _gameMusic[_cS].Play();
                _levels[_cL].Update(gameTime, _player);
                if (_levels[_cL].PlayerCompleteLevel(_player))
                {
                    if (_levels.Count > _cL+1)
                    {
                        _cL++;
                        _levels[_cL].SetDefaults(_player , _difficulty);
                    }
                    else
                    {
                        screen = Screen.Menu;
                        _gameMusic[_cS].Stop();
                    }
                }
                else if (_difficulty != 0){
                    if (_levels[_cL].DidPlayerDie(_player)){
                        _gameMusic[_cS].Stop();
                        screen = Screen.Death;
                        _levels[_cL].SetDefaults(_player, _difficulty);
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
                if (_cL != _cL2)
                {
                    _levels[_cL2].Update(gameTime, _player2);
                    if (_levels[_cL2].PlayerCompleteLevel(_player2))
                    {
                        if (_levels.Count > _cL2 + 1)
                        {
                            _cL++;
                            _levels[_cL2].SetDefaults(_player, _difficulty);
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
            else if (screen == Screen.Menu){

                this.Window.Title = $"Mouse X {_mouseState.X}, MouseY {_mouseState.Y}";
                
                if (_mouseState.LeftButton == ButtonState.Pressed && _prevMS.LeftButton == ButtonState.Released){
                    if (_arrowButtons[0].Clicked(_mouseState)){
                        if (_currentSkin == 0)
                            _currentSkin = _ninjaSkins.Count - 1;
                        else
                            _currentSkin --;
                    }
                    else if (_arrowButtons[1].Clicked(_mouseState)){
                        if (_currentSkin + 1 < _ninjaSkins.Count)
                            _currentSkin++;
                        else
                            _currentSkin = 0;
                    }
                    else if (_arrowButtons[2].Clicked(_mouseState)){
                        if (_cL == 0)
                            _cL = _levels.Count - 1;
                        else
                            _cL--;
                    }
                    else if (_arrowButtons[3].Clicked(_mouseState)){
                        if (_cL + 1 < _levels.Count)
                            _cL++;
                        else
                            _cL = 0;
                    }
                    else if (_arrowButtons[4].Clicked(_mouseState)){
                        if (_cS == 0)
                            _cS = _gameMusic.Count - 1;
                        else
                            _cS--;
                    }
                    else if (_arrowButtons[5].Clicked(_mouseState)){
                        if (_cS + 1 < _gameMusic.Count)
                            _cS++;
                        else
                            _cS = 0;
                    }
                    else if (_arrowButtons[6].Clicked(_mouseState)){
                        if (_difficulty == 0)
                            _difficulty = 3;
                        else
                            _difficulty--;
                    }
                    else if (_arrowButtons[7].Clicked(_mouseState)){
                        if (_difficulty == 3)
                            _difficulty = 0;
                        else
                            _difficulty++;
                    }
                    else if (_arrowButtons[8].Clicked(_mouseState)){
                        _levels[_cL].SetDefaults(_player, _difficulty);
                        _player.SetSkin(_ninjaSkins[_currentSkin]);
                        screen = Screen.Game;
                    }
                    else if (_arrowButtons[9].Clicked(_mouseState)){
                        _graphics.PreferredBackBufferWidth = 1200;
                        _graphics.PreferredBackBufferHeight = 500;
                        _graphics.ApplyChanges();

                        _levels[_cL].SetDefaults(_player, _difficulty);
                        _player.SetSkin(_ninjaSkins[_currentSkin]);
                        _levels[_cL2].SetDefaults(_player2, _difficulty);
                        _player2.SetSkin(_ninjaSkins[_currentSkin2]);

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
            else if (screen == Screen.Settings){
                if (_mouseState.LeftButton == ButtonState.Pressed && _prevMS.LeftButton == ButtonState.Released){
                    if (_settingsButtons[0].Clicked(_mouseState)){
                        if (Keyboard.GetState().GetPressedKeys().Length == 1)
                            _player.SetLeft(Keyboard.GetState().GetPressedKeys()[0]);
                    }
                    else if (_settingsButtons[1].Clicked(_mouseState)){
                        if (Keyboard.GetState().GetPressedKeys().Length == 1)
                            _player.SetRight(Keyboard.GetState().GetPressedKeys()[0]);
                    }
                    else if (_settingsButtons[2].Clicked(_mouseState)){
                        if (Keyboard.GetState().GetPressedKeys().Length == 1)
                            _player.SetJump(Keyboard.GetState().GetPressedKeys()[0]);
                    }
                    else if (_settingsButtons[3].Clicked(_mouseState)){
                        if (Keyboard.GetState().GetPressedKeys().Length == 1)
                            _player.SetSprint(Keyboard.GetState().GetPressedKeys()[0]);
                    }
                    else if (_settingsButtons[4].Clicked(_mouseState))
                        screen = Screen.Game;
                    else if (_settingsButtons[5].Clicked(_mouseState))
                        screen = Screen.Menu;
                    else if (_settingsButtons[6].Clicked(_mouseState))
                        Exit();
                    else if (_settingsButtons[7].Clicked(_mouseState)){
                        _settingsButtons[7].SwitchDisplay();
                        _soundOn = !_soundOn;
                    }
                }
            }
            else if (screen == Screen.Death){
                if(_soundOn)
                    _deathSound.Play();
                _playerRespawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_playerRespawnTimer > 5){
                    screen = Screen.Game;
                    _playerRespawnTimer = 0;
                    _deathSound.Stop();
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
                for (int i = 0; i<_arrowButtons.Count(); i++)
                {
                    _arrowButtons[i].Draw(_spriteBatch);
                }
                _spriteBatch.Draw(_ninjaSkins[_currentSkin], _skinRectangle, new Rectangle(31, 14, 38, 72), Color.White);
                _spriteBatch.DrawString(_ninjaFont, $"Level: {_cL+1}", _menuPositions[0], Color.Black);
                _spriteBatch.DrawString(_ninjaFont, $"Music: {_cS +1}", _menuPositions[1], Color.Black);
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
            else if (screen == Screen.Settings){
                GraphicsDevice.Clear(Color.SkyBlue);
                _spriteBatch.Begin();
                _spriteBatch.DrawString(_ninjaFont, "To Change KeyBinds Hold the Key Down\n                and click the button", new Vector2(22, 10), Color.Black);
                foreach (Button b in _settingsButtons)
                    b.Draw(_spriteBatch);
                _spriteBatch.End();
            }
            else if (screen == Screen.Death){
                GraphicsDevice.Clear(Color.Red);
                _spriteBatch.Begin();
                _spriteBatch.Draw(_deathBG, new Vector2(0, 0), Color.White);
                _spriteBatch.End();
            }
            else if (screen == Screen.Multiplayer)
            {
                GraphicsDevice.Clear(Color.White);

                GraphicsDevice.Viewport = _viewPort1;
                _spriteBatch.Begin(transformMatrix: _camera.Transform);
                _levels[_cL].Draw(_spriteBatch, _player);
                _spriteBatch.End();

                GraphicsDevice.Viewport = _viewPort2;
                _spriteBatch.Begin(transformMatrix: _camera2.Transform);
                _levels[_cL2].Draw(_spriteBatch, _player2);
                _spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}