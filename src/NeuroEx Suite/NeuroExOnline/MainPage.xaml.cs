using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;
using System.Xml.Serialization;
using System.Text;

using NeuroExLib;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace NeuroExOnline
{
	public class CalibrationData
	{
		public List<AccuSwayPlusMeasurement> ASPCalibration = new List<AccuSwayPlusMeasurement>();
		public List<WiiBalanceBoardMeasurement> WiiBBCalibration = new List<WiiBalanceBoardMeasurement>();
	}

	public partial class MainPage : UserControl
	{
		private List<Trial> trials = null;
		private List<AccuSwayPlusMeasurement> ASPCalibration = new List<AccuSwayPlusMeasurement>();
		private List<WiiBalanceBoardMeasurement> WiiBBCalibration = new List<WiiBalanceBoardMeasurement>();

		public MainPage()
		{
			InitializeComponent();
		}

		private CalibrationData GetCalibrationInformation(List<Sample> calibration)
		{
			CalibrationData cd = new CalibrationData();

			ASPCalibration = cd.ASPCalibration;
			WiiBBCalibration = cd.WiiBBCalibration;

			foreach (Sample cal in calibration)
			{
				for (int i = 0; i < cal.WiiBoards.Count; i++)
				{
					if (i >= WiiBBCalibration.Count)
						WiiBBCalibration.Add(new WiiBalanceBoardMeasurement());

					WiiBBCalibration[i].TopLeft += cal.WiiBoards[i].TopLeft;
					WiiBBCalibration[i].TopRight += cal.WiiBoards[i].TopRight;
					WiiBBCalibration[i].BottomLeft += cal.WiiBoards[i].BottomLeft;
					WiiBBCalibration[i].BottomRight += cal.WiiBoards[i].BottomRight;
				}

				for (int i = 0; i < cal.AccuSways.Count; i++)
				{
					if (i >= ASPCalibration.Count)
						ASPCalibration.Add(new AccuSwayPlusMeasurement());

					ASPCalibration[i].C1 += cal.AccuSways[i].C1;
					ASPCalibration[i].C2 += cal.AccuSways[i].C2;
					ASPCalibration[i].C3 += cal.AccuSways[i].C3;
					ASPCalibration[i].C4 += cal.AccuSways[i].C4;
					ASPCalibration[i].C5 += cal.AccuSways[i].C5;
					ASPCalibration[i].C6 += cal.AccuSways[i].C6;
					ASPCalibration[i].C7 += cal.AccuSways[i].C7;
					ASPCalibration[i].C8 += cal.AccuSways[i].C8;
					ASPCalibration[i].C9 += cal.AccuSways[i].C9;
					ASPCalibration[i].C10 += cal.AccuSways[i].C10;
					ASPCalibration[i].C11 += cal.AccuSways[i].C11;
					ASPCalibration[i].C12 += cal.AccuSways[i].C12;
				}
			}

			for (int i = 0; i < WiiBBCalibration.Count; i++)
			{
				WiiBBCalibration[i].TopLeft /= calibration.Count;
				WiiBBCalibration[i].TopRight /= calibration.Count;
				WiiBBCalibration[i].BottomLeft /= calibration.Count;
				WiiBBCalibration[i].BottomRight /= calibration.Count;
			}

			for (int i = 0; i < ASPCalibration.Count; i++)
			{
				ASPCalibration[i].C1 /= calibration.Count;
				ASPCalibration[i].C2 /= calibration.Count;
				ASPCalibration[i].C3 /= calibration.Count;
				ASPCalibration[i].C4 /= calibration.Count;
				ASPCalibration[i].C5 /= calibration.Count;
				ASPCalibration[i].C6 /= calibration.Count;
				ASPCalibration[i].C7 /= calibration.Count;
				ASPCalibration[i].C8 /= calibration.Count;
				ASPCalibration[i].C9 /= calibration.Count;
				ASPCalibration[i].C10 /= calibration.Count;
				ASPCalibration[i].C11 /= calibration.Count;
				ASPCalibration[i].C12 /= calibration.Count;
			}

			return cd;
		}

		private void LoadCalibrationInformation(List<Sample> calibration)
		{
			ASPCalibration = new List<AccuSwayPlusMeasurement>();
			WiiBBCalibration = new List<WiiBalanceBoardMeasurement>();

			foreach (Sample cal in calibration)
			{
				for (int i = 0; i < cal.WiiBoards.Count; i++)
				{
					if (i >= WiiBBCalibration.Count)
						WiiBBCalibration.Add(new WiiBalanceBoardMeasurement());

					WiiBBCalibration[i].TopLeft += cal.WiiBoards[i].TopLeft;
					WiiBBCalibration[i].TopRight += cal.WiiBoards[i].TopRight;
					WiiBBCalibration[i].BottomLeft += cal.WiiBoards[i].BottomLeft;
					WiiBBCalibration[i].BottomRight += cal.WiiBoards[i].BottomRight;
				}

				for (int i = 0; i < cal.AccuSways.Count; i++)
				{
					if (i >= ASPCalibration.Count)
						ASPCalibration.Add(new AccuSwayPlusMeasurement());

					ASPCalibration[i].C1 += cal.AccuSways[i].C1;
					ASPCalibration[i].C2 += cal.AccuSways[i].C2;
					ASPCalibration[i].C3 += cal.AccuSways[i].C3;
					ASPCalibration[i].C4 += cal.AccuSways[i].C4;
					ASPCalibration[i].C5 += cal.AccuSways[i].C5;
					ASPCalibration[i].C6 += cal.AccuSways[i].C6;
					ASPCalibration[i].C7 += cal.AccuSways[i].C7;
					ASPCalibration[i].C8 += cal.AccuSways[i].C8;
					ASPCalibration[i].C9 += cal.AccuSways[i].C9;
					ASPCalibration[i].C10 += cal.AccuSways[i].C10;
					ASPCalibration[i].C11 += cal.AccuSways[i].C11;
					ASPCalibration[i].C12 += cal.AccuSways[i].C12;
				}
			}

			for (int i = 0; i < WiiBBCalibration.Count; i++)
			{
				WiiBBCalibration[i].TopLeft /= calibration.Count;
				WiiBBCalibration[i].TopRight /= calibration.Count;
				WiiBBCalibration[i].BottomLeft /= calibration.Count;
				WiiBBCalibration[i].BottomRight /= calibration.Count;
			}

			for (int i = 0; i < ASPCalibration.Count; i++)
			{
				ASPCalibration[i].C1 /= calibration.Count;
				ASPCalibration[i].C2 /= calibration.Count;
				ASPCalibration[i].C3 /= calibration.Count;
				ASPCalibration[i].C4 /= calibration.Count;
				ASPCalibration[i].C5 /= calibration.Count;
				ASPCalibration[i].C6 /= calibration.Count;
				ASPCalibration[i].C7 /= calibration.Count;
				ASPCalibration[i].C8 /= calibration.Count;
				ASPCalibration[i].C9 /= calibration.Count;
				ASPCalibration[i].C10 /= calibration.Count;
				ASPCalibration[i].C11 /= calibration.Count;
				ASPCalibration[i].C12 /= calibration.Count;
			}
		}

		private Dictionary<string, TrialDeviceSummary> _devices;
		private TrialDeviceSummary GetDeviceById(string id, Trial trial)
		{
			TrialDeviceSummary device = null;
			if (!_devices.ContainsKey(id))
			{
				device = new TrialDeviceSummary();
				trial.DeviceMetrics.Add(device);
				_devices[id] = device;
			}
			else
				device = _devices[id];

			return device;
		}

		private void ProcessMeasurement(IMeasurement meas, Trial trial)
		{
			//TrialDeviceSummary device = GetDeviceById(meas.Id, trial);

			trial.DeviceMetrics[0].sum.X += meas.COP().X;
			trial.DeviceMetrics[0].sum.Y += meas.COP().Y;

			if (trial.DeviceMetrics[0].Measurements.Count > 0)
			{
				PointF prevCOP = trial.DeviceMetrics[0].Measurements[trial.DeviceMetrics[0].Measurements.Count - 1].COP();
				PointF nextCOP = meas.COP();

				double dXDist = Math.Abs(nextCOP.X - prevCOP.X);
				double dYDist = Math.Abs(nextCOP.Y - prevCOP.Y);

				trial.DeviceMetrics[0].dist.X += dXDist;
				trial.DeviceMetrics[0].dist.Y += dYDist;
			}

			trial.DeviceMetrics[0].Range.Update(meas.COP().X);
			trial.DeviceMetrics[0].Range.Update(meas.COP().Y);
			trial.DeviceMetrics[0].Measurements.Add(meas);
		}

		private void btnOpen_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "NeuroEx XML Files (*.xml)|*.xml | All Files (*.*)|*.*";
			ofd.FilterIndex = 1;

			if (ofd.ShowDialog() == true)
			{
				_devices = new Dictionary<string, TrialDeviceSummary>();

				SamplingSession sess = null;
				using (StreamReader sr = new StreamReader(ofd.File.OpenRead()))
				{
					XmlSerializer ser = new XmlSerializer(typeof(SamplingSession));
					sess = (SamplingSession)ser.Deserialize(sr);
				}

				LoadCalibrationInformation(sess.Calibration);

				DateTime dtLast = DateTime.MinValue;
				trials = new List<Trial>();
				Trial currentTrial = null;

				foreach (Sample sample in sess.Samples)
				{
					TimeSpan ts = new TimeSpan(sample.Timestamp.Ticks - dtLast.Ticks);
					if (ts.TotalSeconds > 1)
					{
						currentTrial = new Trial();
						currentTrial.DeviceMetrics.Add(new TrialDeviceSummary());
						currentTrial.Start = sample.Timestamp;
						trials.Add(currentTrial);
					}

					foreach (WiiBalanceBoardMeasurement meas in sample.WiiBoards)
					{
						meas.Calibration = WiiBBCalibration[0];
						meas.SampleTimestamp = sample.Timestamp;
						meas.SampleTicks = sample.Ticks;
						ProcessMeasurement(meas, currentTrial);
					}

					foreach (WiimoteMeasurement meas in sample.Motes)
					{
						meas.SampleTimestamp = sample.Timestamp;
						meas.SampleTicks = sample.Ticks;
						ProcessMeasurement(meas, currentTrial);
					}

					foreach (AccuSwayPlusMeasurement meas in sample.AccuSways)
					{
						meas.SampleTimestamp = sample.Timestamp;
						meas.SampleTicks = sample.Ticks;
						ProcessMeasurement(meas, currentTrial);
					}

					currentTrial.End = sample.Timestamp;
					dtLast = sample.Timestamp;
				}

				foreach (Trial trial in trials)
				{
					TimeSpan tsTrial = new TimeSpan(trial.End.Ticks - trial.Start.Ticks);

					foreach (TrialDeviceSummary summary in trial.DeviceMetrics)
					{
						summary.avg.X = summary.sum.X / summary.Measurements.Count;
						summary.avg.Y = summary.sum.Y / summary.Measurements.Count;
						summary.ValueUpperLimit = Math.Ceiling(Math.Abs(summary.Range.Max) > Math.Abs(summary.Range.Min) ? Math.Abs(summary.Range.Max) : Math.Abs(summary.Range.Min));

						foreach (IMeasurement meas in summary.Measurements)
						{
							summary.max.X = Math.Abs(meas.COP().X - summary.avg.X) > summary.max.X ? Math.Abs(meas.COP().X - summary.avg.X) : summary.max.X;
							summary.max.Y = Math.Abs(meas.COP().Y - summary.avg.Y) > summary.max.Y ? Math.Abs(meas.COP().Y - summary.avg.Y) : summary.max.Y;

							summary.min.X = Math.Abs(meas.COP().X - summary.avg.X) < summary.min.X ? Math.Abs(meas.COP().X - summary.min.X) : summary.min.X;
							summary.min.Y = Math.Abs(meas.COP().Y - summary.avg.Y) < summary.min.Y ? Math.Abs(meas.COP().Y - summary.min.Y) : summary.min.Y;

							summary.avgDev.X += Math.Abs(meas.COP().X - summary.avg.X);
							summary.avgDev.Y += Math.Abs(meas.COP().Y - summary.avg.Y);
						}

						summary.avgDev.X /= summary.Measurements.Count;
						summary.avgDev.Y /= summary.Measurements.Count;

						summary.avgDist.X = summary.dist.X / summary.Measurements.Count;
						summary.avgDist.Y = summary.dist.Y / summary.Measurements.Count;

						summary.speed.X = summary.dist.X / tsTrial.TotalSeconds;
						summary.speed.Y = summary.dist.Y / tsTrial.TotalSeconds;
					}
				}

				lstTrial.ItemsSource = trials.ToArray();
			}
		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			StringBuilder sbCSV = new StringBuilder();

			int counter = 1;
			sbCSV.Append("Type\tSec\tSumX\tSumY\tAvgX\tAvgY\tMaxX\tMaxY\tavgDevX\tavgDevY\tdist X\tdistY\tavgDistX\tavgDistY\tspeedX\tspeedY");
			sbCSV.AppendLine();

			foreach (Trial trial in trials)
			{
				TimeSpan tsTrial = new TimeSpan(trial.End.Ticks - trial.Start.Ticks);

				foreach (TrialDeviceSummary sum in trial.DeviceMetrics)
				{
					//foreach (WiiBalanceBoardMeasurement meas in sum.Measurements)
					//{
					//    sbCSV.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}", counter, meas.SampleTimestamp.ToString("yyyymmddhhMMss"), meas.COPX, meas.COPY, meas.TopLeft, meas.TopRight, meas.BottomLeft, meas.BottomRight);
					//    sbCSV.AppendLine();
					//}

					if (sum.Measurements.Count > 1)
					{
						sbCSV.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}", counter, tsTrial.TotalSeconds.ToString(), sum.sum.X, sum.sum.Y, sum.avg.X, sum.avg.Y, sum.max.X, sum.max.Y, sum.avgDev.X, sum.avgDev.Y, sum.dist.X, sum.dist.Y, sum.avgDist.X, sum.avgDist.Y, sum.speed.X, sum.speed.Y);
						sbCSV.AppendLine();
					}
				}

				counter++;

				if (counter == 4)
					counter = 1;
			}

			SaveFileDialog sfd = new SaveFileDialog()
			{
				DefaultExt = "csv",
				Filter = "CSV Files (*.csv)|*.csv|All files (*.*)|*.*",
				FilterIndex = 1
			};

			if (sfd.ShowDialog() == true)
			{
				using (Stream stream = sfd.OpenFile())
				{
					using (StreamWriter writer = new StreamWriter(stream))
					{
						writer.Write(sbCSV.ToString());
						writer.Close();
					}
					stream.Close();
				}
			}
		}

		private void lstTrial_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Trial trial = (Trial)lstTrial.SelectedItem;

			if (trial != null)
			{
				UpdateCOPDisplay(trial);

				lstSummary.Items.Clear();
				lstSummary.Items.Add("Start: " + trial.Start.ToLongDateString() + " " + trial.Start.ToLongTimeString());
				lstSummary.Items.Add("End: " + trial.End.ToLongDateString() + " " + trial.End.ToLongTimeString());

				foreach (TrialDeviceSummary tds in trial.DeviceMetrics)
				{
					foreach (SummaryDataItem sdi in tds.SummaryItems)
						lstSummary.Items.Add(sdi.Name + "\t\t\t\t X:" + sdi.X.ToString("#####0.###") + "\t\t Y:" + sdi.Y.ToString("#####0.###"));
				}

				dgSamples.ItemsSource = trial.DeviceMetrics[0].Measurements.ToArray();
			}
		}

		private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			Trial trial = (Trial)lstTrial.SelectedItem;

			if (trial != null)
			{
				UpdateCOPDisplay(trial);
			}
		}

		private void UpdateCOPDisplay(Trial trial)
		{
			double sqRad = (canCOP.ActualHeight < canCOP.ActualHeight ? canCOP.ActualWidth : canCOP.ActualHeight) / 2;

			canCOP.Children.Clear();

			foreach (TrialDeviceSummary sum in trial.DeviceMetrics)
			{
				foreach (IMeasurement meas in sum.Measurements)
				{
					double xCoord = ((meas.COP().X / sum.ValueUpperLimit) * sqRad) + (canCOP.ActualWidth / 2);
					double yCoord = (((meas.COP().Y / sum.ValueUpperLimit) * sqRad) * -1) + (canCOP.ActualHeight / 2);

					Ellipse elp = new Ellipse();
					elp.Fill = new SolidColorBrush(Colors.Red);
					elp.Width = 3;
					elp.Height = 3;

					Canvas.SetLeft(elp, xCoord - 1);
					Canvas.SetTop(elp, yCoord - 1);

					canCOP.Children.Add(elp);
				}
			}
		}

		private void btnSaveRaw_Click(object sender, RoutedEventArgs e)
		{
			StringBuilder sbCSV = new StringBuilder();

			sbCSV.Append("DeviceId\tCOPX\tCOPY\tTicks\tTimestamp");
			sbCSV.AppendLine();

			Trial trial = lstTrial.SelectedItem as Trial;

			if (trial != null)
			{
				foreach (TrialDeviceSummary tds in trial.DeviceMetrics)
				{
					foreach (IMeasurement meas in tds.Measurements)
					{
						sbCSV.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}", meas.Id, meas.COPX, meas.COPY, meas.SampleTicks, meas.SampleTimestamp.ToString());
						sbCSV.AppendLine();
					}
				}
			}

			SaveFileDialog sfd = new SaveFileDialog()
			{
				DefaultExt = "csv",
				Filter = "CSV Files (*.csv)|*.csv|All files (*.*)|*.*",
				FilterIndex = 1
			};

			if (sfd.ShowDialog() == true)
			{
				using (Stream stream = sfd.OpenFile())
				{
					using (StreamWriter writer = new StreamWriter(stream))
					{
						writer.Write(sbCSV.ToString());
						writer.Close();
					}
					stream.Close();
				}
			}
		}

		private void btnSaveSummary_Click(object sender, RoutedEventArgs e)
		{
			StringBuilder sbCSV = new StringBuilder();

			int counter = 1;
			sbCSV.Append("Type\tId\tTask\tSec\tSumX\tSumY\tAvgX\tAvgY\tMaxX\tMaxY\tavgDevX\tavgDevY\tdist X\tdistY\tavgDistX\tavgDistY\tspeedX\tspeedY");
			sbCSV.AppendLine();

			foreach (Trial trial in trials)
			{
				TimeSpan tsTrial = new TimeSpan(trial.End.Ticks - trial.Start.Ticks);

				foreach (TrialDeviceSummary sum in trial.DeviceMetrics)
				{
					//foreach (WiiBalanceBoardMeasurement meas in sum.Measurements)
					//{
					//    sbCSV.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}", counter, meas.SampleTimestamp.ToString("yyyymmddhhMMss"), meas.COPX, meas.COPY, meas.TopLeft, meas.TopRight, meas.BottomLeft, meas.BottomRight);
					//    sbCSV.AppendLine();
					//}

					if (sum.Measurements.Count > 1)
					{
						string evt = trial.Name;
						string[] strs = trial.Name.Split('/');
						if (strs.Length > 0)
							evt = strs[0];

						string tp = "";
						if (trial.Name.Contains("i") || trial.Name.Contains("l"))
							tp = "i";
						else if (trial.Name.Contains("s"))
							tp = "s";
						else if (trial.Name.Contains("c"))
							tp = "c";

						sbCSV.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}\t{17}", trial.Name, evt, tp, tsTrial.TotalSeconds.ToString(), sum.sum.X, sum.sum.Y, sum.avg.X, sum.avg.Y, sum.max.X, sum.max.Y, sum.avgDev.X, sum.avgDev.Y, sum.dist.X, sum.dist.Y, sum.avgDist.X, sum.avgDist.Y, sum.speed.X, sum.speed.Y);
						sbCSV.AppendLine();
					}
				}

				counter++;

				if (counter == 4)
					counter = 1;
			}

			SaveFileDialog sfd = new SaveFileDialog()
			{
				DefaultExt = "csv",
				Filter = "CSV Files (*.csv)|*.csv|All files (*.*)|*.*",
				FilterIndex = 1
			};

			if (sfd.ShowDialog() == true)
			{
				using (Stream stream = sfd.OpenFile())
				{
					using (StreamWriter writer = new StreamWriter(stream))
					{
						writer.Write(sbCSV.ToString());
						writer.Close();
					}
					stream.Close();
				}
			}
		}

		private void btnAnalyze_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			
			ofd.Filter = "NeuroEx Zip Archive (*.zip)|*.zip | All Files (*.*)|*.*";
			ofd.FilterIndex = 1;

			if (ofd.ShowDialog() == true)
			{
				_devices = new Dictionary<string, TrialDeviceSummary>();
				trials = new List<Trial>();

				// Open zip file
				try
				{
					using (StreamReader srArchive = new StreamReader(ofd.File.OpenRead()))
					{
						var zf = new ZipFile(srArchive.BaseStream);

						foreach (ZipEntry ze in zf)
						{
							SamplingSession sess = null;
							using (StreamReader srFile = new StreamReader(zf.GetInputStream(ze)))
							{
								XmlSerializer ser = new XmlSerializer(typeof(SamplingSession));
								sess = (SamplingSession)ser.Deserialize(srFile);
							}

							var calibration =  GetCalibrationInformation(sess.Calibration);

							DateTime dtLast = DateTime.MinValue;
							Trial currentTrial = null;

							foreach (Sample sample in sess.Samples)
							{
								TimeSpan ts = new TimeSpan(sample.Timestamp.Ticks - dtLast.Ticks);
								if (ts.TotalSeconds > 1)
								{
									currentTrial = new Trial();
									currentTrial.DeviceMetrics.Add(new TrialDeviceSummary());
									currentTrial.Start = sample.Timestamp;
									currentTrial.Name = ze.Name;
									trials.Add(currentTrial);
								}

								foreach (WiiBalanceBoardMeasurement meas in sample.WiiBoards)
								{
									meas.Calibration = calibration.WiiBBCalibration[0];
									meas.SampleTimestamp = sample.Timestamp;
									meas.SampleTicks = sample.Ticks;
									ProcessMeasurement(meas, currentTrial);
								}

								foreach (WiimoteMeasurement meas in sample.Motes)
								{
									meas.SampleTimestamp = sample.Timestamp;
									meas.SampleTicks = sample.Ticks;
									ProcessMeasurement(meas, currentTrial);
								}

								foreach (AccuSwayPlusMeasurement meas in sample.AccuSways)
								{
									meas.SampleTimestamp = sample.Timestamp;
									meas.SampleTicks = sample.Ticks;
									ProcessMeasurement(meas, currentTrial);
								}

								currentTrial.End = sample.Timestamp;
								dtLast = sample.Timestamp;
							}

							foreach (Trial trial in trials)
							{
								TimeSpan tsTrial = new TimeSpan(trial.End.Ticks - trial.Start.Ticks);

								foreach (TrialDeviceSummary summary in trial.DeviceMetrics)
								{
									summary.avg.X = summary.sum.X / summary.Measurements.Count;
									summary.avg.Y = summary.sum.Y / summary.Measurements.Count;
									summary.ValueUpperLimit = Math.Ceiling(Math.Abs(summary.Range.Max) > Math.Abs(summary.Range.Min) ? Math.Abs(summary.Range.Max) : Math.Abs(summary.Range.Min));

									foreach (IMeasurement meas in summary.Measurements)
									{
										summary.max.X = Math.Abs(meas.COP().X - summary.avg.X) > summary.max.X ? Math.Abs(meas.COP().X - summary.avg.X) : summary.max.X;
										summary.max.Y = Math.Abs(meas.COP().Y - summary.avg.Y) > summary.max.Y ? Math.Abs(meas.COP().Y - summary.avg.Y) : summary.max.Y;

										summary.min.X = Math.Abs(meas.COP().X - summary.avg.X) < summary.min.X ? Math.Abs(meas.COP().X - summary.min.X) : summary.min.X;
										summary.min.Y = Math.Abs(meas.COP().Y - summary.avg.Y) < summary.min.Y ? Math.Abs(meas.COP().Y - summary.min.Y) : summary.min.Y;

										summary.avgDev.X += Math.Abs(meas.COP().X - summary.avg.X);
										summary.avgDev.Y += Math.Abs(meas.COP().Y - summary.avg.Y);
									}

									summary.avgDev.X /= summary.Measurements.Count;
									summary.avgDev.Y /= summary.Measurements.Count;

									summary.avgDist.X = summary.dist.X / summary.Measurements.Count;
									summary.avgDist.Y = summary.dist.Y / summary.Measurements.Count;

									summary.speed.X = summary.dist.X / tsTrial.TotalSeconds;
									summary.speed.Y = summary.dist.Y / tsTrial.TotalSeconds;
								}
							}

						}
					}


				}
				finally
				{
				}

				lstTrial.ItemsSource = trials.ToArray();
			}
		}
	}

	public class SummaryDataItem
	{
		public string Name {get; set;}
		public double X { get; set; }
		public double Y { get; set; }
	}

	public class TrialDeviceSummary
	{
		public string Name;
		public List<IMeasurement> Measurements = new List<IMeasurement>();
		public List<SummaryDataItem> SummaryItems = new List<SummaryDataItem>();

		public double ValueUpperLimit = 0;
		public Range Range = new Range();

		public SummaryDataItem sum = new SummaryDataItem() { Name = "Sum" };
		public SummaryDataItem avg = new SummaryDataItem() { Name = "Average" };
		public SummaryDataItem max = new SummaryDataItem() { Name = "Max" };
		public SummaryDataItem min = new SummaryDataItem() { Name = "Min" };
		public SummaryDataItem skew = new SummaryDataItem() { Name = "Skew" };
		public SummaryDataItem avgDev = new SummaryDataItem() { Name = "Average Deviation" };
		public SummaryDataItem dist = new SummaryDataItem() { Name = "Dist" };
		public SummaryDataItem avgDist = new SummaryDataItem() { Name = "Average Dist" };
		public SummaryDataItem speed = new SummaryDataItem() { Name = "Speed" };

		public TrialDeviceSummary()
		{
			SummaryItems.Add(sum);
			SummaryItems.Add(avg);
			SummaryItems.Add(max);
			SummaryItems.Add(min);
			SummaryItems.Add(skew);
			SummaryItems.Add(avgDev);
			SummaryItems.Add(dist);
			SummaryItems.Add(avgDist);
			SummaryItems.Add(speed);
		}
	}

	public class Trial
	{
		public DateTime Start;
		public DateTime End;
		public string Name;
		public List<TrialDeviceSummary> DeviceMetrics = new List<TrialDeviceSummary>();

		public override string ToString()
		{
			if (Name != null && Name.Length > 0)
				return this.Name;
			else
				return "Trial - " + Start.ToString();
		}
	}

	public struct PointF
	{
		public float X;
		public float Y;
	}

	public interface IMeasurement
	{
		string Id { get; set; }
		float COPX { get; }
		float COPY { get; }
		DateTime SampleTimestamp { get; set; }
		long SampleTicks { get; set; }
		PointF COP();
	}

	public class Measurement : IMeasurement
	{
		public string Id
		{
			get { return _id; }
			set { _id = value; }
		} private string _id;

		public float COPX
		{
			get { return this.COP().X; }
		}

		public float COPY
		{
			get { return this.COP().Y; }
		}

		public DateTime SampleTimestamp { get; set; }
		public long SampleTicks { get; set; }

		public virtual PointF COP()
		{
			PointF pt;
			pt.X = 0;
			pt.Y = 0;

			return pt;
		}
	}

	public class WiiBalanceBoardMeasurement : Measurement
	{
		public WiiBalanceBoardMeasurement Calibration
		{
			set {cal = value;}
			get{return cal;}
		}
		private WiiBalanceBoardMeasurement cal;

		public long Ticks;
		public int TopLeft;
		public int TopRight;
		public int BottomLeft;
		public int BottomRight;

		// length between board sensors
		private const float BSL = 43;
		// width between board sensors
		private const float BSW = 24;

		public WiiBalanceBoardMeasurement()
		{
			this.Id = "WBB";
		}

		public override PointF COP()
		{
			int topLeft = TopLeft - cal.TopLeft;
			int topRight = TopRight - cal.TopRight;
			int bottomLeft = BottomLeft - cal.BottomLeft;
			int bottomRight = BottomRight - cal.BottomRight;

			PointF pt;
			pt.X = 0;
			pt.Y = 0;

			int Fz = topLeft + topRight + bottomLeft + bottomRight;

			if (Fz >= 50)
			{
				pt.X = ((BSW / 2) * (float)(topRight + bottomRight - topLeft - bottomLeft)) / (float)Fz;
				pt.Y = ((BSL / 2) * (float)(bottomLeft + bottomRight - topLeft - topRight)) / (float)Fz;
			}

			pt.Y *= -1;

			return pt;
		}
	}

	public class AccuSwayPlusMeasurement : Measurement
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

		public AccuSwayPlusMeasurement()
		{
			this.Id = "ASP";
		}

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

		public override PointF COP()
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

	public class WiimoteMeasurement : Measurement
	{
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

		public WiimoteMeasurement()
		{
			this.Id = "WM";
		}

		public override PointF COP()
		{
			PointF pt;
			pt.X = 0;
			pt.Y = 0;

			return pt;
		}
	}

	public class Sample
	{
		public DateTime Timestamp;
		public long Ticks;
		public List<WiimoteMeasurement> Motes = new List<WiimoteMeasurement>();
		public List<WiiBalanceBoardMeasurement> WiiBoards = new List<WiiBalanceBoardMeasurement>();
		public List<AccuSwayPlusMeasurement> AccuSways = new List<AccuSwayPlusMeasurement>();
	}

	public class SamplingSession
	{
		public DateTime Timestamp = DateTime.Now;
		public String SubjectId = "";
		public long TicksPerSec = 0;
		public List<Sample> Calibration = new List<Sample>();
		public List<Sample> Samples = new List<Sample>();
	}
}
