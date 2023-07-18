using FirstGame.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FirstGame
{
    public class WorldUnits
    {
        private List<Point> selectedUnitPossibleMoves;

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
                    ITile targetTile = Game.WorldTiles.GetTileFromMouseClick(mousePos); //Check if the mousePos is a valid tile
                    if (targetTile == null) return false;
                    Point targetIndex = Game.WorldTiles.GetPointFromTile(targetTile);
                    MoveUnit(selectedUnit, targetIndex);
                    return true;
                }
            }
        }

        public List<Point> calculatePossibleMoves(IUnit unit)
        {
            int unitMoveRange = unit.MoveRange;
            Point indexOfUnit = GetPointFromUnit(unit);
            int x = indexOfUnit.X;
            int y = indexOfUnit.Y;
            int max_x = GameConstants.MAP_WIDTH - 1;
            int max_y = GameConstants.MAP_HEIGHT - 1;
            List<Point> result = new List<Point>();

            for (int dx = 0 - unitMoveRange; dx <= max_x; ++dx)
            {
                for (int dy = 0 - unitMoveRange; dy <= max_y; ++dy)
                {
                    if (dx != 0 || dy != 0)
                    {
                        if (x+dx < 0 || x+dx > max_x || y+dy < 0 || y+dy > max_y) continue;
                        if (Math.Abs(dx) + Math.Abs(dy) <= unitMoveRange)
                        {
                            if (!IsValidMove(new Point(x + dx, y + dy))) continue;
                            result.Add(new Point(x + dx, y + dy));
                            //Debug.WriteLine("Adding point: " + (x + dx) + ", " + (y + dy));
                        }
                    }
                }
            }   

            return result;
        }

        private bool IsValidMove(Point targetIndex)
        {

            if (unitMap.ContainsKey(targetIndex)) return false; //If there is a unit on the target tile

            ITile targetTile = Game.WorldTiles.GetTileAt(targetIndex);
            if (targetTile.TileType == TileType.water || targetTile.TileType == TileType.mountain) return false; //If the target tile is water or mountain

            return true;    
            //return selectedUnitPossibleMoves.Contains(targetIndex);
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

        internal void MoveUnit(IUnit unit, Point targetIndex)
        {
            if (!selectedUnitPossibleMoves.Contains(targetIndex)) return;
            Point curIndex = GetPointFromUnit(unit);
            ITile targetTile = Game.WorldTiles.GetTileAt(targetIndex);

            IUnit unitValue = unitMap[curIndex];
            unitMap.Remove(curIndex);
            unitMap[targetIndex] = unitValue;

            selectedUnit.DrawingBounds = targetTile.DrawingBounds;
            DeselectUnit();
        }

        internal void SelectUnit(IUnit unit)
        {
            selectedUnit = unit;
            selectedUnit.Color = Color.Gray;
            selectedUnitPossibleMoves = calculatePossibleMoves(unit);
            Game.WorldTiles.ChangeTilesColor(selectedUnitPossibleMoves, Color.Gray);
        }

        internal void DeselectUnit()
        {
            if (selectedUnit == null) return;
            Game.WorldTiles.ChangeTilesColor(selectedUnitPossibleMoves, Color.White);
            selectedUnit.Color = Color.White;
            selectedUnit = null;
        }

        //Method that returns the key of a unit in the unitMap
        public Point GetPointFromUnit(IUnit unit)
        {
            return unitMap.FirstOrDefault(x => x.Value == unit).Key;
        }
    }
}