using System;
using System.Collections.Generic;

namespace DFA
{
    public class DFA
    {
        private List<int> states;
        private List<char> alphabet;
        private int startingState;
        private List<int> acceptingStates;
        private Dictionary<int, Func<char, int>> deltaFunction;

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

            if (deltaFunction == null)
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

            this.states = states;
            this.alphabet = alphabet;
            this.startingState = startingState;
            this.deltaFunction = deltaFunction;
            this.acceptingStates = acceptingStates;
            this.InformalDefinition = informalDefinition;
        }

        public string InformalDefinition { get; }

        public bool Execute(string word)
        {
            var currentState = this.startingState;
            foreach (var letter in word)
            {
                if (!this.states.Contains(currentState))
                {
                    throw new InvalidOperationException($"The current state of: [{currentState}] was not found in the list of possible states: {this.states}");
                }

                if (!this.alphabet.Contains(letter))
                {
                    return false;
                }

                var delta = this.deltaFunction[currentState];
                currentState = delta.Invoke(letter);
            }

            return this.acceptingStates.Contains(currentState);
        }
    }
}
