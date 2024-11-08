namespace Vildmark.Audio.ALObjects;

public abstract class ALObject : IDisposable
{
#if DEBUG
    private static readonly List<ALObject> glObjects = [];

    public static IEnumerable<ALObject> GLObjects => [.. glObjects];
#endif

    protected ALObject(int id)
    {
        ID = id;

#if DEBUG
        glObjects.Add(this);
#endif
    }

    public int ID { get; }

    public void Dispose()
    {
        DisposeOpenAL();
    }

    protected abstract void DisposeOpenAL();

    public static implicit operator int(ALObject obj)
    {
        return obj?.ID ?? 0;
    }

    public override string ToString()
    {
        return $"{GetType().Name} ({ID})";
    }
}
