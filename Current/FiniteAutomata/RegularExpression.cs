using FiniteAutomata.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FiniteAutomata
{
    public static class RegularExpression
    {
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
                    result += (char)item;
                }
            }

            return result;
        }

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

                var newKey = new Tuple<string, char>(state, character);
                if (deltaFunction.ContainsKey(newKey))
                {
                    deltaFunction[newKey].AddRange(resultingStates);
                    deltaFunction[newKey].Distinct().ToList();

                    states.AddRange(resultingStates);
                    states = states.Distinct().ToList();
                }
                else
                {
                    deltaFunction.Add(new Tuple<string, char>(state, character), resultingStates);
                    states.Add(state);
                    states.AddRange(resultingStates);
                }
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

        private static Dictionary<Tuple<string, List<object>>, List<string>> CreateDeltaFunction(Dictionary<Tuple<string, List<object>>, List<string>> deltaFunction)
        {
            var hasProcessedAtLeastOne = false;

            var newDeltaFunctionEntries = new List<Dictionary<Tuple<string, List<object>>, List<string>>>();
            foreach (var key in deltaFunction.Keys)
            {
                var currentState = key.Item1;
                var regex = key.Item2;
                var nextStates = deltaFunction[key];

                if (regex.Count == 1)
                {
                    // We have found a transition with a single character so it does not need to be looked at any more
                    newDeltaFunctionEntries.Add(
                        new Dictionary<Tuple<string, List<object>>, List<string>>
                        {
                            {  key, deltaFunction[key] }
                        });
                    continue;
                }
                else
                {
                    hasProcessedAtLeastOne = true;

                    var newDeltaFunction = GetDeltaFunction(regex, currentState, nextStates);
                    var newDeltaFunctionWithRecursion = CreateDeltaFunction(newDeltaFunction);

                    newDeltaFunctionEntries.Add(newDeltaFunctionWithRecursion);
                }
            }

            if (!hasProcessedAtLeastOne)
            {
                return deltaFunction;
            }

            return newDeltaFunctionEntries.SelectMany(dict => dict).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        private static Dictionary<Tuple<string, List<object>>, List<string>> GetDeltaFunction(List<object> regex, string currentState, List<string> nextStates)
        {
            var deltaFunction = new Dictionary<Tuple<string, List<object>>, List<string>>();

            var simplifiedRegex = RemoveRedundantParentheses(regex);
            var indexToSplit = FindMostImportantOperandIndex(simplifiedRegex);

            var operatorEnum = (OperatorsEnum) regex[indexToSplit];
            switch (operatorEnum)
            {
                case OperatorsEnum.KLEENE:
                    var leftHandKleene = regex.GetRange(0, indexToSplit);
                    var kleeneTransition = ConvertKleeneExpression(leftHandKleene, currentState, nextStates);

                    deltaFunction = MergeTwoDictionaries(deltaFunction, kleeneTransition);

                    break;
                case OperatorsEnum.UNION:
                    var leftHandUnion = regex.GetRange(0, indexToSplit);

                    var rightHandUnionCount = regex.Count() - indexToSplit - 1;
                    var rightHandUnion = regex.GetRange(indexToSplit + 1, rightHandUnionCount);
                    var unionTransition = ConvertUnionExpression(leftHandUnion, rightHandUnion, currentState, nextStates);

                    deltaFunction = MergeTwoDictionaries(deltaFunction, unionTransition);

                    break;
                case OperatorsEnum.CONCAT:
                    var leftHandConcat = regex.GetRange(0, indexToSplit);

                    var rightHandConcatCount = regex.Count() - indexToSplit - 1;
                    var rightHandConcat = regex.GetRange(indexToSplit + 1, rightHandConcatCount);
                    var rightHandConcatDictionary = ConvertConcataExpression(leftHandConcat, rightHandConcat, currentState, nextStates);

                    deltaFunction = MergeTwoDictionaries(deltaFunction, rightHandConcatDictionary);

                    break;
                case OperatorsEnum.EMPTY:
                    var emptyTransition = ConvertEmptyExpression(currentState, nextStates);

                    deltaFunction = MergeTwoDictionaries(deltaFunction, emptyTransition);

                    break;
                default:
                    var symbolTransition = ConvertSymbolExpression(new List<object> { regex[indexToSplit] } , currentState, nextStates);

                    deltaFunction = MergeTwoDictionaries(deltaFunction, symbolTransition);

                    break;
            }

            return deltaFunction;
        }

        private static int FindMostImportantOperandIndex(List<object> regex)
        {
            var paranthesesCount = 0;
            var currentIndex = 0;
            OperatorsEnum? mostImportant = null;
            var mostImportantIndex = 0;

            foreach (var item in regex)
            {
                if (item.GetType() == typeof(OperatorsEnum))
                {
                    var operators = (OperatorsEnum)item;
                    switch (operators)
                    {
                        case OperatorsEnum.KLEENE:
                            if (paranthesesCount == 0)
                            {
                                if (mostImportant.HasValue)
                                {
                                    if (mostImportant != OperatorsEnum.UNION || mostImportant != OperatorsEnum.CONCAT)
                                    {
                                        mostImportantIndex = currentIndex;
                                    }
                                }
                            }

                            break;
                        case OperatorsEnum.OPENPARENS:
                            paranthesesCount++;
                            break;
                        case OperatorsEnum.CLOSEPARENS:
                            paranthesesCount--;
                            break;
                        case OperatorsEnum.UNION:
                            if (paranthesesCount == 0)
                            {
                                return currentIndex;
                            }

                            break;
                        case OperatorsEnum.CONCAT:
                            if (paranthesesCount == 0)
                            {
                                return currentIndex;
                            }

                            break;
                    }

                }

                currentIndex++;
            }

            return currentIndex - 1;
        }

        private static List<object> RemoveRedundantParentheses(List<object> regex)
        {
            var firstItem = regex.First();
            if (firstItem.GetType() == typeof(OperatorsEnum) && (OperatorsEnum) firstItem == OperatorsEnum.OPENPARENS)
            {
                regex.RemoveAt(0);
                var otherParenthesesIndex = 0;
                var openParenthesesCount = 0;

                foreach (var item in regex)
                {
                    if (item.GetType() == typeof(OperatorsEnum))
                    {
                        var operators = (OperatorsEnum)item;
                        switch (operators)
                        {
                            case OperatorsEnum.OPENPARENS:
                                openParenthesesCount++;
                                break;
                            case OperatorsEnum.CLOSEPARENS:
                                if (openParenthesesCount == 0)
                                {
                                    regex.RemoveAt(otherParenthesesIndex);
                                    return regex;
                                }
                                else
                                {
                                    openParenthesesCount--;
                                }
                                break;
                        }
                    }

                    otherParenthesesIndex++;
                }

                throw new Exception("Could not remove the extra parentheses. Error occurred.");
            }
            else
            {
                return regex;
            }
        }

        public static Dictionary<Tuple<string, List<object>>, List<string>> MergeTwoDictionaries(Dictionary<Tuple<string, List<object>>, List<string>> first, Dictionary<Tuple<string, List<object>>, List<string>> second)
        {
            var dictionariesList = new List<Dictionary<Tuple<string, List<object>>, List<string>>> { first, second };
            return dictionariesList.SelectMany(dict => dict).ToDictionary(pair => pair.Key, pair => pair.Value);
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
            var leftHandSideState = RegexAsString(leftHandSide) + "-LeftHandSide-" + Guid.NewGuid().ToString();
            var rightHandSideState = RegexAsString(rightHandSide) + "-RightHandSide-" + Guid.NewGuid().ToString();

            return new Dictionary<Tuple<string, List<object>>, List<string>>
            {
                { new Tuple<string, List<object>>(currentState, rightHandSide), nextState },
                { new Tuple<string, List<object>>(currentState, leftHandSide), nextState },
            };
        }

        private static Dictionary<Tuple<string, List<object>>, List<string>> ConvertConcataExpression(List<object> leftHandSide, List<object> rightHandSide, string currentState, List<string> nextState)
        {
            var leftHandSideState = RegexAsString(leftHandSide) + "-LeftHandSide-" + Guid.NewGuid().ToString();
            var rightHandSideState = RegexAsString(rightHandSide) + "-RightHandSide-" + Guid.NewGuid().ToString();

            return new Dictionary<Tuple<string, List<object>>, List<string>>
            {
                { new Tuple<string, List<object>>(currentState, leftHandSide), new List<string> { leftHandSideState }  },
                { new Tuple<string, List<object>>(leftHandSideState, rightHandSide), nextState },
            };
        }

        private static Dictionary<Tuple<string, List<object>>, List<string>> ConvertKleeneExpression(List<object> kleeneExpression, string currentState, List<string> nextState)
        {
            var kleeneState1 = RegexAsString(kleeneExpression) + "-State1-" + Guid.NewGuid().ToString();
            var kleeneState2 = RegexAsString(kleeneExpression) + "-State2-" + Guid.NewGuid().ToString();

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
