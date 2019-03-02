using System.Text.RegularExpressions;

namespace EzPasswordValidator.Checks
{
    /// <inheritdoc />
    /// <summary>
    /// A class for validating that the position of numbers in the password
    /// are not only in the front or back of password.
    /// </summary>
    /// <seealso cref="EzPasswordValidator.Checks.Check" />
    public class NumberPositionCheck : Check
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="EzPasswordValidator.Checks.NumberPositionCheck" /> class.
        /// </summary>
        public NumberPositionCheck() 
            : base(CheckTypes.NumberMixed)
        { }

        /// <inheritdoc />
        /// <summary>
        /// Checks that the password does not only have numbers in the front and/or end of the password.
        /// </summary>
        /// <remarks>
        /// This check will always return <c>false</c> if there are no numbers in the password.
        /// </remarks>
        protected override bool OnExecute(string password) =>
            Regex.IsMatch(password, @"^.*[^0-9]+[0-9]+[^0-9]+.*$");
    }
}