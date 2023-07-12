using FirstGame.Interfaces;
using FirstGame.Tile;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FirstGame
{
    public class WorldTiles
    {
        public Dictionary<Point, ITile> tilesMap { get; set;}

        public WorldTiles()
        {
            tilesMap = new Dictionary<Point, ITile>();    
        }

        public void GenerateWorldTiles(int[,] map)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    Point position = new Point(x, y);

                    float xPos = x;
                    float yPos = y;

                    int boardWidth = Game._graphics.PreferredBackBufferWidth - 50;
                    int boardHeight = Game._graphics.PreferredBackBufferHeight - 50;
                    int tileWidth = boardWidth / GameConstants.MAP_WIDTH;
                    //int tileHeight = boardHeight / GameConstants.MAP_HEIGHT; //måske ikke bruges
                    int renderXPos = (int)(25 + tileWidth / 2 + (xPos * tileWidth));
                    int renderYPos = (int)(tileWidth / 2 + (yPos * tileWidth));
                    Rectangle renderRectangle = new Rectangle(renderXPos, renderYPos, tileWidth - 2, tileWidth - 2);

                    ITile tile;
                    int tileType = map[y, x];
                    switch (tileType)
                    {
                        case (int)TileType.grass:
                            tile = new GrassTile(renderRectangle);
                            break;
                        case (int)TileType.water:
                            tile = new WaterTile(renderRectangle);
                            break;
                        case (int)TileType.mountain:
                            tile = new MountainTile(renderRectangle);
                            break;
                        default:
                            tile = new GrassTile(renderRectangle);
                            break;
                    }
                    tilesMap.Add(position, tile);
                }
            }
        }

        internal ITile GetTile(Point position)
        {
            if (tilesMap.TryGetValue(position, out ITile tile)) {
                return tile;
            } else
            {
                Debug.WriteLine("Tile not found - returning null");
                return null;
            }
        }

        internal ITile GetTileFromMouseClick(Point mousePos)
        {
            foreach (var tile in tilesMap.Values)
            {
                if (tile.DrawingBounds.Contains(mousePos.X, mousePos.Y))
                {
                    return tile;
                }
            }
            return null;
        }
    }
}
