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
            Add_Dpi();
        }

        FilterInfoCollection USB_Webcams;
        VideoCaptureDevice Cam;


        public void setup()
        {
            USB_Webcams = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (USB_Webcams.Count > 0) //如果有偵測到視訊鏡頭
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
                MessageBox.Show("此機器沒有鏡頭");
            }
        }

        //加入分辨率
        public void Add_Dpi()
        {
            Cam = new VideoCaptureDevice(USB_Webcams[0].MonikerString);

            VideoCapabilities[] videoCapabilities;

            videoCapabilities = Cam.VideoCapabilities;
            foreach (VideoCapabilities capabilty in videoCapabilities)
            {
                if (!comboBox1.Items.Contains(capabilty.FrameSize))
                {
                    comboBox1.Items.Add(capabilty.FrameSize);
                }
            }
            if(comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }


        //設定dpi
        public void Set_Dpi()
        {
            Cam = new VideoCaptureDevice(USB_Webcams[0].MonikerString);
            Cam.VideoResolution = Cam.VideoCapabilities[comboBox1.SelectedIndex];

            Cam.NewFrame += Cam_NewFrame;
        }


        //啟用或關閉
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Start")
            {
                button1.Text = "Stop";
                Set_Dpi();
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


        //關閉視窗
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Cam.Stop();
        }

        
    }
}
