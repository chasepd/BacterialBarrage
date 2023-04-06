using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BacterialBarrage.Objects
{
    internal class Shield : GameObject
    {
        private List<List<bool>> _shieldTiles = new List<List<bool>>(){
            new List<bool>() { false, false, false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, false, false, false },
            new List<bool>() { false, false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, false, false },
            new List<bool>() { false, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, false },
            new List<bool>() { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true },
            new List<bool>() { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true },
            new List<bool>() { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true },
            new List<bool>() { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true },
            new List<bool>() { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true },
            new List<bool>() { true, true, true, true, true, true, true, true, true, true, false, false, false, false, true, true, true, true, true, true, true, true, true, true },
            new List<bool>() { true, true, true, true, true, true, true, true, true, false, false, false, false, false, false, true, true, true, true, true, true, true, true, true },
            new List<bool>() { true, true, true, true, true, true, true, true, false, false, false, false, false, false, false, false, true, true, true, true, true, true, true, true },
            new List<bool>() { true, true, true, true, true, true, true, false, false, false, false, false, false, false, false, false, false, true, true, true, true, true, true, true },
            new List<bool>() { true, true, true, true, true, true, false, false, false, false, false, false, false, false, false, false, false, false, true, true, true, true, true, true },
            new List<bool>() { true, true, true, true, true, true, false, false, false, false, false, false, false, false, false, false, false, false, true, true, true, true, true, true },
            new List<bool>() { true, true, true, true, true, true, false, false, false, false, false, false, false, false, false, false, false, false, true, true, true, true, true, true },
            new List<bool>() { true, true, true, true, true, true, false, false, false, false, false, false, false, false, false, false, false, false, true, true, true, true, true, true },
            new List<bool>() { true, true, true, true, true, true, false, false, false, false, false, false, false, false, false, false, false, false, true, true, true, true, true, true },
            new List<bool>() { true, true, true, true, true, true, false, false, false, false, false, false, false, false, false, false, false, false, true, true, true, true, true, true },
        };

        public List<ShieldTile> ShieldTiles { get; private set; }

        public Shield(Texture2D texture, Vector2 position, float scale) : base(texture) 
        {
            Position = position;
            Scale = new Vector2(scale, scale);
            InitializeTiles();
        }

        public override void OnCollision(GameObject otherObj) { }

        public void KillTile(ShieldTile tile)
        {
            _shieldTiles[(int)tile.ArrayPosition.Y][(int)tile.ArrayPosition.X] = false;
            ShieldTiles.Remove(tile);
        }

        private void InitializeTiles()
        {
            ShieldTiles = new List<ShieldTile>();

            Vector2 position = new Vector2(Position.X - (_shieldTiles.First().Count / 2 * Scale.X), Position.Y - (_shieldTiles.Count / 2 * Scale.Y));

            for (int shieldRowIndex = 0; shieldRowIndex < _shieldTiles.Count; shieldRowIndex++)
            {
                var shieldRow = _shieldTiles[shieldRowIndex];
                for (int shieldTileIndex = 0; shieldTileIndex < shieldRow.Count; shieldTileIndex++)
                {
                    var shieldTile = shieldRow[shieldTileIndex];
                    if (shieldTile)
                    {
                        ShieldTiles.Add(new ShieldTile(Texture,
                            new Vector2(shieldTileIndex, shieldRowIndex),
                            this)
                        {
                            Position = position,
                            Scale = this.Scale,
                            Rotation = 0f
                        });
                    }
                    position.X += Scale.X;
                }
                position.X = Position.X - (_shieldTiles.First().Count / 2 * Scale.X);
                position.Y += Scale.Y;
            }
        }

    }
}
