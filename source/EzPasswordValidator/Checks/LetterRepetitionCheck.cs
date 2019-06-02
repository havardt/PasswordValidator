﻿using System.Text.RegularExpressions;

namespace EzPasswordValidator.Checks
{
    /// <inheritdoc />
    /// <summary>
    /// A class representing a check for letter repetition.
    /// </summary>
    /// <seealso cref="EzPasswordValidator.Checks.Check" />
    public sealed class LetterRepetitionCheck : Check
    {

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="EzPasswordValidator.Checks.LetterRepetitionCheck" /> class.
        /// </summary>
        public LetterRepetitionCheck() 
            : base(CheckTypes.LetterRepetition)
        { }

        /// <inheritdoc />
        /// <summary>
        /// Checks if the password contains letter repetition 3 or longer in length.
        /// The check is not case sensitive meaning 'aAA' and 'aaa' will both match.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the password does NOT contain letter repetition; <c>false</c> otherwise.
        /// </returns>
        protected override bool OnExecute(string password)
        {
            const char baseChar = (char)0;
            char previousPrevious = baseChar;
            char previous = baseChar;
            foreach (char c in password)
            {
                if (char.IsLetter(c))
                {
                    if (previousPrevious == baseChar || previousPrevious != c)
                    {
                        previousPrevious = c;
                    }
                    else if (previous == baseChar || previous != c)
                    {
                        previous = c;
                    }
                    else
                    {
                        if (previousPrevious == previous && previous == c)
                        {
                            return false;
                        }
                    }
                    continue;
                }

                previousPrevious = baseChar;
                previous = baseChar;
            }
            return true;
        }
    }
}