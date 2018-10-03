using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static Dictionary<int, Func<List<int>>> ReturnAsEpsilonFunctions()
        {
            var epsilonFunctions = new Dictionary<int, Func<List<int>>>();
            epsilonFunctions.Add(key: 1, value: StateOneEpsilons);

            return epsilonFunctions;
        }

        public static List<int> StateOne(char input)
        {
            // Only contains epsilon transitions so returns no new states
            return new List<int>();
        }

        public static List<int> StateOneEpsilons()
        {
            // Contains two epsilon transitions so we can just make them one
            return new List<int>()
            {
                2,
                4
            };
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
