using EzPasswordValidator.Checks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EzPasswordValidator.Tests
{
    [TestClass]
    public class LetterCheckTest
    {
        private LetterCheck _check;

        [TestInitialize]
        public void Setup() => _check = new LetterCheck();

        [DataRow("A12345")]
        [DataRow("12345A")]
        [DataRow("123A45")]
        [DataTestMethod]
        public void WhenPasswordContainsExactlyOneLetterThenPasswordIsValid(string validPsw) => 
            Assert.IsTrue(_check.Execute(validPsw));

        [DataRow("ABC")]
        [DataRow("bbb")]
        [DataRow("aBc")]
        [DataTestMethod]
        public void WhenPasswordContainsOnlyLettersThenPasswordIsValid(string validPsw) =>
            Assert.IsTrue(_check.Execute(validPsw));

        [DataRow("123456")]
        [DataRow("///23))")]
        [DataTestMethod]
        public void WhenPasswordContainsNoLettersThenPasswordIsInvalid(string invalidPsw) =>
            Assert.IsFalse(_check.Execute(invalidPsw));
    }
}