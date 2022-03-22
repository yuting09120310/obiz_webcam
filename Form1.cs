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
using AForge.Controls;

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
        SaveFileDialog sfd = new SaveFileDialog();

        public void setup()
        {
            USB_Webcams = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            
            if (USB_Webcams.Count > 0) //如果有偵測到視訊鏡頭
            {
                Btn_Cam.Enabled = true;
            }
            else
            {
                Btn_Cam.Enabled = false;
                MessageBox.Show("此機器沒有鏡頭");
            }
        }

        //加入分辨率到comboxbox
        public void Add_Dpi()
        {
            Cam = new VideoCaptureDevice(USB_Webcams[0].MonikerString);

            videoSourcePlayer1.VideoSource = Cam;

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
            Cam = new VideoCaptureDevice(USB_Webcams[0].MonikerString);//選用第一個鏡頭
            Cam.VideoResolution = Cam.VideoCapabilities[comboBox1.SelectedIndex]; //手動調整index

            videoSourcePlayer1.VideoSource = Cam;
        }


        //啟用或關閉
        private void Btn_Cam_Click(object sender, EventArgs e)
        {
            if (Btn_Cam.Text == "Start")
            {
                Btn_Cam.Text = "Stop";
                Set_Dpi();
                videoSourcePlayer1.Start();
            }
            else
            {
                Btn_Cam.Text = "Start";
                videoSourcePlayer1.Stop();
            }
        }


        //關閉視窗
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Cam.Stop();
        }


        //儲存功能
        private void Btn_save_Click(object sender, EventArgs e)
        {
            Bitmap img = videoSourcePlayer1.GetCurrentVideoFrame();
            pictureBox1.Image = img;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(sfd.FileName);
            }
        }
    }
}
