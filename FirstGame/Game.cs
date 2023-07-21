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

        public static WorldTiles WorldTiles;
        public static WorldUnits WorldUnits;
        public static UIManager UIManager;

        public static Texture2D grassTileTexture;
        public static Texture2D waterTileTexture;
        public static Texture2D mountainTileTexture;
        public static Texture2D tempPlayerTexture;

        public static Texture2D nextTurnUITexture;

        public static Player CurrentPlayer;

        public Game()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();


            // TODO: Add your initialization logic here
            WorldTiles = new WorldTiles();
            WorldUnits = new WorldUnits();
            UIManager = new UIManager();


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

            base.Initialize(); //TODO: Where to put this?

            UIManager.GenerateUIElements();
            WorldTiles.GenerateWorldTiles(worldLayout);


            Rectangle targetRenderRectangle = WorldTiles.GetTileAt(new Point(3, 3)).DrawingBounds;
            WorldUnits.AddUnit(new Point(3,3), new TempPlayerUnit(WorldTiles.GetTileAt(new Point(3, 3)).DrawingBounds, Player.player2));
            WorldUnits.AddUnit(new Point(10, 6), new TempPlayerUnit(WorldTiles.GetTileAt(new Point(10, 6)).DrawingBounds, Player.player1));

            CurrentPlayer = Player.player1;
        }

        protected override void LoadContent()  //Initialize is called before LoadContent
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            grassTileTexture = Content.Load<Texture2D>("grass-tile");
            waterTileTexture = Content.Load<Texture2D>("water-tile");
            mountainTileTexture = Content.Load<Texture2D>("mountain-tile");
            tempPlayerTexture = Content.Load<Texture2D>("temp-player");

            nextTurnUITexture = Content.Load<Texture2D>("next-turn-button");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            var mouseState = Mouse.GetState();

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
            _spriteBatch.Begin(SpriteSortMode.FrontToBack);
            //RasterizerState state = new RasterizerState();
            //state.FillMode = FillMode.WireFrame;
            //_spriteBatch.GraphicsDevice.RasterizerState = state;


            //Draw UI
            foreach (KeyValuePair<Point, IUIELement> uiElement in UIManager.uiElementsMap)
            {
                uiElement.Value.Draw(_spriteBatch);
            }

            //Draw tiles
            foreach (KeyValuePair<Point, ITile> tile in WorldTiles.tilesMap)
            {
                tile.Value.Draw(_spriteBatch);
            }

            //Draw units
            foreach (KeyValuePair<Point, IUnit> unit in WorldUnits.unitMap)
            {
                unit.Value.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);

        }

        public static void NextTurn()
        {
            if (CurrentPlayer == Player.player1)
            {
                CurrentPlayer = Player.player2;
            }
            else
            {
                CurrentPlayer = Player.player1;
            }
            WorldUnits.ResetUnitActions();
        }

    }
}