using System;
using System.Collections.Generic;
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
    ///     //Do something..
    /// }
    /// else
    /// {
    ///     foreach (Check failedCheck in validator.FailedChecks)
    ///     {
    ///         //Inform user?
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="IValidator{T}" />
    public class PasswordValidator : IValidator<string>
    {
        private readonly Dictionary<CheckTypes, Check> _predefinedChecks = new Dictionary<CheckTypes, Check>();
        private readonly Dictionary<string, Check> _customChecks = new Dictionary<string, Check>();
        private uint _requiredLength;

        /// <inheritdoc />
        public PasswordValidator() 
            : this(CheckTypes.None, LengthCheck.DefaultLength)
        {
            
        }

        /// <inheritdoc />
        public PasswordValidator(CheckTypes checkTypes) 
            : this(checkTypes, LengthCheck.DefaultLength)
        {
            
        }

        /// <inheritdoc />
        public PasswordValidator(uint requiredLength)
            : this(Checks.CheckTypes.None, requiredLength)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordValidator"/> class.
        /// </summary>
        /// <param name="checkTypes">Initial check types.</param>
        /// <param name="requiredLength">The minimum required length of the password.</param>
        public PasswordValidator(CheckTypes checkTypes, uint requiredLength)
        {
            _requiredLength = requiredLength;
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
        /// Gets or sets the minimum required length of the password.
        /// </summary>
        /// <value>
        /// A <see cref="UInt32"/> positive number.
        /// </value>
        public uint RequiredLength
        {
            get => _requiredLength;
            set
            {
                _requiredLength = value;
                if (_predefinedChecks.TryGetValue(CheckTypes.Length, out Check check))
                {
                    if (check is LengthCheck lengthCheck)
                    {
                        lengthCheck.RequiredLength = value;
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
        ///   <c>true</c> if the password passes all checks; <c>false</c> otherwise.
        /// </returns>
        public bool Validate(string password)
        {
            bool result = CheckTypes > CheckTypes.None;
            foreach (Check check in AllChecks)
            {
                if (check.Execute(password) == false)
                {
                    result = false;
                }
            }
            return result;
        }
		
		/// <summary>
        /// Validates the specified password asynchronous. This method should be used
        /// when custom checks include long running operations.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <returns>
        ///   <c>true</c> if the password passes all checks; <c>false</c> otherwise.
        /// </returns>
        public async Task<bool> ValidateAsync(string password) => await Task.Run(() => Validate(password));

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
            if(!checkTypes.IsInRange())
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
        /// Adds the custom check.
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
        public void RemoveCheck(int checkTypes) => RemoveCheck((CheckTypes) checkTypes);

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
        /// Creates and adds a check for each active check type.
        /// </summary>
        /// <param name="checkTypes">The check types.</param>
        private void SafeAddChecks(CheckTypes checkTypes)
        {
            foreach (CheckTypes check in checkTypes.GetActiveChecks())
            {
                if (!_predefinedChecks.ContainsKey(check))
                {
                    _predefinedChecks.Add(check, CheckFactory.Create(check, _requiredLength));
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
