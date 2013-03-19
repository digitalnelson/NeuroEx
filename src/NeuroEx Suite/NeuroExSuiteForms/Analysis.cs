using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Collections;

namespace AgileMedicine.MovementStudioForms
{
	public partial class Analysis : Form
	{
		private Dictionary<string, SubjectSession> subjects = new Dictionary<string, SubjectSession>();

		public Analysis()
		{
			InitializeComponent();
		}

		private void btnLoadData_Click(object sender, EventArgs e)
		{
			DataTable dt = new DataTable();

			dt.Columns.Add("Subject");
			dt.Columns.Add("Age");
			dt.Columns.Add("Eyes Open Feet Apart");
			dt.Columns.Add("Eyes Open Feet Together");
			dt.Columns.Add("Eyes Closed Feet Apart");
			dt.Columns.Add("Eyes Closed Feet Together");
	
			string[] files = Directory.GetFiles(@"C:\Files\SVN\svn.digitalnelson.com\BalanceBoard\BalanceBoard\bin\Debug\Prospective_Data_Normals");

			foreach (string file in files)
			{
				SubjectSession sess = null;
				using (StreamReader sr = new StreamReader(file))
				{
					XmlSerializer ser = new XmlSerializer(typeof(SubjectSession));
					sess = (SubjectSession)ser.Deserialize(sr);
				}

				subjects[sess.Id.ToString()] = sess;

				DataRow row = dt.NewRow();
				row["Subject"] = sess.Id;
				row["Age"] = sess.Age;

				foreach (SubjectStep step in sess.Steps)
				{
					double maxVec0 = -99999999999;
					double totalVec0 = 0;
					double L = 0;
					double d = 0;

					double preX = 0;
					double preY = 0;
					bool firstPoint = true;
					CartesianRange range = new CartesianRange();

					double totX = 0;
					double totY = 0;

					foreach (BalanceMeasurement meas in step.BalanceMeasurements)
					{
						range.UpdatePair(meas.X, meas.Y);

						if (firstPoint)
						{
							preX = meas.X;
							preY = meas.Y;
							firstPoint = false;
						}
						else
						{
							L += Math.Sqrt(Math.Pow(Math.Abs(meas.X - preX), 2) + Math.Pow(Math.Abs(meas.Y - preY), 2));
						}

						totX += meas.X;
						totY += meas.Y;

						double magVec0 = Math.Sqrt(Math.Pow(meas.X, 2) + Math.Pow(meas.Y, 2));

						if (magVec0 > maxVec0)
							maxVec0 = magVec0;

						totalVec0 += magVec0;
					}

					step.Range = range;
					step.AvgX = totX / step.BalanceMeasurements.Count;
					step.AvgY = totY / step.BalanceMeasurements.Count;

					double d1 = Math.Sqrt(Math.Pow(Math.Abs(range.MaxX.X - range.MinX.X), 2) + Math.Pow(Math.Abs(range.MaxX.Y - range.MinX.Y), 2));
					double d2 = Math.Sqrt(Math.Pow(Math.Abs(range.MaxX.X - range.MaxY.X), 2) + Math.Pow(Math.Abs(range.MaxX.Y - range.MaxY.Y), 2));
					double d3 = Math.Sqrt(Math.Pow(Math.Abs(range.MaxX.X - range.MinY.X), 2) + Math.Pow(Math.Abs(range.MaxX.Y - range.MinY.Y), 2));
					double d4 = Math.Sqrt(Math.Pow(Math.Abs(range.MinX.X - range.MaxY.X), 2) + Math.Pow(Math.Abs(range.MinX.Y - range.MaxY.Y), 2));
					double d5 = Math.Sqrt(Math.Pow(Math.Abs(range.MinX.X - range.MinY.X), 2) + Math.Pow(Math.Abs(range.MinX.Y - range.MinY.Y), 2));
					double d6 = Math.Sqrt(Math.Pow(Math.Abs(range.MaxY.X - range.MinY.X), 2) + Math.Pow(Math.Abs(range.MaxY.Y - range.MinY.Y), 2));

					d = d1;
					if (d2 > d)
						d = d2;
					if (d3 > d)
						d = d3;
					if (d4 > d)
						d = d4;
					if (d5 > d)
						d = d5;
					if (d6 > d)
						d = d6;

					step.Expanse = Math.Sqrt(Math.Pow(Math.Abs(range.MaxX.X - range.MinX.X), 2) + Math.Pow(Math.Abs(range.MaxY.Y - range.MinY.Y), 2));
					step.AvgVec0 = totalVec0 / step.BalanceMeasurements.Count;
					double n = step.BalanceMeasurements.Count - 1;

					step.dL = d * L;
					step.FD = Math.Log(L / d) / Math.Log(n);

					step.d = d;
					step.L = L;

					row[step.Name] = step.dL;
				}

				dt.Rows.Add(row);
			}

			BindingSource source = new BindingSource();
			source.DataSource = dt;
			dgResults.DataSource = source;
		}

