// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SectionTests.cs" company="John Oxley">
//   2012
// </copyright>
// <summary>
//   Section tests
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CiscoConfigParser.Tests
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Section tests
    /// </summary>
    [TestClass]
    [DeploymentItem("HuaweiRouter.txt")]
    public class SectionTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get token.
        /// </summary>
        [TestMethod]
        public void GetToken()
        {
            var section = new Section(null, "token1 token2 token3");
            var actual = section.GetToken(2);
            Assert.AreEqual("token2", actual);
        }

        /// <summary>
        /// The has child test.
        /// </summary>
        [TestMethod]
        public void HasChild()
        {
            var config = new CiscoConfig("HuaweiRouter.txt");
            var root = config.Filter(l => l == "interface GigabitEthernet1/0/3.1029").First();
            Assert.IsTrue(root.HasChild(l => l.StartsWith("ip binding vpn-instance")));
        }

        /// <summary>
        /// The has child with test.
        /// </summary>
        [TestMethod]
        public void HasChildWith()
        {
            var config = new CiscoConfig("HuaweiRouter.txt");
            var root = config.Filter(l => l == "interface GigabitEthernet1/0/3.1029").First();
            Assert.IsTrue(root.HasChildWith("ip binding vpn-instance"));
        }

        #endregion
    }
}