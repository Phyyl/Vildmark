using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Vildmark.Coroutines
{
	public interface ICoroutine
	{
		IEnumerable Run();
	}
}
