using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SemesterPartTwo.MultiTapeTM
{
    public class MultiTapeTM
    {
        public MultiTapeTM(
            List<string> states,
            List< char > alphabet,
            List<char> tapeAlphabet,
            string startingState,
            string acceptState,
            string rejectState,
            Dictionary<string, Dictionary<Tuple<int, char>, Tuple<char, Direction, string>>> deltaFunction)
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

        public Dictionary<string, Dictionary<Tuple<int, char>, Tuple<char, Direction, string>>> DeltaFunction { get; }

        public bool Execute(List<string> tapes)
        {
            var currentIndexes = new Dictionary<int, int>();
            for (int loop = 0; loop < tapes.Count; loop++)
            {
                currentIndexes[loop] = 0;
            }

            var currentState = this.StartingState;
            while (true)
            {
                for (int loop = 0; loop < tapes.Count; loop++)
                {
                    PrintOutCurrentTape(loop, tapes[loop], currentIndexes[loop]);
                }

                if (this.AcceptState == currentState)
                {
                    return true;
                }
                else if (this.RejectState == currentState)
                {
                    return false;
                }

                var possibleTransitions = this.DeltaFunction[currentState];
                var nextState = string.Empty;

                // Check each tape
                for (int loop = 0; loop < tapes.Count; loop++)
                {
                    var currentTape = tapes[loop];

                    var currentIndex = currentIndexes[loop];
                    var charToCheck = currentTape[currentIndex];
                    var tupleToCheck = new Tuple<int, char>(loop, charToCheck);

                    if (possibleTransitions.ContainsKey(tupleToCheck))
                    {
                        var transition = possibleTransitions[tupleToCheck];

                        // Process Right
                        var charToWrite = transition.Item1;
                        var direction = transition.Item2;
                        var tapeNextState = transition.Item3;

                        var tapeBuilder = new StringBuilder(currentTape);
                        tapeBuilder[currentIndex] = charToWrite;
                        currentTape = tapeBuilder.ToString();
                        if (direction == Direction.LEFT)
                        {
                            currentIndex--;
                            if (currentIndex < 0)
                            {
                                currentTape.Insert(0, "_");
                                currentIndex = 0;
                            }
                        }
                        else
                        {
                            currentIndex++;
                            if (currentIndex >= currentTape.Length)
                            {
                                currentTape += "_";
                            }
                        }

                        currentIndexes[loop] = currentIndex;
                        tapes[loop] = currentTape;

                        if (string.IsNullOrEmpty(nextState))
                        {
                            nextState = tapeNextState;
                        }
                        else if (!tapeNextState.Equals(nextState))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

                currentState = nextState;
            }
        }

        public void PrintOutCurrentTape(int tapeNumber, string internalTape, int currentIndex)
        {
            Thread.Sleep(500);
            var positionIndictorString = string.Empty;
            for (int index = 0; index < currentIndex; index++)
            {
                positionIndictorString += " ";
            }

            positionIndictorString += "\u2193";

            Console.WriteLine($"\nTape {tapeNumber}");
            Console.WriteLine($"{positionIndictorString}");
            Console.WriteLine($"{internalTape}");
        }
    }
}
