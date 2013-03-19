using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiimoteLib;
using System.Xml.Serialization;

namespace AgileMedicine.MovementStudioForms
{
	public class WiimoteManager : IDevice
	{
		private CompleteMote managedMote;
		private bool collectData = false;
		private int moteIndex;

		private List<WiimoteMeasurement> measurements = new List<WiimoteMeasurement>();

		public WiimoteManager(CompleteMote mote, int wiimoteIndex)
		{
			managedMote = mote;
			moteIndex = wiimoteIndex;
		}

		public void Connect()
		{
			managedMote.mote.Connect();
			managedMote.mote.SetReportType(InputReport.ExtensionAccel, true);

			switch (moteIndex)
			{
				case 0:
					{
						managedMote.mote.SetLEDs(true, false, false, false);
						break;
					}
				case 1:
					{
						managedMote.mote.SetLEDs(false, true, false, false);
						break;
					}
				case 2:
					{
						managedMote.mote.SetLEDs(false, false, true, false);
						break;
					}
				case 3:
					{
						managedMote.mote.SetLEDs(false, false, false, true);
						break;
					}
			}

			managedMote.mote.InitializeMotionPlus();

			managedMote.mote.WiimoteChanged += new EventHandler<WiimoteChangedEventArgs>(managedMote_WiimoteChanged);
		}

		public void Start()
		{
			collectData = true;
		}

		void managedMote_WiimoteChanged(object sender, WiimoteChangedEventArgs e)
		{
			if (collectData)
			{
				WiimoteState ws = e.WiimoteState;

				WiimoteMeasurement meas = new WiimoteMeasurement()
				{
					Id = moteIndex.ToString(),
					Ticks = TimeManager.GetTicks(),
					AccelX = ws.AccelState.RawValues.X,
					AccelY = ws.AccelState.RawValues.Y,
					AccelZ = ws.AccelState.RawValues.Z,
					GyroX = ws.MotionPlusState.RawValues.X,
					GyroY = ws.MotionPlusState.RawValues.Y,
					GyroZ = ws.MotionPlusState.RawValues.Z
				};

				lock (measurements)
				{
					measurements.Add(meas);
				}
			}
		}

		public void Calibrate()
		{
		}

		public object GetSample()
		{
			if (measurements.Count > 0)
				return measurements[measurements.Count - 1];
			else
				return null;
		}

		public WiimoteMeasurement GetLatestMeasurement()
		{
			// TODO: Make this use a reader / writer lock so that the update can sync all writers
			//lock (measurements)
			//{
			//    if (measurements.Count > 0)
			//        return measurements.Peek();
			//    else
			        return null;
			//}
		}

		public WiimoteMeasurementGroup GetLatestMeasurements(WiimoteMeasurementGroup oldGrp)
		{
			// TODO: Make this use a reader / writer lock so that the update can sync all writers
			lock (measurements)
			{
				int startIdx = (oldGrp != null && oldGrp.Measurements != null) ? oldGrp.StartIndex + oldGrp.Measurements.Length : 0;
				//if (oldGrp != null && oldGrp.Measurements != null)
				//	startIdx++;

				WiimoteMeasurementGroup newGrp = new WiimoteMeasurementGroup();

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

		public void Stop()
		{
			collectData = false;
		}

		public void Disconnect()
		{
			try
			{
				collectData = false;
				managedMote.mote.Disconnect();
			}
			catch (Exception)
			{ }
		}
	}

	public class WiimoteMeasurement
	{
		[XmlAttribute()]
		public String Id;
		[XmlAttribute()]
		public long Ticks;
		[XmlAttribute()]
		public float AccelX;
		[XmlAttribute()]
		public float AccelY;
		[XmlAttribute()]
		public float AccelZ;
		[XmlAttribute()]
		public float GyroX;
		[XmlAttribute()]
		public float GyroY;
		[XmlAttribute()]
		public float GyroZ;
	}

	public class WiimoteMeasurementGroup
	{
		public int StartIndex;
		public WiimoteMeasurement[] Measurements = new WiimoteMeasurement[0];
	}
}
