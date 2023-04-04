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
    internal abstract class Attack : GameObject
    {
        public Attack(Texture2D texture) : base(texture) 
        {
            _animationFrames = 2;
        }
    }
}
