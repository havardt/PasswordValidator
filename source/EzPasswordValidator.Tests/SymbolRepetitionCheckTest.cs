using EzPasswordValidator.Checks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EzPasswordValidator.Tests
{
    [TestClass]
    public class SymbolRepetitionCheckTest
    {
        private SymbolRepetitionCheck _check;

        [TestInitialize]
        public void Setup() => _check = new SymbolRepetitionCheck();

        [TestMethod]
        public void WhenPasswordContainsNoSymbolsThenPasswordIsValid()
        {
            const string psw = "test";

            Assert.IsTrue(_check.Execute(psw));
        }

        [DataRow("test//")]
        [DataRow("//test")]
        [DataRow("te//st")]
        [DataTestMethod]
        public void WhenPasswordContainsSymbolRepetitionOfLengthTwoThenPasswordIsValid(string psw) => 
            Assert.IsTrue(_check.Execute(psw));

        [DataRow("test///")]
        [DataRow("///test")]
        [DataRow("te///st")]
        [DataRow("test#####")]
        [DataRow("#####test")]
        [DataRow("te#####st")]
        [DataTestMethod]
        public void WhenPasswordContainsSymbolRepetitionOfLengthThreeOrLongerThenPasswordIsValid(string psw) => 
            Assert.IsFalse(_check.Execute(psw));
    }
}