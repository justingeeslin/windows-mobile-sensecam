using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsCE.Forms;

namespace MobileCapture
{
    public class MsgWindow : MessageWindow
    {
        // Assign integers to messages.
        // Note that custom Window messages start at WM_USER = 0x400.
        public const int WM_USER = 0x0400;

        // Create an instance of the form.
        private MainForm msgform;

        // Save a reference to the form so it can
        // be notified when messages are received.
        public MsgWindow(MainForm msgform)
        {
            this.msgform = msgform;
        }

        // Override the default WndProc behavior to examine messages.
        protected override void WndProc(ref Message msg)
        {
            switch (msg.Msg)
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
