using OpenToolkit.Graphics.OpenGL;
using OpenToolkit.Mathematics;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.Models;
using Vildmark.Graphics.Resources;

namespace Vildmark.Graphics.Rendering
{
	public class RenderContext
	{
		private MaterialShader materialShader = new MaterialShader();

		public RenderContext()
		{
			GL.Enable(EnableCap.DepthTest);
			GL.Enable(EnableCap.Blend);
			GL.Enable(EnableCap.CullFace);

			GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
			GL.CullFace(CullFaceMode.Back);
		}

		public void Clear(Color4 color)
		{
			GL.ClearColor(color);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
		}

		public void Render(Model model, Camera camera, PrimitiveType primitiveType = PrimitiveType.Triangles)
		{
			using (materialShader.Use())
			{
				materialShader.Tex0.SetValue(model.Material.GLTexture);
				materialShader.ProjectionMatrix.SetValue(model.ModelMatrix * camera.ViewProjectionMatrix);
				materialShader.Tint.SetValue(model.Material.Tint);

				model.Mesh.Render(primitiveType);
			}
		}
	}
}
