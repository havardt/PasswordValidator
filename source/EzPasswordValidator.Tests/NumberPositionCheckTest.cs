using EzPasswordValidator.Checks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EzPasswordValidator.Tests
{
    [TestClass]
    public class NumberPositionCheckTest
    {
        private NumberPositionCheck _check;

        [TestInitialize]
        public void Setup() => _check = new NumberPositionCheck();

        [TestMethod]
        public void WhenPasswordContainsNoNumbersThenPasswordIsInvalid()
        {
            const string invalidPsw = "test";

            Assert.IsFalse(_check.Execute(invalidPsw));
        }

        [TestMethod]
        public void WhenPasswordOnlyHasNumbersBehindThenPasswordIsInvalid()
        {
            const string invalidPsw = "test123";

            Assert.IsFalse(_check.Execute(invalidPsw));
        }

        [TestMethod]
        public void WhenPasswordOnlyHasNumbersInFrontThenPasswordIsInvalid()
        {
            const string invalidPsw = "123test";

            Assert.IsFalse(_check.Execute(invalidPsw));
        }

        [TestMethod]
        public void WhenPasswordHasNumbersInFrontAndBackOnlyThenPasswordIsInvalid()
        {
            const string invalidPsw = "123test123";

            Assert.IsFalse(_check.Execute(invalidPsw));
        }

        [TestMethod]
        public void WhenPasswordHasNumbersNotOnlyInFrontOrBackThenPasswordIsValid()
        {
            const string validPsw = "te123st";

            Assert.IsTrue(_check.Execute(validPsw));
        }
    }
}