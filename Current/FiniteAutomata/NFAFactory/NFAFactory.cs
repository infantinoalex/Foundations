﻿using System;
using System.Collections.Generic;

namespace FiniteAutomata.NFAFactory
{
    public static class NFAFactory
    {
        public static List<NFA> CreateNFAs()
        {
            var nfas = new List<NFA>
            {
                IsEvenLengthOrOddNumber(),
                ContainsSubstring0101(),
                ContainsEven0orExactlyTwo1(),
                ContainsAtLeastTwo0s(),
            };

            return nfas;
        }

        public static NFA Problem1_7_a_NFA()
        {
            var states = new List<string>
            {
                "q1", "q2", "q3"
            };

            var alphabet = new List<char>
            {
                '1', '0'
            };

            const string startingState = "q1";
            var acceptingStates = new List<string>
            {
                "q3",
            };

            var deltaFunction = new Dictionary<Tuple<string, char>, List<string>>
            {
                { new Tuple<string, char>("q1", '1'), new List<string> { "q1" } },
                { new Tuple<string, char>("q1", '0'), new List<string> { "q1", "q2" } },
                { new Tuple<string, char>("q2", '1'), new List<string> { "q1" } },
                { new Tuple<string, char>("q2", '0'), new List<string> { "q3" } },
                { new Tuple<string, char>("q3", '1'), new List<string> { "q1" } },
                { new Tuple<string, char>("q3", '0'), new List<string> { } },
            };

            return new NFA(
                states: states,
                alphabet: alphabet,
                startingState: startingState,
                deltaFunction: deltaFunction,
                acceptingStates: acceptingStates,
                informalDefinition: "Contains neither the substring ab or ba");
        }

        public static NFA Problem1_7_c_NFA()
        {
            var states = new List<string>
            {
                "q1", "q2", "q3", "q4", "q5", "q6"
            };

            var alphabet = new List<char>
            {
                '1', '0'
            };

            const string startingState = "q1";
            var acceptingStates = new List<string>
            {
                "q1", "q2", "q6"
            };

            var deltaFunction = new Dictionary<Tuple<string, char>, List<string>>
            {
                { new Tuple<string, char>("q1", '0'), new List<string> { } },
                { new Tuple<string, char>("q1", '1'), new List<string> { } },
                { new Tuple<string, char>("q1", '\0'), new List<string> { "q2", "q4" } },
                { new Tuple<string, char>("q2", '0'), new List<string> { "q3" } },
                { new Tuple<string, char>("q2", '1'), new List<string> { "q2" } },
                { new Tuple<string, char>("q3", '0'), new List<string> { "q2"} },
                { new Tuple<string, char>("q3", '1'), new List<string> { "q3" } },
                { new Tuple<string, char>("q4", '0'), new List<string> { "q4" } },
                { new Tuple<string, char>("q4", '1'), new List<string> { "q5" } },
                { new Tuple<string, char>("q5", '0'), new List<string> { "q5" } },
                { new Tuple<string, char>("q5", '1'), new List<string> { "q6" } },
                { new Tuple<string, char>("q6", '0'), new List<string> { "q6" } },
                { new Tuple<string, char>("q6", '1'), new List<string> { } },
            };

            return new NFA(
                states: states,
                alphabet: alphabet,
                startingState: startingState,
                deltaFunction: deltaFunction,
                acceptingStates: acceptingStates,
                informalDefinition: "Contains an even number of 0's or exactly two 1's");
        }

        public static NFA Problem_In_Class_NFA()
        {
            var states = new List<string>
            {
                "a", "b", "c"
            };

            var alphabet = new List<char>
            {
                '1', '0'
            };

            const string startingState = "a";
            var acceptingStates = new List<string>
            {
                "c",
            };

            var deltaFunction = new Dictionary<Tuple<string, char>, List<string>>
            {
                { new Tuple<string, char>("a", '0'), new List<string> { "a", "b", "c" } },
                { new Tuple<string, char>("a", '1'), new List<string> { "b", "c" } },
                { new Tuple<string, char>("b", '0'), new List<string> { "a" } },
                { new Tuple<string, char>("b", '1'), new List<string> { "b" } },
                { new Tuple<string, char>("c", '0'), new List<string> { "c" } },
                { new Tuple<string, char>("c", '1'), new List<string> { } },
            };

            return new NFA(
                states: states,
                alphabet: alphabet,
                startingState: startingState,
                deltaFunction: deltaFunction,
                acceptingStates: acceptingStates,
                informalDefinition: "In Class Check");
        }

        public static NFA IsEvenLengthOrOddNumber()
        {
            var states = new List<string>
            {
                "q1",
                "q2",
                "q3",
                "q4",
                "q5"
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
                "q2",
                "q5"
            };

            var deltaFunction = new Dictionary<Tuple<string, char>, List<string>>
            {
                { new Tuple<string, char>("q1", '0'), new List<string> { "q1" } },
                { new Tuple<string, char>("q1", '1'), new List<string> { "q1" } },
                { new Tuple<string, char>("q1", '\0'), new List<string> { "q2" , "q4" } },
                { new Tuple<string, char>("q2", '0'), new List<string> { "q3" } },
                { new Tuple<string, char>("q2", '1'), new List<string> { "q3" } },
                { new Tuple<string, char>("q3", '0'), new List<string> { "q2" } },
                { new Tuple<string, char>("q3", '1'), new List<string> { "q2" } },
                { new Tuple<string, char>("q4", '0'), new List<string> { "q4" } },
                { new Tuple<string, char>("q4", '1'), new List<string> { "q5" } },
                { new Tuple<string, char>("q5", '0'), new List<string> { "q4" } },
                { new Tuple<string, char>("q5", '1'), new List<string> { "q5" } },
            };

            return new NFA(
                states: states,
                alphabet: alphabet,
                startingState: startingState,
                deltaFunction: deltaFunction,
                acceptingStates: acceptingStates,
                informalDefinition: "Is even length or odd number");
        }

