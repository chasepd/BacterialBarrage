using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BacterialBarrage.Screens
{
    internal abstract class BacterialBarrageGameScreen : GameScreen
    {
        protected readonly Vector2 _originalResolution = new Vector2(256, 224);
        public int ScreenWidth => GraphicsDevice.Viewport.Width;
        public int ScreenHeight => GraphicsDevice.Viewport.Height;

        protected float _scale;
        public BacterialBarrageGameScreen(Game game) : base(game)
        {
            _scale = ScreenHeight / _originalResolution.Y;
        }
    }
}
