using System;
using System.Collections.Generic;

namespace FiniteAutomata
{
    public class DFA
    {
        public DFA(
            List<string> states,
            List<char> alphabet,
            string startingState,
            Dictionary<Tuple<string, char>, string> deltaFunction,
            List<string> acceptingStates,
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

        public List<string> States { get; }

        public List<char> Alphabet { get; }

        public string StartingState { get; }

        public List<string> AcceptingStates { get; }

        public Dictionary<Tuple<string, char>, string> DeltaFunction { get; }

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

                var currentPair = new Tuple<string, char>(currentState, letter);
                if (!this.DeltaFunction.ContainsKey(currentPair))
                {
                    // Enters rabbit hole of never accepting
                    return false;
                }

                currentState = this.DeltaFunction[currentPair];
            }

            return this.AcceptingStates.Contains(currentState);
        }

        public string DeltaFunctionTableAsString()
        {
            var stateToLetterTable = new Dictionary<string, Dictionary<char, string>>();
            foreach (var state in this.States)
            {
                stateToLetterTable.Add(state, null);
            }

            foreach (var tuple in this.DeltaFunction.Keys)
            {
                var state = tuple.Item1;
                var character = tuple.Item2;

                var resultingState = this.DeltaFunction[tuple];

                if (stateToLetterTable[state] == null)
                {
                    stateToLetterTable[state] = new Dictionary<char, string>() { { character, resultingState } };
                }
                else
                {
                    stateToLetterTable[state].Add(character, resultingState);
                }
            }

            var result = $"{this.InformalDefinition}\n\t\t";
            foreach (var item in this.Alphabet)
            {
                result += $"{item}\t\t";
            }

            result += "\n";
            foreach (var key in stateToLetterTable.Keys)
            {
                result += $"{key}\t\t";
                foreach (var innerKey in stateToLetterTable[key].Keys)
                {
                    result += $"{stateToLetterTable[key][innerKey]}\t\t";
                }

                result += "\n";
            }

            result += "\n";

            return result;
        }
    }
}
