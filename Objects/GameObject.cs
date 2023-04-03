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

namespace BacterialBarrage.Objects
{
    internal abstract class GameObject : ICollisionActor
    {
        public Sprite Sprite { get; set; }
        public IShapeF Bounds => Sprite.GetBoundingRectangle(null);
        public bool IsDead { get; protected set; }
        public Rectangle SourceRectangle { get; private set; }
        public Texture2D Texture { get; set; }
        protected int _animationFrames;
        private int _animationFrame;
        private double _animationTimeTracker;
        private const double _animationTimeDelay = 0.5;

        public GameObject()
        {
            IsDead = false;
            _animationTimeTracker = 0.0;
        }

        public void Update(GameTime gameTime)
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
