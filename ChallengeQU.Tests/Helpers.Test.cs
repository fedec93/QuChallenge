using ChallengeQU.Helpers;

namespace ChallengeQU.Tests
{
    [TestClass]
    public class HelpersTests
    {
        [TestMethod]
        public void CharHelper_GetRandomChar()
        {
            var randomChar = CharHelper.GetRandomChar();

            Assert.IsTrue(randomChar >= 'A' && randomChar <= 'Z');
        }

        [TestMethod]
        [DataRow(5, 5)]
        [DataRow(10, 5)]
        [DataRow(5, 10)]
        [DataRow(10, 10)]
        public void WordHelper_GenerateRandomWords(int count, int length)
        {
            var randomWords = WordHelper.GenerateRandomWords(count, length);

            Assert.AreEqual(count, randomWords.Count);

            foreach (var word in randomWords)
            {
                Assert.IsTrue(!string.IsNullOrEmpty(word));

                Assert.IsTrue(word.Length <= length);
            }
        }

        [TestMethod]
        [DataRow(8, 8, 4)]
        [DataRow(16, 8, 5)]
        [DataRow(8, 16, 6)]
        [DataRow(32, 32, 8)]
        public void MatrixHelper_GenerateMatrixWithWords(int rows, int columns, int wordLength)
        {
            var words = WordHelper.GenerateRandomWords(5, wordLength);

            var matrix = MatrixHelper.GenerateMatrixWithWords(words, rows, columns);

            Assert.AreEqual(rows, matrix.GetLength(0));
            Assert.AreEqual(columns, matrix.GetLength(1));

            for (var rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                for (var colIndex = 0; colIndex < columns; colIndex++)
                {
                    var value = matrix[rowIndex, colIndex];

                    Assert.IsTrue(value >= 'A' && value <= 'Z');
                }
            }
        }

        [TestMethod]
        [DataRow(8, 8, 4)]
        [DataRow(16, 8, 5)]
        [DataRow(8, 16, 6)]
        [DataRow(32, 32, 8)]
        public void MatrixHelper_GetMatrixAsListOfStrings(int rows, int columns, int wordLength)
        {
            var words = WordHelper.GenerateRandomWords(5, wordLength);

            var matrix = MatrixHelper.GenerateMatrixWithWords(words, rows, columns);

            var list = MatrixHelper.GetMatrixAsListOfStrings(matrix);

            foreach (var item in list)
            {
                Assert.IsTrue(!string.IsNullOrEmpty(item));
            }

            for (var rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                for (var colIndex = 0; colIndex < columns; colIndex++)
                {
                    var value = matrix[rowIndex, colIndex];

                    Assert.IsTrue(list[rowIndex][colIndex] == value);
                }
            }
        }
    }
}