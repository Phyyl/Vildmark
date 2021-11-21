using OpenTK.Graphics.OpenGL4;
using Vildmark.Graphics.GLObjects;

namespace Vildmark.Graphics.Shaders
{
    public abstract class Attrib : ShaderVariable
    {
        protected Attrib(string name)
            : base(name)
        {
        }
    }

    //TODO: Refactor to use VertexArray.VertexAttribArray
    public class Attrib<T> : Attrib where T : unmanaged
    {
        private unsafe readonly int size = sizeof(T) / sizeof(float);

        public int Size { get; }

        public Attrib(string name)
            : base(name)
        {
        }

        public unsafe void Setup<TVertex>(GLBuffer<TVertex> buffer, int offset = 0) where TVertex : unmanaged
        {
            if (!Defined || !Enabled)
            {
                return;
            }

            buffer.Bind();

            GL.EnableVertexAttribArray(Location);
            GL.VertexAttribPointer(Location, size, VertexAttribPointerType.Float, false, sizeof(TVertex), offset);
        }

        public void Disable()
        {
            if (!Defined)
            {
                return;
            }

            GL.DisableVertexAttribArray(Location);
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Size: {size}";
        }
    }
}
