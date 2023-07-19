using FirstGame.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Channels;

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
                if(unit.Owner != Game.CurrentPlayer) return false; //If the unit is not owned by the current player
                SelectUnit(unit);
                return true;
            } 
            else //If a unit is selected
            {
                if (selectedUnit.Owner != Game.CurrentPlayer) return false; //If the selected unit is not owned by the current player
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

        //https://gamedev.stackexchange.com/questions/54671/displaying-possible-movement-tiles 
        //public List<Point> calculatePossibleMoves(IUnit unit)
        //{
        //    int unitMoveRange = unit.MoveRange;
        //    Point indexOfUnit = GetPointFromUnit(unit);
        //    int x = indexOfUnit.X;
        //    int y = indexOfUnit.Y;
        //    int max_x = GameConstants.MAP_WIDTH - 1;
        //    int max_y = GameConstants.MAP_HEIGHT - 1;
        //    List<Point> result = new List<Point>();

        //    for (int dx = 0 - unitMoveRange; dx <= max_x; ++dx)
        //    {
        //        for (int dy = 0 - unitMoveRange; dy <= max_y; ++dy)
        //        {
        //            if (dx != 0 || dy != 0)
        //            {
        //                if (x + dx < 0 || x + dx > max_x || y + dy < 0 || y + dy > max_y) continue;
        //                if (Math.Abs(dx) + Math.Abs(dy) <= unitMoveRange)
        //                {
        //                    if (!IsValidMove(new Point(x + dx, y + dy))) continue;
        //                    result.Add(new Point(x + dx, y + dy));
        //                    //Debug.WriteLine("Adding point: " + (x + dx) + ", " + (y + dy));
        //                }
        //            }
        //        }
        //    }

        //    return result;
        //}

        public List<Point> CalculatePossibleMoves(IUnit unit)
        {
            int unitMoveRange = unit.MoveRange;
            Point indexOfUnit = GetPointFromUnit(unit);
            int x = indexOfUnit.X;
            int y = indexOfUnit.Y;
            int max_x = GameConstants.MAP_WIDTH - 1;
            int max_y = GameConstants.MAP_HEIGHT - 1;
            List<Point> result = new List<Point>();
            Queue<Point> queue = new Queue<Point>();
            bool[,] visited = new bool[GameConstants.MAP_WIDTH, GameConstants.MAP_HEIGHT];
            int[,] distance = new int[GameConstants.MAP_WIDTH, GameConstants.MAP_HEIGHT];

            queue.Enqueue(new Point(x, y));
            visited[x, y] = true;
            distance[x, y] = 0;

            while (queue.Count > 0)
            {
                Point currentPoint = queue.Dequeue();
                int currentX = currentPoint.X;
                int currentY = currentPoint.Y;

                // Define the four adjacent neighbors (up, down, left, right)
                int[] dxValues = { 0, 0, -1, 1 };
                int[] dyValues = { -1, 1, 0, 0 };

                for (int i = 0; i < dxValues.Length; i++)
                {
                    int dx = dxValues[i];
                    int dy = dyValues[i];

                    int newX = currentX + dx;
                    int newY = currentY + dy;

                    if (newX < 0 || newX > max_x || newY < 0 || newY > max_y) continue; // Skip points outside the map boundaries
                    if (visited[newX, newY]) continue; // Skip already visited points

                    int newDistance = distance[currentX, currentY] + 1;

                    if (newDistance > unitMoveRange) continue; // Skip points beyond the move range

                    if (IsValidMove(new Point(newX, newY)))
                    {
                        result.Add(new Point(newX, newY));
                        queue.Enqueue(new Point(newX, newY));
                        visited[newX, newY] = true;
                        distance[newX, newY] = newDistance;
                    }
                }
            }

            return result;
        }


        public void SelectUnit(IUnit unit)
        {
            selectedUnit = unit;
            selectedUnitPossibleMoves = CalculatePossibleMoves(unit);
            Game.WorldTiles.ChangeTilesColor(selectedUnitPossibleMoves, Color.Gray);
        }

        public void DeselectUnit()
        {
            selectedUnit = null;
            Game.WorldTiles.ChangeTilesColor(selectedUnitPossibleMoves, Color.White);
            selectedUnitPossibleMoves = null;
        }


        private bool IsValidMove(Point targetIndex)
        {
            if (unitMap.ContainsKey(targetIndex)) return false; //If there is a unit on the target tile

            ITile targetTile = Game.WorldTiles.GetTileAt(targetIndex);
            if (targetTile.TileType == TileType.water || targetTile.TileType == TileType.mountain) return false; //If the target tile is water or mountain

            return true;
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

        //Method that returns the key of a unit in the unitMap
        public Point GetPointFromUnit(IUnit unit)
        {
            return unitMap.FirstOrDefault(x => x.Value == unit).Key;
        }
    }
}