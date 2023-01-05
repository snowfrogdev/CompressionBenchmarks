using System;

using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;

namespace Compression.Benchmarks;

[MemoryDiagnoser]
public class Benchmarks
{
    [Benchmark]
    public void Scenario1()
    {
        // Implement your benchmark here
    }

    [Benchmark]
    public void Scenario2()
    {
        // Implement your benchmark here
    }
}

