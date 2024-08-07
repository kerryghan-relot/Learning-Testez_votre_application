namespace JeuOC
{
    class Program
    {
        static void Main(string[] args)
        {
            var ihm = new Ihm(new ConsoleDeSortie(), new De());
            ihm.Demarre();
        }
    }
}
