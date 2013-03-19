using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiimoteLib;

namespace AgileMedicine.MovementStudioForms
{
	public class WiiBalanceBoardManager : IDevice
	{
		int count = 0;
		private WiiBalanceBoardMeasurement calibration = new WiiBalanceBoardMeasurement();

		private CompleteMote managedMote;
		private bool collectData = false;

		private List<WiiBalanceBoardMeasurement> measurements = new List<WiiBalanceBoardMeasurement>();

		public WiiBalanceBoardManager(CompleteMote mote)
		{
			managedMote = mote;
		}

		public void Connect()
		{
			managedMote.mote.Connect();
			managedMote.mote.SetLEDs(true, false, false, false);

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

				WiiBalanceBoardMeasurement meas = new WiiBalanceBoardMeasurement()
				{
					Ticks = TimeManager.GetTicks(),
					TopLeft = ws.BalanceBoardState.SensorValuesRaw.TopLeft,
					TopRight = ws.BalanceBoardState.SensorValuesRaw.TopRight,
					BottomLeft = ws.BalanceBoardState.SensorValuesRaw.BottomLeft,
					BottomRight = ws.BalanceBoardState.SensorValuesRaw.BottomRight
				};

				lock (measurements)
				{
					measurements.Add(meas);
				}
			}
		}

		public WiiBalanceBoardMeasurementGroup GetLatestMeasurements(WiiBalanceBoardMeasurementGroup oldGrp)
		{
			// TODO: Make this use a reader / writer lock so that the update can sync all writers
			lock (measurements)
			{
				int startIdx = oldGrp != null && oldGrp.Measurements != null ? oldGrp.StartIndex + oldGrp.Measurements.Length : 0;
				//if (oldGrp != null && oldGrp.Measurements != null)
				//	startIdx++;

				WiiBalanceBoardMeasurementGroup newGrp = new WiiBalanceBoardMeasurementGroup();

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
				WiiBalanceBoardMeasurement meas = measurements[measurements.Count - 1];

				calibration.TopLeft += (meas.TopLeft - calibration.TopLeft) / (count + 1);
				calibration.TopRight += (meas.TopRight - calibration.TopRight) / (count + 1);
				calibration.BottomLeft += (meas.BottomLeft - calibration.BottomLeft) / (count + 1);
				calibration.BottomRight += (meas.BottomRight - calibration.BottomRight) / (count + 1);

				count++;
			}
		}

		public WiiBalanceBoardMeasurement GetCalibration()
		{
			return calibration;
		}

		public object GetSample()
		{
			if (measurements.Count > 0)
				return measurements[measurements.Count - 1];
			else
				return null;
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

	public class WiiBalanceBoardMeasurement
	{
		public long Ticks;
		public int TopLeft;
		public int TopRight;
		public int BottomLeft;
		public int BottomRight;

		// length between board sensors
		private const float BSL = 43;
		// width between board sensors
		private const float BSW = 24;

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

			if(Fz >= 50)
			{
				pt.X = ((BSW / 2) * (float)(topRight + bottomRight - topLeft - bottomLeft)) / (float)Fz;
				pt.Y = ((BSL / 2) * (float)(bottomLeft + bottomRight - topLeft - topRight)) / (float)Fz;
			}

			pt.Y *= -1;

			return pt;
		}

		public PointF COP()
		{
			return COP(TopLeft, TopRight, BottomLeft, BottomRight);
		}

		public PointF COP(WiiBalanceBoardMeasurement cal)
		{
			return COP(TopLeft - cal.TopLeft, TopRight - cal.TopRight, BottomLeft - cal.BottomLeft, BottomRight - cal.BottomRight);
		}
	}

	public class WiiBalanceBoardMeasurementGroup
	{
		public int StartIndex;
		public WiiBalanceBoardMeasurement[] Measurements = new WiiBalanceBoardMeasurement[0];
	}
}
