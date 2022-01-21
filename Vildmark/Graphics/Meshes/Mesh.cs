using OpenTK.Graphics.OpenGL4;
using System.Reflection;
using System.Runtime.InteropServices;
using Vildmark.Graphics.GLObjects;

namespace Vildmark.Graphics.Meshes
{
    public class Mesh<TVertex> : IMesh<TVertex>
        where TVertex : unmanaged
    {
        private record struct Attrib(int Index, int Size, VertexAttribPointerType Type, int Offset);
        private static readonly Attrib[] attribs = GetAttribs().ToArray();

        internal GLVertexArray VertexArray { get; }
        internal GLBuffer<TVertex> VertexBuffer { get; }

        public int ElementSize { get; } = Marshal.SizeOf<TVertex>();

        public virtual int Count => VertexBuffer.Count;

        public Mesh(Span<TVertex> vertices = default)
        {
            VertexArray = new();
            VertexBuffer = new GLBuffer<TVertex>(vertices);

            VertexArray.Bind();
            VertexBuffer.Bind();
            InitializeAttribs();
        }

        public void UpdateVertices(Span<TVertex> vertices)
        {
            VertexBuffer.SetData(vertices);
        }

        public virtual void Draw(PrimitiveType primitiveType = PrimitiveType.Triangles)
        {
            VertexArray.Bind();
            GL.DrawArrays(primitiveType, 0, Count);
        }

        protected virtual void InitializeAttribs()
        {
            foreach (var attrib in attribs)
            {
                AttribPointer(attrib.Index, attrib.Size, attrib.Type, ElementSize, attrib.Offset);
            }
        }

        protected void AttribPointer(int index, int size, VertexAttribPointerType type, int stride, int offset)
        {
            GL.VertexAttribPointer(index, size, type, false, stride, offset);
            GL.EnableVertexAttribArray(index);
        }

        protected void AttribPointer<T>(int index, int stride, int offset)
            where T : unmanaged
        {
            AttribPointer(index, StructTypeInfo.GetAttribSize<T>(), StructTypeInfo.GetAttribType<T>(), stride, offset);
        }

        private static IEnumerable<Attrib> GetAttribs()
        {
            FieldInfo[] fields = typeof(TVertex).GetFields();

            for (int i = 0; i < fields.Length; i++)
            {
                FieldInfo fieldInfo = fields[i];

                if (StructTypeInfo.TryGetAttribSize(fieldInfo.FieldType, out int size) && StructTypeInfo.TryGetAttribType(fieldInfo.FieldType, out var type))
                {
                    yield return new Attrib(i, size, type, (int)Marshal.OffsetOf<TVertex>(fieldInfo.Name));
                }
            }
        }
    }

    public class Mesh : Mesh<Vertex>
    {
        public Mesh(Span<Vertex> vertices = default)
            : base(vertices)
        {
        }
    }
}

