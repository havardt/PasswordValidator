using EzPasswordValidator.Checks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EzPasswordValidator.Tests
{
    [TestClass]
    public class CaseCheckTest
    {

        private CaseCheck _check;

        [TestInitialize]
        public void Setup() => _check = new CaseCheck();

        /// <summary>The case check should support english alphabet characters.</summary>
        [DataRow("aaBB")]
        [DataRow("CCdd")]
        [DataRow("aBc")]
        [DataRow("AbC")]
        [DataTestMethod]
        public void WhenPasswordHasUpperAndLowerEnglishCharsThenPasswordIsValid(string validPsw) => 
            Assert.IsTrue(_check.Execute(validPsw));

        /// <summary>The case check should support non-english alphabet characters.</summary>
        [DataRow("øøÆÆ")]
        [DataRow("ÅÅøø")]
        [DataRow("æØæ")]
        [DataRow("ÆøÅ")]
        [DataTestMethod]
        public void WhenPasswordHasUpperAndLowerNonEnglishCharsThenPasswordIsValid(string validPsw) =>
            Assert.IsTrue(_check.Execute(validPsw));

        [TestMethod]
        public void WhenPasswordHasOnlyUpperCaseCharsThenPasswordIsNotValid()
        {
            const string invalidPsw = "AAA";

            Assert.IsFalse(_check.Execute(invalidPsw));
        }

        [TestMethod]
        public void WhenPasswordHasOnlyLowerCaseCharsThenPasswordIsNotValid()
        {
            const string invalidPsw = "aaa";

            Assert.IsFalse(_check.Execute(invalidPsw));
        }
    }
}