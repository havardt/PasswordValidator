
namespace EzPasswordValidator.Checks
{
    /// <inheritdoc />
    /// <summary>
    /// A class representing a check for upper- and lower-case letters.
    /// </summary>
    /// <seealso cref="EzPasswordValidator.Checks.Check" />
    public sealed class CaseCheck : Check
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="EzPasswordValidator.Checks.CaseCheck" /> class.
        /// </summary>
        public CaseCheck() 
            : base(CheckTypes.CaseUpperLower)
        { }

        /// <inheritdoc />
        /// <summary>
        /// Checks that the password contains at least one upper- and lower-case letter.
        /// </summary>
        protected override bool OnExecute(string password)
        {
            var hasUpper = false;
            var hasLower = false;
            foreach (char c in password)
            {
                if (char.IsUpper(c))
                {
                    hasUpper = true;
                    if (hasLower)
                    {
                        return true;
                    }
                    continue;//No point checking if lower if the char is already an upper case char.
                }
                if (char.IsLower(c))
                {
                    hasLower = true;
                    if (hasUpper)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}