using Microsoft.Xna.Framework;
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
        private int _screenWidth;
        public Virus(Texture2D texture, int screenWidth) : base(texture)
        {
            PointValue = 100;
            AttackChance = 0.0030;
            _screenWidth = screenWidth;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Velocity = new Vector2(1000 * Scale.X, 0);

            if(Position.X > _screenWidth)
                IsDead = true;
        }
    }
}
