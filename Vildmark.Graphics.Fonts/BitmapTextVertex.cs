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
    public struct BitmapTextVertex
    {
        public static readonly int Size = Marshal.SizeOf<BitmapTextVertex>();
        public static readonly int PositionOffset = (int)Marshal.OffsetOf<BitmapTextVertex>(nameof(Position));
        public static readonly int TexCoordOffset = (int)Marshal.OffsetOf<BitmapTextVertex>(nameof(TexCoord));
        public static readonly int TextureIndexOffset = (int)Marshal.OffsetOf<BitmapTextVertex>(nameof(TextureIndex));

        public Vector2 Position;
        public Vector2 TexCoord;
        public int TextureIndex;

        public BitmapTextVertex(Vector2 position, Vector2 texCoord, int textureIndex)
        {
            Position = position;
            TexCoord = texCoord;
            TextureIndex = textureIndex;
        }
    }
}