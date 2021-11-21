using OpenTK.Graphics.OpenGL;
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

        public void VertexAttribPointer(int index, int size, VertexAttribPointerType type, int stride, int offset)
        {
            Bind();
            GL.VertexAttribPointer(index, size, type, false, stride, offset);
            GL.EnableVertexAttribArray(index);
        }

        public void VertexAttribPointer<T>(int index, int size, VertexAttribPointerType type, GLBuffer<T> buffer, int offset)
            where T : unmanaged
        {
            buffer.Bind();
            VertexAttribPointer(index, size, type, buffer.ElementSize, offset);
        }

        public void VertexAttribPointer(int index, GLBuffer<float> buffer) => VertexAttribPointer(index, 1, VertexAttribPointerType.Float, buffer, 0);
        public void VertexAttribPointer(int index, GLBuffer<Vector2> buffer) => VertexAttribPointer(index, 2, VertexAttribPointerType.Float, buffer, 0);
        public void VertexAttribPointer(int index, GLBuffer<Vector3> buffer) => VertexAttribPointer(index, 3, VertexAttribPointerType.Float, buffer, 0);
        public void VertexAttribPointer(int index, GLBuffer<Vector4> buffer) => VertexAttribPointer(index, 4, VertexAttribPointerType.Float, buffer, 0);

        public void VertexAttribPointers<T>(GLBuffer<T> buffer)
            where T : unmanaged, IAttribPointers
        {
            buffer.Bind();

            foreach (var attrib in new T().GetAttribPointers())
            {
                VertexAttribPointer(attrib.Index, attrib.Size, attrib.Type, attrib.Stride, attrib.Offset);
            }
        }

        public void DrawArrays(PrimitiveType primitiveType, int first, int count)
        {
            Bind();
            GL.DrawArrays(primitiveType, first, count);
        }

        public void DrawArrays(PrimitiveType primitiveType, int count) => DrawArrays(primitiveType, 0, count);
        public void DrawArrays(int count) => DrawArrays(PrimitiveType.Triangles, 0, count);

        public static void Unbind()
        {
            GL.BindVertexArray(0);
        }

        protected override void DisposeOpenGL()
        {
            GL.DeleteVertexArray(this);
        }

        public static GLVertexArray Create<T>(GLBuffer<T> buffer)
            where T : unmanaged, IAttribPointers
        {
            GLVertexArray vertexArray = new GLVertexArray();
            vertexArray.VertexAttribPointers(buffer);
            return vertexArray;
        }
    }
}
