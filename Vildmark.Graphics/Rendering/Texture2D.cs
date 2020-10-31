using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Vildmark.Graphics.GLObjects;

namespace Vildmark.Graphics.Rendering
{
    public class Texture2D
    {
        public GLTexture2D Texture { get; }
        public RectangleF Rectangle { get; }

        public float Width => Rectangle.Width;
        public float Height => Rectangle.Height;

        public Texture2D(GLTexture2D texture, RectangleF? rectangle = default)
        {
            Texture = texture;
            Rectangle = rectangle ?? new RectangleF(0, 0, texture.Width, texture.Height);
        }
    }
}
