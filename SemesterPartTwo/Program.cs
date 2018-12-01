using SemesterPartTwo.CFG;
using SemesterPartTwo.PDA;
using System;
using System.Collections.Generic;

namespace SemesterPartTwo
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testing out CFG Creation");

            var inClassCFG = CreateCFGs.CreateInClassCFG();
            for (int counter = 0; counter < 5; counter++)
            {
                var result = inClassCFG.CalculateStringsInLanguageOfDepth(counter);
                result.Reverse();
                Console.WriteLine($"Strings in the language of depth: {counter} are: {string.Join(",", result)}");
            }

            for (int counter = 0; counter < 10; counter++)
            {
                var result = inClassCFG.RandomStringFromCFG(100);
                Console.WriteLine($"Random string from the language: {result}");
            }

            Console.WriteLine("Done. Press [Enter] to continue.");
            Console.ReadLine();

            //Console.WriteLine("Testing Chris's CFG to see if we get similar results");
            //var chrisCFG = CreateCFGs.CreateChrisCFG();
            //for (int counter = 0; counter < 10; counter++)
            //{
            //    var result = chrisCFG.CalculateStringsInLanguageOfDepth(counter);
            //    result.Reverse();
            //    Console.WriteLine($"Strings in the language of depth: {counter} are: {string.Join(",", result)}");
            //}

            //for (int counter = 0; counter < 10; counter++)
            //{
            //    var result = chrisCFG.RandomStringFromCFG(25);
            //    Console.WriteLine($"Random string from the language: {result}");
            //}

            //Console.WriteLine("Done. Press [Enter] to continue");
            //Console.ReadLine();

            Console.WriteLine("Checking PDA");
            var pda = CreatePDA.Create0N1NPDA();
            foreach (var word in WordsToCheck0N1N())
            {
                var result = pda.Execute(word);
                var acceptedRejectedString = result ? "accepted" : "rejected";
                Console.WriteLine($"Word: {word} has been {acceptedRejectedString}");
            }
        }

        public static List<string> WordsToCheck0N1N()
        {
            return new List<string>
            {
                "01",
                "00001111",
                "01111"
            };
        }
    }
}
