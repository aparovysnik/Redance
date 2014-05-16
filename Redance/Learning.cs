using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Vision.Motion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Redance
{
    public class Learning
    {
        /// <summary>
        /// Floating point structure similar to IntPoint
        /// </summary>
        struct FloatPoint
        {
            public float x;
            public float y;
        }
        //Filters
        ExtractBiggestBlob maxBlobFilter = new ExtractBiggestBlob();
        Bitmap[,] samples;

        //Snapshots can potentially be of different sizes, so the average sample size is used
        FloatPoint avgSize = new FloatPoint();

        //Number of hands detected
        public int detectedNum = 0;

        float avgAspectRatio;
        /// <summary>
        /// Get movement samples from the specified image area
        /// </summary>
        /// <param name="x">Position of the upper left corner of the area on the x axis</param>
        /// <param name="y">Position of the upper left corner on of the area the y axis</param>
        /// <param name="width">Width of the area</param>
        /// <param name="height">Height of the area</param>
        /// <param name="maxCycles">Number of cycles to run</param>
        /// <param name="getImg">A delegate returning images to process</param>
        /// <param name="progEv">A delegate specifying code to run when progress of learning changes</param>
        /// <param name="complEv">A delegate specifying code to run when learning is complete</param>
        public void Learn(int x, int y, int width, int height, int maxCycles, Func<Bitmap> getImg, ProgressChangedEventHandler progEv,
            RunWorkerCompletedEventHandler complEv)
        {
            if (samples != null)
            {
                foreach(Bitmap b in samples)
                {
                    b.Dispose();
                }
            }

            ResizeBilinear resize = new ResizeBilinear(70, (int)(70 * ((float)height / width)));
            BackgroundWorker bw = new BackgroundWorker();
            samples = new Bitmap[maxCycles, 10];
            Crop cropFilter;
            Mirror mirror = new Mirror(false,true);
            HomogenityEdgeDetector edgeDetector = new HomogenityEdgeDetector();

            // this allows our worker to report progress during work
            bw.WorkerReportsProgress = true;

            // what to do in the background thread
            bw.DoWork += new DoWorkEventHandler(
            delegate(object o, DoWorkEventArgs args)
            {
                BackgroundWorker b = o as BackgroundWorker;
                //Bitmap img;
                int xSize = 0, ySize = 0;
                int numImages = 0;
                for (int learningPoints = 0; learningPoints < maxCycles; learningPoints++)
                {
                    b.ReportProgress((100 / maxCycles) * learningPoints);
                    for (int tutorialBox = 0; tutorialBox < 100; tutorialBox++)
                    {
                        if (tutorialBox % 10 == 0)
                        {
                            numImages += 1;
                            b.ReportProgress((100 / maxCycles) * learningPoints + (tutorialBox / maxCycles));
                            cropFilter = new Crop(new Rectangle(x - tutorialBox, y, width, height));
                            //img = resize.Apply(maxBlobFilter.Apply(cropFilter.Apply(getImg())));
                            xSize += width;
                            ySize += height;
                            samples[learningPoints, tutorialBox / 10] = resize.Apply(mirror.Apply(cropFilter.Apply(getImg())));
                            samples[learningPoints, tutorialBox / 10].Save("sample" + tutorialBox / 10 + ".bmp");
                        }
                        System.Threading.Thread.Sleep(15);
                    }
                }
                avgSize.x = (float)xSize / numImages;
                avgSize.y = (float)ySize / numImages;
                avgAspectRatio = (float)xSize / ySize;
                b.ReportProgress(100);
                args.Result = samples;
            });

            // what to do when progress changed (update the progress bar for example)
            bw.ProgressChanged += progEv;

            // what to do when worker completes its task (notify the user)
            bw.RunWorkerCompleted += complEv;

            bw.RunWorkerAsync();

        }

        /// <summary>
        /// Detect hand on a given frame
        /// </summary>
        /// <param name="image">Image to detect the hand palm within</param>
        /// <param name="cycles">Number of snapshot cycles to check against</param>
        /// <param name="similarityRate">The threshold percent of pixels that have to match for a positive result</param>
        /// <param name="trainingPic">A reference to secondary frame field. Temporary.</param>
        /// <returns>IntPoint point at the centre of detected hand palm</returns>
        public IntPoint DetectHand(Bitmap image, int cycles, int similarityRate, PictureBox trainingPic)
        {
            IntPoint dimensions = new IntPoint(image.Width, image.Height);
            BlobCounterBase bc = new BlobCounter();
            Crop cropFilter;

            bc.FilterBlobs = true;
            bc.MinWidth = (int)(avgSize.x * 0.6);
            bc.MinHeight = (int)(avgSize.y * 0.6);
            bc.ObjectsOrder = ObjectsOrder.Size;
            bc.ProcessImage(image);
            Blob[] blobs = bc.GetObjectsInformation();

            int blobWidth = 0;
            int blobHeight = 0;
            float blobAspectRatio;
            double angle;
            RotateBilinear rotate;
            ResizeBilinear resize = new ResizeBilinear((int)avgSize.x, (int)avgSize.y);
            Bitmap bmp;
            List<IntPoint> edges = new List<IntPoint>();

            for (int b = 0; b < blobs.Length; b++)
            {
                Rectangle br = blobs[b].Rectangle;
                cropFilter = new Crop(br);
                image = cropFilter.Apply(image);
                bmp = AForge.Imaging.Image.Clone(image);
                //bc.ExtractBlobsImage(image, blobs[b], false);
                //bmp = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);
                edges = bc.GetBlobsEdgePoints(blobs[b]);
                IntPoint circleCenter = FindLargestCircleCenter(edges, blobs[b].Rectangle);
                //bmp = ImageFilters.Draw(bmp, edges);

                //Highlight circle center
                //Graphics gfx = Graphics.FromImage(bmp);
                //gfx.DrawRectangle(new Pen(Color.Red, 3), circleCenter.X - 10, circleCenter.Y - 10, 20, 20);

                blobWidth = blobs[b].Rectangle.Width;
                blobHeight = blobs[b].Rectangle.Height;
                blobAspectRatio = (float)blobWidth / blobHeight;
                if((blobAspectRatio) > avgAspectRatio + 0.08)
                {
                    float test3 = avgSize.y / avgSize.x;
                    if ((blobAspectRatio - avgAspectRatio) <= 1)
                    {
                        angle = ((blobAspectRatio - avgAspectRatio) / (test3 - avgAspectRatio)) * 90 + (1 - (blobAspectRatio - avgAspectRatio)) * 30;
                    }
                    else
                    {
                        angle = ((blobAspectRatio - avgAspectRatio) / (test3 - avgAspectRatio)) * 90;
                    }
                    rotate = new RotateBilinear(angle, false);
                    image = rotate.Apply(image);
                    //bmp = rotate.Apply(bmp);
                }
                image = resize.Apply(image);
                //bmp = resize.Apply(bmp);

                //image = cropFilter.Apply(image);
                //image = maxBlobFilter.Apply(image);
                int scannedPixels = 0;
                int matches = 0;
                float test = 0;
                float threshold = (float)similarityRate / 100;
                int imgHeight, imgWidth;
                
                for (int i = 0; i < cycles; i++)
                {
                    for (int smpl = 0; smpl < 10; smpl++)
                    {
                        if (image.Size.Width < samples[i, smpl].Size.Width)
                            imgWidth = image.Size.Width;
                        else
                            imgWidth = samples[i, smpl].Size.Width;
                        if (image.Size.Height < samples[i, smpl].Size.Height)
                            imgHeight = image.Size.Height;
                        else
                            imgHeight = samples[i, smpl].Size.Height;
                        for (int y = 0; y < imgHeight; y += 3)
                            for (int x = 0; x < imgWidth; x += 3)
                            {
                                scannedPixels++;
                                lock (samples[i, smpl])
                                {
                                    lock (image)
                                    {
                                        if (image.GetPixel(x, y).Equals(samples[i, smpl].GetPixel(x, y)))
                                        {
                                            matches++;
                                        }
                                    }
                                }
                            }
                        test = (float)matches / (float)scannedPixels;
                        if (test >= threshold)
                        {
                            //bc.ExtractBlobsImage(image, blobs[b], false);
                            //bmp.Save("detect" + detectedNum + "o.bmp");
                            //image.Save("detect" + detectedNum + "r.bmp");
                            detectedNum++;
                            return new IntPoint(dimensions.X - circleCenter.X, circleCenter.Y);

                            //return new IntPoint(image.Width - (int)blobs[b].CenterOfGravity.X, (int)blobs[b].CenterOfGravity.Y);
                        }
                        matches = 0;
                        scannedPixels = 0;
                    }
                }
            }
            return new IntPoint();
        }

        /// <summary>
        /// Searches for the center point of the largest circle inbound into the area defined by the specified set of edges.
        /// </summary>
        /// <param name="edges">Set of edges defining the boundaries of the figure</param>
        /// <param name="area">Rectangular area to search within</param>
        /// <returns>IntPoint pointing at the center of the largest circle that can be inbound.</returns>
        private IntPoint FindLargestCircleCenter(List<IntPoint> edges, Rectangle area)
        {
            int x = 0;
            int y = 0;
            double maxDistance = 0;
            for( int w = area.Left; w < (area.Right); w += (area.Width / 10))
            {
                for (int h = area.Top; h < (area.Bottom); h += (area.Height / 10))
                {
                    IntPoint point = new IntPoint(w, h);
                    if (InsidePoints(edges, point, area))
                    {
                        double minDistance = area.Width;
                        foreach (IntPoint edge in edges)
                        {
                            double distance = DistanceBetweenPoints(edge, point);
                            if(distance < minDistance)
                            {
                                minDistance = distance;
                            }
                        }
                        if (minDistance > maxDistance)
                        {
                            maxDistance = minDistance;
                            x = w;
                            y = h;
                        }
                    }
                }
            }
            return new IntPoint(x, y);
        }

        /// <summary>
        /// Chhecks whether the specified point lies within the figure constrained by given list of edges
        /// </summary>
        /// <param name="edges">List of edges representing the figure</param>
        /// <param name="point">Point to check</param>
        /// <param name="area">Rectangle area to check within</param>
        /// <returns>Bool true if point lies within the set of edges, false otherwise</returns>
        private bool InsidePoints(List<IntPoint> edges, IntPoint point, Rectangle area)
        {
            int linesRight = 0;
            int linesLeft = 0;

            for (int i = point.X; i < area.Right; i++)
                if (edges.Contains(new IntPoint(i, point.Y))) //Fix this line - write a proper comparison!
                    linesRight++;
            for (int i = point.X; i > area.Left; i--)
                if (edges.Contains(new IntPoint(i, point.Y))) //Fix this line - write a proper comparison!
                    linesLeft++;
            return (((linesRight % 2) != 0) && ((linesLeft % 2) != 0));
        }

        /// <summary>
        /// Computes the distance between two 2D points
        /// </summary>
        /// <param name="a">Point A</param>
        /// <param name="b">Point B</param>
        /// <returns>Double representing the distance between the two points</returns>
        private double DistanceBetweenPoints(IntPoint a, IntPoint b)
        {
            int sideA = Math.Abs(a.X - b.X);
            int sideB = Math.Abs(a.Y - b.Y);

            return (Math.Sqrt(sideA * sideA + sideB * sideB));
        }
    }
}
