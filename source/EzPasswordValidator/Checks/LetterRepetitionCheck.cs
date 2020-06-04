using System;

namespace EzPasswordValidator.Checks
{
    /// <inheritdoc />
    /// <summary>
    /// A class representing a check for letter repetition.
    /// </summary>
    /// <seealso cref="EzPasswordValidator.Checks.Check" />
    public sealed class LetterRepetitionCheck : Check
    {
        public const int DefaultRepetitionLength = 4;

        private int _repetitionLength;

        /// <inheritdoc />
        /// <summary>
        ///   Initializes a new instance of the <see cref="LetterRepetitionCheck" /> class.
        /// </summary>
        /// <param name="repetitionLength">
        ///   The amount of characters that must be repeated for this check to fail.
        /// </param>
        public LetterRepetitionCheck(int repetitionLength = DefaultRepetitionLength)
            : base(CheckTypes.LetterRepetition)
        {
            RepetitionLength = DefaultRepetitionLength;
        }

        /// <summary>
        /// The amount of characters that must be repeated for this
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
        /// Checks if the password contains letter repetition 3 or longer in length.
        /// The check is not case sensitive meaning 'aAA' and 'aaa' will both match.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the password does NOT contain letter repetition; <c>false</c> otherwise.
        /// </returns>
        protected override bool OnExecute(string password) =>
            CheckHelper.RepetitionCheck(char.IsLetter, password, RepetitionLength);
    }
}