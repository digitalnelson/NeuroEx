using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech.Synthesis;

using WiimoteLib;
using System.IO;
using System.Xml.Serialization;
using BluetoothHelperWin;
using NeuroExDevices;

namespace AgileMedicine.MovementStudioForms
{
	public partial class MainForm : Form
	{
		BTPairingHelper btHelperDlg = new BTPairingHelper();

		List<IDevice> devices = new List<IDevice>();
		
		List<IDisplayWidget> widgets = new List<IDisplayWidget>();
		Timer widgetUpdateTimer = new Timer();

		System.Threading.Thread sampleThread = null;
		System.Timers.Timer calibrateTimer = new System.Timers.Timer();
		bool sample = false;
		bool calibrate = false;

		private SamplingSession session = new SamplingSession();

		List<double> sampleTiming = new List<double>();

		BluetoothHelper btHelper = new BluetoothHelper();
		List<BluetoothRadio> btRadios = new List<BluetoothRadio>();
		List<BluetoothDevice> btDevices = new List<BluetoothDevice>();

		Dictionary<string, CompleteMote> allMotes = new Dictionary<string, CompleteMote>();

		public MainForm()
		{
			InitializeComponent();

			calibrateTimer.Interval = 10 * 1000;
			calibrateTimer.Elapsed += new System.Timers.ElapsedEventHandler(calibrateTimer_Elapsed);

			widgetUpdateTimer.Interval = 250;
			widgetUpdateTimer.Tick += new EventHandler(widgetUpdateTimer_Tick);
			widgetUpdateTimer.Start();

            DeviceManager dm = new DeviceManager();

            dm.EnableRefresh();
		}

		void widgetUpdateTimer_Tick(object sender, EventArgs e)
		{
			foreach (IDisplayWidget widget in widgets)
				widget.UpdateValues();
		}

		void LogException(Exception ex)
		{
			try
			{
				using (StreamWriter sw = new StreamWriter("Error.log", true))
				{
					sw.WriteLine("-----------------------------------------------------------------");
					sw.WriteLine("Error - " + DateTime.Now.ToString("yyyyMMdd HHmmss"));
					sw.WriteLine(ex.ToString());
					sw.WriteLine("");
				}
			}
			catch (Exception exInner)
			{
				MessageBox.Show(exInner.Message);
			}
		}

