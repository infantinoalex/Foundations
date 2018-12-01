using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SemesterPartTwo.TM
{
    public class TM
    {
        public TM(
            List<string> states,
            List<char> alphabet,
            List<char> tapeAlphabet,
            string startingState,
            string acceptState,
            string rejectState,
            Dictionary<string, Dictionary<char, Tuple<char, Direction, string>>> deltaFunction)
        {
            this.States = states;
            this.Alphabet = alphabet;
            this.TapeAlphabet = tapeAlphabet;
            this.StartingState = startingState;
            this.AcceptState = acceptState;
            this.RejectState = rejectState;
            this.DeltaFunction = deltaFunction;
        }

        public List<string> States { get; }

        public List<char> Alphabet { get; }

        public List<char> TapeAlphabet { get; }

        public string StartingState { get; }

        public string AcceptState { get; }

        public string RejectState { get; }

        public Dictionary<string, Dictionary<char, Tuple<char, Direction, string>>> DeltaFunction { get; }

        public bool Execute(string word)
        {
            var internalTape = word;
            internalTape = "_" + internalTape;
            internalTape += "_";

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
                            internalTape.Insert(0, "_");
                            currentIndex = 0;
                        }
                    }
                    else
                    {
                        currentIndex++;
                        if (currentIndex > internalTape.Length)
                        {
                            internalTape += "_";
                        }
                    }

                    currentState = nextState;
                }
                else
                {
                    return false;
                }
            }
        }

        public void PrintOutCurrentTape(string internalTape, int currentIndex)
        {
            Thread.Sleep(500);
            var positionIndictorString = string.Empty;
            for (int index = 0; index < currentIndex; index++)
            {
                positionIndictorString += " ";
            }

            positionIndictorString += "\u2193";

            Console.WriteLine($"\n{positionIndictorString}");
            Console.WriteLine($"{internalTape}");
        }
    }
}
