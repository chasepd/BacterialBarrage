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
    internal class ShieldTile : GameObject
    {
        public ShieldTile(Texture2D texture) : base(texture) 
        {
            Velocity = new Vector2 (0, 0);
        }
        public override void OnCollision(CollisionEventArgs collisionEvent)
        {
            if (collisionEvent.Other is RNA || collisionEvent.Other is Germ)
                IsDead = true;                            
        }
    }
}
