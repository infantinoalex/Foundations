﻿using System;
using System.Collections.Generic;

namespace FiniteAutomata.NFAFactory
{
    public static class NFAToDFAFactory
    {
        public static List<NFA> CreateNFAs()
        {
            var nfas = new List<NFA>
            {
                TestNFA(),
                TestNFAWithEpsilon(),
            };

            return nfas;
        }

        public static NFA TestNFA()
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
                { new Tuple<string, char>("q0", '0'), new List<string> { "q0", "q1" } },
                { new Tuple<string, char>("q0", '1'), new List<string> { "q0" } },
                { new Tuple<string, char>("q1", '1'), new List<string> { "q2" } },
            };

            return new NFA(
                states: states,
                alphabet: alphabet,
                startingState: startingState,
                deltaFunction: deltaFunction,
                acceptingStates: acceptingStates,
                informalDefinition: "Ends with 01");
        }

        public static NFA TestNFAWithEpsilon()
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
