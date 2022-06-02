namespace Vildmark.Graphics.GLObjects;

public abstract class GLObject : IDisposable
{
#if DEBUG
    private static readonly List<GLObject> glObjects = new();

    public static IEnumerable<GLObject> GLObjects => glObjects.ToArray();
#endif

    protected GLObject(int id)
    {
        ID = id;

#if DEBUG
        glObjects.Add(this);
#endif
    }

    public int ID { get; }

    public void Dispose()
    {
        DisposeOpenGL();
    }

    protected abstract void DisposeOpenGL();

    public static implicit operator int(GLObject obj)
    {
        return obj?.ID ?? 0;
    }

    public override string ToString()
    {
        return $"{GetType().Name} ({ID})";
    }
}
