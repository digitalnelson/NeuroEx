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

namespace AgileMedicine.MovementStudioForms
{
	public partial class ExperiementDesigner : Form
	{
		//bool run = false;
		//SpeechSynthesizer speech;

		//private List<ExperiementStep> steps;
		//private Permutation currentPermutation;
		//private int stepCount;

		//Timer stepTimer = new Timer();

		//private ExperiementStep CurrentStep;
		//private SubjectSession ActiveSession;
		//private SubjectStep ActiveStep;

		//private State CurrentState = State.NewSubject;

		public ExperiementDesigner()
		{
			//InitializeComponent();

			//this.SetStyle(ControlStyles.DoubleBuffer |
			//        ControlStyles.OptimizedDoubleBuffer |
			//        ControlStyles.UserPaint |
			//        ControlStyles.AllPaintingInWmPaint, true);

			//this.UpdateStyles();

			//speech = new SpeechSynthesizer();
			//steps = new List<ExperiementStep>();
			//stepCount = 0;

			//speech.Rate = -3;

			//steps.Add(new ExperiementStep() { Name = "Eyes Open Feet Apart", Duration = 30000, Direction = "Eyes open and feet apart.", Brush = Brushes.Red });
			//steps.Add(new ExperiementStep() { Name = "Eyes Closed Feet Apart", Duration = 30000, Direction = "Eyes closed and feet apart.", Brush = Brushes.Blue });
			//steps.Add(new ExperiementStep() { Name = "Eyes Open Feet Together", Duration = 30000, Direction = "Eyes open and feet together.", Brush = Brushes.Green });
			//steps.Add(new ExperiementStep() { Name = "Eyes Closed Feet Together", Duration = 30000, Direction = "Eyes closed and feet together.", Brush = Brushes.Yellow });

			////stepTimer.Tick += new EventHandler(stepTimer_Tick);

			////_btnRun.Text = "Load Next Subject";
		}

		private void ExperiementDesigner_Load(object sender, EventArgs e)
		{

		}

		private delegate void UpdateWiimoteStateDelegate(WiimoteChangedEventArgs args);

		void wm_WiimoteChanged(object sender, WiimoteChangedEventArgs args)
		{
			/*if (run)
			{
				// current state information
				WiimoteState ws = args.WiimoteState;

				if (args.WiimoteState.ExtensionType == ExtensionType.BalanceBoard)
				{

					float fX = ws.BalanceBoardState.CenterOfGravity.X;
					float fY = ws.BalanceBoardState.CenterOfGravity.Y;

					BalanceBoardState s = ws.BalanceBoardState;

					ActiveSession.WeightLb.Update(s.WeightLb);
					ActiveSession.WeightKg.Update(s.WeightKg);

					BalanceMeasurement meas = new BalanceMeasurement() { Ticks = DateTime.Now.Ticks, X = fX, Y = fY };
					BalanceValues raw = new BalanceValues() { BottomLeft = s.SensorValuesRaw.BottomLeft, BottomRight = s.SensorValuesRaw.BottomRight, TopLeft = s.SensorValuesRaw.TopLeft, TopRight = s.SensorValuesRaw.TopRight };
					BalanceValues calKg0 = new BalanceValues() { BottomLeft = s.CalibrationInfo.Kg0.BottomLeft, BottomRight = s.CalibrationInfo.Kg0.BottomRight, TopLeft = s.CalibrationInfo.Kg0.TopLeft, TopRight = s.CalibrationInfo.Kg0.TopRight };
					BalanceValues calKg17 = new BalanceValues() { BottomLeft = s.CalibrationInfo.Kg17.BottomLeft, BottomRight = s.CalibrationInfo.Kg17.BottomRight, TopLeft = s.CalibrationInfo.Kg17.TopLeft, TopRight = s.CalibrationInfo.Kg17.TopRight };
					BalanceValues calKg34 = new BalanceValues() { BottomLeft = s.CalibrationInfo.Kg34.BottomLeft, BottomRight = s.CalibrationInfo.Kg34.BottomRight, TopLeft = s.CalibrationInfo.Kg34.TopLeft, TopRight = s.CalibrationInfo.Kg34.TopRight };
					meas.RawValues = raw;
					meas.CalibrationValuesKg0 = calKg0;
					meas.CalibrationValuesKg17 = calKg17;
					meas.CalibrationValuesKg34 = calKg34;

					ActiveStep.BalanceMeasurements.Add(meas);

					float fTransX = (fX * (pnlTrace.Width)) / 30;
					float fTransY = (fY * (pnlTrace.Height)) / 30;

					int centerX = pnlTrace.Width / 2;
					int centerY = pnlTrace.Height / 2;

					try
					{
						using (Graphics g = pnlTrace.CreateGraphics())
						{
							if (CurrentStep != null && CurrentStep.Brush != null)
								g.FillEllipse(CurrentStep.Brush, centerX + fTransX, centerY + fTransY, 6, 6);
							else
								g.FillEllipse(Brushes.White, centerX + fTransX, centerY + fTransY, 6, 6);
						}
					}
					catch (Exception) { }
				}
				else
				{
					this.Invoke(new UpdateWiimoteStateDelegate(UpdateMote), args);
				}

				this.Invoke(new UpdateWiimoteStateDelegate(UpdateWiimoteChanged), args);
			}*/
		}

