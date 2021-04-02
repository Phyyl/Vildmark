using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Runtime.InteropServices;

namespace Vildmark.Graphics.GLObjects
{
    public class GLTexture2D : GLObject
    {
        public TextureMagFilter MagFilter { get; }
        public TextureMinFilter MinFilter { get; }
        public TextureWrapMode WrapSMode { get; }
        public TextureWrapMode WrapTMode { get; }
        public PixelFormat PixelFormat { get; }
        public PixelInternalFormat PixelInternalFormat { get; }
        public PixelType PixelType { get; }
        public TextureTarget TextureTarget { get; }

        public int Width { get; private set; }
        public int Height { get; private set; }
        public Vector2 Size => new(Width, Height);

        public float TexelWidth => 1 / (float)Width;
        public float TexelHeight => 1 / (float)Height;
        public Vector2 TexelSize => new(TexelWidth, TexelHeight);

        public GLTexture2D()
            : this(0, 0)
        {
        }

        public GLTexture2D(int width, int height, Span<byte> data = default, TextureLoadOptions options = default)
            : base(GL.GenTexture())
        {
            options ??= TextureLoadOptions.Default;

            MagFilter = options.MagFilter;
            MinFilter = options.MinFilter;
            WrapSMode = options.WrapSMode;
            WrapTMode = options.WrapTMode;
            TextureTarget = options.Target;
            PixelFormat = options.PixelFormat;
            PixelType = options.PixelType;
            PixelInternalFormat = options.PixelInternalFormat;

            Bind();
            {
                GL.TexParameter(TextureTarget, TextureParameterName.TextureMagFilter, (int)MagFilter);
                GL.TexParameter(TextureTarget, TextureParameterName.TextureMinFilter, (int)MinFilter);
                GL.TexParameter(TextureTarget, TextureParameterName.TextureWrapS, (int)WrapSMode);
                GL.TexParameter(TextureTarget, TextureParameterName.TextureWrapT, (int)WrapTMode);

                SetData(width, height, data);
            }
            Unbind();
        }

        public void UpdateData<T>(int x, int y, int width, int height, Span<T> data) where T : unmanaged
        {
            if (data.IsEmpty)
            {
                return;
            }

            GL.TexSubImage2D(TextureTarget, 0, x, y, width, height, PixelFormat, PixelType, ref MemoryMarshal.GetReference(data));
        }

        public void SetData<T>(int width, int height, Span<T> data) where T : unmanaged
        {
            if (data.IsEmpty)
            {
                GL.TexImage2D(TextureTarget, 0, PixelInternalFormat, width, height, 0, PixelFormat, PixelType, IntPtr.Zero);
            }
            else
            {
                GL.TexImage2D(TextureTarget, 0, PixelInternalFormat, width, height, 0, PixelFormat, PixelType, ref MemoryMarshal.GetReference(data));
            }

            Width = width;
            Height = height;
        }

        public void Resize(int width, int height)
        {
            SetData<byte>(width, height, default);
        }

        public void Bind(int index = 0)
        {
            GL.ActiveTexture(TextureUnit.Texture0 + index);
            GL.BindTexture(TextureTarget, this);
        }

        public void Unbind(int index = 0)
        {
            Unbind(TextureTarget, index);
        }

        protected override void DisposeOpenGL()
        {
            GL.DeleteTexture(this);
        }

        public static void Unbind(TextureTarget textureTarget, int index = 0)
        {
            GL.ActiveTexture(TextureUnit.Texture0 + index);
            GL.BindTexture(textureTarget, 0);
        }
    }
}
