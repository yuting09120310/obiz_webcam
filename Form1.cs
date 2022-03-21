using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace obiz_webcam
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            setup();
        }

        FilterInfoCollection USB_Webcams;
        VideoCaptureDevice Cam;


        public void setup()
        {
            USB_Webcams = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (USB_Webcams.Count > 0) //如果有偵測到視訊鏡頭
            {
                button1.Enabled = true;
                Cam = new VideoCaptureDevice(USB_Webcams[0].MonikerString);

                Cam.NewFrame += Cam_NewFrame;
            }
            else
            {
                button1.Enabled = false;
                MessageBox.Show("此機器沒有鏡頭");
            }

            VideoCapabilities[] videoCapabilities;

            videoCapabilities = Cam.VideoCapabilities;
            foreach (VideoCapabilities capabilty in videoCapabilities)
            {
                if (!comboBox1.Items.Contains(capabilty.FrameSize))
                {
                    comboBox1.Items.Add(capabilty.FrameSize);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Start")
            {
                button1.Text = "Stop";
                Cam.Start();
            }
            else
            {
                button1.Text = "Start";
                Cam.Stop();
            }
        }


        //創建新畫面
        private void Cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }


        //關閉鏡頭
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Cam.Stop();
        }

        
    }
}
