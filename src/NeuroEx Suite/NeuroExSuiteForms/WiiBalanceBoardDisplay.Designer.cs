namespace AgileMedicine.MovementStudioForms
{
	partial class WiiBalanceBoardDisplay
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.pnlPath = new System.Windows.Forms.Panel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.lblTL = new System.Windows.Forms.Label();
			this.lblTR = new System.Windows.Forms.Label();
			this.lblBL = new System.Windows.Forms.Label();
			this.lblBR = new System.Windows.Forms.Label();
			this.lblCOP = new System.Windows.Forms.Label();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnClear = new System.Windows.Forms.Button();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.pnlPath);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
			this.splitContainer1.Size = new System.Drawing.Size(443, 282);
			this.splitContainer1.SplitterDistance = 294;
			this.splitContainer1.TabIndex = 0;
			// 
			// pnlPath
			// 
			this.pnlPath.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.pnlPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlPath.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlPath.Location = new System.Drawing.Point(0, 0);
			this.pnlPath.Name = "pnlPath";
			this.pnlPath.Size = new System.Drawing.Size(294, 282);
			this.pnlPath.TabIndex = 0;
			this.pnlPath.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlPath_Paint);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.7931F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.20689F));
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.lblTL, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.lblTR, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.lblBL, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.lblBR, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.lblCOP, 1, 4);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 5;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(145, 282);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(29, 26);
			this.label1.TabIndex = 0;
			this.label1.Text = "Top Left";
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 71);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 26);
			this.label2.TabIndex = 1;
			this.label2.Text = "Top Right";
			// 
			// label3
			// 
			this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 127);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(43, 26);
			this.label3.TabIndex = 2;
			this.label3.Text = "Bottom Left";
			// 
			// label4
			// 
			this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 183);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(43, 26);
			this.label4.TabIndex = 3;
			this.label4.Text = "Bottom Right";
			// 
			// label5
			// 
			this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(3, 246);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(29, 13);
			this.label5.TabIndex = 4;
			this.label5.Text = "COP";
			// 
			// lblTL
			// 
			this.lblTL.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblTL.AutoSize = true;
			this.lblTL.Location = new System.Drawing.Point(52, 21);
			this.lblTL.Name = "lblTL";
			this.lblTL.Size = new System.Drawing.Size(22, 13);
			this.lblTL.TabIndex = 5;
			this.lblTL.Text = "     ";
			// 
			// lblTR
			// 
			this.lblTR.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblTR.AutoSize = true;
			this.lblTR.Location = new System.Drawing.Point(52, 77);
			this.lblTR.Name = "lblTR";
			this.lblTR.Size = new System.Drawing.Size(22, 13);
			this.lblTR.TabIndex = 6;
			this.lblTR.Text = "     ";
			// 
			// lblBL
			// 
			this.lblBL.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblBL.AutoSize = true;
			this.lblBL.Location = new System.Drawing.Point(52, 133);
			this.lblBL.Name = "lblBL";
			this.lblBL.Size = new System.Drawing.Size(22, 13);
			this.lblBL.TabIndex = 7;
			this.lblBL.Text = "     ";
			// 
			// lblBR
			// 
			this.lblBR.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblBR.AutoSize = true;
			this.lblBR.Location = new System.Drawing.Point(52, 189);
			this.lblBR.Name = "lblBR";
			this.lblBR.Size = new System.Drawing.Size(22, 13);
			this.lblBR.TabIndex = 8;
			this.lblBR.Text = "     ";
			// 
			// lblCOP
			// 
			this.lblCOP.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblCOP.AutoSize = true;
			this.lblCOP.Location = new System.Drawing.Point(52, 246);
			this.lblCOP.Name = "lblCOP";
			this.lblCOP.Size = new System.Drawing.Size(22, 13);
			this.lblCOP.TabIndex = 9;
			this.lblCOP.Text = "     ";
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btnClear);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 282);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(443, 31);
			this.flowLayoutPanel1.TabIndex = 1;
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(3, 3);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(75, 23);
			this.btnClear.TabIndex = 0;
			this.btnClear.Text = "Clear";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// WiiBalanceBoardDisplay
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.flowLayoutPanel1);
			this.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
			this.MinimumSize = new System.Drawing.Size(445, 315);
			this.Name = "WiiBalanceBoardDisplay";
			this.Size = new System.Drawing.Size(443, 313);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Panel pnlPath;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label lblTL;
		private System.Windows.Forms.Label lblTR;
		private System.Windows.Forms.Label lblBL;
		private System.Windows.Forms.Label lblBR;
		private System.Windows.Forms.Label lblCOP;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button btnClear;
	}
}
