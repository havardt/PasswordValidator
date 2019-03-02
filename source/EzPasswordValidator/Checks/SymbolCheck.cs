using System.Text.RegularExpressions;

namespace EzPasswordValidator.Checks
{
    /// <inheritdoc />
    /// <summary>
    /// A class representing a check for symbols.
    /// </summary>
    /// <seealso cref="EzPasswordValidator.Checks.Check" />
    public class SymbolCheck : Check
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="EzPasswordValidator.Checks.SymbolCheck" /> class.
        /// </summary>
        public SymbolCheck() 
            : base(CheckTypes.Symbols)
        { }

        /// <inheritdoc />
        /// <summary>
        /// Checks that the password contains at least one symbol.
        /// Symbols are here defined as any character that is not a digit or
        /// letter in the range A through Z.
        /// </summary>
        protected override bool OnExecute(string password) =>
            Regex.IsMatch(password, "^.*[^0-9A-Za-z].*$");
    }
}