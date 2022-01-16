using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Vildmark.Helpers;
using Vildmark.Resources;

namespace Vildmark
{
    public static class Service
    {
        private static readonly Dictionary<Type, object> instances = new();
        private static readonly Dictionary<Type, ServiceTypeRegistration> registrations = new();

        static Service()
        {
            AssemblyHelper.LoadAllReferencedAssemblies();

            IEnumerable<ServiceTypeRegistration> registrations = TypeHelper
                .TypesWith<RegisterAttribute>()
                .SelectMany(type => type
                    .GetCustomAttributes<RegisterAttribute>()
                    .Select(attribute => new ServiceTypeRegistration(type, attribute)))
                .Where(r =>
                    r.Attribute.Type.IsAssignableFrom(r.DeclaringType) &&
                    !r.DeclaringType.IsAbstract)
                .OrderBy(r => r.Attribute.Priority);

            foreach (var registration in registrations)
            {
                Service.registrations.AddOrSet(registration.Attribute.Type, registration);
            }
        }

        public static T Get<T>() where T : class
        {
            return Storage<T>.Instance ??= Instanciate<T>();
        }

        public static T Set<T>(T value) where T : class
        {
            instances.TryAdd(typeof(T), value);
            return Storage<T>.Instance ??= value;
        }

        public static T Set<T>() where T : class, new()
        {
            return Set(new T());
        }

        public static T? CreateInstance<T>() where T : class
        {
            return CreateInstance(typeof(T)) is T value ? value : default;
        }

        public static object Get(Type type)
        {
            return instances.GetValueOrDefault(type);
        }

        public static bool TryGet<T>(out T service) where T : class
        {
            service = Get<T>();

            return service is not null;
        }

        public static object? CreateInstance(Type type)
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

        private static T? Instanciate<T>() where T : class
        {
            if (registrations.TryGetValue(typeof(T), out ServiceTypeRegistration? registration))
            {
                return CreateInstance(registration.DeclaringType) as T;
            }

            return default;
        }

        private static class Storage<T> where T : class
        {
            public static T? Instance;
        }

        private record ServiceTypeRegistration(Type DeclaringType, RegisterAttribute Attribute);
    }

    public static class Service<T> where T : class
    {
        public static T Instance
        {
            get => Service.Get<T>();
            set => Service.Set(value);
        }
    }
}
