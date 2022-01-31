using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Vildmark.Helpers
{
    public static class TypeHelper
    {
        private static bool staticConstructorsRan;

        private static Type[]? typeCache;

        static TypeHelper()
        {
            AssemblyHelper.LoadAllReferencedAssemblies();

            RefreshTypeCache();
        }

        public static void RefreshTypeCache()
        {
            typeCache = AssemblyHelper.GetAllLoadedUserAssemblies().SelectMany(assembly => assembly.GetTypes()).ToArray();
        }

        public static void RunAllStaticConstructors()
        {
            if (staticConstructorsRan)
            {
                return;
            }

            foreach (var type in AssemblyHelper.GetAllLoadedUserAssemblies().SelectMany(a => a.GetTypes()))
            {
                RunStaticConstructor(type);
            }

            staticConstructorsRan = true;
        }

        public static void RunStaticConstructor<T>() => RunStaticConstructor(typeof(T));

        public static void RunStaticConstructor(Type type)
        {
            try
            {
                type.TypeInitializer?.Invoke(null, null);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        public static IEnumerable<Type> TypesOf<T>() => typeCache?.Where(typeof(T).IsAssignableFrom) ?? Array.Empty<Type>();

        public static IEnumerable<Type> TypesOf<T, TAttribute>() => typeCache?.Where(typeof(T).IsAssignableFrom) ?? Array.Empty<Type>();

        public static IEnumerable<Type> ConcreteTypesOf<T>() => TypesOf<T>().Where(t => !t.IsAbstract);

        public static IEnumerable<Type> ConcreteTypesOf<T, TAttribute>() => ConcreteTypesOf<T>().Where(HasAttribute<TAttribute>);

        public static IEnumerable<Type> TypesWith<TAttribute>() => typeCache?.Where(HasAttribute<TAttribute>) ?? Array.Empty<Type>();

        public static bool HasAttribute<T, TAttribute>() => HasAttribute(typeof(T), typeof(TAttribute));

        public static bool HasAttribute<TAttribute>(Type type) => HasAttribute(type, typeof(TAttribute));

        public static bool HasAttribute(Type type, Type attributeType) => Attribute.IsDefined(type, attributeType);

        public static object? CreateInstanceOrDefault(Type type, params object?[]? parameters)
        {
            try
            {
                if (parameters is { Length: > 0 })
                {
                    return Activator.CreateInstance(type, BindingFlags.Public | BindingFlags.NonPublic, null, parameters, null);
                }
                else
                {
                    return Activator.CreateInstance(type, true);
                }
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return null;
            }
        }

        public static bool TryCreateIsntance(Type type, [NotNullWhen(true)] out object? result, params object?[]? parameters)
        {
            result = CreateInstanceOrDefault(type, parameters);

            return result is not null;
        }
    }
}
