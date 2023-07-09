using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstGame.Interfaces
{
    public interface ITile
    {
        TileType TileType { get; set; }
        Texture2D TileTexture { get; set; }
    }
}

