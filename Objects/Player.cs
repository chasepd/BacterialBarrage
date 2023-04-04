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
    internal class Player : GameObject
    {
        public int Score { get; set; }        
        public int LivesRemaining { get; private set; }

        public Player(Texture2D texture) : base(texture) {
            Score = 0;
            LivesRemaining = 3;
            _animationFrames = 16;
        }

        public override void OnCollision(CollisionEventArgs collisionEvent)
        {
            if((collisionEvent.Other is Germ || collisionEvent.Other is RNA) && LivesRemaining <= 0)            
                IsDead = true;
            
        }
    }
}
