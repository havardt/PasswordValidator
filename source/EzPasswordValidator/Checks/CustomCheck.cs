
namespace EzPasswordValidator.Checks
{
    /// <inheritdoc />
    /// <summary>
    /// A class representing a custom password check. All checks that are not predefined
    /// should inherit this class.
    /// </summary>
    /// <seealso cref="EzPasswordValidator.Checks.Check" />
    public abstract class CustomCheck : Check
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="EzPasswordValidator.Checks.CustomCheck" /> class.
        /// </summary>
        /// <param name="tag">Unique string identifier.</param>
        protected CustomCheck(string tag) 
            : base(CheckTypes.None)
        {
            Tag = tag;
        }

        /// <summary>
        /// Gets the unique identifier for this check. 
        /// </summary>
        /// <value>
        /// Unique string identifier.
        /// </value>
        public string Tag { get; }

        /// <inheritdoc />
        public override bool Equals(Check other) => 
            other is CustomCheck customOther && Tag == customOther.Tag;

        /// <inheritdoc />
        public override int GetHashCode() => Tag.GetHashCode();

        /// <inheritdoc />
        public override string ToString() => Tag;
    }
}