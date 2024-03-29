using System.Reflection;

namespace Vildmark;

public static partial class Extensions
{
    public static IEnumerable<T> GetInstancePropertiesOfType<T>(this object obj)
    {
        return obj.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(p => p.GetValue(obj))
            .OfType<T>();
    }
}
