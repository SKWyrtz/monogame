using FirstGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame.Tile
{
    internal class MountainTile : AbstractTile, ITile
    {
        public override TileType TileType { get; set; }
        public override Texture2D TileTexture { get; set; }
        public override Rectangle DrawingBounds { get; set; }
        public override Color Color { get; set; }

        public MountainTile(Rectangle renderRectangle)
        {
            TileType = TileType.mountain;
            TileTexture = Game.mountainTileTexture;
            DrawingBounds = renderRectangle;
            Color = Color.White;
        }

    }
}