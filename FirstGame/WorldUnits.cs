using FirstGame.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FirstGame
{
    public class WorldUnits
    {
        public Dictionary<Point, IUnit> unitMap { get; set; }
        public IUnit selectedUnit { get; set; }

        public WorldUnits()
        {
            unitMap = new Dictionary<Point, IUnit>();
        }

        public bool CheckMouseClick(Point mousePos)
        {
            if(selectedUnit == null) //If no unit is selected
            {
                IUnit unit = GetUnitFromMouseClick(mousePos);
                if (unit == null) return false;
                SelectUnit(unit);
                Debug.WriteLine("Unit clicked");
                return true;
            } 
            else //If a unit is selected
            {
                if (selectedUnit.DrawingBounds.Contains(mousePos)) //If the selected unit is clicked again
                {
                    DeselectUnit();
                    return true;
                }
                else //Move the selected unit to the mousePos
                {
                    ITile targetTile = Game.worldTiles.GetTileFromMouseClick(mousePos); //Check if the mousePos is a valid tile
                    if (targetTile == null) return false;
                    Rectangle targetRenderRectangle = targetTile.DrawingBounds;
                    Game.worldUnits.MoveUnit(targetRenderRectangle);
                    return true;
                }
            }
        }

        public IUnit GetUnitFromMouseClick(Point mousePos)
        {
            foreach (var unit in unitMap.Values)
            {
                if (unit.DrawingBounds.Contains(mousePos.X, mousePos.Y))
                {
                    return unit;
                }
            }
            return null;
        }

        internal void AddUnit(Point position, IUnit unit)
        {
            unitMap.Add(position, unit);
        }

        internal void MoveUnit(Rectangle targetRenderRectangle)
        {
            selectedUnit.DrawingBounds = targetRenderRectangle;
            selectedUnit.Color = Color.White;
            selectedUnit = null;
        }

        internal void SelectUnit(IUnit unit)
        {
            selectedUnit = unit;
            selectedUnit.Color = Color.Gray;
        }

        internal void DeselectUnit()
        {
            if (selectedUnit == null) return;
            selectedUnit.Color = Color.White;
            selectedUnit = null;
        }
    }
}