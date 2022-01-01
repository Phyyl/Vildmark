using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Vildmark.Helpers;

namespace Vildmark.Graphics.GLObjects
{
    public class GLVertexArray : GLObject
    {
        public GLVertexArray()
            : base(GL.GenVertexArray())
        {

        }

        public void Bind()
        {
            GL.BindVertexArray(this);
        }

        public void AttribPointer(int index, int size, VertexAttribPointerType type, int stride, int offset)
        {
            GL.VertexAttribPointer(index, size, type, false, stride, offset);
            GL.EnableVertexAttribArray(index);
        }

        public void AttribPointer<T>(int index, int stride, int offset)
            where T : unmanaged
        {
            AttribPointer(index, StructTypeInfo.GetAttribSize<T>(), StructTypeInfo.GetAttribType<T>(), stride, offset);
        }

        public void DrawArrays(int count, PrimitiveType primitiveType = PrimitiveType.Triangles) => DrawArrays(0, count, primitiveType);
        public void DrawArrays(int first, int count, PrimitiveType primitiveType = PrimitiveType.Triangles)
        {
            Bind();
            GL.DrawArrays(primitiveType, first, count);
        }

        public static void Unbind()
        {
            GL.BindVertexArray(0);
        }

        protected override void DisposeOpenGL()
        {
            GL.DeleteVertexArray(this);
        }
    }
}
