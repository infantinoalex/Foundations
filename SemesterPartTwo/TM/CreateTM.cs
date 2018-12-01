using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemesterPartTwo.TM
{
    public static class CreateTM
    {
        public static TM CreateInClassTM()
        {
            var states = new List<string>
            {
                "a", "b", "c", "d", "e", "f", "g", "qa", "qr"
            };

            var alphabet = new List<char>
            {
                '0', '1', '#', '_'
            };

            var tapeAlphabet = new List<char>
            {
                '^'
            };

            tapeAlphabet.AddRange(alphabet);

            const string startingState = "a";

            const string acceptingState = "qa";

            const string rejectingState = "qr";

            var deltaFunction = new Dictionary<string, Dictionary<char, Tuple<char, Direction, string>>>
            {
                {
                    "a", new Dictionary<char, Tuple<char, Direction, string>>
                    {
                        { '#',  new Tuple<char, Direction, string>('#', Direction.RIGHT, "g") },
                        { '_',  new Tuple<char, Direction, string>('_', Direction.RIGHT, "qr") },
                        { '0',  new Tuple<char, Direction, string>('_', Direction.RIGHT, "b") },
                        { '1',  new Tuple<char, Direction, string>('_', Direction.RIGHT, "e") },
                    }
                },
                {
                    "b", new Dictionary<char, Tuple<char, Direction, string>>
                    {
                        { '0', new Tuple<char, Direction, string>('0', Direction.RIGHT, "b") },
                        { '1', new Tuple<char, Direction, string>('1', Direction.RIGHT, "b") },
                        { '#', new Tuple<char, Direction, string>('#', Direction.RIGHT, "c") },
                    }
                },
                {
                    "c", new Dictionary<char, Tuple<char, Direction, string>>
                    {
                        { '^', new Tuple<char, Direction, string>('^', Direction.RIGHT, "c") },
                        { '0', new Tuple<char, Direction, string>('^', Direction.LEFT, "d") },
                    }
                },
                {
                    "d", new Dictionary<char, Tuple<char, Direction, string>>
                    {
                        { '0', new Tuple<char, Direction, string>('0', Direction.LEFT, "d") },
                        { '1', new Tuple<char, Direction, string>('1', Direction.LEFT, "d") },
                        { '^', new Tuple<char, Direction, string>('^', Direction.LEFT, "d") },
                        { '#', new Tuple<char, Direction, string>('#', Direction.LEFT, "d") },
                        { '_', new Tuple<char, Direction, string>('_', Direction.RIGHT, "a") },
                    }
                },
                {
                    "e", new Dictionary<char, Tuple<char, Direction, string>>
                    {
                        { '0', new Tuple<char, Direction, string>('0', Direction.RIGHT, "e") },
                        { '1', new Tuple<char, Direction, string>('1', Direction.RIGHT, "e") },
                        { '#', new Tuple<char, Direction, string>('#', Direction.RIGHT, "f") },
                    }
                },
                {
                    "f", new Dictionary<char, Tuple<char, Direction, string>>
                    {
                        { '^', new Tuple<char, Direction, string>('^', Direction.RIGHT, "f") },
                        { '1', new Tuple<char, Direction, string>('^', Direction.LEFT, "d") },
                    }
                },
                {
                    "g", new Dictionary<char, Tuple<char, Direction, string>>
                    {
                        { '^', new Tuple<char, Direction, string>('^', Direction.RIGHT, "g") },
                        { '_', new Tuple<char, Direction, string>('_', Direction.LEFT, "qa") },
                    }
                }
            };

            return new TM(
                states: states,
                alphabet: alphabet,
                tapeAlphabet: tapeAlphabet,
                startingState: startingState,
                acceptState: acceptingState,
                rejectState: rejectingState,
                deltaFunction: deltaFunction);
        }

        public static TM Create0WhoseLengthIsPowerTwo()
        {
            var states = new List<string>
            {
                "q1", "q2", "q3", "q4", "q5", "qa", "qr"
            };

            var alphabet = new List<char>
            {
                '0', '_'
            };

            var tapeAlphabet = new List<char>
            {
                'x'
            };

            tapeAlphabet.AddRange(alphabet);

            const string startingState = "q1";

            const string acceptingState = "qa";

            const string rejectingState = "qr";

            var deltaFunction = new Dictionary<string, Dictionary<char, Tuple<char, Direction, string>>>
            {
                {
                    "q1", new Dictionary<char, Tuple<char, Direction, string>>
                    {
                        { '_',  new Tuple<char, Direction, string>('_', Direction.RIGHT, "qr") },
                        { 'x',  new Tuple<char, Direction, string>('x', Direction.RIGHT, "qr") },
                        { '0',  new Tuple<char, Direction, string>('_', Direction.RIGHT, "q2") },
                    }
                },
                {
                    "q2", new Dictionary<char, Tuple<char, Direction, string>>
                    {
                        { 'x',  new Tuple<char, Direction, string>('x', Direction.RIGHT, "q2") },
                        { '_',  new Tuple<char, Direction, string>('_', Direction.RIGHT, "qa") },
                        { '0',  new Tuple<char, Direction, string>('x', Direction.RIGHT, "q3") },
                    }
                },
                {
                    "q3", new Dictionary<char, Tuple<char, Direction, string>>
                    {
                        { 'x',  new Tuple<char, Direction, string>('x', Direction.RIGHT, "q3") },
                        { '_',  new Tuple<char, Direction, string>('_', Direction.LEFT, "q5") },
                        { '0',  new Tuple<char, Direction, string>('0', Direction.RIGHT, "q4") },
                    }
                },
                {
                    "q4", new Dictionary<char, Tuple<char, Direction, string>>
                    {
                        { 'x',  new Tuple<char, Direction, string>('x', Direction.RIGHT, "q4") },
                        { '_',  new Tuple<char, Direction, string>('_', Direction.RIGHT, "qr") },
                        { '0',  new Tuple<char, Direction, string>('x', Direction.RIGHT, "q3") },
                    }
                },
                {
                    "q5", new Dictionary<char, Tuple<char, Direction, string>>
                    {
                        { '0',  new Tuple<char, Direction, string>('0', Direction.LEFT, "q5") },
                        { 'x',  new Tuple<char, Direction, string>('x', Direction.LEFT, "q5") },
                        { '_',  new Tuple<char, Direction, string>('_', Direction.RIGHT, "q2") },
                    }
                }
            };

            return new TM(
                states: states,
                alphabet: alphabet,
                tapeAlphabet: tapeAlphabet,
                startingState: startingState,
                acceptState: acceptingState,
                rejectState: rejectingState,
                deltaFunction: deltaFunction);
        }
    }
}
