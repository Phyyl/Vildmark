using OpenToolkit.Graphics.OpenGL;
using OpenToolkit.Mathematics;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Rendering;
using Vildmark.Graphics.Resources;

namespace Vildmark.Graphics.Models
{
	public class Model
	{
		public Model(Mesh mesh, Material material = default, Transforms transforms = default, PrimitiveType primitiveType = PrimitiveType.Triangles)
		{
			Mesh = mesh;
			Material = material ?? new Material(Textures.WhitePixel);
			Transforms = transforms ?? new Transforms();

			PrimitiveType = primitiveType;
		}
		public Transforms Transforms { get; }

		public Mesh Mesh { get; }

		public Material Material { get; }

		public PrimitiveType PrimitiveType { get; }
	}
}
