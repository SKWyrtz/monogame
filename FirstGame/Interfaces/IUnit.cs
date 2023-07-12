using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame.Interfaces
{
    public interface IUnit
    {
        UnitType UnitType { get; set; } 
        Texture2D UnitTexture { get; set; }
        Rectangle DrawingBounds { get; set; }
        Color Color { get; set; }

        void Draw(SpriteBatch spriteBatch);
    }

}