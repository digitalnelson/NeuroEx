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
	public partial class WiiBalanceBoardDisplay : UserControl, IDisplayWidget
	{
		private WiiBalanceBoardManager manager;
		private WiiBalanceBoardMeasurementGroup measGroup;
		private Image imgCOP = null;
		Graphics gImage = null;

		public WiiBalanceBoardDisplay()
		{
			InitializeComponent();

			imgCOP = new Bitmap(pnlPath.Width, pnlPath.Height);
			gImage = Graphics.FromImage(imgCOP);
		}

		public void SetManager(WiiBalanceBoardManager man)
		{
			manager = man;
		}

		public void UpdateValues()
		{
			WiiBalanceBoardMeasurement calibration = manager.GetCalibration();

			measGroup = manager.GetLatestMeasurements(measGroup);
			if (measGroup.Measurements != null && measGroup.Measurements.Length > 0)
			{
				int lastIdx = measGroup.Measurements.Length - 1;

				WiiBalanceBoardMeasurement lastMeas = measGroup.Measurements[lastIdx];

				lblTL.Text = (lastMeas.TopLeft - calibration.TopLeft).ToString();
				lblTR.Text = (lastMeas.TopRight - calibration.TopRight).ToString();
				lblBL.Text = (lastMeas.BottomLeft - calibration.BottomLeft).ToString();
				lblBR.Text = (lastMeas.BottomRight - calibration.BottomRight).ToString();
				lblCOP.Text = lastMeas.COP(calibration).ToString();
			}

			using(Brush b = new SolidBrush(Color.FromArgb(70, Color.Red)))
			{
				if (measGroup != null && measGroup.Measurements != null)
				{
					foreach (WiiBalanceBoardMeasurement meas in measGroup.Measurements)
					{
						WiimoteLib.PointF cop = meas.COP(calibration);

						float fTransX = (cop.X * (imgCOP.Width / 2)) / 12;
						float fTransY = ((cop.Y * (imgCOP.Height / 2)) / 12) * -1;

						int centerX = imgCOP.Width / 2;
						int centerY = imgCOP.Height / 2;

						try
						{
							gImage.FillEllipse(b, centerX + fTransX - 2, centerY + fTransY - 2, 4, 4);
						}
						catch (Exception) { }
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
			measGroup = new WiiBalanceBoardMeasurementGroup();
			gImage.Clear(Color.Black);
			this.Refresh();
		}
	}
}
