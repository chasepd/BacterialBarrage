using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BacterialBarrage.Screens
{
    internal class TitleScreen : BacterialBarrageGameScreen
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private Texture2D _virusTexture;
        private Texture2D _bacteria1Texture;
        private Texture2D _bacteria2Texture;
        private Texture2D _bacteria3Texture;
        public TitleScreen(Game game) : base(game) { }

        public override void LoadContent()
        {
            base.LoadContent();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("Public Pixel");
            _virusTexture = Content.Load<Texture2D>("Virus");
            _bacteria1Texture = Content.Load<Texture2D>("Bacteria1");
            _bacteria2Texture = Content.Load<Texture2D>("Bacteria2");
            _bacteria3Texture = Content.Load<Texture2D>("Bacteria3");
        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Game.Exit();
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkRed);

            _spriteBatch.Begin();

            _spriteBatch.DrawString(
                _font,
                "SCORE<1>",
                new Vector2((ScreenWidth / 6) - _font.MeasureString("SCORE<1>").X / 2 * _scale / 8, 0),
                Color.White,
                0f,
                Vector2.One,
                _scale / 8,
                SpriteEffects.None,
                0f);

            _spriteBatch.DrawString(
                _font,
                "HI-SCORE",
                new Vector2((ScreenWidth / 2) - _font.MeasureString("HI-SCORE").X / 2 * _scale / 8, 0),
                Color.White,
                0f,
                Vector2.One,
                _scale / 8,
                SpriteEffects.None,
                0f);

            _spriteBatch.DrawString(
                _font,
                "SCORE<2>",
                new Vector2((ScreenWidth / 6) * 5 - _font.MeasureString("SCORE<2>").X / 2 * _scale / 8, 0),
                Color.White,
                0f,
                Vector2.One,
                _scale / 8,
                SpriteEffects.None,
                0f);

            _spriteBatch.DrawString(
                _font,
                "PLAY",
                new Vector2((ScreenWidth / 2) - _font.MeasureString("PLAY").X / 2 * _scale / 8, ScreenHeight / 12 * 3),
                Color.White,
                0f,
                Vector2.One,
                _scale / 8,
                SpriteEffects.None,
                0f);

            _spriteBatch.DrawString(
                _font,
                "BACTERIAL BARRAGE",
                new Vector2((ScreenWidth / 2) - _font.MeasureString("BACTERIAL BARRAGE").X / 2 * _scale / 8, ScreenHeight / 12 * 4),
                Color.White,
                0f,
                Vector2.One,
                _scale / 8,
                SpriteEffects.None,
                0f);

            _spriteBatch.DrawString(
                _font,
                "*SCORE ADVANCE TABLE*",
                new Vector2((ScreenWidth / 2) - _font.MeasureString("*SCORE ADVANCE TABLE*").X / 2 * _scale / 8, 
                ScreenHeight / 12 * 6 - _font.MeasureString("*SCORE ADVANCE TABLE*").Y / 2 * _scale / 8),
                Color.White,
                0f,
                Vector2.One,
                _scale / 8,
                SpriteEffects.None,
                0f);

            var sourceRectangle = new Rectangle(0, 0, _virusTexture.Width / 5, _virusTexture.Height);

            _spriteBatch.Draw(_virusTexture,
                new Vector2((ScreenWidth / 2) - _font.MeasureString("=? MYSTERY").X / 2 * _scale / 8 - sourceRectangle.Width / 2 * _scale,
                ScreenHeight / 12 * 7 - _font.MeasureString("=? MYSTERY").Y / 2 * _scale / 8),
                sourceRectangle,
                Color.White,
                0f,
                Vector2.One,
                _scale,
                SpriteEffects.None,
                0f);

            _spriteBatch.DrawString(
                _font,
                "=? MYSTERY",
                new Vector2((ScreenWidth / 2) - _font.MeasureString("=? MYSTERY").X / 2 * _scale / 8 + sourceRectangle.Width / 2 * _scale,
                ScreenHeight / 12 * 7 - _font.MeasureString("=? MYSTERY").Y / 2 * _scale / 8),
                Color.White,
                0f,
                Vector2.One,
                _scale / 8,
                SpriteEffects.None,
                0f);

            _spriteBatch.Draw(_bacteria1Texture,
                new Vector2((ScreenWidth / 2) - _font.MeasureString(" = 30 POINTS").X / 2 * _scale / 8 - sourceRectangle.Width / 2 * _scale,
                ScreenHeight / 12 * 8 - _font.MeasureString(" = 30 POINTS").Y / 2 * _scale / 8),
                sourceRectangle,
                Color.White,
                0f,
                Vector2.One,
                _scale,
                SpriteEffects.None,
                0f);

            _spriteBatch.DrawString(
                _font,
                " = 30 POINTS",
                new Vector2((ScreenWidth / 2) - _font.MeasureString(" = 30 POINTS").X / 2 * _scale / 8 + sourceRectangle.Width / 2 * _scale,
                ScreenHeight / 12 * 8 - _font.MeasureString(" = 30 POINTS").Y / 2 * _scale / 8),
                Color.White,
                0f,
                Vector2.One,
                _scale / 8,
                SpriteEffects.None,
                0f);

            _spriteBatch.Draw(_bacteria2Texture,
                new Vector2((ScreenWidth / 2) - _font.MeasureString(" = 20 POINTS").X / 2 * _scale / 8 - sourceRectangle.Width / 2 * _scale,
                ScreenHeight / 12 * 9 - _font.MeasureString(" = 20 POINTS").Y / 2 * _scale / 8),
                sourceRectangle,
                Color.White,
                0f,
                Vector2.One,
                _scale,
                SpriteEffects.None,
                0f);

            _spriteBatch.DrawString(
                _font,
                " = 20 POINTS",
                new Vector2((ScreenWidth / 2) - _font.MeasureString(" = 20 POINTS").X / 2 * _scale / 8 + sourceRectangle.Width / 2 * _scale,
                ScreenHeight / 12 * 9 - _font.MeasureString(" = 20 POINTS").Y / 2 * _scale / 8),
                Color.White,
                0f,
                Vector2.One,
                _scale / 8,
                SpriteEffects.None,
                0f);

            _spriteBatch.Draw(_bacteria3Texture,
                new Vector2((ScreenWidth / 2) - _font.MeasureString(" = 10 POINTS").X / 2 * _scale / 8 - sourceRectangle.Width / 2 * _scale,
                ScreenHeight / 12 * 10 - _font.MeasureString(" = 10 POINTS").Y / 2 * _scale / 8),
                sourceRectangle,
                Color.White,
                0f,
                Vector2.One,
                _scale,
                SpriteEffects.None,
                0f);

            _spriteBatch.DrawString(
                _font,
                " = 10 POINTS",
                new Vector2((ScreenWidth / 2) - _font.MeasureString(" = 10 POINTS").X / 2 * _scale / 8 + sourceRectangle.Width / 2 * _scale,
                ScreenHeight / 12 * 10 - _font.MeasureString(" = 10 POINTS").Y / 2 * _scale / 8),
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
