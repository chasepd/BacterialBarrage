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
        public Antibody(Texture2D texture) : base(texture)
        {
            Velocity = new Vector2 (0, -5);
        }
        public override void OnCollision(CollisionEventArgs collisionEvent) 
        {
            if(collisionEvent.Other is Germ || collisionEvent.Other is RNA)            
                IsDead = true;           
        }
    }
}
