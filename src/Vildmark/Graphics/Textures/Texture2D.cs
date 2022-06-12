using OpenTK.Mathematics;
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

    public Texture2D(int width, int height)
         : this(new GLTexture2D(width, height))
    {
    }

    internal Texture2D(GLTexture2D glTexture)
    {
        GLTexture = glTexture;
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