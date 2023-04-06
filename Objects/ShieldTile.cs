using MonoGame.Extended.Collisions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace BacterialBarrage.Objects
{
    internal class ShieldTile : GameObject
    {
        public Vector2 ArrayPosition { get; set; }
        public override RectangleF Bounds => new RectangleF(Position.X, Position.Y, Scale.X, Scale.Y);
        public override Rectangle SourceRectangle => new Rectangle(0, 0, 1, 1);
        private Shield _parentShieldArray;
        public ShieldTile(Texture2D texture, Vector2 tilePosition, Shield parent) : base(texture) 
        {
            Velocity = Vector2.Zero;
            ArrayPosition = tilePosition;
            _parentShieldArray = parent;
        }

        public override void Update(GameTime gameTime) { }
        public override void OnCollision(GameObject other)
        {            
            if ((other is RNA || other is Germ) && !other.IsDead)
            {
                IsDead = true;
                other.IsDead = true;
            }
        }
    }
}
