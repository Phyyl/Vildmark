using OpenTK.Graphics.OpenGL4;

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
