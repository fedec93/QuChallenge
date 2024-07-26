using BenchmarkDotNet.Running;
using ChallengeQU.Benchmarks.Benchmarks;

namespace ChallengeQU.Benchmarks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<WordFinderBenchmarks>();
        }
    }
}
