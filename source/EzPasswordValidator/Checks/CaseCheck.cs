using System.Text.RegularExpressions;

namespace EzPasswordValidator.Checks
{
    /// <inheritdoc />
    /// <summary>
    /// A class representing a check for upper- and lower-case letters.
    /// </summary>
    /// <seealso cref="EzPasswordValidator.Checks.Check" />
    public sealed class CaseCheck : Check
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="EzPasswordValidator.Checks.CaseCheck" /> class.
        /// </summary>
        public CaseCheck() 
            : base(CheckTypes.CaseUpperLower)
        { }

        /// <inheritdoc />
        /// <summary>
        /// Checks that the password contains at least one upper- and lower-case letter.
        /// </summary>
        protected override bool OnExecute(string password) =>
            Regex.IsMatch(password, "^.*([a-z]+.*[A-Z]+|[A-Z]+.*[a-z]+)+.*$");
    }
}