using IcsManagerLibrary;
using NETCONLib;
using System;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace IcsManagerGUI
{
    public partial class IcsManagerForm : Form
    {

        public IcsManagerForm()
        {
            InitializeComponent();
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormSharingManager_Load(object sender, EventArgs e)
        {
            try
            {
                RefreshConnections();
            }
            catch (UnauthorizedAccessException)
            {
                _ = MessageBox.Show("Please restart this program with administrative priviliges.");
                Close();
            }
            catch (NotImplementedException)
            {
                _ = MessageBox.Show("This program is not supported on your operating system.");
                Close();
            }
        }

        private void AddNic(NetworkInterface nic)
        {
            ConnectionItem connItem = new ConnectionItem(nic);
            _ = cbSharedConnection.Items.Add(connItem);
            _ = cbHomeConnection.Items.Add(connItem);
            INetConnection netShareConnection = connItem.Connection;
            if (netShareConnection != null)
            {
                INetSharingConfiguration sc = IcsManager.GetConfiguration(netShareConnection);
                if (sc.SharingEnabled)
                {
                    switch (sc.SharingConnectionType)
                    {
                        case tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PUBLIC:
                            cbSharedConnection.SelectedIndex = cbSharedConnection.Items.Count - 1;
                            break;
                        case tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PRIVATE:
                            cbHomeConnection.SelectedIndex = cbSharedConnection.Items.Count - 1;
                            break;
                    }
                }
            }
        }

        private void RefreshConnections()
        {
            cbSharedConnection.Items.Clear();
            cbHomeConnection.Items.Clear();
            foreach (NetworkInterface nic in IcsManager.GetAllIPv4Interfaces())
            {
                AddNic(nic);
            }
        }

        private void ButtonApply_Click(object sender, EventArgs e)
        {
            if ((!(cbSharedConnection.SelectedItem is ConnectionItem sharedConnectionItem)) || (!(cbHomeConnection.SelectedItem is ConnectionItem homeConnectionItem)))
            {
                _ = MessageBox.Show(@"Please select both connections.");
                return;
            }
            if (sharedConnectionItem.Connection == homeConnectionItem.Connection)
            {
                _ = MessageBox.Show(@"Please select different connections.");
                return;
            }
            IcsManager.ShareConnection(sharedConnectionItem.Connection, homeConnectionItem.Connection);
            RefreshConnections();
        }

        private void cbHomeConnection_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonStopSharing_Click(object sender, EventArgs e)
        {
            IcsManager.ShareConnection(null, null);
            RefreshConnections();
        }
    }
}
