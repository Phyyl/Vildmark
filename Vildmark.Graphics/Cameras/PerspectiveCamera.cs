using OpenToolkit.Mathematics;
using System.Transactions;
using Vildmark.Graphics.Rendering;

namespace Vildmark.Graphics.Cameras
{
	public class PerspectiveCamera : Camera
	{
		public float FovY { get; set; }

		public int Width { get; set; }

		public int Height { get; set; }

		public float ZNear { get; set; }

		public float ZFar { get; set; }

		public override Matrix4 ProjectionMatrix => Matrix4.CreatePerspectiveFieldOfView(FovY, Width / (float)Height, ZNear, ZFar);

		public PerspectiveCamera(float fovY, int width, int height, float zNear = 0.01f, float zFar = 1000, Transforms transforms = default)
			: base(transforms)
		{
			FovY = fovY;
			Width = width;
			Height = height;
			ZNear = zNear;
			ZFar = zFar;
		}

		public override void Resize(int width, int height)
		{
			base.Resize(width, height);

			Width = width;
			Height = height;
		}
	}
}
