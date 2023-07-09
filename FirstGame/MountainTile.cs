using FirstGame.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame
{
    internal class MountainTile : ITile
    {
        public TileType TileType { get; set; }
        public Texture2D TileTexture { get; set; }
        public MountainTile(Texture2D mountainTileTexture)
        {
            TileType = TileType.mountain;
            TileTexture = mountainTileTexture;
        }

    }
}