using System;
using System.Collections.Generic;
using System.Linq;

namespace SemesterPartTwo.CFG
{
    public class CFG
    {
        private Random random = new Random();

        public CFG(
            List<char> symbols,
            List<char> alphabet,
            Dictionary<char, List<string>> rules,
            char startSymbol)
        {
            this.Symbols = symbols;
            this.Alphabet = alphabet;
            this.Rules = rules;
            this.StartSymbol = startSymbol;
        }

        public List<char> Symbols;

        public List<char> Alphabet;

        public Dictionary<char, List<string>> Rules;

        public char StartSymbol;

        public List<string> CalculateStringsInLanguageOfDepth(int depth)
        {
            var currentStrings = new List<string>();
            if (depth == 0)
            {
                return currentStrings;
            }

            currentStrings.Add(this.StartSymbol.ToString());
            var result = new List<string>();
            for (int currentDepth = 1; currentDepth <= depth; currentDepth++)
            {
                var newStrings = new List<string>();
                foreach (var currentString in currentStrings)
                {
                    var possibleTransformationsDictionary = this.ProcessString(currentString);
                    foreach (var key in possibleTransformationsDictionary.Keys)
                    {
                        var possibleTransformations = possibleTransformationsDictionary[key];

                        var recalculatedStrings = new List<string>();
                        foreach (var transformation in possibleTransformations)
                        {
                            var firstHalf = currentString.Substring(0, key);
                            var secondHalf = currentString.Substring(key + 1);
                            var recalculatedResult = firstHalf + transformation + secondHalf;
                            recalculatedStrings.Add(recalculatedResult);
                        }

                        newStrings.AddRange(recalculatedStrings);
                    }
                }

                currentStrings = newStrings;
                result.AddRange(newStrings);
            }

            result = result.Where(currentString => !this.Symbols.Any(symbol => currentString.Contains(symbol))).ToList();
            return result;
        }

        public Dictionary<int, List<string>> ProcessString(string currentString)
        {
            var currentIndex = 0;
            var result = new Dictionary<int, List<string>>();
            foreach (var item in currentString)
            {
                if (this.Symbols.Contains(item))
                {
                    var possibleTransformations = this.Rules[item];
                    result[currentIndex] = possibleTransformations;
                }

                currentIndex++;
            }

            return result;
        }

        public string RandomStringFromCFG()
        {
            var randomDepth = random.Next(1, 100);
            var results = this.CalculateStringsInLanguageOfDepth(randomDepth);

            var randomIndex = random.Next(0, results.Count - 1);
            return results[randomIndex];
        }
    }
}
