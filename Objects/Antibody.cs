using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BacterialBarrage.Objects
{
    internal class Antibody : Attack
    {
        public Player Player { get; set; }
        public Antibody(Texture2D texture) : base(texture) { }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if(Velocity.Y >= 0)
                Velocity = new Vector2(0, -4000 * Scale.Y);

            if (Position.X < 0)            
                IsDead = true;
            
        }
        public override void OnCollision(GameObject other) 
        {
            if (!IsDead)
            {
                if (other is Germ || other is RNA)
                {
                    IsDead = true;
                    other.IsDead = true;
                    if (other is Germ)
                    {
                        Germ germ = (Germ)other;
                        Player.AddScore(germ.PointValue);
                    }

                }
            }
        }
    }
}
