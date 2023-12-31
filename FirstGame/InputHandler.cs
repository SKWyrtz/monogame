﻿using FirstGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace FirstGame
{
    //Handling Input (Click on objects) https://community.monogame.net/t/handling-input-click-on-objects/8848
    internal class InputHandler { 

        public static MouseState curMouseState;
        public static MouseState prevMouseState;
        public static void HandleMouseclick(Point mousePos)
        {


            if (Game.UIManager.CheckMousClick(mousePos))
                return;

            //if (WorldObjects.CheckMousClick(mousePos))
            //    return;


            if (Game.WorldUnits.CheckMouseClick(mousePos)) //Select a unit if on mousePos
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

        public static bool MouseHasBeenPressedOnce()
        {
            if (curMouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                return true;
            else
                return false;
        }
    }
}