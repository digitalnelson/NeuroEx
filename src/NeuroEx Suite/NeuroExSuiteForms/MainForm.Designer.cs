namespace AgileMedicine.MovementStudioForms
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnBtHelper = new System.Windows.Forms.Button();
			this.btnStart = new System.Windows.Forms.Button();
			this.btnStop = new System.Windows.Forms.Button();
			this.pnlSensors = new System.Windows.Forms.FlowLayoutPanel();
			this.btnCal = new System.Windows.Forms.Button();
			this.btnCollect = new System.Windows.Forms.Button();
			this.btnStopCollect = new System.Windows.Forms.Button();
			this.lstDevices = new System.Windows.Forms.ListBox();
			this.btnFind = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.txtSubjectId = new System.Windows.Forms.TextBox();
			this.btnNewSession = new System.Windows.Forms.Button();
			this.btnSaveSession = new System.Windows.Forms.Button();
			this.pnlPlatforms = new System.Windows.Forms.Panel();
			this.pnlContainer = new System.Windows.Forms.Panel();
			this.chkAccuSway = new System.Windows.Forms.CheckBox();
			this.pnlContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnBtHelper
			// 
			this.btnBtHelper.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBtHelper.Location = new System.Drawing.Point(785, 12);
			this.btnBtHelper.Name = "btnBtHelper";
			this.btnBtHelper.Size = new System.Drawing.Size(75, 23);
			this.btnBtHelper.TabIndex = 14;
			this.btnBtHelper.Text = "Bluetooth Helper";
			this.btnBtHelper.UseVisualStyleBackColor = true;
			this.btnBtHelper.Click += new System.EventHandler(this.btnBtHelper_Click);
			// 
			// btnStart
			// 
			this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnStart.Enabled = false;
			this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnStart.Location = new System.Drawing.Point(785, 41);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(75, 23);
			this.btnStart.TabIndex = 15;
			this.btnStart.Text = "Connect";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// btnStop
			// 
			this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnStop.Enabled = false;
			this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnStop.Location = new System.Drawing.Point(866, 41);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(75, 23);
			this.btnStop.TabIndex = 16;
			this.btnStop.Text = "Disconnect";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// pnlSensors
			// 
			this.pnlSensors.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlSensors.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnlSensors.Location = new System.Drawing.Point(486, 0);
			this.pnlSensors.Margin = new System.Windows.Forms.Padding(0);
			this.pnlSensors.Name = "pnlSensors";
			this.pnlSensors.Size = new System.Drawing.Size(281, 565);
			this.pnlSensors.TabIndex = 17;
			this.pnlSensors.Visible = false;
			// 
			// btnCal
			// 
			this.btnCal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCal.Enabled = false;
			this.btnCal.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCal.Location = new System.Drawing.Point(785, 259);
			this.btnCal.Name = "btnCal";
			this.btnCal.Size = new System.Drawing.Size(75, 23);
			this.btnCal.TabIndex = 18;
			this.btnCal.Text = "Cal";
			this.btnCal.UseVisualStyleBackColor = true;
			this.btnCal.Click += new System.EventHandler(this.btnCal_Click);
			// 
			// btnCollect
			// 
			this.btnCollect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCollect.Enabled = false;
			this.btnCollect.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCollect.Location = new System.Drawing.Point(785, 288);
			this.btnCollect.Name = "btnCollect";
			this.btnCollect.Size = new System.Drawing.Size(75, 23);
			this.btnCollect.TabIndex = 19;
			this.btnCollect.Text = "Collect";
			this.btnCollect.UseVisualStyleBackColor = true;
			this.btnCollect.Click += new System.EventHandler(this.btnCollect_Click);
			// 
			// btnStopCollect
			// 
			this.btnStopCollect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnStopCollect.Enabled = false;
			this.btnStopCollect.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnStopCollect.Location = new System.Drawing.Point(866, 288);
			this.btnStopCollect.Name = "btnStopCollect";
			this.btnStopCollect.Size = new System.Drawing.Size(75, 23);
			this.btnStopCollect.TabIndex = 20;
			this.btnStopCollect.Text = "Stop";
			this.btnStopCollect.UseVisualStyleBackColor = true;
			this.btnStopCollect.Click += new System.EventHandler(this.btnStopCollect_Click);
			// 
			// lstDevices
			// 
			this.lstDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lstDevices.FormattingEnabled = true;
			this.lstDevices.HorizontalScrollbar = true;
			this.lstDevices.Location = new System.Drawing.Point(785, 70);
			this.lstDevices.Name = "lstDevices";
			this.lstDevices.Size = new System.Drawing.Size(156, 69);
			this.lstDevices.TabIndex = 22;
			// 
			// btnFind
			// 
			this.btnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFind.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnFind.Location = new System.Drawing.Point(866, 12);
			this.btnFind.Name = "btnFind";
			this.btnFind.Size = new System.Drawing.Size(75, 23);
			this.btnFind.TabIndex = 23;
			this.btnFind.Text = "Find";
			this.btnFind.UseVisualStyleBackColor = true;
			this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BackColor = System.Drawing.SystemColors.ButtonShadow;
			this.panel1.Location = new System.Drawing.Point(785, 171);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(156, 10);
			this.panel1.TabIndex = 24;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
			this.label1.Location = new System.Drawing.Point(785, 185);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 13);
			this.label1.TabIndex = 25;
			this.label1.Text = "Subject Id:";
			// 
			// txtSubjectId
			// 
			this.txtSubjectId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSubjectId.Location = new System.Drawing.Point(785, 204);
			this.txtSubjectId.Name = "txtSubjectId";
			this.txtSubjectId.Size = new System.Drawing.Size(156, 20);
			this.txtSubjectId.TabIndex = 26;
			// 
			// btnNewSession
			// 
			this.btnNewSession.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNewSession.Enabled = false;
			this.btnNewSession.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnNewSession.Location = new System.Drawing.Point(785, 230);
			this.btnNewSession.Name = "btnNewSession";
			this.btnNewSession.Size = new System.Drawing.Size(156, 23);
			this.btnNewSession.TabIndex = 27;
			this.btnNewSession.Text = "New Session";
			this.btnNewSession.UseVisualStyleBackColor = true;
			this.btnNewSession.Click += new System.EventHandler(this.btnNewSession_Click);
			// 
			// btnSaveSession
			// 
			this.btnSaveSession.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSaveSession.Enabled = false;
			this.btnSaveSession.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSaveSession.Location = new System.Drawing.Point(785, 317);
			this.btnSaveSession.Name = "btnSaveSession";
			this.btnSaveSession.Size = new System.Drawing.Size(156, 23);
			this.btnSaveSession.TabIndex = 28;
			this.btnSaveSession.Text = "Save Session";
			this.btnSaveSession.UseVisualStyleBackColor = true;
			this.btnSaveSession.Click += new System.EventHandler(this.btnSaveSession_Click);
			// 
			// pnlPlatforms
			// 
			this.pnlPlatforms.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.pnlPlatforms.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlPlatforms.Location = new System.Drawing.Point(0, 0);
			this.pnlPlatforms.Name = "pnlPlatforms";
			this.pnlPlatforms.Size = new System.Drawing.Size(486, 565);
			this.pnlPlatforms.TabIndex = 1;
			// 
			// pnlContainer
			// 
			this.pnlContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pnlContainer.Controls.Add(this.pnlPlatforms);
			this.pnlContainer.Controls.Add(this.pnlSensors);
			this.pnlContainer.Location = new System.Drawing.Point(12, 12);
			this.pnlContainer.Name = "pnlContainer";
			this.pnlContainer.Size = new System.Drawing.Size(767, 565);
			this.pnlContainer.TabIndex = 0;
			// 
			// chkAccuSway
			// 
			this.chkAccuSway.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkAccuSway.AutoSize = true;
			this.chkAccuSway.ForeColor = System.Drawing.SystemColors.Control;
			this.chkAccuSway.Location = new System.Drawing.Point(788, 145);
			this.chkAccuSway.Name = "chkAccuSway";
			this.chkAccuSway.Size = new System.Drawing.Size(111, 17);
			this.chkAccuSway.TabIndex = 29;
			this.chkAccuSway.Text = "Enable Accusway";
			this.chkAccuSway.UseVisualStyleBackColor = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(946, 589);
			this.Controls.Add(this.chkAccuSway);
			this.Controls.Add(this.pnlContainer);
			this.Controls.Add(this.btnSaveSession);
			this.Controls.Add(this.btnNewSession);
			this.Controls.Add(this.txtSubjectId);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.btnFind);
			this.Controls.Add(this.lstDevices);
			this.Controls.Add(this.btnStopCollect);
			this.Controls.Add(this.btnCollect);
			this.Controls.Add(this.btnCal);
			this.Controls.Add(this.btnStop);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.btnBtHelper);
			this.Name = "MainForm";
			this.Text = "NeuroEx Suite";
			this.pnlContainer.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnBtHelper;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.FlowLayoutPanel pnlSensors;
		private System.Windows.Forms.Button btnCal;
		private System.Windows.Forms.Button btnCollect;
		private System.Windows.Forms.Button btnStopCollect;
		private System.Windows.Forms.ListBox lstDevices;
		private System.Windows.Forms.Button btnFind;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtSubjectId;
		private System.Windows.Forms.Button btnNewSession;
		private System.Windows.Forms.Button btnSaveSession;
		private System.Windows.Forms.Panel pnlPlatforms;
		private System.Windows.Forms.Panel pnlContainer;
		private System.Windows.Forms.CheckBox chkAccuSway;

	}
}

