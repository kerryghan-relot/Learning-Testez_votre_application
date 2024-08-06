
TestComptageMots(1, "", 0);
TestComptageMots(2, "mot", 1);
TestComptageMots(3, "Deux mots", 2); 

int CompteMots(string phrase)
{
    if (phrase.Length == 0) return 0;
    int countSpace = 0;
    foreach (char c in phrase)
    {
        if (c == ' ') countSpace++;
    }
    return countSpace + 1;
}

void TestComptageMots(int testNumber, string phrase, int expectedOutput)
{
    int output = CompteMots(phrase);
    if (output == expectedOutput)
    {
        Console.WriteLine($"Le test n°{testNumber} a réussi: \"{phrase}\" a {output} mots");
    }
    else
    {
        Console.WriteLine($"Le test n°{testNumber} a échoué: \"{phrase}\" a {output} mots ({expectedOutput} attendu)");
    }
}