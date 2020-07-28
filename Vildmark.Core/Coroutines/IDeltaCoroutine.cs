using System;
using System.Collections.Generic;
using System.Text;

namespace Vildmark.Coroutines
{
	public interface IDeltaCoroutine : ICoroutine
	{
		float Delta { get; set; }
	}
}
