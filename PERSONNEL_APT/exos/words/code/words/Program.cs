using System.Diagnostics.Tracing;
using System;
using System.Linq;

string[] words = { "bonjour", "hello", "monde", "vert", "rouge", "bleu", "jaune" };


var filtered = words.Where(w =>
!w.Contains('x') &&
w.Length >= 4 &&
w.Length == avgArrayLength(words)).ToList();

Console.WriteLine(filtered);
foreach (var word in filtered)
{
    Console.WriteLine(word);

}
int avgArrayLength(string[] words)
{
    int arrayLength=words.Length;
    Console.WriteLine(arrayLength);
    int charTotal =words.Sum(w => w.Length);
    Console.WriteLine(charTotal);

    return charTotal /arrayLength;
}
Console.ReadLine();