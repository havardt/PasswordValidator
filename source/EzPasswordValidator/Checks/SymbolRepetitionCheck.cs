
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
        protected override bool OnExecute(string password)
        {
            const char baseSymbol = (char) 0;
            char previousPrevious = baseSymbol;
            char previous = baseSymbol;
            foreach (char c in password)
            {
                if (SymbolCheck.SymbolsHashSet.Contains(c))
                {
                    if (previousPrevious == baseSymbol || previousPrevious != c)
                    {
                        previousPrevious = c;
                    }else if (previous == baseSymbol || previous != c)
                    {
                        previous = c;
                    }
                    else
                    {
                        if (previousPrevious == previous && previous == c)
                        {
                            return false;
                        }
                    }
                    continue;
                }

                previousPrevious = baseSymbol;
                previous = baseSymbol;
            }
            return true;
        }
    }
}