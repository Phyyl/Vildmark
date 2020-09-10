using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.InteropServices;
using Vildmark.Helpers;

namespace Vildmark.DependencyServices
{
    public class DependencyService : IDependencyService
    {
        private static IDependencyService global;

        public static IDependencyService Global => global ??= CreateGlobal();

        private readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

        public T Get<T>() where T : class
        {
            return Get(typeof(T)) as T;
        }

        public object Get(Type type)
        {
            if (services.TryGetValue(type, out var value))
            {
                return value;
            }

            IEnumerable<Assembly> assemblies = AssemblyHelper.GetAllLoadedUserAssemblies();
            IEnumerable<Type> types = assemblies.SelectMany(a => a.GetTypes().Where(t => !t.IsAbstract));
            IEnumerable<Type> instanceTypes = types.Where(type.IsAssignableFrom);

            Type instanceType = instanceTypes.OrderByDescending(GetPriority).FirstOrDefault();

            if (instanceType == null)
            {
                return null;
            }

            int GetPriority(Type type)
            {
                ServiceAttribute[] attributes = type.GetCustomAttributes<ServiceAttribute>().ToArray();
                ServiceAttribute attribute = attributes.FirstOrDefault(a => a.Type == type) ??
                    attributes.FirstOrDefault(a => type.IsAssignableFrom(a.Type)) ??
                    attributes.FirstOrDefault(a => a.Type == null);

                return attribute?.Priority ?? 0;
            }

            value ??= services.Values.FirstOrDefault(o => instanceType.IsAssignableFrom(o.GetType()));
            value ??= Create(instanceType);

            if (!(value is null))
            {
                services.Add(type, value);
            }

            return value;
        }

        public T Create<T>() where T : class
        {
            return Create(typeof(T)) as T;
        }

        public object Create(Type type)
        {
            object value = Create(type, false);

            if (value is { })
            {
                return value;
            }

            return Create(type, true);
        }

        private object Create(Type type, bool throwOnError)
        {
            foreach (var constructor in type.GetConstructors(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic).OrderBy(c => c.IsPublic ? 0 : 1))
            {
                var parameters = constructor.GetParameters();
                var parameterValues = parameters.Select(p => Get(p.ParameterType)).ToArray();

                if (parameters.Any(p => p is null))
                {
                    if (throwOnError)
                    {
                        string missingServices = string.Join(", ", Enumerable.Range(0, parameters.Length).Where(i => parameterValues[i] is null).Select(i => parameters[i].ParameterType.Name));

                        throw new Exception($"Missing services ({missingServices})");
                    }
                    else
                    {
                        continue;
                    }
                }

                object value = constructor.Invoke(parameterValues);

                if (value is null)
                {
                    continue;
                }

                return value;
            }

            return default;
        }

        private static IDependencyService CreateGlobal()
        {
            AssemblyHelper.LoadAllReferencedAssemblies();

            return new DependencyService();
        }

        private class Entry
        {
            public Entry(Type type, object value, int priority)
            {
                Type = type;
                Value = value;
                Priority = priority;
            }

            public Type Type { get; }

            public object Value { get; }

            public int Priority { get; }
        }
    }
}
