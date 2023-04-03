using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Input;

namespace BacterialBarrage.Screens
{
    internal class ControlsDisplayScreen : BacterialBarrageGameScreen
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private Texture2D _AButton;
        private Texture2D _YButton;
        private Texture2D _backButton;
        private Texture2D _stickLeft;
        private Texture2D _stickRight;

        public ControlsDisplayScreen(Game game) : base(game) { }

        public override void LoadContent()
        {
            base.LoadContent();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("Public Pixel");
            _AButton = Content.Load<Texture2D>("A-Button");
            _YButton = Content.Load<Texture2D>("Y-Button");
            _backButton = Content.Load<Texture2D>("Back-Button");
            _stickLeft = Content.Load<Texture2D>("Gamepad-Left");
            _stickRight = Content.Load<Texture2D>("Gamepad-Right");
        }

        public override void Update(GameTime gameTime)
        {
            if (KeyboardExtended.GetState().WasKeyJustDown(Keys.Escape))
                ScreenManager.LoadScreen(new TitleScreen(Game));
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkRed);

            _spriteBatch.Begin();

            _spriteBatch.DrawString(
                _font,
                "CONTROLS",
                new Vector2((ScreenWidth / 5) - _font.MeasureString("CONTROLS").X / 2 * _scale / 8, ScreenHeight / 16),
                Color.White,
                0f,
                Vector2.One,
                _scale / 8,
                SpriteEffects.None,
                0f);

            _spriteBatch.DrawString(
                _font,
                "P1",
                new Vector2((ScreenWidth / 5 * 2) - _font.MeasureString("P1").X / 2 * _scale / 8, ScreenHeight / 16),
                Color.White,
                0f,
                Vector2.One,
                _scale / 8,
                SpriteEffects.None,
                0f);

            _spriteBatch.DrawString(
                _font,
                "P2",
                new Vector2((ScreenWidth / 5 * 3) - _font.MeasureString("P2").X / 2 * _scale / 8, ScreenHeight / 16),
                Color.White,
                0f,
                Vector2.One,
                _scale / 8,
                SpriteEffects.None,
                0f);

            _spriteBatch.DrawString(
                _font,
                "GAMEPAD",
                new Vector2((ScreenWidth / 5 * 4) - _font.MeasureString("LEFT").X / 2 * _scale / 8, ScreenHeight / 16),
                Color.White,
                0f,
                Vector2.One,
                _scale / 8,
                SpriteEffects.None,
                0f);

            _spriteBatch.DrawString(
                _font,
                "LEFT",
                new Vector2((ScreenWidth / 5) - _font.MeasureString("LEFT").X / 2 * _scale / 8, ScreenHeight / 16 * 4),
                Color.White,
                0f,
                Vector2.One,
                _scale / 8,
                SpriteEffects.None,
                0f);

            _spriteBatch.DrawString(
                _font,
                "A",
                new Vector2((ScreenWidth / 5 * 2) - _font.MeasureString("A").X / 2 * _scale / 8, ScreenHeight / 16 * 4),
                Color.White,
                0f,
                Vector2.One,
                _scale / 8,
                SpriteEffects.None,
                0f);

            _spriteBatch.DrawString(
                _font,
                "L-ARROW",
                new Vector2((ScreenWidth / 5 * 3) - _font.MeasureString("L-ARROW").X / 2 * _scale / 8, ScreenHeight / 16 * 4),
                Color.White,
                0f,
                Vector2.One,
                _scale / 8,
                SpriteEffects.None,
                0f);

            _spriteBatch.Draw(_stickLeft,
                new Vector2((ScreenWidth / 5 * 4) , ScreenHeight / 16 * 4),
                null,
                Color.White,
                0f,
                Vector2.One,
                _scale / 5,
                SpriteEffects.None,
                0f);

            _spriteBatch.DrawString(
                _font,
                "RIGHT",
                new Vector2((ScreenWidth / 5) - _font.MeasureString("RIGHT").X / 2 * _scale / 8, ScreenHeight / 16 * 6),
                Color.White,
                0f,
                Vector2.One,
                _scale / 8,
                SpriteEffects.None,
                0f);

            _spriteBatch.DrawString(
                _font,
                "D",
                new Vector2((ScreenWidth / 5 * 2) - _font.MeasureString("D").X / 2 * _scale / 8, ScreenHeight / 16 * 6),
                Color.White,
                0f,
                Vector2.One,
                _scale / 8,
                SpriteEffects.None,
                0f);

            _spriteBatch.DrawString(
                _font,
                "R-ARROW",
                new Vector2((ScreenWidth / 5 * 3) - _font.MeasureString("R-ARROW").X / 2 * _scale / 8, ScreenHeight / 16 * 6),
                Color.White,
                0f,
                Vector2.One,
                _scale / 8,
                SpriteEffects.None,
                0f);

