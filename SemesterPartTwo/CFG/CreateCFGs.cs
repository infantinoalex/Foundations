using System.Collections.Generic;

namespace SemesterPartTwo.CFG
{
    public static class CreateCFGs
    {
        public static CFG CreateInClassCFG()
        {
            var symbols = new List<char> { 'B' };
            var alphabet = new List<char> { '0', '1' };
            var rules = new Dictionary<char, List<string>>
            {
                { 'B', new List<string> { string.Empty, "0B1" } },
            };
            var startSymbol = 'B';

            var cfg = new CFG(symbols, alphabet, rules, startSymbol);
            return cfg;
        }

        public static CFG CreateChrisCFG()
        {
            var symbols = new List<char> { 'A', 'B' };
            var alphabet = new List<char> { '0', '1' };
            var rules = new Dictionary<char, List<string>>
            {
                { 'A', new List<string> { "0", "1", "AB" } },
                { 'B', new List<string> { "01", string.Empty }}
            };
            var startSymbol = 'A';

            var cfg = new CFG(symbols, alphabet, rules, startSymbol);
            return cfg;
        }
    }
}
