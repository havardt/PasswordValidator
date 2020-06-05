
namespace EzPasswordValidator.Validators
{
    /// <summary>
    /// Classes that implement this interface contain logic for validating an item.
    /// </summary>
    /// <typeparam name="T">The type to validate.</typeparam>
    public interface IValidator<in T>
    {
        /// <summary>
        /// Validates the specified item.
        /// </summary>
        /// <param name="item">The item to validate.</param>
        /// <returns>
        ///   <c>true</c> if the passed item is valid,<c>false</c> otherwise.
        /// </returns>
        bool Validate(T item);

        /// <summary>
        /// Validates the specified item.
        /// </summary>
        /// <param name="item">The item to validate.</param>
        /// <param name="p">
        ///   The % amount of checks that need to pass for the item to be valid.
        ///   This is a value between 0 and 1. Where 0.5 means that 50% of the checks need to pass.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the passed item is valid, <c>false</c> otherwise.
        /// </returns>
        bool Validate(T item, double p);

        bool Validate(string password, int minPassedChecks);
    }
}
