using System.Collections.Concurrent;

namespace ChallengeQU.Challenge
{
    public class WordFinderParallel : WordFinder
    {
        public WordFinderParallel(IEnumerable<string> matrix) : base(matrix)
        {

        }

        public override IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            var wordSet = GetWordSet(wordstream);

            // Use a concurrent dictionary for thread-safe operations
            var wordCounter = new ConcurrentDictionary<string, int>();

            Parallel.ForEach(wordSet, word =>
            {
                var wordCount = SearchWord(word);

                // Avoid saving 0 count words in the dictionary
                if (wordCount > 0)
                {
                    wordCounter[word] = wordCount;
                }
            });

            return wordCounter
                .OrderByDescending(x => x.Value)
                .Take(10)
                .Select(x => x.Key);
        }
    }
}
