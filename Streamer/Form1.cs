using Emgu.CV;
using System;
using System.Windows.Forms;
using Capture = Emgu.CV.Capture;

namespace Streamer

{
    public partial class Form1 : Form
    {
        private Capture _capture = null;
        private bool _captureInProgress;
        public Form1()
        {
            InitializeComponent();
            CvInvoke.UseOpenCL = false;
            try
            {
                //_capture = new Capture("rtsp://帳號:密碼@10.248.63.220/");//
                _capture = new Capture("rtsp://192.168.13.205:5556/unicast");
                _capture.ImageGrabbed += ProcessFrame;
            }
            catch (Exception excpt)
            {
                MessageBox.Show(excpt.Message);
            }
        }
        private void ProcessFrame(object sender, EventArgs arg)
        {
            try
            {
                Mat frame = new Mat();
                _capture.Retrieve(frame, 0);
                captureImageBox.Image = frame.Bitmap;
            }
            catch (Exception excpt)
            {
                MessageBox.Show(excpt.Message);
            }
        }
        private void captureButton_Click(object sender, EventArgs e)
        {
            if (_capture != null)
            {
                if (_captureInProgress)
                {  //stop the capture
                    captureButton.Text = "Start Capture";
                    _capture.Pause();
                }
                else
                {
                    //start the capture
                    captureButton.Text = "Stop";
                    _capture.Start();
                }

                _captureInProgress = !_captureInProgress;
            }
        }
    }
}
