using FirstGame.Interfaces;
using FirstGame.Units;
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

        public static WorldTiles worldTiles;
        public static WorldUnits worldUnits;

        public static Texture2D grassTileTexture;
        public static Texture2D waterTileTexture;
        public static Texture2D mountainTileTexture;
        public static Texture2D tempPlayerTexture;

        private Vector2 playerPos;
        public static bool unitIsSelected;

        public Game()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            worldTiles = new WorldTiles();
            worldUnits = new WorldUnits();


            int[,] worldLayout = new int[,] {
            {0,0,0,2,2,1,1,1,1,1,1,0,0,0,0 },
            {0,0,0,0,0,1,1,1,1,1,1,0,0,0,0 },
            {0,0,0,0,0,0,1,1,1,2,2,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,2,0,0,0,0,0,0,0,0 },
            {0,0,0,1,1,0,2,2,0,0,0,1,0,0,0 },
            {0,0,1,1,1,1,0,0,0,0,1,1,1,0,0 }
            };
            GameConstants.MAP_WIDTH = worldLayout.GetLength(1);
            GameConstants.MAP_HEIGHT = worldLayout.GetLength(0);

            base.Initialize();

            worldTiles.GenerateWorldTiles(worldLayout);

            unitIsSelected = false;
            Rectangle targetRenderRectangle = worldTiles.GetTile(new Point(0, 0)).DrawingBounds;
            worldUnits.AddUnit(new Point(0,0), new TempPlayerUnit(targetRenderRectangle));
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
            var kstate = Keyboard.GetState();
            var mouseState = Mouse.GetState();

            if (kstate.IsKeyDown(Keys.Down))
            {
                playerPos.Y++;
            }

            InputHandler.GetMouseState(); 
            if (InputHandler.MouseHasBeenPressedOnce())
            {
                InputHandler.HandleMouseclick(mouseState.Position);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
            //RasterizerState state = new RasterizerState();
            //state.FillMode = FillMode.WireFrame;
            //_spriteBatch.GraphicsDevice.RasterizerState = state;


            //Draw tiles
            foreach (KeyValuePair<Point, ITile> tile in worldTiles.tilesMap)
            {
                tile.Value.Draw(_spriteBatch);
            }

            //Draw units
            foreach (KeyValuePair<Point, IUnit> unit in worldUnits.unitMap)
            {
                unit.Value.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);

        }

    }
}