            _spriteBatch.Draw(_stickRight,
                new Vector2((ScreenWidth / 5 * 4), ScreenHeight / 16 * 6),
                null,
                Color.White,
                0f,
                Vector2.One,
                _scale / 5,
                SpriteEffects.None,
                0f);

            _spriteBatch.DrawString(
                _font,
                "FIRE",
                new Vector2((ScreenWidth / 5) - _font.MeasureString("FIRE").X / 2 * _scale / 8, ScreenHeight / 16 * 8),
                Color.White,
                0f,
                Vector2.One,
                _scale / 8,
                SpriteEffects.None,
                0f);

            _spriteBatch.DrawString(
                _font,
                "SPACE",
                new Vector2((ScreenWidth / 5 * 2) - _font.MeasureString("SPACE").X / 2 * _scale / 8, ScreenHeight / 16 * 8),
                Color.White,
                0f,
                Vector2.One,
                _scale / 8,
                SpriteEffects.None,
                0f);

            _spriteBatch.DrawString(
                _font,
                "ENTER",
                new Vector2((ScreenWidth / 5 * 3) - _font.MeasureString("ENTER").X / 2 * _scale / 8, ScreenHeight / 16 * 8),
                Color.White,
                0f,
                Vector2.One,
                _scale / 8,
                SpriteEffects.None,
                0f);

            _spriteBatch.Draw(_AButton,
                new Vector2((ScreenWidth / 5 * 4), ScreenHeight / 16 * 8),
                null,
                Color.White,
                0f,
                Vector2.One,
                _scale / 5,
                SpriteEffects.None,
                0f);

            _spriteBatch.DrawString(
                _font,
                "ADD/RM P2",
                new Vector2((ScreenWidth / 5) - _font.MeasureString("ADD/RM P2").X / 2 * _scale / 8, ScreenHeight / 16 * 10),
                Color.White,
                0f,
                Vector2.One,
                _scale / 8,
                SpriteEffects.None,
                0f);

            _spriteBatch.DrawString(
                _font,
                "P",
                new Vector2((ScreenWidth / 5 * 2) - _font.MeasureString("P").X / 2 * _scale / 8, ScreenHeight / 16 * 10),
                Color.White,
                0f,
                Vector2.One,
                _scale / 8,
                SpriteEffects.None,
                0f);

            _spriteBatch.DrawString(
                _font,
                "N/A",
                new Vector2((ScreenWidth / 5 * 3) - _font.MeasureString("N/A").X / 2 * _scale / 8, ScreenHeight / 16 * 10),
                Color.White,
                0f,
                Vector2.One,
                _scale / 8,
                SpriteEffects.None,
                0f);

            _spriteBatch.Draw(_YButton,
                new Vector2((ScreenWidth / 5 * 4), ScreenHeight / 16 * 10),
                null,
                Color.White,
                0f,
                Vector2.One,
                _scale / 5,
                SpriteEffects.None,
                0f);


            _spriteBatch.DrawString(
                _font,
                "QUIT TO MENU",
                new Vector2((ScreenWidth / 5) - _font.MeasureString("QUIT TO MENU").X / 2 * _scale / 8, ScreenHeight / 16 * 12),
                Color.White,
                0f,
                Vector2.One,
                _scale / 8,
                SpriteEffects.None,
                0f);

            _spriteBatch.DrawString(
                _font,
                "ESC",
                new Vector2((ScreenWidth / 5 * 2) - _font.MeasureString("ESC").X / 2 * _scale / 8, ScreenHeight / 16 * 12),
                Color.White,
                0f,
                Vector2.One,
                _scale / 8,
                SpriteEffects.None,
                0f);

            _spriteBatch.DrawString(
                _font,
                "N/A",
                new Vector2((ScreenWidth / 5 * 3) - _font.MeasureString("N/A").X / 2 * _scale / 8, ScreenHeight / 16 * 12),
                Color.White,
                0f,
                Vector2.One,
                _scale / 8,
                SpriteEffects.None,
                0f);

            _spriteBatch.Draw(_backButton,
                new Vector2((ScreenWidth / 5 * 4), ScreenHeight / 16 * 12),
                null,
                Color.White,
                0f,
                Vector2.One,
                _scale / 5,
                SpriteEffects.None,
                0f);

            _spriteBatch.DrawString(
                _font,
                "PRESS ESC TO RETURN TO MENU",
                new Vector2((ScreenWidth / 2) - _font.MeasureString("PRESS ESC TO RETURN TO MENU").X / 2 * _scale / 8, ScreenHeight / 16 * 15),
                Color.White,
                0f,
                Vector2.One,
                _scale / 8,
                SpriteEffects.None,
                0f);


            _spriteBatch.End();
        }
    }
}
