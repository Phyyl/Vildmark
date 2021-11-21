using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Vildmark.Graphics
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex : IAttribPointers
    {
        public static readonly int Size = Marshal.SizeOf<Vertex>();
        public static readonly int PositionOffset = (int)Marshal.OffsetOf<Vertex>(nameof(Position));
        public static readonly int TexCoordOffset = (int)Marshal.OffsetOf<Vertex>(nameof(TexCoord));
        public static readonly int ColorOffset = (int)Marshal.OffsetOf<Vertex>(nameof(Color));
        public static readonly int NormalOffset = (int)Marshal.OffsetOf<Vertex>(nameof(Normal));

        public Vector3 Position;
        public Vector2 TexCoord;
        public Vector4 Color;
        public Vector3 Normal;

        public Vertex(Vector3 position) : this(position, default, Vector4.One, default) { }
        public Vertex(Vector3 position, Vector2 texCoord) : this(position, texCoord, Vector4.One, default) { }
        public Vertex(Vector3 position, Vector2 texCoord, Vector4 color) : this(position, texCoord, color, default) { }
        public Vertex(Vector3 position, Vector4 color) : this(position, default, color, default) { }

        public Vertex(float xyz) : this(new Vector3(xyz, xyz, xyz)) { }
        public Vertex(float x, float y) : this(new Vector3(x, y, 0)) { }
        public Vertex(float x, float y, float z) : this(new Vector3(x, y, z)) { }

        public Vertex(Vector3 position, Vector2 texCoord, Vector4 color, Vector3 normal)
        {
            Position = position;
            TexCoord = texCoord;
            Color = color;
            Normal = normal;
        }

        public IEnumerable<AttribPointer> GetAttribPointers()
        {
            yield return new(0, 3, VertexAttribPointerType.Float, Size, PositionOffset);
            yield return new(1, 2, VertexAttribPointerType.Float, Size, TexCoordOffset);
            yield return new(2, 4, VertexAttribPointerType.Float, Size, ColorOffset);
            yield return new(3, 3, VertexAttribPointerType.Float, Size, NormalOffset);
        }
    }
}
