using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO.Ports;
using WiimoteLib;
using System.Diagnostics;

namespace AgileMedicine.MovementStudioForms
{
	public class AccuSwayPlusManager : IDevice
	{
		private SerialPort port;
		private bool collectData = false;

		private int count;
		private AccuSwayPlusMeasurement calibration = new AccuSwayPlusMeasurement();
		private List<AccuSwayPlusMeasurement> measurements = new List<AccuSwayPlusMeasurement>();

		public PortState PortState { get; set; }

		public AccuSwayPlusManager()
		{}

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

		public void Connect()
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

		public void Start()
		{
			collectData = true;

			port.Write(new byte[] { 0x51 }, 0, 1);

			int resp = port.ReadByte(); // Ack
			if (resp != 0x55)
				throw new Exception("Error starting data transmission: " + resp.ToString());

			port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
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

		void port_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
		{
			throw new Exception("Error received on port: " + e.EventType.ToString());
		}

		byte[] nbuf = new byte[128];

        int prevIdx = -1;
        int prevChannel = -1;
        int nextIdx = 0;
        byte prevByte;

        // TODO: These need to be changed to 12bit words
        int[] frame = new int[12];

		void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
            try
            {
                if (collectData)
                {
                    int nRead = port.Read(nbuf, 0, 128);
                    while (nRead > 0)
                    {
                        for (int i = 0; i < nRead; i++)
                        {
                            byte b = nbuf[i];

                            int data = b & 0x0F;
                            data <<= 8;
                            data |= prevByte;

                            int channel = b & 0xF0;
                            channel >>= 4;

                            if ((channel == nextIdx) && (prevChannel != prevIdx))
                            {
                                frame[nextIdx] = data;
                                prevIdx = nextIdx;
                                nextIdx++;

								if (channel == 11)
								{
									#region Accumulate data
									AccuSwayPlusMeasurement meas = new AccuSwayPlusMeasurement()
									{
										C1 = frame[0],
										C2 = frame[1],
										C3 = frame[2],
										C4 = frame[3],
										C5 = frame[4],
										C6 = frame[5],
										C7 = frame[6],
										C8 = frame[7],
										C9 = frame[8],
										C10 = frame[9],
										C11 = frame[10],
										C12 = frame[10]
									};

									Debug.WriteLine(meas.ToString());

									lock (measurements)
									{
										measurements.Add(meas);
									}
									#endregion

									nextIdx = 0;
									Array.Clear(frame, 0, frame.Length);
								}
                            }

                            prevByte = b;
                            prevChannel = channel;
                        }

                        nRead = port.Read(nbuf, 0, 128);
                    }
                }
            }
            catch (Exception ex)
            {
            }
		}

		public object GetSample()
		{
			if (measurements.Count > 0)
				return measurements[measurements.Count - 1];
			else
				return null;
		}

		public AccuSwayPlusMeasurement GetCalibration()
		{
			return calibration;
		}

		public AccuSwayPlusMeasurementGroup GetLatestMeasurements(AccuSwayPlusMeasurementGroup oldGrp)
		{
			lock (measurements)
			{
				int startIdx = oldGrp != null && oldGrp.Measurements != null ? oldGrp.StartIndex + oldGrp.Measurements.Length : 0;

				AccuSwayPlusMeasurementGroup newGrp = new AccuSwayPlusMeasurementGroup();

				if (measurements.Count > 0)
				{
					newGrp.StartIndex = startIdx;
					newGrp.Measurements = measurements.GetRange(startIdx, measurements.Count - startIdx).ToArray();
				}
				else
				{
					newGrp.StartIndex = 0;
					newGrp.Measurements = null;
				}

				return newGrp;
			}
		}

		public void Calibrate()
		{
			if (measurements.Count > 0)
			{
				AccuSwayPlusMeasurement meas = measurements[measurements.Count - 1];

				calibration.C1 += (meas.C1 - calibration.C1) / (count + 1);
				calibration.C2 += (meas.C2 - calibration.C2) / (count + 1);
				calibration.C3 += (meas.C3 - calibration.C3) / (count + 1);
				calibration.C4 += (meas.C4 - calibration.C4) / (count + 1);
				calibration.C5 += (meas.C5 - calibration.C5) / (count + 1);
				calibration.C6 += (meas.C6 - calibration.C6) / (count + 1);
				calibration.C7 += (meas.C7 - calibration.C7) / (count + 1);
				calibration.C8 += (meas.C8 - calibration.C8) / (count + 1);
				calibration.C9 += (meas.C9 - calibration.C9) / (count + 1);
				calibration.C10 += (meas.C10 - calibration.C10) / (count + 1);
				calibration.C11 += (meas.C11 - calibration.C11) / (count + 1);
				calibration.C12 += (meas.C12 - calibration.C12) / (count + 1);

				count++;
			}
		}

		public void Stop()
		{
			collectData = false;

			port.DataReceived -= new SerialDataReceivedEventHandler(port_DataReceived);
			port.Write(new byte[] { 0x52 }, 0, 1);  // Does not ack
		}

		public void Disconnect()
		{
			try
			{
				collectData = false;

				if (port != null && port.IsOpen)
					port.Close();
			}
			catch (Exception)
			{ }
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

	public class AccuSwayPlusMeasurement
	{
		public long Ticks;
		public int C1;
		public int C2;
		public int C3;
		public int C4;
		public int C5;
		public int C6;
		public int C7;
		public int C8;
		public int C9; 
		public int C10;
		public int C11;
		public int C12;

		// length between board sensors
		private const float BSL = 43;
		// width between board sensors
		private const float BSW = 24;

		public override string ToString()
		{
			return string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11}", C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12);
		}

		public PointF COP(int topLeft, int topRight, int bottomLeft, int bottomRight)
		{
			PointF pt;
			pt.X = 0;
			pt.Y = 0;

			//float Kx = (TopLeft + BottomLeft) / (TopRight + BottomRight);
			//float Ky = (TopLeft + TopRight) / (BottomLeft + BottomRight);

			//pt.X = ((float)(Kx - 1) / (float)(Kx + 1)) * (float)(-BSL / 2);
			//pt.Y = ((float)(Ky - 1) / (float)(Ky + 1)) * (float)(-BSW / 2);

			int Fz = topLeft + topRight + bottomLeft + bottomRight;

			if (Fz >= 50)
			{
				pt.X = ((BSW / 2) * (float)(topRight + bottomRight - topLeft - bottomLeft)) / (float)Fz;
				pt.Y = ((BSL / 2) * (float)(bottomLeft + bottomRight - topLeft - topRight)) / (float)Fz;
			}

			pt.Y *= -1;

			return pt;
		}

		public PointF COP()
		{
			//return COP(TopLeft, TopRight, BottomLeft, BottomRight);
			return COP(0, 0, 0, 0);
		}

		public PointF COP(AccuSwayPlusMeasurement cal)
		{
			//return COP(TopLeft - cal.TopLeft, TopRight - cal.TopRight, BottomLeft - cal.BottomLeft, BottomRight - cal.BottomRight);
			return COP(0, 0, 0, 0);
		}
	}

	public class AccuSwayPlusMeasurementGroup
	{
		public int StartIndex;
		public AccuSwayPlusMeasurement[] Measurements = new AccuSwayPlusMeasurement[0];
	}
}
