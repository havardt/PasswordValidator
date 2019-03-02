using System;
using System.Collections.Generic;

namespace EzPasswordValidator.Checks
{
    /// <summary>
    /// Extension class for <see cref="CheckTypes"/>.
    /// </summary>
    public static class CheckTypesExtensions
    {

        /// <summary>
        /// Determines whether the passed value represents actual checks.
        /// </summary>
        /// <param name="checkTypes">The value to check.</param>
        /// <returns>
        ///   <c>true</c> if the check is in range of the preset checks; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInRange(this CheckTypes checkTypes) => checkTypes >= CheckTypes.None && checkTypes <= CheckTypes.All;

        /// <summary>
        /// Determines whether the check contains only a single flag.
        /// </summary>
        /// <param name="checkTypes">The check</param>
        /// <returns>
        ///   <c>true</c> if the check contains one and only one flag; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>EzPasswordValidator.Checks.None is not considered a single flag.</remarks>
        public static bool IsSingleFlag(this CheckTypes checkTypes) => checkTypes != 0 && (checkTypes & (checkTypes - 1)) == 0;

        /// <summary>
        /// Gets the checks currently set on this check.
        /// </summary>
        /// <param name="checkTypes">The checks.</param>
        /// <returns>Each active check is iterated over and returned.</returns>
        public static IEnumerable<CheckTypes> GetActiveChecks(this CheckTypes checkTypes)
        {
            foreach (CheckTypes check in Enum.GetValues(checkTypes.GetType()))
            {
                if (checkTypes.HasFlag(check) && check.IsSingleFlag())
                {
                    yield return check;
                }
            }
        }
    }
}