namespace AgileMedicine.MovementStudioForms
{
	partial class BTPairingHelper
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
			this.btnConnect = new System.Windows.Forms.Button();
			this.btnDisconnect = new System.Windows.Forms.Button();
			this.btnPair = new System.Windows.Forms.Button();
			this.btnFindDevices = new System.Windows.Forms.Button();
			this.lstDevices = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// btnConnect
			// 
			this.btnConnect.Location = new System.Drawing.Point(295, 70);
			this.btnConnect.Name = "btnConnect";
			this.btnConnect.Size = new System.Drawing.Size(75, 23);
			this.btnConnect.TabIndex = 23;
			this.btnConnect.Text = "Connect";
			this.btnConnect.UseVisualStyleBackColor = true;
			this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
			// 
			// btnDisconnect
			// 
			this.btnDisconnect.Location = new System.Drawing.Point(295, 99);
			this.btnDisconnect.Name = "btnDisconnect";
			this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
			this.btnDisconnect.TabIndex = 22;
			this.btnDisconnect.Text = "Disconnect";
			this.btnDisconnect.UseVisualStyleBackColor = true;
			this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
			// 
			// btnPair
			// 
			this.btnPair.Location = new System.Drawing.Point(295, 41);
			this.btnPair.Name = "btnPair";
			this.btnPair.Size = new System.Drawing.Size(75, 23);
			this.btnPair.TabIndex = 21;
			this.btnPair.Text = "Pair";
			this.btnPair.UseVisualStyleBackColor = true;
			this.btnPair.Click += new System.EventHandler(this.btnPair_Click);
			// 
			// btnFindDevices
			// 
			this.btnFindDevices.Location = new System.Drawing.Point(295, 12);
			this.btnFindDevices.Name = "btnFindDevices";
			this.btnFindDevices.Size = new System.Drawing.Size(75, 23);
			this.btnFindDevices.TabIndex = 20;
			this.btnFindDevices.Text = "Find";
			this.btnFindDevices.UseVisualStyleBackColor = true;
			this.btnFindDevices.Click += new System.EventHandler(this.btnFindDevices_Click);
			// 
			// lstDevices
			// 
			this.lstDevices.FormattingEnabled = true;
			this.lstDevices.Location = new System.Drawing.Point(12, 12);
			this.lstDevices.Name = "lstDevices";
			this.lstDevices.Size = new System.Drawing.Size(277, 290);
			this.lstDevices.TabIndex = 19;
			// 
			// BTPairingHelper
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(379, 314);
			this.Controls.Add(this.btnConnect);
			this.Controls.Add(this.btnDisconnect);
			this.Controls.Add(this.btnPair);
			this.Controls.Add(this.btnFindDevices);
			this.Controls.Add(this.lstDevices);
			this.Name = "BTPairingHelper";
			this.Text = "BTPairingHelper";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnConnect;
		private System.Windows.Forms.Button btnDisconnect;
		private System.Windows.Forms.Button btnPair;
		private System.Windows.Forms.Button btnFindDevices;
		private System.Windows.Forms.ListBox lstDevices;
	}
}