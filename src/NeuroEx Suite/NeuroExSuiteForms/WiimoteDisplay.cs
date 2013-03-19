using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace AgileMedicine.MovementStudioForms
{
	public partial class WiimoteDisplay : UserControl, IDisplayWidget
	{
		private WiimoteManager manager;
		private WiimoteMeasurementGroup measGroup;
		private Image imgDisplay = null;
		Graphics gImage = null;

		//int count = 0;

		public WiimoteDisplay()
		{
			InitializeComponent();

			imgDisplay = new Bitmap(pnlDisplay.Width, pnlDisplay.Height);
			gImage = Graphics.FromImage(imgDisplay);
		}

		public void SetManager(WiimoteManager man)
		{
			manager = man;
		}

		public void UpdateValues()
		{
			measGroup = manager.GetLatestMeasurements(measGroup);
			if (measGroup.Measurements != null && measGroup.Measurements.Length > 0)
			{
				int lastIdx = measGroup.Measurements.Length - 1;

				WiimoteMeasurement lastMeas = measGroup.Measurements[lastIdx];

				lblAX.Text = lastMeas.AccelX.ToString();
				lblAY.Text = lastMeas.AccelY.ToString();
				lblAZ.Text = lastMeas.AccelZ.ToString();
				lblGX.Text = lastMeas.GyroX.ToString();
				lblGY.Text = lastMeas.GyroY.ToString();
				lblGZ.Text = lastMeas.GyroZ.ToString();
			}

			const int accelRange = 255;
			const int gyroRange = 15000;
			int itemHeight = imgDisplay.Height / 6;
			using (Pen p = new Pen(Color.FromArgb(50, Color.White)))
			{
				if (measGroup != null && measGroup.Measurements != null)
				{
					foreach (WiimoteMeasurement meas in measGroup.Measurements)
					{
						float ax = (meas.AccelX * (float)imgDisplay.Width) / (float)accelRange;
						float ay = (meas.AccelY * (float)imgDisplay.Width) / (float)accelRange;
						float az = (meas.AccelZ * (float)imgDisplay.Width) / (float)accelRange;
						float gp = (meas.GyroX * (float)imgDisplay.Width) / (float)gyroRange;
						float gr = (meas.GyroY * (float)imgDisplay.Width) / (float)gyroRange;
						float gy = (meas.GyroZ * (float)imgDisplay.Width) / (float)gyroRange;
						
						//float gy = ((count % gyroRange) * (float)imgDisplay.Width) / (float)gyroRange;

						Debug.WriteLine(string.Format("{0} {1} {2} {3} {4} {5}", meas.GyroX, meas.GyroY, meas.GyroZ, gp, gr, gy));

						try
						{
							gImage.DrawLine(p, (int)ax, 0, (int)ax, itemHeight);
							gImage.DrawLine(p, (int)ay, itemHeight, (int)ay, itemHeight * 2);
							gImage.DrawLine(p, (int)az, itemHeight * 2, (int)az, itemHeight * 3);
							gImage.DrawLine(p, gp, itemHeight * 3, gp, itemHeight * 4);
							gImage.DrawLine(p, gr, itemHeight * 4, gr, itemHeight * 5);
							gImage.DrawLine(p, gy, itemHeight * 5, gy, itemHeight * 6);
						}
						catch (Exception) { }

						//count++;
					}
				}
			}

			this.Refresh();
		}

		public void Clear()
		{
			gImage.Clear(Color.Black);
			this.Refresh();
		}

		private void pnlDisplay_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawImage(imgDisplay, 0, 0, pnlDisplay.Width, pnlDisplay.Height);
		}
	}
}
