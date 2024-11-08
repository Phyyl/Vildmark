using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Runtime.InteropServices;
using Vildmark.Graphics.Textures;

namespace Vildmark.Graphics.GLObjects;

internal class GLTexture2D : GLObject
{
    private readonly Texture2DParameters parameters;

    public int Width { get; private set; }
    public int Height { get; private set; }

    public Vector2 Size => new(Width, Height);
    public float TexelWidth => 1 / (float)Width;
    public float TexelHeight => 1 / (float)Height;
    public Vector2 TexelSize => new(TexelWidth, TexelHeight);

    public GLTexture2D(
        int width = 0,
        int height = 0,
        Span<byte> data = default,
        Texture2DParameters? parameters = default)
        : base(GL.GenTexture())
    {
        this.parameters = parameters ?? Texture2DParameters.Texture2D;

        Bind();
        GL.TexParameteri(this.parameters.Target, TextureParameterName.TextureMagFilter, (int)this.parameters.MagFilter);
        GL.TexParameteri(this.parameters.Target, TextureParameterName.TextureMinFilter, (int)this.parameters.MinFilter);
        GL.TexParameteri(this.parameters.Target, TextureParameterName.TextureWrapS, (int)this.parameters.WrapS);
        GL.TexParameteri(this.parameters.Target, TextureParameterName.TextureWrapT, (int)this.parameters.WrapT);
        Unbind();

        SetData(width, height, data);
    }

    public void UpdateData<T>(int x, int y, int width, int height, Span<T> data) where T : unmanaged
    {
        if (data.IsEmpty)
        {
            return;
        }

        Bind();
        GL.TexSubImage2D(parameters.Target, 0, x, y, width, height, parameters.PixelFormat, parameters.PixelType, ref MemoryMarshal.GetReference(data));
        Unbind();
    }

    public void SetData<T>(int width, int height, Span<T> data) where T : unmanaged
    {
        Bind();
        if (data.IsEmpty)
        {
            GL.TexImage2D(parameters.Target, 0, parameters.InternalFormat, width, height, 0, parameters.PixelFormat, parameters.PixelType, IntPtr.Zero);
        }
        else
        {
            GL.TexImage2D(parameters.Target, 0, parameters.InternalFormat, width, height, 0, parameters.PixelFormat, parameters.PixelType, ref MemoryMarshal.GetReference(data));
        }

        Width = width;
        Height = height;
        Unbind();
    }

    public void Resize(int width, int height)
    {
        SetData<byte>(width, height, default);
    }

    public void Bind(int index = 0)
    {
        GL.ActiveTexture((TextureUnit)((int)TextureUnit.Texture0 + index));
        GL.BindTexture(parameters.Target, this);
    }

    public void Unbind(int index = 0)
    {
        Unbind(parameters.Target, index);
    }

    protected override void DisposeOpenGL(ref int id)
    {
        GL.DeleteTexture(ref id);
    }

    public static void Unbind(TextureTarget textureTarget, int index = 0)
    {
        GL.ActiveTexture((TextureUnit)((int)TextureUnit.Texture0 + index));
        GL.BindTexture(textureTarget, 0);
    }
}
