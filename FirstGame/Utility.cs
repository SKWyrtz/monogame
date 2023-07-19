using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstGame
{
    internal class Utility
    {
        public static Color GetPlayerColor(Player player)
        {
            switch (player)
            {
                case Player.player1:
                    return Color.Blue;
                case Player.player2:
                    return Color.Red;
                default:
                    return Color.White;
            }
        }
    }
}
