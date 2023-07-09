using FirstGame.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame
{
    internal class GrassTile : ITile
    {
        public TileType TileType { get; set; }
        public Texture2D TileTexture { get; set; }

        public GrassTile(Texture2D grassTileTexture)
        {
            TileType = TileType.grass;
            TileTexture = grassTileTexture;
        }
        
    }
}
