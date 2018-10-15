using System;
using System.Collections.Generic;

namespace FiniteAutomata.DFAFactory
{
    public static class DFAFactory
    {
        public static List<DFA> CreateDFAs()
        {
            var dfas = new List<DFA>
            {
                AcceptsEmptyString(),
                ContainsAtLeastOne_One(),
                IsEvenLengthString(),
                ThirdToLastLetterIsOne()
            };

            return dfas;
        }

        public static DFA Problem1_4_a_DFA()
        {
            var states = new List<string>
            {
                "q1", "q2", "q3", "q4", "q5", "q6", "q7", "q8", "q9", "q10", "q11", "q12"
            };

            var alphabet = new List<char>
            {
                'a', 'b'
            };

            const string startingState = "q1";
            var acceptingStates = new List<string>
            {
                "q12"
            };

            var deltaFunction = new Dictionary<Tuple<string, char>, string>
            {
                { new Tuple<string, char>("q1", 'a'), "q2" },
                { new Tuple<string, char>("q1", 'b'), "q5" },
                { new Tuple<string, char>("q2", 'a'), "q3" },
                { new Tuple<string, char>("q2", 'b'), "q6" },
                { new Tuple<string, char>("q3", 'a'), "q4" },
                { new Tuple<string, char>("q3", 'b'), "q7" },
                { new Tuple<string, char>("q4", 'a'), "q4" },
                { new Tuple<string, char>("q4", 'b'), "q8" },
                { new Tuple<string, char>("q5", 'a'), "q6" },
                { new Tuple<string, char>("q5", 'b'), "q9" },
                { new Tuple<string, char>("q6", 'a'), "q7" },
                { new Tuple<string, char>("q6", 'b'), "q10" },
                { new Tuple<string, char>("q7", 'a'), "q8" },
                { new Tuple<string, char>("q7", 'b'), "q11" },
                { new Tuple<string, char>("q8", 'a'), "q8" },
                { new Tuple<string, char>("q8", 'b'), "q12" },
                { new Tuple<string, char>("q9", 'a'), "q10" },
                { new Tuple<string, char>("q9", 'b'), "q9" },
                { new Tuple<string, char>("q10", 'a'), "q11" },
                { new Tuple<string, char>("q10", 'b'), "q10" },
                { new Tuple<string, char>("q11", 'a'), "q12" },
                { new Tuple<string, char>("q11", 'b'), "q11" },
                { new Tuple<string, char>("q12", 'a'), "q12" },
                { new Tuple<string, char>("q12", 'b'), "q12" },
            };

            return new DFA(
                states: states,
                alphabet: alphabet,
                startingState: startingState,
                deltaFunction: deltaFunction,
                acceptingStates: acceptingStates,
                informalDefinition: "Contains at least three a's and two b's");
        }

        public static DFA Problem1_5_c_DFA()
        {
            var states = new List<string>
            {
                "q1", "q2", "q3", "q4"
            };

            var alphabet = new List<char>
            {
                'a', 'b'
            };

            const string startingState = "q1";
            var acceptingStates = new List<string>
            {
                "q1", "q2", "q4"
            };

            var deltaFunction = new Dictionary<Tuple<string, char>, string>
            {
                { new Tuple<string, char>("q1", 'a'), "q2" },
                { new Tuple<string, char>("q1", 'b'), "q4" },
                { new Tuple<string, char>("q2", 'a'), "q2" },
                { new Tuple<string, char>("q2", 'b'), "q3" },
                { new Tuple<string, char>("q3", 'a'), "q3" },
                { new Tuple<string, char>("q3", 'b'), "q3" },
                { new Tuple<string, char>("q4", 'a'), "q3" },
                { new Tuple<string, char>("q4", 'b'), "q4" },
            };

            return new DFA(
                states: states,
                alphabet: alphabet,
                startingState: startingState,
                deltaFunction: deltaFunction,
                acceptingStates: acceptingStates,
                informalDefinition: "Contains neither the substring ab or ba");
        }

