using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.Models;
using Vildmark.Graphics.Resources;
using Vildmark.Maths;

namespace Vildmark.Graphics.Rendering
{
    public abstract class RenderContext
    {
        public Color4 ClearColor { get; set; } = Color4.CornflowerBlue;

        public abstract Camera Camera { get; }

        public RenderContext()
        {
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

        public void Resize(int width, int height)
        {
            Camera.Width = width;
            Camera.Height = height;
        }

        public void Clear()
        {
            GL.ClearColor(ClearColor);
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
        }

        public void EnableDepthTest()
        {
            GL.Enable(EnableCap.DepthTest);
        }

        public void DisableDepthTest()
        {
            GL.Disable(EnableCap.DepthTest);
        }

        public virtual IDisposable Begin()
        {
            Clear();

            return new BeginContext(this);
        }

        public virtual void End()
        {
        }

        public void Render(Mesh mesh, Material material, Vector3 offset = default)
        {
            Render(mesh, material, Matrix4.Identity, offset);
        }

        public void Render(Mesh mesh, Material material, Matrix4 modelMatrix, Vector3 offset = default)
        {
            Render(mesh, material, Resources.Shaders.Material, modelMatrix, offset);
        }

        public void Render(Mesh mesh, Material material, MaterialShader shader, Vector3 offset = default)
        {
            Render(mesh, material, shader, Matrix4.Identity, offset);
        }

        public virtual void Render(Mesh mesh, Material material, MaterialShader shader, Matrix4 modelMatrix, Vector3 offset = default)
        {
            shader.Render(mesh, material, Camera, modelMatrix, offset);
        }

        public void SetViewPort(int width, int height)
        {
            GL.Viewport(0, 0, width, height);
        }

        private class BeginContext : IDisposable
        {
            private readonly RenderContext renderContext;

            public BeginContext(RenderContext renderContext)
            {
                this.renderContext = renderContext;
            }

            public void Dispose()
            {
                renderContext.End();
            }
        }
    }
}
