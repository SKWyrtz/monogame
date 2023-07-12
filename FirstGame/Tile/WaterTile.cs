using FirstGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame.Tile
{
    internal class WaterTile : AbstractTile, ITile
    {
        public override TileType TileType { get; set; }
        public override Texture2D TileTexture { get; set; }
        public override Rectangle DrawingBounds { get; set; }
        public WaterTile(Rectangle renderRectangle)
        {
            TileType = TileType.water;
            TileTexture = Game.waterTileTexture;
            DrawingBounds = renderRectangle;
        }
    }
}
