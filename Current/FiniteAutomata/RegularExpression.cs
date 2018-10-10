using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteAutomata
{
    public class RegularExpression
    {
        public RegularExpression(string regex)
        {
            this.Regex = regex;
        }

        public string Regex { get; }

        public NFA ConvertToNFA()
        {
            // Do the conversions here

            return new NFA(
                states: null,
                alphabet: null,
                startingState: null,
                deltaFunction: null,
                acceptingStates: null,
                informalDefinition: null);
        }
    }
}
