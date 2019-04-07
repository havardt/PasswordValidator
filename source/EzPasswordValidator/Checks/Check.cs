using System;

namespace EzPasswordValidator.Checks
{
    /// <summary>
    /// A class representing a single check/test/validation executed on the password.
    /// </summary>
    public abstract class Check : IEquatable<Check>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Check"/> class.
        /// </summary>
        /// <param name="type">The type of check represented by this class.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Type must be a single flag in range of <see cref="CheckTypes"/>.
        /// </exception>
        protected Check(CheckTypes type)
        {
            if ((!type.IsInRange() || !type.IsSingleFlag()) && type != CheckTypes.None)
            {
                throw new ArgumentOutOfRangeException(nameof(type));
            }
            Type = type;
        }

        /// <summary>
        /// Gets the type of check that is represented by this class.
        /// </summary>
        /// <value>
        /// The type is a single flag representing the type of check.
        /// </value>
        public CheckTypes Type { get; }

        /// <summary>
        /// The end result of the check.
        /// </summary>
        /// <value>
        ///   <c>true</c> indicates that the password passed the test;
        ///   <c>false</c> indicates a failed test.
        /// </value>
        public bool Result { get; protected set; }

        /// <summary>
        /// Executes the check on the password.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <returns><c>true</c> if the test is passed, <c>false</c> otherwise.</returns>
        public bool Execute(string password) => Result = OnExecute(password);

        /// <summary>
        /// This method contains logic for executing a check/ validation
        /// on the given password.
        /// Called when <see cref="Execute"/> is called.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <returns>
        ///   <c>true</c> if the test is passed, <c>false</c> otherwise.
        /// </returns>
        protected abstract bool OnExecute(string password);

        /// <inheritdoc />
        public virtual bool Equals(Check other) => Type == other?.Type;

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is Check other && Equals(other);

        /// <inheritdoc />
        public override int GetHashCode() => (int) Type;

        /// <inheritdoc />
        public override string ToString() => Type.ToString();
    }
}