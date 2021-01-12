using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Vildmark
{
    public static class Extensions
    {
        public delegate bool SetPredicate<T>(T currentValue, T newValue);

        public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.TryGetValue(key, out TValue value) ? value : default;
        }

        public static TValue AddOrSet<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }

            return value;
        }

        public static TValue AddOrSet<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value, SetPredicate<TValue> setPredicate)
        {
            if (dictionary.ContainsKey(key))
            {
                TValue current = dictionary[key];

                if (!setPredicate(current, value))
                {
                    return dictionary[key];
                }

                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }

            return value;
        }

        public static bool TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
            {
                return false;
            }

            dictionary.Add(key, value);

            return true;
        }

        public static void RemoveAll<T>(this List<T> list, IEnumerable<T> values)
        {
            foreach (var value in values)
            {
                list.Remove(value);
            }
        }

        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, TResult> selector)
        {
            int index = 0;

            foreach (var item in source)
            {
                yield return selector(item, index++);
            }
        }
    }
}