        public static NFA ContainsSubstring0101()
        {
            var states = new List<string>
            {
                "q1",
                "q2",
                "q3",
                "q4",
                "q5"
            };

            var alphabet = new List<char>
            {
                '0',
                '1'
            };

            const string startingState = "q1";
            var acceptingStates = new List<string>
            {
                "q5"
            };

            var deltaFunction = new Dictionary<Tuple<string, char>, List<string>>
            {
                { new Tuple<string, char>("q1", '0'), new List<string> { "q1", "q2" } },
                { new Tuple<string, char>("q1", '1'), new List<string> { "q1" } },
                { new Tuple<string, char>("q2", '0'), new List<string> { "q1" } },
                { new Tuple<string, char>("q2", '1'), new List<string> { "q3" } },
                { new Tuple<string, char>("q3", '0'), new List<string> { "q4" } },
                { new Tuple<string, char>("q3", '1'), new List<string> { "q1" } },
                { new Tuple<string, char>("q4", '0'), new List<string> { "q1" } },
                { new Tuple<string, char>("q4", '1'), new List<string> { "q5" } },
                { new Tuple<string, char>("q5", '0'), new List<string> { "q5" } },
                { new Tuple<string, char>("q5", '1'), new List<string> { "q5" } },
            };

            return new NFA(
                states: states,
                alphabet: alphabet,
                startingState: startingState,
                deltaFunction: deltaFunction,
                acceptingStates: acceptingStates,
                informalDefinition: "Contains substring 0101");
        }

        public static NFA ContainsEven0orExactlyTwo1()
        {
            var states = new List<string>
            {
                "q1",
                "q2",
                "q3",
                "q4",
                "q5",
                "q6"
            };

            var alphabet = new List<char>
            {
                '0',
                '1'
            };

            const string startingState = "q1";
            var acceptingStates = new List<string>
            {
                "q5",
                "q2"
            };

            var deltaFunction = new Dictionary<Tuple<string, char>, List<string>>
            {
                { new Tuple<string, char>("q1", '0'), new List<string> { "q1" } },
                { new Tuple<string, char>("q1", '1'), new List<string> { "q4" } },
                { new Tuple<string, char>("q1", '\0'), new List<string> { "q2" } },
                { new Tuple<string, char>("q2", '0'), new List<string> { "q2", "q3" } },
                { new Tuple<string, char>("q2", '1'), new List<string> { "q2" } },
                { new Tuple<string, char>("q3", '0'), new List<string> { "q2" } },
                { new Tuple<string, char>("q3", '1'), new List<string> { "q3" } },
                { new Tuple<string, char>("q4", '0'), new List<string> { "q4" } },
                { new Tuple<string, char>("q4", '1'), new List<string> { "q5" } },
                { new Tuple<string, char>("q5", '0'), new List<string> { "q5" } },
                { new Tuple<string, char>("q5", '1'), new List<string> { "q6" } },
                { new Tuple<string, char>("q6", '0'), new List<string> { "q6" } },
                { new Tuple<string, char>("q6", '1'), new List<string> { "q6" } },
            };

            return new NFA(
                states: states,
                alphabet: alphabet,
                startingState: startingState,
                deltaFunction: deltaFunction,
                acceptingStates: acceptingStates,
                informalDefinition: "Contains even # of 0s or exactly two 1's");
        }

        public static NFA ContainsAtLeastTwo0s()
        {
            var states = new List<string>
            {
                "q0",
                "q1",
                "q2",
            };

            var alphabet = new List<char>
            {
                '0',
                '1'
            };

            const string startingState = "q0";
            var acceptingStates = new List<string>
            {
                "q2",
            };

            var deltaFunction = new Dictionary<Tuple<string, char>, List<string>>
            {
                { new Tuple<string, char>("q0", '0'), new List<string> { "q1", "q2" } },
                { new Tuple<string, char>("q0", '1'), new List<string> { "q0" } },
                { new Tuple<string, char>("q0", '\0'), new List<string> { "q1" } },
                { new Tuple<string, char>("q1", '0'), new List<string> { } },
                { new Tuple<string, char>("q1", '1'), new List<string> { "q1" } },
                { new Tuple<string, char>("q1", '\0'), new List<string> { "q2" } },
                { new Tuple<string, char>("q2", '0'), new List<string> { "q2" } },
                { new Tuple<string, char>("q2", '1'), new List<string> { "q2" } },
            };

            return new NFA(
                states: states,
                alphabet: alphabet,
                startingState: startingState,
                deltaFunction: deltaFunction,
                acceptingStates: acceptingStates,
                informalDefinition: "Contains Anything");
        }
    }
}