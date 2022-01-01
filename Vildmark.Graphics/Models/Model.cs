using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Meshes;
using Vildmark.Graphics.Rendering;
using Vildmark.Graphics.Shaders;

namespace Vildmark.Graphics.Models
{
    public abstract class Model<TVertex, TMaterial, TShader> : IModel
        where TVertex : unmanaged
        where TShader : IShader<TMaterial>, new()
    {
        private readonly GLVertexArray vertexArray;

        public IMesh<TVertex> Mesh { get; }
        public TMaterial Material { get; set; }
        public TShader Shader { get; }
        public Transform Transform { get; set; } = new();

        protected Model(IMesh<TVertex> mesh, TMaterial material, TShader shader)
        {
            Mesh = mesh;
            Material = material;
            Shader = shader;

            vertexArray = new();
            vertexArray.Bind();
            SetupAttribs();
        }

        public Model(TMaterial material)
            : this(new Mesh<TVertex>(), material, new())
        {
        }

        protected abstract void SetupAttribs();

        public virtual void Render(RenderContext renderContext)
        {
            Shader.Begin(Material, renderContext.Camera, Transform);
            vertexArray.Bind();
            Mesh.Draw();
        }

        protected unsafe void AttribPointer(string attribName, string fieldName)
        {
            VertexAttribPointerType type = Shader.GetAttribType(attribName);
            int size = Shader.GetAttribSize(attribName);

            vertexArray.AttribPointer(Shader.GetAttribLocation(attribName), size, type, sizeof(TVertex), (int)Marshal.OffsetOf<TVertex>(fieldName));
        }
    }
}
