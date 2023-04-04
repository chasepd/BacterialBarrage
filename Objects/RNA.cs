﻿using MonoGame.Extended.Collisions;
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
        public override void OnCollision(CollisionEventArgs collisionEvent)
        {
            if(collisionEvent.Other is Player || collisionEvent.Other is ShieldTile || collisionEvent.Other is Antibody)  
                IsDead = true;
        }
    }
}
