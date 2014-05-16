using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge;
using AForge.Vision.Motion;
using AForge.Imaging.Filters;
using AForge.Imaging;
using AForge.Math.Geometry;
using System.Drawing.Imaging;
using System.Reflection;

namespace Redance
{
    public partial class Form1 : Form
    {
        private FilterInfoCollection vcds; //Video capturing devices
        private VideoCaptureDevice video;

        //Frame buffers
        public  List<Bitmap> lastFrame = new List<Bitmap>();
        public  List<Bitmap> backupImage = new List<Bitmap>();

        public struct Line
        {
            public IntPoint start;
            public IntPoint end;
        }

        public int frameWidth = 0;
        public int frameHeight = 0;
        
        //Frame buffer index
        private int frameIndex = -1;

        //Frame count - for testing
        private int fCount = 0;

        //Filters
        public  MotionDetector motionDetector; //Motion detector
        public  TwoFramesDifferenceDetector diff;
        public SimpleBackgroundModelingDetector bgm;
        public Crop cropFilter;
        private BlobCounter blobCounter = new BlobCounter();

        //Reverses images around the y axis
        private Mirror mirror = new Mirror(false,true);

        //Teaching
        private string[] instText = { Properties.Resources.FullHandText, Properties.Resources.WaveHandText };
        private int progress = 0;
        private Learning learner = new Learning();

        //Graphics
        Graphics gfx; //Used for various drawing tasks

        //Timer
        Timer detectionRate;
        bool detectMotion = false; //Specifies whether the frame has to be processed for motions

        //Indicates whether colours are being filtered within the captured video frames
        bool coloursFiltered = false;

        //Hand position
        IntPoint lastHandPosition;

        /// <summary>
        /// Form constructor
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            //this.Load += new System.EventHandler(Form1_Load);
            buttonStart.Click += new System.EventHandler(buttonStart_Click);
            buttonStop.Click += new System.EventHandler(buttonStop_Click);
        }

        /// <summary>
        /// Event to trigger when the form loads
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event arguments</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            //diff = new TwoFramesDifferenceDetector();
            detectionRate = new Timer();
            detectionRate.Tick += new EventHandler(delegate { detectMotion = true; });
            detectionRate.Interval = 500;

