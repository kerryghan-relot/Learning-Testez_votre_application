﻿
using static System.Net.Mime.MediaTypeNames;

TestComptageMots(1, "", 0);
TestComptageMots(2, "mot", 1);
TestComptageMots(3, "Deux mots", 2);
TestComptageMots(4, "Des Mots  avec des  espaces en trop  ", 7);
TestComptageMots(5, null, -1);
TestComptageMots(6, "Une phrase avec des accents où çà", 7);
TestComptageMots(7, "Plusieurs phrases. Avec des points, et des virgules.", 8);
TestComptageMots(8, "Une phrase ou j'ai mis une apostrophe", 8);
TestComptageMots(9, " Plein de phrases;   avec une combinaison de ce que l'on a vu précédement.  ", 14);
TestComptageMots(10, " Encore plus,compliqué; car il y  a des espaces en trop et   manquant! Mais aussi des accents à gogo.L'apostrophe c'est génial! Ãh Bon ? ", 26);

int CompteMots(string? phrase)
{
    if (phrase == null) return -1;
    char[] delimiter = [ ' ', ',', ';', '\'', '.', '!', '?' ];
    return phrase.Split(delimiter, StringSplitOptions.RemoveEmptyEntries).Length;
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