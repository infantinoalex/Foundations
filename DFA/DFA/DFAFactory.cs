using DFA.ContainsAtLeastOne_One;
using DFA.EventLengthString;
using DFA.ThirdToLastLetterIsOne;
using System.Collections.Generic;

namespace DFA
{
    public static class DFAFactory
    {
        public static List<DFA> CreateDFAs()
        {
            var dfas = new List<DFA>();

            dfas.Add(ContainsAtLeastOne_One());
            dfas.Add(IsEvenLengthString());
            dfas.Add(ThirdToLastLetterIsOne());

            return dfas;
        }

        public static DFA ContainsAtLeastOne_One()
        {
            var states = new List<int>
            {
                0,
                1
            };

            var alphabet = new List<char>
            {
                '0',
                '1'
            };

            const int startingState = 0;
            var acceptingStates = new List<int>
            {
                1
            };

            var deltaFunction = ContainsAtLeastOne_One_Deltas.ReturnAsDeltaFunction();

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
            var states = new List<int>
            {
                0,
                1
            };

            var alphabet = new List<char>
            {
                '0',
                '1'
            };

            const int startingState = 0;
            var acceptingStates = new List<int>
            {
                0
            };

            var deltaFunction = EvenLengthString_Deltas.ReturnAsDeltaFunction();


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
            var states = new List<int>
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8
            };

            var alphabet = new List<char>
            {
                '0',
                '1'
            };

            const int startingState = 1;
            var acceptingStates = new List<int>
            {
                4,
                5,
                7,
                8
            };

            var deltaFunction = ThirdToLastLetterIsOne_Deltas.ReturnAsDeltaFunction();

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
