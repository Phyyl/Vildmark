﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Vildmark.Components
{
	public interface IComponentObject
	{
		T SetComponent<T>(T value);
		TInstance SetComponent<T, TInstance>() where TInstance : T, new();

		T GetComponent<T>();
		bool TryGetComponent<T>(out T component);

		T RemoveComponent<T>();

		IEnumerable<object> GetComponents();
	}
}
