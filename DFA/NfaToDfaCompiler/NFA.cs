using System;
using System.Collections.Generic;
using System.Linq;

namespace NfaToDfaCompiler
{
    public class NFA
    {
        public NFA(
            List<int> states,
            List<char> alphabet,
            int startingState,
            Dictionary<int, Func<char, List<int>>> deltaFunction,
            Dictionary<int, Func<List<int>>> epsilonTransitions,
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
            this.EpsilonTransitions = epsilonTransitions;
        }

        public List<int> States { get; }

        public List<char> Alphabet { get; }

        public int StartingState { get; }

        public List<int> AcceptingStates { get; }

        public Dictionary<int, Func<char, List<int>>> DeltaFunction { get; }

        public Dictionary<int, Func<List<int>>> EpsilonTransitions { get; }

        public string InformalDefinition { get; }

        public bool Execute(string word)
        {
            if (word == null)
            {
                throw new InvalidOperationException($"{nameof(word)} cannot be null. It CAN be empty");
            }

            var currentStates = new List<int>();
            currentStates.Add(this.StartingState);
            currentStates.AddRange(this.processEpsilonTransitions(currentStates));

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

                currentStates.AddRange(this.processEpsilonTransitions(currentStates));
                currentStates = currentStates.Distinct().ToList();

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

        private List<int> processEpsilonTransitions(List<int> currentStates)
        {
            // Need to loop through until there are no more epsilon transitions
            // Can we do recursion?
            var resultingStates = new List<int>();
            foreach (var currentState in currentStates)
            {
                if (this.EpsilonTransitions.ContainsKey(currentState))
                {
                    var epsilonFunction = this.EpsilonTransitions[currentState];
                    resultingStates.AddRange(epsilonFunction.Invoke());
                }
            }

            if (resultingStates.Any(state => this.EpsilonTransitions.ContainsKey(state)))
            {
                resultingStates.AddRange(this.processEpsilonTransitions(resultingStates));
            }

            return resultingStates;
        }
    }
}
