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

        public static TM CreateBinaryIncrement()
        {
            var states = new List<string>
            {
                "a", "b", "c", "qa"
            };

            var alphabet = new List<char>
            {
                '0', '1', '_'
            };

            var tapeAlphabet = new List<char>
            {
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
                        { '$',  new Tuple<char, Direction, string>('$', Direction.RIGHT, "b") },
                    }
                },
                {
                    "b", new Dictionary<char, Tuple<char, Direction, string>>
                    {
                        { '0', new Tuple<char, Direction, string>('0', Direction.RIGHT, "b") },
                        { '1', new Tuple<char, Direction, string>('1', Direction.RIGHT, "b") },
                        { '#', new Tuple<char, Direction, string>('#', Direction.LEFT, "c") },
                    }
                },
                {
                    "c", new Dictionary<char, Tuple<char, Direction, string>>
                    {
                        { '1', new Tuple<char, Direction, string>('0', Direction.LEFT, "c") },
                        { '0', new Tuple<char, Direction, string>('1', Direction.LEFT, "qa") },
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

        public static TM CreateBinaryDecrement()
        {
            var states = new List<string>
            {
                "a", "b", "c", "d", "e", "qa"
            };

            var alphabet = new List<char>
            {
                '0', '1', '_'
            };

            var tapeAlphabet = new List<char>
            {
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
                        { '$',  new Tuple<char, Direction, string>('$', Direction.RIGHT, "b") },
                    }
                },
                {
                    "b", new Dictionary<char, Tuple<char, Direction, string>>
                    {
                        { '0', new Tuple<char, Direction, string>('1', Direction.RIGHT, "b") },
                        { '1', new Tuple<char, Direction, string>('0', Direction.RIGHT, "b") },
                        { '$', new Tuple<char, Direction, string>('$', Direction.LEFT, "c") },
                    }
                },
                {
                    "c", new Dictionary<char, Tuple<char, Direction, string>>
                    {
                        { '1', new Tuple<char, Direction, string>('0', Direction.LEFT, "c") },
                        { '0', new Tuple<char, Direction, string>('1', Direction.LEFT, "d") },
                    }
                },
                {
                    "d", new Dictionary<char, Tuple<char, Direction, string>>
                    {
                        { '0', new Tuple<char, Direction, string>('0', Direction.LEFT, "d") },
                        { '1', new Tuple<char, Direction, string>('1', Direction.LEFT, "d") },
                        { '$', new Tuple<char, Direction, string>('$', Direction.RIGHT, "e") },
                    }
                },
                {
                    "e", new Dictionary<char, Tuple<char, Direction, string>>
                    {
                        { '0', new Tuple<char, Direction, string>('1', Direction.RIGHT, "e") },
                        { '1', new Tuple<char, Direction, string>('0', Direction.RIGHT, "e") },
                        { '$', new Tuple<char, Direction, string>('$', Direction.RIGHT, "qa") },
                    }
                },
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

        public static TM BinaryAdditionTM()
        {
            var states = new List<string>
            {
                "st", "z1", "z2", "a2", "a3", "m+", "s2", "s3", "s4", "s5",
            };

            var alphabet = new List<char>
            {
                '0', '1', '+', '$'
            };

            var tapeAlphabet = new List<char>
            {
            };

            tapeAlphabet.AddRange(alphabet);

            const string startingState = "st";

            const string acceptingState = "qa";

            const string rejectingState = "qr";

            var deltaFunction = new Dictionary<string, Dictionary<char, Tuple<char, Direction, string>>>
            {
                // Start
                {
                    "st", new Dictionary<char, Tuple<char, Direction, string>>
                    {
                        { '$',  new Tuple<char, Direction, string>('$', Direction.RIGHT, "z1") },
                    }
                },

                // Check if 0
                {
                    "z1", new Dictionary<char, Tuple<char, Direction, string>>
                    {
                        { '0',  new Tuple<char, Direction, string>('0', Direction.RIGHT, "z1") },
                        { '+',  new Tuple<char, Direction, string>('+', Direction.RIGHT, "qa") },
                        { '1',  new Tuple<char, Direction, string>('1', Direction.LEFT, "z2") },
                    }
                },

                // Not zero, move back to start
                {
                    "z2", new Dictionary<char, Tuple<char, Direction, string>>
                    {
                        { '0',  new Tuple<char, Direction, string>('0', Direction.LEFT, "z2") },
                        { '1',  new Tuple<char, Direction, string>('1', Direction.LEFT, "z2") },
                        { '$', new Tuple<char, Direction, string>('$', Direction.RIGHT, "s2") },
                    }
                },

                // Decrement
                {
                    "s2", new Dictionary<char, Tuple<char, Direction, string>>
                    {
                        { '0', new Tuple<char, Direction, string>('1', Direction.RIGHT, "s2") },
                        { '1', new Tuple<char, Direction, string>('0', Direction.RIGHT, "s2") },
                        { '+', new Tuple<char, Direction, string>('+', Direction.LEFT, "s3") },
                    }
                },
                {
                    "s3", new Dictionary<char, Tuple<char, Direction, string>>
                    {
                        { '1', new Tuple<char, Direction, string>('0', Direction.LEFT, "s3") },
                        { '0', new Tuple<char, Direction, string>('1', Direction.LEFT, "s4") },
                    }
                },
                {
                    "s4", new Dictionary<char, Tuple<char, Direction, string>>
                    {
                        { '0', new Tuple<char, Direction, string>('0', Direction.LEFT, "s4") },
                        { '1', new Tuple<char, Direction, string>('1', Direction.LEFT, "s4") },
                        { '$', new Tuple<char, Direction, string>('$', Direction.RIGHT, "s5") },
                    }
                },
                {
                    "s5", new Dictionary<char, Tuple<char, Direction, string>>
                    {
                        { '0', new Tuple<char, Direction, string>('1', Direction.RIGHT, "s5") },
                        { '1', new Tuple<char, Direction, string>('0', Direction.RIGHT, "s5") },
                        { '+', new Tuple<char, Direction, string>('+', Direction.RIGHT, "a2") },
                    }
                },

                // Add
                {
                    "a2", new Dictionary<char, Tuple<char, Direction, string>>
                    {
                        { '0', new Tuple<char, Direction, string>('0', Direction.RIGHT, "a2") },
                        { '1', new Tuple<char, Direction, string>('1', Direction.RIGHT, "a2") },
                        { '$', new Tuple<char, Direction, string>('$', Direction.LEFT, "a3") },
                    }
                },
                {
                    "a3", new Dictionary<char, Tuple<char, Direction, string>>
                    {
                        { '1', new Tuple<char, Direction, string>('0', Direction.LEFT, "a3") },
                        { '0', new Tuple<char, Direction, string>('1', Direction.LEFT, "mb1") },
                    }
                },

                // Move back to start
                {
                    "mb1", new Dictionary<char, Tuple<char, Direction, string>>
                    {
                        { '0', new Tuple<char, Direction, string>('0', Direction.LEFT, "mb1") },
                        { '1', new Tuple<char, Direction, string>('1', Direction.LEFT, "mb1") },
                        { '+', new Tuple<char, Direction, string>('+', Direction.LEFT, "mb1") },
                        { '$', new Tuple<char, Direction, string>('$', Direction.RIGHT, "z1") }
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

        public static TM BinaryAddition()
        {
            var states = new List<string>
            {
                "q0", "q1", "q2", "q3", "q4", "q5"
            };

            var alphabet = new List<char>
            {
                '0', '1', '+'
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
                        { '_',  new Tuple<char, Direction, string>('_', Direction.RIGHT, "q2") },
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

        public static NonDeterministicTM.NDTM CreateInClassNDTM()
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

            var deltaFunction = new Dictionary<string, Dictionary<char, List<Tuple<char, Direction, string>>>>
            {
                {
                    "a", new Dictionary<char, List<Tuple<char, Direction, string>>>
                    {
                        { '#',  new List<Tuple<char, Direction, string>> { new Tuple<char, Direction, string>('#', Direction.RIGHT, "g") } },
                        { '_',  new List<Tuple<char, Direction, string>> { new Tuple<char, Direction, string>('_', Direction.RIGHT, "qr") } },
                        { '0',  new List<Tuple<char, Direction, string>> { new Tuple<char, Direction, string>('_', Direction.RIGHT, "b") } },
                        { '1',  new List<Tuple<char, Direction, string>> { new Tuple<char, Direction, string>('_', Direction.RIGHT, "e") } },
                    }
                },
                {
                    "b", new Dictionary<char, List<Tuple<char, Direction, string>>>
                    {
                        { '0', new List<Tuple<char, Direction, string>> { new Tuple<char, Direction, string>('0', Direction.RIGHT, "b") } },
                        { '1', new List<Tuple<char, Direction, string>> { new Tuple<char, Direction, string>('1', Direction.RIGHT, "b") } },
                        { '#', new List<Tuple<char, Direction, string>> { new Tuple<char, Direction, string>('#', Direction.RIGHT, "c") } },
                    }
                },
                {
                    "c", new Dictionary<char, List<Tuple<char, Direction, string>>>
                    {
                        { '^', new List<Tuple<char, Direction, string>> { new Tuple<char, Direction, string>('^', Direction.RIGHT, "c") } },
                        { '0', new List<Tuple<char, Direction, string>> { new Tuple<char, Direction, string>('^', Direction.LEFT, "d") } },
                    }
                },
                {
                    "d", new Dictionary<char, List<Tuple<char, Direction, string>>>
                    {
                        { '0', new List<Tuple<char, Direction, string>> { new Tuple<char, Direction, string>('0', Direction.LEFT, "d") } },
                        { '1', new List<Tuple<char, Direction, string>> { new Tuple<char, Direction, string>('1', Direction.LEFT, "d") } },
                        { '^', new List<Tuple<char, Direction, string>> { new Tuple<char, Direction, string>('^', Direction.LEFT, "d") } },
                        { '#', new List<Tuple<char, Direction, string>> { new Tuple<char, Direction, string>('#', Direction.LEFT, "d") } },
                        { '_', new List<Tuple<char, Direction, string>> { new Tuple<char, Direction, string>('_', Direction.RIGHT, "a") } },
                    }
                },
                {
                    "e", new Dictionary<char, List<Tuple<char, Direction, string>>>
                    {
                        { '0', new List<Tuple<char, Direction, string>> { new Tuple<char, Direction, string>('0', Direction.RIGHT, "e") } },
                        { '1', new List<Tuple<char, Direction, string>> { new Tuple<char, Direction, string>('1', Direction.RIGHT, "e") } },
                        { '#', new List<Tuple<char, Direction, string>> { new Tuple<char, Direction, string>('#', Direction.RIGHT, "f") } },
                    }
                },
                {
                    "f", new Dictionary<char, List<Tuple<char, Direction, string>>>
                    {
                        { '^', new List<Tuple<char, Direction, string>> { new Tuple<char, Direction, string>('^', Direction.RIGHT, "f") } },
                        { '1', new List<Tuple<char, Direction, string>> { new Tuple<char, Direction, string>('^', Direction.LEFT, "d") } },
                    }
                },
                {
                    "g", new Dictionary<char, List<Tuple<char, Direction, string>>>
                    {
                        { '^', new List<Tuple<char, Direction, string>> { new Tuple<char, Direction, string>('^', Direction.RIGHT, "g") } },
                        { '_', new List<Tuple<char, Direction, string>> { new Tuple<char, Direction, string>('_', Direction.LEFT, "qa") } },
                    }
                }
            };

            return new NonDeterministicTM.NDTM(
                states: states,
                alphabet: alphabet,
                tapeAlphabet: tapeAlphabet,
                startingState: startingState,
                acceptState: acceptingState,
                rejectState: rejectingState,
                deltaFunction: deltaFunction);
        }

        public static LinearBoundedTM CreateInClassTMLinearlyBound()
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

            return new LinearBoundedTM(
                states: states,
                alphabet: alphabet,
                tapeAlphabet: tapeAlphabet,
                startingState: startingState,
                acceptState: acceptingState,
                rejectState: rejectingState,
                deltaFunction: deltaFunction);
        }

        public static LinearBoundedTM CreateFailedLinearBoundedTM()
        {
            var states = new List<string>
            {
                "a", "qa", "qr"
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
                        { '#',  new Tuple<char, Direction, string>('#', Direction.RIGHT, "a") },
                        { '0',  new Tuple<char, Direction, string>('0', Direction.RIGHT, "a") },
                        { '1',  new Tuple<char, Direction, string>('1', Direction.RIGHT, "a") },
                        { '_',  new Tuple<char, Direction, string>('_', Direction.RIGHT, "a") },
                    }
                },
            };

            return new LinearBoundedTM(
                states: states,
                alphabet: alphabet,
                tapeAlphabet: tapeAlphabet,
                startingState: startingState,
                acceptState: acceptingState,
                rejectState: rejectingState,
                deltaFunction: deltaFunction);
        }

        public static MultiTapeTM.MultiTapeTM CreateExampleMultiTapeTM()
        {
            var states = new List<string>
            {
                "a", "qa", "qr"
            };

            var alphabet = new List<char>
            {
                '0', '1', '_'
            };

            var tapeAlphabet = new List<char>
            {
                '*'
            };

            tapeAlphabet.AddRange(alphabet);

            const string startingState = "a";

            const string acceptingState = "qa";

            const string rejectingState = "qr";

            var deltaFunction = new Dictionary<string, Dictionary<Tuple<int, char>, Tuple<char, Direction, string>>>
            {
                {
                    "a", new Dictionary<Tuple<int, char>, Tuple<char, Direction, string>>
                    {
                        { new Tuple<int, char>(0, '0'), new Tuple<char, Direction, string>('*', Direction.RIGHT, "a") },
                        { new Tuple<int, char>(0, '1'), new Tuple<char, Direction, string>('1', Direction.RIGHT, "a") },
                        { new Tuple<int, char>(0, '_'), new Tuple<char, Direction, string>('_', Direction.LEFT, "qa") },
                        { new Tuple<int, char>(1, '0'), new Tuple<char, Direction, string>('0', Direction.RIGHT, "a") },
                        { new Tuple<int, char>(1, '1'), new Tuple<char, Direction, string>('*', Direction.RIGHT, "a") },
                        { new Tuple<int, char>(1, '_'), new Tuple<char, Direction, string>('_', Direction.LEFT, "qa") }
                    }
                }
            };

            return new MultiTapeTM.MultiTapeTM(
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
