using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using WiimoteLib;

namespace AgileMedicine.MovementStudioForms
{
	public partial class Calibration : Form
	{
		private Wiimote wiimote;
		private bool isAwake = false;

		public Calibration()
		{
			InitializeComponent();
		}

		private void Calibration_Load(object sender, EventArgs e)
		{
		}

		private void btnSample_Click(object sender, EventArgs e)
		{
			try
			{
				WiimoteCollection coll = new WiimoteCollection();
				coll.FindAllWiimotes();

				foreach (Wiimote mote in coll)
				{
					mote.Connect();
					mote.WiimoteChanged += new EventHandler<WiimoteChangedEventArgs>(mote_WiimoteChanged);

					wiimote = mote;

					isAwake = true;
				}
			}
			catch (Exception)
			{
				MessageBox.Show("Could not find balance board.");
			}
		}

		private delegate void UpdateWiimoteStateDelegate(WiimoteChangedEventArgs args);
		private const float KG2LB = 2.20462262f;
		private float valCount = 0;
		private float valAdjUL = 0;
		private float valTotUL = 0;
		private float valAdjUR = 0;
		private float valTotUR = 0;
		private float valAdjBL = 0;
		private float valTotBL = 0;
		private float valAdjBR = 0;
		private float valTotBR = 0;

		private float valMvAvgUL = 0;
		private float valMvAvgUR = 0;
		private float valMvAvgLL = 0;
		private float valMvAvgLR = 0;
		private float valMvAvgTot = 0;

		private Queue<float> queueMvAvgUL = new Queue<float>();
		private Queue<float> queueMvAvgUR = new Queue<float>();
		private Queue<float> queueMvAvgLL = new Queue<float>();
		private Queue<float> queueMvAvgLR = new Queue<float>();
		private Queue<float> queueMvAvgTot = new Queue<float>();

		public short AdjustVal(short val, float adj)
		{
			return (short)(val - (int)adj);
		}

