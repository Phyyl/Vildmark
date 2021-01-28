using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Vildmark.Graphics.GLObjects;

namespace Vildmark.Graphics.Rendering
{
    public class Texture2D
    {
        public GLTexture2D GLTexture { get; }

        public RectangleF SourceRectangle { get; }

        public int Width => (int)(GLTexture.Width * SourceRectangle.Width);

        public int Height => (int)(GLTexture.Height * SourceRectangle.Width);

        public Vector2 Size => new Vector2(Width, Height);

        public Texture2D(GLTexture2D glTexture, RectangleF sourceRectangle = default)
        {
            GLTexture = glTexture;
            SourceRectangle = sourceRectangle.IsEmpty ? new RectangleF(0, 0, 1, 1) : sourceRectangle;
        }

        public Texture2D(int width, int height)
             : this(new GLTexture2D(width, height))
        {
        }

        public Texture2D CreateSubTexture(RectangleF sourceRectangle)
        {
            sourceRectangle = new RectangleF(
                SourceRectangle.X + sourceRectangle.X * SourceRectangle.Width,
                SourceRectangle.Y + sourceRectangle.Y * SourceRectangle.Height,
                sourceRectangle.Width * SourceRectangle.Width,
                sourceRectangle.Height * SourceRectangle.Height);

            return new Texture2D(GLTexture, sourceRectangle);
        }

        public static implicit operator Texture2D(GLTexture2D texture)
        {
            return new Texture2D(texture);
        }
    }
}
