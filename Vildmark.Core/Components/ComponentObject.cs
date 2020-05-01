using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vildmark.Components
{
	public class ComponentObject : IComponentObject
	{
		private readonly Dictionary<Type, object> components = new Dictionary<Type, object>();

		public T GetComponent<T>()
		{
			return components.GetValueOrDefault(typeof(T)) is T t ? t : default;
		}

		public T RemoveComponent<T>()
		{
			if (components.TryGetValue(typeof(T), out object value))
			{
				components.Remove(typeof(T));

				return value is T t ? t : default;
			}

			return default;
		}

		public T SetComponent<T>(T value)
		{
			components.AddOrSet(typeof(T), value);

			return value;
		}

		public TInstance SetComponent<T, TInstance>() where TInstance : T, new()
		{
			TInstance value = new TInstance();

			SetComponent<T>(value);

			return value;
		}

		public bool TryGetComponent<T>(out T component)
		{
			if (components.TryGetValue(typeof(T), out object value) && value is T t)
			{
				component = t;
				return true;
			}

			component = default;
			return false;
		}

		public IEnumerable<object> GetComponents()
		{
			return components.Values.ToArray();
		}
	}
}
