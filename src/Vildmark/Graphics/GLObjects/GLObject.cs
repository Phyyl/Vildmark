namespace Vildmark.Graphics.GLObjects;

internal abstract class GLObject : IDisposable
{
#if DEBUG
    private static readonly List<GLObject> glObjects = [];

    public static IEnumerable<GLObject> GLObjects => [.. glObjects];
#endif

    private int id;

    protected GLObject(int id)
    {
        this.id = id;

#if DEBUG
        glObjects.Add(this);
#endif
    }

    public int ID => id;

    public void Dispose()
    {
        DisposeOpenGL(ref id);
    }

    protected abstract void DisposeOpenGL(ref int id);

    public static implicit operator int(GLObject obj)
    {
        return obj?.ID ?? 0;
    }

    public override string ToString()
    {
        return $"{GetType().Name} ({ID})";
    }
}
