namespace ChallengeQU.Challenge
{
    public interface IWordFinder
    {
        public IEnumerable<string> Find(IEnumerable<string> wordstream);
    }
}
