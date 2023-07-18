using FirstGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstGame.UI
{
    internal abstract class AbstractUIElement : IUIELement
    {
        public abstract Texture2D UITexture2D { get; set; }
        public abstract Rectangle DrawingBounds { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
        spriteBatch.Draw(
            texture: UITexture2D,
            destinationRectangle: DrawingBounds,
            null,
            color: Color.White,
            rotation: 0f,
            origin: new Vector2(0, 0),
            effects: SpriteEffects.None,
            layerDepth: 1f
        );
        }
    }
}
