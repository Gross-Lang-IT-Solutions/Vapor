namespace VaporExperiment;

partial class VaporTitlebar
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
			this.CloseButton = new System.Windows.Forms.Panel();
			this.MaximizeRestoreButton = new System.Windows.Forms.Panel();
			this.MinimizeButton = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// CloseButton
			// 
			this.CloseButton.Dock = System.Windows.Forms.DockStyle.Right;
			this.CloseButton.Location = new System.Drawing.Point(601, 0);
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.Size = new System.Drawing.Size(46, 30);
			this.CloseButton.TabIndex = 0;
			this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
			// 
			// MaximizeRestoreButton
			// 
			this.MaximizeRestoreButton.Dock = System.Windows.Forms.DockStyle.Right;
			this.MaximizeRestoreButton.Location = new System.Drawing.Point(555, 0);
			this.MaximizeRestoreButton.Name = "MaximizeRestoreButton";
			this.MaximizeRestoreButton.Size = new System.Drawing.Size(46, 30);
			this.MaximizeRestoreButton.TabIndex = 1;
			this.MaximizeRestoreButton.Click += new System.EventHandler(this.MaximizeRestoreButton_Click);
			// 
			// MinimizeButton
			// 
			this.MinimizeButton.Dock = System.Windows.Forms.DockStyle.Right;
			this.MinimizeButton.Location = new System.Drawing.Point(509, 0);
			this.MinimizeButton.Name = "MinimizeButton";
			this.MinimizeButton.Size = new System.Drawing.Size(46, 30);
			this.MinimizeButton.TabIndex = 2;
			this.MinimizeButton.Click += new System.EventHandler(this.MinimizeButton_Click);
			// 
			// VaporTitlebar
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.Controls.Add(this.MinimizeButton);
			this.Controls.Add(this.MaximizeRestoreButton);
			this.Controls.Add(this.CloseButton);
			this.DoubleBuffered = true;
			this.Name = "VaporTitlebar";
			this.Size = new System.Drawing.Size(647, 30);
			this.ResumeLayout(false);

	}

	#endregion

	internal Panel CloseButton;
	internal Panel MaximizeRestoreButton;
	internal Panel MinimizeButton;
}
