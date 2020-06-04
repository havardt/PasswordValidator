using EzPasswordValidator.Checks;
using EzPasswordValidator.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EzPasswordValidator.Tests
{
    [TestClass]
    public class NumberSequenceCheckTest
    {
        private PasswordValidator _validator;
        private NumberSequenceCheck _check;

        [TestInitialize]
        public void Setup()
        {
            _validator = new PasswordValidator();
            _validator.AddCheck(CheckTypes.NumberSequence);
            _check = new NumberSequenceCheck();
        }

        [TestMethod]
        public void WhenPasswordContainsNoNumbersThenPasswordIsValid()
        {
            const string psw = "test";

            Assert.IsTrue(_check.Execute(psw));
        }

        [DataRow("123test")]
        [DataRow("te123st")]
        [DataRow("test123")]
        [DataTestMethod]
        public void WhenPasswordContainsAThreeDigitNumberSequenceThenPasswordIsValid(string psw) => 
            Assert.IsTrue(_check.Execute(psw));

        [DataRow("2345test")]
        [DataRow("te2345st")]
        [DataRow("test2345")]
        [DataTestMethod]
        public void WhenPasswordContainsAFourDigitNumberSequenceThenPasswordIsInvalid(string psw) =>
            Assert.IsFalse(_check.Execute(psw));

        [DataRow("6543test")]
        [DataRow("te6543st")]
        [DataRow("test6543")]
        [DataTestMethod]
        public void WhenPasswordContainsAFourDigitReverseNumberSequenceThenPasswordIsInvalid(string psw) =>
            Assert.IsFalse(_check.Execute(psw));

        [TestMethod]
        public void DefaultLengthIsFour() =>
            Assert.IsTrue(_validator.NumberSequenceLength == 4);

        [DataRow("6543test")]
        [DataRow("te6543st")]
        [DataRow("test1234")]
        [DataRow("test")]
        [DataTestMethod]
        public void ChangingSequenceLengthToFiveAllowsSequencesOfLowerLength(string psw)
        {
            _validator.NumberSequenceLength = 5;
            Assert.IsTrue(_validator.Validate(psw));
        }

        [DataRow("te98765st")]
        [DataRow("test12345")]
        [DataTestMethod]
        public void ChangingSequenceLengthToFiveDisallowsSequencesOfLengthFive(string psw)
        {
            _validator.NumberSequenceLength = 5;
            Assert.IsFalse(_validator.Validate(psw));
        }
    }
}