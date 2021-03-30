using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Vildmark.Graphics.Fonts
{
    [StructLayout(LayoutKind.Sequential)]
    public struct TextVertex : IVertex, IPositionVertex, ITexCoodVertex
    {
        public static readonly int Size = Marshal.SizeOf<TextVertex>();
        public static readonly int PositionOffset = (int)Marshal.OffsetOf<TextVertex>(nameof(Position));
        public static readonly int TexCoordOffset = (int)Marshal.OffsetOf<TextVertex>(nameof(TexCoord));
        public static readonly int TextureIndexOffset = (int)Marshal.OffsetOf<TextVertex>(nameof(TextureIndex));

        public Vector2 Position;
        public Vector2 TexCoord;
        public int TextureIndex;

        int IVertex.Size => Size;
        int IPositionVertex.PositionOffset => PositionOffset;
        int ITexCoodVertex.TexCoordOffset => TexCoordOffset;

        public TextVertex(Vector2 position, Vector2 texCoord, int textureIndex)
        {
            Position = position;
            TexCoord = texCoord;
            TextureIndex = textureIndex;
        }
    }
}
