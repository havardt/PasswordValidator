using System;

namespace EzPasswordValidator.Checks
{
    /// <inheritdoc />
    /// <summary>
    /// A class representing a simple custom check. 
    /// </summary>
    /// <example>
    /// Usage example:
    /// <code>
    /// var validator = new PasswordValidator();
    /// validator.AddCheck(new SimpleCheck("MyCustomCheck", psw => psw.Length > 8));
    /// //or using an already defined method..
    /// validator.AddCheck(new SimpleCheck(nameof(MyCustomCheck), MyCustomCheck));
    /// </code>
    /// </example>
    /// <seealso cref="EzPasswordValidator.Checks.CustomCheck" />
    internal sealed class SimpleCheck : CustomCheck
    {
        /// <summary>
        /// Represents the check to be executed.
        /// </summary>
        private readonly Func<string, bool> _check;

        /// <inheritdoc />
        /// <summary>
        ///   Initializes a new instance of the <see cref="EzPasswordValidator.Checks.SimpleCheck" /> class.
        /// </summary>
        /// <param name="tag">
        ///   Unique string identifier.
        /// </param>
        /// <param name="check">
        ///   A custom password check taking the password as a parameter and returning
        ///   a <c>bool</c> where <c>true</c> represents a passed check and <c>false</c>a failed check.
        /// </param>
        public SimpleCheck(string tag, Func<string, bool> check) 
            : base(tag)
        {
            _check = check;
        }

        /// <inheritdoc />
        protected override bool OnExecute(string password) => _check(password);
    }
}
