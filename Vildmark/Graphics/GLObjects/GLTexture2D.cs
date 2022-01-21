using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Runtime.InteropServices;

namespace Vildmark.Graphics.GLObjects
{
    public class GLTexture2D : GLObject
    {
        private readonly PixelFormat pixelFormat = PixelFormat.Bgra;
        private readonly PixelInternalFormat pixelInternalFormat = PixelInternalFormat.Rgba;
        private readonly PixelType pixelType = PixelType.UnsignedByte;

        public TextureTarget TextureTarget { get; }

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
            PixelFormat pixelFormat = PixelFormat.Bgra,
            PixelInternalFormat pixelInternalFormat = PixelInternalFormat.Rgba,
            PixelType pixelType = PixelType.UnsignedByte,
            TextureMagFilter magFilter = TextureMagFilter.Linear,
            TextureMinFilter minFilter = TextureMinFilter.Linear,
            TextureWrapMode wrapS = TextureWrapMode.ClampToEdge,
            TextureWrapMode wrapT = TextureWrapMode.ClampToEdge,
            TextureTarget textureTarget = TextureTarget.Texture2D)
            : base(GL.GenTexture())
        {
            this.pixelFormat = pixelFormat;
            this.pixelInternalFormat = pixelInternalFormat;
            this.pixelType = pixelType;

            TextureTarget = textureTarget;

            SetData(width, height, data);
            Configure(magFilter, minFilter, wrapS, wrapT);
        }

        public void Configure(TextureMagFilter magFilter, TextureMinFilter minFilter, TextureWrapMode wrapS, TextureWrapMode wrapT)
        {
            Bind();
            GL.TexParameter(TextureTarget, TextureParameterName.TextureMagFilter, (int)magFilter);
            GL.TexParameter(TextureTarget, TextureParameterName.TextureMinFilter, (int)minFilter);
            GL.TexParameter(TextureTarget, TextureParameterName.TextureWrapS, (int)wrapS);
            GL.TexParameter(TextureTarget, TextureParameterName.TextureWrapT, (int)wrapT);
            Unbind();
        }

        public void UpdateData<T>(int x, int y, int width, int height, Span<T> data) where T : unmanaged
        {
            if (data.IsEmpty)
            {
                return;
            }

            GL.TexSubImage2D(TextureTarget, 0, x, y, width, height, pixelFormat, pixelType, ref MemoryMarshal.GetReference(data));
        }

        public void SetData<T>(int width, int height, Span<T> data) where T : unmanaged
        {
            Bind();
            if (data.IsEmpty)
            {
                GL.TexImage2D(TextureTarget, 0, pixelInternalFormat, width, height, 0, pixelFormat, pixelType, IntPtr.Zero);
            }
            else
            {
                GL.TexImage2D(TextureTarget, 0, pixelInternalFormat, width, height, 0, pixelFormat, pixelType, ref MemoryMarshal.GetReference(data));
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
