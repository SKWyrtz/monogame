using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace FirstGame
{
    internal class InputHandler { 

        public static MouseState curMouseState;
        public static MouseState prevMouseState;
        private static Point selectedUnitPos { get; set; }
        private static bool unitIsSelected { get; set; }
        public static void HandleMouseclick(Point mousePos)
        {
            //if (UIManager.CheckMousClick(mousePos))
            //    return;

            //if (WorldObjects.CheckMousClick(mousePos))
            //    return;

            if (Game.worldUnits.CheckMouseClick(mousePos))
            {
                return;
            }
        }

        public static MouseState GetMouseState()
        {
            prevMouseState = curMouseState;
            curMouseState = Mouse.GetState();
            return curMouseState;
        }

        //public static bool MouseIsPressed(Keys key)
        //{
        //    return curMouseState.IsKeyDown(key);
        //}

        public static bool MouseHasBeenPressedOnce()
        {
            if (curMouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                return true;
            else
                return false;
        }

        //public static bool IsLeftMouseClick()
        //{
        //    return curMouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released;
        //}
    }
}