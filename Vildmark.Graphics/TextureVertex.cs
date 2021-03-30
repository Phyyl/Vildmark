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
    public struct TextureVertex : IVertex, IPositionVertex, ITexCoodVertex
    {
        public static readonly int Size = Marshal.SizeOf<TextureVertex>();
        public static readonly int PositionOffset = (int)Marshal.OffsetOf<TextureVertex>(nameof(Position));
        public static readonly int TexCoordOffset = (int)Marshal.OffsetOf<TextureVertex>(nameof(TexCoord));

        public Vector3 Position;
        public Vector2 TexCoord;

        int IVertex.Size => Size;
        int IPositionVertex.PositionOffset => PositionOffset;
        int ITexCoodVertex.TexCoordOffset => TexCoordOffset;
    }
}
