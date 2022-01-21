using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Drawing;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.Materials;
using Vildmark.Graphics.Meshes;
using Vildmark.Graphics.Resources;
using Vildmark.Graphics.Shaders;
using Vildmark.Resources;
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

        public int Width => window.Width;
        public int Height => window.Height;
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
            this.frameBuffer = frameBuffer;

            int width = frameBuffer?.Width ?? window.Width;
            int height = frameBuffer?.Height ?? window.Height;

            frameBuffer?.Bind();

            OnBegin?.Invoke(width, height);

            GL.Viewport(0, 0, width, height);

            if (clear)
            {
                Clear();
            }
        }

        public virtual void End()
        {
            frameBuffer?.Unbind();
        }

        public virtual void Render<TMaterial>(IMesh mesh, TMaterial material, Transform? transform = default, PrimitiveType primitiveType = PrimitiveType.Triangles, IShader? shader = default)
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

            if (shader is IShaderSetup<Transform?> transformShader)
            {
                transformShader.Setup(transform);
            }

            mesh.Draw(primitiveType);
        }
    }
}
