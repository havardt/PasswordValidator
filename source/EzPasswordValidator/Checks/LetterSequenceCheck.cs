using System;

namespace EzPasswordValidator.Checks
{
    /// <inheritdoc />
    /// <summary>
    ///   A class representing a check for letter sequences.
    /// </summary>
    /// <seealso cref="EzPasswordValidator.Checks.Check" />
    public sealed class LetterSequenceCheck : Check
    {
        public const int DefaultSequenceLength = 4;
        private const int LowerLetterBound = (int)'a';
        private const int HigherLetterBound = (int)'z';

        private int _sequenceLength;

        /// <inheritdoc />
        /// <summary>
        ///   Initializes a new instance of the <see cref="LetterSequenceCheck" /> class.
        /// </summary>
        public LetterSequenceCheck(int maxSequenceLength = DefaultSequenceLength)
            : base(CheckTypes.LetterSequence)
        {
            SequenceLength = maxSequenceLength;
        }

        /// <summary>
        /// The amount of characters that must be in sequence for this
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
                if(value <= 1)
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
        ///   Checks if the password contains a forward alphabetical letter sequence
        ///   consisting of <see cref="SequenceLength"/> or more characters which
        ///  defaults to <see cref="DefaultSequenceLength"/>.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the password does NOT contain a letter sequence; <c>false</c> otherwise.
        /// </returns>
        /// <remarks>
        ///   Note that the check is NOT case sensitive and only supports ISO basic
        ///   latin alphabet (A-Za-z)
        /// </remarks>
        protected override bool OnExecute(string password)
        {
            var currentSequenceLength = 1;
            var previousChar = 0;

            foreach (char c in password.ToLowerInvariant())
            {
                if (IsInRange(c, LowerLetterBound, HigherLetterBound) &&
                    (previousChar + 1) == (int)c)
                {
                    currentSequenceLength++;
                    if (currentSequenceLength == SequenceLength)
                    {
                        return false;
                    }
                }
                else
                {
                    currentSequenceLength = 1;
                }
                previousChar = (int)c;
            }
            return true;
        }

        /// <summary>
        /// Checks that the given char is within the given upper and lower bounds.
        /// The upper and lower bounds are inclusive. 
        /// </summary>
        /// <param name="c">The char to check.</param>
        /// <param name="lower">The lowest value allowed.</param>
        /// <param name="upper">The highest value allowed.</param>
        /// <returns><code>true</code> if the character is within the range, <code>false</code> otherwise.</returns>
        private static bool IsInRange(char c, int lower, int upper) => (int) c >= lower && (int) c <= upper;
    }
}