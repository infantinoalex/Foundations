using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NfaToDfaCompiler.ContainsEven0orExactlyTwo1
{
    public static class ContainsEven0OrExactlyTwo1
    {
        public static Dictionary<int, Func<char, List<int>>> ReturnAsDeltaFunction()
        {
            var deltaFunction = new Dictionary<int, Func<char, List<int>>>();
            deltaFunction.Add(key: 1, value: StateOne);
            deltaFunction.Add(key: 2, value: StateTwo);
            deltaFunction.Add(key: 3, value: StateThree);
            deltaFunction.Add(key: 4, value: StateFour);
            deltaFunction.Add(key: 5, value: StateFive);
            deltaFunction.Add(key: 6, value: StateSix);

            return deltaFunction;
        }

        public static List<int> StateOne(char input)
        {
            var states = new List<int>();

            if (input == '1')
            {
                states.Add(4);
            }
            else if (input == '0')
            {
                states.Add(1);
            }

            states.AddRange(StateTwo(input));

            return states;
        }

        public static List<int> StateTwo(char input)
        {
            var states = new List<int>();

            if (input == '1')
            {
                states.Add(2);
            }
            else if (input == '0')
            {
                states.Add(2);
                states.Add(3);
            }

            return states;
        }

        public static List<int> StateThree(char input)
        {
            var states = new List<int>();

            if (input == '1')
            {
                states.Add(3);
            }
            else if (input == '0')
            {
                states.Add(2);
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
                states.Add(4);
            }

            return states;
        }

        public static List<int> StateFive(char input)
        {
            var states = new List<int>();

            if (input == '1')
            {
                states.Add(6);
            }
            else if (input == '0')
            {
                states.Add(5);
            }

            return states;
        }

        public static List<int> StateSix(char input)
        {
            var states = new List<int>();

            if (input == '1')
            {
                states.Add(6);
            }
            else if (input == '0')
            {
                states.Add(6);
            }

            return states;
        }
    }
}
