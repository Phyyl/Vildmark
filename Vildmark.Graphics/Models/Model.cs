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
        where TMaterial : new()
        where TShader : IShader<TMaterial>, new()
    {
        private static TShader? shaderInstance;
        public static TShader ShaderInstance => shaderInstance ??= new();

        private readonly GLVertexArray vertexArray;

        public IMesh<TVertex> Mesh { get; }
        public TMaterial Material { get; set; }
        public TShader Shader => ShaderInstance;
        public Transform Transform { get; set; } = new();

        protected Model(IMesh<TVertex> mesh, TMaterial material, TShader shader)
        {
            Mesh = mesh;
            Material = material;

            vertexArray = new();
            vertexArray.Bind();
            shader.Initialize(vertexArray);
        }

        public Model()
            : this(new Mesh<TVertex>(), new(), new())
        {
        }

        public virtual void Render(RenderContext renderContext)
        {
            Shader.Begin(Material, renderContext.Camera, Transform);
            vertexArray.Bind();
            Mesh.Draw();
        }
    }
}
