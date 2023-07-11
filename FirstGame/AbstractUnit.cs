using FirstGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame
{
    internal abstract class AbstractUnit : IUnit
    {
        public abstract UnitType UnitType { get; set; }
        public abstract Texture2D UnitTexture { get; set; }
        public abstract Rectangle DrawingBounds { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                UnitTexture,
                DrawingBounds,
                null,
                Color.White,
                0f,
                origin: new Vector2(UnitTexture.Width / 2, UnitTexture.Height / 2),
                effects: SpriteEffects.None,
                layerDepth: 1f
            );
        }
    }
}