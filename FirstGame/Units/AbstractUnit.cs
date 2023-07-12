using FirstGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame.Units
{
    internal abstract class AbstractUnit : IUnit
    {
        public abstract UnitType UnitType { get; set; }
        public abstract Texture2D UnitTexture { get; set; }
        public abstract Rectangle DrawingBounds { get; set; }
        public abstract Color Color { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                UnitTexture,
                DrawingBounds,
                null,
                Color,
                0f,
                origin: new Vector2(0,0),
                effects: SpriteEffects.None,
                layerDepth: 1f
            );
        }
    }
}