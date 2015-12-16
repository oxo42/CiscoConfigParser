// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SectionDelimiterTests.cs" company="John Oxley">
//   2012
// </copyright>
// <summary>
//   Section delimiter tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CiscoConfigParser.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Section delimiter tests.
    /// </summary>
    [TestClass]
    public class SectionDelimiterTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// The test not delim.
        /// </summary>
        [TestMethod]
        public void TestNotDelim()
        {
            var checker = new SectionDelimiter();
            const string Line = " vlan batch 10 to 12 99 1662 2126 2252 to 2253";
            var result = checker.IsSectionDelimiter(Line);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// The test Huawei Delimiter
        /// </summary>
        [TestMethod]
        public void TestHuaweiDelim()
        {
            var checker = new SectionDelimiter();
            const string Line = "#";
            var result = checker.IsSectionDelimiter(Line);
            Assert.IsTrue(result);
        }

        /// <summary>
        /// The test Cisco Delimiter
        /// </summary>
        [TestMethod]
        public void TestCiscoDelim()
        {
            var checker = new SectionDelimiter();
            const string Line = "!";
            var result = checker.IsSectionDelimiter(Line);
            Assert.IsTrue(result);
        }

        #endregion
    }
}