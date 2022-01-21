using System.Collections.Generic;

namespace Vildmark.Coroutines
{
    public interface ICoroutine
	{
		IEnumerable<bool> Run();
	}
}
