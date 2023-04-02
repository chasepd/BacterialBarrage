using BacterialBarrage.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Screens;

namespace BacterialBarrage
{
    public class BacterialBarrageGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private ScreenManager _screenManager;

        public BacterialBarrageGame()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720
            };
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            _screenManager = Components.Add<ScreenManager>();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            _screenManager.LoadScreen(new TitleScreen(this));
        }
    }
}