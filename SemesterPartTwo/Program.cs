using SemesterPartTwo.CFG;
using SemesterPartTwo.PDA;
using SemesterPartTwo.TM;
using System;
using System.Collections.Generic;

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

            //Console.WriteLine("Checking PDA");
            //var pda = CreatePDA.Create0N1NPDA();
            //foreach (var word in WordsToCheck0N1N())
            //{
            //    var result = pda.Execute(word);
            //    var acceptedRejectedString = result ? "accepted" : "rejected";
            //    Console.WriteLine($"Word: {word} has been {acceptedRejectedString}");
            //}

            //Console.WriteLine("Done. Press [Enter] to continue");
            //Console.ReadLine();

            //Console.WriteLine("Checking TM");
            //var inClassTM = CreateTM.CreateInClassTM();

            //foreach (var word in WordsToCheckForInClassTM())
            //{
            //    var result = inClassTM.Execute(word);
            //    var acceptedRejectedString = result ? "accepted" : "rejected";
            //    Console.WriteLine($"Word: {word} has been {acceptedRejectedString}");
            //}

            //Console.WriteLine("Done. Press [Enter] to continue");
            //Console.ReadLine();

            //Console.WriteLine("Checking TM");
            //var inBookTM = CreateTM.Create0WhoseLengthIsPowerTwo();

            //foreach (var word in WordsToCheckForInBookTM())
            //{
            //    var result = inBookTM.Execute(word);
            //    var acceptedRejectedString = result ? "accepted" : "rejected";
            //    Console.WriteLine($"Word: {word} has been {acceptedRejectedString}");
            //}

            //Console.WriteLine("Done. Press [Enter] to continue");
            //Console.ReadLine();

            //Console.WriteLine("Checking ND TM");
            //var ndTM = CreateTM.CreateInClassNDTM();
            //var upperLimit = 100;

            //foreach (var word in WordsToCheckForInClassTM())
            //{
            //    var result = ndTM.Execute(word, upperLimit);
            //    var acceptedRejectedString = result ? "accepted" : "rejected";
            //    Console.WriteLine($"Word: {word} has been {acceptedRejectedString}");
            //}

            //Console.WriteLine("Done. Press [Enter] to continue");
            //Console.ReadLine();

            //Console.WriteLine("Checking Linearly Bounded TM");
            //var inClassTMBounded = CreateTM.CreateInClassTMLinearlyBound();

            //foreach (var word in WordsToCheckForInClassTM())
            //{
            //    var result = inClassTMBounded.Execute(word);
            //    var acceptedRejectedString = result ? "accepted" : "rejected";
            //    Console.WriteLine($"Word: {word} has been {acceptedRejectedString}");
            //}

            //Console.WriteLine("Done. Press [Enter] to continue");
            //Console.ReadLine();

            //Console.WriteLine("Checking Linearly Bounded TM");
            //var failedLienarBounded = CreateTM.CreateFailedLinearBoundedTM();

            //foreach (var word in WordsToCheckForInClassTM())
            //{
            //    var result = failedLienarBounded.Execute(word);
            //    var acceptedRejectedString = result ? "accepted" : "rejected";
            //    Console.WriteLine($"Word: {word} has been {acceptedRejectedString}");
            //}

            //Console.WriteLine("Done. Press [Enter] to continue");
            //Console.ReadLine();

            //Console.WriteLine("Checking Binary Increment TM");
            //var binaryIncrement = CreateTM.CreateBinaryIncrement();

            //foreach (var word in BinaryIncrementWords())
            //{
            //    var result = binaryIncrement.Execute(word);
            //    var acceptedRejectedString = result ? "accepted" : "rejected";
            //    Console.WriteLine($"Word: {word} has been {acceptedRejectedString}");
            //}

            //Console.WriteLine("Done. Press [Enter] to continue");
            //Console.ReadLine();

            //Console.WriteLine("Checking Binary Decrement TM");
            //var binaryAddition = CreateTM.BinaryAdditionTM();

            //foreach (var word in BinaryAdditionWords())
            //{
            //    var result = binaryAddition.Execute(word);
            //    var acceptedRejectedString = result ? "accepted" : "rejected";
            //    Console.WriteLine($"Word: {word} has been {acceptedRejectedString}");
            //}

            //Console.WriteLine("Done. Press [Enter] to continue");
            //Console.ReadLine();

            Console.WriteLine("Checking MultiTape TM");
            var multiTape = CreateTM.CreateExampleMultiTapeTM();

            foreach (var word in MultiTapeTMWords())
            {
                var result = multiTape.Execute(word);
                var acceptedRejectedString = result ? "accepted" : "rejected";
                Console.WriteLine($"Word: {word} has been {acceptedRejectedString}");
            }

            Console.WriteLine("Done. Press [Enter] to continue");
            Console.ReadLine();
        }

        public static List<List<string>> MultiTapeTMWords()
        {
            var list = new List<List<string>>();
            var tapesOne = new List<string>
            {
                "1111111000110101",
                "1111111000110101"
            };

            list.Add(tapesOne);
            return list;
        }

        public static List<string> BinaryAdditionWords()
        {
            return new List<string>
            {
                "$000001+000001$",
                "$0010+0010$",
            };
        }

        public static List<string> BinaryIncrementWords()
        {
            return new List<string>
            {
                "$010101010#",
                "$011111111#",
                "$0001#",
            };
        }

        public static List<string> BinaryDecrement()
        {
            return new List<string>
            {
                "$010101010$",
                "$011111111$",
                "$0001$",
            };
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

        public static List<string> WordsToCheckForInClassTM()
        {
            return new List<string>
            {
                "0101#0101",
                "11#11",
                "00#11"
            };
        }

        public static List<string> WordsToCheckForInBookTM()
        {
            return new List<string>
            {
                "0000",
                "00",
                "00000"
            };
        }
    }
}
