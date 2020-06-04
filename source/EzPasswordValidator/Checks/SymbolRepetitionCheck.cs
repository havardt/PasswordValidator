
using System;

namespace EzPasswordValidator.Checks
{
    /// <inheritdoc />
    /// <summary>
    /// A class representing a password check for symbol repetition.
    /// </summary>
    /// <seealso cref="EzPasswordValidator.Checks.Check" />
    public sealed class SymbolRepetitionCheck : Check
    {
        public const int DefaultRepetitionLength = 4;

        private int _repetitionLength;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="EzPasswordValidator.Checks.SymbolRepetitionCheck" /> class.
        /// </summary>
        public SymbolRepetitionCheck(int repetitionLength = DefaultRepetitionLength)
            : base(CheckTypes.SymbolRepetition)
        {
            RepetitionLength = repetitionLength;
        }

        /// <summary>
        /// The amount of symbols that must be repeated for this
        /// check to fail.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the set repetition length is less than or equal to 1.
        /// </exception>
        public int RepetitionLength
        {
            get => _repetitionLength;
            set
            {
                if (value <= 1)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(RepetitionLength),
                        "The repetition length cannot be less than or equal to 1.");
                }

                _repetitionLength = value;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Checks for immediate symbol repetition 3 or longer in sequence. The check
        /// is executed for symbols as defined in <see cref="SymbolCheck.SymbolsHashSet"/>.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the password does NOT contain symbol repetition; <c>false</c> otherwise.
        /// </returns>
        protected override bool OnExecute(string password) => 
            CheckHelper.RepetitionCheck(c => SymbolCheck.SymbolsHashSet.Contains(c), password, RepetitionLength);
    }
}