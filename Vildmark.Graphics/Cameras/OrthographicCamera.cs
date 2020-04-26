using OpenToolkit.Mathematics;

namespace Vildmark.Graphics.Cameras
{
	public class OrthographicCamera : Camera
	{
		public OrthographicCamera(int viewWidth, int viewHeight, float zNear = 1, float zFar = -1, Vector3 position = default, Vector3 angle = default)
			: base(viewWidth, viewHeight, zNear, zFar, position, angle)
		{
		}

		public override Matrix4 ProjectionMatrix => Matrix4.CreateOrthographicOffCenter(0, ViewWidth, ViewHeight, 0, ZNear, ZFar);
	}
}
