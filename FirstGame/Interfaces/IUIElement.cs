using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstGame.Interfaces
{
    public interface IUIELement
    { 
        Texture2D UITexture2D { get; set; }
        Rectangle DrawingBounds { get; set; }
        void Draw(SpriteBatch spriteBatch);

    }
}
