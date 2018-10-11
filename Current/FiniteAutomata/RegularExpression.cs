using FiniteAutomata.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FiniteAutomata
{
    public static class RegularExpression
    {
        public static NFA ConvertToNFA(List<object> regex)
        {
            // Do the conversions here
            if (regex.Count == 0)
            {
                // Only accepts the empty set. Do some stuff here just to create an NFA that does that
            }

            // Find what is part of the alphabet
            var alphabet = new List<char>();
            foreach (var item in regex)
            {
                if (item.GetType() == typeof(OperatorsEnum))
                {
                    continue;
                }

                alphabet.Add((char) item);
            }

            // Create GNFA
            const string startingGNFAState = "START";
            const string acceptingGNFAState = "END";

            var deltaFunctionToProcess = new Dictionary<Tuple<string, List<object>>, List<string>>
            {
                { new Tuple<string, List<object>>("START", regex), new List<string> { "END" } }
            };

            deltaFunctionToProcess = CreateDeltaFunction(deltaFunctionToProcess);

            var deltaFunction = new Dictionary<Tuple<string, char>, List<string>>();
            var states = new List<string>();
            foreach (var key in deltaFunctionToProcess.Keys)
            {
                var state = key.Item1;
                char character;
                if (key.Item2.Count > 1)
                {
                    throw new Exception("Cannot convert due to regex not working");
                }
                else
                {
                    var item2 = key.Item2.First();
                    character = (char)item2;
                }

                var resultingStates = deltaFunctionToProcess[key];

                deltaFunction.Add(new Tuple<string, char>(state, character), resultingStates);
                states.Add(state);
                states.AddRange(resultingStates);
            }

            states = states.Distinct().ToList();
            states.Sort();

            alphabet = alphabet.Distinct().ToList();
            alphabet.Sort();

            var acceptingStates = new List<string> { acceptingGNFAState };
            var startingState = startingGNFAState;

            return new NFA(
                states: states,
                alphabet: alphabet,
                startingState: startingState,
                deltaFunction: deltaFunction,
                acceptingStates: acceptingStates,
                informalDefinition: $"Regex: {RegexAsString(regex)}");
        }

        public static string RegexAsString(List<object> regex)
        {
            var result = string.Empty;
            foreach (var item in regex)
            {
                if (item.GetType() == typeof(OperatorsEnum))
                {
                    switch (item)
                    {
                        case OperatorsEnum.KLEENE:
                            result += "*";
                            break;
                        case OperatorsEnum.CONCAT:
                            result += " o ";
                            break;
                        case OperatorsEnum.UNION:
                            result += " U ";
                            break;
                        case OperatorsEnum.OPENPARENS:
                            result += "(";
                            break;
                        case OperatorsEnum.CLOSEPARENS:
                            result += ")";
                            break;
                    }
                }
                else
                {
                    result += (char) item;
                }
            }

            return result;
        }

        private static Dictionary<Tuple<string, List<object>>, List<string>> CreateDeltaFunction(Dictionary<Tuple<string, List<object>>, List<string>> deltaFunction)
        {
            var hasProcessedAtleastOne = false;
            var newDeltaFunction = new Dictionary<Tuple<string, List<object>>, List<string>>();
            foreach (var key in deltaFunction.Keys)
            {
                var state = key.Item1;
                var transition = key.Item2;

                if (transition.Count == 1)
                {
                    continue;
                }
                else
                {
                    hasProcessedAtleastOne = true;
                    var newGroups = GroupRegexMembers(transition);

                    // Group and split up the List<object> in the tuple and determine what new states it should go to.
                }
            }

            if (hasProcessedAtleastOne)
            {
                newDeltaFunction = CreateDeltaFunction(newDeltaFunction);
            }

            return newDeltaFunction;
        }

        private static List<List<object>> GroupRegexMembers(List<object> regexToGroup)
        {
            return null;
        }
    }
}
