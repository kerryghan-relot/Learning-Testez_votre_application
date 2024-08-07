namespace JeuOC
{
    public class Jeu
    {
        private readonly IFournisseurMeteo _fournisseurMeteo;
        public Heros Heros { get; }

        public Jeu(FournisseurMeteo fournisseurMeteo)
        {
            Heros = new Heros(15);
            _fournisseurMeteo = fournisseurMeteo;
        }

        public Resultat Tour(int deHeros, int deMonstre)
        {
            if (deHeros < 1 || deHeros > 6)
                throw new ArgumentOutOfRangeException(nameof(deHeros), "deHeros doit être compris entre 1 et 6 inclu.");
            if (deMonstre < 1 || deMonstre > 6)
                throw new ArgumentOutOfRangeException(nameof(deMonstre), "deMonstre doit être compris entre 1 et 6 inclu.");

            if (GagneLeCombat(deHeros, deMonstre))
            {
                Heros.GagneUnCombat();
                return Resultat.Gagne;
            }
            else
            {
                Heros.PerdsUnCombat(deMonstre - deHeros);
                return Resultat.Perdu;
            }
        }

        private bool GagneLeCombat(int de1, int de2)
        {
            return de1 >= de2;
        }
    }
}
