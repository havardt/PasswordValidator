using EzPasswordValidator.Checks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EzPasswordValidator.Tests
{
    [TestClass]
    public class NumberRepetitionCheckTest
    {
        private NumberRepetitionCheck _check;

        [TestInitialize]
        public void Setup() => _check = new NumberRepetitionCheck();

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

        [DataRow("test22")]
        [DataRow("te22st")]
        [DataRow("224test")]
        [DataTestMethod]
        public void WhenPasswordContainsTheSameDigitTwoTimesInARowThenPasswordIsValid(string psw) =>
            Assert.IsTrue(_check.Execute(psw));

        [DataRow("test222")]
        [DataRow("te222st")]
        [DataRow("222test")]
        [DataRow("37222test")]
        [DataTestMethod]
        public void WhenPasswordContainsTheSameDigitThreeTimesInARowThenPasswordIsInvalid(string psw) =>
            Assert.IsFalse(_check.Execute(psw));
    }
}