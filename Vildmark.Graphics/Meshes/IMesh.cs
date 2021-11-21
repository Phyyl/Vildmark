using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Shaders;

namespace Vildmark.Graphics.Meshes
{
    public interface IMesh : IShaderSetup
    {
        GLVertexArray VertexArray { get; }

        void Render(PrimitiveType primitiveType = PrimitiveType.Triangles);
    }

    public interface IMesh<TVertex> : IMesh where TVertex : unmanaged
    {
        GLBuffer<TVertex> VertexBuffer { get; }
    }

    public interface IIndexedMesh : IMesh
    {
        GLBuffer<uint> IndexBuffer { get; }
    }
}
