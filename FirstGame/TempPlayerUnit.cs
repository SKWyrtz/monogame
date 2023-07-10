using FirstGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame
{
    internal class TempPlayerUnit : IUnit
    {
        public UnitType UnitType { get; set; }
        public Texture2D UnitTexture { get; set; }
        public Rectangle DrawingBounds { get; set; }
        public TempPlayerUnit(Rectangle renderRectangle)
        {
            UnitType = UnitType.player;
            UnitTexture = Game.tempPlayerTexture;
            DrawingBounds = renderRectangle;
        }

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