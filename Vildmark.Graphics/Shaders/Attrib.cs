using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Resources;

namespace Vildmark.Graphics.Shaders
{
    public abstract class Attrib : ShaderVariable
    {
        protected Attrib(string name)
            : base(name)
        {
        }
    }

    public class Attrib<T> : Attrib where T : unmanaged
    {
        private readonly int size;

        public unsafe Attrib(string name)
            : base(name)
        {
            size = sizeof(T) / sizeof(float);
        }

        public unsafe void Setup<TVertex>(GLBuffer<TVertex> buffer, int offset) where TVertex : unmanaged
        {
            if (!Defined || !Enabled)
            {
                return;
            }

            using (buffer.Bind())
            {
                GL.EnableVertexAttribArray(Location);
                GL.VertexAttribPointer(Location, size, VertexAttribPointerType.Float, false, sizeof(TVertex), offset);
            }
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
