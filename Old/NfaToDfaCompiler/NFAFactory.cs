using NfaToDfaCompiler.ContainsSubstring_0101;
using NfaToDfaCompiler.IsEvenLengthOrOddNumber;
using System.Collections.Generic;

namespace NfaToDfaCompiler
{
    public static class NFAFactory
    {
        public static List<NFA> CreateNFAs()
        {
            var nfas = new List<NFA>();

            //nfas.Add(IsEvenLengthOrOddNumber());
            //nfas.Add(ContainsSubstring0101());
            nfas.Add(ContainsEven0orExactlyTwo1());

            return nfas;
        }

        public static NFA IsEvenLengthOrOddNumber()
        {
            var states = new List<int>
            {
                1,
                2,
                3,
                4,
                5
            };

            var alphabet = new List<char>
            {
                '0',
                '1'
            };

            const int startingState = 1;
            var acceptingStates = new List<int>
            {
                1,
                2,
                5
            };

            var deltaFunction = IsEvenLengthOrOddNumber_Deltas.ReturnAsDeltaFunction();

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
            var states = new List<int>
            {
                1,
                2,
                3,
                4,
                5
            };

            var alphabet = new List<char>
            {
                '0',
                '1'
            };

            const int startingState = 1;
            var acceptingStates = new List<int>
            {
                5
            };

            var deltaFunction = ContainsSubstring_0101_Deltas.ReturnAsDeltaFunction();

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
            var states = new List<int>
            {
                1,
                2,
                3,
                4,
                5,
                6
            };

            var alphabet = new List<char>
            {
                '0',
                '1'
            };

            const int startingState = 1;
            var acceptingStates = new List<int>
            {
                5,
                2
            };

            var deltaFunction = ContainsSubstring_0101_Deltas.ReturnAsDeltaFunction();

            return new NFA(
                states: states,
                alphabet: alphabet,
                startingState: startingState,
                deltaFunction: deltaFunction,
                acceptingStates: acceptingStates,
                informalDefinition: "Contains even # of 0s or exactly two 1's");
        }
    }
}
