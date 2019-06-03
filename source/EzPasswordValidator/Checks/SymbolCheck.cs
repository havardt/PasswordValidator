using System.Collections.Generic;

namespace EzPasswordValidator.Checks
{
    /// <inheritdoc />
    /// <summary>
    /// A class representing a check for symbols.
    /// </summary>
    /// <seealso cref="EzPasswordValidator.Checks.Check" />
    public sealed class SymbolCheck : Check
    {

        public static HashSet<char> SymbolsHashSet = new HashSet<char>(new[]
        {
            '~','!','@','#','$','%','^','&','*','(',')','_','-','+','=','[','{','}',']','\'','"',';',
            ':','/','?','<','>',',','.','`','£','§','€'
        });

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="EzPasswordValidator.Checks.SymbolCheck" /> class.
        /// </summary>
        public SymbolCheck() 
            : base(CheckTypes.Symbols)
        { }

        /// <inheritdoc />
        /// <summary>
        /// Checks that the password contains at least one symbol.
        /// </summary>
        /// <remarks>
        /// Note that this check only checks for the most common symbols, <see cref="SymbolsHashSet"/>.
        /// </remarks>
        protected override bool OnExecute(string password)
        {
            foreach (char c in password)
            {
                if (SymbolsHashSet.Contains(c))
                {
                    return true;
                }   
            }
            return false;
        }
    }
}