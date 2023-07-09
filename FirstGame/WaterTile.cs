using FirstGame.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstGame
{
    internal class WaterTile : ITile
    {
        public TileType TileType { get; set; }
        public Texture2D TileTexture { get; set; }

        public WaterTile(Texture2D waterTileTexture)
        {
            TileType = TileType.water;
            TileTexture = waterTileTexture;
        }

    }
}
