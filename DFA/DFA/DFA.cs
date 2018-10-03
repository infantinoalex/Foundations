using System;
using System.Collections.Generic;

namespace DFA
{
    public class DFA
    {
        public DFA(
            List<int> states,
            List<char> alphabet,
            int startingState,
            Dictionary<int, Func<char, int>> deltaFunction,
            List<int> acceptingStates,
            string informalDefinition)
        {
            if (states == null || states.Count == 0)
            {
                throw new ArgumentNullException(nameof(states));
            }

            if (alphabet == null)
            {
                throw new ArgumentNullException(nameof(alphabet));
            }

            if (deltaFunction == null || deltaFunction.Count == 0)
            {
                throw new ArgumentNullException(nameof(deltaFunction));
            }

            if (acceptingStates == null)
            {
                throw new ArgumentNullException(nameof(acceptingStates));
            }

            if (string.IsNullOrEmpty(informalDefinition))
            {
                throw new ArgumentNullException(nameof(informalDefinition));
            }

            this.States = states;
            this.Alphabet = alphabet;
            this.StartingState = startingState;
            this.DeltaFunction = deltaFunction;
            this.AcceptingStates = acceptingStates;
            this.InformalDefinition = informalDefinition;
        }

        public List<int> States { get; }

        public List<char> Alphabet { get; }

        public int StartingState { get; }

        public List<int> AcceptingStates { get; }

        public Dictionary<int, Func<char, int>> DeltaFunction { get; }

        public string InformalDefinition { get; }

        public bool Execute(string word)
        {
            if (word == null)
            {
                throw new InvalidOperationException($"{nameof(word)} cannot be null. It CAN be empty");
            }

            var currentState = this.StartingState;
            foreach (var letter in word)
            {
                if (!this.States.Contains(currentState))
                {
                    throw new InvalidOperationException($"The current state of: [{currentState}] was not found in the list of possible states: {this.States}");
                }

                if (!this.Alphabet.Contains(letter))
                {
                    return false;
                }

                var delta = this.DeltaFunction[currentState];
                currentState = delta.Invoke(letter);
            }

            return this.AcceptingStates.Contains(currentState);
        }
    }
}
