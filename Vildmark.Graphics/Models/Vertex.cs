using OpenToolkit.Mathematics;
using System.Runtime.InteropServices;

namespace Vildmark.Graphics.Models
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Vertex
	{
		public static readonly int SizeInBytes = Marshal.SizeOf<Vertex>();
		public static readonly int PositionOffset = (int)Marshal.OffsetOf<Vertex>(nameof(Position));
		public static readonly int TexCoordOffset = (int)Marshal.OffsetOf<Vertex>(nameof(TexCoord));
		public static readonly int ColorOffset = (int)Marshal.OffsetOf<Vertex>(nameof(Color));
		public static readonly int NormalOffset = (int)Marshal.OffsetOf<Vertex>(nameof(Normal));

		public Vector3 Position;
		public Vector2 TexCoord;
		public Vector4 Color;
		public Vector3 Normal;

		public Vertex(Vector3 position)
			: this(position, Vector2.Zero, Vector4.One, Vector3.One)
		{
		}

		public Vertex(Vector3 position, Vector2 texCoord)
			: this(position, texCoord, Vector4.One, Vector3.One)
		{
		}

		public Vertex(Vector3 position, Vector4 color)
			: this(position, Vector2.Zero, color, Vector3.One)
		{
		}

		public Vertex(Vector3 position, Vector2 texCoord, Vector4 color)
			: this(position, texCoord, color, Vector3.One)
		{
		}

		public Vertex(Vector3 position, Vector2 texCoord, Vector3 normal)
			: this(position, texCoord, Vector4.One, normal)
		{
		}

		public Vertex(Vector3 position, Vector2 texCoord, Vector4 color, Vector3 normal)
		{
			Position = position;
			TexCoord = texCoord;
			Color = color;
			Normal = normal.Normalized();
		}
	}
}
