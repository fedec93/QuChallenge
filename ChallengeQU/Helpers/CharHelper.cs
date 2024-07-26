namespace ChallengeQU.Helpers
{
    public static class CharHelper
    {
        private static Random random = new Random();

        public static char GetRandomChar()
        {
            return (char)random.Next('A', 'Z' + 1);
        }
    }
}
