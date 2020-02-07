using EzPasswordValidator.Checks;
using EzPasswordValidator.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EzPasswordValidator.Tests
{
    [TestClass]
    public class LengthCheckTest
    {
        private const int RequiredLen = 8;

        private LengthCheck _check;

        [TestInitialize]
        public void Setup() => _check = new LengthCheck(RequiredLen);

        [TestMethod]
        public void WhenPasswordIsTooShortThenPasswordIsNotValid()
        {
            const string invalidPsw = "1234567";

            Assert.IsFalse(_check.Execute(invalidPsw));
        }

        [TestMethod]
        public void WhenPasswordIsLongEnoughOnlyDueToTrailingSpaceThenPasswordIsNotValid()
        {
            const string invalidPsw = "123456  ";

            Assert.IsFalse(_check.Execute(invalidPsw));
        }

        [TestMethod]
        public void WhenPasswordIsLongEnoughOnlyDueToLeadingSpaceThenPasswordIsNotValid()
        {
            const string invalidPsw = "  123456";

            Assert.IsFalse(_check.Execute(invalidPsw));
        }

        [TestMethod]
        public void WhenPasswordIsNullThenPasswordIsNotValid()
        {
            const string invalidPsw = null;

            Assert.IsFalse(_check.Execute(invalidPsw));
        }

        [TestMethod]
        public void WhenPasswordIsEmptyThenPasswordIsNotValid()
        {
            const string invalidPsw = "             ";

            Assert.IsFalse(_check.Execute(invalidPsw));
        }

        [TestMethod]
        public void WhenPasswordIsExactlyRequiredLengthThenPasswordIsValid()
        {
            const string validPsw = "12345678";

            Assert.IsTrue(_check.Execute(validPsw));
        }

        [TestMethod]
        public void WhenPasswordIsAboveRequiredLengthThenPasswordIsValid()
        {
            const string validPsw = "123456789123456789";

            Assert.IsTrue(_check.Execute(validPsw));
        }

        [TestMethod]
        public void WhenUsingBasicCheckDefaultLengthsShouldApply()
        {
            const string validPsw = "abc123XYZ/";
            const string invalidPsw = "Ab123/"; // Fits all criteria except minimum length.

            var validator = new PasswordValidator(CheckTypes.Basic);

            Assert.AreEqual(LengthCheck.DefaultMinLength, validator.MinLength);
            Assert.AreEqual(LengthCheck.DefaultMaxLength, validator.MaxLength);
            Assert.IsTrue(validator.Validate(validPsw));
            Assert.IsFalse(validator.Validate(invalidPsw));
        }
    }
}