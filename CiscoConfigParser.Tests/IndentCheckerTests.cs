// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IndentCheckerTests.cs" company="John Oxley">
//   2012
// </copyright>
// <summary>
//   The indent checker tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CiscoConfigParser.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The indent checker tests.
    /// </summary>
    [TestClass]
    public class IndentCheckerTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// The indent 0.
        /// </summary>
        [TestMethod]
        public void Indent0()
        {
            var checker = new IndentChecker();
            const string Line = "fan speed auto";
            const int Expected = 0;
            var result = checker.GetIndentLevel(Line);
            Assert.AreEqual(Expected, result);
        }

        /// <summary>
        /// The indent 1.
        /// </summary>
        [TestMethod]
        public void Indent1()
        {
            var checker = new IndentChecker();
            const string Line = " fan speed auto";
            const int Expected = 1;
            var result = checker.GetIndentLevel(Line);
            Assert.AreEqual(Expected, result);
        }

        /// <summary>
        /// The indent 2.
        /// </summary>
        [TestMethod]
        public void Indent2()
        {
            var checker = new IndentChecker();
            const string Line = "  fan speed auto";
            const int Expected = 2;
            var result = checker.GetIndentLevel(Line);
            Assert.AreEqual(Expected, result);
        }

        /// <summary>
        /// The indent 5.
        /// </summary>
        [TestMethod]
        public void Indent5()
        {
            var checker = new IndentChecker();
            const string Line = "     fan speed auto";
            const int Expected = 5;
            var result = checker.GetIndentLevel(Line);
            Assert.AreEqual(Expected, result);
        }

        #endregion
    }
}