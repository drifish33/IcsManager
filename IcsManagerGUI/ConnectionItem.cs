using IcsManagerLibrary;
using NETCONLib;
using System.Net.NetworkInformation;

namespace IcsManagerGUI
{
    internal class ConnectionItem
    {
        public NetworkInterface Nic;

        public INetConnection Connection => IcsManager.GetConnectionById(Nic.Id);

        public ConnectionItem(NetworkInterface nic)
        {
            Nic = nic;
        }

        public override string ToString()
        {
            return Nic.Name;
        }
    }
}
