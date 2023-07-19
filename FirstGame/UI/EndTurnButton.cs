using FirstGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstGame.UI
{
    internal class EndTurnButton : AbstractUIElement, IUIELement
    {
        public override Texture2D UITexture2D {get; set;}
        public override Rectangle DrawingBounds { get; set; }

        public EndTurnButton(Rectangle renderRectangle)
        {
            UITexture2D = Game.nextTurnUITexture;
            DrawingBounds = renderRectangle;
        }

        public override void Activate()
        {
            Game.NextTurn();
            Debug.WriteLine("Next turn button activated");
        }
    }
}
