using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
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

        public bool DepthTest
        {
            get => GL.IsEnabled(EnableCap.DepthTest);
            set
            {
                if (value)
                {
                    GL.Enable(EnableCap.DepthTest);
                }
                else
                {
                    GL.Disable(EnableCap.DepthTest);
                }
            }
        }

        public bool Blending
        {
            get => GL.IsEnabled(EnableCap.DepthTest);
            set
            {
                if (value)
                {
                    GL.Enable(EnableCap.Blend);
                    GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
                }
                else
                {
                    GL.Disable(EnableCap.Blend);
                }
            }
        }

        public bool Multisample
        {
            get => GL.IsEnabled(EnableCap.Multisample);
            set
            {
                if (value)
                {
                    GL.Enable(EnableCap.Multisample);
                }
                else
                {
                    GL.Disable(EnableCap.Multisample);
                }
            }
        }

        public RenderContext(Camera camera, IWindow window, bool mutlisample = false)
        {
            this.window = window;

            Camera = camera;
            texturedShader = new TexturedShader();

            Multisample = mutlisample;
        }

        public virtual void Clear()
        {
            GL.ClearColor(ClearColor);
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
        }

        public virtual void Begin(FrameBuffer? frameBuffer = default, bool clear = true)
        {
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

        public void Render(IMesh mesh, Color4 color, Transform? transform = default, PrimitiveType primitiveType = PrimitiveType.Triangles, IShader? shader = default) => Render(mesh, new ColorMaterial(color), transform, primitiveType, shader);
        public void Render(IMesh mesh, Texture2D texture, Transform? transform = default, PrimitiveType primitiveType = PrimitiveType.Triangles, IShader? shader = default) => Render(mesh, new TextureMaterial(texture), transform, primitiveType, shader);
        public virtual void Render<TMaterial>(IMesh mesh, TMaterial material, Transform? transform = default, PrimitiveType primitiveType = PrimitiveType.Triangles, IShader? shader = default)
            where TMaterial : IMaterial
        {
            RenderBatch(new[] { new BatchEntry(mesh, transform) }, material, primitiveType, shader);
        }

        public void RenderBatch(IEnumerable<BatchEntry> entries, Color4 color, PrimitiveType primitiveType = PrimitiveType.Triangles, IShader? shader = default) => RenderBatch(entries, new ColorMaterial(color), primitiveType, shader);
        public void RenderBatch(IEnumerable<BatchEntry> entries, Texture2D texture, PrimitiveType primitiveType = PrimitiveType.Triangles, IShader? shader = default) => RenderBatch(entries, new TextureMaterial(texture), primitiveType, shader);
        public virtual void RenderBatch<TMaterial>(IEnumerable<BatchEntry> entries, TMaterial material, PrimitiveType primitiveType = PrimitiveType.Triangles, IShader? shader = default)
            where TMaterial : IMaterial
        {
            shader ??= texturedShader;

            shader.Use();

            if (shader is IShaderSetup<TMaterial> materialShader)
            {
                materialShader.Setup(material);
            }

            if (shader is IShaderSetup<IMaterial> iMaterialShader)
            {
                iMaterialShader.Setup(material);
            }

            if (shader is IShaderSetup<Camera> cameraShader)
            {
                cameraShader.Setup(Camera);
            }

            foreach (var entry in entries)
            {
                if (shader is IShaderSetup<Transform?> transformShader)
                {
                    transformShader.Setup(entry.Transform);
                }

                entry.Mesh?.Draw(primitiveType);
            }
        }
    }

    public record class BatchEntry(IMesh Mesh, Transform? Transform = default);
}
