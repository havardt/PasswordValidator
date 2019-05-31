using System;
using EzPasswordValidator.Checks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EzPasswordValidator.Tests
{
    [TestClass]
    public class NumberCheckTest
    {

        private NumberCheck _check;

        [TestInitialize]
        public void Setup() => _check = new NumberCheck();

        [DataRow("test0")]
        [DataRow("0test")]
        [DataRow("te0st")]
        [DataRow("test1")]
        [DataRow("1test")]
        [DataRow("te1st")]
        [DataRow("test2")]
        [DataRow("2test")]
        [DataRow("te2st")]
        [DataRow("test3")]
        [DataRow("3test")]
        [DataRow("te3st")]
        [DataRow("test4")]
        [DataRow("4test")]
        [DataRow("te4st")]
        [DataRow("test5")]
        [DataRow("5test")]
        [DataRow("te5st")]
        [DataRow("test6")]
        [DataRow("6test")]
        [DataRow("te6st")]
        [DataRow("test7")]
        [DataRow("7test")]
        [DataRow("te7st")]
        [DataRow("test8")]
        [DataRow("8test")]
        [DataRow("te8st")]
        [DataRow("test9")]
        [DataRow("9test")]
        [DataRow("te9st")]
        [DataTestMethod]
        public void WhenPasswordContainsExactlyOneDigitThenPasswordIsValid(string psw) => 
            Assert.IsTrue(_check.Execute(psw));

        [DataRow("2147000000")]
        [DataRow("0")]
        [DataRow("123")]
        [DataTestMethod]
        public void WhenPasswordContainsOnlyDigitsThenPasswordIsValid(string psw) =>
            Assert.IsTrue(_check.Execute(psw));

        [TestMethod]
        public void WhenPasswordDoesNotContainADigitThenPasswordIsInvalid()
        {
            const string invalidPsw = "test";

            Assert.IsFalse(_check.Execute(invalidPsw));
        }
    }
}