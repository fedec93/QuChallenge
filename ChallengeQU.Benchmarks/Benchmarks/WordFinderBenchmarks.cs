using BenchmarkDotNet.Attributes;
using ChallengeQU.Challenge;
using ChallengeQU.Helpers;

namespace ChallengeQU.Benchmarks.Benchmarks
{
    [MemoryDiagnoser(false)]
    public class WordFinderBenchmarks
    {
        [Params(8, 32, 64)]
        public int _matrixSize;

        [Params(10, 100, 250)]
        public int _wordCount;

        private List<string> _matrix;
        private List<string> _words;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _words = WordHelper.GenerateRandomWords(_wordCount, _matrixSize);

            var charMatrix = MatrixHelper.GenerateMatrixWithWords(_words, _matrixSize, _matrixSize);

            _matrix = MatrixHelper.GetMatrixAsListOfStrings(charMatrix);
        }


        [Benchmark]
        public void WordFinder()
        {
            var wordFinder = new WordFinder(_matrix);

            var result = wordFinder.Find(_words);
        }

        [Benchmark]
        public void WordFinderParallel()
        {
            var wordFinder = new WordFinderParallel(_matrix);

            var result = wordFinder.Find(_words);
        }
    }
}
