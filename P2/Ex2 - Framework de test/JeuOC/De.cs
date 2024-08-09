namespace JeuOC
{
    public class De : ILanceurDeDe
    {
        private readonly Random random;

        public De()
        {
            random = new Random();
        }

        public int Lance()
        {
            return random.Next(1, 7);
        }
    }
}
