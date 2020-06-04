using EzPasswordValidator.Checks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EzPasswordValidator.Tests
{
    [TestClass]
    public class LetterSequenceCheckTest
    {
        private LetterSequenceCheck _check;

        [TestInitialize]
        public void Setup() => _check = new LetterSequenceCheck();

        [DataRow("abcd")]
        [DataRow("testbcde")]
        [DataRow("wxyz")]
        [DataRow("efghtest")]
        [DataTestMethod]
        public void WhenPasswordContainsMostCommonThreeLetterSequencesThenPasswordIsInvalid(string psw) => 
            Assert.IsFalse(_check.Execute(psw));

        /// <summary>
        /// This test assumes that the default sequence length is 4.
        /// </summary>
        [DataRow("abcd")]
        [DataRow("testbcde")]
        [DataRow("WXYZ")]
        [DataRow("efghijkXYZ")]
        [DataTestMethod]
        public void WhenPasswordContainsFourLetterSequenceThenPasswordIsInvalid(string psw) => 
            Assert.IsFalse(_check.Execute(psw));

        /// <summary>
        /// This test sets a custom sequence length and checks that the
        /// password with a sequence of that length fails.
        /// </summary>
        [TestMethod]
        public void WhenPasswordContainsCustomLengthLetterSequenceThenPasswordIsInvalid()
        {
            const string invalidPsw = "abc";
            _check.SequenceLength = 3;
            Assert.IsFalse(_check.Execute(invalidPsw));
        }

        [TestMethod]
        public void WhenPasswordHasNoLetterSequenceThenPasswordIsValid()
        {
            const string validPsw = "valid";

            Assert.IsTrue(_check.Execute(validPsw));
        }

        [TestMethod]
        public void WhenIncrementingCharByOneThenNextChar()
        {
            const char c = 'a';
            Assert.IsTrue(((int)c + 1) == (int)'b');
        }
    }
}