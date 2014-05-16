namespace Redance
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
            this.cameras = new System.Windows.Forms.ComboBox();
            this.videoBox = new System.Windows.Forms.PictureBox();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.colorR = new System.Windows.Forms.NumericUpDown();
            this.colorG = new System.Windows.Forms.NumericUpDown();
            this.colorB = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.colorRMax = new System.Windows.Forms.NumericUpDown();
            this.colorGMax = new System.Windows.Forms.NumericUpDown();
            this.colorBMax = new System.Windows.Forms.NumericUpDown();
            this.snapshot = new System.Windows.Forms.Button();
            this.trainingPic = new System.Windows.Forms.PictureBox();
            this.instructions = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.accuracyPercentage = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.calibrationBoxWidth = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.framesTxt = new System.Windows.Forms.Label();
            this.detTxt = new System.Windows.Forms.Label();
            this.framesCount = new System.Windows.Forms.Label();
            this.detectionsCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.videoBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorRMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorGMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorBMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trainingPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accuracyPercentage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.calibrationBoxWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // cameras
            // 
            this.cameras.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cameras.FormattingEnabled = true;
            this.cameras.Location = new System.Drawing.Point(591, 46);
            this.cameras.Margin = new System.Windows.Forms.Padding(4);
            this.cameras.Name = "cameras";
            this.cameras.Size = new System.Drawing.Size(160, 24);
            this.cameras.TabIndex = 0;
            // 
            // videoBox
            // 
            this.videoBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.videoBox.Location = new System.Drawing.Point(17, 124);
            this.videoBox.Margin = new System.Windows.Forms.Padding(4);
            this.videoBox.Name = "videoBox";
            this.videoBox.Size = new System.Drawing.Size(735, 417);
            this.videoBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.videoBox.TabIndex = 1;
            this.videoBox.TabStop = false;
            // 
            // buttonStop
            // 
            this.buttonStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStop.Location = new System.Drawing.Point(652, 549);
            this.buttonStop.Margin = new System.Windows.Forms.Padding(4);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(100, 28);
            this.buttonStop.TabIndex = 2;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStart.Location = new System.Drawing.Point(544, 549);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(4);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(100, 28);
            this.buttonStart.TabIndex = 3;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Red";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(94, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Green";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(167, 30);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Blue";
            // 
            // colorR
            // 
            this.colorR.Location = new System.Drawing.Point(26, 50);
            this.colorR.Margin = new System.Windows.Forms.Padding(4);
            this.colorR.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.colorR.Name = "colorR";
            this.colorR.Size = new System.Drawing.Size(64, 22);
            this.colorR.TabIndex = 10;
            // 
            // colorG
            // 
            this.colorG.Location = new System.Drawing.Point(98, 50);
            this.colorG.Margin = new System.Windows.Forms.Padding(4);
            this.colorG.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.colorG.Name = "colorG";
            this.colorG.Size = new System.Drawing.Size(64, 22);
            this.colorG.TabIndex = 11;
            // 
            // colorB
            // 
            this.colorB.Location = new System.Drawing.Point(170, 51);
            this.colorB.Margin = new System.Windows.Forms.Padding(4);
            this.colorB.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.colorB.Name = "colorB";
            this.colorB.Size = new System.Drawing.Size(64, 22);
            this.colorB.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 78);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "to";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(189, 79);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 17);
            this.label5.TabIndex = 14;
            this.label5.Text = "to";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(121, 78);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 17);
            this.label6.TabIndex = 15;
            this.label6.Text = "to";
            // 
            // colorRMax
            // 
            this.colorRMax.Location = new System.Drawing.Point(26, 95);
            this.colorRMax.Margin = new System.Windows.Forms.Padding(4);
            this.colorRMax.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.colorRMax.Name = "colorRMax";
            this.colorRMax.Size = new System.Drawing.Size(64, 22);
            this.colorRMax.TabIndex = 16;
            this.colorRMax.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // colorGMax
            // 
            this.colorGMax.Location = new System.Drawing.Point(98, 95);
            this.colorGMax.Margin = new System.Windows.Forms.Padding(4);
            this.colorGMax.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.colorGMax.Name = "colorGMax";
            this.colorGMax.Size = new System.Drawing.Size(64, 22);
            this.colorGMax.TabIndex = 17;
            this.colorGMax.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // colorBMax
            // 
            this.colorBMax.Location = new System.Drawing.Point(170, 95);
            this.colorBMax.Margin = new System.Windows.Forms.Padding(4);
            this.colorBMax.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.colorBMax.Name = "colorBMax";
            this.colorBMax.Size = new System.Drawing.Size(64, 22);
            this.colorBMax.TabIndex = 18;
            this.colorBMax.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // snapshot
            // 
            this.snapshot.Location = new System.Drawing.Point(242, 46);
            this.snapshot.Margin = new System.Windows.Forms.Padding(4);
            this.snapshot.Name = "snapshot";
            this.snapshot.Size = new System.Drawing.Size(100, 28);
            this.snapshot.TabIndex = 19;
            this.snapshot.Text = "Snapshot";
            this.snapshot.UseVisualStyleBackColor = true;
            this.snapshot.Click += new System.EventHandler(this.snapshot_Click);
            // 
            // trainingPic
            // 
            this.trainingPic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trainingPic.Location = new System.Drawing.Point(157, 165);
            this.trainingPic.Margin = new System.Windows.Forms.Padding(4);
            this.trainingPic.Name = "trainingPic";
            this.trainingPic.Size = new System.Drawing.Size(572, 385);
            this.trainingPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.trainingPic.TabIndex = 20;
            this.trainingPic.TabStop = false;
            this.trainingPic.Visible = false;
            // 
            // instructions
            // 
            this.instructions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.instructions.AutoSize = true;
            this.instructions.ForeColor = System.Drawing.Color.Red;
            this.instructions.Location = new System.Drawing.Point(528, 96);
            this.instructions.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.instructions.Name = "instructions";
            this.instructions.Size = new System.Drawing.Size(113, 17);
            this.instructions.TabIndex = 21;
            this.instructions.Text = "Show your hand!";
            this.instructions.Visible = false;
            this.instructions.Click += new System.EventHandler(this.label7_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(652, 89);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 22;
            this.button1.Text = "Calibrate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // accuracyPercentage
            // 
            this.accuracyPercentage.Location = new System.Drawing.Point(242, 95);
            this.accuracyPercentage.Margin = new System.Windows.Forms.Padding(4);
            this.accuracyPercentage.Name = "accuracyPercentage";
            this.accuracyPercentage.Size = new System.Drawing.Size(53, 22);
            this.accuracyPercentage.TabIndex = 23;
            this.accuracyPercentage.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(226, 74);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 17);
            this.label7.TabIndex = 24;
            this.label7.Text = "Accuracy %";
            this.label7.Click += new System.EventHandler(this.label7_Click_1);
            // 
            // calibrationBoxWidth
            // 
            this.calibrationBoxWidth.Location = new System.Drawing.Point(437, 96);
            this.calibrationBoxWidth.Margin = new System.Windows.Forms.Padding(4);
            this.calibrationBoxWidth.Maximum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.calibrationBoxWidth.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.calibrationBoxWidth.Name = "calibrationBoxWidth";
            this.calibrationBoxWidth.Size = new System.Drawing.Size(64, 22);
            this.calibrationBoxWidth.TabIndex = 25;
            this.calibrationBoxWidth.Value = new decimal(new int[] {
            127,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(303, 97);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label8.Size = new System.Drawing.Size(126, 17);
            this.label8.TabIndex = 26;
            this.label8.Text = ":Training box width";
            // 
            // framesTxt
            // 
            this.framesTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.framesTxt.AutoSize = true;
            this.framesTxt.Location = new System.Drawing.Point(14, 549);
            this.framesTxt.Name = "framesTxt";
            this.framesTxt.Size = new System.Drawing.Size(59, 17);
            this.framesTxt.TabIndex = 27;
            this.framesTxt.Text = "Frames:";
            this.framesTxt.Click += new System.EventHandler(this.label9_Click);
            // 
            // detTxt
            // 
            this.detTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.detTxt.AutoSize = true;
            this.detTxt.Location = new System.Drawing.Point(14, 566);
            this.detTxt.Name = "detTxt";
            this.detTxt.Size = new System.Drawing.Size(79, 17);
            this.detTxt.TabIndex = 28;
            this.detTxt.Text = "Detections:";
            // 
            // framesCount
            // 
            this.framesCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.framesCount.AutoSize = true;
            this.framesCount.Location = new System.Drawing.Point(79, 549);
            this.framesCount.Name = "framesCount";
            this.framesCount.Size = new System.Drawing.Size(12, 17);
            this.framesCount.TabIndex = 29;
            this.framesCount.Text = " ";
            // 
            // detectionsCount
            // 
            this.detectionsCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.detectionsCount.AutoSize = true;
            this.detectionsCount.Location = new System.Drawing.Point(94, 566);
            this.detectionsCount.Name = "detectionsCount";
            this.detectionsCount.Size = new System.Drawing.Size(12, 17);
            this.detectionsCount.TabIndex = 31;
            this.detectionsCount.Text = " ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 592);
            this.Controls.Add(this.detectionsCount);
            this.Controls.Add(this.framesCount);
            this.Controls.Add(this.detTxt);
            this.Controls.Add(this.framesTxt);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.calibrationBoxWidth);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.accuracyPercentage);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.instructions);
            this.Controls.Add(this.trainingPic);
            this.Controls.Add(this.snapshot);
            this.Controls.Add(this.colorBMax);
            this.Controls.Add(this.colorGMax);
            this.Controls.Add(this.colorRMax);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.colorB);
            this.Controls.Add(this.colorG);
            this.Controls.Add(this.colorR);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.videoBox);
            this.Controls.Add(this.cameras);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "VCTest";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.videoBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorRMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorGMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorBMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trainingPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accuracyPercentage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.calibrationBoxWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cameras;
        private System.Windows.Forms.PictureBox videoBox;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown colorR;
        private System.Windows.Forms.NumericUpDown colorG;
        private System.Windows.Forms.NumericUpDown colorB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown colorRMax;
        private System.Windows.Forms.NumericUpDown colorGMax;
        private System.Windows.Forms.NumericUpDown colorBMax;
        private System.Windows.Forms.Button snapshot;
        private System.Windows.Forms.PictureBox trainingPic;
        private System.Windows.Forms.Label instructions;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown accuracyPercentage;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown calibrationBoxWidth;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label framesTxt;
        private System.Windows.Forms.Label detTxt;
        private System.Windows.Forms.Label framesCount;
        private System.Windows.Forms.Label detectionsCount;
    }
}

