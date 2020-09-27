using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Helpers;
using Vildmark.Graphics.Models;
using Vildmark.Graphics.Resources;
using Vildmark.Graphics.Shaders;
using Vildmark.Maths;

namespace Vildmark.Graphics.Rendering
{
    public class RenderContext2D
    {
        private Mesh squareMesh;
        private Mesh circleMesh;

        public Color4 ClearColor { get; set; } = Color4.CornflowerBlue;

        public OrthographicOffCenterCamera OrthographicCamera { get; }

        public RenderContext2D(int width, int height)
        {
            EnableDepthTest();

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            OrthographicCamera = new OrthographicOffCenterCamera(width, height);

            squareMesh = new Mesh(new Vertex[]
            {
                new Vertex(new Vector3(0, 0, 0), new Vector2(0, 0), new Vector3(0, 0, 1)),
                new Vertex(new Vector3(0, 1, 0), new Vector2(0, 1), new Vector3(0, 0, 1)),
                new Vertex(new Vector3(1, 1, 0), new Vector2(1, 1), new Vector3(0, 0, 1)),
                new Vertex(new Vector3(0, 0, 0), new Vector2(0, 0), new Vector3(0, 0, 1)),
                new Vertex(new Vector3(1, 1, 0), new Vector2(1, 1), new Vector3(0, 0, 1)),
                new Vertex(new Vector3(1, 0, 0), new Vector2(1, 0), new Vector3(0, 0, 1)),
            });

            const int circleSideCount = 72;

            IEnumerable<Vertex> GetCircleVertices(int i)
            {
                float angle = (i / (float)circleSideCount) * MathHelper.TwoPi;
                float angle2 = ((i + 1) / (float)circleSideCount) * MathHelper.TwoPi;

                if (i == 0)
                {
                    yield return new Vertex(new Vector3(0, 0, 0), Vector3.Zero);
                }

                yield return new Vertex(new Vector3((float)Math.Cos(angle), (float)Math.Sin(angle), 0), Vector3.Zero);
            }

            circleMesh = new Mesh(Enumerable.Range(0, circleSideCount + 1).SelectMany(GetCircleVertices).ToArray());
        }

        public void Resize(int width, int height)
        {
            OrthographicCamera.Resize(width, height);
        }

        public void ClearDepthBuffer()
        {
            GL.Clear(ClearBufferMask.DepthBufferBit);
        }

        public void ClearColorBuffer()
        {
            GL.ClearColor(ClearColor);
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }

        public void EnableDepthTest()
        {
            GL.Enable(EnableCap.DepthTest);
        }

        public void DisableDepthTest()
        {
            GL.Disable(EnableCap.DepthTest);
        }

        public void SetViewPort(int width, int height)
        {
            GL.Viewport(0, 0, width, height);
        }

        public void Render(Mesh mesh, Material material, Matrix4? modelMatrix = default, PrimitiveType primitiveType = PrimitiveType.Triangles, MaterialShader shader = default)
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
                    shader.ProjectionMatrix.SetValue(OrthographicCamera.ProjectionMatrix);
                    shader.ViewMatrix.SetValue(OrthographicCamera.ViewMatrix);
                    shader.ModelMatrix.SetValue(modelMatrix ?? Matrix4.Identity);
                    shader.Tex0.SetValue(material.Texture);
                    shader.Tint.SetValue(material.Tint);
                    shader.SourceRect.SetValue(material.SourceRect.ToVector());

                    mesh.Render(primitiveType);
                }
            }
        }

        public void RenderRectangle(GLTexture2D texture, Vector2 position = default, Vector2 size = default, Vector4? color = default, float scale = 1, float angle = 0, Vector2 origin = default, RectangleF sourceRect = default, float z = 0)
        {
            texture ??= Textures.WhitePixel;
            color ??= Vector4.One;

            if (size.X == 0)
            {
                size.X = texture.Width;
            }

            if (size.Y == 0)
            {
                size.Y = texture.Height;
            }

            if (sourceRect == RectangleF.Empty)
            {
                sourceRect = new RectangleF(0, 0, 1, 1);
            }
            else if (sourceRect.Width > 1 || sourceRect.Height > 1)
            {
                sourceRect.X /= texture.Width;
                sourceRect.Width /= texture.Width;
                sourceRect.Y /= texture.Height;
                sourceRect.Height /= texture.Height;
            }
            else
            {
                size.X *= sourceRect.Width;
                size.Y *= sourceRect.Height;
            }

            DisableDepthTest();
            Render(squareMesh, new Material(texture, color.Value, sourceRect), CreateModelMatrix(position, size, scale, angle, origin, z));
            EnableDepthTest();
        }

        public void RenderRectangle(Vector2 position, Vector2 size, Vector4? color = default, float scale = 1, float angle = 0, Vector2 origin = default, float z = 0)
        {
            color ??= Vector4.One;

            DisableDepthTest();
            Render(squareMesh, new Material(Textures.WhitePixel, color.Value), CreateModelMatrix(position, size, scale, angle, origin, z));
            EnableDepthTest();
        }

        public void RenderCircle(Vector2 position, float radius, Vector4? color = default, float scale = 1, Vector2 origin = default, float z = 0)
        {
            color ??= Vector4.One;

            DisableDepthTest();
            Render(circleMesh, new Material(Textures.WhitePixel, color.Value), CreateModelMatrix(position, new Vector2(radius, radius), scale, 0, origin, z), PrimitiveType.TriangleFan);
            EnableDepthTest();
        }

        private Matrix4 CreateModelMatrix(Vector2 position, Vector2 size, float scale, float angle, Vector2 origin, float z)
        {
            Matrix4 modelMatrix = Matrix4.Identity;

            modelMatrix *= Matrix4.CreateScale(new Vector3(size.X, size.Y, 1));

            if (origin.LengthSquared > 0)
            {
                modelMatrix *= Matrix4.CreateTranslation(new Vector3(-origin));
            }

            if (scale != 0)
            {
                modelMatrix *= Matrix4.CreateScale(scale, scale, 1);
            }

            if (angle != 0)
            {
                modelMatrix *= Matrix4.CreateRotationZ(angle);
            }

            if (position.LengthSquared > 0)
            {
                modelMatrix *= Matrix4.CreateTranslation(new Vector3(position.X, position.Y, z));
            }

            return modelMatrix;
        }
    }
}
