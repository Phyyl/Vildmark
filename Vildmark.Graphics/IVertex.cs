namespace Vildmark.Graphics
{
    public interface IVertex
    {
        public int Size { get; }
    }

    public interface IColorVertex : IVertex
    {
        int ColorOffset { get; }
    }

    public interface IPositionVertex : IVertex
    {
        int PositionOffset { get; }
    }

    public interface INormalVertex : IVertex
    {
        int NormalOffset { get; }
    }

    public interface ITexCoodVertex : IVertex
    {
        int TexCoordOffset { get; }
    }
}
