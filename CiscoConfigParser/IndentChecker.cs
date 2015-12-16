// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IndentChecker.cs" company="John Oxley">
//   2012
// </copyright>
// <summary>
//   Indent Checker
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CiscoConfigParser
{
    /// <summary>
    /// Indent Checker
    /// </summary>
    public class IndentChecker
    {
        #region Public Methods and Operators

        /// <summary>
        /// Gets the indent level.
        /// </summary>
        /// <param name="line">
        /// The line. 
        /// </param>
        /// <returns>
        /// The indent level. 
        /// </returns>
        public int GetIndentLevel(string line)
        {
            int indent;
            for (indent = 0; indent < line.Length && line[indent] == ' '; indent++)
            {
            }

            return indent;
        }

        #endregion
    }
}