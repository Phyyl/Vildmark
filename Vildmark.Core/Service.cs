using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Vildmark.Helpers;
using Vildmark.Resources;

namespace Vildmark
{
    public static class Service
    {
        private static readonly Dictionary<Type, object> instances = new Dictionary<Type, object>();

        static Service()
        {
            if (ServiceOptions.AutoRegisterTypes)
            {
                AssemblyHelper.LoadAllReferencedAssemblies();

                IEnumerable<ServiceRegistration> registrations = TypeHelper
                    .TypesWith<RegisterAttribute>()
                    .SelectMany(type => type
                        .GetCustomAttributes<RegisterAttribute>()
                        .Select(attribute => new ServiceRegistration(type, attribute, TypeHelper.CreateInstanceOrDefault(type))))
                    .Where(r =>
                        r.Value != null &&
                        r.Attribute.Type.IsAssignableFrom(r.DeclaringType) &&
                        !r.DeclaringType.IsAbstract)
                    .OrderByDescending(r => r.Attribute.Priority);

                foreach (var registration in registrations)
                {
                    Set(registration.Attribute.Type, registration.Value);
                }
            }
        }

        public static T Get<T>() where T : class
        {
            return Storage<T>.Instance;
        }

        public static object Get(Type type)
        {
            return instances.GetValueOrDefault(type);
        }

        public static T Set<T>(T value) where T : class
        {
            instances.TryAdd(typeof(T), value);
            return Storage<T>.Instance ??= value;
        }

        public static bool Set(Type serviceType, object value)
        {
            instances.TryAdd(serviceType, value);

            FieldInfo propertyInfo = typeof(Storage<>).MakeGenericType(serviceType).GetField(nameof(Storage<object>.Instance));

            if (propertyInfo.GetValue(null) == null)
            {
                propertyInfo.SetValue(null, value);

                return true;
            }

            return false;
        }

        public static T Initialize<T>() where T : class, new()
        {
            return Initialize(new T());
        }

        public static T Initialize<T>(T value) where T : class
        {
            return Set(value);
        }

        public static T CreateInstance<T>() where T : class
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

        private static class Storage<T> where T : class
        {
            public static T Instance;
        }

        private class ServiceRegistration
        {
            public Type DeclaringType { get; }

            public RegisterAttribute Attribute { get; }

            public object Value { get; }

            public ServiceRegistration(Type declaringType, RegisterAttribute attribute, object value)
            {
                DeclaringType = declaringType;
                Attribute = attribute;
                Value = value;
            }
        }
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
