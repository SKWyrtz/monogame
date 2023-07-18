using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FirstGame.Interfaces
{
    public interface ITile
    {
        TileType TileType { get; set; }
        Texture2D TileTexture { get; set; }
        Rectangle DrawingBounds { get; set; }
        Color Color { get; set; }
        void Draw(SpriteBatch spriteBatch);
    }
}

