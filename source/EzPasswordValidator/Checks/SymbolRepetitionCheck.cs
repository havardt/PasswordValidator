
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
        /// Checks for immediate symbol repetition 3 or longer in sequence. The check
        /// is executed for symbols as defined in <see cref="SymbolCheck.SymbolsHashSet"/>.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the password does NOT contain symbol repetition; <c>false</c> otherwise.
        /// </returns>
        protected override bool OnExecute(string password) => 
            CheckHelper.RepetitionCheck(c => SymbolCheck.SymbolsHashSet.Contains(c), password);
    }
}