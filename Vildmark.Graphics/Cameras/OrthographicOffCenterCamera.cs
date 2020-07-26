using OpenToolkit.Mathematics;
using Vildmark.Graphics.Rendering;

namespace Vildmark.Graphics.Cameras
{
	public class OrthographicOffCenterCamera : Camera
	{
		public float Width { get; set; }

		public float Height { get; set; }

		public float ZNear { get; set; }

		public float ZFar { get; set; }

		public override Matrix4 ProjectionMatrix => Matrix4.CreateOrthographicOffCenter(0, Width, Height, 0, ZNear, ZFar);

		public OrthographicOffCenterCamera(float width, float height, float zNear = 1, float zFar = -1, Transforms transforms = default)
			: base(transforms)
		{
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
