using OpenToolkit.Mathematics;

namespace Vildmark.Graphics.Cameras
{
	public abstract class Camera
	{
		private Vector3 position;

		private Vector3 rotation;

		public Camera(int viewWidth, int viewHeight, float zNear, float zFar, Vector3 position = default, Vector3 rotation = default)
		{
			ViewWidth = viewWidth;
			ViewHeight = viewHeight;
			ZNear = zNear;
			ZFar = zFar;
			Position = position;
			Rotation = rotation;
		}

		public int ViewWidth { get; set; }

		public int ViewHeight { get; set; }

		public float ZNear { get; set; }

		public float ZFar { get; set; }

		public ref Vector3 Position => ref position;

		public ref Vector3 Rotation => ref rotation;

		public float Aspect => ViewWidth / (float)ViewHeight;

		public Vector3 Rotate(Vector3 direction)
		{
			Quaternion quaternion = new Quaternion(Rotation.X, -Rotation.Y, Rotation.Z);

			return quaternion * direction;
		}

		public void Resize(int width, int height)
		{
			ViewWidth = width;
			ViewHeight = height;
		}

		public Matrix4 ViewMatrix => Matrix4.CreateTranslation(-position) * Matrix4.CreateRotationY(rotation.Y) * Matrix4.CreateRotationX(rotation.X) * Matrix4.CreateRotationX(rotation.Z);

		public Matrix4 ViewProjectionMatrix => ViewMatrix * ProjectionMatrix;

		public abstract Matrix4 ProjectionMatrix { get; }
	}
}
