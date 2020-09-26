using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Models;
using Vildmark.Maths;

namespace Vildmark.Graphics.Rendering
{
    public class TextureAtlas
    {
        private readonly Vector2 tileSize;

        public GLTexture2D Texture { get; }

        public TextureAtlas(GLTexture2D texture, int xTiles, int yTiles)
        {
            Texture = texture;

            tileSize = new Vector2(texture.Width / xTiles, texture.Height / yTiles);
        }

        public Texture2D this[int x, int y] => new Texture2D(Texture, new Vector4(x * tileSize.X, y * tileSize.Y, tileSize.X, tileSize.Y).ToRectangleF());
    }
}