		//private Brush[] stepBrushes = new Brush[4] {Brushes.Blue, Brushes.Green, Brushes.Yellow, Brushes.Red };


		private void btnDetails_Click(object sender, EventArgs e)
		{
			Dictionary<string, Brush> stepBrushes = new Dictionary<string, Brush>();
			stepBrushes.Add("Eyes Open Feet Apart", new SolidBrush(Color.FromArgb(255, Color.White)));
			stepBrushes.Add("Eyes Closed Feet Apart", new SolidBrush(Color.FromArgb(255, Color.Yellow)));
			stepBrushes.Add("Eyes Open Feet Together", new SolidBrush(Color.FromArgb(255, Color.Pink)));
			stepBrushes.Add("Eyes Closed Feet Together", new SolidBrush(Color.FromArgb(255, Color.Blue)));

			Dictionary<string, Brush> stepBrushesAlt = new Dictionary<string, Brush>();
			stepBrushesAlt.Add("Eyes Open Feet Apart", new SolidBrush(Color.FromArgb(255, Color.White)));
			stepBrushesAlt.Add("Eyes Closed Feet Apart", new SolidBrush(Color.FromArgb(255, Color.Yellow)));
			stepBrushesAlt.Add("Eyes Open Feet Together", new SolidBrush(Color.FromArgb(255, Color.Pink)));
			stepBrushesAlt.Add("Eyes Closed Feet Together", new SolidBrush(Color.FromArgb(255, Color.Blue)));
			
			if (dgResults.SelectedRows.Count > 0)
			{
				DataTable dt = new DataTable();
				dt.Columns.Add("Step");
				dt.Columns.Add("dL");
				dt.Columns.Add("FD");
				dt.Columns.Add("d");
				dt.Columns.Add("L");
				dt.Columns.Add("Measurements");
				dt.Columns.Add("AvgVec0");
				dt.Columns.Add("Expanse");
				dt.Columns.Add("Id");

				using (Graphics g = pnlGraph.CreateGraphics())
				{
					g.Clear(Color.Black);

					foreach (DataGridViewRow dgRow in dgResults.SelectedRows)
					{
						DataRow row = ((DataRowView)dgRow.DataBoundItem).Row;

						string id = row["Subject"].ToString();
						SubjectSession sess = subjects[id];

						Range xRange = new Range();
						Range yRange = new Range();

						int stepCount = 0;
						foreach (SubjectStep step in sess.Steps)
						{
							Brush brush = stepBrushes[step.Name];

							foreach (BalanceMeasurement meas in step.BalanceMeasurements)
							{
								xRange.Update(meas.X);
								yRange.Update(meas.Y);
							}
						}

						xRange.Update((float)Math.Ceiling(xRange.Max));
						xRange.Update((float)Math.Floor(xRange.Min));

						yRange.Update((float)Math.Ceiling(yRange.Max));
						yRange.Update((float)Math.Floor(yRange.Min));

						foreach (SubjectStep step in sess.Steps)
						{
							DataRow propRow = dt.NewRow();
							propRow["Step"] = step.Name;
							propRow["dL"] = step.dL;
							propRow["FD"] = step.FD;
							propRow["d"] = step.d;
							propRow["L"] = step.L;
							propRow["Measurements"] = step.BalanceMeasurements.Count;
							propRow["AvgVec0"] = step.AvgVec0;
							propRow["Expanse"] = step.Expanse;
							propRow["Id"] = sess.Id.ToString();

							Brush brush = stepBrushes[step.Name];
							Brush brushAlt = stepBrushesAlt[step.Name];

							foreach (BalanceMeasurement meas in step.BalanceMeasurements)
							{
								PointF loc = TranslatePoint(meas.X, meas.Y, xRange, yRange, step);

								try
								{
									g.FillEllipse(brush, loc.X - 1, loc.Y - 1, 3, 3);
								}
								catch (Exception) { }
							}

							PointF minX = TranslatePoint(step.Range.MinX.X, step.Range.MinX.Y, xRange, yRange, step);
							PointF maxX = TranslatePoint(step.Range.MaxX.X, step.Range.MaxX.Y, xRange, yRange, step);
							PointF minY = TranslatePoint(step.Range.MinY.X, step.Range.MinY.Y, xRange, yRange, step);
							PointF maxY = TranslatePoint(step.Range.MaxY.X, step.Range.MaxY.Y, xRange, yRange, step);


							g.FillRectangle(brushAlt, minX.X - 5, minX.Y - 5, 10, 10);
							g.FillRectangle(brushAlt, maxX.X - 5, maxX.Y - 5, 10, 10);
							g.FillRectangle(brushAlt, minY.X - 5, minY.Y - 5, 10, 10);
							g.FillRectangle(brushAlt, maxY.X - 5, maxY.Y - 5, 10, 10);

							stepCount++;

							dt.Rows.Add(propRow);
						}
					}
				}

				BindingSource source = new BindingSource();
				source.DataSource = dt;
				dgProps.DataSource = source;
			}
		}

