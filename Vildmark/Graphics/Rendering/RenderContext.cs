using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Diagnostics;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.FrameBuffers;
using Vildmark.Graphics.Materials;
using Vildmark.Graphics.Meshes;
using Vildmark.Graphics.Shaders;
using Vildmark.Graphics.Textures;
using Vildmark.Windowing;

namespace Vildmark.Graphics.Rendering
{
    public delegate void RenderContextBeginEventHandler(int width, int height);

    public partial class RenderContext
    {
        private readonly TexturedShader texturedShader;
        private readonly IWindow window;
        private FrameBuffer? frameBuffer;

        public Color4 ClearColor { get; set; } = Color4.Black;
        public Camera Camera { get; }

        public int Width { get; private set; }
        public int Height { get; private set; }
        public Vector2 Size => new(Width, Height);

        public event RenderContextBeginEventHandler? OnBegin;

        public bool DepthTest { get; set; }
        public bool Blending { get; set; }
        public bool Multisample { get; set; }
        public bool CullFace { get; set; }

        public RenderContext(Camera camera, IWindow window)
        {
            this.window = window;

            Camera = camera;
            texturedShader = new TexturedShader();
        }

        public virtual void Clear()
        {
            GL.ClearColor(ClearColor);
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
        }

        public virtual void Begin(FrameBuffer? frameBuffer = default, bool clear = true)
        {
            SetOptions();

            this.frameBuffer = frameBuffer;

            Width = frameBuffer?.Width ?? window.Width;
            Height = frameBuffer?.Height ?? window.Height;

            frameBuffer?.Bind();
            OnBegin?.Invoke(Width, Height);
            GL.Viewport(0, 0, Width, Height);

            if (clear)
            {
                Clear();
            }
        }

        public virtual void End()
        {
            frameBuffer?.Unbind();
        }

        public virtual void Render<TMaterial>(IMesh mesh, TMaterial material, Transform? transform = default, RenderOverrides? overrides = default)
        {
            RenderBatch(new[] { new BatchEntry(mesh, transform) }, material, overrides);
        }

        public virtual void RenderBatch<TMaterial>(IEnumerable<BatchEntry> meshes, TMaterial material, RenderOverrides? overrides = default)
        {
            IShader shader = overrides?.Shader ?? texturedShader;

            shader.Use();

            if (shader is IShaderMaterialSetup materialShader)
            {
                materialShader.Setup(material);
            }

            if (shader is IShaderSetup<Camera> cameraShader)
            {
                cameraShader.Setup(overrides?.Camera ?? Camera);
            }

            foreach (var mesh in meshes)
            {
                if (shader is IShaderSetup<Transform?> transformShader)
                {
                    transformShader.Setup(mesh.Transform);
                }

                mesh.Mesh?.Draw(overrides?.PrimitiveType ?? PrimitiveType.Triangles);
            }
        }

        private void SetOptions()
        {
            if (DepthTest)
            {
                GL.Enable(EnableCap.DepthTest);
            }
            else
            {
                GL.Disable(EnableCap.DepthTest);
            }

            if (Blending)
            {
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            }
            else
            {
                GL.Disable(EnableCap.Blend);
            }

            if (Multisample)
            {
                GL.Enable(EnableCap.Multisample);
            }
            else
            {
                GL.Disable(EnableCap.Multisample);
            }

            if (CullFace)
            {
                GL.Enable(EnableCap.CullFace);
            }
            else
            {
                GL.Disable(EnableCap.CullFace);
            }
        }
    }

    public record class RenderOverrides(Camera? Camera = default, IShader? Shader = default, PrimitiveType PrimitiveType = PrimitiveType.Triangles);
    public record class BatchEntry(IMesh Mesh, Transform? Transform = default);
}
