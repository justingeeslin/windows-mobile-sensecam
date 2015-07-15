using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml;

namespace MobileCapture
{
    public partial class MainForm : Form
    {
        enum DSHOW_MESSAGE
        {
            MESSAGE_INFO,
            MESSAGE_ERROR,
            MESSAGE_ENDRECORDING,
            MESSAGE_FILECAPTURED
        };

        [DllImport("MobileCamera.DLL")]
        private static extern bool CaptureStill(string Path);

        [DllImport("MobileCamera.DLL")]
        private static extern bool InitializeGraph(IntPtr hWnd);

        protected string ImageStoreLocation { get; set; }
        protected MsgWindow MsgWin { get; set; }
        
        bool _running;
        private string _currentFile;
        int imgCounter, imgCounterForDirectories;
        private string folder;
        
        [DllImport("coredll", SetLastError = true)]
        private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
        public byte VK_F22;  // Lock the keys on PocketPC (VK_KEYLOCK)
        public byte VK_OFF;  // Power button
        public byte VK_APP6;  // Lock the keys on Smartphone
        public byte VK_NONAME; //No Key


        public MainForm()
        {
            //Keystroke Setup
            this.VK_F22 = 0x85;     // Lock the keys on PocketPC
            this.VK_OFF = 0xDF;     // Power button
            this.VK_APP6 = 0xC6;
            this.VK_NONAME = 0xFC;
            InitializeComponent();
        }
        protected void OnClosing(object sender, EventArgs e)
        {
            MessageBox.Show("Goodbye");
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.MsgWin = new MsgWindow(this);
            imgCounter = 0;
            imgCounterForDirectories = 0;
            IsRunning = false;
            folder = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), "Photos");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            
            ImageStoreLocation =  folder + "\\{0}.jpg";
            textBox2.Text = ImageStoreLocation;
            timer1.Interval = 3000;
            timer1.Tick += new EventHandler(timer1_Tick);
            InitializeGraph(MsgWin.Hwnd);

            textBox1.Text = "3";
            this.Start();
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        public bool IsRunning
        {
            get { return _running; }
            set
            {
                _running = value;
                // Thread unsafe!
                try
                {
                    label2.Text = _running ? "Running" : "Stopped";
                    menuItemStart.Text = _running ? "Stop" : "Start";

                }
                catch (NullReferenceException)
                {
                    // Drop the null ref if this is called before the form components have been assigned.
                }
            }
        }

        private void Start()
        {
            IsRunning = true;
            timer1.Enabled = true;
        }

        private void Stop()
        {
            IsRunning = false;
            timer1.Enabled = false;
        }

        internal void RespondToMessage(int p, int p_2)
        {
            // Check for the return type for capture
            switch (p_2)
            {
                case (int)DSHOW_MESSAGE.MESSAGE_FILECAPTURED:
                        imgCounter++;
                        // Show the value on the screen
                        lbl_picLabel.Text = "Pictures Taken: " + imgCounter;
                    break;

                case (int)DSHOW_MESSAGE.MESSAGE_ERROR:
                    //                    MessageBox.Show("Error from capture");
                    break;

                case (int)DSHOW_MESSAGE.MESSAGE_INFO:
                    //                    MessageBox.Show("Something not too bad happened");
                    break;
            }

        }

        private void TimerInterval_TextChanged(object sender, EventArgs e)
        {
            // Make sure its a number
            int Interval = 0;
            try
            {
                Interval = Convert.ToInt32(textBox1.Text);
                if (Interval > 0 && Interval < 9999)
                {
                    timer1.Interval = Interval * 1000;
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("Invalid Time");
                textBox1.Text = (timer1.Interval / 1000).ToString();
            }
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            string newFileName = string.Format(ImageStoreLocation, DateTime.Now.ToString("hhmmss-yyyyMMdd"));
            _currentFile = newFileName;
            CaptureStill(newFileName);
            imgCounterForDirectories++;
            if (imgCounterForDirectories >= 500)
            {
                //Change Directory
                folder = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), "Photos" + imgCounter);
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                ImageStoreLocation = folder + "\\{0}.jpg";
                textBox2.Text = ImageStoreLocation;
                imgCounterForDirectories = 0;
            }
        }
        
        private void menuItemStart_Click(object sender, EventArgs e)
        {
            if (IsRunning)
            {
                Stop();
            }
            else
            {
                Start();
            }
        }

        private void lbl_picLabel_ParentChanged(object sender, EventArgs e)
        {

        }
        private static void SendKey(byte key)
        {
            const int KEYEVENTF_KEYUP = 0x02;
            const int KEYEVENTF_KEYDOWN = 0x00;
            keybd_event(key, 0, KEYEVENTF_KEYDOWN, 0);
            keybd_event(key, 0, KEYEVENTF_KEYUP, 0);
        }
        private void lockDevice(object sender, EventArgs e)
        {
            //Lock Device
            SendKey(this.VK_APP6);
            SendKey(this.VK_F22);
        }
    }
}