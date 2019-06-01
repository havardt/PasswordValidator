using EzPasswordValidator.Checks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EzPasswordValidator.Tests
{
    [TestClass]
    public class NumberSequenceCheckTest
    {
        private NumberSequenceCheck _check;

        [TestInitialize]
        public void Setup() => _check = new NumberSequenceCheck();

        [TestMethod]
        public void WhenPasswordContainsNoNumbersThenPasswordIsValid()
        {
            const string psw = "test";

            Assert.IsTrue(_check.Execute(psw));
        }

        [DataRow("12test")]
        [DataRow("te12st")]
        [DataRow("test12")]
        [DataTestMethod]
        public void WhenPasswordContainsATwoDigitNumberSequenceThenPasswordIsValid(string psw) => 
            Assert.IsTrue(_check.Execute(psw));

        [DataRow("234test")]
        [DataRow("te234st")]
        [DataRow("test234")]
        [DataTestMethod]
        public void WhenPasswordContainsAThreeDigitNumberSequenceThenPasswordIsInvalid(string psw) =>
            Assert.IsFalse(_check.Execute(psw));

        [DataRow("654test")]
        [DataRow("te654st")]
        [DataRow("test654")]
        [DataTestMethod]
        public void WhenPasswordContainsAThreeDigitReverseNumberSequenceThenPasswordIsInvalid(string psw) =>
            Assert.IsFalse(_check.Execute(psw));
    }
}