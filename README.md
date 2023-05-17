# IcsManagerLibrary

This is a C# library that provides functionality for managing Internet Connection Sharing (ICS) on Windows systems. It allows you to retrieve network interfaces, get information about shared connections, enable or disable sharing, and perform cleanup operations. It is built based on the project utapyngo/icsmanager, with some modifications and improvements.

All contributions and thanks should be to the original author, utapyngo, for their work on the project.

## Usage

To use this library, follow the steps below:

1. Include the `IcsManagerLibrary` namespace in your code:

   ```csharp
   using IcsManagerLibrary;
   ```

2. Retrieve IPv4 Ethernet and Wireless network interfaces:

   ```csharp
   IEnumerable<NetworkInterface> interfaces = IcsManager.GetIPv4EthernetAndWirelessInterfaces();
   ```

   This method returns a collection of `NetworkInterface` objects that support IPv4 and are of type Ethernet, Wireless80211, or GigabitEthernet.

3. Retrieve all IPv4 network interfaces:

   ```csharp
   IEnumerable<NetworkInterface> interfaces = IcsManager.GetAllIPv4Interfaces();
   ```

   This method returns a collection of all network interfaces that support IPv4.

4. Get information about currently shared connections:

   ```csharp
   NetShare sharedConnections = IcsManager.GetCurrentlySharedConnections();
   ```

   This method returns a `NetShare` object that contains information about the shared connections. The `NetShare` object has properties `SharedConnection` and `HomeConnection` that represent the shared and home connections, respectively.

5. Share a connection:

   ```csharp
   IcsManager.ShareConnection(connectionToShare, homeConnection);
   ```

   This method enables Internet Connection Sharing between the specified `connectionToShare` and `homeConnection` interfaces. Note that the connections must be different.

6. Clean up WMI sharing entries:

   ```csharp
   IcsManager.CleanupWMISharingEntries();
   ```

   This method removes any existing Internet Connection Sharing entries in the Windows Management Instrumentation (WMI) repository.

7. Additional helper methods are available for retrieving configuration and properties of connections:

   - `GetConfiguration`: Get the sharing configuration for a given connection.
   - `GetProperties`: Get the properties of a given connection.

## Example

Here's an example that demonstrates the usage of the `IcsManagerLibrary`:

```csharp
using System;
using IcsManagerLibrary;

public class Program
{
    public static void Main()
    {
        // Retrieve and display IPv4 Ethernet and Wireless interfaces
        IEnumerable<NetworkInterface> interfaces = IcsManager.GetIPv4EthernetAndWirelessInterfaces();
        foreach (NetworkInterface nic in interfaces)
        {
            Console.WriteLine("Interface Name: " + nic.Name);
            Console.WriteLine("Interface Description: " + nic.Description);
            Console.WriteLine("Interface Type: " + nic.NetworkInterfaceType);
            Console.WriteLine();
        }

        // Share a connection
        INetConnection connectionToShare = IcsManager.GetConnectionByName("Ethernet");
        INetConnection homeConnection = IcsManager.GetConnectionByName("Wi-Fi");
        IcsManager.ShareConnection(connectionToShare, homeConnection);

        // Get information about currently shared connections
        NetShare sharedConnections = IcsManager.GetCurrentlySharedConnections();
        Console.WriteLine("Shared Connection: " + sharedConnections.SharedConnection);
        Console.WriteLine("Home Connection: " + sharedConnections.HomeConnection);
    }
}
```

## Requirements

- This library requires the .NET Framework.
- It has been tested on Windows systems.