﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BacterialBarrage.Objects
{
    internal class BacteriaLevel1 : Germ
    {
        public BacteriaLevel1(Texture2D texture) : base(texture)
        {
            PointValue = 10;
        }
    }
}
