using ChallengeQU.Challenge;

namespace ChallengeQU.Tests
{
    [TestClass]
    public class WordFinderTests
    {
        [TestMethod]
        public void FillSquareMatrix()
        {
            var wordFinder = new WordFinder(["aaaa", "bbbb", "cccc", "dddd"]);

            var matrix = wordFinder.GetMatrix();

            var generatedMatrix = new[,] {
                { 'A', 'A', 'A', 'A' },
                { 'B', 'B', 'B', 'B' },
                { 'C', 'C', 'C', 'C' },
                { 'D', 'D', 'D', 'D' }
            };

            CollectionAssert.AreEqual(generatedMatrix, matrix);
        }

        [TestMethod]
        public void FillNonSquareMatrix()
        {
            var wordFinder = new WordFinder(["aaaa", "bbbb"]);

            var matrix = wordFinder.GetMatrix();

            var generatedMatrix = new[,] {
                { 'A', 'A', 'A', 'A' },
                { 'B', 'B', 'B', 'B' }
            };

            CollectionAssert.AreEqual(generatedMatrix, matrix);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow([])]
        public void EmptyMatrix(string[] input)
        {
            Assert.ThrowsException<ArgumentException>(() => new WordFinder(input));
        }


        [TestMethod]
        [DataRow(["aa", "bbbb"])]
        [DataRow(["aaaa", "bbbb", "cccccc"])]
        public void VariableRowsLengthMatrix(string[] input)
        {
            Assert.ThrowsException<ArgumentException>(() => new WordFinder(input));
        }

        [TestMethod]
        public void FindWordsBasic()
        {
            var wordFinder = new WordFinder(["abcdc", "rgwio", "chill", "pqnsd", "uvdxy"]);

            var result = wordFinder.Find(["cold", "wind", "snow", "chill"]);

            CollectionAssert.AreEquivalent(new[] { "COLD", "WIND", "CHILL" }, result.ToArray());
        }

        [TestMethod]
        public void FindWordsParallelBasic()
        {
            var wordFinder = new WordFinderParallel(["abcdc", "rgwio", "chill", "pqnsd", "uvdxy"]);

            var result = wordFinder.Find(["cold", "wind", "snow", "chill"]);

            CollectionAssert.AreEquivalent(new[] { "COLD", "WIND", "CHILL" }, result.ToArray());
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow([])]
        [DataRow([null])]
        [DataRow([""])]
        public void EmptyWordStream(string[] input)
        {
            var wordFinder = new WordFinder(["abcdc", "rgwio", "chill", "pqnsd", "uvdxy"]);

            Assert.ThrowsException<ArgumentException>(() => wordFinder.Find(input));
        }
    }
}