		private void btnFind_Click(object sender, EventArgs e)
		{
			try
			{
				BluetoothHelper btHelper = new BluetoothHelper();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}

			try
			{
				allMotes.Clear();
				lstDevices.Items.Clear();

				btRadios = btHelper.GetRadios();

				if (btRadios != null && btRadios.Count > 0)
				{
					btDevices = btHelper.DiscoverDevices(btRadios[0]);

					foreach (BluetoothDevice btdev in btDevices)
					{
						if (btdev.Name.StartsWith("Nin"))
						{
							string addr = string.Format("{0:x2}{1:x2}{2:x2}{3:x2}{4:x2}{5:x2}",
								btdev.Address[5], btdev.Address[4], btdev.Address[3], btdev.Address[2], btdev.Address[1], btdev.Address[0]);

							CompleteMote m = new CompleteMote();
							m.BtDevice = btdev;

							allMotes[addr] = m;
						}
					}
				}
				else
					MessageBox.Show("No bluetooth radio found");

				WiimoteCollection motes = new WiimoteCollection();
				try { motes.FindAllWiimotes(); }
				catch (Exception) { }

				foreach (Wiimote mote in motes)
				{
					CompleteMote m = allMotes[mote.HIDDeviceSerial];

					if (m != null)
						m.mote = mote;
					else
					{
						m = new CompleteMote();
						m.mote = mote;

						allMotes[mote.HIDDeviceSerial] = m;
					}
				}

				foreach (CompleteMote mote in allMotes.Values)
				{
					string strname = "";

					if (mote.BtDevice != null)
						strname = mote.BtDevice.Name + " ";

					if (mote.mote != null)
						strname += mote.mote.HIDDeviceSerial + " " + mote.mote.HIDDevicePath;

					lstDevices.Items.Add(strname);
				}

				if (allMotes.Values.Count > 0)
					btnStart.Enabled = true;
			}
			catch (Exception ex)
			{
				LogException(ex);
				MessageBox.Show(ex.Message);
			}

			btnStart.Enabled = true;
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			int count = 0;
			pnlSensors.Controls.Clear();
			pnlPlatforms.Controls.Clear();

			pnlSensors.Visible = false;
			pnlPlatforms.Visible = false;

			try
			{
				foreach (CompleteMote mote in allMotes.Values)
				{
					IDevice dev = null;

					if (mote.mote != null && mote.mote.HIDDevicePath.Length > 0)
					{
						if (mote.BtDevice.Name.Contains("RVL-CNT"))
						{
							WiimoteManager man = new WiimoteManager(mote, count);
							dev = man;
							devices.Add(man);

							WiimoteDisplay widget = new WiimoteDisplay();
							widget.SetManager(man);
							widgets.Add(widget);

							pnlSensors.Controls.Add(widget);
							pnlSensors.Visible = true;

							count++;
						}
						else if (mote.BtDevice.Name.Contains("RVL-WBC"))
						{
							WiiBalanceBoardManager man = new WiiBalanceBoardManager(mote);
							dev = man;
							devices.Add(man);

							WiiBalanceBoardDisplay widget = new WiiBalanceBoardDisplay();
							widget.SetManager(man);
							if (chkAccuSway.Checked)
								widget.Dock = DockStyle.Top;
							else
								widget.Dock = DockStyle.Fill;
							widgets.Add(widget);
							pnlPlatforms.Controls.Add(widget);
							pnlPlatforms.Visible = true;
						}
					}

					if (dev != null)
					{
						dev.Connect();
						dev.Start();
					}
				}

			}
			catch (Exception ex)
			{
				LogException(ex);
				MessageBox.Show(ex.Message);
			}

			// TODO: Connect to accusway
			// TODO: Connect to xBees
			// TODO: Connect to mag

			// TODO: Need to build out a nice device configuration screen

			if (chkAccuSway.Checked)
			{
				// Hook up to the accusway
				AccuSwayPlusManager aspMan = new AccuSwayPlusManager();
				devices.Add(aspMan);

				AccuSwayPlusDisplay aspWidget = new AccuSwayPlusDisplay();
				aspWidget.SetManager(aspMan);
				aspWidget.Dock = DockStyle.Top;
				widgets.Add(aspWidget);
				pnlPlatforms.Controls.Add(aspWidget);
				pnlPlatforms.Visible = true;

				aspMan.Connect();
				aspMan.Start();
				//aspMan.Autozero();
			}

			btnNewSession.Enabled = true;
			btnStop.Enabled = true;
			btnStart.Enabled = false;
		}

		private void btnCal_Click(object sender, EventArgs e)
		{
			try
			{
				calibrate = true;
				sample = true;

				sampleThread = new System.Threading.Thread(new System.Threading.ThreadStart(CollectSample));
				sampleThread.Start();
				calibrateTimer.Start();

				btnCollect.Enabled = false;
				btnStopCollect.Enabled = false;
				btnCal.Enabled = false;
				btnStop.Enabled = false;
			}
			catch (Exception ex)
			{
				LogException(ex);
				MessageBox.Show(ex.Message);
			}
		}

		void calibrateTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			try
			{
				sample = false;
				calibrate = false;
				calibrateTimer.Stop();

				this.Invoke(new MethodInvoker(delegate() { btnCal.Enabled = true; btnCollect.Enabled = true; btnStop.Enabled = true; }));

				MessageBox.Show("Calibration Complete");
			}
			catch (Exception ex)
			{
				LogException(ex);
				MessageBox.Show(ex.Message);
			}
		}

		private void btnCollect_Click(object sender, EventArgs e)
		{
			try
			{
				sample = true;
				calibrate = false;

				btnCollect.Enabled = false;
				btnStop.Enabled = false;
				btnStopCollect.Enabled = true;

				foreach (IDisplayWidget widget in widgets)
					widget.Clear();

				sampleThread = new System.Threading.Thread(new System.Threading.ThreadStart(CollectSample));
				sampleThread.Start();
			}
			catch (Exception ex)
			{
				LogException(ex);
				MessageBox.Show(ex.Message);
			}
		}

