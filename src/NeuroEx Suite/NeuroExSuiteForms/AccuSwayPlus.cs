using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO.Ports;

namespace AgileMedicine.MovementStudioForms
{
	class AccuSwayPlus
	{
		/*AccuSwayPlus asp = new AccuSwayPlus();

			string aspSerial = "";
			byte[] aspZeros = null;

			asp.Open();
			try
			{
				aspSerial = asp.GetSerialNumber();
				aspZeros = asp.Autozero();
				asp.StartData();

				System.Threading.Thread.Sleep(10000);

				asp.StopData();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
			finally
			{
				asp.Close();
			}

			byte[] sessData = asp.SessionData.ToArray();*/

		// TODO: Make this class multi board aware

		private SerialPort port;

		private PortState portState = PortState.None;
		public PortState PortState
		{
			get { return portState; }
		}

		private List<byte> sessionData = new List<byte>();
		public List<byte> SessionData
		{
			get { return sessionData; }
		}

		public AccuSwayPlus()
		{}

		public void Open()
		{
			if (port == null)
			{
				port = new SerialPort("COM1");
				port.DataBits = 8;
				port.StopBits = StopBits.One;
				port.Handshake = Handshake.None;
				port.BaudRate = 57600;
				port.ReadBufferSize = 1024 * 1000;

				port.ErrorReceived += new SerialErrorReceivedEventHandler(port_ErrorReceived);

				port.Open();
			}
			else
				throw new Exception("Must close first");
		}

		public void SetDataRate(DataRate rate)
		{
			switch (rate)
			{
				case DataRate.Hz50B57600:
					{
						port.Write(new byte[] { 0x72 }, 0, 1); // Set baud and data rate to 57.6 and 50hz
						int resp = port.ReadByte();
						if (resp != 0x72)
							throw new Exception("Error setting data rate: " + resp.ToString());
						break;
					}
				case DataRate.Hz100B115200:
					{
						port.Write(new byte[] { 0x66 }, 0, 1); // Set baud and data rate to 115.2 and 100hz
						int resp = port.ReadByte();
						if (resp != 0x66)
							throw new Exception("Error setting data rate: " + resp.ToString());
						break;
					}
			}
		}

		private byte[] readFromPort(int cbToRead)
		{
			int nRead = 0;
			byte[] buf = new byte[cbToRead];
			byte[] tmpBuf = new byte[cbToRead];

			while (nRead < cbToRead)
			{
				tmpBuf.Initialize();

				int n = port.Read(tmpBuf, 0, cbToRead - nRead);

				Array.Copy(tmpBuf, 0, buf, nRead, n);

				nRead += n;
			}

			return buf;
		}

		public string GetSerialNumber()
		{
			port.Write(new byte[] { 0x58 }, 0, 1);

			int resp = port.ReadByte();
			if (resp != 0x78)
				throw new Exception("Error getting serialnumber: " + resp.ToString());

			byte[] buf = readFromPort(4);
			return Encoding.ASCII.GetString(buf, 0, 4);
		}

		public byte[] Autozero()
		{
			port.Write(new byte[] { 0x53 }, 0, 1);

			int resp = port.ReadByte();
			if (resp != 0x54)
				throw new Exception("Error with autozero: " + resp.ToString());

			byte[] buf = readFromPort(12);

			return buf;
		}

		public void StartData()
		{
			port.Write(new byte[] { 0x51 }, 0, 1);
			int resp = port.ReadByte(); // Ack
			if (resp != 0x55)
				throw new Exception("Error starting data transmission: " + resp.ToString());
			port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
		}

		public void StopData()
		{
			port.DataReceived -= new SerialDataReceivedEventHandler(port_DataReceived);
			port.Write(new byte[] { 0x52 }, 0, 1);  // Does not ack
		}

		public void Close()
		{
			if (port != null && port.IsOpen)
				port.Close();
		}

		void port_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
		{
			throw new Exception("Error received on port: " + e.EventType.ToString());
		}

		void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			string str = port.ReadExisting();
			sessionData.AddRange(Encoding.ASCII.GetBytes(str));
		}
	}

	public enum DataRate
	{
		Hz50B57600,
		Hz100B115200
	}

	public enum PortState
	{
		None,
		SetCommSpeed,
		Autozero,
		DataTransmission
	}
}
