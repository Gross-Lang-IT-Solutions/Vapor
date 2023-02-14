namespace VaporExperiment;

partial class Form1
{
	/// <summary>
	///  Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	///  Clean up any resources being used.
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
	///  Required method for Designer support - do not modify
	///  the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
            this.borderTop = new System.Windows.Forms.Panel();
            this.borderBottom = new System.Windows.Forms.Panel();
            this.borderLeft = new System.Windows.Forms.Panel();
            this.borderRight = new System.Windows.Forms.Panel();
            this.titlebar = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // borderTop
            // 
            this.borderTop.BackColor = System.Drawing.Color.DimGray;
            this.borderTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.borderTop.Location = new System.Drawing.Point(0, 0);
            this.borderTop.Name = "borderTop";
            this.borderTop.Size = new System.Drawing.Size(800, 1);
            this.borderTop.TabIndex = 0;
            // 
            // borderBottom
            // 
            this.borderBottom.BackColor = System.Drawing.Color.DimGray;
            this.borderBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.borderBottom.Location = new System.Drawing.Point(0, 449);
            this.borderBottom.Name = "borderBottom";
            this.borderBottom.Size = new System.Drawing.Size(800, 1);
            this.borderBottom.TabIndex = 1;
            // 
            // borderLeft
            // 
            this.borderLeft.BackColor = System.Drawing.Color.DimGray;
            this.borderLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.borderLeft.Location = new System.Drawing.Point(0, 1);
            this.borderLeft.Name = "borderLeft";
            this.borderLeft.Size = new System.Drawing.Size(1, 448);
            this.borderLeft.TabIndex = 2;
            // 
            // borderRight
            // 
            this.borderRight.BackColor = System.Drawing.Color.DimGray;
            this.borderRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.borderRight.Location = new System.Drawing.Point(799, 1);
            this.borderRight.Name = "borderRight";
            this.borderRight.Size = new System.Drawing.Size(1, 448);
            this.borderRight.TabIndex = 3;
            // 
            // titlebar
            // 
            this.titlebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.titlebar.Dock = System.Windows.Forms.DockStyle.Top;
            this.titlebar.Location = new System.Drawing.Point(1, 1);
            this.titlebar.Name = "titlebar";
            this.titlebar.Size = new System.Drawing.Size(798, 30);
            this.titlebar.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.titlebar);
            this.Controls.Add(this.borderRight);
            this.Controls.Add(this.borderLeft);
            this.Controls.Add(this.borderBottom);
            this.Controls.Add(this.borderTop);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Vapor";
            this.ResumeLayout(false);

	}

    #endregion

    private Panel borderTop;
    private Panel borderBottom;
    private Panel borderLeft;
    private Panel borderRight;
    private Panel titlebar;
}
