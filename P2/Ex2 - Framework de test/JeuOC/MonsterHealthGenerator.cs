namespace JeuOC
{
    public class MonsterHealthGenerator : IMonsterHealthGenerator
    {
        private readonly Random rnd;
        public MonsterHealthGenerator()
        {
            rnd = new Random();
        }
        public int Generate()
        {
            // Half of the time, it will generate an "easy" monster one-hitable.
            if (rnd.Next(2) == 0) return 1;

            // Otherwise, return a monster with several HPs.
            return rnd.Next(2, 10);
        }
    }
}
