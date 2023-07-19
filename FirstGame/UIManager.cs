using FirstGame.Interfaces;
using FirstGame.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace FirstGame
{
    public class UIManager
    {
        public Dictionary<Point, IUIELement> uiElementsMap { get; set; }

        public UIManager()
        {
            uiElementsMap = new Dictionary<Point, IUIELement>{};
        }

        internal void GenerateUIElements()
        {
            uiElementsMap.Add(new Point(100, 100), new NextTurnButton(new Rectangle(100, 100, 100, 100)));
        }
        internal bool CheckMousClick(Point mousePos)
        {
            throw new NotImplementedException();
        }
    }
}