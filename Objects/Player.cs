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

        public void Damage()
        {
            LivesRemaining--;
            if (LivesRemaining <= 0)
            {
                IsDead = true;
            }
        }

        public void AddScore (int score)
        {
            Score += score;
        }

        public override void OnCollision(GameObject other)
        {
            if ((other is Germ || other is RNA) && !other.IsDead)
            {
                Damage();   
                other.IsDead = true;
            }
        }
    }
}