        public static DFA AcceptsEmptyString()
        {
            var states = new List<string>
            {
                "q1",
                "q2"
            };

            var alphabet = new List<char>
            {
                '0',
                '1'
            };

            const string startingState = "q1";
            var acceptingStates = new List<string>
            {
                "q1",
            };

            var deltaFunction = new Dictionary<Tuple<string, char>, string>
            {
                { new Tuple<string, char>("q1", '0'), "q2" },
                { new Tuple<string, char>("q1", '1'), "q2" },
                { new Tuple<string, char>("q2", '0'), "q2" },
                { new Tuple<string, char>("q2", '1'), "q2" }
            };

            return new DFA(
                states: states,
                alphabet: alphabet,
                startingState: startingState,
                deltaFunction: deltaFunction,
                acceptingStates: acceptingStates,
                informalDefinition: "Accepts empty string");
        }

        public static DFA ContainsAtLeastOne_One()
        {
            var states = new List<string>
            {
                "q1",
                "q2"
            };

            var alphabet = new List<char>
            {
                '0',
                '1'
            };

            const string startingState = "q1";
            var acceptingStates = new List<string>
            {
                "q2"
            };

            var deltaFunction = new Dictionary<Tuple<string, char>, string>
            {
                { new Tuple<string, char>("q1", '0'), "q1" },
                { new Tuple<string, char>("q1", '1'), "q2" },
                { new Tuple<string, char>("q2", '0'), "q2" },
                { new Tuple<string, char>("q2", '1'), "q2" }
            };

            return new DFA(
                states: states,
                alphabet: alphabet,
                startingState: startingState,
                deltaFunction: deltaFunction,
                acceptingStates: acceptingStates,
                informalDefinition: "Contains at least One 1");
        }

        public static DFA IsEvenLengthString()
        {
            var states = new List<string>
            {
                "q1",
                "q2"
            };

            var alphabet = new List<char>
            {
                '0',
                '1'
            };

            const string startingState = "q1";
            var acceptingStates = new List<string>
            {
                "q1"
            };

            var deltaFunction = new Dictionary<Tuple<string, char>, string>
            {
                { new Tuple<string, char>("q1", '0'), "q2" },
                { new Tuple<string, char>("q1", '1'), "q2" },
                { new Tuple<string, char>("q2", '0'), "q1" },
                { new Tuple<string, char>("q2", '1'), "q1" }
            };


            return new DFA(
                states: states,
                alphabet: alphabet,
                startingState: startingState,
                deltaFunction: deltaFunction,
                acceptingStates: acceptingStates,
                informalDefinition: "Is Even Length String");
        }

        public static DFA ThirdToLastLetterIsOne()
        {
            var states = new List<string>
            {
                "q1",
                "q2",
                "q3",
                "q4",
                "q5",
                "q6",
                "q7",
                "q8"
            };

            var alphabet = new List<char>
            {
                '0',
                '1'
            };

            const string startingState = "q1";
            var acceptingStates = new List<string>
            {
                "q4",
                "q5",
                "q7",
                "q8"
            };

            var deltaFunction = new Dictionary<Tuple<string, char>, string>
            {
                { new Tuple<string, char>("q1", '0'), "q1" },
                { new Tuple<string, char>("q1", '1'), "q2" },
                { new Tuple<string, char>("q2", '0'), "q6" },
                { new Tuple<string, char>("q2", '1'), "q3" },
                { new Tuple<string, char>("q3", '0'), "q7" },
                { new Tuple<string, char>("q3", '1'), "q4" },
                { new Tuple<string, char>("q4", '0'), "q7" },
                { new Tuple<string, char>("q4", '1'), "q4" },
                { new Tuple<string, char>("q5", '0'), "q1" },
                { new Tuple<string, char>("q5", '1'), "q2" },
                { new Tuple<string, char>("q6", '0'), "q5" },
                { new Tuple<string, char>("q6", '1'), "q8" },
                { new Tuple<string, char>("q7", '0'), "q5" },
                { new Tuple<string, char>("q7", '1'), "q8" },
                { new Tuple<string, char>("q8", '0'), "q6" },
                { new Tuple<string, char>("q8", '1'), "q3" }

            };

            return new DFA(
                states: states,
                alphabet: alphabet,
                startingState: startingState,
                deltaFunction: deltaFunction,
                acceptingStates: acceptingStates,
                informalDefinition: "Third to Last Letter is a One");
        }
    }
}
