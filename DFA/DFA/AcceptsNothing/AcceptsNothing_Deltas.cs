using System;
using System.Collections.Generic;

namespace DFA.AcceptsNothing
{
    public static class AcceptsNothing_Deltas
    {
        public static Dictionary<int, Func<char, int>> ReturnAsDeltaFunction()
        {
            var deltaFunction = new Dictionary<int, Func<char, int>>();
            deltaFunction.Add(key: 1, value: StateOne);
            deltaFunction.Add(key: 2, value: StateTwo);

            return deltaFunction;
        }

        public static int StateOne(char input)
        {
            return 2;
        }

        public static int StateTwo(char input)
        {
            return 2;
        }
    }
}
