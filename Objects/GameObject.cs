using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace BacterialBarrage.Objects
{
    internal abstract class GameObject
    {
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Scale { get; set; }
        public virtual RectangleF Bounds => new RectangleF(Position.X, Position.Y, Texture.Width / _animationFrames * Scale.X, Texture.Height * Scale.Y);
        public bool IsDead { get; set; }
        public virtual Rectangle SourceRectangle { get; protected set; }
        public Texture2D Texture { get; set; }
        public Vector2 Velocity { get; set; }
        protected int _animationFrames;
        protected int _animationFrame;
        private double _animationTimeTracker;
        protected double _animationTimeDelay = 0.5;

        public GameObject(Texture2D texture)
        {
            IsDead = false;
            _animationTimeTracker = 0.0;

            Texture = texture;
        }

        public virtual void Update(GameTime gameTime)
        {
            _animationTimeTracker += gameTime.ElapsedGameTime.TotalSeconds;
            if(_animationTimeTracker > _animationTimeDelay) 
            {
                _animationTimeTracker = 0.0;
                _animationFrame++;
                if(_animationFrame >= _animationFrames)
                {
                    _animationFrame = 0;
                }
            }

            SourceRectangle = new Rectangle((Texture.Width / _animationFrames * _animationFrame), 0, Texture.Width / _animationFrames, Texture.Height);
        }

        public abstract void OnCollision(GameObject otherObj);
    }
}
