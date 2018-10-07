using DFACompiler.EvenLengthString;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using DFACompiler;

namespace DFA.Tests
{
    [TestClass]
    public class DFAUnitTests
    {
        [TestMethod]
        public void Constructor_ValidArguments_ValidObjectCreated()
        {
            // Arrange
            const int state = 1;
            var states = new List<int>()
            {
                state
            };

            var letter = 'a';
            var alphabet = new List<char>
            {
                letter
            };

            const int startingState = 1;

            var deltaFunction = new Dictionary<int, Func<char, int>>
            {
                { startingState, DeltaFunction },
            };

            var acceptingStates = new List<int>();
            const string informalDefinition = "Test";

            // Act
            var dfa = new DFACompiler.DFA(
                states: states,
                alphabet: alphabet,
                startingState: startingState,
                deltaFunction: deltaFunction,
                acceptingStates: acceptingStates,
                informalDefinition: informalDefinition);

            // Assert
            Assert.IsInstanceOfType(dfa, typeof(DFACompiler.DFA));
            Assert.AreEqual(states, dfa.States);
            Assert.AreEqual(startingState, dfa.StartingState);
            Assert.AreEqual(deltaFunction, dfa.DeltaFunction);
            Assert.AreEqual(acceptingStates, dfa.AcceptingStates);
            Assert.AreEqual(alphabet, dfa.Alphabet);     
            Assert.AreEqual(informalDefinition, dfa.InformalDefinition);
        }

        [TestMethod]
        public void Constructor_StatesIsNull_ExceptionThrown()
        {
            // Arrange
            var letter = 'a';
            var alphabet = new List<char>
            {
                letter
            };

            const int startingState = 1;

            var deltaFunction = new Dictionary<int, Func<char, int>>
            {
                { startingState, DeltaFunction },
            };

            var acceptingStates = new List<int>();
            const string informalDefinition = "Test";

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(
                () =>
                    new DFACompiler.DFA(
                        states: null,
                        alphabet: alphabet,
                        startingState: startingState,
                        deltaFunction: deltaFunction,
                        acceptingStates: acceptingStates,
                        informalDefinition: informalDefinition));
        }

        [TestMethod]
        public void Constructor_StatesIsEmpty_ExceptionThrown()
        {
            // Arrange
            const int state = 1;
            var states = new List<int>()
            {
                state
            };

            const int startingState = 1;

            var deltaFunction = new Dictionary<int, Func<char, int>>
            {
                { startingState, DeltaFunction },
            };

            var acceptingStates = new List<int>();
            const string informalDefinition = "Test";

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(
                () =>
                    new DFACompiler.DFA(
                        states: states,
                        alphabet: null,
                        startingState: startingState,
                        deltaFunction: deltaFunction,
                        acceptingStates: acceptingStates,
                        informalDefinition: informalDefinition));
        }

        [TestMethod]
        public void Constructor_AlphabetIsNull_ExceptionThrown()
        {
            // Arrange
            var letter = 'a';
            var alphabet = new List<char>
            {
                letter
            };

            const int startingState = 1;

            var deltaFunction = new Dictionary<int, Func<char, int>>
            {
                { startingState, DeltaFunction },
            };

            var acceptingStates = new List<int>();
            const string informalDefinition = "Test";

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(
                () =>
                    new DFACompiler.DFA(
                        states: null,
                        alphabet: alphabet,
                        startingState: startingState,
                        deltaFunction: deltaFunction,
                        acceptingStates: acceptingStates,
                        informalDefinition: informalDefinition));
        }

        [TestMethod]
        public void Constructor_DeltaFunctionIsNull_ExceptionThrown()
        {
            // Arrange
            const int state = 1;
            var states = new List<int>()
            {
                state
            };

            var letter = 'a';
            var alphabet = new List<char>
            {
                letter
            };

            const int startingState = 1;

            var acceptingStates = new List<int>();
            const string informalDefinition = "Test";

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(
                () =>
                    new DFACompiler.DFA(
                        states: states,
                        alphabet: alphabet,
                        startingState: startingState,
                        deltaFunction: null,
                        acceptingStates: acceptingStates,
                        informalDefinition: informalDefinition));
        }

        [TestMethod]
        public void Constructor_DeltaFunctionIsEmpty_ExceptionThrown()
        {
            // Arrange
            const int state = 1;
            var states = new List<int>()
            {
                state
            };

            var letter = 'a';
            var alphabet = new List<char>
            {
                letter
            };

            const int startingState = 1;

            var deltaFunction = new Dictionary<int, Func<char, int>>();

            var acceptingStates = new List<int>();
            const string informalDefinition = "Test";

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(
                () =>
                    new DFACompiler.DFA(
                        states: states,
                        alphabet: alphabet,
                        startingState: startingState,
                        deltaFunction: deltaFunction,
                        acceptingStates: acceptingStates,
                        informalDefinition: informalDefinition));
        }

