using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO.Ports;

namespace AgileMedicine.MovementStudioForms
{
	/*
	 * 
	 * lst6DOF.Items.Clear();

			Atomic6DOF a6d = new Atomic6DOF();

			a6d.Open();

			try
			{
				a6d.StartData();
				System.Threading.Thread.Sleep(5000);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
			finally
			{
				a6d.StopData();
				a6d.Close();
			}

			using (StreamWriter sw = new StreamWriter("c:\\users\\brent\\desktop\\dof.txt"))
			{
				sw.WriteLine("Ticks:\tX:\tY:\tZ:\tP:\tR:\tW:");

				foreach (ReadResult rr in a6d.Results)
				{
					int x = 0;
					x ^= rr.Vals[3];
					x <<= 8;
					x ^= rr.Vals[4];

					int y = 0;
					y ^= rr.Vals[5];
					y <<= 8;
					y ^= rr.Vals[6];

					int z = 0;
					z ^= rr.Vals[7];
					z <<= 8;
					z ^= rr.Vals[8];

					int p = 0;
					p ^= rr.Vals[9];
					p <<= 8;
					p ^= rr.Vals[10];

					int r = 0;
					r ^= rr.Vals[11];
					r <<= 8;
					r ^= rr.Vals[12];

					int w = 0;
					w ^= rr.Vals[13];
					w <<= 8;
					w ^= rr.Vals[14];

					string str = string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}", rr.Timestamp.Ticks, x.ToString(), y.ToString(), z.ToString(), p.ToString(), r.ToString(), w.ToString());

					sw.WriteLine(str);

					lst6DOF.Items.Add(str);
				}

			}
	 * 
	 */

	class Atomic6DOF
	{
		// TODO: Make this class multi disc aware

		private SerialPort port;
		private byte[] buf = new byte[1048];

		private Stack<ReadResult> results = new Stack<ReadResult>();
		public Stack<ReadResult> Results
		{
			get { return results; }
		}

		public Atomic6DOF()
		{}

		public void Open()
		{
			if (port == null)
			{
				port = new SerialPort("COM1");
				port.DataBits = 8;
				port.StopBits = StopBits.One;
				port.Handshake = Handshake.RequestToSend;
				port.Parity = Parity.None;
				port.BaudRate = 115200;
				port.ReadBufferSize = 1024 * 1000;

				port.ErrorReceived += new SerialErrorReceivedEventHandler(port_ErrorReceived);

				port.Open();
			}
			else
				throw new Exception("Must close first");
		}

		public void StartData()
		{
			port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
			port.Write(new byte[] { 0x23 }, 0, 1);
		}

		public void StopData()
		{
			port.Write(new byte[] { 0x20 }, 0, 1);
			port.DataReceived -= new SerialDataReceivedEventHandler(port_DataReceived);
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
			int nRead = port.Read(buf, 0, 1048);
			if (nRead > 0)
			{
				for(int i=0; i<nRead; i++)
				{
					if ((int)buf[i] == 65 && (Results.Count == 0 || Results.Peek().Vals.Count == 0 || Results.Peek().Vals.Count >= 16))
					{
						ReadResult res = new ReadResult();
						res.Vals.Add(buf[i]);
						results.Push(res);
					}
					else
					{
						if (Results.Count == 0)
							results.Push(new ReadResult());

						results.Peek().Vals.Add(buf[i]);
					}
				}
			}
		}
	}

	public class ReadResult
	{
		public DateTime Timestamp = DateTime.Now;
		public List<byte> Vals = new List<byte>();
	}
}
