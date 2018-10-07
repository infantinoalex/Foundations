using System;
using System.Collections.Generic;

namespace NfaToDfaCompiler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var nfas = NFAFactory.CreateNFAs();
            var wordsToCheck = CreateWordsToCheck();

            foreach (var word in wordsToCheck)
            {
                foreach (var nfa in nfas)
                {
                    Console.WriteLine($"Checking word:\t[{word}] with DFA: [{nfa.InformalDefinition}]");

                    var result = nfa.Execute(word);

                     var acceptedRejected = result ? "accepted" : "rejected";
                    Console.WriteLine($"Result is word:\t[{word}] has been [{acceptedRejected}]\n\n");
                }
            }

            Console.WriteLine("All words have been checked.\nPress [Enter] exit.");
            Console.ReadLine();
        }

        public static List<string> CreateWordsToCheck()
        {
            var words = new List<string>();

            //words.Add("101010101010");
            //words.Add(string.Empty);
            //words.Add("00000110");
            words.Add("000000000");
            words.Add("00000100");
            words.Add("000001001");

            return words;
        }
    }
}
