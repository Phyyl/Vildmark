using OpenTK.Mathematics;
using System.Runtime.InteropServices;

namespace Vildmark.Graphics
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ColorVertex : IVertex, IPositionVertex, IColorVertex
    {
        public static readonly int Size = Marshal.SizeOf<ColorVertex>();
        public static readonly int PositionOffset = (int)Marshal.OffsetOf<ColorVertex>(nameof(Position));
        public static readonly int ColorOffset = (int)Marshal.OffsetOf<ColorVertex>(nameof(Color));

        public Vector3 Position;
        public Vector4 Color;

        int IVertex.Size => Size;
        int IPositionVertex.PositionOffset => PositionOffset;
        int IColorVertex.ColorOffset => ColorOffset;
    }
}