		private void btnToggle_Click(object sender, EventArgs e)
		{
			/*if (CurrentState == State.Stepping)
			{
				_btnRun.Text = "Running...";
				_btnRun.Enabled = false;
				System.Threading.Thread.Sleep(2000);

				RunNextExperimentStep();
			}
			else if (CurrentState == State.NewSubject)
			{
				speech.SpeakAsync("Connecting to balance board.");

				try
				{
					
					WiimoteCollection coll = new WiimoteCollection();
					coll.FindAllWiimotes();

					foreach (Wiimote mote in coll)
					{
						mote.Connect();

						string path = mote.HIDDevicePath;
						Guid id = mote.ID;
						WiimoteState state = mote.WiimoteState;

						mote.WiimoteChanged += new EventHandler<WiimoteChangedEventArgs>(wm_WiimoteChanged);
					}

					speech.SpeakAsync("Balance board connected.");
				}
				catch (Exception)
				{
					MessageBox.Show("Could not find balance board.");
				}

				speech.SpeakAsync("Loading experiment for new subject.");

				_btnRun.Text = "Loading...";
				_btnRun.Enabled = false;

				using (Graphics g = pnlTrace.CreateGraphics())
				{
					g.Clear(Color.Black);
				}

				// Reset step count
				stepCount = 0;

				// If first run of the steps
				ActiveSession = new SubjectSession();

				// Collect age
				if (txtAge.Text.Length == 0)
				{
					MessageBox.Show("Please set age");
					return;
				}
				ActiveSession.Age = Convert.ToInt32(txtAge.Text);

				currentPermutation = new Permutation(steps.Count, Convert.ToInt32(txtInitialK.Text));
				ActiveSession.K = currentPermutation.K;

				LoadNextExperimentStep();
			}*/
		}

		private void LoadNextExperimentStep()
		{
			//if (stepCount < steps.Count)
			//{
			//    speech.SpeakAsync("Beginning experiment step.");

			//    ExperiementStep step = steps[currentPermutation.Idxs[stepCount]];
			//    CurrentStep = step;

			//    // Put graphical instructions on screen
			//    txtInstructions.Text = step.Direction;

			//    // Read directions to subject
			//    speech.Speak(step.Direction);

			//    // Set new step to collect data to
			//    ActiveStep = new SubjectStep();
			//    ActiveStep.Name = step.Name;
			//    ActiveSession.Steps.Add(ActiveStep);

			//    // Set our timer length
			//    stepTimer.Interval = step.Duration;

			//    CurrentState = State.Stepping;
			//    _btnRun.Text = "Run";
			//    _btnRun.Enabled = true;
			//}
			//else
			//{
			//    CurrentState = State.NewSubject;

			//    // Clear out interface items
			//    txtInstructions.Text = "";
			//    _btnRun.Text = "Complete";
			//    _btnRun.Enabled = true;

			//    speech.SpeakAsync("Experiment session complete.  Thank you for your participation.  Ready for next subject.");

			//    DateTime t = DateTime.Now;

			//    // Save patient run data
			//    using (StreamWriter sw = new StreamWriter(t.ToString("yyyymmddhhmm") + "-" + currentPermutation.K.ToString() + "-" + ActiveSession.Id.ToString().Replace("-", "").Replace("{", "").Replace("}", "") + ".xml"))
			//    {
			//        XmlSerializer ser = new XmlSerializer(typeof(SubjectSession));
			//        ser.Serialize(sw, ActiveSession);
			//    }

			//    // TODO: Update experiment file with new completed k val

			//    // Queue up stuff for next pt
			//    int k = Convert.ToInt32(txtInitialK.Text) + 1;
			//    if (k < 24)
			//        txtInitialK.Text = k.ToString();
			//    else
			//        txtInitialK.Text = "0";

			//    _btnRun.Text = "Load Next Subject";

			//    //if (wiimote != null)
			//    //	wiimote.Disconnect();
			//}
		}

		private void RunNextExperimentStep()
		{
			//if (stepCount < steps.Count)
			//{
			//    speech.SpeakAsync("Starting step.");

			//    // Start the step timer
			//    stepTimer.Start();

			//    // Enable data collection
			//    run = true;
			//}
			//else
			//{
			//    CurrentState = State.NewSubject;
			//    throw new ArgumentException("No experiement loaded");
			//}
		}

		void stepTimer_Tick(object sender, EventArgs e)
		{
			//// Stop our timer so it doesn't fire again
			//stepTimer.Stop();

			//// Stop collecting data
			//run = false;

			//// Advance to next step
			//stepCount++;

			//_btnRun.Text = "Step complete.";

			//speech.SpeakAsync("Step complete.");

			//System.Threading.Thread.Sleep(1500);

			//// Load next step
			//LoadNextExperimentStep();
		}
	}

	public enum State
	{
		NewSubject,
		Stepping
	}
}
