using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BacterialBarrage.Objects
{
    internal class Virus : Germ
    {
        public Virus(Texture2D texture) : base(texture)
        {
            PointValue = 100;
        }
    }
}
