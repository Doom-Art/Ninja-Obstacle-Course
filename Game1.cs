using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Ninja_Obstacle_Course
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private MouseState _mouseState;
        private MouseState _prevMS;
        private KeyboardState _keyBoard;

        private List<Level> _levels;
        private int _cL;

        private Player _player;
        private Vector2 _playerStartingPosition;
        private float _playerRespawnTimer;
        private Camera _camera;

        private Sprite _settingsOpener;
        private Button[] _settingsButtons;

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
            screen = Screen.Settings;
            _playerStartingPosition = new Vector2(-200, -200);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D rectangleTex = Content.Load<Texture2D>("Images/Rectangle");
            SpriteFont font = Content.Load<SpriteFont>("Fonts/Font");

            //Player Content
            _player = new Player(Content.Load<Texture2D>("Images/Ninja"), new Rectangle[8] { new Rectangle(31, 14, 38, 72), new Rectangle(131, 14, 38, 72), new Rectangle(231, 14, 38, 72), new Rectangle(31, 114, 38, 72), new Rectangle(131, 114, 38, 72), new Rectangle(231, 114, 38, 72), new Rectangle(31, 214, 38, 72), new Rectangle(131, 214, 38, 72) });
            _player.Position = _playerStartingPosition;

            //Settings
            _settingsOpener = new Sprite(Content.Load<Texture2D>("Images/Gear"));
            _settingsOpener.Position = new Vector2(570, 10);
            _settingsButtons = new Button[6];
            string[] st = new string[6] { "Set Left Key", "Set Right Key", "Set Jump Key", "Set Sprint Key", "Resume Game", "Quit Game" };
            for (int i = 0; i < 6; i++)
            {
                _settingsButtons[i] = new Button(rectangleTex, font, new Rectangle(190, (i * 65 + 90), 230, 45), st[i]);
            }

            //Level 1 Content



        }

        protected override void Update(GameTime gameTime)
        {
            //Get User Inputs
            _prevMS = _mouseState;
            _mouseState = Mouse.GetState();
            _keyBoard = Keyboard.GetState();

            if (screen == Screen.Settings)
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
                        screen = Screen.Game;
                    }
                    else if (_settingsButtons[5].Clicked(_mouseState))
                    {
                        Exit();
                    }
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (screen == Screen.Settings)
            {
                GraphicsDevice.Clear(Color.SkyBlue);
                _spriteBatch.Begin();
                _spriteBatch.DrawString(_settingsButtons[0].SpriteFont, "To Change KeyBinds Hold the Key Down\n                and click the button", new Vector2(22, 10), Color.Black);
                foreach (Button b in _settingsButtons)
                    b.Draw(_spriteBatch);
                _spriteBatch.End();
            }
           

            base.Draw(gameTime);
        }
    }
}