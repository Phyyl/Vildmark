using OpenTK.Mathematics;
using System.Runtime.InteropServices;

namespace Vildmark.Graphics.Fonts
{
    [StructLayout(LayoutKind.Sequential)]
    public struct BitmapFontVertex
    {
        public static readonly int PositionOffset = (int)Marshal.OffsetOf<BitmapFontVertex>(nameof(Position));
        public static readonly int TexCoordOffset = (int)Marshal.OffsetOf<BitmapFontVertex>(nameof(TexCoord));
        public static readonly int PageIndexOffset = (int)Marshal.OffsetOf<BitmapFontVertex>(nameof(PageIndex));

        public Vector2 Position;
        public Vector2 TexCoord;
        public int PageIndex;

        public BitmapFontVertex(Vector2 position, Vector2 texCoord, int pageIndex)
        {
            Position = position;
            TexCoord = texCoord;
            PageIndex = pageIndex;
        }
    }
}
