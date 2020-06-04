
using System;

namespace EzPasswordValidator.Checks
{
    /// <inheritdoc />
    /// <summary>
    /// A class representing a check for number repetition.
    /// </summary>
    /// <seealso cref="EzPasswordValidator.Checks.Check" />
    public sealed class DigitRepetitionCheck : Check
    {
        public const int DefaultRepetitionLength = 4;

        private int _repetitionLength;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="DigitRepetitionCheck" /> class.
        /// </summary>
        public DigitRepetitionCheck(int repetitionLength = DefaultRepetitionLength)
            : base(CheckTypes.DigitRepetition)
        {
            RepetitionLength = repetitionLength;
        }

        /// <summary>
        /// The amount of digits that must be repeated for this
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
        ///   Checks if the password contains digit repetition equal to or longer
        ///   in length than <see cref="RepetitionLength"/>.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the check is passed and NO number repetition is found;
        ///   <c>false</c> otherwise.
        /// </returns>
        protected override bool OnExecute(string password) => 
            CheckHelper.RepetitionCheck(char.IsDigit, password, RepetitionLength);
    }
}