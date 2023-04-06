using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BacterialBarrage.Objects
{
    internal class BacteriaLevel2 : Germ
    {
        public BacteriaLevel2(Texture2D texture) : base(texture)
        {
            PointValue = 20;
            AttackChance = 0.001;
        }
    }
}
