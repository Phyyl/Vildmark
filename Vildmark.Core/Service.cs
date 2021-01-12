using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Vildmark
{
    public static class Service
    {
        private static readonly Dictionary<Type, object> instances = new Dictionary<Type, object>();

        public static T Get<T>()
        {
            return Storage<T>.Instance;
        }

        public static object Get(Type type)
        {
            return instances.GetValueOrDefault(type);
        }

        public static T Set<T>(T value)
        {
            instances.AddOrSet(typeof(T), value);
            return Storage<T>.Instance = value;
        }

        public static T Initialize<T>() where T : new()
        {
            return Initialize(new T());
        }

        public static T Initialize<T>(T value)
        {
            return Set(value);
        }

        public static T CreateInstance<T>()
        {
            return CreateInstance(typeof(T)) is T value ? value : default;
        }

        public static object CreateInstance(Type type)
        {
            foreach (var constructor in type.GetConstructors(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic).OrderBy(c => c.IsPublic ? 0 : 1))
            {
                try
                {
                    var parameters = constructor.GetParameters().Select(p => Get(p.ParameterType)).ToArray();

                    if (parameters.Any(p => p is null))
                    {
                        continue;
                    }

                    object value = constructor.Invoke(parameters);

                    if (value is null)
                    {
                        continue;
                    }

                    return value;
                }
                catch
                {
                }
            }

            return default;
        }

        private static class Storage<T>
        {
            public static T Instance;
        }
    }

    public static class Service<T>
    {
        public static T Instance
        {
            get => Service.Get<T>();
            set => Service.Set(value);
        }
    }
}
