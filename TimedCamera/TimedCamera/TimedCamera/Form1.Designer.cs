namespace TimedCamera
{
    partial class TimedCaptureForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.StartStop = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.LastImage = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.RunningLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CampturedSent = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer();
            this.label1 = new System.Windows.Forms.Label();
            this.TimerInterval = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.URLText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.StartStop);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // StartStop
            // 
            this.StartStop.Text = "Start";
            this.StartStop.Click += new System.EventHandler(this.StartStop_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.MenuItems.Add(this.menuItem1);
            this.menuItem2.MenuItems.Add(this.menuItem3);
            this.menuItem2.MenuItems.Add(this.menuItem4);
            this.menuItem2.Text = "Options";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "Close";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "Set Web URL";
            // 
            // menuItem4
            // 
            this.menuItem4.Text = "Set Timer";
            // 
            // LastImage
            // 
            this.LastImage.Location = new System.Drawing.Point(99, 108);
            this.LastImage.Name = "LastImage";
            this.LastImage.Size = new System.Drawing.Size(74, 69);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(4, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 22);
            this.label2.Text = "Status:";
            // 
            // RunningLabel
            // 
            this.RunningLabel.Font = new System.Drawing.Font("Segoe Condensed", 10F, System.Drawing.FontStyle.Regular);
            this.RunningLabel.Location = new System.Drawing.Point(85, 9);
            this.RunningLabel.Name = "RunningLabel";
            this.RunningLabel.Size = new System.Drawing.Size(74, 18);
            this.RunningLabel.Text = "Stopped";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 18);
            this.label3.Text = "Captured/Sent:";
            // 
            // CampturedSent
            // 
            this.CampturedSent.Font = new System.Drawing.Font("Segoe Condensed", 10F, System.Drawing.FontStyle.Regular);
            this.CampturedSent.Location = new System.Drawing.Point(85, 28);
            this.CampturedSent.Name = "CampturedSent";
            this.CampturedSent.Size = new System.Drawing.Size(74, 18);
            this.CampturedSent.Text = "0/0";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(4, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 18);
            this.label4.Text = "Last Image";
            // 
            // timer1
            // 
            this.timer1.Interval = 30000;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 19);
            this.label1.Text = "Timer (sec):";
            // 
            // TimerInterval
            // 
            this.TimerInterval.Location = new System.Drawing.Point(85, 46);
            this.TimerInterval.MaxLength = 20;
            this.TimerInterval.Name = "TimerInterval";
            this.TimerInterval.Size = new System.Drawing.Size(52, 22);
            this.TimerInterval.TabIndex = 9;
            this.TimerInterval.Text = "30";
            this.TimerInterval.TextChanged += new System.EventHandler(this.TimerInterval_TextChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(4, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 17);
            this.label5.Text = "URL";
            // 
            // URLText
            // 
            this.URLText.Location = new System.Drawing.Point(4, 83);
            this.URLText.Name = "URLText";
            this.URLText.Size = new System.Drawing.Size(169, 22);
            this.URLText.TabIndex = 17;
            this.URLText.Text = "http://";
            this.URLText.TextChanged += new System.EventHandler(this.URLText_TextChanged);
            // 
            // TimedCaptureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(176, 180);
            this.Controls.Add(this.URLText);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TimerInterval);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CampturedSent);
            this.Controls.Add(this.RunningLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LastImage);
            this.Menu = this.mainMenu1;
            this.Name = "TimedCaptureForm";
            this.Text = "Timed Capture";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem StartStop;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.PictureBox LastImage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label RunningLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label CampturedSent;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TimerInterval;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox URLText;
    }
}

