using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BacterialBarrage.Objects
{
    internal class BacteriaLevel3 : Germ
    {
        public BacteriaLevel3(Texture2D texture) : base(texture)
        {
            PointValue = 30;
            AttackChance = 0.0015;
        }
    }
}