		public PointF TranslatePoint(double x, double y, Range xRange, Range yRange, SubjectStep step)
		{
			float fX = (float) ((x + Math.Abs(xRange.Min)) * pnlGraph.Width) / ((xRange.Max + Math.Abs(xRange.Min)) - (xRange.Min + Math.Abs(xRange.Min)));
			float fY = (float) ((y + Math.Abs(yRange.Min)) * pnlGraph.Width) / ((yRange.Max + Math.Abs(yRange.Min)) - (yRange.Min + Math.Abs(yRange.Min)));

			return new PointF(fX, fY);
		}
	}

	public class CartesianRange
	{
		public CartesianPair MinX = new CartesianPair() { X = Double.MaxValue, Y = 0 };
		public CartesianPair MaxX = new CartesianPair() { X = Double.MinValue, Y = 0 };
		public CartesianPair MinY = new CartesianPair() { X = 0, Y = Double.MaxValue };
		public CartesianPair MaxY = new CartesianPair() { X = 0, Y = Double.MinValue };

		public void UpdatePair(double X, double Y)
		{
			if (X < MinX.X)
				MinX = new CartesianPair(X, Y);
			if (X > MaxX.X)
				MaxX = new CartesianPair(X, Y);
			if (Y < MinY.Y)
				MinY = new CartesianPair(X, Y);
			if (Y > MaxY.Y)
				MaxY = new CartesianPair(X, Y);
		}
	}

	public class CartesianPair
	{
		public double X;
		public double Y;

		public CartesianPair() { }

		public CartesianPair(double x, double y)
		{
			X = x;
			Y = y;
		}
	}
}
