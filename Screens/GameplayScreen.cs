using BacterialBarrage.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Input;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BacterialBarrage.Screens
{
    internal class GameplayScreen : BacterialBarrageGameScreen
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private Texture2D _virusTexture;
        private Texture2D _bacteria1Texture;
        private Texture2D _bacteria2Texture;
        private Texture2D _bacteria3Texture;
        private Texture2D _antibodyTexture;
        private Texture2D _rnaTexture;
        private readonly CollisionComponent _collisionComponent;
        private Player _player1;
        private Player _player2;
        private List<Germ> _enemies;
        private List<Attack> _attacks;
        private double _roundCountDown;
        private bool _stillInCountDown;
        private bool _aHasBeenUp;
        public GameplayScreen(Game game) : base(game) 
        {
            _aHasBeenUp = false;
            _stillInCountDown = true;
            _collisionComponent = new CollisionComponent(new MonoGame.Extended.RectangleF(0, 0, ScreenWidth, ScreenHeight));
            _roundCountDown = 0;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("Public Pixel");
            _virusTexture = Content.Load<Texture2D>("Virus");
            _bacteria1Texture = Content.Load<Texture2D>("Bacteria1");
            _bacteria2Texture = Content.Load<Texture2D>("Bacteria2");
            _bacteria3Texture = Content.Load<Texture2D>("Bacteria3");
            _antibodyTexture = Content.Load<Texture2D>("Antibody");
            _rnaTexture = Content.Load<Texture2D>("RNA-Attack");
        }

        public override void Update(GameTime gameTime)
        {
            var kstate = KeyboardExtended.GetState();
            var p1GamepadState = GamePad.GetState(PlayerIndex.One);
            var p2GamepadState = GamePad.GetState(PlayerIndex.Two);
            if (_stillInCountDown)
            {
                _roundCountDown += gameTime.ElapsedGameTime.TotalSeconds;
                if(_roundCountDown > 5)
                    _stillInCountDown = false;
            }
            else
            {
                //Make sure we're not still catching the button press from the previous screen or previous fire
                if ((p1GamepadState.Buttons.A == ButtonState.Pressed && _aHasBeenUp) || kstate.WasKeyJustDown(Keys.Space) || kstate.WasKeyJustDown(Keys.W))
                {
                    Fire(PlayerIndex.One);
                    _aHasBeenUp = false;
                }

                if (p2GamepadState.Buttons.A == ButtonState.Pressed || kstate.WasKeyJustDown(Keys.Up))
                    Fire(PlayerIndex.Two);


                if (kstate.IsKeyDown(Keys.A))
                    MoveLeft(PlayerIndex.One, gameTime);

                if (kstate.IsKeyDown(Keys.Left))
                    MoveLeft(PlayerIndex.Two, gameTime);

                if (kstate.IsKeyDown(Keys.D))
                    MoveRight(PlayerIndex.One, gameTime);

                if (kstate.IsKeyDown(Keys.Right))
                    MoveRight(PlayerIndex.Two, gameTime);

            }
            if (p1GamepadState.Buttons.Back == ButtonState.Pressed || kstate.WasKeyJustDown(Keys.Escape))
                ScreenManager.LoadScreen(new TitleScreen(Game));
            if (p1GamepadState.Buttons.A == ButtonState.Released)
                _aHasBeenUp = true;
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkRed);

            _spriteBatch.Begin();

            if (_stillInCountDown)
            {                
                var text = ((int)(6 - _roundCountDown)).ToString();
                if (_roundCountDown < 2) 
                {
                    text = "READY?";
                }
                var adjustment = _font.MeasureString(text) * _scale / 4 / 2;

                _spriteBatch.DrawString(_font,
                    text,
                    new Vector2(ScreenWidth / 2 -  adjustment.X, ScreenHeight / 2 - adjustment.Y),
                    Color.White,
                    0f,
                    Vector2.One,
                    _scale / 4,
                    SpriteEffects.None,
                    0f);

            }

            _spriteBatch.End();
        }

        private void Fire(PlayerIndex player)
        {

        }

        private void MoveLeft(PlayerIndex player, GameTime gameTime) { }

        private void MoveRight(PlayerIndex player, GameTime gameTime) { }
    }
}
