using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.FrameBuffers;
using Vildmark.Graphics.Meshes;
using Vildmark.Graphics.Shaders;
using Vildmark.Logging;

namespace Vildmark.Graphics.Rendering;

public delegate void RenderContextBeginEventHandler(int width, int height);

public partial class Renderer
{
    private RenderOptions? renderOptions;

    public Color4 ClearColor { get; set; } = Color4.Black;

    public bool DepthTest { get; set; }
    public bool Blending { get; set; }
    public bool Multisample { get; set; }
    public bool CullFace { get; set; }

    public int Width => renderOptions?.FrameBuffer?.Width ?? VildmarkGame.Width;
    public int Height => renderOptions?.FrameBuffer?.Height ?? VildmarkGame.Height;

    public virtual void Begin(Camera camera, FrameBuffer? frameBuffer = default, bool clear = true)
    {
        if (renderOptions is not null)
        {
            End();
        }

        renderOptions = new RenderOptions(camera, frameBuffer);
        frameBuffer?.Bind();

        SetOptions();
        SetViewport();

        if (clear)
        {
            GL.ClearColor(ClearColor);
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
        }
    }

    public virtual void End()
    {
        if (renderOptions is null)
        {
            return;
        }

        renderOptions.FrameBuffer?.Unbind();
        renderOptions = null;
    }

    public virtual void Render<TVertex, TMaterial>(Mesh<TVertex> mesh, TMaterial material, Shader<TVertex, TMaterial> shader, Transform? transform = default, PrimitiveType primitiveType = PrimitiveType.Triangles)
        where TVertex : unmanaged
    {
        if (renderOptions is null)
        {
            Logger.Warning("Begin needs to be called before rendering");
            return;
        }

        shader.Use();

        mesh.VertexArray.Bind();
        mesh.VertexBuffer.Bind();

        shader.Setup(mesh);
        shader.Setup(material, renderOptions.Camera, transform);

        mesh.Draw(primitiveType);
    }

    private void SetViewport()
    {
        if (renderOptions is null)
        {
            return;
        }

        int width = renderOptions.FrameBuffer?.Width ?? VildmarkGame.Width;
        int height = renderOptions.FrameBuffer?.Height ?? VildmarkGame.Height;

        GL.Viewport(0, 0, width, height);
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

    private record class RenderOptions(Camera Camera, FrameBuffer? FrameBuffer);
}
