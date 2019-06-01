using EzPasswordValidator.Checks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EzPasswordValidator.Tests
{
    [TestClass]
    public class SymbolCheckTest
    {
        private SymbolCheck _check;

        [TestInitialize]
        public void Setup() => _check = new SymbolCheck();

        [TestMethod]
        public void WhenPasswordContainsNoSymbolsThenPasswordIsInvalid()
        {
            const string psw = "test123";

            Assert.IsFalse(_check.Execute(psw));
        }

        [DataRow("test@")]
        [DataRow("@test")]
        [DataRow("te@st")]
        [DataRow("test#")]
        [DataRow("#test")]
        [DataRow("te#st")]
        [DataTestMethod]
        public void WhenPasswordContainsExactlyOneSymbolThenPasswordIsValid(string psw) =>
            Assert.IsTrue(_check.Execute(psw));

        [DataRow("@@@@@")]
        [DataRow("#####")]
        [DataRow("%%%%%")]
        [DataRow("%&^!@)((*;'")]
        [DataTestMethod]
        public void WhenPasswordContainsOnlySymbolsThenPasswordIsValid(string psw) =>
            Assert.IsTrue(_check.Execute(psw));
    }
}