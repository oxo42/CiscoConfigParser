// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="John Oxley">
//   2012
// </copyright>
// <summary>
//   The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Play
{
    using System;
    using System.IO;
    using System.Linq;

    using CiscoConfigParser;

    /// <summary>
    /// The program.
    /// </summary>
    internal class Program
    {
        #region Public Methods and Operators

        /// <summary>
        /// The test.
        /// </summary>
        public void Test()
        {
            var config = new CiscoConfig("HuaweiRouter.txt");

            // Get all Gig E Interfaces that are VPN's
            var gigInterfaces = config.Filter(l => l.StartsWith("interface GigabitEthernet")).Where(i => i.HasChild(l => l.StartsWith("ip binding vpn-instance")));

            foreach (var gigInterface in gigInterfaces)
            {
                var vpnName = gigInterface.Filter(l => l.StartsWith("ip binding vpn-instance")).First().GetToken(3);

                Console.WriteLine(vpnName);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args. 
        /// </param>
        private static void Main(string[] args)
        {
            var lines = File.ReadAllLines("HuaweiRouter.txt");
            var config = new CiscoConfig();
            config.Parse(lines);
            File.WriteAllText("HuaweiOutput.txt", config.ToString());

            // Get all Gig E Interfaces that are VPN's
            var gigEInterfaces = config.Filter(l => l.StartsWith("interface GigabitEthernet")).Where(i => i.HasChild(l => l.StartsWith("ip binding vpn-instance")));

            foreach (var gigInterface in gigEInterfaces)
            {
                var vpnName = gigInterface.Filter(l => l.StartsWith("ip binding vpn-instance")).First().GetToken(3);

                var inboundSpeed = "Max";
                var outboundSpeed = "Max";
                var inboundItem =
                gigInterface.Filter(l => l.StartsWith("qos car cir") && l.EndsWith("inbound")).FirstOrDefault();
                if (inboundItem != null)
                {
                    inboundSpeed = inboundItem.GetToken(3);
                }

                var outboundItem =
                gigInterface.Filter(l => l.StartsWith("qos car cir") && l.EndsWith("outbound")).FirstOrDefault();
                if (outboundItem != null)
                {
                    outboundSpeed = outboundItem.GetToken(3);
                }

                Console.WriteLine("{0}: {1} in, {2} out", vpnName, inboundSpeed, outboundSpeed);
            }

            Console.WriteLine("Done.");
            Console.ReadKey();
        }

        #endregion
    }
}