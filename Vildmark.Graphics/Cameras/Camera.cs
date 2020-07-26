using OpenToolkit.Mathematics;
using Vildmark.Graphics.Rendering;

namespace Vildmark.Graphics.Cameras
{
	public abstract class Camera
	{
		public Transforms Transforms { get; }

		public abstract Matrix4 ProjectionMatrix { get; }

		protected Camera(Transforms transforms = default)
		{
			Transforms = transforms ?? new Transforms();
			Transforms.TranslationScale = new Vector3(-1);
		}

		public virtual void Resize(int width, int height)
		{
		}
	}
}
