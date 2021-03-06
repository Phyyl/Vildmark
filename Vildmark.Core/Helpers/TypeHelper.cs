﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Vildmark.Helpers
{
    public static class TypeHelper
    {
        private static bool staticConstructorsRan;

        private static Type[] typeCache;

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
                type.TypeInitializer?.Invoke(null, null);
            }

            staticConstructorsRan = true;
        }

        public static IEnumerable<Type> TypesOf<T>() => typeCache.Where(typeof(T).IsAssignableFrom);

        public static IEnumerable<Type> TypesOf<T, TAttribute>() => typeCache.Where(typeof(T).IsAssignableFrom);

        public static IEnumerable<Type> ConcreteTypesOf<T>() => TypesOf<T>().Where(t => !t.IsAbstract);

        public static IEnumerable<Type> ConcreteTypesOf<T, TAttribute>() => ConcreteTypesOf<T>().Where(HasAttribute<TAttribute>);

        public static IEnumerable<Type> TypesWith<TAttribute>() => typeCache.Where(HasAttribute<TAttribute>);

        public static bool HasAttribute<T, TAttribute>() => HasAttribute(typeof(T), typeof(TAttribute));

        public static bool HasAttribute<TAttribute>(Type type) => HasAttribute(type, typeof(TAttribute));

        public static bool HasAttribute(Type type, Type attributeType) => Attribute.IsDefined(type, attributeType);

        public static object CreateInstanceOrDefault(Type type)
        {
            try
            {
                return Activator.CreateInstance(type, true);
            }
            catch
            {
                return null;
            }
        }
    }
}
