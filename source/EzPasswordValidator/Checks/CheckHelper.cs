using System;

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
    }
}