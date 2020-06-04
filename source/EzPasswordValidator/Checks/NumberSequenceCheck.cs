using System;

namespace EzPasswordValidator.Checks
{
    /// <inheritdoc />
    /// <summary>
    /// A class representing a check for number sequences, such as '12345'.
    /// This check tests for both forward(12345) and backward(54321) sequences.
    /// </summary>
    /// <seealso cref="EzPasswordValidator.Checks.Check" />
    public sealed class NumberSequenceCheck : Check
    {
        public const int DefaultSequenceLength = 4;
        private int _sequenceLength;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="NumberSequenceCheck" /> class.
        /// </summary>
        public NumberSequenceCheck(int sequenceLength = DefaultSequenceLength)
            : base(CheckTypes.NumberSequence)
        {
            SequenceLength = sequenceLength;
        }

        /// <summary>
        /// The amount of digits that must be in sequence for this
        /// test to fail.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the set sequence length is less than or equal to 1.
        /// </exception>
        public int SequenceLength
        {
            get => _sequenceLength;
            set
            {
                if (value <= 1)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(SequenceLength),
                        "The sequence length cannot be less than or equal to 1.");
                }

                _sequenceLength = value;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Checks if the password contains a number series of <see cref="SequenceLength"/> length or longer.
        /// The check is passed if the password does NOT contain a number sequence.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the check is passed and NO number sequence is found; <c>false</c> otherwise.
        /// </returns>
        /// <remarks>Both forward and backward sequences are checked.</remarks>
        protected override bool OnExecute(string password) =>
            !(HasNumberSequence(password, _sequenceLength, -1) ||
              HasNumberSequence(password, _sequenceLength, 1));

        /// <summary>
        ///   Checks if a string contains a number sequence of a set length.
        /// </summary>
        /// <param name="str">The string to check for a number sequence.</param>
        /// <param name="len">The length of the sequence to look for.</param>
        /// <param name="d">
        ///   Positive 1 if looking for a backwards sequence (9876) or
        ///   negative 1 if looking for a forward sequence (1234).
        /// </param>
        /// <returns>
        ///   <code>true</code> if the string contains a sequence of the
        ///   given length; otherwise <code>false</code>.
        /// </returns>
        private static bool HasNumberSequence(string str, int len, int d)
        {
            int count = 1;
            int previousDigit = int.MinValue;

            foreach (char c in str)
            {
                if (char.IsDigit(c))
                {
                    if (previousDigit == (int.Parse(c.ToString()) + d))
                    {
                        count++;
                        if (count == len)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        count = 1;
                    }

                    previousDigit = int.Parse(c.ToString());
                }
                else
                {
                    count = 1;
                    previousDigit = int.MinValue;
                }
            }
            return false;
        }
        
    }
}