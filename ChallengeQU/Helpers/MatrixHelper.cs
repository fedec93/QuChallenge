namespace ChallengeQU.Helpers
{
    public class MatrixHelper
    {
        private static Random random = new Random();

        public static char[,] GenerateMatrixWithWords(List<string> words, int rowCount, int columnCount)
        {
            var matrix = new char[rowCount, columnCount];

            // Initialize the matrix with random characters
            for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < columnCount; columnIndex++)
                {
                    matrix[rowIndex, columnIndex] = CharHelper.GetRandomChar();
                }
            }

            // Add some repeated words
            var repeatedWordCount = random.Next(words.Count + 1, words.Count * 5);
            var wordsWithRepetitions = new List<string>(repeatedWordCount);

            for (int i = 0; i < repeatedWordCount; i++)
            {
                wordsWithRepetitions.Add(words[random.Next(words.Count)]);
            }

            // Place the words in the matrix
            foreach (var word in wordsWithRepetitions)
            {
                // 0 for horizontal, 1 for vertical
                var direction = random.Next(2);

                if (direction == 0 && word.Length <= columnCount)
                {
                    // Place horizontally
                    var rowIndex = random.Next(rowCount);
                    var columnIndex = random.Next(columnCount - word.Length + 1);

                    for (var index = 0; index < word.Length; index++)
                    {
                        matrix[rowIndex, columnIndex + index] = word[index];
                    }
                }
                else if (direction == 1 && word.Length <= rowCount)
                {
                    // Place vertically
                    int rowIndex = random.Next(rowCount - word.Length + 1);
                    int columnIndex = random.Next(columnCount);

                    for (var index = 0; index < word.Length; index++)
                    {
                        matrix[rowIndex + index, columnIndex] = word[index];
                    }
                }
            }

            return matrix;
        }

        public static List<string> GetMatrixAsListOfStrings(char[,] matrix)
        {
            var rowCount = matrix.GetLength(0);
            var columnCount = matrix.GetLength(1);

            List<string> result = new List<string>(rowCount);

            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                var rowArray = new char[columnCount];

                for (var columnIndex = 0; columnIndex < columnCount; columnIndex++)
                {
                    rowArray[columnIndex] = matrix[rowIndex, columnIndex];
                }

                result.Add(new string(rowArray));
            }

            return result;
        }

        private static bool CanPlaceHorizontally(char[,] matrix, int row, int col, string word)
        {
            var columnCount = matrix.GetLength(1);

            if (col + word.Length > columnCount)
            {
                return false;
            }

            return true;
        }

        private static bool CanPlaceVertically(char[,] matrix, int row, int col, string word)
        {
            var rowCount = matrix.GetLength(0);

            if (row + word.Length > rowCount)
            {
                return false;
            }

            return true;
        }
    }
}
