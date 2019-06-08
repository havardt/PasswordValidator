namespace EzPasswordValidator.Checks
{
    /// <inheritdoc />
    /// <summary>
    /// A class for checking password length.
    /// </summary>
    /// <seealso cref="EzPasswordValidator.Checks.Check" />
    public sealed class LengthCheck : Check
    {
        /// <summary>
        /// The default minimum length of the password.
        /// </summary>
        public const uint DefaultMinLength = 8;

        /// <summary>
        /// The default maximum length of the password.
        /// </summary>
        public const uint DefaultMaxLength = 128;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="EzPasswordValidator.Checks.LengthCheck" /> class.
        /// </summary>
        /// <param name="minLen">The minimum required length of the password.</param>
        /// <param name="maxLen">The maximum allowed length of the password.</param>
        public LengthCheck(uint minLen = DefaultMinLength, uint maxLen = DefaultMaxLength) 
            : base(CheckTypes.Length)
        {
            MinLength = minLen;
            MaxLength = maxLen;
        }

        /// <summary>
        /// Gets or sets the minimum required length of the password.
        /// </summary>
        public uint MinLength { get; set; }

        /// <summary>
        /// Gets or sets the maximum allowed length of the password.
        /// </summary>
        public uint MaxLength { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Checks if the given password is equal to or longer than the required minimum length
        /// and equal to or shorter than the maximum length.
        /// The password is trimmed (trailing and leading white space is removed) before checking length.
        /// <c>null</c> is always invalid.
        /// </summary>
        protected override bool OnExecute(string password) =>
            password != null && 
            password.Trim().Length >= MinLength &&
            password.Trim().Length <= MaxLength;
    }
}