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
            Dictionary<string, Dictionary<Tuple<char, char>, Tuple<Tuple<char, Direction, string>, Tuple<char, Direction, string>>>> deltaFunction)
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

        public Dictionary<string, Dictionary<Tuple<char, char>, Tuple<Tuple<char, Direction, string>, Tuple<char, Direction, string>>>> DeltaFunction { get; }

        public bool Execute(string word)
        {
            var leftTape = "_" + word + "_";
            var rightTape = "_" + word + "_";

            var currentState = this.StartingState;
            var leftIndex = 1;
            var rightIndex = 1;
            while (true)
            {
                PrintOutCurrentTape(leftTape, leftIndex);
                PrintOutCurrentTape(rightTape, rightIndex);
                if (this.AcceptState == currentState)
                {
                    return true;
                }
                else if (this.RejectState == currentState)
                {
                    return false;
                }

                var possibleTransitions = this.DeltaFunction[currentState];
                // Check left tape
                var leftChar = leftTape[leftIndex];
                var rightChar = rightTape[rightIndex];

                var tupleToCheck = new Tuple<char, char>(leftChar, rightChar);

                if (possibleTransitions.ContainsKey(tupleToCheck))
                {
                    var transition = possibleTransitions[tupleToCheck];

                    // Process Left
                    var leftCharToWrite = transition.Item1.Item1;
                    var leftDirection = transition.Item1.Item2;
                    var nextLeftState = transition.Item1.Item3;

                    var leftTapeBuilder = new StringBuilder(leftTape);
                    leftTapeBuilder[leftIndex] = leftCharToWrite;
                    leftTape = leftTapeBuilder.ToString();
                    if (leftDirection == Direction.LEFT)
                    {
                        leftIndex--;
                        if (leftIndex < 0)
                        {
                            leftTape.Insert(0, "_");
                            leftIndex = 0;
                        }
                    }
                    else
                    {
                        leftIndex++;
                        if (leftIndex > leftTape.Length)
                        {
                            leftTape += "_";
                        }
                    }

                    // Process Right
                    var rightCharToWrite = transition.Item2.Item1;
                    var rightDirection = transition.Item2.Item2;
                    var nextRightState = transition.Item2.Item3;

                    var rightTapeBuilder = new StringBuilder(rightTape);
                    rightTapeBuilder[leftIndex] = rightCharToWrite;
                    rightTape = rightTapeBuilder.ToString();
                    if (rightDirection == Direction.LEFT)
                    {
                        rightIndex--;
                        if (rightIndex < 0)
                        {
                            rightTape.Insert(0, "_");
                            rightIndex = 0;
                        }
                    }
                    else
                    {
                        rightIndex++;
                        if (rightIndex > rightTape.Length)
                        {
                            rightTape += "_";
                        }
                    }

                    if (nextLeftState.Equals(nextRightState))
                    {
                        currentState = nextLeftState;
                    }
                    else
                    {
                        return false;
                    }
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
