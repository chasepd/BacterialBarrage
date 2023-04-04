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
    internal abstract class GameObject : ICollisionActor
    {     
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Scale { get; set; }
        public IShapeF Bounds => new RectangleF(Position.X, Position.Y, Texture.Width / _animationFrames * Scale.X, Texture.Height * Scale.Y);//Sprite.GetBoundingRectangle(Position, Rotation, Scale);
        //public Sprite Sprite { get; set; }
        public bool IsDead { get; protected set; }
        public Rectangle SourceRectangle { get; private set; }
        public Texture2D Texture { get; set; }
        public Vector2 Velocity { get; set; }
        protected int _animationFrames;
        private int _animationFrame;
        private double _animationTimeTracker;
        private const double _animationTimeDelay = 0.5;

        public GameObject(Texture2D texture)
        {
            IsDead = false;
            _animationTimeTracker = 0.0;

            Texture = texture;

            //Sprite = new Sprite(Texture);
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

        public abstract void OnCollision(CollisionEventArgs collisionEvent);
    }
}
