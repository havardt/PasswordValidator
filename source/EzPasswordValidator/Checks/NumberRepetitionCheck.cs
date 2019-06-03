
namespace EzPasswordValidator.Checks
{
    /// <inheritdoc />
    /// <summary>
    /// A class representing a check for number repetition.
    /// </summary>
    /// <seealso cref="EzPasswordValidator.Checks.Check" />
    public sealed class NumberRepetitionCheck : Check
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="EzPasswordValidator.Checks.NumberRepetitionCheck" /> class.
        /// </summary>
        public NumberRepetitionCheck() 
            : base(CheckTypes.NumberRepetition)
        { }

        /// <inheritdoc />
        /// <summary>
        /// Checks if the password contains number repetition 3 or longer in length, such as 444 or 222.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the check is passed and NO number repetition is found; <c>false</c> otherwise.
        /// </returns>
        protected override bool OnExecute(string password) => 
            CheckHelper.RepetitionCheck(char.IsDigit, password);
    }
}