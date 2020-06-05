using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EzPasswordValidator.Checks;

namespace EzPasswordValidator.Validators
{
    /// <inheritdoc />
    /// <summary>
    /// A class for validating passwords.
    /// </summary>
    /// <example>
    /// Example usage:
    /// <code>
    /// var validator = new PasswordValidator(CheckTypes.Basic);
    /// bool isValid = validator.Validate(password);
    /// if (isValid)
    /// {
    ///     
    /// }
    /// else
    /// {
    ///     foreach (Check failedCheck in validator.FailedChecks)
    ///     {
    ///         
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="IValidator{T}" />
    public class PasswordValidator : IValidator<string>
    {
        private readonly Dictionary<CheckTypes, Check> _predefinedChecks = new Dictionary<CheckTypes, Check>();
        private readonly Dictionary<string, Check> _customChecks = new Dictionary<string, Check>();
        private uint _minLength = LengthCheck.DefaultMinLength;
        private uint _maxLength = LengthCheck.DefaultMaxLength;
        private int _letterSequenceLength = LetterSequenceCheck.DefaultSequenceLength;
        private int _letterRepetitionLength = LetterRepetitionCheck.DefaultRepetitionLength;
        private int _symbolRepetitionLength = SymbolRepetitionCheck.DefaultRepetitionLength;
        private int _digitRepetitionLength = DigitRepetitionCheck.DefaultRepetitionLength;
        private int _numberSequenceLength = NumberSequenceCheck.DefaultSequenceLength;

        /// <inheritdoc />
        public PasswordValidator()
            : this(CheckTypes.None)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordValidator"/> class.
        /// </summary>
        /// <param name="checkTypes">Initial check types.</param>
        public PasswordValidator(CheckTypes checkTypes)
        {
            AddCheck(checkTypes);
        }

        /// <summary>
        /// Gets the active check types.
        /// </summary>
        /// <value>
        /// All check flags that have been set in this validation instance.
        /// </value>
        public CheckTypes CheckTypes { get; private set; }

        /// <summary>
        /// Gets or sets the minimum required length of the password,
        /// <see cref="LengthCheck.DefaultMinLength"/> for the default length.
        /// </summary>
        /// <value>
        /// A <see cref="UInt32"/> positive number.
        /// </value>
        public uint MinLength
        {
            get => _minLength;
            set
            {
                _minLength = value;
                if (_predefinedChecks.TryGetValue(CheckTypes.Length, out Check check))
                {
                    if (check is LengthCheck lengthCheck)
                    {
                        lengthCheck.MinLength = value;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the maximum allowed length of the password,
        /// <see cref="LengthCheck.DefaultMaxLength"/> for the default length.
        /// </summary>
        /// <value>
        /// A <see cref="UInt32"/> positive number.
        /// </value>
        public uint MaxLength
        {
            get => _maxLength;
            set
            {
                _maxLength = value;
                if (_predefinedChecks.TryGetValue(CheckTypes.Length, out Check check))
                {
                    if (check is LengthCheck lengthCheck)
                    {
                        lengthCheck.MaxLength = value;
                    }
                }
            }
        }

        /// <summary>
        /// The amount of digits that must be in a number sequence for
        /// the <see cref="NumberSequenceCheck"/> test to fail.
        /// Eg. if set to 5, then a password containing a number sequence like '12345'
        /// fails validation (if the check is active).
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the set sequence length is less than or equal to 1.
        /// </exception>
        public int NumberSequenceLength
        {
            get => _numberSequenceLength;
            set
            {
                if (value <= 1)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(NumberSequenceLength),
                        "The sequence length cannot be less than or equal to 1.");
                }

                _numberSequenceLength = value;
                if (_predefinedChecks.TryGetValue(CheckTypes.NumberSequence, out Check check))
                {
                    if (check is NumberSequenceCheck numberSequenceCheck)
                    {
                        numberSequenceCheck.SequenceLength = value;
                    }
                }
            }
        }

        /// <summary>
        /// The amount of characters that must be in a letter sequence for
        /// the <see cref="LetterSequenceCheck"/> test to fail.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the set sequence length is less than or equal to 1.
        /// </exception>
        public int LetterSequenceLength
        {
            get => _letterSequenceLength;
            set
            {
                if (value <= 1)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(LetterSequenceLength),
                        "The sequence length cannot be less than or equal to 1.");
                }

                _letterSequenceLength = value;
                if (_predefinedChecks.TryGetValue(CheckTypes.LetterSequence, out Check check))
                {
                    if (check is LetterSequenceCheck letterSequenceCheck)
                    {
                        letterSequenceCheck.SequenceLength = value;
                    }
                }
            }
        }

