using OpenTK.Mathematics;

namespace Vildmark.Qubicle;

public struct QubicleVoxel
{
    public byte R, G, B, A;

    public bool Invisible => A == 0;
    public bool RightVisible => (A & 2) == 2;
    public bool LeftVisible => (A & 4) == 4;
    public bool TopVisible => (A & 8) == 8;
    public bool BottomVisible => (A & 16) == 16;
    public bool FrontVisible => (A & 32) == 32;
    public bool BackVisible => (A & 64) == 64;

    public Vector4 Color => new(R / 255f, G / 255f, B / 255f, 1);

    public static QubicleVoxel FromRGBA(uint rgba)
    {
        QubicleVoxel result = new()
        {
            R = (byte)((rgba >> 0) & 0xFF),
            G = (byte)((rgba >> 8) & 0xFF),
            B = (byte)((rgba >> 16) & 0xFF),
            A = (byte)((rgba >> 24) & 0xFF)
        };

        return result;
    }

    public static QubicleVoxel FromBGRA(uint bgra)
    {
        QubicleVoxel result = new()
        {
            B = (byte)((bgra >> 0) & 0xFF),
            G = (byte)((bgra >> 8) & 0xFF),
            R = (byte)((bgra >> 16) & 0xFF),
            A = (byte)((bgra >> 24) & 0xFF)
        };

        return result;
    }
}
