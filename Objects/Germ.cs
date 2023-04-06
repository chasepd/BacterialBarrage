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
    internal abstract class Germ : GameObject
    {
        public int PointValue { get; protected set; }

        public double AttackChance { get; protected set; }

        public Germ(Texture2D texture) : base(texture)
        {
            _animationFrames = 5;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if(Velocity.Y > 0)
            {
                Velocity = new Vector2(Velocity.X, Velocity.Y - 50 * Scale.Y * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            if(Velocity.Y < 0) 
            {
                Velocity = new Vector2(Velocity.X, 0);
            }
        }

        public override void OnCollision(GameObject other)
        {
            if ((other is Player || other is ShieldTile || other is Antibody) && !other.IsDead)
            {
                IsDead = true;
                if (other is Player)
                {
                    var player = (Player)other;
                    player.Damage();
                }
            }
        }
    }
}
