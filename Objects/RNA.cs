using MonoGame.Extended.Collisions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace BacterialBarrage.Objects
{
    internal class RNA : Attack
    {
        private int _screenHeight;
        public RNA(Texture2D texture, int screenHeight) : base(texture)
        {
            Velocity = new Vector2(0, 2000 * Scale.Y);
            _screenHeight = screenHeight;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Velocity = new Vector2(0, 2000 * Scale.Y);

            if (Position.Y > _screenHeight)
            {
                IsDead = true;
            }

        }
        public override void OnCollision(GameObject other)
        {
            if (!IsDead)
            {                
                if (other is Player || other is ShieldTile || other is Antibody)
                {
                    IsDead = true;
                    if (other is Player)
                    {
                        var player = (Player)other;
                        player.Damage();
                    }
                    else
                    {
                        other.IsDead = true;
                    }
                }
            }
        }
    }
}
