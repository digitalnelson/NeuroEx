using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AgileMedicine.MovementStudioForms
{
	public partial class AccuSwayPlusDisplay : UserControl, IDisplayWidget
	{
		private AccuSwayPlusManager manager;
		private AccuSwayPlusMeasurementGroup measGroup;
		private Image imgCOP = null;
		Graphics gImage = null;

		public AccuSwayPlusDisplay()
		{
			InitializeComponent();

			imgCOP = new Bitmap(pnlPath.Width, pnlPath.Height);
			gImage = Graphics.FromImage(imgCOP);
		}

		public void SetManager(AccuSwayPlusManager man)
		{
			manager = man;
		}

		public void UpdateValues()
		{
			AccuSwayPlusMeasurement calibration = manager.GetCalibration();

			measGroup = manager.GetLatestMeasurements(measGroup);
			if (measGroup.Measurements != null && measGroup.Measurements.Length > 0)
			{
				int lastIdx = measGroup.Measurements.Length - 1;

				AccuSwayPlusMeasurement lastMeas = measGroup.Measurements[lastIdx];

				lblC1.Text = (lastMeas.C1 - calibration.C1).ToString();
				lblC2.Text = (lastMeas.C2 - calibration.C2).ToString();
				lblC3.Text = (lastMeas.C3 - calibration.C3).ToString();
				lblC4.Text = (lastMeas.C4 - calibration.C4).ToString();
				lblC5.Text = (lastMeas.C5 - calibration.C5).ToString();
				lblC6.Text = (lastMeas.C6 - calibration.C6).ToString();
				lblC7.Text = (lastMeas.C7 - calibration.C7).ToString();
				lblC8.Text = (lastMeas.C8 - calibration.C8).ToString();
				lblC9.Text = (lastMeas.C9 - calibration.C9).ToString();
				lblC10.Text = (lastMeas.C10 - calibration.C10).ToString();
				lblC11.Text = (lastMeas.C11 - calibration.C11).ToString();
				lblC12.Text = (lastMeas.C12 - calibration.C12).ToString();
			}

			using(Brush b = new SolidBrush(Color.FromArgb(70, Color.Red)))
			{
				if (measGroup != null && measGroup.Measurements != null)
				{
					foreach (AccuSwayPlusMeasurement meas in measGroup.Measurements)
					{
						//WiimoteLib.PointF cop = meas.COP(calibration);

						//float fTransX = (cop.X * (imgCOP.Width / 2)) / 12;
						//float fTransY = ((cop.Y * (imgCOP.Height / 2)) / 12) * -1;

						//int centerX = imgCOP.Width / 2;
						//int centerY = imgCOP.Height / 2;

						//try
						//{
						//    gImage.FillEllipse(b, centerX + fTransX - 2, centerY + fTransY - 2, 4, 4);
						//}
						//catch (Exception) { }
					}
				}
			}

			this.Refresh();
		}

		private void pnlPath_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawImage(imgCOP, 0, 0, pnlPath.Width, pnlPath.Height);
		}

		public void Clear()
		{
			gImage.Clear(Color.Black);
			this.Refresh();
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			measGroup = new AccuSwayPlusMeasurementGroup();
			gImage.Clear(Color.Black);
			this.Refresh();
		}
	}
}
