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
        /// The default required length of the password.
        /// </summary>
        public const uint DefaultLength = 8;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="EzPasswordValidator.Checks.LengthCheck" /> class.
        /// </summary>
        public LengthCheck() 
            : base(CheckTypes.Length)
        {
            RequiredLength = DefaultLength;
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="EzPasswordValidator.Checks.LengthCheck" /> class.
        /// </summary>
        /// <param name="requiredLength">The required minimum length of the password.</param>
        public LengthCheck(uint requiredLength)
            : base(CheckTypes.Length)
        {
            RequiredLength = requiredLength;
        }

        /// <summary>
        /// Gets or sets the minimum required length of the password.
        /// </summary>
        public uint RequiredLength { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Checks if the given password is equal to or longer than the required minimum length.
        /// The password is trimmed (trailing and leading white space is removed) before checking length.
        /// <c>null</c> is always invalid.
        /// </summary>
        protected override bool OnExecute(string password) =>
            !string.IsNullOrWhiteSpace(password) && password.Trim().Length >= RequiredLength;
    }
}