using System;
using System.Collections.Generic;

namespace SemesterPartTwo.PDA
{
    public static class CreatePDA
    {
        public static PDA Create0N1NPDA()
        {
            var states = new List<string>
            {
                "q1", "q2", "q3", "q4"
            };

            var alphabet = new List<char>
            {
                '0', '1'
            };

            var stackAlphabet = new List<char>
            {
                '0', '1', '$'
            };

            const string startingState = "q1";

            var acceptingStates = new List<string>
            {
                "q4"
            };

            var deltaFunction = new Dictionary<string, Dictionary<Tuple<string, char?, char?, char?>, string>>
            {
                { "q1", new Dictionary<Tuple<string, char?, char?, char?>, string>
                        {
                            { new Tuple<string, char?, char?, char?>("push", null, null, '$'), "q2" },
                        }
                },
                { "q2", new Dictionary<Tuple<string, char?, char?, char?>, string>
                        {
                            { new Tuple<string, char?, char?, char?>("push", '0', null, '0'), "q2" },
                            { new Tuple<string, char?, char?, char?>("pop", '1', '0', null), "q3" },
                        }
                },
                { "q3", new Dictionary<Tuple<string, char?, char?, char?>, string>
                        {
                            { new Tuple<string, char?, char?, char?>("pop", '1', '0', null), "q3" },
                            { new Tuple<string, char?, char?, char?>("pop", null, '$', null), "q4" },
                        }
                }
            };

            var pda = new PDA(
                states: states,
                alphabet: alphabet,
                stackAlphabet: stackAlphabet,
                startingState: startingState,
                acceptingState: acceptingStates,
                deltaFunction: deltaFunction);

            return pda;
        }
    }
}
