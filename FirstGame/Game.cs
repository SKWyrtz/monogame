using FirstGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FirstGame
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        public static GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private TileMap tilesMap;
        //private Dictionary<Vector2, ITile> tilesMap;

        public static Texture2D grassTileTexture;
        public static Texture2D waterTileTexture;
        public static Texture2D mountainTileTexture;
        public static Texture2D tempPlayerTexture;

        public Game()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //tilesMap = new Dictionary<Vector2, ITile>();
            tilesMap = new TileMap();

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

            tilesMap.GenerateMap(map);
        }

        protected override void LoadContent()  //Initialize is called before LoadContent
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            grassTileTexture = Content.Load<Texture2D>("grass-tile");
            waterTileTexture = Content.Load<Texture2D>("water-tile");
            mountainTileTexture = Content.Load<Texture2D>("mountain-tile");
            tempPlayerTexture = Content.Load<Texture2D>("temp-player");
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
            foreach (KeyValuePair<Vector2, ITile> tile in tilesMap.tilesMapDictionary)
            {
                tile.Value.Draw(_spriteBatch);
            }
            Rectangle targetRenderRectangle = tilesMap.GetTile(new Vector2(0, 0)).RenderRectangle;
            _spriteBatch.Draw(
                tempPlayerTexture,
                targetRenderRectangle,
                null,
                Color.White,
                0f,
                origin: new Vector2(tempPlayerTexture.Width / 2, tempPlayerTexture.Height / 2),
                effects: SpriteEffects.None,
                layerDepth: 1f
            );
            _spriteBatch.End();

            base.Draw(gameTime);

        }

    }
}