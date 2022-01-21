using System.Diagnostics;

namespace Vildmark.Coroutines
{
    public class CoroutineRunner<TCoroutine> where TCoroutine : ICoroutine
	{
		private readonly bool autoReset;
		private IEnumerator<bool>? enumerator;

		public TCoroutine Coroutine { get; }

		public bool Done { get; private set; }

		public CoroutineRunner(TCoroutine coroutine, bool autoReset = true)
		{
			this.Coroutine = coroutine;
			this.autoReset = autoReset;
		}

		public float Update(float delta)
		{
			if (Done)
			{
				return 0;
			}

			int count = 0;

			Stopwatch stopwatch = Stopwatch.StartNew();

			while (stopwatch.Elapsed.TotalSeconds < delta)
			{
				if (Coroutine is IDeltaCoroutine deltaCoroutine)
				{
					deltaCoroutine.Delta = delta;
				}

				enumerator ??= Coroutine.Run().GetEnumerator();

				Done = !enumerator.MoveNext() || !enumerator.Current;

				count++;

				if (Done)
				{
					enumerator = null;

					if (autoReset)
					{
						Reset();
					}

					break;
				}
			}

			return (float)stopwatch.Elapsed.TotalSeconds;
		}

		public void Reset()
		{
			enumerator = null;
			Done = false;
		}
	}
}
