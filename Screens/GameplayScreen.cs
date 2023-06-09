﻿using BacterialBarrage.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended;
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
        private Texture2D _player2Texture;
        private Texture2D _explosionTexture;
        private SoundEffect _germDeath;
        private SoundEffect _playerDeath;
        private SoundEffect _playerShoot;
        private SoundEffect _germShoot;
        private Song virusPresent;
        private Player _player1;
        private Player _player2;
        private List<List<Germ>> _enemies;
        private List<Virus> _viruses;
        private List<Attack> _attacks;
        private List<Shield> _shields;
        private List<GameObject> _allOnScreenObjects;
        private double _roundCountDown = 0;
        private double _gameOverCountDown = 0;
        private int _currentLevel;
        private bool _stillInCountDown;
        private bool _aHasBeenUp;
        private const int _enemiesPerRow = 12;
        private const int _enemyRows = 5;
        private const int _playerMovementMagnitude = 100;
        private double _songDelay = 1.5;
        private double _songDelayTracker = 0;
        private List<SoundEffect> _notes;
        private int _noteIndex;
        private double _enemyAttackChanceCooldown = 0.2;
        private double _playerAttackCooldown = 0.2;
        private double _virusChanceCooldown = 0.3;
        private double _enemyAttackTimeTracker = 0;
        private double _virusChanceTimeTracker = 0;
        private bool _gameOver;
        public GameplayScreen(Game game) : base(game)
        {
            _gameOver = false;
            _aHasBeenUp = false;
            _currentLevel = 1;
            _notes = new List<SoundEffect>() { GameState.C, GameState.E, GameState.CSharp, GameState.C };
            _noteIndex = 0;
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
            _player2Texture = Content.Load<Texture2D>("WhiteBloodCellP2");
            _explosionTexture = Content.Load<Texture2D>("Explosion");

            _germDeath = Content.Load<SoundEffect>("EnemyHit");
            _playerDeath = Content.Load<SoundEffect>("PlayerHit");
            _playerShoot = Content.Load<SoundEffect>("PlayerShoot");
            _germShoot = Content.Load<SoundEffect>("EnemyShoot");
            virusPresent = Content.Load<Song>("VirusPresent");
            CreateNewLevel();
        }

        public override void Update(GameTime gameTime)
        {
            if (MediaPlayer.State == MediaState.Stopped)
            {
                _songDelayTracker += gameTime.GetElapsedSeconds();
                if (_songDelayTracker > _songDelay)
                {
                    _notes[_noteIndex].CreateInstance().Play();
                    _noteIndex++;
                    if (_noteIndex >= _notes.Count)
                        _noteIndex = 0;
                    _songDelayTracker = 0;
                }
            }
            CheckCollisions();

            var kstate = KeyboardExtended.GetState();
            var p1GamepadState = GamePad.GetState(PlayerIndex.One);
            var p2GamepadState = GamePad.GetState(PlayerIndex.Two);
            _enemyAttackTimeTracker += gameTime.GetElapsedSeconds();
            _virusChanceTimeTracker += gameTime.GetElapsedSeconds();
            if (_stillInCountDown)
            {
                _roundCountDown += gameTime.GetElapsedSeconds();
                if(_roundCountDown > 7)
                    _stillInCountDown = false;
            }

            if (_gameOver)
            {
                _gameOverCountDown += gameTime.GetElapsedSeconds();

                if (_gameOverCountDown > 5)
                    ScreenManager.LoadScreen(new TitleScreen(Game));
            }

            if(!_stillInCountDown && !_gameOver)
            {
                List<GameObject> deadObjects = new List<GameObject>();
                foreach(var obj in _allOnScreenObjects)
                {
                    obj.Update(gameTime);
                    obj.Position += obj.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if(obj.IsDead)
                        deadObjects.Add(obj);
                }
                bool shiftRows = false;
                bool atLeastOneGermAlive = false;

                if (_viruses.Count > 0 && MediaPlayer.State != MediaState.Playing)
                {
                    MediaPlayer.Play(virusPresent);                 

                }
                else
                {
                    if (_viruses.Count == 0)
                    {
                        MediaPlayer.IsRepeating = false;
                    }
                }

                foreach (Virus virus in _viruses)
                {
                    if (_enemyAttackTimeTracker > _enemyAttackChanceCooldown)
                    {
                        if (new Random().NextDouble() < virus.AttackChance)
                            Fire(virus);
                    }
                }

                foreach (var germRow in _enemies)
                {
                    foreach (var germ in germRow)
                    {
                        atLeastOneGermAlive = atLeastOneGermAlive || !germ.IsDead;

                        if (germ.Position.X > ScreenWidth / 32 * 31 || germ.Position.X < ScreenWidth / 32)
                        {
                            shiftRows = true;
                        }
                        if (_enemyAttackTimeTracker > _enemyAttackChanceCooldown && !germ.IsDead)
                        {
                            if (new Random().NextDouble() < germ.AttackChance)
                                Fire(germ);                            
                        }
                    }
                }

                atLeastOneGermAlive = atLeastOneGermAlive || _viruses.Count > 0;

                if(_enemyAttackTimeTracker  < _enemyAttackChanceCooldown)
                    _enemyAttackTimeTracker = 0;

                if (_virusChanceTimeTracker > _virusChanceCooldown)
                {
                    if (new Random().NextDouble() < 0.0003 * _currentLevel)
                    {
                        Virus virus = new Virus(_virusTexture, ScreenWidth)
                        {
                            Scale = new Vector2(_scale / 8, _scale / 8),
                            Rotation = 0f,
                            Position = new Vector2(-200, ScreenHeight / 10)
                        };
                        _allOnScreenObjects.Add(virus);
                        _viruses.Add(virus);
                    }
                }

                if (!atLeastOneGermAlive)
                {
                    _currentLevel++;
                    CreateNewLevel();
                }                

                if (shiftRows)
                {
                    _songDelay -= 0.2;
                    if(_songDelay < 0.25)
                        _songDelay = 0.25;
                    
                    foreach(var germRow in _enemies)
                    {
                        foreach( var germ in germRow)
                        {
                            germ.Velocity = new Vector2(Math.Abs(germ.Velocity.X * -1.3f) < ScreenWidth / 6 ? germ.Velocity.X * -1.3f : germ.Velocity.X * -1f, 10 * _scale);
                        }
                    }
                }

                foreach (var obj in deadObjects)
                {
                    _allOnScreenObjects.Remove(obj);
                    if (obj is Germ)
                        _allOnScreenObjects.Add(new Explosion(_explosionTexture, _germDeath)
                        {
                            Position = obj.Position,
                            Velocity = Vector2.Zero,
                            Rotation = 0f,
                            Scale = new Vector2(_scale / 4, _scale / 4)
                        });
                    if (obj is Virus virus)
                        _viruses.Remove(virus);
                }

                _player1.Velocity = Vector2.Zero;
                _player1.AttackCooldownTracker += gameTime.GetElapsedSeconds();
                if (_player2 != null)
                {
                    _player2.Velocity = Vector2.Zero;
                    _player2.AttackCooldownTracker += gameTime.GetElapsedSeconds();
                }
                
                //Make sure we're not still catching the button press from the previous screen or previous fire
                if ((p1GamepadState.Buttons.A == ButtonState.Pressed && _aHasBeenUp) || kstate.WasKeyJustDown(Keys.Space) || kstate.WasKeyJustDown(Keys.W))
                {
                    Fire(_player1);
                    _aHasBeenUp = false;
                }

                if ((p2GamepadState.Buttons.A == ButtonState.Pressed || kstate.WasKeyJustDown(Keys.Up)) && _player2 != null)
                    Fire(_player2);

                if (p1GamepadState.Buttons.Y == ButtonState.Pressed || kstate.WasKeyJustDown(Keys.P))
                {
                    if (_player2 == null)
                    {
                        _player2 = new Player(_player2Texture)
                        {
                            Position = new Vector2(ScreenWidth / 2 - (_playerTexture.Height * _scale / 8 / 2), ScreenHeight - (40 * _scale)),
                            Velocity = Vector2.Zero,
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
                    
                if (kstate.IsKeyDown(Keys.A) || p1GamepadState.DPad.Left == ButtonState.Pressed)
                    MoveLeft(_player1, gameTime);

                if ((kstate.IsKeyDown(Keys.Left) || p2GamepadState.DPad.Left == ButtonState.Pressed) && _player2 != null)
                    MoveLeft(_player2, gameTime);

                if (kstate.IsKeyDown(Keys.D) || p1GamepadState.DPad.Right == ButtonState.Pressed)
                    MoveRight(_player1, gameTime);

                if ((kstate.IsKeyDown(Keys.Right) || p2GamepadState.DPad.Right == ButtonState.Pressed) && _player2 != null)
                    MoveRight(_player2, gameTime);

            }
            if (p1GamepadState.Buttons.Back == ButtonState.Pressed || kstate.WasKeyJustDown(Keys.Escape))
                ScreenManager.LoadScreen(new TitleScreen(Game));
            if (p1GamepadState.Buttons.A == ButtonState.Released)
                _aHasBeenUp = true;

            if (_player1.IsDead && (_player2 == null || (_player2 != null && _player2.IsDead)))
                _gameOver = true;
        }

        private void CheckCollisions()
        {
            foreach (var obj in _allOnScreenObjects)
            {
                foreach (var obj2 in _allOnScreenObjects)
                {
                    if (obj.Bounds.Intersects(obj2.Bounds))
                        obj.OnCollision(obj2);
                }
            }
            foreach(var shield in _shields)
            {
                List<ShieldTile> deadTiles = new List<ShieldTile>();
                foreach(var shieldTile in shield.ShieldTiles)
                {
                    foreach(var obj in _allOnScreenObjects)
                    {
                        if (shieldTile.Bounds.Intersects(obj.Bounds))
                        {
                            shieldTile.OnCollision(obj);
                            break;
                        }
                    }
                    if (shieldTile.IsDead)
                        deadTiles.Add(shieldTile);
                }
                foreach(var shieldTile in deadTiles)
                {
                    shield.KillTile(shieldTile);
                }
            }
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
            else if (_gameOver)
            {
                var text = "GAME OVER!";
              
                var adjustment = _font.MeasureString(text) * _scale / 4 / 2;

                _spriteBatch.DrawString(_font,
                    text,
                    new Vector2(ScreenWidth / 2 - adjustment.X, ScreenHeight / 2 - adjustment.Y),
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

                    //Texture2D _texture;

                    //_texture = new Texture2D(GraphicsDevice, 1, 1);
                    //_texture.SetData(new Color[] { Color.Red });

                    //_spriteBatch.Draw(_texture, ((RectangleF)obj.Bounds).ToRectangle(), Color.White);
                }
                foreach (var shield in _shields)
                {
                    var shieldTiles = shield.ShieldTiles;
                    foreach(var tile in shieldTiles)
                    {
                        _spriteBatch.Draw(tile.Texture,
                        tile.Bounds.ToRectangle(),
                        tile.SourceRectangle,
                        Color.White,
                        tile.Rotation,
                        Vector2.One,
                        SpriteEffects.None,
                        0f);
                    }
                }
                _spriteBatch.DrawRectangle(new RectangleF(
                    0,
                    ScreenHeight - (25 * _scale),
                    ScreenWidth,
                    1 * _scale
                    ),
                    Color.White,
                    thickness: _scale
                    );
            }
            DrawScores();
            DrawLives();

            _spriteBatch.End();
        }

        private void CreateNewLevel()
        {
            _songDelay = 1.5;
            _enemies = new List<List<Germ>>();
            _attacks = new List<Attack>();
            _allOnScreenObjects = new List<GameObject>();
            _viruses = new List<Virus>();
            if(_shields == null)
            {
                Texture2D _texture;

                _texture = new Texture2D(GraphicsDevice, 1, 1);
                _texture.SetData(new Color[] { Color.Green });
                _shields = new List<Shield>()
                    {new Shield(_texture, new Vector2(ScreenWidth / 5, ScreenHeight / 4 * 3), _scale),
                    new Shield(_texture, new Vector2(ScreenWidth / 5 * 2, ScreenHeight / 4 * 3), _scale),
                    new Shield(_texture, new Vector2(ScreenWidth / 5 * 3, ScreenHeight / 4 * 3), _scale),
                    new Shield(_texture, new Vector2(ScreenWidth / 5 * 4, ScreenHeight / 4 * 3), _scale)
                    };
            }
            _roundCountDown = 0;
            _stillInCountDown = true;
            if (_player1 == null)
            {
                _player1 = new Player(_playerTexture)
                {
                    Position = new Vector2(ScreenWidth / 2 - (_playerTexture.Height * _scale / 8 / 2) , ScreenHeight - (40 * _scale)),
                    Velocity = new Vector2(0, 0),
                    Scale = new Vector2(_scale / 8, _scale / 8),
                    Rotation = 0f
                };
                _allOnScreenObjects.Add(_player1);
            }
            else
            {
                _allOnScreenObjects.Add(_player1);
            }

            if( _player2 != null)             
                _allOnScreenObjects.Add(_player2);
            
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
                    newGerm.Velocity = new Vector2(10 * _currentLevel * _scale, 0);
                    newGerm.Position = new Vector2(ScreenWidth / 2 - (_bacteria1Texture.Height * newGerm.Scale.Y + 14 * _scale) * (6 - enemyNo), 20 * _scale + 12 * _scale * (row + 1));
                    germRow.Add(newGerm);
                    _allOnScreenObjects.Add(newGerm);
                }
                _enemies.Add(germRow);
            }
            
        }

        private void DrawScores()
        {
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
                _player1.Score.ToString("D4"),
                new Vector2((ScreenWidth / 6) - _font.MeasureString(_player1.Score.ToString("D4")).X / 2 * _scale / 8, ScreenHeight / 20),
                Color.White,
                0f,
                Vector2.One,
                _scale / 8,
                SpriteEffects.None,
                0f);

            _spriteBatch.DrawString(
                _font,
                GameState.HighScore.ToString("D4"),
                new Vector2((ScreenWidth / 2) - _font.MeasureString(GameState.HighScore.ToString("D4")).X / 2 * _scale / 8, ScreenHeight / 20),
                Color.White,
                0f,
                Vector2.One,
                _scale / 8,
                SpriteEffects.None,
                0f);
            if (_player2 != null)
            {
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
                    _player2.Score.ToString("D4"),
                    new Vector2((ScreenWidth / 6) * 5 - _font.MeasureString(_player2.Score.ToString("D4")).X / 2 * _scale / 8, ScreenHeight / 20),
                    Color.White,
                    0f,
                    Vector2.One,
                    _scale / 8,
                    SpriteEffects.None,
                    0f);
            }
        }

        private void DrawLives()
        {
            _spriteBatch.DrawString(
                    _font,
                    "P1 LIVES:",
                    new Vector2((ScreenWidth / 8) * 2 - _font.MeasureString("P1 LIVES:").X / 2 * _scale / 8, ScreenHeight - (10 * _scale)),
                    Color.White,
                    0f,
                    Vector2.One,
                    _scale / 8,
                    SpriteEffects.None,
                    0f);
            for (int life = 0; life < _player1.LivesRemaining - 1; life++)
                _spriteBatch.Draw(
                    _playerTexture,
                    new Vector2(ScreenWidth / 8 * 2 + _font.MeasureString("P1 LIVES:").X / 2 * _scale / 8 + (_playerTexture.Height * _scale / 8) * life + (2 * _scale * (life + 1)), ScreenHeight - (10 * _scale)),
                    new Rectangle(0, 0, _playerTexture.Height, _playerTexture.Height),
                    Color.White,
                    0f,
                    Vector2.One,
                    _scale / 8,
                    SpriteEffects.None,
                    0f
                    );

            if(_player2 != null)
            {
                _spriteBatch.DrawString(
                    _font,
                    "P2 LIVES:",
                    new Vector2((ScreenWidth / 8) * 5 - _font.MeasureString("P2 LIVES:").X / 2 * _scale / 8, ScreenHeight - (10 * _scale)),
                    Color.White,
                    0f,
                    Vector2.One,
                    _scale / 8,
                    SpriteEffects.None,
                    0f);
                for (int life = 0; life < _player2.LivesRemaining - 1; life++)
                    _spriteBatch.Draw(
                        _player2Texture,
                        new Vector2(ScreenWidth / 8 * 5 + _font.MeasureString("P2 LIVES:").X / 2 * _scale / 8 + (_playerTexture.Height * _scale / 8) * life + (2 * _scale * (life + 1)), ScreenHeight - (10 * _scale)),
                        new Rectangle(0, 0, _playerTexture.Height, _playerTexture.Height),
                        Color.White,
                        0f,
                        Vector2.One,
                        _scale / 8,
                        SpriteEffects.None,
                        0f
                        );
            }

        }

        private void Fire(Player player)
        {
            if (player.AttackCooldownTracker < _playerAttackCooldown)
                return;

            player.AttackCooldownTracker = 0;
            Antibody antibody = new Antibody(_antibodyTexture)
            {
                Scale = new Vector2(_scale / 16, _scale / 16),
                Rotation = 0f,
            };

            antibody.Player = player;
            antibody.Position = player.Position;

            _attacks.Add(antibody);
            _allOnScreenObjects.Add(antibody);
            _playerShoot.CreateInstance().Play();
        }

        private void Fire(Germ germ)
        {
            RNA rna = new RNA(_rnaTexture, ScreenHeight)
            {
                Scale = new Vector2(_scale / 16, _scale / 16),
                Rotation = 0f,
                Position = germ.Position
            };
            _attacks.Add(rna);
            _allOnScreenObjects.Add(rna);
            _germShoot.CreateInstance().Play();

        }

            private void MoveLeft(Player player, GameTime gameTime) 
        {
            player.Velocity = new Vector2(-_playerMovementMagnitude * _scale, 0);
            ConstrainPlayer(player);
        }

        private void MoveRight(Player player, GameTime gameTime) 
        {
            player.Velocity = new Vector2(_playerMovementMagnitude * _scale, 0);
            ConstrainPlayer(player);
        }

        private void ConstrainPlayer(Player player)
        {
            if (player.Position.X < ScreenWidth / 8)
            {
                player.Position = new Vector2(ScreenWidth / 8, player.Position.Y);
                player.Velocity = Vector2.Zero;
            }
            if(player.Position.X > ScreenWidth / 8 * 7)
            {
                player.Position = new Vector2(ScreenWidth / 8 * 7, player.Position.Y);
                player.Velocity = Vector2.Zero;
            }
        }
    }
}