           vcds = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo vcd in vcds)
            {
                cameras.Items.Add(vcd.Name);
            }
            cameras.SelectedIndex = 0;
        }

        /// <summary>
        /// Event to trigger prior to form termination
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            buttonStop.PerformClick();

            base.OnClosing(e);
        }

        /// <summary>
        /// Event to trigger upon clicking the "Start" button
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event arguments</param>
        private void buttonStart_Click(object sender, EventArgs e)
        {
            diff = new TwoFramesDifferenceDetector(true);
            bgm = new SimpleBackgroundModelingDetector(true, true);
            bgm.FramesPerBackgroundUpdate = 3;
            bgm.MillisecondsPerBackgroundUpdate = 150;
            motionDetector = new MotionDetector(bgm, new MotionAreaHighlighting());
            //motionDetector = new MotionDetector(diff, new MotionAreaHighlighting());
            video = new VideoCaptureDevice(vcds[cameras.SelectedIndex].MonikerString);
            video.NewFrame += new NewFrameEventHandler(Video_NewFrame);
            video.Start();

            trainingPic.Image = Properties.Resources.ShowPalmFull;
            trainingPic.Visible = true;
            instructions.Visible = true;
            
        }

        /// <summary>
        /// Process new video frame.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="eventArgs">Event arguments</param>
        void Video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            framesCount.Text = fCount.ToString();
            detectionsCount.Text = learner.detectedNum.ToString();
            if (frameIndex == 30)
            {
                lastFrame[0].Dispose();
                lastFrame.RemoveAt(0);
                backupImage[0].Dispose();
                backupImage.RemoveAt(0);
            }
            else
                frameIndex++;

            lastFrame.Add((Bitmap)AForge.Imaging.Image.Clone(eventArgs.Frame));
            backupImage.Add((Bitmap)AForge.Imaging.Image.Clone(eventArgs.Frame)); //Backing up the image for further snapshots

            frameWidth = lastFrame[frameIndex].Width;
            frameHeight = lastFrame[frameIndex].Height;
            
            IntRange r = new IntRange((int)colorR.Value, (int)colorRMax.Value);
            IntRange g = new IntRange((int)colorG.Value, (int)colorGMax.Value);
            IntRange b = new IntRange((int)colorB.Value, (int)colorBMax.Value);
            ImageFilters.FilterColor(r,g,b,lastFrame[frameIndex]);
            lock (lastFrame[frameIndex])
            {
                if (coloursFiltered)
                {
                    motionDetector.ProcessFrame(lastFrame[frameIndex]); //Detects motion
                    trainingPic.Image = (Bitmap)AForge.Imaging.Image.Clone(mirror.Apply(eventArgs.Frame));
                }
            }
            if (detectMotion)
            {
                fCount++;
                detectMotion = false;
                BackgroundWorker bgDetect = new BackgroundWorker();
                bgDetect.DoWork += new DoWorkEventHandler(delegate
                {
                    lastHandPosition = learner.DetectHand(getMovementImage(), 1, (int)accuracyPercentage.Value, trainingPic);
                    if (instructions.InvokeRequired)
                    {
                        instructions.Invoke(new MethodInvoker(delegate { instructions.Text = lastHandPosition.X + "x, " + lastHandPosition.Y + "y"; }));
                    };
                });
                bgDetect.RunWorkerAsync();
            }
            lock (lastFrame[frameIndex])
            {
                mirror.ApplyInPlace(lastFrame[frameIndex]);
                //Draws a rectangle around the area the snapshot is taken from

                gfx = Graphics.FromImage(lastFrame[frameIndex]);
                gfx.DrawRectangle(new Pen(Color.Aqua, 3), (frameWidth / 2) - 23, (frameHeight / 2) - 23, 46, 46);
                gfx.DrawRectangle(new Pen(Color.Red, 3), (frameWidth / 2) - ((int)calibrationBoxWidth.Value / 2) + progress, (frameHeight / 2) - 120, (int)calibrationBoxWidth.Value, 200);

                if (lastHandPosition.X > 0 && lastHandPosition.Y > 0)
                {
                    gfx.DrawRectangle(new Pen(Color.Blue, 3), lastHandPosition.X - 10, lastHandPosition.Y - 10, 20, 20);
                }

                videoBox.Image = lastFrame[frameIndex];
                //videoBox.Image = mirror.Apply(diff.MotionFrame).ToManagedImage();
                gfx.Dispose();
            }
        }

        /// <summary>
        /// Evenyt to trigger upon clicking "Stop" button
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event arguments</param>
        private void buttonStop_Click(object sender, EventArgs e)
        {
            colorR.Value = 0;
            colorRMax.Value = 255;
            colorG.Value = 0;
            colorGMax.Value = 255;
            colorB.Value = 0;
            colorBMax.Value = 255;
            coloursFiltered = false;
            progress = 0;
            detectionRate.Stop();

            if (video != null && video.IsRunning)
            {
                video.Stop();
            }
            videoBox.Image = null;

            trainingPic.Visible = false;
            instructions.Visible = false;
        }

        /// <summary>
        /// Event to trigger upon clicking "Snapshot" button
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event arguments</param>
        private void snapshot_Click(object sender, EventArgs e)
        {
            Bitmap image = AForge.Imaging.Image.Clone(backupImage[frameIndex-3]);

            Color col;
            int minR = 255, minG = 255, minB = 255;
            int maxR = 0, maxG = 0, maxB = 0;
            int centerX = image.Width / 2;
            int centerY = image.Height / 2;

            progress = 0;

            for (int i = -22; i < 23; i++)
            {
                for (int k = -22; k < 23; k++)
                {
                    col = image.GetPixel(centerX + k, centerY + i);
                    if (col.R < minR)
                    {
                        minR = col.R;
                    }
                    if (col.R > maxR)
                    {
                        maxR = col.R;
                    }
                    if (col.G < minG)
                    {
                        minG = col.G;
                    }
                    if (col.G > maxG)
                    {
                        maxG = col.G;
                    }
                    if (col.B < minB)
                    {
                        minB = col.B;
                    }
                    if (col.B > maxB)
                    {
                        maxB = col.B;
                    }
                }
            }
            colorR.Value = minR;
            colorRMax.Value = maxR;
            colorG.Value = minG;
            colorGMax.Value = maxG;
            colorB.Value = minB;
            colorBMax.Value = maxB;

            coloursFiltered = true;
            image.Dispose();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Gets current motion frame
        /// </summary>
        /// <returns>Bitmap with current motion frame data</returns>
        public Bitmap getMovementImage()
        {
            return AForge.Imaging.Image.Clone(bgm.MotionFrame.ToManagedImage());

        }

        /// <summary>
        /// Event to trigger upon clicking the "Calibrate" button
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event arguments</param>
        private void button1_Click(object sender, EventArgs e)
        {
            // what to do when progress changed (update the progress bar for example)
            ProgressChangedEventHandler prog = new ProgressChangedEventHandler(
            delegate(object o, ProgressChangedEventArgs args)
            {
                progress = args.ProgressPercentage;
                instructions.Text = string.Format("{0}% Completed", args.ProgressPercentage);
            });

            // what to do when worker completes its task (notify the user)
            RunWorkerCompletedEventHandler compl = new RunWorkerCompletedEventHandler(
            delegate(object o, RunWorkerCompletedEventArgs args)
            {
                if (!args.Cancelled && (null == args.Error))
                {
                    instructions.Text = "Ready to go!";
                    detectionRate.Start();
                    Bitmap[,] samples = (Bitmap[,])args.Result;

                    BackgroundWorker showRes = new BackgroundWorker();
                    showRes.DoWork += new DoWorkEventHandler(delegate(object obj, DoWorkEventArgs arguments)
                        {
                            try
                            {
                                Blob[] blobs;
                                List<IntPoint> leftEdge, rightEdge, topEdge, bottomEdge;

                                for (int i = 0; i < 10; i++)
                                {
                                    //Build a convex hull around blobs
                                    //maxBlobFilter = new ExtractBiggestBlob();
                                    Bitmap image = samples[0, i];
                                    lock (image)
                                    {
                                        blobCounter.ProcessImage(image);
                                        blobs = blobCounter.GetObjectsInformation();
                                        // lock image to draw on it
                                        BitmapData data = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, image.PixelFormat);

                                        GrahamConvexHull grahamScan = new GrahamConvexHull();
                                        foreach (Blob blob in blobs)
                                        {
                                            // collect edge points
                                            blobCounter.GetBlobsLeftAndRightEdges(blob, out leftEdge, out rightEdge);
                                            blobCounter.GetBlobsTopAndBottomEdges(blob, out topEdge, out bottomEdge);

                                            // find convex hull
                                            List<IntPoint> edgePoints = new List<IntPoint>();
                                            edgePoints.AddRange(leftEdge);
                                            edgePoints.AddRange(rightEdge);

                                            List<IntPoint> hull = grahamScan.FindHull(edgePoints);

                                            // Paint the control
                                            Drawing.Polygon(data, hull, Color.Red);

                                            Line l1, l2, l3;
                                            int vertexNum = hull.Count;
                                            for (int firstEdge = 0; firstEdge < vertexNum - 3; firstEdge++)
                                            {
                                                l1.start = hull.ElementAt(firstEdge);
                                                l1.end = hull.ElementAt(firstEdge + 1);
                                                for (int secondEdge = 1; secondEdge < vertexNum - 2; secondEdge++)
                                                {
                                                    l2.start = hull.ElementAt(secondEdge);
                                                    l2.end = hull.ElementAt(secondEdge);
                                                    for (int thirdEdge = 2; thirdEdge < vertexNum - 1; thirdEdge++)
                                                    {
                                                        l3.start = hull.ElementAt(thirdEdge);
                                                        l3.end = hull.ElementAt(thirdEdge);


                                                    }
                                                }
                                            }
                                        }

                                        image.UnlockBits(data);
                                    }
                                    //trainingPic.Image = image;

                                    //System.Threading.Thread.Sleep(1000);
                                }
                            }
                            catch (TargetInvocationException ex)
                            {
                                Console.Out.WriteLine(ex.Message);
                            }
                        });
                    showRes.WorkerReportsProgress = false;
                    showRes.RunWorkerAsync();
                    bool working = showRes.IsBusy;
                }
            });
            //trainingPic.Image = mirror.Apply(bgm.MotionFrame).ToManagedImage();
            
            learner.Learn((frameWidth / 2) - ((int)calibrationBoxWidth.Value / 2), (frameHeight / 2) - 120, (int)calibrationBoxWidth.Value, 200, 1, getMovementImage, prog, compl);
        }

        private void label7_Click_1(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Checks whether two lines are parallel
        /// </summary>
        /// <param name="a">Line A</param>
        /// <param name="b">Line B</param>
        /// <returns>bool true if parallel, false otherwise</returns>
        public bool AreOnParallelLines(Line a, Line b)
        {
            return ((Math.Abs(a.start.X - a.end.X) == Math.Abs(b.start.X - b.end.X)) &&
                (Math.Abs(a.start.Y - a.end.Y) == Math.Abs(b.start.Y - b.end.Y)));
        }

        /// <summary>
        /// Finds the intersection point of two lines
        /// </summary>
        /// <exception cref="ArgumentException">Throws ArgumentException when lines are parallel</exception>
        /// <param name="a">Line A</param>
        /// <param name="b">Line B</param>
        /// <returns>IntPoint point of intersection</returns>
        public IntPoint FindIntersection(Line a, Line b)
        {
            //Line1
            float A1 = a.end.Y - a.start.Y;
            float B1 = a.end.X - a.start.X;
            float C1 = A1 * a.start.X + B1 * a.start.Y;

            //Line2
            float A2 = b.end.Y - b.start.Y;
            float B2 = b.end.X - b.start.X;
            float C2 = A2 * b.start.X + B2 * b.start.Y;

            float delta = A1 * B2 - A2 * B1;
            if (delta == 0)
                throw new ArgumentException("Lines are parallel");

            float x = (B2 * C1 - B1 * C2) / delta;
            float y = (A1 * C2 - A2 * C1) / delta;

            return new IntPoint((int)x, (int)y);
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }


}
