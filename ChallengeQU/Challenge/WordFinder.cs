namespace ChallengeQU.Challenge
{
    public class WordFinder : IWordFinder
    {
        private readonly char[,] _matrix;
        private readonly int _rowCount;
        private readonly int _columnCount;

        public WordFinder(IEnumerable<string> matrix)
        {
            // Check if Matrix input is null or empty
            if (matrix == null || !matrix.Any())
            {
                throw new ArgumentException(nameof(matrix), "Matrix is empty");
            }

            // Convert the IEnumerable to a List to avoid multiple enumerators
            var matrixList = matrix.ToList();

            // Check if any row has a different length
            if (matrixList.Select(x => x.Length).Distinct().Count() > 1)
            {
                throw new ArgumentException(nameof(matrix), "Matrix contains rows with different lengths");
            }

            _rowCount = matrixList.Count;
            _columnCount = matrixList[0].Length;

            // Build a 2D matrix with a fixed size instad of jagged arrays.
            // Since all strings in the matrix input containt the same number of characters,
            // we can take advantage of this for better performance.
            _matrix = new char[_rowCount, _columnCount];

            for (var rowIndex = 0; rowIndex < _rowCount; rowIndex++)
            {
                for (var colIndex = 0; colIndex < _columnCount; colIndex++)
                {
                    // Convert char to UpperCase to do case-insensitive search later.
                    _matrix[rowIndex, colIndex] = char.ToUpper(matrixList[rowIndex][colIndex]);
                }
            }
        }

        public virtual IEnumerable<string> Find(IEnumerable<string> wordStream)
        {
            var wordSet = GetWordSet(wordStream);
            var wordCounter = new Dictionary<string, int>();

            foreach (var word in wordSet)
            {
                var wordCount = SearchWord(word);

                if (wordCount > 0)
                {
                    // Avoid saving 0 count words in the dictionary
                    wordCounter[word] = wordCount;
                }
            }

            return wordCounter
                .OrderByDescending(x => x.Value)
                .Take(10)
                .Select(x => x.Key);
        }

        protected HashSet<string> GetWordSet(IEnumerable<string> wordStream)
        {
            // Remove all null or empty words and convert the valid words into upper case
            wordStream = wordStream?.Where(x => !string.IsNullOrEmpty(x)).Select(x => x.ToUpper());

            if (wordStream == null || !wordStream.Any())
            {
                throw new ArgumentException(nameof(wordStream), "WordStream is empty");
            }

            // Init a hash set with all distinct words in the stream
            return new HashSet<string>(wordStream);
        }

        protected int SearchWord(string word)
        {
            var wordLength = word.Length;
            var wordCount = 0;

            // Iterate and check both, horizontal and vertical, in the same loop.
            for (int rowIndex = 0; rowIndex < _rowCount; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < _columnCount; columnIndex++)
                {
                    // NOTE: Before checking Horizontally or Vertically, we check if the
                    // column or row can fit the word from the starting cell and word's length

                    // Search horizontally
                    if (columnIndex <= _columnCount - wordLength &&
                        CheckWord(rowIndex, columnIndex, word, isHorizontalSearch: true))
                    {
                        wordCount++;
                    }

                    // Search vertically
                    if (rowIndex <= _rowCount - wordLength &&
                        CheckWord(rowIndex, columnIndex, word, isHorizontalSearch: false))
                    {
                        wordCount++;
                    }
                }
            }

            return wordCount;
        }


        // Check if the word exists horizontally from the starting point
        private bool CheckWord(int rowIndex, int columnIndex, string word, bool isHorizontalSearch)
        {
            for (var index = 0; index < word.Length; index++)
            {
                var value = isHorizontalSearch
                    ? _matrix[rowIndex, columnIndex + index]
                    : _matrix[rowIndex + index, columnIndex];

                if (value != word[index])
                {
                    return false;
                }
            }

            return true;
        }

        // Expose matrix to perform some checks with UnitTests
        public char[,] GetMatrix() => _matrix;
    }
}
