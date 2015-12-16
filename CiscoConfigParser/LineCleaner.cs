// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LineCleaner.cs" company="John Oxley">
//   2012
// </copyright>
// <summary>
//   Line Cleaner
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CiscoConfigParser
{
    /// <summary>
    /// Line Cleaner
    /// </summary>
    public class LineCleaner
    {
        #region Public Methods and Operators

        /// <summary>
        /// The clean.
        /// </summary>
        /// <param name="line">
        /// The line.
        /// </param>
        /// <returns>
        /// The clean line.
        /// </returns>
        public string Clean(string line)
        {
            if (line.StartsWith("  ---- More ---"))
            {
                return line.Substring(68);
            }

            return line;
        }

        #endregion
    }
}