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

        public int Width => GLTexture.Width;

        public int Height => GLTexture.Height;

        public Vector2 Size => GLTexture.Size;

        public Texture2D(GLTexture2D glTexture, RectangleF sourceRectangle = default)
        {
            GLTexture = glTexture;
            SourceRectangle = sourceRectangle.IsEmpty ? new RectangleF(0, 0, 1, 1) : sourceRectangle;
        }

        public Texture2D(int width, int height)
             : this(new GLTexture2D(width, height))
        {
        }

        public static implicit operator Texture2D(GLTexture2D texture)
        {
            return new Texture2D(texture);
        }
    }
}
