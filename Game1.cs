using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

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

        //Settings
        private Sprite _settingsOpener;
        private Button[] _settingsButtons;

        //Music
        private SoundEffectInstance _gameMusic;
        private SoundEffectInstance _deathSound;

        //Background Images
        Texture2D _deathBG;
        private Screen screen;
        private enum Screen
        {
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
            _levels = new List<Level>();
            screen = Screen.Game;
            _camera = new Camera();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D rectangleTex = Content.Load<Texture2D>("Images/Rectangle");
            SpriteFont font = Content.Load<SpriteFont>("Fonts/Font");

            //Player Content
            _player = new Player(Content.Load<Texture2D>("Images/NinjaSkins/NinjaDarkBlue"), new Rectangle[8] { new Rectangle(31, 14, 38, 72), new Rectangle(131, 14, 38, 72), new Rectangle(231, 14, 38, 72), new Rectangle(31, 114, 38, 72), new Rectangle(131, 114, 38, 72), new Rectangle(231, 114, 38, 72), new Rectangle(31, 214, 38, 72), new Rectangle(131, 214, 38, 72) });

            //Settings
            _settingsOpener = new Sprite(Content.Load<Texture2D>("Images/Gear"));
            _settingsOpener.Position = new Vector2(570, 10);
            _settingsButtons = new Button[8];
            string[] st = new string[8] { "Set Left Key", "Set Right Key", "Set Jump Key", "Set Sprint Key", "Resume Game", "Restart Level","Quit Game","" };
            int num = 0;
            for (int i = 0; i< 4; i++)
            {
                for (int j = 1; j < 3; j++)
                {
                    _settingsButtons[num] = new Button(rectangleTex, font, new Rectangle((240*j)-170, (i * 65 + 90), 230, 45), st[num]);
                    num++;
                }
            }

            //Music
            _gameMusic = Content.Load<SoundEffect>("Sounds/GameMusicK").CreateInstance();
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
            tempRWalkers.Add(new RedWalker(redWalker, redWalkerSourceRects, new Rectangle(2800, -480, 60, 80), 2680, 3400, rWalkerDoorTex));
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
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1520, -680, 80, 40), Color.Green, 0.6f, true,true));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(1280, -680, 160, 40), Color.Yellow));

            tempPortals.Add(new Portal(portalTex, new Rectangle(1320, -760, 50, 80), new Rectangle(400, -920, 50, 80)));
            tempPlatforms.Add(new Platform(rectangleTex, new Rectangle(240, -840, 240, 40), Color.Yellow));

            _levels.Add(new Level(tempPlatforms, tempPortals,tempRWalkers,tempSpikes));
            _levels[1].SetFont(font);
            _levels[1].AddSign(new Vector2(400, -500), "Beware of the Red Walkers \nThey kill anything they touch \nwhen walking between their \ndesignated squares");
            _levels[1].AddSign(new Vector2(1440, -400), "Pick a door any door\n4 are fake 1 is real");
            _levels[1].AddSign(new Vector2(1800, -330), "Watch out for \n  Spikes Ahead");
            _levels[1].SetExit(Content.Load<Texture2D>("Images/ExitPortalW"), new Rectangle(240, -960, 120, 120));

            //Sets Default Values
            _levels[_cL].SetDefaults(_player);
        }

        protected override void Update(GameTime gameTime)
        {
            //Get User Inputs
            _prevMS = _mouseState;
            _mouseState = Mouse.GetState();
            _keyBoard = Keyboard.GetState();
            
            if (screen == Screen.Game){
                _gameMusic.Play();
                _levels[_cL].Update(gameTime, _player);
                if (_levels[_cL].PlayerCompleteLevel(_player))
                {
                    if (_levels.Count > _cL+1)
                    {
                        _cL++;
                        _levels[_cL].SetDefaults(_player);
                    }
                    else
                    {
                        _levels[_cL].SetDefaults(_player);
                    }
                }
                else if (_levels[_cL].DidPlayerDie(_player))
                {
                    _gameMusic.Stop();
                    screen = Screen.Death;
                    _levels[_cL].SetDefaults(_player);
                }
                _camera.Follow(_player);
                if (_settingsOpener.Rectangle.Contains(_mouseState.X, _mouseState.Y) && _mouseState.LeftButton == ButtonState.Pressed)
                {
                    _gameMusic.Pause();
                    screen = Screen.Settings;
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
                        _levels[_cL].SetDefaults(_player);
                    else if (_settingsButtons[6].Clicked(_mouseState))
                        Exit();
                }
            }
            else if (screen == Screen.Death){
                _deathSound.Play();
                _playerRespawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_playerRespawnTimer > 5){
                    screen = Screen.Game;
                    _levels[_cL].SetDefaults(_player);
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
                GraphicsDevice.Clear(Color.SkyBlue);
                _spriteBatch.Begin(transformMatrix: _camera.Transform);
                _levels[_cL].Draw(_spriteBatch, _player);
                _spriteBatch.End();
                _spriteBatch.Begin();
                _settingsOpener.Draw(_spriteBatch);
                _spriteBatch.End();
            }
            else if (screen == Screen.Settings){
                GraphicsDevice.Clear(Color.SkyBlue);
                _spriteBatch.Begin();
                _spriteBatch.DrawString(_settingsButtons[0].SpriteFont, "To Change KeyBinds Hold the Key Down\n                and click the button", new Vector2(22, 10), Color.Black);
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


            base.Draw(gameTime);
        }
    }
}