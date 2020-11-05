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
    public class RenderContext2D : RenderContext
    {
        private Mesh squareMesh;
        private Mesh circleMesh;

        public OrthographicOffCenterCamera OrthographicCamera { get; }

        public override Camera Camera => OrthographicCamera;

        public RenderContext2D(int width, int height, float scale = 1)
        {
            const int circleSideCount = 72;

            OrthographicCamera = new OrthographicOffCenterCamera(width, height)
            {
                Scale = scale
            };

            squareMesh = new Mesh(new Vertex[]
            {
                new Vertex(new Vector3(0, 0, 0), new Vector2(0, 0), new Vector3(0, 0, 1)),
                new Vertex(new Vector3(0, 1, 0), new Vector2(0, 1), new Vector3(0, 0, 1)),
                new Vertex(new Vector3(1, 1, 0), new Vector2(1, 1), new Vector3(0, 0, 1)),
                new Vertex(new Vector3(0, 0, 0), new Vector2(0, 0), new Vector3(0, 0, 1)),
                new Vertex(new Vector3(1, 1, 0), new Vector2(1, 1), new Vector3(0, 0, 1)),
                new Vertex(new Vector3(1, 0, 0), new Vector2(1, 0), new Vector3(0, 0, 1)),
            });

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

            Render(squareMesh, new Material(texture, color.Value, sourceRect), CreateModelMatrix(position, size, scale, angle, origin, z));
        }

        public void RenderRectangle(Vector2 position, Vector2 size, Vector4? color = default, float scale = 1, float angle = 0, Vector2 origin = default, float z = 0)
        {
            Render(squareMesh, new Material(Textures.WhitePixel, color ?? Vector4.One), CreateModelMatrix(position, size, scale, angle, origin, z));
        }

        public void RenderRectangle(Texture2D texture, Vector2 position = default, Vector2 size = default, Vector4? color = default, float scale = 1, float angle = 0, Vector2 origin = default, float z = 0)
        {
            RenderRectangle(texture.Texture, position, size, color, scale, angle, origin, texture.Rectangle, z);
        }

        public void RenderCircle(Vector2 position, float radius, Vector4? color = default, float scale = 1, Vector2 origin = default, float z = 0)
        {
            color ??= Vector4.One;

            Render(circleMesh, new Material(Textures.WhitePixel, color.Value), CreateModelMatrix(position, new Vector2(scale * radius), scale, 0, origin, z), PrimitiveType.TriangleFan);
        }

        private Matrix4 CreateModelMatrix(Vector2 position, Vector2 size, float scale, float angle, Vector2 origin, float z)
        {
            return MatrixHelper.CreateMatrix(new Vector3(position.X, position.Y, z), new Vector3(0, 0, angle), new Vector3(scale, scale, 1), new Vector3(origin.X, origin.Y, 0));
        }
    }
}
