using FirstGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame
{
    internal class WaterTile : ITile
    {
        public TileType TileType { get; set; }
        public Texture2D TileTexture { get; set; }
        public Rectangle RenderRectangle { get; set; }
        public WaterTile(Rectangle renderRectangle, Texture2D waterTileTexture)
        {
            TileType = TileType.water;
            TileTexture = waterTileTexture;
            RenderRectangle = renderRectangle;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                    texture: TileTexture,
                    destinationRectangle: RenderRectangle, //TODO: Casting might create trouble? something with "snapped"
                    null,
                    color: Color.White,
                    rotation: 0f,
                    origin: new Vector2(TileTexture.Width / 2, TileTexture.Height / 2),
                    effects: SpriteEffects.None,
                    layerDepth: 0f
                );
        }
    }
}