        [TestMethod]
        public void Constructor_AcceptingStatesIsNull_ExceptionThrown()
        {
            // Arrange
            const int state = 1;
            var states = new List<int>()
            {
                state
            };

            var letter = 'a';
            var alphabet = new List<char>
            {
                letter
            };

            const int startingState = 1;

            var deltaFunction = new Dictionary<int, Func<char, int>>
            {
                { startingState, DeltaFunction },
            };

            const string informalDefinition = "Test";

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(
                () =>
                    new DFACompiler.DFA(
                        states: states,
                        alphabet: alphabet,
                        startingState: startingState,
                        deltaFunction: deltaFunction,
                        acceptingStates: null,
                        informalDefinition: informalDefinition));
        }

        [TestMethod]
        public void Constructor_InformalDefinitionIsNull_ExceptionThrown()
        {
            // Arrange
            const int state = 1;
            var states = new List<int>()
            {
                state
            };

            var letter = 'a';
            var alphabet = new List<char>
            {
                letter
            };

            const int startingState = 1;

            var deltaFunction = new Dictionary<int, Func<char, int>>
            {
                { startingState, DeltaFunction },
            };

            var acceptingStates = new List<int>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(
                () =>
                    new DFACompiler.DFA(
                        states: states,
                        alphabet: alphabet,
                        startingState: startingState,
                        deltaFunction: deltaFunction,
                        acceptingStates: acceptingStates,
                        informalDefinition: null));
        }

        [TestMethod]
        public void Constructor_InformalDefinitionIsEmpty_ExceptionThrown()
        {
            // Arrange
            const int state = 1;
            var states = new List<int>()
            {
                state
            };

            var letter = 'a';
            var alphabet = new List<char>
            {
                letter
            };

            const int startingState = 1;

            var deltaFunction = new Dictionary<int, Func<char, int>>
            {
                { startingState, DeltaFunction },
            };

            var acceptingStates = new List<int>();
            var informalDefinition = string.Empty;

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(
                () =>
                    new DFACompiler.DFA(
                        states: states,
                        alphabet: alphabet,
                        startingState: startingState,
                        deltaFunction: deltaFunction,
                        acceptingStates: acceptingStates,
                        informalDefinition: informalDefinition));
        }

        [TestMethod]
        public void Execute_WordIsNull_ExceptionExcepted()
        {
            // Arrange
            const int state = 1;
            var states = new List<int>()
            {
                state
            };

            var letter = 'a';
            var alphabet = new List<char>
            {
                letter
            };

            const int startingState = 1;

            var deltaFunction = new Dictionary<int, Func<char, int>>
            {
                { startingState, DeltaFunction },
            };

            var acceptingStates = new List<int>();
            const string informalDefinition = "Test";

            var dfa = new DFACompiler.DFA(
                states: states,
                alphabet: alphabet,
                startingState: startingState,
                deltaFunction: deltaFunction,
                acceptingStates: acceptingStates,
                informalDefinition: informalDefinition);

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(
                () =>
                    dfa.Execute(null));
        }

        [TestMethod]
        public void Execute_StateNotInStates_ExceptionExcepted()
        {
            // Arrange
            const int state = 1;
            var states = new List<int>()
            {
                state
            };

            var letter = 'a';
            var alphabet = new List<char>
            {
                letter
            };

            const int startingState = 2;

            var deltaFunction = new Dictionary<int, Func<char, int>>
            {
                { startingState, DeltaFunction },
            };

            var acceptingStates = new List<int>();
            const string informalDefinition = "Test";

            var dfa = new DFACompiler.DFA(
                states: states,
                alphabet: alphabet,
                startingState: startingState,
                deltaFunction: deltaFunction,
                acceptingStates: acceptingStates,
                informalDefinition: informalDefinition);

            const string word = "100";

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(
                () =>
                    dfa.Execute(word));
        }

        [TestMethod]
        public void Execute_IsEvenLengthString_EvenString_TrueReturned()
        {
            // Arrange
            const string word = "1000";

            var dfa = IsEvenLengthString();

            // Act
            var result = dfa.Execute(word);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Execute_IsEvenLengthString_EmptyString_TrueReturned()
        {
            // Arrange
            var word = string.Empty;

            var dfa = IsEvenLengthString();

            // Act
            var result = dfa.Execute(word);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Execute_IsEvenLengthString_OddString_FalseReturned()
        {
            // Arrange
            const string word = "10000";

            var dfa = IsEvenLengthString();

            // Act
            var result = dfa.Execute(word);

            // Assert
            Assert.IsFalse(result);
        }

        private static DFACompiler.DFA IsEvenLengthString()
        {
            var states = new List<int>
            {
                0,
                1
            };

            var alphabet = new List<char>
            {
                '0',
                '1'
            };

            const int startingState = 0;
            var acceptingStates = new List<int>
            {
                0
            };

            var deltaFunction = EvenLengthString_Deltas.ReturnAsDeltaFunction();

            return new DFACompiler.DFA(
                states: states,
                alphabet: alphabet,
                startingState: startingState,
                deltaFunction: deltaFunction,
                acceptingStates: acceptingStates,
                informalDefinition: "Is Even Length String");
        }

        private int DeltaFunction(char input)
        {
            return 1;
        }
    }
}
