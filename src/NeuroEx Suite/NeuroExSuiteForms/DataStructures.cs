using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace AgileMedicine.MovementStudioForms
{
	public class ExperiementStep
	{
		public String Name;
		public String Direction;
		public int Duration;
		public Brush Brush;

		public override string ToString()
		{
			return Name;
		}
	}

	public class SubjectSession
	{
		public int Age;
		public Range WeightLb = new Range();
		public Range WeightKg = new Range();
		public Guid Id = Guid.NewGuid();
		public int K;

		public List<SubjectStep> Steps = new List<SubjectStep>();
	}

	public class SubjectStep
	{
		public string Name;

		public double L;
		public double d;
		public double FD;
		public double dL;
		public double AvgVec0;
		public double Expanse;

		public double AvgX;
		public double AvgY;
		public CartesianRange Range;

		public List<BalanceMeasurement> BalanceMeasurements = new List<BalanceMeasurement>();
	}

	public class BalanceMeasurement
	{
		public long Ticks;
		public float X;
		public float Y;

		public BalanceValues RawValues;
		public BalanceValues CalibrationValuesKg0;
		public BalanceValues CalibrationValuesKg17;
		public BalanceValues CalibrationValuesKg34;
	}

	public class BalanceValues
	{
		public int TopLeft;
		public int TopRight;
		public int BottomLeft;
		public int BottomRight;
	}

	public class Range
	{
		public float Max = -99999999999f;
		public float Min = 99999999999f;
		public float Avg = 0;

		private float total = 0;
		private float count = 0;

		public void Update(float val)
		{
			if (val > Max)
				Max = val;
			if (val < Min)
				Min = val;

			total += val;
			count++;

			Avg = total / count;
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
		public long TicksPerSec = Stopwatch.Frequency;
		public List<Sample> Calibration = new List<Sample>();
		public List<Sample> Samples = new List<Sample>();
	}
}
