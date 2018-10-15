using System;
using System.Collections.Generic;
using System.Linq;

namespace FiniteAutomata
{
    public class NFA
    {
        public NFA(
            List<string> states,
            List<char> alphabet,
            string startingState,
            Dictionary<Tuple<string, char>, List<string>> deltaFunction,
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

        public Dictionary<Tuple<string, char>, List<string>> DeltaFunction { get; }

        public string InformalDefinition { get; }

        public bool Execute(string word)
        {
            if (word == null)
            {
                throw new InvalidOperationException($"{nameof(word)} cannot be null. It CAN be empty");
            }

            var currentStates = new List<string>();
            currentStates.Add(this.StartingState);
            currentStates.AddRange(this.ProcessEpsilonTransitions(currentStates, new List<string>()));

            foreach (var letter in word)
            {
                if (!currentStates.Any(state => this.States.Contains(state)))
                {
                    return false;
                }

                if (!this.Alphabet.Contains(letter))
                {
                    return false;
                }

                var foundStates = new List<string>();
                foreach (var currentState in currentStates)
                {
                    var currentPair = new Tuple<string, char>(currentState, letter);
                    if (!this.DeltaFunction.ContainsKey(currentPair))
                    {
                        // Enters rabbit hole of not accepting
                        continue ;
                    }

                    foundStates.AddRange(this.DeltaFunction[currentPair]);
                }

                currentStates = foundStates;
                currentStates.AddRange(this.ProcessEpsilonTransitions(currentStates, new List<string>()));
            }

            return currentStates.Any(state => this.AcceptingStates.Contains(state));
        }

        public string DeltaFunctionTableAsString()
        {
            var stateToLetterTable = new Dictionary<string, Dictionary<char, List<string>>>();
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
                    stateToLetterTable[state] = new Dictionary<char, List<string>>() { { character, resultingState } };
                }
                else
                {
                    stateToLetterTable[state].Add(character, resultingState);
                }
            }

            var result = $"{this.InformalDefinition}\n\t\t\t\t";
            foreach (var item in this.Alphabet)
            {
                result += $"{item}\t\t\t\t";
            }

            result += "\n";
            foreach (var key in stateToLetterTable.Keys)
            {
                result += $"{key}\t\t\t\t";

                if (stateToLetterTable[key] == null)
                {
                    for (int i = 0; i < this.Alphabet.Count; i++)
                    {
                        result += "\t\t\t\t";
                    }
                }
                else
                {
                    int counter = 0;
                    foreach (var innerKey in stateToLetterTable[key].Keys)
                    {
                        var index = this.Alphabet.IndexOf(innerKey);
                        for (int i = counter; i < index; i++)
                        {
                            result += "\t\t\t\t";
                        }

                        result += $"[{string.Join(", ", stateToLetterTable[key][innerKey].ToArray())}]\t\t\t\t";
                        counter++;
                    }
                }

                result += "\n";
            }

            result += "\n";

            return result;
        }

        public DFA ConvertToDFA()
        {
            var dfaDeltaFunction = new Dictionary<Tuple<string, char>, string>();
            var qPrime = new List<string>();

            // While there is a state in our dfaDeltaFunction that we havent expanded in qPrime, expand it
            var currentState = this.StartingState;
            var dfaStartingState = string.Empty;
            while (currentState != null)
            {
                var currentStateAsList = currentState.Split(' ').ToList();

                var epsilonStates = this.CalculateEpsilonTransitions(currentStateAsList);
                currentStateAsList.AddRange(epsilonStates);
                currentStateAsList = currentStateAsList.Distinct().ToList();
                currentStateAsList.Sort();

                currentState = string.Join(" ", currentStateAsList.ToArray());

                // Add to qPrime now since we are currently expanding
                qPrime.Add(currentState);
                
                if (string.IsNullOrWhiteSpace(dfaStartingState))
                {
                    dfaStartingState = currentState;
                }

                // Find all the transitions for a single letter that the current state can go to
                foreach (var letter in this.Alphabet)
                {
                    var pair = new Tuple<string, char>(currentState, letter);

                    // This function will take a state and a letter and append all possible paths it could take
                    var resultingStates = this.CalculateNewInput(currentStateAsList, letter);

                    if (!dfaDeltaFunction.ContainsKey(pair))
                    {
                        var resultingStatesAsString = string.Join(" ", resultingStates.ToArray());
                        dfaDeltaFunction.Add(pair, resultingStatesAsString);
                    }
                }

                currentState = dfaDeltaFunction.Values.Where(value => !qPrime.Contains(value)).FirstOrDefault();
            }

            var acceptingStates = new List<string>();
            foreach (var item in qPrime)
            {
                if (this.AcceptingStates.Any(state => item.Contains(state)))
                {
                    acceptingStates.Add(item);
                }
            }

            return new DFA(
                states: qPrime,
                alphabet: this.Alphabet,
                startingState: dfaStartingState,
                deltaFunction: dfaDeltaFunction,
                acceptingStates: acceptingStates,
                informalDefinition: this.InformalDefinition);
        }

        private List<string> CalculateEpsilonTransitions(List<string> states)
        {
            var epsilonTransitionResult = this.ProcessEpsilonTransitions(states, new List<string>());
            epsilonTransitionResult = epsilonTransitionResult.Distinct().ToList();
            epsilonTransitionResult.Sort();
            return epsilonTransitionResult;
        }

        private List<string> CalculateNewInput(List<string> states, char character)
        {
            var result = new List<string>();
            foreach (var input in states)
            {
                var pair = new Tuple<string, char>(input, character);
                if (!this.DeltaFunction.ContainsKey(pair) || this.DeltaFunction[pair] == null)
                {
                    continue;
                }

                result.AddRange(this.DeltaFunction[pair]);
            }

            result = result.Distinct().ToList();
            result.Sort();
            return result;
        }

        private List<string> ProcessEpsilonTransitions(List<string> currentStates, List<string> previousStates)
        {
            var epsilonStates = new List<string>();
            foreach (var state in currentStates)
            {
                var epsilonPair = new Tuple<string, char>(state, '\0');
                if (this.DeltaFunction.ContainsKey(epsilonPair))
                {
                    epsilonStates.AddRange(this.DeltaFunction[epsilonPair]);
                }
            }

            var unexploredStates = epsilonStates.Where(state => !previousStates.Contains(state)).ToList();
            if (unexploredStates.Count != 0)
            {
                previousStates.AddRange(currentStates);
                epsilonStates.AddRange(this.ProcessEpsilonTransitions(unexploredStates, previousStates));
            }

            return epsilonStates;
        }
    }
}
