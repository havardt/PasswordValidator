using System;

namespace EzPasswordValidator.Checks
{
    /// <summary>
    /// Represents the preset password validation checks.
    /// </summary>
    [Flags]
    public enum CheckTypes
    {
        /// <summary>
        /// Represents no checks.
        /// </summary>
        None = 0b0000000000000000000000000000000,
        /// <summary>
        /// Represents all checks.
        /// </summary>
        All = 0b0000000000000000000011111111111,
        /// <summary>
        /// Represents the most basic checks.
        /// <list type="bullet">
        ///   <item><description>Length check <see cref="Length"/></description></item>
        ///   <item><description>Number check <see cref="Numbers"/></description></item>
        ///   <item><description>Letter check <see cref="Letters"/></description></item>
        ///   <item><description>Symbol check <see cref="Symbols"/></description></item>
        ///   <item><description>Upper and lower case letter check <see cref="CaseUpperLower"/></description></item>
        /// </list>
        /// </summary>
        Basic = 0b0000000000000000000000000011111,
        /// <summary>
        /// Represents an advanced check.
        /// The following listed checks are included.
        /// <list type="bullet">
        ///    <item><description>All basic checks <see cref="Basic"/></description></item>
        ///    <item><description>Checks for a number sequence <see cref="NumberSequence"/></description></item>
        ///    <item><description>Checks for number repetition <see cref="NumberRepetition"/></description></item>
        ///    <item><description>Checks if numbers are mixed <see cref="NumberMixed"/></description></item>
        ///    <item><description>Checks for letter sequence <see cref="LetterSequence"/></description></item>
        ///    <item><description>Checks for letter repetition <see cref="LetterRepetition"/></description></item>
        ///    <item><description>Checks for symbol repetition <see cref="SymbolRepetition"/></description></item>
        /// </list>
        /// </summary>
        Advanced = 0b0000000000000000000011111111111,
        /// <summary>
        /// Represents a length check. 
        /// </summary>
        /// <seealso cref="LengthCheck"/>
        Length = 0b0000000000000000000000000000001,
        /// <summary>
        /// Represents a check for numbers.
        /// </summary>
        /// <seealso cref="NumberCheck"/>
        Numbers = 0b0000000000000000000000000000010,
        /// <summary>
        /// Represents a check for letters.
        /// </summary>
        /// <seealso cref="LetterCheck"/>
        Letters = 0b0000000000000000000000000000100,
        /// <summary>
        /// Represents a check for symbols.
        /// </summary>
        /// <seealso cref="SymbolCheck"/>
        Symbols = 0b0000000000000000000000000001000,
        /// <summary>
        /// Represents a check for upper and lower case letters.
        /// </summary>
        /// <seealso cref="CaseCheck"/>
        CaseUpperLower = 0b0000000000000000000000000010000,
        /// <summary>
        /// Represents a check for a number sequence 3.
        /// </summary>
        /// <seealso cref="NumberSequenceCheck"/>
        NumberSequence = 0b0000000000000000000000000100000,
        /// <summary>
        /// Represents a check for number repetition.
        /// </summary>
        /// <seealso cref="NumberRepetitionCheck"/>
        NumberRepetition = 0b0000000000000000000000001000000,
        /// <summary>
        /// Represents a check for digits in other positions than the start/end.
        /// </summary>
        /// <seealso cref="NumberPositionCheck"/>
        NumberMixed = 0b0000000000000000000000010000000,
        /// <summary>
        /// Represents a check for a letter sequence.
        /// </summary>
        /// <seealso cref="LetterSequenceCheck"/>
        LetterSequence = 0b0000000000000000000000100000000,
        /// <summary>
        /// Represents a check for letter repetition.
        /// </summary>
        /// <seealso cref="LetterRepetitionCheck"/>
        LetterRepetition = 0b0000000000000000000001000000000,
        /// <summary>
        /// Represents a check for symbol repetition.
        /// </summary>
        /// <seealso cref="SymbolRepetitionCheck"/>
        SymbolRepetition = 0b0000000000000000000010000000000,
    }
}