        /// <summary>
        /// The amount of characters that must be repeated for
        /// the <see cref="LetterRepetitionCheck"/> test to fail.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the set value is less than or equal to 1.
        /// </exception>
        public int LetterRepetitionLength
        {
            get => _letterRepetitionLength;
            set
            {
                if (value <= 1)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(LetterRepetitionLength),
                        "The letter repetition length cannot be less than or equal to 1.");
                }

                _letterRepetitionLength = value;
                if (_predefinedChecks.TryGetValue(CheckTypes.LetterRepetition, out Check check))
                {
                    if (check is LetterRepetitionCheck letterRepetitionCheck)
                    {
                        letterRepetitionCheck.RepetitionLength = value;
                    }
                }
            }
        }


        /// <summary>
        /// The amount of symbols that must be repeated for
        /// the <see cref="SymbolRepetitionCheck"/> test to fail.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the set value is less than or equal to 1.
        /// </exception>
        public int SymbolRepetitionLength
        {
            get => _symbolRepetitionLength;
            set
            {
                if (value <= 1)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(SymbolRepetitionLength),
                        "The symbol repetition length cannot be less than or equal to 1.");
                }

                _symbolRepetitionLength = value;
                if (_predefinedChecks.TryGetValue(CheckTypes.SymbolRepetition, out Check check))
                {
                    if (check is SymbolRepetitionCheck symbolRepetitionCheck)
                    {
                        symbolRepetitionCheck.RepetitionLength = value;
                    }
                }
            }
        }

        /// <summary>
        /// The amount of digits that must be repeated for
        /// the <see cref="DigitRepetitionCheck"/> test to fail.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the set value is less than or equal to 1.
        /// </exception>
        public int DigitRepetitionLength
        {
            get => _digitRepetitionLength;
            set
            {
                if (value <= 1)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(DigitRepetitionLength),
                        "The digit repetition length cannot be less than or equal to 1.");
                }

                _digitRepetitionLength = value;
                if (_predefinedChecks.TryGetValue(CheckTypes.DigitRepetition, out Check check))
                {
                    if (check is DigitRepetitionCheck digitRepetitionCheck)
                    {
                        digitRepetitionCheck.RepetitionLength = value;
                    }
                }
            }
        }

        /// <summary>
        /// Gets all checks; predefined and custom.
        /// </summary>
        /// <returns>An <see cref="System.Collections.Generic.IEnumerable{T}" /> that contains all checks.</returns>
        public IEnumerable<Check> AllChecks => _predefinedChecks.Values.Concat(_customChecks.Values);

        /// <summary>
        /// Gets all checks that failed validation.
        /// </summary>
        /// <returns>An <see cref="System.Collections.Generic.IEnumerable{T}" /> that contains all failed checks.</returns>
        public IEnumerable<Check> FailedChecks => AllChecks.Where(check => check.Result == false);

        /// <summary>
        /// Gets all checks that passed validation.
        /// </summary>
        /// <returns>An <see cref="System.Collections.Generic.IEnumerable{T}" /> that contains all passed checks.</returns>
        public IEnumerable<Check> PassedChecks => AllChecks.Where(check => check.Result);

        /// <inheritdoc />
        /// <summary>
        /// Validates the specified password.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <returns>
        ///   <c>true</c> if the password passes all checks; if no checks have been added
        ///   then password is considered valid and <c>true</c> will be returned.
        ///   If a check fails the password is invalid and <c>false</c> is returned.
        /// </returns>
        public bool Validate(string password)
        {
            var result = true;
            foreach (Check check in AllChecks)
            {
                if (check.Execute(password) == false)
                {
                    result = false;
                }
            }
            return result;
        }

        /// <inheritdoc />
        /// <summary>
        /// Validates the specified password.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <param name="p">
        ///   The % of checks that need to pass for the password to be valid.
        ///   This is a value between 0 and 1. Eg. 0.5 means that 50% of the checks need to pass.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the password passes all checks; if no checks have been added
        ///   then password is considered valid and <c>true</c> will be returned.
        ///   If a check fails the password is invalid and <c>false</c> is returned.
        /// </returns>
        public bool Validate(string password, double p)
        {
            bool isValid = Validate(password);
            if (isValid)
            {
                return true;
            }
            double pActual = (double)PassedChecks.Count() / AllChecks.Count();
            return (pActual + double.Epsilon) >= p;
        }

        /// <inheritdoc />
        /// <summary>
        /// Validates the specified password.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <param name="minPassedChecks">
        ///   The minimum number of checks that need to pass for the password to be valid.
        ///   Eg. a value of 3 means that at least 3 checks must pass.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the password passes at least the given minimum amount of checks;
        ///   if no checks have been added, then the password is considered valid and <c>true</c>
        ///   will be returned. If a check fails the password is invalid and <c>false</c> is returned.
        /// </returns>
        public bool Validate(string password, int minPassedChecks) => 
            Validate(password) || PassedChecks.Count() >= minPassedChecks;

        /// <summary>
        /// Validates the specified password asynchronous. This method should be used
        /// when custom checks include long running operations.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <returns>
        ///   <c>true</c> if the password passes all checks; <c>false</c> otherwise.
        /// </returns>
        public async Task<bool> ValidateAsync(string password) =>
            await Task.Run(() => Validate(password));

        /// <summary>
        /// Validates the specified password.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <param name="p">
        ///   The % of checks that need to pass for the password to be valid.
        ///   This is a value between 0 and 1. Eg. 0.5 means that 50% of the checks need to pass.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the password passes all checks; if no checks have been added
        ///   then password is considered valid and <c>true</c> will be returned.
        ///   If a check fails the password is invalid and <c>false</c> is returned.
        /// </returns>
        public async Task<bool> ValidateAsync(string password, double p) =>
            await Task.Run(() => Validate(password, p));

        /// <summary>
        /// Validates the specified password.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <param name="minPassedChecks">
        ///   The minimum number of checks that need to pass for the password to be valid.
        ///   Eg. a value of 3 means that at least 3 checks must pass.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the password passes all checks; if no checks have been added
        ///   then password is considered valid and <c>true</c> will be returned.
        ///   If a check fails the password is invalid and <c>false</c> is returned.
        /// </returns>
        public async Task<bool> ValidateAsync(string password, int minPassedChecks) =>
            await Task.Run(() => Validate(password, minPassedChecks));

        /// <summary>
        /// Convenience method that adds the check types represented by the Int32 value.
        /// </summary>
        /// <param name="checkTypes">An <see cref="Int32"/> value representing the check types to add.</param>
        /// <exception cref="ArgumentOutOfRangeException">The given value does not represent any <see cref="CheckTypes"/>.</exception>
        public void AddCheck(int checkTypes) => AddCheck((CheckTypes)checkTypes);

        /// <summary>
        /// Adds the checks represented by the given flags.
        /// </summary>
        /// <param name="checkTypes">The check types to add.</param>
        /// <exception cref="ArgumentOutOfRangeException">The given value does not represent any <see cref="CheckTypes"/>.</exception>
        public void AddCheck(CheckTypes checkTypes)
        {
            if (!checkTypes.IsInRange())
            {
                throw new ArgumentOutOfRangeException($"No checks within the given value: {checkTypes}");
            }
            CheckTypes |= checkTypes;
            SafeAddChecks(checkTypes);
        }

        /// <summary>
        /// Adds the custom check.
        /// </summary>
        /// <param name="check">The check to be added.</param>
        public void AddCheck(CustomCheck check)
        {
            if (check == null) return;

            if (!_customChecks.ContainsKey(check.Tag))
            {
                _customChecks.Add(check.Tag, check);
            }
        }

        /// <summary>
        /// Adds the custom check if a check with the given tag does not already exist.
        /// </summary>
        /// <param name="tag">
        ///   Unique string identifier.
        /// </param>
        /// <param name="check">
        ///   A custom password check taking the password as a parameter and returning
        ///   a <c>bool</c> where <c>true</c> represents a passed check and <c>false</c>a failed check.
        /// </param>
        public void AddCheck(string tag, Func<string, bool> check) => AddCheck(new SimpleCheck(tag, check));

        /// <summary>
        /// Removes the checks represented by the given flags.
        /// </summary>
        /// <param name="checkTypes">The check types to remove.</param>
        /// <exception cref="ArgumentOutOfRangeException">The given value does not represent any <see cref="CheckTypes"/>.</exception>
        public void RemoveCheck(CheckTypes checkTypes)
        {
            if (!checkTypes.IsInRange())
            {
                throw new ArgumentOutOfRangeException($"No checks within the given value: {checkTypes}");
            }
            CheckTypes ^= checkTypes;
            SafeRemoveChecks(checkTypes);
        }

        /// <summary>
        /// Convenience method that removes the checks represented by the given flags.
        /// </summary>
        /// <param name="checkTypes">An <see cref="Int32"/> value representing check types.</param>
        /// <exception cref="ArgumentOutOfRangeException">The given value does not represent any <see cref="CheckTypes"/>.</exception>
        public void RemoveCheck(int checkTypes) => RemoveCheck((CheckTypes)checkTypes);

        /// <summary>
        /// Removes the custom check.
        /// </summary>
        /// <param name="check">The check to remove.</param>
        public void RemoveCheck(CustomCheck check) => RemoveCheck(check?.Tag);

        /// <summary>
        /// Removes the custom check.
        /// </summary>
        /// <param name="tag">The tag representing the custom check to remove.</param>
        public void RemoveCheck(string tag)
        {
            if (tag == null) return;

            if (_customChecks.ContainsKey(tag))
            {
                _customChecks.Remove(tag);
            }
        }

        /// <summary>
        /// Sets the upper and lower password length bounds.
        /// If a length check exists its properties are updated with the new bounds.
        /// </summary>
        /// <param name="minLength">The minimum required password length.</param>
        /// <param name="maxLength">The maximum allowed password length.</param>
        public void SetLengthBounds(uint minLength, uint maxLength)
        {
            _minLength = minLength;
            _maxLength = maxLength;

            if (!_predefinedChecks.TryGetValue(CheckTypes.Length, out Check check)) return;
            if (!(check is LengthCheck lengthCheck)) return;

            lengthCheck.MinLength = _minLength;
            lengthCheck.MaxLength = _maxLength;
        }

        /// <summary>
        /// Creates and adds a check for each active check type.
        /// </summary>
        /// <param name="checkTypes">The check types.</param>
        private void SafeAddChecks(CheckTypes checkTypes)
        {
            foreach (CheckTypes check in checkTypes.GetActiveChecks())
            {
                if (!_predefinedChecks.ContainsKey(check))
                {
                    _predefinedChecks.Add(
                        check,
                        CheckFactory.Create(
                            check,
                            _minLength,
                            _maxLength,
                            _letterSequenceLength,
                            _letterRepetitionLength,
                            _symbolRepetitionLength,
                            _digitRepetitionLength,
                            _numberSequenceLength)
                    );
                }
            }
        }

        /// <summary>
        /// Removes the checks represented by the given check types.
        /// </summary>
        /// <param name="checkTypes">The check types.</param>
        private void SafeRemoveChecks(CheckTypes checkTypes)
        {
            foreach (CheckTypes check in checkTypes.GetActiveChecks())
            {
                if (_predefinedChecks.ContainsKey(check))
                {
                    _predefinedChecks.Remove(check);
                }
            }
        }

    }
}
