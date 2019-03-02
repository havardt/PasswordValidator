using System.Text.RegularExpressions;

namespace EzPasswordValidator.Checks
{
    /// <inheritdoc />
    /// <summary>
    /// A class representing a check for letter sequences.
    /// </summary>
    /// <seealso cref="EzPasswordValidator.Checks.Check" />
    public class LetterSequenceCheck : Check
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="LetterSequenceCheck" /> class.
        /// </summary>
        public LetterSequenceCheck() 
            : base(CheckTypes.LetterSequence)
        { }

        /// <inheritdoc />
        /// <summary>
        /// Checks if the password contains an alphabetical letter sequence consisting of four or more
        /// characters. Two common three letter sequences are added: abc and xyz.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the password does NOT contain a letter sequence; <c>false</c> otherwise.
        /// </returns>
        /// <remarks>Note that the check is NOT case sensitive.</remarks>
        protected override bool OnExecute(string password) => 
            !Regex.IsMatch(password, @"^.*(abc|xyz|abcd|bcde|cdef|defg|efgh|fghi|ghij|hijk|ijkl|jklm|klmn|lmno|mnop|nopq|opqr|pqrs|qrst|rstu|stuv|tuvw|uvwx|vwxy|wxyz)+.*$", RegexOptions.IgnoreCase);
    }
}