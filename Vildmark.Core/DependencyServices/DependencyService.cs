using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Vildmark.DependencyServices
{
    public class DependencyService : IDependencyService
    {
        private readonly Dictionary<Type, List<Entry>> services = new Dictionary<Type, List<Entry>>();

        public DependencyService()
        {
            Register<IDependencyService>(this);
        }

        public T Get<T>() where T : class
        {
            return Get(typeof(T)) as T;
        }

        public object Get(Type type)
        {
            return services.GetValueOrDefault(type)?.FirstOrDefault()?.Value;
        }

        public IEnumerable<object> GetAll(Type type)
        {
            return services.GetValueOrDefault(type)?.Select(e => e.Value);

        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            return GetAll(typeof(T)) as IEnumerable<T>;

        }

        public object Register(Type type, object value, int priority = 0)
        {
            services.TryAdd(type, new List<Entry>());
            services[type].Add(new Entry(type, value, priority));
            services[type].Sort((a, b) => a.Priority - b.Priority);

            return value;
        }

        public T Register<T>(T value, int priority = 0) where T : class
        {
            return Register(typeof(T), value, priority) as T;
        }

        public TInstance Register<T, TInstance>(int priority = 0)
            where T : class
            where TInstance : class, T, new()
        {
            return Register<T>(new TInstance(), priority) as TInstance;
        }

        public T CreateInstance<T>() where T : class
        {
            return CreateInstance(typeof(T)) as T;
        }

        public object CreateInstance(Type type)
        {
            object value = CreateInstance(type, false);

            if (value is { })
            {
                return value;
            }

            return CreateInstance(type, true);
        }

        private object CreateInstance(Type type, bool throwOnError)
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
