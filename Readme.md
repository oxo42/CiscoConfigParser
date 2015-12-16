# Description

`CiscoConfigParser` is a .Net library that parses the output of `show run` on a Cisco router or `dis curr` on a Huawei.  It stores the information in a tree structure that can be queried with `.Filter` and `.HasChild`.

# Install
You can use the shiny [NuGet Package](https://nuget.org/packages/CiscoConfigParser) I made:

```powershell
Install-Package CiscoConfigParser
```

# Sample Code

```csharp
public void Test()
{
    var config = new CiscoConfig("HuaweiRouter.txt");
    
    // Get all Gig E Interfaces that are VPN's
    var gigInterfaces =
        config.Filter(l => l.StartsWith("interface GigabitEthernet")).Where(
            i => i.HasChild(l => l.StartsWith("ip binding vpn-instance")));

    foreach (var gigInterface in gigInterfaces)
    {
        var vpnName = gigInterface.Filter(l => l.StartsWith("ip binding vpn-instance")).First().GetToken(3);
        Console.WriteLine(vpnName);
    }
}

public void ComplexTest()
{
    var config = new CiscoConfig(this.Filename);

    // All interfaces that are VPN's and have a speed on them
    var interfaces = from i in config.Filter(l => l.StartsWith("interface GigabitEthernet"))
                        where
                            i.HasChild(l => l.StartsWith("ip binding vpn-instance"))
                            && i.HasChild(l => l.StartsWith("qos car cir") && l.EndsWith("inbound"))
                            && i.HasChild(l => l.StartsWith("qos car cir") && l.EndsWith("outbound"))
                        select i;

    // We only want VPNs that have a qos car cis inbound and outbound
    var vpns = from i in interfaces
                let name = i.Filter(l => l.StartsWith("ip binding vpn-instance")).First().GetToken(3)
                let inbound =
                    i.Filter(l => l.StartsWith("qos car cir") && l.EndsWith("inbound")).First().GetToken(3)
                let outbound =
                    i.Filter(l => l.StartsWith("qos car cir") && l.EndsWith("outbound")).First().GetToken(3)
                select new {sysname, name, long.Parse(inbound), long.Parse(outbound)};
}

```

# Dev notes

I have deleted the HuaweiRouter.txt file used in unit tests because it had some potentially sensitive information.  I'm eager for pull requests to fix the tests.
