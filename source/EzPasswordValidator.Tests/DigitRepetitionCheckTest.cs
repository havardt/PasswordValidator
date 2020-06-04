using EzPasswordValidator.Checks;
using EzPasswordValidator.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EzPasswordValidator.Tests
{
    [TestClass]
    public class DigitRepetitionCheckTest
    {
        private PasswordValidator _validator;
        private DigitRepetitionCheck _check;

        [TestInitialize]
        public void Setup()
        {
            _validator = new PasswordValidator();
            _validator.AddCheck(CheckTypes.DigitRepetition);
            _check = new DigitRepetitionCheck();
        }

        /// <summary>
        ///     Whens the password contains no numbers then password is valid
        ///     as no number repetition can occur if there are no numbers.
        /// </summary>
        [TestMethod]
        public void WhenPasswordContainsNoNumbersThenPasswordIsValid()
        {
            const string psw = "test";

            Assert.IsTrue(_check.Execute(psw));
        }

        /// <summary> This test assumes that the default repetition length is 4. </summary>
        [DataRow("test222")]
        [DataRow("te222st")]
        [DataRow("2224test")]
        [DataTestMethod]
        public void WhenPasswordContainsTheSameDigitThreeTimesInARowThenPasswordIsValid(string psw) =>
            Assert.IsTrue(_check.Execute(psw));

        /// <summary> This test assumes that the default repetition length is 4. </summary>
        [DataRow("test2222")]
        [DataRow("te2222st")]
        [DataRow("2222test")]
        [DataRow("372222test")]
        [DataTestMethod]
        public void WhenPasswordContainsTheSameDigitFourTimesInARowThenPasswordIsInvalid(string psw) =>
            Assert.IsFalse(_check.Execute(psw));

        [DataRow("test33333")]
        [DataRow("te11111st")]
        [DataRow("6666666test")]
        [DataTestMethod]
        public void WhenPasswordContainsRepetitionOfFiveOrLongerWhenSetToFiveThenPasswordIsInvalid(string psw)
        {
            _check.RepetitionLength = 5;
            Assert.IsFalse(_check.Execute(psw));
        }

        [DataRow("test5555")]
        [DataRow("te5st")]
        [DataRow("5555test")]
        [DataTestMethod]
        public void WhenPasswordContainsRepetitionOfLessThanFiveDigitsWhenSetToFiveThenPasswordIsValid(string psw)
        {
            _check.RepetitionLength = 5;
            Assert.IsTrue(_check.Execute(psw));
        }

        [TestMethod]
        public void PasswordWithFourRepetitionsAreValidWhenSettingRepetitionLengthToFive()
        {
            const string pswWithFourRepetitions = "test4444";
            _validator.DigitRepetitionLength = 5;
            Assert.IsTrue(_validator.Validate(pswWithFourRepetitions));
        }

        [TestMethod]
        public void DigitRepetitionDefaultsToFourWhenUsingTheValidator()
        {
            Assert.IsTrue(_validator.DigitRepetitionLength == 4);
        }

        [TestMethod]
        public void WhenUsingTheValidatorThenPasswordsWithFourDigitRepetitionsFail()
        {
            const string pswWithFourRepetitions = "test4444";
            Assert.IsFalse(_validator.Validate(pswWithFourRepetitions));
        }

        [TestMethod]
        public void WhenUsingTheDefualtValidatorThenPasswordsWithLessThanFourDigitRepetitionsPass()
        {
            const string pswWithThreeRepetitions = "test444";
            Assert.IsTrue(_validator.Validate(pswWithThreeRepetitions));
        }

    }
}