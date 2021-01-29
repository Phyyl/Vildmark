using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vildmark
{
    public class Benchmark
    {
        private static readonly Dictionary<string, Benchmark> benchmarks = new Dictionary<string, Benchmark>();

        public static Benchmark Get(string name)
        {
            if (!benchmarks.TryGetValue(name, out Benchmark benchmark))
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

        private Stopwatch stopwatch = Stopwatch.StartNew();

        private readonly List<float> samples = new List<float>();

        public float Average => samples.Average();

        public void Clear()
        {
            samples.Clear();
        }

        public IDisposable Start()
        {
            stopwatch.Restart();

            return new StartContext(this);
        }

        private class StartContext : IDisposable
        {
            private Benchmark benchmark;

            public StartContext(Benchmark benchmark)
            {
                this.benchmark = benchmark;
            }

            public void Dispose()
            {
                benchmark.samples.Add(benchmark.stopwatch.ElapsedTicks / 10000f);
            }
        }
    }
}
