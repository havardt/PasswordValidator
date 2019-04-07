using System.Text.RegularExpressions;

namespace EzPasswordValidator.Checks
{
    /// <inheritdoc />
    /// <summary>
    /// A class representing a password check for symbol repetition.
    /// </summary>
    /// <seealso cref="EzPasswordValidator.Checks.Check" />
    public sealed class SymbolRepetitionCheck : Check
    {

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="EzPasswordValidator.Checks.SymbolRepetitionCheck" /> class.
        /// </summary>
        public SymbolRepetitionCheck() 
            : base(CheckTypes.SymbolRepetition)
        { }

        /// <inheritdoc />
        /// <summary>
        /// Checks for immediate symbol repetition 3 or longer in sequence.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the password does NOT contain symbol repetition; <c>false</c> otherwise.
        /// </returns>
        protected override bool OnExecute(string password) =>
            !Regex.IsMatch(password, @"^.*([^A-Za-z0-9])\1{2}.*$");
    }
}