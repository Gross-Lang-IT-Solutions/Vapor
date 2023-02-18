namespace VaporExperiment
{
    partial class Form1
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.titlebar = new VaporExperiment.VaporTitlebar();
			this.SuspendLayout();
			// 
			// titlebar
			// 
			this.titlebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
			this.titlebar.Dock = System.Windows.Forms.DockStyle.Top;
			this.titlebar.Location = new System.Drawing.Point(0, 0);
			this.titlebar.Name = "titlebar";
			this.titlebar.Size = new System.Drawing.Size(800, 30);
			this.titlebar.TabIndex = 0;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 480);
			this.Controls.Add(this.titlebar);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Location = new System.Drawing.Point(0, 0);
			this.MinimumSize = new System.Drawing.Size(640, 480);
			this.Name = "Form1";
			this.Text = "Vapor";
			this.ResumeLayout(false);

        }

		#endregion

		private VaporTitlebar titlebar;
	}
}