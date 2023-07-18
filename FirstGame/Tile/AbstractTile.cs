using FirstGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame.Tile
{
    internal abstract class AbstractTile : ITile
    {
        public abstract TileType TileType {get;set;}
        public abstract Texture2D TileTexture { get; set; }
        public abstract Rectangle DrawingBounds { get; set; }
        public abstract Color Color { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                    texture: TileTexture,
                    destinationRectangle: DrawingBounds,
                    null,
                    color: Color,
                    rotation: 0f,
                    origin: new Vector2(0, 0),
                    effects: SpriteEffects.None,
                    layerDepth: 0f
                );
        }
    }
}
