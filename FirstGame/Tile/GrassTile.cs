using FirstGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame.Tile
{
    internal class GrassTile : AbstractTile, ITile
    {
        public override TileType TileType { get; set; }
        public override Texture2D TileTexture { get; set; }
        public override Rectangle DrawingBounds { get; set; }
        public GrassTile(Rectangle renderRectangle)
        {
            TileType = TileType.grass;
            TileTexture = Game.grassTileTexture;
            DrawingBounds = renderRectangle;
        }
    }
}
