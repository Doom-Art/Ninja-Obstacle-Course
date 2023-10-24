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
        KeyboardState _keyBoard;

        List<Level> _levels;
        int _cL;

        Player _player;
        Vector2 _playerStartingPosition;
        float _playerRespawnTimer;
        Camera _camera;

        Sprite _settingsOpener;
        Button[] _settingsButtons;

        Screen screen;
        enum Screen
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
            _playerStartingPosition = new Vector2(-200, -200);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D rectangleTex = Content.Load<Texture2D>("Rectangle");
            SpriteFont font = Content.Load<SpriteFont>("Font");

            //Player Content
            _player = new Player(Content.Load<Texture2D>("Images/Ninja"), new Rectangle[8] { new Rectangle(31, 14, 38, 72), new Rectangle(131, 14, 38, 72), new Rectangle(231, 14, 38, 72), new Rectangle(31, 114, 38, 72), new Rectangle(131, 114, 38, 72), new Rectangle(231, 114, 38, 72), new Rectangle(31, 214, 38, 72), new Rectangle(131, 214, 38, 72) });
            _player.Position = _playerStartingPosition;

            //Settings
            _settingsOpener = new Sprite(Content.Load<Texture2D>("gear"));
            _settingsOpener.Position = new Vector2(570, 10);
            _settingsButtons = new Button[6];
            string[] st = new string[6] { "Set Left Key", "Set Right Key", "Set Jump Key", "Set Sprint Key", "Resume Game", "Quit Game" };
            for (int i = 0; i < 6; i++)
            {
                _settingsButtons[i] = new Button(rectangleTex, font, new Rectangle(200, (i * 70 + 80), 200, 40), st[i]);
            }

            //Level 1 Content

        }

        protected override void Update(GameTime gameTime)
        {
            


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}