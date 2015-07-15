using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.WindowsCE.Forms;
using System.IO;
using System.Xml;
using Microsoft.WindowsMobile.Forms;

namespace TimedCamera
{
    public partial class TimedCaptureForm : Form
    {
        enum DSHOW_MESSAGE
        {
            MESSAGE_INFO,
            MESSAGE_ERROR,
            MESSAGE_ENDRECORDING,
            MESSAGE_FILECAPTURED
        };

        [DllImport("CameraCaptureDLL.DLL")]
        private static extern bool CaptureStill(string Path);

        [DllImport("CameraCaptureDLL.DLL")]
        private static extern bool InitializeGraph(IntPtr hWnd);

        bool running;
        public bool IsRunning
        {
            get { return running; }
            set { 
                    running = value;
                    // Thread unsafe!
                    try
                    {
                        RunningLabel.Text = running ? "Running" : "Stopped";
                        StartStop.Text = running ? "Stop":"Start";

                    }
                    catch (NullReferenceException)
                    {
                        // Drop the null ref if this is called before the form components have been assigned.
                    }
                }
        }
        int Captured;
        int Sent;
        string ImageStoreLocation;
        MsgWindow MsgWin;
        ImageUploadService.Service SVC;

        public TimedCaptureForm()
        {
            InitializeComponent();
            SVC = new TimedCamera.ImageUploadService.Service();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MsgWin = new MsgWindow(this);

            IsRunning = false;
            URLText.Text = SVC.Url;
            Captured = 0;
            Sent = 0;
            ImageStoreLocation = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\ImageFile.jpg";
            timer1.Tick += new EventHandler(timer1_Tick);
            TimerInterval.Text = (timer1.Interval / 1000).ToString();
            InitializeGraph(MsgWin.Hwnd);
        }

        private void StartStop_Click(object sender, EventArgs e)
        {
            if (IsRunning)
            {
                Stop();
            }
            else
            {
                Start();
            }
            IsRunning = !IsRunning;
        }

        private void Start()
        {
            // Start a timer to call my camera capture api
            // What is the camera capture API?
            timer1.Enabled = true;

        }

        void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                File.Delete(ImageStoreLocation);
            }
            catch (Exception ex)
            {
                // ignore it!
            }
            CaptureStill(ImageStoreLocation);
        }

        private void Stop()
        {
            timer1.Enabled = false;
        }

        private void URL_TextChanged(object sender, EventArgs e)
        {
            // Change the web reference location
            SVC.Url = URLText.Text;
        }


        internal void RespondToMessage(int p, int p_2)
        {
            // Check for the return type for capture
            switch (p_2)
            {
                case (int)DSHOW_MESSAGE.MESSAGE_FILECAPTURED:
                    // Show the value on the screen
                    this.LastImage.Image = new Bitmap(ImageStoreLocation);
                    Captured++;
                    UpdateCapturedSentDisplay();
                    // Send it over to the web server
                    StringBuilder sb = new StringBuilder();
                    XmlTextWriter writter = new XmlTextWriter(new StringWriter(sb));
                    using (FileStream fs = File.OpenRead(ImageStoreLocation))
                    {
                        byte[] bufferSize = new byte[fs.Length];
                        fs.Read(bufferSize, 0, (int)fs.Length);
                        writter.WriteBase64(bufferSize, 0, (int)fs.Length);
                        fs.Close();
                    }
                    try
                    {
                        SVC.AcceptImage(DateTime.Now, sb.ToString());
                        Sent++;
                        UpdateCapturedSentDisplay();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Failed to send Image");
                    }
                    break;

                case (int)DSHOW_MESSAGE.MESSAGE_ERROR:
//                    MessageBox.Show("Error from capture");
                    break;

                case (int)DSHOW_MESSAGE.MESSAGE_INFO:
//                    MessageBox.Show("Something not too bad happened");
                    break;
            }
        }

        private void UpdateCapturedSentDisplay()
        {
            this.CampturedSent.Text = Captured.ToString() + "/" + Sent.ToString();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TimerInterval_TextChanged(object sender, EventArgs e)
        {
            // Make sure its a number
            int Interval = 0;
            try
            {
                Interval = Convert.ToInt32(TimerInterval.Text);
                if (Interval > 0 && Interval < 9999)
                {
                    timer1.Interval = Interval * 1000;
                }
            }
            catch(Exception )
            {
                MessageBox.Show("Invalid Time");
                TimerInterval.Text = (timer1.Interval / 1000).ToString();
            }
        }

        private void URLText_TextChanged(object sender, EventArgs e)
        {
            // Need to do something with the URL
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class MsgWindow : MessageWindow
    {
        // Assign integers to messages.
        // Note that custom Window messages start at WM_USER = 0x400.
        public const int WM_USER = 0x0400;

        // Create an instance of the form.
        private TimedCaptureForm msgform;

        // Save a reference to the form so it can
        // be notified when messages are received.
        public MsgWindow(TimedCaptureForm msgform)
        {
            this.msgform = msgform;
        }

        // Override the default WndProc behavior to examine messages.
        protected override void WndProc(ref Message msg)
        {
            switch(msg.Msg)
            {
                // If message is of interest, invoke the method on the form that
                // functions as a callback to perform actions in response to the message.
                case WM_USER:
                    this.msgform.RespondToMessage((int)msg.WParam, (int)msg.LParam);
                    break;
            }
            // Call the base WndProc method
            // to process any messages not handled.
            base.WndProc(ref msg);
        }
    }
}