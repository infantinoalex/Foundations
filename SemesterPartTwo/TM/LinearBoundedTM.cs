using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SemesterPartTwo.TM
{
    public class LinearBoundedTM : TM
    {
        public LinearBoundedTM(
            List<string> states,
            List<char> alphabet,
            List<char> tapeAlphabet,
            string startingState,
            string acceptState,
            string rejectState,
            Dictionary<string, Dictionary<char, Tuple<char, Direction, string>>> deltaFunction)
            : base (states, alphabet, tapeAlphabet, startingState, acceptState, rejectState, deltaFunction)
        {
        }

        public bool Execute(string word)
        {
            var internalTape = "_" + word + "_";


            var currentState = this.StartingState;
            var currentIndex = 1;
            while (true)
            {
                PrintOutCurrentTape(internalTape, currentIndex);
                if (this.AcceptState == currentState)
                {
                    return true;
                }
                else if (this.RejectState == currentState)
                {
                    return false;
                }

                var possibleTransitions = this.DeltaFunction[currentState];
                var varToCheck = internalTape[currentIndex];
                if (possibleTransitions.ContainsKey(varToCheck))
                {
                    var transition = possibleTransitions[varToCheck];

                    var charToWrite = transition.Item1;
                    var direction = transition.Item2;
                    var nextState = transition.Item3;

                    var stringBuilder = new StringBuilder(internalTape);

                    stringBuilder[currentIndex] = charToWrite;
                    internalTape = stringBuilder.ToString();
                    if (direction == Direction.LEFT)
                    {
                        currentIndex--;
                        if (currentIndex < 0)
                        {
                            Console.WriteLine("Excceeded limit of tape.");
                            return false;
                        }
                    }
                    else
                    {
                        currentIndex++;
                        if (currentIndex >= internalTape.Length)
                        {
                            Console.WriteLine("Excceeded limit of tape.");
                            return false;
                        }
                    }

                    currentState = nextState;
                    Thread.Sleep(200);
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
