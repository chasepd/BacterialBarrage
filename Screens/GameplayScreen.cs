using BacterialBarrage.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
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
        private Texture2D _playerTexture;
        private readonly CollisionComponent _collisionComponent;
        private Player _player1;
        private Player _player2;
        private List<List<Germ>> _enemies;
        private List<Attack> _attacks;
        private List<ShieldTile> shieldTiles;
        private List<GameObject> _allOnScreenObjects;
        private double _roundCountDown;
        private int _currentLevel;
        private bool _stillInCountDown;
        private bool _aHasBeenUp;
        private const int _enemiesPerRow = 12;
        private const int _enemyRows = 5;
        public GameplayScreen(Game game) : base(game) 
        {
            _aHasBeenUp = false;
            _collisionComponent = new CollisionComponent(new MonoGame.Extended.RectangleF(0, 0, ScreenWidth, ScreenHeight));
            _currentLevel = 1;

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
            _playerTexture = Content.Load<Texture2D>("WhiteBloodCell");
            CreateNewLevel();
        }

        public override void Update(GameTime gameTime)
        {
            var kstate = KeyboardExtended.GetState();
            var p1GamepadState = GamePad.GetState(PlayerIndex.One);
            var p2GamepadState = GamePad.GetState(PlayerIndex.Two);
            if (_stillInCountDown)
            {
                _roundCountDown += gameTime.ElapsedGameTime.TotalSeconds;
                if(_roundCountDown > 7)
                    _stillInCountDown = false;
            }
            if(!_stillInCountDown)
            {
                foreach(var obj in _allOnScreenObjects)
                {
                    obj.Update(gameTime);
                }
                //Make sure we're not still catching the button press from the previous screen or previous fire
                if ((p1GamepadState.Buttons.A == ButtonState.Pressed && _aHasBeenUp) || kstate.WasKeyJustDown(Keys.Space) || kstate.WasKeyJustDown(Keys.W))
                {
                    Fire(PlayerIndex.One);
                    _aHasBeenUp = false;
                }

                if ((p2GamepadState.Buttons.A == ButtonState.Pressed || kstate.WasKeyJustDown(Keys.Up)) && _player2 != null)
                    Fire(PlayerIndex.Two);

                if (p1GamepadState.Buttons.Y == ButtonState.Pressed || kstate.WasKeyJustDown(Keys.P))
                {
                    if (_player2 == null)
                    {
                        _player2 = new Player(_playerTexture)
                        {
                            Position = new Vector2(0, 0),
                            Velocity = new Vector2(0, 0),
                            Scale = new Vector2(_scale / 8, _scale / 8),
                            Rotation = 0f
                        };
                        _allOnScreenObjects.Add(_player2);
                    }
                    else
                    {
                        _allOnScreenObjects.Remove(_player2);
                        _player2 = null;                        
                    }
                }
                    
                if (kstate.IsKeyDown(Keys.A))
                    MoveLeft(PlayerIndex.One, gameTime);

                if (kstate.IsKeyDown(Keys.Left) && _player2 != null)
                    MoveLeft(PlayerIndex.Two, gameTime);

                if (kstate.IsKeyDown(Keys.D))
                    MoveRight(PlayerIndex.One, gameTime);

                if (kstate.IsKeyDown(Keys.Right) && _player2 != null)
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
                var text = ((int)(8 - _roundCountDown)).ToString();

                if (_roundCountDown < 2)
                {
                    text = "LEVEL " + _currentLevel;
                }

                if (_roundCountDown < 4 && _roundCountDown >= 2) 
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
            else
            {
                foreach (var obj in _allOnScreenObjects)
                {
                    _spriteBatch.Draw(obj.Texture,
                        obj.Position,
                        obj.SourceRectangle,
                        Color.White,
                        obj.Rotation,
                        Vector2.One,
                        obj.Scale,
                        SpriteEffects.None,
                        0f);
                }
            }

            _spriteBatch.End();
        }

        private void CreateNewLevel()
        {
            _enemies = new List<List<Germ>>();
            _attacks = new List<Attack>();
            _allOnScreenObjects = new List<GameObject>();
            _roundCountDown = 0;
            _stillInCountDown = false;
            if (_player1 == null)
            {
                _player1 = new Player(_playerTexture)
                {
                    Position = new Vector2(ScreenWidth / 2 - (_playerTexture.Height * _scale / 8 / 2) , ScreenHeight - (10 * _scale)),
                    Velocity = new Vector2(0, 0),
                    Scale = new Vector2(_scale / 8, _scale / 8),
                    Rotation = 0f
                };
                _allOnScreenObjects.Add(_player1);
            }
            Random random = new Random();
            for(int row = 0; row < _enemyRows; row++)
            {               
                List<Germ> germRow = new List<Germ>();
                for(int enemyNo = 0; enemyNo < _enemiesPerRow; enemyNo++)
                {
                    var randomValue = random.NextDouble();
                    int selection;

                    // Increase likelihood of higher point enemies in higher levels
                    if (randomValue < 0.05 * _currentLevel)                    
                        selection = 3;
                    else if (randomValue < 0.10 * _currentLevel)
                        selection = 2;
                    else
                        selection = 1;

                    Germ newGerm = null;

                    switch(selection)
                    {
                        case 1:
                            newGerm = new BacteriaLevel1(_bacteria1Texture) {
                                Scale = new Vector2(_scale / 8, _scale / 8),
                                Rotation = 0f
                            };
                            break;
                        case 2:
                            newGerm = new BacteriaLevel2(_bacteria2Texture) {
                                Scale = new Vector2(_scale / 8, _scale / 8),
                                Rotation = 0f
                            }; ;                            
                            break;
                        case 3:
                            newGerm = new BacteriaLevel3(_bacteria3Texture) {
                                Scale = new Vector2(_scale / 8, _scale / 8),
                                Rotation = 0f
                            };                    
                            break;
                    }
                    newGerm.Velocity = new Vector2(1 * _scale, 0);
                    newGerm.Position = new Vector2(ScreenWidth / 2 - (_bacteria1Texture.Height * newGerm.Scale.Y + 4 * _scale) * (6 - enemyNo), 20 * _scale + 12 * _scale * (row + 1));
                    germRow.Add(newGerm);
                    _allOnScreenObjects.Add(newGerm);
                }
                _enemies.Add(germRow);
            }
            
        }

        private void Fire(PlayerIndex player)
        {
            Antibody antibody = new Antibody(_antibodyTexture);

            if(player == PlayerIndex.One)
            {
                antibody.Bounds.Position = _player1.Bounds.Position;
            }

            if(player == PlayerIndex.Two)
            {
                antibody.Bounds.Position = _player2.Bounds.Position;
            }
            _attacks.Add(antibody);
            _allOnScreenObjects.Add(antibody);
        }

        private void MoveLeft(PlayerIndex player, GameTime gameTime) { }

        private void MoveRight(PlayerIndex player, GameTime gameTime) { }

        private void ConstrainPlayer()
        {

        }
    }
}
