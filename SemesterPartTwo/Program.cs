using SemesterPartTwo.CFG;
using System;

namespace SemesterPartTwo
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Testing out CFG Creation");

            //var inClassCFG = CreateCFGs.CreateInClassCFG();
            //for (int counter = 0; counter < 5; counter++)
            //{
            //    var result = inClassCFG.CalculateStringsInLanguageOfDepth(counter);
            //    result.Reverse();
            //    Console.WriteLine($"Strings in the language of depth: {counter} are: {string.Join(",", result)}");
            //}

            //for (int counter = 0; counter < 10; counter++)
            //{
            //    var result = inClassCFG.RandomStringFromCFG(100);
            //    Console.WriteLine($"Random string from the language: {result}");
            //}

            //Console.WriteLine("Done. Press [Enter] to continue.");
            //Console.ReadLine();

            Console.WriteLine("Testing Chris's CFG to see if we get similar results");
            var chrisCFG = CreateCFGs.CreateChrisCFG();
            for (int counter = 0; counter < 10; counter++)
            {
                var result = chrisCFG.CalculateStringsInLanguageOfDepth(counter);
                result.Reverse();
                Console.WriteLine($"Strings in the language of depth: {counter} are: {string.Join(",", result)}");
            }

            for (int counter = 0; counter < 10; counter++)
            {
                var result = chrisCFG.RandomStringFromCFG(25);
                Console.WriteLine($"Random string from the language: {result}");
            }

            Console.WriteLine("Done. Press [Enter] to continue");
            Console.ReadLine();
        }
    }
}
