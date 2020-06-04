using System;
using System.Runtime.CompilerServices;

namespace EzPasswordValidator.Checks
{
    public static class CheckHelper
    {
        /// <summary>
        /// Checks if the given string contains character repetition of length 3 or longer.
        /// </summary>
        /// <param name="expr">The expression of which filters which characters to check for repetition.</param>
        /// <param name="str">The string to check.</param>
        /// <returns>
        /// <c>true</c> if the given string passes the test and does NOT contain character repetition.
        /// If the given string does contain character repetition over the allowed length then <c>false</c>
        /// is returned.
        /// </returns>
        public static bool RepetitionCheck(Func<char, bool> expr, string str)
        {
            const char baseSymbol = (char)0;
            char previousPrevious = baseSymbol;
            char previous = baseSymbol;
            foreach (char c in str)
            {
                if (expr.Invoke(c))
                {
                    if (previousPrevious == baseSymbol || previousPrevious != c)
                    {
                        previousPrevious = c;
                    }
                    else if (previous == baseSymbol || previous != c)
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
                previousPrevious = baseSymbol;
                previous = baseSymbol;
            }
            return true;
        }

        /// <summary>
        ///   Checks if the given string contains character repetition of the
        ///   given length or longer.
        /// </summary>
        /// <param name="expr">The expression of which filters which characters to check for repetition.</param>
        /// <param name="str">The string to check.</param>
        /// <param name="len">The repetition length at which the check fails.</param>
        /// <returns>
        /// <c>true</c> if the given string passes the test and does NOT contain character repetition.
        /// If the given string does contain character repetition over the allowed length then <c>false</c>
        /// is returned.
        /// </returns>
        public static bool RepetitionCheck(Func<char, bool> expr, string str, int len)
        {
            int count = 1; // The current amount of repetitions.
            int previousChar = 0;

            foreach (char c in str)
            {
                if (expr.Invoke(c))
                {
                    if (previousChar == c)
                    {
                        count++;
                        if (count == len)
                        { // The char has been repeated too many times and the check fails.
                            return false;
                        }
                    }
                    else
                    {
                        previousChar = c;
                        count = 1;
                    }
                }
            }


            return true;
        }
    }

}