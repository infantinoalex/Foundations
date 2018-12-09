using System;
using System.Collections.Generic;
using System.Text;

namespace SemesterPartTwo.NonDeterministicTM
{
    public class NDTM
    {
        public NDTM(
            List<string> states,
            List<char> alphabet,
            List<char> tapeAlphabet,
            string startingState,
            string acceptState,
            string rejectState,
            Dictionary<string, Dictionary<char, List<Tuple<char, Direction, string>>>> deltaFunction)
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

        public Dictionary<string, Dictionary<char, List<Tuple<char, Direction, string>>>> DeltaFunction { get; }

        public bool Execute(string word, int upperLimit)
        {
            for (int currentLoop = 0; currentLoop < upperLimit; currentLoop++)
            {
                var currentDepth = 0;
                List<Tuple<string, string, int>> toCheck = new List<Tuple<string, string, int>>
                {
                    new Tuple<string, string, int>(this.StartingState, word, 0)
                };

                while (currentDepth <= currentLoop)
                {
                    var newToCheck = new List<Tuple<string, string, int>>();
                    foreach (var tuple in toCheck)
                    {
                        var internalTape = tuple.Item2;

                        var currentState = tuple.Item1;

                        var currentIndex = tuple.Item3;

                        if (this.AcceptState == currentState)
                        {
                            Console.WriteLine($"Found solution at depth {currentLoop}. Tape is {internalTape}");
                            return true;
                        }
                        else if (this.RejectState == currentState)
                        {
                            // Do nothing
                            // Do not add it back to the list
                            continue;
                        }

                        var possibleTransitions = this.DeltaFunction[currentState];
                        var varToCheck = internalTape[currentIndex];

                        if (possibleTransitions.ContainsKey(varToCheck))
                        {
                            var transitions = possibleTransitions[varToCheck];

                            foreach (var transition in transitions)
                            {
                                var currentTransitionTape = internalTape.Clone() as string;
                                var currentTransitionState = currentState.Clone() as string;
                                var currentTransitionIndex = currentIndex;

                                var charToWrite = transition.Item1;
                                var direction = transition.Item2;
                                var nextState = transition.Item3;

                                var stringBuilder = new StringBuilder(currentTransitionTape);

                                stringBuilder[currentTransitionIndex] = charToWrite;
                                currentTransitionTape = stringBuilder.ToString();

                                if (direction == Direction.LEFT)
                                {
                                    currentTransitionIndex--;
                                    if (currentTransitionIndex < 0)
                                    {
                                        currentTransitionTape.Insert(0, "_");
                                        currentTransitionIndex = 0;
                                    }
                                }
                                else
                                {
                                    currentTransitionIndex++;
                                    if (currentTransitionIndex >= currentTransitionTape.Length)
                                    {
                                        currentTransitionTape += "_";
                                    }
                                }

                                currentTransitionState = nextState;
                                var newTuple = new Tuple<string, string, int>(currentTransitionState, currentTransitionTape, currentTransitionIndex);
                                newToCheck.Add(newTuple);
                            }
                        }
                        else
                        {
                            // Ignore this
                            continue;
                        }
                    }

                    toCheck = newToCheck;
                    currentDepth++;
                }
            }

            Console.WriteLine($"Could not find solution in depth of {upperLimit}. Try increasing the depth");
            return false;
        }
    }
}
