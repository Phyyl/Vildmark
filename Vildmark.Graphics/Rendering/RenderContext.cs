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
    public class RenderContext
    {
        public Color4 ClearColor { get; set; } = Color4.CornflowerBlue;

        public RenderContext()
        {
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

        public virtual void Resize(int width, int height) { }

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

        public void Render(Mesh mesh, Material material, Camera camera, Matrix4? modelMatrix = default, PrimitiveType primitiveType = PrimitiveType.Triangles, MaterialShader shader = default)
        {
            if (mesh.VertexBuffer.Count == 0)
            {
                return;
            }

            shader ??= Resources.Shaders.Material;

            using (shader.Use())
            {
                using (material.Texture.Bind())
                {
                    shader.ProjectionMatrix.SetValue(camera.ProjectionMatrix);
                    shader.ViewMatrix.SetValue(camera.ViewMatrix);
                    shader.ModelMatrix.SetValue(modelMatrix ?? Matrix4.Identity);
                    shader.Tex0.SetValue(material.Texture);
                    shader.Tint.SetValue(material.Tint);
                    shader.SourceRect.SetValue(material.SourceRect.ToVector());

                    mesh.Render(primitiveType);
                }
            }
        }

        public void SetViewPort(int width, int height)
        {
            GL.Viewport(0, 0, width, height);
        }
    }
}
