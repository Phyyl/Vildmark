using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Runtime.InteropServices;
using Vildmark.Graphics.Rendering;

namespace Vildmark.Graphics.GLObjects
{
    public class GLTexture2D : GLObject
    {
        private const PixelInternalFormat pixelInternalFormat = PixelInternalFormat.Rgba;
        private const PixelFormat pixelFormat = PixelFormat.Bgra;
        private const PixelType pixelType = PixelType.UnsignedByte;

        public GLTexture2D(
            int width,
            int height,
            Span<byte> data = default,
            TextureMagFilter textureMagFilter = TextureMagFilter.Nearest,
            TextureMinFilter textureMinFilter = TextureMinFilter.Nearest,
            TextureWrapMode wrapSMode = TextureWrapMode.Clamp,
            TextureWrapMode wrapTMode = TextureWrapMode.Clamp,
            TextureTarget textureTarget = TextureTarget.Texture2D)
            : base(GL.GenTexture())
        {
            TextureTarget = textureTarget;

            TextureMagFilter = textureMagFilter;
            TextureMinFilter = textureMinFilter;
            WrapSMode = wrapSMode;
            WrapTMode = wrapTMode;

            SetData(width, height, data);
        }

        public TextureMagFilter TextureMagFilter
        {
            get
            {
                GL.GetTextureParameter(this, GetTextureParameter.TextureMagFilter, out int value);
                return (TextureMagFilter)value;
            }
            set
            {
                using (Bind())
                {
                    GL.TexParameter(TextureTarget, TextureParameterName.TextureMagFilter, (int)value);
                }
            }
        }

        public TextureMinFilter TextureMinFilter
        {
            get
            {
                GL.GetTextureParameter(this, GetTextureParameter.TextureMinFilter, out int value);
                return (TextureMinFilter)value;
            }
            set
            {
                using (Bind())
                {
                    GL.TexParameter(TextureTarget, TextureParameterName.TextureMinFilter, (int)value);
                }
            }
        }

        public TextureWrapMode WrapSMode
        {
            get
            {
                GL.GetTextureParameter(this, GetTextureParameter.TextureWrapS, out int value);
                return (TextureWrapMode)value;
            }
            set
            {
                using (Bind())
                {
                    GL.TexParameter(TextureTarget, TextureParameterName.TextureWrapS, (int)value);
                }
            }
        }

        public TextureWrapMode WrapTMode
        {
            get
            {
                GL.GetTextureParameter(this, GetTextureParameter.TextureWrapT, out int value);
                return (TextureWrapMode)value;
            }
            set
            {
                using (Bind())
                {
                    GL.TexParameter(TextureTarget, TextureParameterName.TextureWrapT, (int)value);
                }
            }
        }

        public TextureTarget TextureTarget { get; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public Vector2 Size => new(Width, Height);

        public float TexelWidth => 1 / (float)Width;

        public float TexelHeight => 1 / (float)Height;

        public Vector2 TexelSize => new(TexelWidth, TexelHeight);

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
            using (Bind())
            {
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
            }
        }

        public void Resize(int width, int height)
        {
            SetData<byte>(width, height, default);
        }

        public IDisposable Bind(int index = 0)
        {
            return new BindContext(this, index);
        }

        public void Unbind(int index = 0)
        {
            GL.ActiveTexture(TextureUnit.Texture0 + index);
            GL.BindTexture(TextureTarget, 0);
        }

        protected override void DisposeOpenGL()
        {
            GL.DeleteTexture(this);
        }

        private class BindContext : IDisposable
        {
            public GLTexture2D Texture { get; }

            public int Index { get; }

            public BindContext(GLTexture2D texture, int index, bool bind = true)
            {
                Texture = texture;
                Index = index;

                if (bind)
                {
                    GL.ActiveTexture(TextureUnit.Texture0 + Index);
                    GL.BindTexture(texture.TextureTarget, Texture);
                }
            }

            public void Dispose()
            {
                Texture.Unbind(Index);
            }
        }

        public static GLTexture2D FromPixels(int width, int height, params byte[] data)
        {
            return new GLTexture2D(width, height, data);
        }
    }
}
