namespace JeuOC
{
    class Program
    {
        static void Main(string[] args)
        {
            Ihm ihm = new Ihm(new ConsoleDeSortie(),
                              new De(),
                              new FournisseurMeteo(),
                              new MonsterHealthGenerator(),
                              new RandomGenerator());
            ihm.Demarre();
        }
    }
}
