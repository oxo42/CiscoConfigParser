// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SectionDelimiter.cs" company="John Oxley">
//   2012
// </copyright>
// <summary>
//   Section Delimiter
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CiscoConfigParser
{
    /// <summary>
    /// Section Delimiter
    /// </summary>
    public class SectionDelimiter
    {
        #region Public Methods and Operators

        /// <summary>
        /// Checks if the line is a section delimiter
        /// </summary>
        /// <param name="line">
        /// The line.
        /// </param>
        /// <returns>
        /// The is section delimiter.
        /// </returns>
        public bool IsSectionDelimiter(string line)
        {
            return line.Equals("#") || line.Equals("!");
        }

        #endregion
    }
}