namespace JeuOC
{
    public class Heros
    {
        public int PointDeVies { get; private set; }
        public int Points { get; private set; }

        public Heros(int pointDeVies)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(pointDeVies, "Monster cannot be innitialized with a negative amount of HP");

            PointDeVies = pointDeVies;
        }

        public void GagneUnCombat()
        {
            Points++;
        }

        public void PerdsUnCombat(int nb)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(nb, "Heros cannot lose a negative amount of HP");

            PointDeVies -= nb;
        }
    }
}
