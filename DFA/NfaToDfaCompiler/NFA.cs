using System;
using System.Collections.Generic;
using System.Linq;
using DFACompiler;

namespace NfaToDfaCompiler
{
    public class NFA
    {
        public NFA(
            List<int> states,
            List<char> alphabet,
            int startingState,
            Dictionary<int, Func<char, List<int>>> deltaFunction,
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

        public Dictionary<int, Func<char, List<int>>> DeltaFunction { get; }

        public string InformalDefinition { get; }

        public bool Execute(string word)
        {
            if (word == null)
            {
                throw new InvalidOperationException($"{nameof(word)} cannot be null. It CAN be empty");
            }

            var currentStates = new List<int>();
            currentStates.Add(this.StartingState);

            foreach (var letter in word)
            {
                if (!currentStates.Any(state => this.States.Contains(state)))
                {
                    throw new InvalidOperationException($"The current states of: [{currentStates}] was not found in the list of possible states: {this.States}");
                }

                if (!this.Alphabet.Contains(letter))
                {
                    return false;
                }

                currentStates = currentStates.ToList();

                var foundStates = new List<int>();
                foreach (var currentState in currentStates)
                {
                    var delta = this.DeltaFunction[currentState];
                    foundStates.AddRange(delta.Invoke(letter));
                }

                currentStates = foundStates;
            }

            return currentStates.Any(state => this.AcceptingStates.Contains(state));
        }

        public DFA ConvertToDFA()
        {
            var nfaStates = new List<int>();

            return new DFA(
                states: null,
                alphabet: this.Alphabet,
                startingState: -1,
                deltaFunction: null,
                acceptingStates: null,
                informalDefinition: this.InformalDefinition);
        }
    }
}
