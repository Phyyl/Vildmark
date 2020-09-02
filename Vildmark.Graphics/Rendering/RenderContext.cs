using OpenToolkit.Graphics.OpenGL;
using OpenToolkit.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Helpers;
using Vildmark.Graphics.Models;
using Vildmark.Graphics.Resources;
using Vildmark.Graphics.Shaders;

namespace Vildmark.Graphics.Rendering
{
	public class RenderContext
	{
		public Color4 ClearColor { get; set; } = Color4.CornflowerBlue;

		public OrthographicOffCenterCamera OrthographicCamera { get; }

		public RenderContext(int width, int height)
		{
			EnableDepthTest();

			GL.Enable(EnableCap.Blend);
			GL.Enable(EnableCap.CullFace);

			GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
			GL.CullFace(CullFaceMode.Back);

			OrthographicCamera = new OrthographicOffCenterCamera(width, height);
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

		public void Render(Mesh mesh, Camera camera, Material material, Transforms transforms = default, MaterialShader shader = default, PrimitiveType primitiveType = PrimitiveType.Triangles)
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
					shader.ViewMatrix.SetValue(camera.Transforms.Matrix);
					shader.ModelMatrix.SetValue(transforms?.Matrix ?? Matrix4.Identity);
					shader.Tex0.SetValue(material.Texture);
					shader.Tint.SetValue(material.Tint);

					mesh.Render(primitiveType);
				}
			}
		}

		public void Render(GLTexture2D texture, Vector2 position = default, Vector2 size = default)
        {
			if (size.X == 0)
            {
				size.X = texture.Width;
            }

			if (size.Y == 0)
			{
				size.Y = texture.Height;
			}

			GL.Disable(EnableCap.CullFace);
			Render(new Mesh(CubeHelper.GetBackVertices(new Vector3(position), new Vector3(size), Vector4.One, Vector2.Zero, Vector2.One).ToArray()), OrthographicCamera, new Material(texture));
			GL.Enable(EnableCap.CullFace);
		}

		public void Render(Model model, Camera camera)
		{
			if (model is null)
			{
				return;
			}

			Render(model.Mesh, camera, model.Material, model.Transforms, primitiveType: model.PrimitiveType);
		}
	}
}
