﻿using Microsoft.Xna.Framework;
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

        public Germ(Texture2D texture) : base(texture)
        {
            _animationFrames = 5;
        }


        public override void OnCollision(CollisionEventArgs collisionEvent)
        {
            if(collisionEvent.Other is Antibody || collisionEvent.Other is ShieldTile)
                IsDead = true;
        }
    }
}
