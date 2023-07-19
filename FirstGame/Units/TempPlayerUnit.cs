using FirstGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame.Units
{
    internal class TempPlayerUnit : AbstractUnit, IUnit
    {
        public override UnitType UnitType { get; set; }
        public override Texture2D UnitTexture { get; set; }
        public override Rectangle DrawingBounds { get; set; }
        public override Color Color { get; set; }
        public override int MoveRange { get; set; }
        public override Player Owner { get; set; }

        public TempPlayerUnit(Rectangle renderRectangle, Player owner)
        {
            UnitType = UnitType.player;
            UnitTexture = Game.tempPlayerTexture;
            DrawingBounds = renderRectangle;
            MoveRange = GameConstants.DEFAULT_MOVE_RANGE;
            Owner = owner;
            Color = Utility.GetPlayerColor(owner);
        }
    }
}