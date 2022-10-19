using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Runtime.InteropServices;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Textures.Loaders;
using Vildmark.Resources;

namespace Vildmark.Graphics.Textures;

[ResourceLoader(typeof(Texture2DResourceLoader))]
public class Texture2D
{
    public static Texture2D WhitePixel { get; } = new(new GLTexture2D(1, 1, new byte[] { 255, 255, 255, 255 }));
    public static Texture2D TransparentPixel { get; } = new(new GLTexture2D(1, 1, new byte[] { 0, 0, 0, 0 }));

    internal GLTexture2D GLTexture { get; }

    public virtual int Width => GLTexture.Width;
    public virtual int Height => GLTexture.Height;

    public Vector2 Size => new(Width, Height);
    public Box2 Bounds => new(0, 0, Width, Height);

    public Texture2D(int width, int height, Texture2DParameters? parameters = default)
         : this(new GLTexture2D(width, height, default, parameters))
    {
    }

    internal Texture2D(GLTexture2D glTexture)
    {
        GLTexture = glTexture;
    }

    public void UpdateData<T>(int x, int y, int width, int height, Span<T> data)
        where T : unmanaged
    {
        GLTexture.UpdateData(x, y, width, height, data);
    }

    public void SetData<T>(int width, int height, Span<T> data)
        where T : unmanaged
    {
        GLTexture.SetData(width, height, data);
    }
}

public class SubTexture2D : Texture2D
{
    public Box2 SourceRectangle { get; }

    public override int Width => (int)(base.Width * SourceRectangle.Size.X);
    public override int Height => (int)(base.Height * SourceRectangle.Size.Y);

    public SubTexture2D(Texture2D texture, Box2 sourceRectangle)
        : base(texture.GLTexture)
    {
        SourceRectangle = sourceRectangle;
    }
}
