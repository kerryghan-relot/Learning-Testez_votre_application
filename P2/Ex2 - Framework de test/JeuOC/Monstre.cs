namespace JeuOC
{
    public class Monstre
    {
        public int PointDeVie { get; private set; }

        public Monstre(int pointDeVies)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(pointDeVies, "Monster cannot be innitialized with a negative amount of HP");
            PointDeVie = pointDeVies;
        }

        public void PerdUnCombat(int nb)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(nb, "Monster cannot lose a negative amount of HP");

            PointDeVie -= nb + 1;
        }
    }
}
