using BenchmarkDotNet.Running;
using MappingBenchmark.Benchmarks;

var summary = BenchmarkRunner.Run<Benchmarks>();

Console.WriteLine(summary.ToString());
Console.ReadLine();