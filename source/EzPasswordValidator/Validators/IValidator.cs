
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
    }
}
