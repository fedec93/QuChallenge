namespace Helpers
{
    public class WordHelper
    {
        private static Random random = new Random();

        public static List<string> GenerateRandomWords(int count, int maxLength)
        {
            var words = new List<string>();

            for (var wordIndex = 0; wordIndex < count; wordIndex++)
            {
                var wordLength = random.Next(3, maxLength);

                var charArray = new char[wordLength];

                for (var charIndex = 0; charIndex < wordLength; charIndex++)
                {
                    charArray[charIndex] = GetRandomChar();
                }

                words.Add(new string(charArray));
            }

            return words;
        }

        private static char GetRandomChar()
        {
            return (char)random.Next('A', 'Z' + 1);
        }
    }
}
