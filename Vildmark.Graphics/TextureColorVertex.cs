using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Vildmark.Graphics
{
    [StructLayout(LayoutKind.Sequential)]
    public struct TextureColorVertex : IVertex, IPositionVertex, ITexCoodVertex, IColorVertex
    {
        public static readonly int Size = Marshal.SizeOf<TextureColorVertex>();
        public static readonly int PositionOffset = (int)Marshal.OffsetOf<TextureColorVertex>(nameof(Position));
        public static readonly int TexCoordOffset = (int)Marshal.OffsetOf<TextureColorVertex>(nameof(TexCoord));
        public static readonly int ColorOffset = (int)Marshal.OffsetOf<TextureColorVertex>(nameof(Color));

        public Vector3 Position;
        public Vector2 TexCoord;
        public Vector4 Color;

        int IVertex.Size => Size;
        int IPositionVertex.PositionOffset => PositionOffset;
        int ITexCoodVertex.TexCoordOffset => TexCoordOffset;
        int IColorVertex.ColorOffset => ColorOffset;
    }
}
