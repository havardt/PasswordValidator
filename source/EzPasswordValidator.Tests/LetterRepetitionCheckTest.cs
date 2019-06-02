using EzPasswordValidator.Checks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EzPasswordValidator.Tests
{
    [TestClass]
    public class LetterRepetitionCheckTest
    {

        private LetterRepetitionCheck _check;

        [TestInitialize]
        public void Setup() => _check = new LetterRepetitionCheck();

        [DataRow("testAA")]
        [DataRow("teAAst")]
        [DataRow("AAtest")]
        [DataRow("ØØtest")] //Non ISO basic latin test case
        [DataTestMethod]
        public void WhenPasswordContainsTheSameLetterTwoTimesInARowThenPasswordIsValid(string psw) => 
            Assert.IsTrue(_check.Execute(psw));

        [DataRow("testAAA")]
        [DataRow("teAAAst")]
        [DataRow("AAAtest")]
        [DataRow("ØØØtest")] //Non ISO basic latin test case
        [DataTestMethod]
        public void WhenPasswordContainsTheSameLetterThreeTimesInARowThenPasswordIsInvalid(string psw) => 
            Assert.IsFalse(_check.Execute(psw));
    }
}