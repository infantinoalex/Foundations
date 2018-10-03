using NfaToDfaCompiler.IsEvenLengthOrOddNumber;
using System.Collections.Generic;

namespace NfaToDfaCompiler
{
    public static class NFAFactory
    {
        public static List<NFA> CreateNFAs()
        {
            var nfas = new List<NFA>();

            nfas.Add(IsEvenLengthOrOddNumber());

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
                2,
                5
            };

            var deltaFunction = IsEvenLengthOrOddNumber_Deltas.ReturnAsDeltaFunction();
            var epsilonFunctions = IsEvenLengthOrOddNumber_Deltas.ReturnAsEpsilonFunctions();

            return new NFA(
                states: states,
                alphabet: alphabet,
                startingState: startingState,
                deltaFunction: deltaFunction,
                epsilonTransitions: epsilonFunctions,
                acceptingStates: acceptingStates,
                informalDefinition: "Is even length or odd number");
        }
    }
}
