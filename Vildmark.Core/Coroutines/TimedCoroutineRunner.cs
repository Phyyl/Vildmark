using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Vildmark.Coroutines
{
	public class TimedCoroutineRunner
	{
		private readonly int ms;
		private readonly ICoroutine coroutine;
		private IEnumerator enumerator;

		public bool Done { get; private set; }

		public TimedCoroutineRunner(int ms, ICoroutine coroutine)
		{
			this.ms = ms;
			this.coroutine = coroutine;
		}

		public int Update()
		{
			if (Done)
			{
				return 0;
			}

			int updates = 0;

			Stopwatch stopwatch = Stopwatch.StartNew();

			while (stopwatch.ElapsedMilliseconds < ms)
			{
				if (enumerator is null)
				{
					enumerator = coroutine.Run().GetEnumerator();
					updates++;
				}
				else
				{
					if (!enumerator.MoveNext())
					{
						Done = true;
						break;
					}

					updates++;
				}
			}

			return updates;
		}
	}
}
