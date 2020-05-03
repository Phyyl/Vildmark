using OpenToolkit.Mathematics;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Resources;

namespace Vildmark.Graphics.Models
{
	public class Model
	{
		private Vector3 position;

		private Vector3 rotation;

		private Vector3 origin;

		public Model(Mesh mesh, Vector3 position = default, Vector3 rotation = default, Vector3 origin = default, Material material = default)
		{
			Mesh = mesh;
			Material = material ?? new Material(Textures.WhitePixel);

			Position = position;
			Rotation = rotation;
			Origin = origin;
		}

		public ref Vector3 Position => ref position;

		public ref Vector3 Rotation => ref rotation;

		public ref Vector3 Origin => ref origin;

		public Mesh Mesh { get; }

		public Material Material { get; }

		public Matrix4 ModelMatrix => Matrix4.CreateTranslation(-origin) * Matrix4.CreateRotationY(rotation.Y) * Matrix4.CreateRotationX(rotation.X) * Matrix4.CreateRotationX(rotation.Z) * Matrix4.CreateTranslation(-position);
	}
}