		private void UpdateWiimoteState(WiimoteChangedEventArgs args)
		{
			WiimoteState state = args.WiimoteState;
			BalanceBoardState bbs = state.BalanceBoardState;
			BalanceBoardSensors sens = bbs.SensorValuesRaw;
			BalanceBoardSensors cal0 = bbs.CalibrationInfo.Kg0;
			BalanceBoardSensors cal17 = bbs.CalibrationInfo.Kg17;
			BalanceBoardSensors cal34 = bbs.CalibrationInfo.Kg34;

			if (valCount < 500)
			{
				valTotUL += sens.TopLeft;
				valTotUR += sens.TopRight;
				valTotBL += sens.BottomLeft;
				valTotBR += sens.BottomRight;

				valCount++;

				valAdjUL = (valTotUL / valCount) - cal0.TopLeft;
				valAdjUR = (valTotUR / valCount) - cal0.TopRight;
				valAdjBL = (valTotBL / valCount) - cal0.BottomLeft;
				valAdjBR = (valTotBR / valCount) - cal0.BottomRight;
			}

			int rawUL = sens.TopLeft - (int)valAdjUL;
			int rawUR = sens.TopRight - (int)valAdjUR;
			int rawBL = sens.BottomLeft - (int)valAdjBL;
			int rawBR = sens.BottomRight - (int)valAdjBR;

			float valUL = GetBalanceBoardSensorValue((short)rawUL, cal0.TopLeft, cal17.TopLeft, cal34.TopLeft) * KG2LB;
			float valUR = GetBalanceBoardSensorValue((short)rawUR, cal0.TopRight, cal17.TopRight, cal34.TopRight) * KG2LB;
			float valBL = GetBalanceBoardSensorValue((short)rawBL, cal0.BottomLeft, cal17.BottomLeft, cal34.BottomLeft) * KG2LB;
			float valBR = GetBalanceBoardSensorValue((short)rawBR, cal0.BottomRight, cal17.BottomRight, cal34.BottomRight) * KG2LB;
			float valTotal = (valUL + valUR + valBL + valBR);

			int mvAvgWnd = 200;

			if (valCount >= 500)
			{
				if (queueMvAvgUL.Count < mvAvgWnd)
				{
					queueMvAvgUL.Enqueue(valUL);
					queueMvAvgUR.Enqueue(valUR);
					queueMvAvgLL.Enqueue(valBL);
					queueMvAvgLR.Enqueue(valBR);
					queueMvAvgTot.Enqueue(valTotal);
				}
				else
				{
					valMvAvgUL = valMvAvgUL - ((float)queueMvAvgUL.Dequeue() / mvAvgWnd) + ((float)valUL / mvAvgWnd);
					queueMvAvgUL.Enqueue((float)valUL);

					valMvAvgUR = valMvAvgUR - ((float)queueMvAvgUR.Dequeue() / mvAvgWnd) + ((float)valUR / mvAvgWnd);
					queueMvAvgUR.Enqueue((float)valUR);

					valMvAvgLL = valMvAvgLL - ((float)queueMvAvgLL.Dequeue() / mvAvgWnd) + ((float)valBL / mvAvgWnd);
					queueMvAvgLL.Enqueue((float)valBL);

					valMvAvgLR = valMvAvgLR - ((float)queueMvAvgLR.Dequeue() / mvAvgWnd) + ((float)valBR / mvAvgWnd);
					queueMvAvgLR.Enqueue((float)valBR);

					valMvAvgTot = valMvAvgTot - ((float)queueMvAvgTot.Dequeue() / mvAvgWnd) + ((float)valTotal / mvAvgWnd);
					queueMvAvgTot.Enqueue((float)valTotal);
				}
			}

			lblUL.Text = valUL.ToString();
			lblUR.Text = valUR.ToString();
			lblLL.Text = valBL.ToString();
			lblLR.Text = valBR.ToString();

			lblRawUL.Text = state.BalanceBoardState.SensorValuesRaw.TopLeft.ToString();
			lblRawUR.Text = state.BalanceBoardState.SensorValuesRaw.TopRight.ToString();
			lblRawLL.Text = state.BalanceBoardState.SensorValuesRaw.BottomLeft.ToString();
			lblRawLR.Text = state.BalanceBoardState.SensorValuesRaw.BottomRight.ToString();

			lblAvgUL.Text = valMvAvgUL.ToString();
			lblAvgUR.Text = valMvAvgUR.ToString();
			lblAvgLL.Text = valMvAvgLL.ToString();
			lblAvgLR.Text = valMvAvgLR.ToString();
			lblAvgTot.Text = valMvAvgTot.ToString();

			lblCalUL.Text = state.BalanceBoardState.CalibrationInfo.Kg0.TopLeft.ToString();
			lblCalUR.Text = state.BalanceBoardState.CalibrationInfo.Kg0.TopRight.ToString();
			lblCalLL.Text = state.BalanceBoardState.CalibrationInfo.Kg0.BottomLeft.ToString();
			lblCalLR.Text = state.BalanceBoardState.CalibrationInfo.Kg0.BottomRight.ToString();

			lblCalUL17.Text = state.BalanceBoardState.CalibrationInfo.Kg17.TopLeft.ToString();
			lblCalUR17.Text = state.BalanceBoardState.CalibrationInfo.Kg17.TopRight.ToString();
			lblCalLL17.Text = state.BalanceBoardState.CalibrationInfo.Kg17.BottomLeft.ToString();
			lblCalLR17.Text = state.BalanceBoardState.CalibrationInfo.Kg17.BottomRight.ToString();

			lblCalUL34.Text = state.BalanceBoardState.CalibrationInfo.Kg34.TopLeft.ToString();
			lblCalUR34.Text = state.BalanceBoardState.CalibrationInfo.Kg34.TopRight.ToString();
			lblCalLL34.Text = state.BalanceBoardState.CalibrationInfo.Kg34.BottomLeft.ToString();
			lblCalLR34.Text = state.BalanceBoardState.CalibrationInfo.Kg34.BottomRight.ToString();

			lblWeight.Text = valTotal.ToString();
		}

		private float GetBalanceBoardSensorValue(short sensor, short min, short mid, short max)
		{
			if (max == mid || mid == min)
				return 0;

			if (sensor < mid)
				return 17f * ((float)(sensor - min) / (mid - min));
			else
				return (17.0f * ((float)(sensor - mid) / (max - mid))) + 17.0f;
		}

		void mote_WiimoteChanged(object sender, WiimoteChangedEventArgs e)
		{
			try
			{
				if (isAwake)
					this.Invoke(new UpdateWiimoteStateDelegate(UpdateWiimoteState), e);
			}
			catch (Exception)
			{ }
		}

		private void btnDisconnect_Click(object sender, EventArgs e)
		{
			if (isAwake)
			{
				isAwake = false;
				wiimote.Disconnect();
			}
		}

		private void Calibration_Deactivate(object sender, EventArgs e)
		{
		}

		private void Calibration_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (isAwake)
			{
				isAwake = false;
				wiimote.Disconnect();
			}
		}
	}
}
