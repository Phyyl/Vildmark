using System.Diagnostics;

namespace Vildmark;

public class Benchmark
{
    private static readonly Dictionary<string, Benchmark> benchmarks = [];

    public static Benchmark Get(string name)
    {
        if (!benchmarks.TryGetValue(name, out Benchmark? benchmark))
        {
            benchmark = new Benchmark();
            benchmarks.Add(name, benchmark);
        }

        return benchmark;
    }

    public static IDisposable Start(string name)
    {
        return Get(name).Start();
    }

    private readonly Stopwatch stopwatch = Stopwatch.StartNew();

    private readonly List<float> samples = [];

    public float Average => samples.Average();

    public int MaxSamples { get; } = 10;

    public void Clear()
    {
        samples.Clear();
    }

    public IDisposable Start()
    {
        stopwatch.Restart();

        return new StartContext(this);
    }

    private class StartContext(Benchmark benchmark) : IDisposable
    {
        public void Dispose()
        {
            benchmark.samples.Add(benchmark.stopwatch.ElapsedTicks / 10000f);

            if (benchmark.samples.Count > benchmark.MaxSamples)
            {
                benchmark.samples.RemoveAt(0);
            }
        }
    }
}
