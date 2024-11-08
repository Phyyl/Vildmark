using System.Diagnostics;

namespace Vildmark.Coroutines;

public class CoroutineRunner<TCoroutine>(TCoroutine coroutine, bool autoReset = true) where TCoroutine : ICoroutine
	{
    private IEnumerator<bool>? enumerator;

    public TCoroutine Coroutine { get; } = coroutine;

    public bool Done { get; private set; }

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
