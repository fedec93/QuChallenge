namespace ChallengeQU.Helpers
{
    public class WordHelper
    {
        private static Random random = new Random();

        public static List<string> GenerateRandomWords(int count, int maxLength)
        {
            var words = new List<string>();

            for (var wordIndex = 0; wordIndex < count; wordIndex++)
            {
                // Set a minimun of 3 chars for the generated word
                var wordLength = random.Next(3, maxLength);

                // Init a char array with the word length to avoid multiple string allocation and concatenation
                var charArray = new char[wordLength];

                for (var charIndex = 0; charIndex < wordLength; charIndex++)
                {
                    charArray[charIndex] = CharHelper.GetRandomChar();
                }

                words.Add(new string(charArray));
            }

            return words;
        }


    }
}
