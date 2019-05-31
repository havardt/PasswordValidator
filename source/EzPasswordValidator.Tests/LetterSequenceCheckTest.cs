using EzPasswordValidator.Checks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EzPasswordValidator.Tests
{
    [TestClass]
    public class LetterSequenceCheckTest
    {
        private LetterSequenceCheck _check;

        [TestInitialize]
        public void Setup() => _check = new LetterSequenceCheck();

        [DataRow("abc")]
        [DataRow("testabc")]
        [DataRow("xyz")]
        [DataRow("xyztest")]
        [DataTestMethod]
        public void WhenPasswordContainsMostCommonThreeLetterSequencesThenPasswordIsInvalid(string psw) => 
            Assert.IsFalse(_check.Execute(psw));

        [TestMethod]
        public void WhenPasswordContainsFourLetterSequenceThenPasswordIsInvalid()
        {
            const string invalidPsw = "defg";

            Assert.IsFalse(_check.Execute(invalidPsw));
        }

        [TestMethod]
        public void WhenPasswordHasNoLetterSequenceThenPasswordIsValid()
        {
            const string validPsw = "valid";

            Assert.IsTrue(_check.Execute(validPsw));
        }
    }
}