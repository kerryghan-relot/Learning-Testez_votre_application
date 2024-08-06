﻿
TestComptageMots(1, "", 0);
TestComptageMots(2, "mot", 1);
TestComptageMots(3, "Deux mots", 2);
TestComptageMots(4, "Des Mots  avec des  espaces en trop  ", 7);
TestComptageMots(5, null, -1);

int CompteMots(string? phrase)
{
    if (phrase == null) return -1;
    return phrase.Split(' ').Count(mot => mot.Length > 0);
}

void TestComptageMots(int testNumber, string? phrase, int expectedOutput)
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