using System;
using System.Collections.Generic;

namespace NfaToDfaCompiler.IsEvenLengthOrOddNumber
{
    public static class IsEvenLengthOrOddNumber_Deltas
    {
        public static Dictionary<int, Func<char, List<int>>> ReturnAsDeltaFunction()
        {
            var deltaFunction = new Dictionary<int, Func<char, List<int>>>();
            deltaFunction.Add(key: 1, value: StateOne);
            deltaFunction.Add(key: 2, value: StateTwo);
            deltaFunction.Add(key: 3, value: StateThree);
            deltaFunction.Add(key: 4, value: StateFour);
            deltaFunction.Add(key: 5, value: StateFive);

            return deltaFunction;
        }

        public static List<int> StateOne(char input)
        {
            var states = new List<int>();
            states.AddRange(StateTwo(input));
            states.AddRange(StateFour(input));

            return states;
        }

        public static List<int> StateTwo(char input)
        {
            var newStates = new List<int>();
            if (input == '0')
            {
                newStates.Add(3);
            }
            else
            {
                newStates.Add(3);
            }

            return newStates;
        }

        public static List<int> StateThree(char input)
        {
            var newStates = new List<int>();
            if (input == '0')
            {
                newStates.Add(2);
            }
            else
            {
                newStates.Add(2);
            }

            return newStates;
        }

        public static List<int> StateFour(char input)
        {
            var newStates = new List<int>();
            if (input == '0')
            {
                newStates.Add(4);
            }
            else
            {
                newStates.Add(5);
            }

            return newStates;
        }

        public static List<int> StateFive(char input)
        {
            var newStates = new List<int>();
            if (input == '0')
            {
                newStates.Add(4);
            }
            else
            {
                newStates.Add(5);
            }

            return newStates;
        }
    }
}
