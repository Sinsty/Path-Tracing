namespace RayTracing
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
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            TimeElapsedLabel = new System.Windows.Forms.Label();
            TimeLeftLabel = new System.Windows.Forms.Label();
            PixelsRenderedLabel = new System.Windows.Forms.Label();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            ThreadCountNumeric = new System.Windows.Forms.NumericUpDown();
            ChangeThreadsLabel = new System.Windows.Forms.Label();
            StartRenderButton = new System.Windows.Forms.Button();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ThreadCountNumeric).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(TimeElapsedLabel);
            splitContainer1.Panel1.Controls.Add(TimeLeftLabel);
            splitContainer1.Panel1.Controls.Add(PixelsRenderedLabel);
            splitContainer1.Panel1.Controls.Add(pictureBox1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(ThreadCountNumeric);
            splitContainer1.Panel2.Controls.Add(ChangeThreadsLabel);
            splitContainer1.Panel2.Controls.Add(StartRenderButton);
            splitContainer1.Size = new System.Drawing.Size(933, 519);
            splitContainer1.SplitterDistance = 737;
            splitContainer1.TabIndex = 0;
            // 
            // TimeElapsedLabel
            // 
            TimeElapsedLabel.AutoSize = true;
            TimeElapsedLabel.Location = new System.Drawing.Point(11, 23);
            TimeElapsedLabel.Name = "TimeElapsedLabel";
            TimeElapsedLabel.Size = new System.Drawing.Size(88, 15);
            TimeElapsedLabel.TabIndex = 3;
            TimeElapsedLabel.Text = "Time elapsed: 0";
            // 
            // TimeLeftLabel
            // 
            TimeLeftLabel.AutoSize = true;
            TimeLeftLabel.Location = new System.Drawing.Point(11, 38);
            TimeLeftLabel.Name = "TimeLeftLabel";
            TimeLeftLabel.Size = new System.Drawing.Size(65, 15);
            TimeLeftLabel.TabIndex = 2;
            TimeLeftLabel.Text = "Time left: 0";
            // 
            // PixelsRenderedLabel
            // 
            PixelsRenderedLabel.AutoSize = true;
            PixelsRenderedLabel.Location = new System.Drawing.Point(11, 8);
            PixelsRenderedLabel.Name = "PixelsRenderedLabel";
            PixelsRenderedLabel.Size = new System.Drawing.Size(110, 15);
            PixelsRenderedLabel.TabIndex = 1;
            PixelsRenderedLabel.Text = "Pixels rendered: 0/0";
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            pictureBox1.Location = new System.Drawing.Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(735, 517);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // ThreadCountNumeric
            // 
            ThreadCountNumeric.Location = new System.Drawing.Point(3, 36);
            ThreadCountNumeric.Name = "ThreadCountNumeric";
            ThreadCountNumeric.Size = new System.Drawing.Size(120, 23);
            ThreadCountNumeric.TabIndex = 2;
            ThreadCountNumeric.ValueChanged += OnThreadCountNumericValueChange;
            // 
            // ChangeThreadsLabel
            // 
            ChangeThreadsLabel.AutoSize = true;
            ChangeThreadsLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            ChangeThreadsLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            ChangeThreadsLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            ChangeThreadsLabel.Location = new System.Drawing.Point(3, 8);
            ChangeThreadsLabel.Name = "ChangeThreadsLabel";
            ChangeThreadsLabel.Size = new System.Drawing.Size(102, 25);
            ChangeThreadsLabel.TabIndex = 1;
            ChangeThreadsLabel.Text = "Use Treads";
            // 
            // StartRenderButton
            // 
            StartRenderButton.Location = new System.Drawing.Point(104, 483);
            StartRenderButton.Name = "StartRenderButton";
            StartRenderButton.Size = new System.Drawing.Size(75, 23);
            StartRenderButton.TabIndex = 0;
            StartRenderButton.Text = "Render";
            StartRenderButton.UseVisualStyleBackColor = true;
            StartRenderButton.Click += StartRenderButtonClick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(933, 519);
            Controls.Add(splitContainer1);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "Render";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)ThreadCountNumeric).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button StartRenderButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label PixelsRenderedLabel;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label TimeLeftLabel;
        private System.Windows.Forms.Label TimeElapsedLabel;
        private System.Windows.Forms.Label ChangeThreadsLabel;
        private System.Windows.Forms.NumericUpDown ThreadCountNumeric;
    }
}

