using FirstGame.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FirstGame
{
    public class WorldUnits
    {
        public Dictionary<Vector2, IUnit> unitMap { get; set; }
        public IUnit selectedUnit { get; set; }

        public WorldUnits()
        {
            unitMap = new Dictionary<Vector2, IUnit>();
        }

        public bool CheckMouseClick(Point mouseWorldPos)
        {
            foreach (var unit in unitMap.Values)
            {
                if (unit.DrawingBounds.Contains(mouseWorldPos.X, mouseWorldPos.Y))
                {
                    //npc.Interact();
                    selectedUnit = unit;
                    selectedUnit.DrawingBounds.Inflate(5, 5);
                    Debug.WriteLine("Unit clicked");
                    return true;
                }
            }
            return false;
        }

        internal void AddUnit(Vector2 position, IUnit unit)
        {
            unitMap.Add(position, unit);
        }
    }
}