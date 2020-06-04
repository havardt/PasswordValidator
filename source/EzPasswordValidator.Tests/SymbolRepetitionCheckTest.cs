using EzPasswordValidator.Checks;
using EzPasswordValidator.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EzPasswordValidator.Tests
{
    [TestClass]
    public class SymbolRepetitionCheckTest
    {
        private PasswordValidator _validator;
        private SymbolRepetitionCheck _check;

        [TestInitialize]
        public void Setup()
        {
            _validator = new PasswordValidator();
            _validator.AddCheck(CheckTypes.SymbolRepetition);
            _check = new SymbolRepetitionCheck();
        }

        [TestMethod]
        public void WhenPasswordContainsNoSymbolsThenPasswordIsValid()
        {
            const string psw = "test";

            Assert.IsTrue(_check.Execute(psw));
        }

        /// <summary> This test assumes that the default repetition length is 4. </summary>
        [DataRow("test///")]
        [DataRow("///test")]
        [DataRow("te///st")]
        [DataTestMethod]
        public void WhenPasswordContainsSymbolRepetitionOfLengthThreeThenPasswordIsValid(string psw) => 
            Assert.IsTrue(_check.Execute(psw));

        /// <summary> This test assumes that the default repetition length is 4. </summary>
        [DataRow("test$////")]
        [DataRow("$////test")]
        [DataRow("te////st")]
        [DataRow("test#####")]
        [DataRow("#####test")]
        [DataRow("te#####st")]
        [DataTestMethod]
        public void WhenPasswordContainsSymbolRepetitionOfLengthFourOrLongerThenPasswordIsInvalid(string psw) => 
            Assert.IsFalse(_check.Execute(psw));

        /// <summary>
        /// This test assumes that the default repetition length is 4.
        /// The goal of this test is to simply verify that the test does not
        /// wrongly compare symbols.
        /// </summary>
        [TestMethod]
        public void WhenPasswordContainsThreeDifferentSymbolsInSequenceThenPasswordIsValid()
        {
            const string psw = "test@#$";

            Assert.IsTrue(_check.Execute(psw));
        }

        [TestMethod]
        public void SymbolRepetitionDefaultsToFourWhenUsingTheValidator()
        {
            Assert.IsTrue(_validator.LetterRepetitionLength == 4);
        }

        [TestMethod]
        public void WhenUsingTheValidatorThenPasswordsWithFourSymbolRepetitionsFail()
        {
            const string pswWithFourRepetitions = "test;;;;";
            Assert.IsFalse(_validator.Validate(pswWithFourRepetitions));
        }

        [TestMethod]
        public void WhenUsingTheValidatorThenPasswordsWithLessThanFourSymbolRepetitionsPass()
        {
            const string pswWithThreeRepetitions = "test;;;";
            Assert.IsTrue(_validator.Validate(pswWithThreeRepetitions));
        }

        [TestMethod]
        public void PasswordWithFourRepetitionsAreValidWhenSettingRepetitionLengthToFive()
        {
            const string pswWithFourRepetitions = "test;;;;";
            _validator.SymbolRepetitionLength = 5;
            Assert.IsTrue(_validator.Validate(pswWithFourRepetitions));
        }
    }
}