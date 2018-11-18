using System;
using System.Collections.Generic;
using FiniteAutomata.Constants;

namespace FiniteAutomata
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Homework problems for DFA check
            //CheckProblem1_4_a();
            //CheckProblem1_5_c();

            // Homework problems for NFA check
            //CheckProblem1_7_a();
            //CheckProblem1_7_c();
            //CheckProblemInClass();

            // Homework problems for Regex check
            CheckProblem1_20_c();
            CheckProblem1_20_f();
        }

        public static void CheckProblem1_4_a()
        {
            Console.WriteLine("Checking problem 1.4.a results:\n");
            var dfa = DFAFactory.DFAFactory.Problem1_4_a_DFA();
            foreach (var word in DFA1_4_a_Words())
            {
                var result = dfa.Execute(word);
                var acceptedRejectedString = result ? "accepted" : "rejected";
                Console.WriteLine($"Result is word:\t[{word}] has been [{acceptedRejectedString}]\n");
            }

            Console.WriteLine("All words checked for 1.4.a. Press [Enter] to exit/continue.");
            Console.ReadLine();
        }

        public static void CheckProblem1_5_c()
        {
            Console.WriteLine("Checking problem 1.5.c results:\n");
            var dfa = DFAFactory.DFAFactory.Problem1_5_c_DFA();
            foreach (var word in DFA1_5_c_Words())
            {
                var result = dfa.Execute(word);
                var acceptedRejectedString = result ? "accepted" : "rejected";
                Console.WriteLine($"Result is word:\t[{word}] has been [{acceptedRejectedString}]\n");
            }

            Console.WriteLine("All words checked for 1.5.c. Press [Enter] to exit/continue.");
            Console.ReadLine();
        }

        public static void CheckProblem1_7_a()
        {
            Console.WriteLine("Checking problem 1.7.a results:\n");
            var nfa = NFAFactory.NFAFactory.Problem1_7_a_NFA();
            foreach (var word in NFA1_7_a_Words())
            {
                var result = nfa.Execute(word);
                var acceptedRejectedString = result ? "accepted" : "rejected";
                Console.WriteLine($"Result is word:\t[{word}] has been [{acceptedRejectedString}]\n");
            }

            Console.WriteLine("All words checked for 1.7.a. Press [Enter] to exit/continue.");
            Console.ReadLine();

            Console.WriteLine("Converting the NFA -> DFA");
            var dfa = nfa.ConvertToDFA();
            Console.WriteLine($"Resulting DFA:\n{dfa.DeltaFunctionTableAsString()}");
            Console.WriteLine("Checking the same words with the DFA now");
            foreach (var word in NFA1_7_a_Words())
            {
                var nfaResult = nfa.Execute(word);
                var nfaAcceptedRejected = nfaResult ? "accepted" : "rejected";

                var dfaResult = dfa.Execute(word);
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

            Console.WriteLine("All words checked for 1.7.a with the converted DFA. Press [Enter] to exit/continue.");
            Console.ReadLine();
        }

        public static void CheckProblem1_7_c()
        {
            Console.WriteLine("Checking problem 1.7.c results:\n");
            var nfa = NFAFactory.NFAFactory.Problem1_7_c_NFA();
            foreach (var word in NFA1_7_c_Words())
            {
                var result = nfa.Execute(word);
                var acceptedRejectedString = result ? "accepted" : "rejected";
                Console.WriteLine($"Result is word:\t[{word}] has been [{acceptedRejectedString}]\n");
            }

            Console.WriteLine("All words checked for 1.7.c. Press [Enter] to exit/continue.");
            Console.ReadLine();

            Console.WriteLine("Converting the NFA -> DFA");
            var dfa = nfa.ConvertToDFA();
            Console.WriteLine($"Resulting DFA:\n{dfa.DeltaFunctionTableAsString()}");
            Console.WriteLine("Checking the same words with the DFA now");
            foreach (var word in NFA1_7_c_Words())
            {
                var nfaResult = nfa.Execute(word);
                var nfaAcceptedRejected = nfaResult ? "accepted" : "rejected";

                var dfaResult = dfa.Execute(word);
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

            Console.WriteLine("All words checked for 1.7.c with the converted DFA. Press [Enter] to exit/continue.");
            Console.ReadLine();
        }

        public static void CheckProblemInClass()
        {
            Console.WriteLine("Checking problem in class results:\n");
            var nfa = NFAFactory.NFAFactory.Problem_In_Class_NFA();

            Console.WriteLine("Converting the NFA -> DFA");
            var dfa = nfa.ConvertToDFA();

            Console.WriteLine($"Resulting DFA:\n{dfa.DeltaFunctionTableAsString()}");
            Console.WriteLine("In class problem has been checked.\nPress [Enter] to exit.");
            Console.ReadLine();
        }

        public static void CheckProblem1_20_c()
        {
            Console.WriteLine("Converting Regex -> NFA");
            var regex = Regex1_20_c_Regex();

            Console.WriteLine($"Regex: {RegularExpression.RegexAsString(regex)}");
            var nfa = RegularExpression.ConvertToNFA(regex);
            var wordsToCheck = Regex1_20_c_Words();

            foreach (var word in wordsToCheck)
            {
                var nfaResult = nfa.Execute(word);
                var nfaAcceptedRejected = nfaResult ? "accepted" : "rejected";
                Console.WriteLine($"Result is word:\t[{word}] has been [{nfaAcceptedRejected}]\n\n");
            }

            Console.WriteLine("All words have been checked using the Regex converted to NFA.\nPress [Enter] to exit.");
            Console.ReadLine();
        }

        public static void CheckProblem1_20_f()
        {
            Console.WriteLine("Converting Regex -> NFA");
            var regex = Regex1_20_f_Regex();

            Console.WriteLine($"Regex: {RegularExpression.RegexAsString(regex)}");
            var nfa = RegularExpression.ConvertToNFA(regex);
            var wordsToCheck = Regex1_20_f_Words();

            foreach (var word in wordsToCheck)
            {
                var nfaResult = nfa.Execute(word);
                var nfaAcceptedRejected = nfaResult ? "accepted" : "rejected";
                Console.WriteLine($"Result is word:\t[{word}] has been [{nfaAcceptedRejected}]\n\n");
            }

            Console.WriteLine("All words have been checked using the Regex converted to NFA.\nPress [Enter] to exit.");
            Console.ReadLine();
        }

        public static List<string> DFA1_4_a_Words()
        {
            return new List<string>
            {
                "aab",
                "abab",
                "aabb",
                "aabba",
                "aaaaab",
                "abbabba"
            };
        }

        public static List<string> DFA1_5_c_Words()
        {
            return new List<string>
            {
                "aaa",
                "bbb",
                "aab",
                "aaaaaaaaaabbbbb",
            };
        }

        public static List<string> NFA1_7_a_Words()
        {
            return new List<string>
            {
                "001",
                "111110",
                "000000",
                "11101100",
                "1111000110101010100"
            };
        }

        public static List<string> NFA1_7_c_Words()
        {
            return new List<string>
            {
                "00011",
                "000",
                "11100",
                "111110",
                "0000100"
            };
        }

        public static List<object> Regex1_20_c_Regex()
        {
            // Contains substring any number of b's or a's
            // ((a*) U (b*))
            return new List<object>
            {
                OperatorsEnum.OPENPARENS, OperatorsEnum.OPENPARENS, 'a', OperatorsEnum.KLEENE, OperatorsEnum.CLOSEPARENS, OperatorsEnum.UNION, OperatorsEnum.OPENPARENS, 'b', OperatorsEnum.KLEENE, OperatorsEnum.CLOSEPARENS, OperatorsEnum.CLOSEPARENS
            };
        }

        public static List<string> Regex1_20_c_Words()
        {
            return new List<string>
            {
                "aaabbbb",
                "bbbbbbb",
                "aaaaaaa"
            };
        }

        public static List<object> Regex1_20_f_Regex()
        {
            // Contains substring aba or bab
            // aba U bab
            // ((a o b o a) U (b o a o b))
            return new List<object>
            {
                OperatorsEnum.OPENPARENS, OperatorsEnum.OPENPARENS, 'a', OperatorsEnum.CONCAT, 'b', OperatorsEnum.CONCAT, 'a', OperatorsEnum.CLOSEPARENS, OperatorsEnum.UNION, OperatorsEnum.OPENPARENS, 'b', OperatorsEnum.CONCAT, 'a', OperatorsEnum.CONCAT, 'b', OperatorsEnum.CLOSEPARENS, OperatorsEnum.CLOSEPARENS
            };
        }

        public static List<string> Regex1_20_f_Words()
        {
            return new List<string>
            {
                "aaaaaaba",
                "bbbbbbab",
                "bab",
                "aba"
            };
        }

        //public static List<List<object>> CreatedRegexes()
        //{
        //    var regex = new List<List<object>>();

        //    // Contains substring any number of b's or a's
        //    // ((a*) U (b*))
        //    regex.Add(new List<object>
        //    {
        //        OperatorsEnum.OPENPARENS, OperatorsEnum.OPENPARENS, 'a', OperatorsEnum.KLEENE, OperatorsEnum.CLOSEPARENS, OperatorsEnum.UNION, OperatorsEnum.OPENPARENS, 'b', OperatorsEnum.KLEENE, OperatorsEnum.CLOSEPARENS, OperatorsEnum.CLOSEPARENS
        //    });

        //    regex.Add(new List<object>
        //    {
        //        'c', OperatorsEnum.CONCAT, OperatorsEnum.OPENPARENS, OperatorsEnum.OPENPARENS, 'a', OperatorsEnum.UNION, 'b', OperatorsEnum.CLOSEPARENS, OperatorsEnum.KLEENE, OperatorsEnum.CLOSEPARENS
        //    });

        //    regex.Add(new List<object>
        //    {
        //        '1', OperatorsEnum.CONCAT, OperatorsEnum.OPENPARENS, '1', OperatorsEnum.CLOSEPARENS, OperatorsEnum.KLEENE
        //    });

        //    return regex;
        //}
    }
}
