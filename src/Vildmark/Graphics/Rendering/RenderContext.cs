using OpenTK.Graphics.OpenGL4;
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

    public int Width => renderOptions?.FrameBuffer?.Width ?? VildmarkGame.Width;
    public int Height => renderOptions?.FrameBuffer?.Height ?? VildmarkGame.Height;
    public Vector2 Size => new(Width, Height);
    public Box2 Bounds => new(0, 0, Width, Height);

    public virtual void Begin(Camera camera, FrameBuffer? frameBuffer = default, bool clear = true, bool depthTest = false, bool multiSample = false, bool blending = false, RenderFace renderFace = RenderFace.All)
    {
        if (renderOptions is not null)
        {
            End();
        }

        renderOptions = new RenderOptions(camera, frameBuffer, depthTest, blending, multiSample, renderFace);
        frameBuffer?.Bind();

        SetOptions(renderOptions);
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

    public void Render(Mesh<Vertex> mesh, TexturedMaterial material, Transform? transform = default, PrimitiveType primitiveType = PrimitiveType.Triangles)
    {
        Render(mesh, material, texturedShader, transform, primitiveType);
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

    private void SetOptions(RenderOptions renderOptions)
    {
        if (renderOptions.DepthTest)
        {
            GL.Enable(EnableCap.DepthTest);
        }
        else
        {
            GL.Disable(EnableCap.DepthTest);
        }

        if (renderOptions.Blending)
        {
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }
        else
        {
            GL.Disable(EnableCap.Blend);
        }

        if (renderOptions.Multisample)
        {
            GL.Enable(EnableCap.Multisample);
        }
        else
        {
            GL.Disable(EnableCap.Multisample);
        }

        if (renderOptions.RenderFace == RenderFace.All)
        {
            GL.Disable(EnableCap.CullFace);
        }
        else
        {
            GL.FrontFace(renderOptions.RenderFace == RenderFace.CounterClockwise ? FrontFaceDirection.Ccw : FrontFaceDirection.Cw);
            GL.CullFace(CullFaceMode.Back);
            GL.Enable(EnableCap.CullFace);
        }
    }

    private record class RenderOptions(Camera Camera, FrameBuffer? FrameBuffer, bool DepthTest, bool Blending, bool Multisample, RenderFace RenderFace);

}
