using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using BluetoothHelperWin;
using WiimoteLib;

namespace NeuroExDevices
{
    public class DeviceManager
    {
        int interval = 500;
        Timer t = null;
        object gate = new object();

        BluetoothHelper btHelper = new BluetoothHelper();
        Dictionary<string, NeuroExDevice> devices = new Dictionary<string, NeuroExDevice>();

        public DeviceManager()
        {
            t = new Timer(new TimerCallback(OnTimeDue), null, Timeout.Infinite, Timeout.Infinite);
        }

        public void EnableRefresh()
        {
            t.Change(interval, interval);
        }

        public void DisableRefresh()
        {
            t.Change(Timeout.Infinite, Timeout.Infinite);
        }

        public void OnTimeDue(object state)
        {
            bool bLock = Monitor.TryEnter(gate);

            if (bLock)
            {
                try
                {
                    FindBluetoothDevices();
                    FindWiiDevices();

                    // Loop through the devices and connect to them if they are happy
                }
                catch (Exception ex)
                {
                    int i = 0;
                }
            }
        }

        public void FindBluetoothDevices()
        {
            // Get the bluetooth status of a device
            var btRadios = btHelper.GetRadios();  // TODO: Make the radios refresh much slower
            if (btRadios != null && btRadios.Count > 0)
            {
                var btDevices = btHelper.DiscoverDevices(btRadios[0]); // TODO: Make able to use another radio

                if (btDevices != null && btDevices.Count > 0)
                {
                    foreach (BluetoothDevice dev in btDevices)
                    {
                        if (dev.Name.StartsWith("Nin"))
                        {
                            string addr = string.Format("{0:x2}{1:x2}{2:x2}{3:x2}{4:x2}{5:x2}", dev.Address[5], dev.Address[4], dev.Address[3], dev.Address[2], dev.Address[1], dev.Address[0]);

                            if (devices.ContainsKey(addr))
                                devices[addr].BluetoothAddress = addr;
                            else
                                devices[addr] = new NeuroExDevice() { BluetoothAddress = addr };
                        }
                    }
                }
            }
        }

        public void FindWiiDevices()
        {
            // Get the HID status of a device
            WiimoteCollection motes = new WiimoteCollection();
            try { motes.FindAllWiimotes(); }
            catch (Exception) { }

            foreach (Wiimote mote in motes)
            {
                string serial = mote.HIDDeviceSerial;

                NeuroExDevice device = null;
                if (!devices.ContainsKey(serial))
                {
                    device = new NeuroExDevice();
                    devices[serial] = device;
                }

                device.HIDSerial = serial;
                device.Device = mote;
            }
        }

        public void ConnectDevices()
        {
            foreach (var dev in devices.Values)
            {
                if ((dev.Type == NeuroExDeviceType.WiiDevice) && (dev.Connected == false))
                {
                    ((Wiimote)dev.Device).Connect();
                }
            }
        }
    }

    public enum NeuroExDeviceType
    {
        WiiDevice,
        AccuswayPlus
    }

    public class NeuroExDevice
    {
        public string BluetoothAddress;
        public string HIDPath;
        public string HIDSerial;
        public string COMPort;
        public string Name;
        public bool Connected = false;
        public NeuroExDeviceType Type; // Nintendo vs ASP vs OTHER
        public object Device;
    }
}
