namespace RayTracing
{
    partial class RenderForm
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
            pictureBox1 = new System.Windows.Forms.PictureBox();
            saveImageButton = new System.Windows.Forms.Button();
            useKDTreeCheckBox = new System.Windows.Forms.CheckBox();
            sahPlanesDivisionsLabel = new System.Windows.Forms.Label();
            MinObjectsInBoundingBox = new System.Windows.Forms.Label();
            maxDepthLabel = new System.Windows.Forms.Label();
            sahPlanesDivisionsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            minObjectsInBoundingBoxNumericUpDown = new System.Windows.Forms.NumericUpDown();
            kdTreeMaxDepthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            renderingLabel = new System.Windows.Forms.Label();
            kdTreeLabel = new System.Windows.Forms.Label();
            TimeElapsedLabel = new System.Windows.Forms.Label();
            imageResolutionLabel = new System.Windows.Forms.Label();
            TimeLeftLabel = new System.Windows.Forms.Label();
            imageHeightNumericUpDown = new System.Windows.Forms.NumericUpDown();
            PixelsRenderedLabel = new System.Windows.Forms.Label();
            imageWidthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            rayBouncesCountLabel = new System.Windows.Forms.Label();
            samplesCountLabel = new System.Windows.Forms.Label();
            rayBouncesNumericUpDown = new System.Windows.Forms.NumericUpDown();
            samplesCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            StartRenderButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sahPlanesDivisionsNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)minObjectsInBoundingBoxNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kdTreeMaxDepthNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)imageHeightNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)imageWidthNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)rayBouncesNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)samplesCountNumericUpDown).BeginInit();
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
            splitContainer1.Panel1.Controls.Add(pictureBox1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(saveImageButton);
            splitContainer1.Panel2.Controls.Add(useKDTreeCheckBox);
            splitContainer1.Panel2.Controls.Add(sahPlanesDivisionsLabel);
            splitContainer1.Panel2.Controls.Add(MinObjectsInBoundingBox);
            splitContainer1.Panel2.Controls.Add(maxDepthLabel);
            splitContainer1.Panel2.Controls.Add(sahPlanesDivisionsNumericUpDown);
            splitContainer1.Panel2.Controls.Add(minObjectsInBoundingBoxNumericUpDown);
            splitContainer1.Panel2.Controls.Add(kdTreeMaxDepthNumericUpDown);
            splitContainer1.Panel2.Controls.Add(renderingLabel);
            splitContainer1.Panel2.Controls.Add(kdTreeLabel);
            splitContainer1.Panel2.Controls.Add(TimeElapsedLabel);
            splitContainer1.Panel2.Controls.Add(imageResolutionLabel);
            splitContainer1.Panel2.Controls.Add(TimeLeftLabel);
            splitContainer1.Panel2.Controls.Add(imageHeightNumericUpDown);
            splitContainer1.Panel2.Controls.Add(PixelsRenderedLabel);
            splitContainer1.Panel2.Controls.Add(imageWidthNumericUpDown);
            splitContainer1.Panel2.Controls.Add(rayBouncesCountLabel);
            splitContainer1.Panel2.Controls.Add(samplesCountLabel);
            splitContainer1.Panel2.Controls.Add(rayBouncesNumericUpDown);
            splitContainer1.Panel2.Controls.Add(samplesCountNumericUpDown);
            splitContainer1.Panel2.Controls.Add(StartRenderButton);
            splitContainer1.Size = new System.Drawing.Size(933, 519);
            splitContainer1.SplitterDistance = 722;
            splitContainer1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            pictureBox1.Location = new System.Drawing.Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(720, 517);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // saveImageButton
            // 
            saveImageButton.Location = new System.Drawing.Point(104, 454);
            saveImageButton.Name = "saveImageButton";
            saveImageButton.Size = new System.Drawing.Size(75, 23);
            saveImageButton.TabIndex = 17;
            saveImageButton.Text = "Save Image";
            saveImageButton.UseVisualStyleBackColor = true;
            saveImageButton.Click += saveImageButton_Click;
            // 
            // useKDTreeCheckBox
            // 
            useKDTreeCheckBox.AutoSize = true;
            useKDTreeCheckBox.Checked = true;
            useKDTreeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            useKDTreeCheckBox.Location = new System.Drawing.Point(3, 238);
            useKDTreeCheckBox.Name = "useKDTreeCheckBox";
            useKDTreeCheckBox.Size = new System.Drawing.Size(89, 19);
            useKDTreeCheckBox.TabIndex = 16;
            useKDTreeCheckBox.Text = "Use KD-Tree";
            useKDTreeCheckBox.UseVisualStyleBackColor = true;
            // 
            // sahPlanesDivisionsLabel
            // 
            sahPlanesDivisionsLabel.AutoSize = true;
            sahPlanesDivisionsLabel.Location = new System.Drawing.Point(0, 348);
            sahPlanesDivisionsLabel.Name = "sahPlanesDivisionsLabel";
            sahPlanesDivisionsLabel.Size = new System.Drawing.Size(116, 15);
            sahPlanesDivisionsLabel.TabIndex = 15;
            sahPlanesDivisionsLabel.Text = "SAH planes divisions";
            // 
            // MinObjectsInBoundingBox
            // 
            MinObjectsInBoundingBox.AutoSize = true;
            MinObjectsInBoundingBox.Location = new System.Drawing.Point(3, 304);
            MinObjectsInBoundingBox.Name = "MinObjectsInBoundingBox";
            MinObjectsInBoundingBox.Size = new System.Drawing.Size(160, 15);
            MinObjectsInBoundingBox.TabIndex = 14;
            MinObjectsInBoundingBox.Text = "Min objects in bounding box";
            // 
            // maxDepthLabel
            // 
            maxDepthLabel.AutoSize = true;
            maxDepthLabel.Location = new System.Drawing.Point(3, 260);
            maxDepthLabel.Name = "maxDepthLabel";
            maxDepthLabel.Size = new System.Drawing.Size(64, 15);
            maxDepthLabel.TabIndex = 13;
            maxDepthLabel.Text = "Max depth";
            // 
            // sahPlanesDivisionsNumericUpDown
            // 
            sahPlanesDivisionsNumericUpDown.Location = new System.Drawing.Point(3, 366);
            sahPlanesDivisionsNumericUpDown.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            sahPlanesDivisionsNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            sahPlanesDivisionsNumericUpDown.Name = "sahPlanesDivisionsNumericUpDown";
            sahPlanesDivisionsNumericUpDown.Size = new System.Drawing.Size(120, 23);
            sahPlanesDivisionsNumericUpDown.TabIndex = 12;
            sahPlanesDivisionsNumericUpDown.Value = new decimal(new int[] { 32, 0, 0, 0 });
            // 
            // minObjectsInBoundingBoxNumericUpDown
            // 
            minObjectsInBoundingBoxNumericUpDown.Location = new System.Drawing.Point(3, 322);
            minObjectsInBoundingBoxNumericUpDown.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            minObjectsInBoundingBoxNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            minObjectsInBoundingBoxNumericUpDown.Name = "minObjectsInBoundingBoxNumericUpDown";
            minObjectsInBoundingBoxNumericUpDown.Size = new System.Drawing.Size(120, 23);
            minObjectsInBoundingBoxNumericUpDown.TabIndex = 11;
            minObjectsInBoundingBoxNumericUpDown.Value = new decimal(new int[] { 6, 0, 0, 0 });
            // 
            // kdTreeMaxDepthNumericUpDown
            // 
            kdTreeMaxDepthNumericUpDown.Location = new System.Drawing.Point(3, 278);
            kdTreeMaxDepthNumericUpDown.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            kdTreeMaxDepthNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            kdTreeMaxDepthNumericUpDown.Name = "kdTreeMaxDepthNumericUpDown";
            kdTreeMaxDepthNumericUpDown.Size = new System.Drawing.Size(120, 23);
            kdTreeMaxDepthNumericUpDown.TabIndex = 10;
            kdTreeMaxDepthNumericUpDown.Value = new decimal(new int[] { 32, 0, 0, 0 });
            // 
            // renderingLabel
            // 
            renderingLabel.AutoSize = true;
            renderingLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            renderingLabel.Location = new System.Drawing.Point(3, 8);
            renderingLabel.Name = "renderingLabel";
            renderingLabel.Size = new System.Drawing.Size(99, 25);
            renderingLabel.TabIndex = 9;
            renderingLabel.Text = "Rendering";
            // 
            // kdTreeLabel
            // 
            kdTreeLabel.AutoSize = true;
            kdTreeLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            kdTreeLabel.Location = new System.Drawing.Point(3, 210);
            kdTreeLabel.Name = "kdTreeLabel";
            kdTreeLabel.Size = new System.Drawing.Size(81, 25);
            kdTreeLabel.TabIndex = 8;
            kdTreeLabel.Text = "KD-Tree";
            // 
            // TimeElapsedLabel
            // 
            TimeElapsedLabel.AutoSize = true;
            TimeElapsedLabel.Location = new System.Drawing.Point(3, 180);
            TimeElapsedLabel.Name = "TimeElapsedLabel";
            TimeElapsedLabel.Size = new System.Drawing.Size(88, 15);
            TimeElapsedLabel.TabIndex = 3;
            TimeElapsedLabel.Text = "Time elapsed: 0";
            // 
            // imageResolutionLabel
            // 
            imageResolutionLabel.AutoSize = true;
            imageResolutionLabel.Location = new System.Drawing.Point(3, 121);
            imageResolutionLabel.Name = "imageResolutionLabel";
            imageResolutionLabel.Size = new System.Drawing.Size(96, 15);
            imageResolutionLabel.TabIndex = 7;
            imageResolutionLabel.Text = "Image resolution";
            // 
            // TimeLeftLabel
            // 
            TimeLeftLabel.AutoSize = true;
            TimeLeftLabel.Location = new System.Drawing.Point(3, 195);
            TimeLeftLabel.Name = "TimeLeftLabel";
            TimeLeftLabel.Size = new System.Drawing.Size(65, 15);
            TimeLeftLabel.TabIndex = 2;
            TimeLeftLabel.Text = "Time left: 0";
            // 
            // imageHeightNumericUpDown
            // 
            imageHeightNumericUpDown.Location = new System.Drawing.Point(94, 139);
            imageHeightNumericUpDown.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            imageHeightNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            imageHeightNumericUpDown.Name = "imageHeightNumericUpDown";
            imageHeightNumericUpDown.Size = new System.Drawing.Size(85, 23);
            imageHeightNumericUpDown.TabIndex = 6;
            imageHeightNumericUpDown.Value = new decimal(new int[] { 1080, 0, 0, 0 });
            // 
            // PixelsRenderedLabel
            // 
            PixelsRenderedLabel.AutoSize = true;
            PixelsRenderedLabel.Location = new System.Drawing.Point(3, 165);
            PixelsRenderedLabel.Name = "PixelsRenderedLabel";
            PixelsRenderedLabel.Size = new System.Drawing.Size(110, 15);
            PixelsRenderedLabel.TabIndex = 1;
            PixelsRenderedLabel.Text = "Pixels rendered: 0/0";
            // 
            // imageWidthNumericUpDown
            // 
            imageWidthNumericUpDown.Location = new System.Drawing.Point(3, 139);
            imageWidthNumericUpDown.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            imageWidthNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            imageWidthNumericUpDown.Name = "imageWidthNumericUpDown";
            imageWidthNumericUpDown.Size = new System.Drawing.Size(85, 23);
            imageWidthNumericUpDown.TabIndex = 5;
            imageWidthNumericUpDown.Value = new decimal(new int[] { 1920, 0, 0, 0 });
            // 
            // rayBouncesCountLabel
            // 
            rayBouncesCountLabel.AutoSize = true;
            rayBouncesCountLabel.Location = new System.Drawing.Point(3, 77);
            rayBouncesCountLabel.Name = "rayBouncesCountLabel";
            rayBouncesCountLabel.Size = new System.Drawing.Size(74, 15);
            rayBouncesCountLabel.TabIndex = 4;
            rayBouncesCountLabel.Text = "Ray bounces";
            // 
            // samplesCountLabel
            // 
            samplesCountLabel.AutoSize = true;
            samplesCountLabel.Location = new System.Drawing.Point(3, 33);
            samplesCountLabel.Name = "samplesCountLabel";
            samplesCountLabel.Size = new System.Drawing.Size(85, 15);
            samplesCountLabel.TabIndex = 3;
            samplesCountLabel.Text = "Samples count";
            // 
            // rayBouncesNumericUpDown
            // 
            rayBouncesNumericUpDown.Location = new System.Drawing.Point(3, 95);
            rayBouncesNumericUpDown.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            rayBouncesNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            rayBouncesNumericUpDown.Name = "rayBouncesNumericUpDown";
            rayBouncesNumericUpDown.Size = new System.Drawing.Size(120, 23);
            rayBouncesNumericUpDown.TabIndex = 2;
            rayBouncesNumericUpDown.Value = new decimal(new int[] { 8, 0, 0, 0 });
            // 
            // samplesCountNumericUpDown
            // 
            samplesCountNumericUpDown.Location = new System.Drawing.Point(3, 51);
            samplesCountNumericUpDown.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            samplesCountNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            samplesCountNumericUpDown.Name = "samplesCountNumericUpDown";
            samplesCountNumericUpDown.Size = new System.Drawing.Size(120, 23);
            samplesCountNumericUpDown.TabIndex = 1;
            samplesCountNumericUpDown.Value = new decimal(new int[] { 8, 0, 0, 0 });
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
            // RenderForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(933, 519);
            Controls.Add(splitContainer1);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "RenderForm";
            Text = "Render";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)sahPlanesDivisionsNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)minObjectsInBoundingBoxNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)kdTreeMaxDepthNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)imageHeightNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)imageWidthNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)rayBouncesNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)samplesCountNumericUpDown).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button StartRenderButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label PixelsRenderedLabel;
        private System.Windows.Forms.Label TimeLeftLabel;
        private System.Windows.Forms.Label TimeElapsedLabel;
        private System.Windows.Forms.Label rayBouncesCountLabel;
        private System.Windows.Forms.Label samplesCountLabel;
        private System.Windows.Forms.NumericUpDown rayBouncesNumericUpDown;
        private System.Windows.Forms.NumericUpDown samplesCountNumericUpDown;
        private System.Windows.Forms.Label imageResolutionLabel;
        private System.Windows.Forms.NumericUpDown imageHeightNumericUpDown;
        private System.Windows.Forms.NumericUpDown imageWidthNumericUpDown;
        private System.Windows.Forms.Label kdTreeLabel;
        private System.Windows.Forms.Label renderingLabel;
        private System.Windows.Forms.Label maxDepthLabel;
        private System.Windows.Forms.NumericUpDown sahPlanesDivisionsNumericUpDown;
        private System.Windows.Forms.NumericUpDown minObjectsInBoundingBoxNumericUpDown;
        private System.Windows.Forms.NumericUpDown kdTreeMaxDepthNumericUpDown;
        private System.Windows.Forms.Label sahPlanesDivisionsLabel;
        private System.Windows.Forms.Label MinObjectsInBoundingBox;
        private System.Windows.Forms.CheckBox useKDTreeCheckBox;
        private System.Windows.Forms.Button saveImageButton;
    }
}

