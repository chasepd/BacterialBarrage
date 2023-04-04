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
        public RNA(Texture2D texture) : base(texture)
        {
            Velocity = new Vector2(0, 5);
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
