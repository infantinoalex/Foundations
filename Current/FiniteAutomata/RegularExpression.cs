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
                            result += string.Empty;
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
                        case OperatorsEnum.EMPTY:
                            result += string.Empty;
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
                var nextStates = deltaFunction[key];

                if (transition.Count == 1)
                {
                    continue;
                }
                else
                {
                    hasProcessedAtleastOne = true;
                    var newGroups = GroupRegexMembers(transition);

                    foreach (var operatorsEnum in newGroups.Keys)
                    {
                        switch (operatorsEnum)
                        {
                            case OperatorsEnum.KLEENE:
                                var newKleeneStates = ConvertKleeneExpression(newGroups[operatorsEnum].Item1, state, nextStates);
                                var dictionariesWithNewKleeneStates = new List<Dictionary<Tuple<string, List<object>>, List<string>>> { newKleeneStates, newDeltaFunction };
                                newDeltaFunction = dictionariesWithNewKleeneStates.SelectMany(dict => dict).ToDictionary(pair => pair.Key, pair => pair.Value);
                                break;
                            case OperatorsEnum.UNION:
                                var newUnionStates = ConvertUnionExpression(newGroups[operatorsEnum].Item1, newGroups[operatorsEnum].Item2, state, nextStates);
                                var dictionariesWithNewUnionStates = new List<Dictionary<Tuple<string, List<object>>, List<string>>> { newUnionStates, newDeltaFunction };
                                newDeltaFunction = dictionariesWithNewUnionStates.SelectMany(dict => dict).ToDictionary(pair => pair.Key, pair => pair.Value);
                                break;
                            case OperatorsEnum.CONCAT:
                                var newConcatStates = ConvertConcataExpression(newGroups[operatorsEnum].Item1, newGroups[operatorsEnum].Item2, state, nextStates);
                                var dictionariesWithNewConcatStates = new List<Dictionary<Tuple<string, List<object>>, List<string>>> { newConcatStates, newDeltaFunction };
                                newDeltaFunction = dictionariesWithNewConcatStates.SelectMany(dict => dict).ToDictionary(pair => pair.Key, pair => pair.Value);
                                break;
                            case OperatorsEnum.EMPTY:
                                var newEmptyStates = ConvertEmptyExpression(state, nextStates);
                                var dictionariesWithNewEmptyStates = new List<Dictionary<Tuple<string, List<object>>, List<string>>> { newEmptyStates, newDeltaFunction };
                                newDeltaFunction = dictionariesWithNewEmptyStates.SelectMany(dict => dict).ToDictionary(pair => pair.Key, pair => pair.Value);
                                break;
                            case OperatorsEnum.SYMBOL:
                                var newSymbolStates = ConvertSymbolExpression(newGroups[operatorsEnum].Item1, state, nextStates);
                                var dictionariesWithNewSymbolStates = new List<Dictionary<Tuple<string, List<object>>, List<string>>> { newSymbolStates, newDeltaFunction };
                                newDeltaFunction = dictionariesWithNewSymbolStates.SelectMany(dict => dict).ToDictionary(pair => pair.Key, pair => pair.Value);
                                break;
                        }
                    }
                }
            }

            if (hasProcessedAtleastOne)
            {
                var newDeltaFunctionRecursion = CreateDeltaFunction(newDeltaFunction);
                var dictionariesWithRecursion = new List<Dictionary<Tuple<string, List<object>>, List<string>>> { newDeltaFunctionRecursion, newDeltaFunction };
                newDeltaFunction = dictionariesWithRecursion.SelectMany(dict => dict).ToDictionary(pair => pair.Key, pair => pair.Value);
            }

            return newDeltaFunction;
        }

        private static Dictionary<OperatorsEnum, Tuple<List<object>, List<object>>> GroupRegexMembers(List<object> regexToGroup)
        {
            // Split into two groups by the most prominent OperatorsEnum
            var openParensCount = 0;
            var index = 0;

            if (regexToGroup.Count == 1)
            {
                if (regexToGroup[0].GetType() == typeof(OperatorsEnum) && (OperatorsEnum)regexToGroup[0] == OperatorsEnum.EMPTY)
                {
                    return new Dictionary<OperatorsEnum, Tuple<List<object>, List<object>>>
                    {
                        { OperatorsEnum.EMPTY, null }
                    };
                }

                return new Dictionary<OperatorsEnum, Tuple<List<object>, List<object>>>
                {
                    { OperatorsEnum.SYMBOL, new Tuple<List<object>, List<object>>(regexToGroup, null) }
                };
            }

            var firstObject = regexToGroup.First();
            var isStartingParens = false;
            if (firstObject.GetType() == typeof(OperatorsEnum) && (OperatorsEnum) firstObject == OperatorsEnum.OPENPARENS)
            {
                isStartingParens = true;
                regexToGroup.RemoveAt(0);
                openParensCount = 0;
            }
            foreach (var item in regexToGroup)
            {
                if (!isStartingParens)
                {
                    if (item.GetType() == typeof(OperatorsEnum))
                    {
                        var operators = (OperatorsEnum)item;
                        switch (operators)
                        {
                            case OperatorsEnum.KLEENE:
                                var kleeneCount = regexToGroup.Count();
                                var kleeneLeftHandSide = regexToGroup.GetRange(0, index);
                                return new Dictionary<OperatorsEnum, Tuple<List<object>, List<object>>>
                                {
                                    { OperatorsEnum.KLEENE, new Tuple<List<object>, List<object>>(kleeneLeftHandSide, null) }
                                };
                            case OperatorsEnum.UNION:
                                var unionCount = regexToGroup.Count();
                                var unionLeftHandSide = regexToGroup.GetRange(0, index);
                                var unionRightHandSide = regexToGroup.GetRange(index + 1, unionCount - unionLeftHandSide.Count() - 1);
                                return new Dictionary<OperatorsEnum, Tuple<List<object>, List<object>>>
                                {
                                    { OperatorsEnum.UNION, new Tuple<List<object>, List<object>>(unionLeftHandSide, unionRightHandSide) }
                                };
                            case OperatorsEnum.CONCAT:
                                var concatCount = regexToGroup.Count();
                                var concatLeftHandSide = regexToGroup.GetRange(0, index);
                                var concatRightHandSide = regexToGroup.GetRange(index + 1, concatCount - concatLeftHandSide.Count());
                                return new Dictionary<OperatorsEnum, Tuple<List<object>, List<object>>>
                                {
                                    { OperatorsEnum.CONCAT, new Tuple<List<object>, List<object>>(concatLeftHandSide, concatRightHandSide) }
                                };
                        }
                    }
                }
                else
                {
                    if (item.GetType() == typeof(OperatorsEnum))
                    {
                        var operators = (OperatorsEnum) item;
                        switch (operators)
                        {
                            case OperatorsEnum.OPENPARENS:
                                openParensCount++;
                                break;
                            case OperatorsEnum.CLOSEPARENS:
                                if (openParensCount == 0)
                                {
                                    var count = regexToGroup.Count();
                                    var innerParaenthasis = regexToGroup.GetRange(0, index);
                                    var innerParens = GroupRegexMembers(innerParaenthasis);

                                }
                                else
                                {
                                    openParensCount--;
                                }
                                break;
                        }
                    }
                }

                index++;
            }

            return null;
        }

        private static Dictionary<Tuple<string, List<object>>, List<string>> ConvertEmptyExpression(string currentState, List<string> nextState)
        {
            return new Dictionary<Tuple<string, List<object>>, List<string>>
            {
                { new Tuple<string, List<object>>(currentState, new List<object> { '\0' } ), nextState }
            };
        }

        private static Dictionary<Tuple<string, List<object>>, List<string>> ConvertSymbolExpression(List<object> symbol, string currentState, List<string> nextState)
        {
            return new Dictionary<Tuple<string, List<object>>, List<string>>
            {
                { new Tuple<string, List<object>>(currentState, symbol), nextState }
            };
        }

        private static Dictionary<Tuple<string, List<object>>, List<string>> ConvertUnionExpression(List<object> leftHandSide, List<object> rightHandSide, string currentState, List<string> nextState)
        {
            var leftHandSideState = RegexAsString(leftHandSide) + "-LeftHandSide";
            var rightHandSideState = RegexAsString(rightHandSide) + "-RightHandSide";

            return new Dictionary<Tuple<string, List<object>>, List<string>>
            {
                { new Tuple<string, List<object>>(currentState, rightHandSide), new List<string> { rightHandSideState }  },
                { new Tuple<string, List<object>>(currentState, leftHandSide), new List<string> { leftHandSideState }  },
                { new Tuple<string, List<object>>(rightHandSideState, new List<object> { '\0' } ), nextState },
                { new Tuple<string, List<object>>(leftHandSideState, new List<object> { '\0' } ), nextState }
            };
        }

        private static Dictionary<Tuple<string, List<object>>, List<string>> ConvertConcataExpression(List<object> leftHandSide, List<object> rightHandSide, string currentState, List<string> nextState)
        {
            var leftHandSideState = RegexAsString(leftHandSide) + "-LeftHandSide";
            var rightHandSideState = RegexAsString(rightHandSide) + "-RightHandSide";

            return new Dictionary<Tuple<string, List<object>>, List<string>>
            {
                { new Tuple<string, List<object>>(currentState, leftHandSide), new List<string> { leftHandSideState }  },
                { new Tuple<string, List<object>>(leftHandSideState, rightHandSide), nextState },
            };
        }

        private static Dictionary<Tuple<string, List<object>>, List<string>> ConvertKleeneExpression(List<object> kleeneExpression, string currentState, List<string> nextState)
        {
            var kleeneState1 = RegexAsString(kleeneExpression) + "-State1";
            var kleeneState2 = RegexAsString(kleeneExpression) + "-State2";

            return new Dictionary<Tuple<string, List<object>>, List<string>>
            {
                { new Tuple<string, List<object>>(currentState, new List<object> { '\0' } ), new List<string> { kleeneState1 }  },
                { new Tuple<string, List<object>>(kleeneState1, kleeneExpression ), new List<string> { kleeneState2 }  },
                { new Tuple<string, List<object>>(kleeneState2, new List<object> { '\0' } ), new List<string> { kleeneState1 }  },
                { new Tuple<string, List<object>>(kleeneState2, new List<object> { '\0' } ), nextState },
                { new Tuple<string, List<object>>(currentState, new List<object> { '\0' } ), nextState },
            };
        }
    }
}
