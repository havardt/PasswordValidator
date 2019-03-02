using System.Text.RegularExpressions;

namespace EzPasswordValidator.Checks
{
    /// <inheritdoc />
    /// <summary>
    /// A class checking for numbers in passwords.
    /// </summary>
    /// <seealso cref="EzPasswordValidator.Checks.Check" />
    public class NumberCheck : Check
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="EzPasswordValidator.Checks.NumberCheck" /> class.
        /// </summary>
        public NumberCheck() 
            : base(CheckTypes.Numbers)
        { }

        /// <inheritdoc />
        /// <summary>
        /// Checks that the password contains at least one number.
        /// </summary>
        protected override bool OnExecute(string password) =>
            Regex.IsMatch(password, "^.*[0-9]+.*$");
    }
}