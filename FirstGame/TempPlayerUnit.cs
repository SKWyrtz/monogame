using FirstGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame
{
    internal class TempPlayerUnit : AbstractUnit, IUnit
    {
        public override UnitType UnitType { get; set; }
        public override Texture2D UnitTexture { get; set; }
        public override Rectangle DrawingBounds { get; set; }
        public TempPlayerUnit(Rectangle renderRectangle)
        {
            UnitType = UnitType.player;
            UnitTexture = Game.tempPlayerTexture;
            DrawingBounds = renderRectangle;
        }

        //public void Draw(SpriteBatch spriteBatch)
        //{
        //    base.Draw(spriteBatch);
        //}
    }
}