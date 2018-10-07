using System;
using System.Collections.Generic;

namespace NfaToDfaCompiler.ContainsSubstring_0101
{
    public static class ContainsSubstring_0101_Deltas
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

            if (input == '1')
            {
                states.Add(1);
            }
            else if (input == '0')
            {
                states.Add(2);
                states.Add(1);
            }

            return states;
        }

        public static List<int> StateTwo(char input)
        {
            var states = new List<int>();

            if (input == '1')
            {
                states.Add(3);
            }
            else if (input == '0')
            {
                states.Add(1);
            }

            return states;
        }

        public static List<int> StateThree(char input)
        {
            var states = new List<int>();

            if (input == '1')
            {
                states.Add(1);
            }
            else if (input == '0')
            {
                states.Add(4);
            }

            return states;
        }

        public static List<int> StateFour(char input)
        {
            var states = new List<int>();

            if (input == '1')
            {
                states.Add(5);
            }
            else if (input == '0')
            {
                states.Add(1);
            }

            return states;
        }

        public static List<int> StateFive(char input)
        {
            var states = new List<int>();

            if (input == '1')
            {
                states.Add(5);
            }
            else if (input == '0')
            {
                states.Add(5);
            }

            return states;
        }
    }
}
