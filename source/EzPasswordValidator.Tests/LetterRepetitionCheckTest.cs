using EzPasswordValidator.Checks;
using EzPasswordValidator.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EzPasswordValidator.Tests
{
    [TestClass]
    public class LetterRepetitionCheckTest
    {

        private PasswordValidator _validator;
        private LetterRepetitionCheck _check;

        [TestInitialize]
        public void Setup()
        {
            _validator = new PasswordValidator();
            _validator.AddCheck(CheckTypes.LetterRepetition);
            _check = new LetterRepetitionCheck();
        }

        /// <summary> This test assumes that the default repetition length is 4. </summary>
        [DataRow("testAAA")]
        [DataRow("teAAAst")]
        [DataRow("AAAtest")]
        [DataRow("ØØØtest")] //Non ISO basic latin test case
        [DataTestMethod]
        public void WhenPasswordContainsTheSameLetterThreeTimesInARowThenPasswordIsValid(string psw) => 
            Assert.IsTrue(_check.Execute(psw));

        /// <summary> This test assumes that the default repetition length is 4. </summary>
        [DataRow("testAAAA")]
        [DataRow("teAAAAAst")]
        [DataRow("AAAAtesBBBt")]
        [DataRow("ØØØØtest")] //Non ISO basic latin test case
        [DataTestMethod]
        public void WhenPasswordContainsTheSameLetterFourOrMoreTimesInARowThenPasswordIsInvalid(string psw) => 
            Assert.IsFalse(_check.Execute(psw));

        [DataRow("testAAAAA")]
        [DataRow("teYYYYYst")]
        [DataRow("TTTTTTTTtest")]
        [DataRow("ÅÅÅÅÅtest")] //Non ISO basic latin test case
        [DataTestMethod]
        public void WhenPasswordContainsRepetitionOfFiveOrLongerWhenSetToFiveThenPasswordIsInvalid(string psw)
        {
            _check.RepetitionLength = 5;
            Assert.IsFalse(_check.Execute(psw));
        }

        [DataRow("testAAAA")]
        [DataRow("teYst")]
        [DataRow("TTTTtest")]
        [DataRow("ÅÅtest")] //Non ISO basic latin test case
        [DataTestMethod]
        public void WhenPasswordContainsRepetitionOfLessThanFiveCharsWhenSetToFiveThenPasswordIsValid(string psw)
        {
            _check.RepetitionLength = 5;
            Assert.IsTrue(_check.Execute(psw));
        }

        [TestMethod]
        public void LetterRepetitionDefaultsToFourWhenUsingTheValidator()
        {
            Assert.IsTrue(_validator.LetterRepetitionLength == 4);
        }

        [TestMethod]
        public void WhenUsingTheValidatorThenPasswordsWithFourLetterRepetitionsFail()
        {
            const string pswWithFourRepetitions = "testGGGG";
            Assert.IsFalse(_validator.Validate(pswWithFourRepetitions));
        }

        [TestMethod]
        public void WhenUsingTheValidatorThenPasswordsWithLessThanFourLetterRepetitionsPass()
        {
            const string pswWithThreeRepetitions = "testGGG";
            Assert.IsTrue(_validator.Validate(pswWithThreeRepetitions));
        }
    }
}