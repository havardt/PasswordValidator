using System;
using System.ComponentModel;

namespace EzPasswordValidator.Checks
{
    /// <summary>
    /// Factory class for generating instances of <see cref="Check"/> classes.
    /// </summary>
    public static class CheckFactory
    {
        /// <summary>
        /// Creates the a check for the specified check type.
        /// </summary>
        /// <param name="checkType">Type of check to generate.</param>
        /// <param name="minLength">The minimum required length of the password.</param>
        /// <param name="maxLength">The maximum allowed length of the password.</param>
        /// <param name="letterSequenceLength">The sequence length at which the <see cref="LetterSequenceCheck"/> fails.</param>
        /// <param name="letterRepetitionLength">The amount of letter repetitions that result in a failed <see cref="LetterRepetitionCheck"/> check.</param>
        /// <param name="symbolRepetitionLength">The amount of symbol repetitions that result in a failed <see cref="SymbolRepetitionCheck"/> check.</param>
        /// <returns>An instance of a <see cref="Check"/> object representing the given check type.</returns>
        /// <exception cref="InvalidEnumArgumentException">The check must only contain a single flag.</exception>
        /// <exception cref="ArgumentOutOfRangeException">No check found for the given argument.</exception>
        public static Check Create(
            CheckTypes checkType,
            uint minLength,
            uint maxLength,
            int letterSequenceLength,
            int letterRepetitionLength,
            int symbolRepetitionLength)
        {
            if (!checkType.IsSingleFlag())
            {
                throw new InvalidEnumArgumentException("The check must only contain a single flag.");
            }
            switch (checkType)
            {
                case CheckTypes.Length:
                    return new LengthCheck(minLength, maxLength);
                case CheckTypes.Numbers:
                    return new NumberCheck();
                case CheckTypes.Letters:
                    return new LetterCheck();
                case CheckTypes.Symbols:
                    return new SymbolCheck();
                case CheckTypes.CaseUpperLower:
                    return new CaseCheck();
                case CheckTypes.NumberSequence:
                    return new NumberSequenceCheck();
                case CheckTypes.NumberRepetition:
                    return new NumberRepetitionCheck();
                case CheckTypes.NumberMixed:
                    return new NumberPositionCheck();
                case CheckTypes.LetterSequence:
                    return new LetterSequenceCheck(letterSequenceLength);
                case CheckTypes.LetterRepetition:
                    return new LetterRepetitionCheck(letterRepetitionLength);
                case CheckTypes.SymbolRepetition:
                    return new SymbolRepetitionCheck(symbolRepetitionLength);
                default:
                    throw new ArgumentOutOfRangeException(nameof(checkType), checkType, "No check found for the given argument.");
            }
        }

            
    }
}