using OpenToolkit.Graphics.OpenGL;
using OpenToolkit.Mathematics;
using System;
using System.Collections.Generic;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.Models;
using Vildmark.Graphics.Resources;

namespace Vildmark.Graphics.Rendering
{
	public class RenderContext
	{
		private readonly List<Batch> batches = new List<Batch>();

		private readonly MaterialShader materialShader;

		public Camera Camera { get; set; }

		public int Width { get; private set; }

		public int Height { get; private set; }

		public Color4 ClearColor { get; set; } = Color4.CornflowerBlue;

		public RenderContext(int width, int height, Camera camera)
		{
			materialShader = new MaterialShader();

			Camera = camera;

			GL.Enable(EnableCap.DepthTest);
			GL.Enable(EnableCap.Blend);
			GL.Enable(EnableCap.CullFace);

			GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
			GL.CullFace(CullFaceMode.Back);

			Resize(width, height);
		}

		public void Resize(int width, int height)
		{
			Width = width;
			Height = height;

			Camera.Resize(width, height);
		}

		public void ClearDepthBuffer()
		{
			GL.Clear(ClearBufferMask.DepthBufferBit);
		}

		public void ClearColorBuffer()
		{
			GL.ClearColor(ClearColor);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
		}

		public void SetViewPort(int width, int height)
		{
			GL.Viewport(0, 0, width, height);
		}

		public void Render(Mesh mesh, Material material = default, Transforms transforms = default, PrimitiveType primitiveType = PrimitiveType.Triangles)
		{
			batches.Add(new Batch(mesh, material, transforms?.Matrix ?? Matrix4.Identity, primitiveType));
		}

		public void Render(Model model)
		{
			if (model is null)
			{
				return;
			}

			Render(model.Mesh, model.Material, model.Transforms, model.PrimitiveType);
		}

		public IDisposable Begin()
		{
			batches.Clear();

			return new BeginContext(this);
		}

		public void End()
		{
			Flush();
		}

		private void Flush()
		{
			GL.Viewport(0, 0, Width, Height);
			ClearColorBuffer();
			RenderBatches(materialShader, Camera);
		}

		private void RenderBatches(MaterialShader materialShader, Camera camera)
		{
			foreach (var batch in batches)
			{
				RenderBatch(batch, materialShader, camera);
			}
		}

		private void RenderBatch(Batch batch, MaterialShader shader, Camera camera)
		{
			using (shader.Use())
			{
				shader.Tex0.SetValue(batch.Material.Texture);
				shader.ProjectionMatrix.SetValue(batch.ModelMatrix * camera.ViewProjectionMatrix);
				shader.Tint.SetValue(batch.Material.Tint);

				batch.Mesh.Render(batch.PrimitiveType);
			}
		}

		private class Batch
		{
			public Mesh Mesh { get; }

			public Material Material { get; }

			public Matrix4 ModelMatrix { get; }

			public PrimitiveType PrimitiveType { get; }

			public Batch(Mesh mesh, Material material, Matrix4 modelMatrix, PrimitiveType primitiveType)
			{
				Mesh = mesh;
				Material = material ?? Materials.WhitePixel;
				ModelMatrix = modelMatrix;
				PrimitiveType = primitiveType;
			}

			public Batch(Model model)
				: this(model.Mesh, model.Material, model.Transforms.Matrix, model.PrimitiveType)
			{
			}
		}

		private class BeginContext : IDisposable
		{
			public RenderContext RenderContext { get; }

			public BeginContext(RenderContext renderContext)
			{
				RenderContext = renderContext;
			}

			public void Dispose()
			{
				RenderContext.End();
			}
		}
	}
}
