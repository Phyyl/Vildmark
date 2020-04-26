using OpenToolkit.Mathematics;

namespace Vildmark.Graphics.Cameras
{
	public class PerspectiveCamera : Camera
	{
		public PerspectiveCamera(float fovY, int viewWidth, int viewHeight, float zNear, float zFar, Vector3 position = default, Vector3 angle = default)
			: base(viewWidth, viewHeight, zNear, zFar, position, angle)
		{
			FovY = fovY;
		}

		public float FovY { get; set; }

		public override Matrix4 ProjectionMatrix => Matrix4.CreatePerspectiveFieldOfView(FovY, Aspect, ZNear, ZFar);
	}
}