		void CollectSample()
		{
			try
			{
				sampleTiming.Clear();
				while (sample)
				{
					System.Diagnostics.Stopwatch stop = new System.Diagnostics.Stopwatch();
					stop.Start();

					Sample samp = new Sample();
					samp.Ticks = TimeManager.GetTicks();
					samp.Timestamp = DateTime.Now;

					// TODO: Put the bar up so no new samples come in

					foreach (IDevice device in devices)
					{
						object s = device.GetSample();

						if (s is WiimoteMeasurement)
						{
							samp.Motes.Add((WiimoteMeasurement)s);
							continue;
						}
						else if (s is WiiBalanceBoardMeasurement)
						{
							samp.WiiBoards.Add((WiiBalanceBoardMeasurement)s);
						}
						else if (s is AccuSwayPlusMeasurement)
						{
							samp.AccuSways.Add((AccuSwayPlusMeasurement)s);
						}

						if (calibrate)
							device.Calibrate();
					}

					if (calibrate)
						session.Calibration.Add(samp);
					else
						session.Samples.Add(samp);

					stop.Stop();
					sampleTiming.Add(((double)stop.ElapsedTicks / (double)System.Diagnostics.Stopwatch.Frequency) * 1000);

					System.Threading.Thread.Sleep(20);
				}
			}
			catch (Exception ex)
			{
				LogException(ex);
				MessageBox.Show(ex.Message);
			}
		}

		private void btnStopCollect_Click(object sender, EventArgs e)
		{
			try
			{
				sample = false;

				btnStopCollect.Enabled = false;
				btnCollect.Enabled = true;
				btnStop.Enabled = true;

				MessageBox.Show("Collection Complete");
			}
			catch (Exception ex)
			{
				LogException(ex);
				MessageBox.Show(ex.Message);
			}
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
			try
			{
				btnStop.Enabled = false;
				btnCal.Enabled = false;
				btnCollect.Enabled = false;
				btnStopCollect.Enabled = false;
				btnStart.Enabled = true;

				foreach (IDevice dev in devices)
				{
					try
					{
						dev.Stop();
						dev.Disconnect();
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
					}
				}
			}
			catch (Exception ex)
			{
				LogException(ex);
				MessageBox.Show(ex.Message);
			}
		}

		private void btnBtHelper_Click(object sender, EventArgs e)
		{
			try
			{
				btHelperDlg.ShowDialog();
			}
			catch (Exception ex)
			{
				LogException(ex);
				MessageBox.Show(ex.Message);
			}
		}

		private void btnNewSession_Click(object sender, EventArgs e)
		{
			try
			{
				if (txtSubjectId.Text.Length > 0)
				{
					session = new SamplingSession();
					session.SubjectId = txtSubjectId.Text;
					btnCal.Enabled = true;
					btnCollect.Enabled = true;
					btnSaveSession.Enabled = true;
					btnNewSession.Enabled = false;
				}
				else
					MessageBox.Show("Please specify a subject id.");
			}
			catch (Exception ex)
			{
				LogException(ex);
				MessageBox.Show(ex.Message);
			}
		}

		private void btnSaveSession_Click(object sender, EventArgs e)
		{
			try
			{
				SaveFileDialog sfd = new SaveFileDialog();

				sfd.FileName = string.Format("{0}_{1}_SamplingSession.xml", session.Timestamp.ToString("yyyyMMddHHmmss"), session.SubjectId);

				if (sfd.ShowDialog() == DialogResult.OK)
				{
					using (StreamWriter sw = new StreamWriter(sfd.FileName))
					{
						XmlSerializer ser = new XmlSerializer(typeof(SamplingSession));
						ser.Serialize(sw, session);
					}

					btnNewSession.Enabled = true;
					btnCal.Enabled = false;
					btnCollect.Enabled = false;
					btnSaveSession.Enabled = false;
				}
			}
			catch (Exception ex)
			{
				LogException(ex);
				MessageBox.Show(ex.Message);
			}
		}
	}

	public class CompleteMote
	{
		public BluetoothDevice BtDevice;
		public Wiimote mote;
	}
}
