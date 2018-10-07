using System;
using System.Collections.Generic;
using FiniteAutomata.DFAFactory;
using FiniteAutomata.NFAFactory;

namespace FiniteAutomata
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var wordsToCheck = CreateWordsToCheck();

            //Console.WriteLine("Checking words with DFAs");
            //var dfas = DFAFactory.DFAFactory.CreateDFAs();

            //foreach (var word in wordsToCheck)
            //{
            //    foreach (var dfa in dfas)
            //    {
            //        Console.WriteLine($"Checking word:\t[{word}] with DFA: [{dfa.InformalDefinition}]");

            //        var result = dfa.Execute(word);

            //        var acceptedRejected = result ? "accepted" : "rejected";
            //        Console.WriteLine($"Result is word:\t[{word}] has been [{acceptedRejected}]\n\n");
            //    }
            //}

            //Console.WriteLine("All words have been checked using DFAs.\nPress [Enter] to check with NFAs.");
            //Console.ReadLine();

            //Console.WriteLine("Checking words with NFAs");

            //var nfas = NFAFactory.NFAFactory.CreateNFAs();
            //foreach (var word in wordsToCheck)
            //{
            //    foreach (var nfa in nfas)
            //    {
            //        Console.WriteLine(nfa.DeltaFunctionTableAsString());
            //        Console.WriteLine($"Checking word:\t[{word}] with NFA: [{nfa.InformalDefinition}]");

            //        var result = nfa.Execute(word);

            //        var acceptedRejected = result ? "accepted" : "rejected";
            //        Console.WriteLine($"Result is word:\t[{word}] has been [{acceptedRejected}]\n\n");
            //    }
            //}

            //Console.WriteLine("All words have been checked using NFAs.\nPress [Enter] exit.");
            //Console.ReadLine();

            Console.WriteLine("Converting NFAs to DFAs");
            var nfaToDfaWords = NFAToDFAWords();
            var nfaToConvert = NFAToDFAFactory.CreateNFAs();
            foreach (var nfa in nfaToConvert)
            {
                foreach (var word in nfaToDfaWords)
                {
                    Console.WriteLine($"Checking word:\t[{word}] with NFA: [{nfa.InformalDefinition}]");
                    var result = nfa.Execute(word);
                    var acceptedRejected = result ? "accepted" : "rejected";
                    Console.WriteLine($"Before Conversion Result of NFA on word: [{word}] is [{result}]");

                    //Console.WriteLine($"NFA:\n{nfa.DeltaFunctionTableAsString()}\n");
                    var dfa = nfa.ConvertToDFA();
                    //Console.WriteLine($"Converted DFA:\n{dfa.DeltaFunctionTableAsString()}\n");

                    Console.WriteLine($"Checking word:\t[{word}] with DFA: [{dfa.InformalDefinition}]");
                    result = dfa.Execute(word);
                    acceptedRejected = result ? "accepted" : "rejected";
                    Console.WriteLine($"After Conversion Result of DFA on word: [{word}] is [{result}]\n\n");
                }
            }

            Console.WriteLine("All words have been checked using NFAs and the converted DFAs.\nPress [Enter] exit.");
            Console.ReadLine();
        }

        public static List<string> CreateWordsToCheck()
        {
            return new List<string>
            {
                "101010101010",
                string.Empty,
                "00000110",
                "00000000",
                "0000010",
                "0001100"
            };
        }

        public static List<string> NFAToDFAWords()
        {
            return new List<string>
            {
                //"aaaaaaaaab",
                //"aaaaaaaaaa",
                //"bbbbbbbbbb",
                //"ab",
                "abbbbbbb",
            };
        }
    }
}
