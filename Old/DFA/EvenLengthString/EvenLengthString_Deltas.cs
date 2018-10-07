using System;
using System.Collections.Generic;

namespace DFACompiler.EvenLengthString
{
    public static class EvenLengthString_Deltas
    {
        public static Dictionary<int, Func<char, int>> ReturnAsDeltaFunction()
        {
            var deltaFunction = new Dictionary<int, Func<char, int>>();
            deltaFunction.Add(key: 0, value: StateZero);
            deltaFunction.Add(key: 1, value: StateOne);

            return deltaFunction;
        }

        public static int StateZero(char input)
        {
            return 1;
        }

        public static int StateOne(char input)
        {
            return 0;
        }
    }
}
