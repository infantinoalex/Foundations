using SemesterPartTwo.CFG;
using System;

namespace SemesterPartTwo
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testing out CFG Creation");

            var inClassCFG = CreateCFGs.CreateInClassCFG();
            var stringsInDepth0 = inClassCFG.CalculateStringsInLanguageOfDepth(0);

            for (int counter = 0; counter < 5; counter++)
            {
                var result = inClassCFG.CalculateStringsInLanguageOfDepth(counter);
                result.Reverse();
                Console.WriteLine($"Strings in the language of depth: {counter} are: {string.Join(",", result)}");
            }

            for (int counter = 0; counter < 10; counter++)
            {
                var result = inClassCFG.RandomStringFromCFG();
                Console.WriteLine($"Random string from the language: {result}");
            }

            Console.WriteLine("Done. Press [Enter] to exit.");
            Console.ReadLine();
        }
    }
}
