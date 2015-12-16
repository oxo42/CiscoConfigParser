// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LineCleanerTests.cs" company="John Oxley">
//   2012
// </copyright>
// <summary>
//   The line cleaner tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CiscoConfigParser.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The line cleaner tests.
    /// </summary>
    [TestClass]
    public class LineCleanerTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// The clean line is the same.
        /// </summary>
        [TestMethod]
        public void CleanLineIsTheSame()
        {
            var cleaner = new LineCleaner();
            const string Line = " vlan batch 10 to 12 99 1662 2126 2252 to 2253";
            var result = cleaner.Clean(Line);
            Assert.AreEqual(Line, result);
        }

        /// <summary>
        /// The more text is cleaned.
        /// </summary>
        [TestMethod]
        public void MoreTextIsCleaned()
        {
            var cleaner = new LineCleaner();
            const string Line = "  ---- More ----[42D                                          [42Dfan speed auto";
            const string Expected = "fan speed auto";
            var result = cleaner.Clean(Line);
            Assert.AreEqual(Expected, result);
        }

        /// <summary>
        /// The more text with index is cleaned.
        /// </summary>
        [TestMethod]
        public void MoreTextWithIndexIsCleaned()
        {
            var cleaner = new LineCleaner();
            const string Line = "  ---- More ----[42D                                          [42D color red low-limit 20 high-limit 60 discard-percentage 100";
            const string Expected = " color red low-limit 20 high-limit 60 discard-percentage 100";
            var result = cleaner.Clean(Line);
            Assert.AreEqual(Expected, result);
        }

        #endregion
    }
}