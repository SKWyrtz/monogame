using FirstGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FirstGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Dictionary<Vector2, ITile> tilesMap;

        Texture2D grassTileTexture;
        Texture2D waterTileTexture;
        Texture2D mountainTileTexture;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            tilesMap = new Dictionary<Vector2, ITile>();

            int[,] map = new int[,] {
            {0,0,0,2,2,1,1,1,1,1,1,0,0,0,0 },
            {0,0,0,0,0,1,1,1,1,1,1,0,0,0,0 },
            {0,0,0,0,0,0,1,1,1,2,2,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,2,0,0,0,0,0,0,0,0 },
            {0,0,0,1,1,0,2,2,0,0,0,1,0,0,0 },
            {0,0,1,1,1,1,0,0,0,0,1,1,1,0,0 }
            };
            GameConstants.MAP_WIDTH = map.GetLength(1);
            GameConstants.MAP_HEIGHT = map.GetLength(0);

            base.Initialize();

            GenerateMap(map);
        }

        protected override void LoadContent()  //Initialize is called before LoadContent
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            grassTileTexture = Content.Load<Texture2D>("grass-tile");
            waterTileTexture = Content.Load<Texture2D>("water-tile");
            mountainTileTexture = Content.Load<Texture2D>("mountain-tile");
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            foreach (KeyValuePair<Vector2, ITile> entry in tilesMap)
            {
                float xPos = entry.Key.X;
                float yPos = entry.Key.Y;
                ITile tile = entry.Value;
                Texture2D texture = tile.TileTexture;
                
                int boardWidth = _graphics.PreferredBackBufferWidth - 50;
                int boardHeight = _graphics.PreferredBackBufferHeight - 50;
                int tileWidth = boardWidth / GameConstants.MAP_WIDTH;
                //int tileHeight = boardHeight / GameConstants.MAP_HEIGHT; //måske ikke bruges
                int renderXPos = (int)(25 + tileWidth / 2 + (xPos * tileWidth));
                int renderYPos = (int)(tileWidth / 2 + (yPos * tileWidth));
                _spriteBatch.Draw(
                    texture: texture,
                    destinationRectangle: new Rectangle(renderXPos, renderYPos, tileWidth - 2, tileWidth - 2), //TODO: Casting might create trouble? something with "snapped"
                    null,
                    color: Color.White,
                    rotation: 0f,
                    origin: new Vector2(texture.Width / 2, texture.Height / 2),
                    effects: SpriteEffects.None,
                    layerDepth: 0f
                );
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void GenerateMap(int[,] map)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    Vector2 position = new Vector2(x, y);

                    ITile tile;
                    int tileType = map[y, x];
                    switch (tileType)
                    {
                        case (int)TileType.grass:
                            tile = new GrassTile(grassTileTexture);
                            break;
                        case (int)TileType.water:
                            tile = new WaterTile(waterTileTexture);
                            break;
                        case (int)TileType.mountain:
                            tile = new MountainTile(mountainTileTexture);
                            break;
                        default:
                            tile = new GrassTile(grassTileTexture);
                            break;
                    }
                    tilesMap.Add(position, tile);
                }
            }
        }

        internal ITile GetTile(Vector2 position)
        {
            return tilesMap[position];
        }

        internal void AddTile(Vector2 position, ITile tile)
        {
            tilesMap.Add(position, tile);
        }
    }
}