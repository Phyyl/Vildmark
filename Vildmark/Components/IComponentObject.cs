using System.Diagnostics.CodeAnalysis;

namespace Vildmark.Components
{
    public interface IComponentObject
    {
        T? SetComponent<T>(T value) where T : notnull;
        TInstance SetComponent<T, TInstance>()
            where T : notnull
            where TInstance : T, new();

        T? GetComponent<T>();
        bool TryGetComponent<T>([NotNullWhen(true)] out T? component);

        T? RemoveComponent<T>();

        IEnumerable<object> GetComponents();
    }
}
