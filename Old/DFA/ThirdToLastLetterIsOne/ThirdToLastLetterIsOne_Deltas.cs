using System;
using System.Collections.Generic;

namespace DFACompiler.ThirdToLastLetterIsOne
{
    public static class ThirdToLastLetterIsOne_Deltas
    {
        public static Dictionary<int, Func<char, int>> ReturnAsDeltaFunction()
        {
            var deltaFunction = new Dictionary<int, Func<char, int>>();
            deltaFunction.Add(key: 1, value: StateOne);
            deltaFunction.Add(key: 2, value: StateTwo);
            deltaFunction.Add(key: 3, value: StateThree);
            deltaFunction.Add(key: 4, value: StateFour);
            deltaFunction.Add(key: 5, value: StateFive);
            deltaFunction.Add(key: 6, value: StateSix);
            deltaFunction.Add(key: 7, value: StateSeven);
            deltaFunction.Add(key: 8, value: StateEight);

            return deltaFunction;
        }

        public static int StateOne(char input)
        {
            if (input == '0')
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        public static int StateTwo(char input)
        {
            if (input == '0')
            {
                return 6;
            }
            else
            {
                return 3;
            }
        }

        public static int StateThree(char input)
        {
            if (input == '0')
            {
                return 7;
            }
            else
            {
                return 4;
            }
        }

        public static int StateFour(char input)
        {
            if (input == '0')
            {
                return 7;
            }
            else
            {
                return 4;
            }
        }

        public static int StateFive(char input)
        {
            if (input == '0')
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        public static int StateSix(char input)
        {
            if (input == '0')
            {
                return 5;
            }
            else
            {
                return 8;
            }
        }

        public static int StateSeven(char input)
        {
            if (input == '0')
            {
                return 5;
            }
            else
            {
                return 8;
            }
        }

        public static int StateEight(char input)
        {
            if (input == '0')
            {
                return 6;
            }
            else
            {
                return 3;
            }
        }
    }
}
