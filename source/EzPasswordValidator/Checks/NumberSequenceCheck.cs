using System.Text.RegularExpressions;

namespace EzPasswordValidator.Checks
{
    /// <inheritdoc />
    /// <summary>
    /// A class representing a check for number sequences.
    /// </summary>
    /// <seealso cref="EzPasswordValidator.Checks.Check" />
    public sealed class NumberSequenceCheck : Check
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="NumberSequenceCheck" /> class.
        /// </summary>
        public NumberSequenceCheck() 
            : base(CheckTypes.NumberSequence)
        { }

        /// <inheritdoc />
        /// <summary>
        /// Checks if the password contains a number series 3 or longer such as 123 or 765.
        /// The check is passed if the password does NOT contain a number sequence.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the check is passed and NO number sequence is found; <c>false</c> otherwise.
        /// </returns>
        protected override bool OnExecute(string password) =>
            !Regex.IsMatch(password, @"^.*(012|123|234|345|456|567|678|789|987|876|765|654|543|432|321|210)+.*$");
    }
}