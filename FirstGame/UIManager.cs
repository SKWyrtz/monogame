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
            uiElementsMap.Add(new Point(100, 100), new EndTurnButton(new Rectangle(Game._graphics.PreferredBackBufferWidth - 150, Game._graphics.PreferredBackBufferHeight - 250, 100, 100)));
        }
        internal bool CheckMousClick(Point mousePos)
        {
            IUIELement uiElement = GetUIElementFromMouseClick(mousePos);
            if (uiElement == null) return false;
            uiElement.Activate();
            return true;
        }

        private IUIELement GetUIElementFromMouseClick(Point mousePos)
        {
            foreach (var uiElement in uiElementsMap.Values)
            {
                if (uiElement.DrawingBounds.Contains(mousePos.X, mousePos.Y))
                {
                    return uiElement;
                }
            }
            return null;
        }
    }
}