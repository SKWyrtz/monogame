using FirstGame.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace FirstGame
{
    internal class WorldUnits
    {
        public Dictionary<Vector2, IUnit> unitMap { get; set; } 

        public WorldUnits()
        {
            unitMap = new Dictionary<Vector2, IUnit>();
        }
        //public bool CheckMousClick(Point mouseWorldPos)
        //{
        //    foreach (var unit in unitMap.Values)
        //    {
        //        if (unit.BoundingBox.Contains(mousePos))
        //        {
        //            //npc.Interact();
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        internal void AddUnit(Vector2 position, IUnit unit)
        {
            unitMap.Add(position, unit);
        }
    }
}