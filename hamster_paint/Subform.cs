using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp.Extensions;
using OpenCvSharp;

namespace hamster_paint
{
    public partial class Form2 : Form
    {
        public ColorData Data { get; set;}
        public Form2()
        {
            InitializeComponent();
            pictureBox1.Image = BitmapConverter.ToBitmap(new Mat(new OpenCvSharp.Size(pictureBox1.Width, pictureBox1.Height), MatType.CV_8UC3, Scalar.FromRgb(255, 255, 255)));
            Data = new ColorData(0, 0, 0);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            TrackBar bar = (TrackBar)sender;
            switch (bar.Name)
            {
                case "R":
                    Data.R = bar.Value;
                    break;
                case "G":
                    Data.G = bar.Value;
                    break;
                case "B":
                    Data.B = bar.Value;
                    break;
                default:
                    Data.A = bar.Value;
                    break;
            }
            Console.WriteLine(Data.R + " " + Data.G + " " + Data.B);
            pictureBox1.Image = BitmapConverter.ToBitmap(new Mat(new OpenCvSharp.Size(pictureBox1.Width, pictureBox1.Height), MatType.CV_8UC3, Scalar.FromRgb(Data.R, Data.G, Data.B)));
        }
        private void buttonSelectColor_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
