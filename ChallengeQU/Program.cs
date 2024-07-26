using ChallengeQU.Challenge;
using ChallengeQU.Helpers;
using System.Diagnostics;

namespace ChallengeQU
{
    public class Program
    {
        // ADJUST THESE PARAMS AS NEEDED

        // Numbers of words in the stream
        const int WORD_STREAM_SIZE = 100;

        // Max length for each random generated word in the stream
        const int WORD_STREAM_MAX_LENGTH = 12;

        // Matrix width
        const int MATRIX_ROWS = 64;

        // Matrix height
        const int MATRIX_COLUMNS = 64;

        public static void Main(string[] args)
        {
            var words = WordHelper.GenerateRandomWords(WORD_STREAM_SIZE, WORD_STREAM_MAX_LENGTH);

            var matrix = MatrixHelper.GenerateMatrixWithWords(words, MATRIX_ROWS, MATRIX_COLUMNS);

            var list = MatrixHelper.GetMatrixAsListOfStrings(matrix);

            PrintMatrix(matrix);

            Console.WriteLine("STARTING TESTS..");

            Run<WordFinder>(list, words);
            Run<WordFinderParallel>(list, words);
            Console.WriteLine();
        }

        public static void Run<T>(List<string> list, List<string> words) where T : IWordFinder
        {
            var sw = Stopwatch.StartNew();

            var wordFinder = (T)Activator.CreateInstance(typeof(T), list);

            Console.WriteLine($"GENERATE MATRIX {typeof(T)}: {sw.ElapsedMilliseconds} ms");

            var result = wordFinder.Find(words);

            Console.WriteLine($"FINISH FINDINGS {typeof(T)}: {sw.ElapsedMilliseconds} ms");
        }


        public static void PrintMatrix(char[,] matrix)
        {
            var rowCount = matrix.GetLength(0);
            var columnCount = matrix.GetLength(1);

            for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < columnCount; columnIndex++)
                {
                    Console.Write($"{matrix[rowIndex, columnIndex]} ");
                }

                Console.WriteLine();
            }
        }
    }
}
