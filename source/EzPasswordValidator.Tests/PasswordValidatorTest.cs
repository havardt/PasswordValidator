﻿using System;
using System.Diagnostics;
using System.Linq;
using EzPasswordValidator.Checks;
using EzPasswordValidator.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EzPasswordValidator.Tests
{
    [TestClass]
    public class PasswordValidatorTest
    {
        private const CheckTypes OutOfRangeCheckType = (CheckTypes) (1 << 11);

        private PasswordValidator _validator;

        [TestInitialize]
        public void Setup() => _validator = new PasswordValidator();

        [TestMethod]
        public void WhenValidatorHasNoChecksThenPasswordIsValid() => 
            Assert.IsTrue(_validator.Validate("password"));

        [TestMethod]
        public void WhenAddingNonExistingCheckTypeThenExceptionIsThrown() => 
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _validator.AddCheck(OutOfRangeCheckType));

        [TestMethod]
        public void WhenAddingNonExistingCheckTypeToConstructorThenExceptionIsThrown() => 
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new PasswordValidator(OutOfRangeCheckType));

        [TestMethod]
        public void WhenAddingCustomCheckWithAnExistingTagThenCheckIsNotAdded()
        {
            const string tag = "tag";

            _validator.AddCheck(tag, (_) => false);
            _validator.AddCheck(tag, (_) => true);

            Assert.IsFalse(_validator.Validate("password"));
        }

        [TestMethod]
        public void WhenAddingCheckByTypeThenCheckIsAdded()
        {
            _validator.AddCheck(CheckTypes.LetterSequence);

            Check addedCheck = _validator.AllChecks.FirstOrDefault(_ => _.Type == CheckTypes.LetterSequence);

            Assert.IsNotNull(addedCheck);
        }

        [TestMethod]
        public void WhenRemovingNonExistingCheckTypeThenExceptionIsThrown() =>
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _validator.RemoveCheck(OutOfRangeCheckType));

        [TestMethod]
        public void WhenRemovingByTagThenCheckIsRemoved()
        {
            const string tag = "tag";

            _validator.AddCheck(tag, (_) => true);

            int before = _validator.AllChecks.Count();

            _validator.RemoveCheck(tag);

            int after = _validator.AllChecks.Count();

            Assert.AreEqual(after, before - 1);
        }

        [TestMethod]
        public void WhenLengthCheckIsAddedAndUserChangesRequiredLengthThenLengthCheckIsUpdated()
        {
            _validator.AddCheck(CheckTypes.Length);

            var checkBefore = (LengthCheck) _validator.AllChecks.FirstOrDefault(_ => _.Type == CheckTypes.Length);
            Debug.Assert(checkBefore != null, nameof(checkBefore) + " != null");
            uint minLenBefore = checkBefore.MinLength;
            uint maxLenBefore = checkBefore.MaxLength;

            _validator.SetLengthBounds(minLenBefore + 1, maxLenBefore + 1);

            var checkAfter = (LengthCheck)_validator.AllChecks.FirstOrDefault(_ => _.Type == CheckTypes.Length);
            Debug.Assert(checkAfter != null, nameof(checkAfter) + " != null");
            uint minLenAfter = checkBefore.MinLength;
            uint maxLenAfter = checkBefore.MaxLength;

            Assert.AreNotEqual(minLenBefore, minLenAfter);
            Assert.AreNotEqual(maxLenBefore, maxLenAfter);
        }

        [TestMethod]
        public void WhenValidatingWith50PercentOnTwoChecksOnlyOneShouldBeRequired()
        {
            const string psw = "test";
            _validator.AddCheck(CheckTypes.Letters | CheckTypes.Numbers);
            Assert.IsTrue(_validator.Validate(psw, 0.5));
        }
    }
}