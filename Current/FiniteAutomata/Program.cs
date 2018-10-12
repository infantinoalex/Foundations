using System;
using System.Collections.Generic;
using FiniteAutomata.Constants;
using FiniteAutomata.DFAFactory;
using FiniteAutomata.NFAFactory;

namespace FiniteAutomata
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var wordsToCheck = CreateWordsToCheck();

            Console.WriteLine("Checking words with DFAs");
            var dfas = DFAFactory.DFAFactory.CreateDFAs();

            foreach (var word in wordsToCheck)
            {
                foreach (var dfa in dfas)
                {
                    Console.WriteLine($"Checking word:\t[{word}] with DFA: [{dfa.InformalDefinition}]");

                    var result = dfa.Execute(word);

                    var acceptedRejected = result ? "accepted" : "rejected";
                    Console.WriteLine($"Result is word:\t[{word}] has been [{acceptedRejected}]\n\n");
                }
            }

            Console.WriteLine("All words have been checked using DFAs.\nPress [Enter] to check with NFAs.");
            Console.ReadLine();

            Console.WriteLine("Checking words with NFAs");

            var nfas = NFAFactory.NFAFactory.CreateNFAs();
            foreach (var word in wordsToCheck)
            {
                foreach (var nfa in nfas)
                {
                    //Console.WriteLine(nfa.DeltaFunctionTableAsString());
                    Console.WriteLine($"Checking word:\t[{word}] with NFA: [{nfa.InformalDefinition}]");

                    var result = nfa.Execute(word);

                    var acceptedRejected = result ? "accepted" : "rejected";
                    Console.WriteLine($"Result is word:\t[{word}] has been [{acceptedRejected}]\n\n");
                }
            }

            Console.WriteLine("All words have been checked using NFAs.\nPress [Enter] to check the converter.");
            Console.ReadLine();

            Console.WriteLine("Converting NFAs to DFAs");
            var nfaToConvert = NFAToDFAFactory.CreateNFAs();
            foreach (var nfa in nfaToConvert)
            {
                var convertedDFA = nfa.ConvertToDFA();
                //Console.WriteLine($"NFA:\n{nfa.DeltaFunctionTableAsString()}\n");
                //Console.WriteLine($"Converted DFA:\n{convertedDFA.DeltaFunctionTableAsString()}\n");

                foreach (var word in wordsToCheck)
                {
                    var nfaResult = nfa.Execute(word);
                    var nfaAcceptedRejected = nfaResult ? "accepted" : "rejected";

                    var dfaResult = convertedDFA.Execute(word);
                    var dfaAcceptedRejected = dfaResult ? "accepted" : "rejected";

                    if (nfaResult != dfaResult)
                    {
                        Console.WriteLine($"Error converting NFA to DFA.\nTested word: [{word}] and got result [{nfaAcceptedRejected}] from NFA and got result [{dfaAcceptedRejected}] from DFA.");
                    }
                    else
                    {
                        Console.WriteLine($"Testing [{word}] received the same result of [{nfaAcceptedRejected}] from both NFA and DFA.");
                    }
                }
            }

            Console.WriteLine("All words have been checked using NFAs and the converted DFAs.\nPress [Enter] to check the Regex converter.");
            Console.ReadLine();

            Console.WriteLine("Converting Regex -> NFA");
            var regexes = CreatedRegexes();
            foreach (var regex in regexes)
            {
                Console.WriteLine($"Regex: {RegularExpression.RegexAsString(regex)}");
                var nfa = RegularExpression.ConvertToNFA(regex);
                //Console.WriteLine($"Regex as NFA: {nfa.DeltaFunctionTableAsString()}");

                foreach (var word in wordsToCheck)
                {
                    var nfaResult = nfa.Execute(word);
                    var nfaAcceptedRejected = nfaResult ? "accepted" : "rejected";
                    Console.WriteLine($"Result is word:\t[{word}] has been [{nfaAcceptedRejected}]\n\n");
                }
            }

            Console.WriteLine("All words have been checked using the Regex converted to NFA.\nPress [Enter] to exit.");
            Console.ReadLine();
        }

        public static List<string> CreateWordsToCheck()
        {
            return new List<string>
            {
                "caaabbbb",
                "cab",
                "c",
                string.Empty,
                "101010101010",
                "1",
                string.Empty,
                "00000110",
                "00000000",
                "0000010",
                "0001100",
                "111111110",
                "01111111",
            };
        }

        public static List<List<object>> CreatedRegexes()
        {
            var regex = new List<List<object>>();

            regex.Add(new List<object>
            {
                'c', OperatorsEnum.CONCAT, OperatorsEnum.OPENPARENS, OperatorsEnum.OPENPARENS, 'a', OperatorsEnum.UNION, 'b', OperatorsEnum.CLOSEPARENS, OperatorsEnum.KLEENE, OperatorsEnum.CLOSEPARENS
            });

            regex.Add(new List<object>
            {
                '1', OperatorsEnum.CONCAT, OperatorsEnum.OPENPARENS, '1', OperatorsEnum.CLOSEPARENS, OperatorsEnum.KLEENE
            });

            return regex;
        }
    }
}
