using OpenTK.Graphics.OpenGL4;

namespace Vildmark.Graphics.Meshes
{
    public interface IMesh
    {
        int Count { get; }
        int ElementSize { get; }

        void Draw(PrimitiveType primitiveType = PrimitiveType.Triangles);
    }

    public interface IMesh<TVertex> : IMesh
        where TVertex : unmanaged
    {
        void UpdateVertices(Span<TVertex> vertices);
    }
}
