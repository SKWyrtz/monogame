using FirstGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame
{
    internal class MountainTile : ITile
    {
        public TileType TileType { get; set; }
        public Texture2D TileTexture { get; set; }
        public Rectangle RenderRectangle { get; set; }
        public MountainTile(Rectangle renderRectangle, Texture2D mountainTileTexture)
        {
            TileType = TileType.mountain;
            TileTexture = mountainTileTexture;
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