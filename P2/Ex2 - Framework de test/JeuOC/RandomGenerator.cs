namespace JeuOC
{
    public class RandomGenerator : IRandomGenerator
    {
        private readonly Random _random = new Random();

        public int Generate(int start, int end)
        {
            return _random.Next(start, end);
        }
    }
}
