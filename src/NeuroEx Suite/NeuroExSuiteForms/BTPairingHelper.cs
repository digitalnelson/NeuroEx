using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using BluetoothHelperWin;

namespace AgileMedicine.MovementStudioForms
{
	public partial class BTPairingHelper : Form
	{
		BluetoothHelper btHelper = new BluetoothHelper();
		List<BluetoothRadio> btRadios = new List<BluetoothRadio>();
		List<BluetoothDevice> btDevices = new List<BluetoothDevice>();

		public BTPairingHelper()
		{
			InitializeComponent();
		}

		private void btnFindDevices_Click(object sender, EventArgs e)
		{
			lstDevices.Items.Clear();

			btRadios = btHelper.GetRadios();
			if (btRadios != null && btRadios.Count > 0)
			{
				btDevices = btHelper.DiscoverDevices(btRadios[0]);

				if (btDevices != null && btDevices.Count > 0)
				{
					foreach (BluetoothDevice dev in btDevices)
					{
						lstDevices.Items.Add(new DeviceListItem() { Device = dev });
					}
				}
			}
		}

		private void btnPair_Click(object sender, EventArgs e)
		{
			DeviceListItem dev = (DeviceListItem)lstDevices.SelectedItem;

			if (lstDevices.SelectedIndex != -1 && dev != null)
			{
				if (dev.Device.Authenticated)
					MessageBox.Show("Device already paired");
				else
				{
					btHelper.PairWithDevice(btRadios[0], dev.Device, btRadios[0].Address.ToArray());
				}
			}
		}

		private void btnDisconnect_Click(object sender, EventArgs e)
		{
			DeviceListItem dev = (DeviceListItem)lstDevices.SelectedItem;

			if (lstDevices.SelectedIndex != -1 && dev != null)
			{
				btHelper.DisconnectDevice(btRadios[0], dev.Device);
			}
		}

		private void btnConnect_Click(object sender, EventArgs e)
		{
			DeviceListItem dev = (DeviceListItem)lstDevices.SelectedItem;

			if (lstDevices.SelectedIndex != -1 && dev != null)
			{
				try
				{
					btHelper.ConnectDevice(btRadios[0], dev.Device);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
				}
			}
		}

		private void btnFindDevices_Click_1(object sender, EventArgs e)
		{

		}

		private void btnPair_Click_1(object sender, EventArgs e)
		{

		}

		private void btnConnect_Click_1(object sender, EventArgs e)
		{

		}
	}

	public class DeviceListItem
	{
		public BluetoothDevice Device;

		public override string ToString()
		{
			string devStr = Device.Name;

			if (Device.Authenticated)
				devStr += " - Authed";
			if (Device.Connected)
				devStr += " - Connected";
			if (Device.Remembered)
				devStr += " - Remembered";

			return devStr;
		}
	}
}
