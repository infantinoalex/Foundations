using System;
using System.Collections.Generic;

namespace SemesterPartTwo.PDA
{
    public class PDA
    {
        private Stack<char> internalStack;

        public PDA(
            List<string> states,
            List<char> alphabet,
            List<char> stackAlphabet,
            string startingState,
            List<string> acceptingState,
            Dictionary<string, Dictionary<Tuple<string, char?, char?, char?>, string>> deltaFunction)
        {
            this.States = states;
            this.Alphabet = alphabet;
            this.StackAlphabet = stackAlphabet;
            this.StartingState = startingState;
            this.AcceptingStates = acceptingState;
            this.DeltaFunction = deltaFunction;
            this.internalStack = new Stack<char>();
        }

        public List<string> States { get; }

        public List<char> Alphabet { get; }

        public List<char> StackAlphabet { get; }

        public string StartingState { get; }

        public List<string> AcceptingStates { get; }

        public Dictionary<string, Dictionary<Tuple<string, char?, char?, char?>, string>> DeltaFunction { get; }

        public bool Execute(string word)
        {
            if (word == null)
            {
                throw new InvalidOperationException($"{nameof(word)} cannot be null. It CAN be empty");
            }

            var currentIndex = 0;
            var currentState = this.StartingState;
            var possibleActions = this.DeltaFunction[currentState];
            while (true)
            {
                var anyActions = false;
                foreach (var key in possibleActions.Keys)
                {
                    var actionTuple = key;

                    var expectedLetter = key.Item2;
                    var expectedStackLetter = key.Item3;

                    char? letter = null;
                    if (currentIndex < word.Length)
                    {
                        letter = word[currentIndex];
                    }

                    var shouldPush = key.Item1;
                    if (shouldPush.Equals("push"))
                    {
                        var letterToPush = key.Item4;

                        var result = this.Push(expectedLetter, expectedStackLetter, letter, letterToPush);
                        if (result)
                        {
                            if (expectedLetter != null)
                            {
                                currentIndex++;
                            }

                            anyActions = true;
                            currentState = possibleActions[key];
                            break;
                        }
                    }
                    else
                    {
                        var result = this.Pop(expectedLetter, expectedStackLetter, letter);
                        if (result)
                        {
                            if (expectedLetter != null)
                            {
                                currentIndex++;
                            }

                            anyActions = true;
                            currentState = possibleActions[key];
                            break;
                        }
                    }

                }

                if (!anyActions)
                {
                    break;
                }

                if (this.DeltaFunction.ContainsKey(currentState))
                {
                    possibleActions = this.DeltaFunction[currentState];
                }
                else
                {
                    break;
                }
            }

            return (this.AcceptingStates.Contains(currentState) && currentIndex > word.Length - 1);
        }

        public bool Pop(char? expectedLetter, char? expectedStackLetter, char? letter)
        {
            var topOfStack = this.internalStack.Peek();

            if ((expectedStackLetter == null || expectedStackLetter == topOfStack) &&
                (expectedLetter == null || expectedLetter == letter))
            {
                this.internalStack.Pop();
                return true;
            }

            return false;
        }

        public bool Push(char? expectedLetter, char? expectedStackLetter, char? letter, char? toPush)
        {
            char? topOfStack = null;
            if (expectedStackLetter != null)
            {
                try
                {
                    topOfStack = this.internalStack.Peek();
                }
                catch (Exception)
                {
                    return false;
                }
            }

            if ((expectedStackLetter == null || expectedStackLetter == topOfStack) &&
                (expectedLetter == null || expectedLetter == letter))
            {
                if (toPush == null)
                {
                    return true;
                }

                this.internalStack.Push(toPush.Value);
                return true;
            }

            return false;
        }
    }
}
