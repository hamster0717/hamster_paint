using OpenCvSharp.Extensions;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection.Emit;
using System.Reflection;
using OpenCvSharp.Dnn;

namespace hamster_paint
{
    public partial class Form1 : Form
    {
        private Stack<Mat> pre=new Stack<Mat>();
        private Stack<Mat> nex = new Stack<Mat>();
        private Mat canvas;
        private Mat tempCanvas;
        private OpenCvSharp.Point previousPoint;
        private OpenCvSharp.Point startPoint;
        private bool isDrawing = false;
        private int drawingType = 0;
        private int zoomstep = 30;
        private OpenCvSharp.Point dragStartPoint;
        public Form1()
        {
            InitializeComponent();
            canvas = new Mat(new OpenCvSharp.Size(pictureBox1.Width, pictureBox1.Height), MatType.CV_8UC3, Scalar.White);
            tempCanvas = canvas.Clone();
            pictureBox1.Image = BitmapConverter.ToBitmap(canvas);

        }

        

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine(canvas.Width + " " + pictureBox1.Width);
            if (e.Button == MouseButtons.Left)
            {
                if (canvas != null)
                    pre.Push(canvas.Clone());
                isDrawing = true;
                double scaleX = (double)canvas.Width / pictureBox1.Width;
                double scaleY = (double)canvas.Height / pictureBox1.Height;
                startPoint = new OpenCvSharp.Point(e.X * scaleX, e.Y * scaleY);
                previousPoint = startPoint;
            }
            else if (e.Button == MouseButtons.Right)
            {
                isDrawing = true;
                dragStartPoint.X = Cursor.Position.X;
                dragStartPoint.Y = Cursor.Position.Y;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing && e.Button == MouseButtons.Left)
            {
                double scaleX = (double)canvas.Width / pictureBox1.Width;
                double scaleY = (double)canvas.Height / pictureBox1.Height;
                OpenCvSharp.Point currentPoint = new OpenCvSharp.Point(e.X * scaleX, e.Y * scaleY);
                tempCanvas = canvas.Clone();
                switch (drawingType)
                {
                    case 0:
                        Cv2.Line(canvas, previousPoint, currentPoint, Scalar.Black, 2);
                        previousPoint = currentPoint;
                        break;
                    case 1:
                        DrawEllipse(tempCanvas, startPoint, currentPoint);
                        break;
                    case 2:
                        DrawRectangle(tempCanvas, startPoint, currentPoint);
                        break;
                    default:
                        return;
                }
                pictureBox1.Image = BitmapConverter.ToBitmap(tempCanvas);
            }
            else if (e.Button == MouseButtons.Right&&isDrawing)
            {

                int x, y;
                int moveX, moveY;
                moveX = Cursor.Position.X - dragStartPoint.X;
                moveY = Cursor.Position.Y - dragStartPoint.Y;
                x = pictureBox1.Location.X + moveX;
                y = pictureBox1.Location.Y + moveY;
                pictureBox1.Location = new System.Drawing.Point(x, y);
                dragStartPoint.X = Cursor.Position.X;
                dragStartPoint.Y = Cursor.Position.Y;
            }
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                double scaleX = (double)canvas.Width / pictureBox1.Width;
                double scaleY = (double)canvas.Height / pictureBox1.Height;
                OpenCvSharp.Point currentPoint = new OpenCvSharp.Point(e.X * scaleX, e.Y * scaleY);
                switch (drawingType)
                {
                    case 0:
                        Cv2.Line(canvas, previousPoint, currentPoint, Scalar.Black, 2);
                        previousPoint = currentPoint;
                        break;
                    case 1:
                        DrawEllipse(canvas, startPoint, currentPoint);
                        break;
                    case 2:
                        DrawRectangle(canvas, startPoint, currentPoint);
                        break;
                    default:
                        break;
                }
                pictureBox1.Image = BitmapConverter.ToBitmap(canvas);
                isDrawing = false;

            }
            else if (e.Button == MouseButtons.Right)
            {
                isDrawing = false;
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|Png Image|*.png";
                saveFileDialog.Title = "储存圖片";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    Cv2.ImWrite(filePath, canvas);
                }
            }
        }
        private void Open_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|Png Image|*.png";
                openFileDialog.Title = "打開圖片";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    //Console.WriteLine(filePath);
                    Mat image = Cv2.ImRead(filePath);
                    canvas = image.Clone();
                    tempCanvas = canvas.Clone();
                    pictureBox1.Image = BitmapConverter.ToBitmap(canvas);
                }
            }
        }
        private void DrawEllipse(Mat img, OpenCvSharp.Point start, OpenCvSharp.Point end)
        {
            int centerX = (start.X + end.X) / 2;
            int centerY = (start.Y + end.Y) / 2;
            int axisX = Math.Abs(start.X - end.X) / 2;
            int axisY = Math.Abs(start.Y - end.Y) / 2;
            Cv2.Ellipse(img, new OpenCvSharp.Point(centerX, centerY), new OpenCvSharp.Size(axisX, axisY), 0, 0, 360, Scalar.Black, 2);
        }
        private void DrawRectangle(Mat img, OpenCvSharp.Point start, OpenCvSharp.Point end)
        {
            Cv2.Rectangle(img, start, end, Scalar.Black, 2, LineTypes.Link8, 0);
        }
        private readonly Dictionary<String, int> TypeToInt = new Dictionary<String, int>
        {
            {"line",0},{"circle",1},{"square",2}
        };

        private void TypeChange(object sender, EventArgs e)
        {
            RadioButton val= (RadioButton)sender;
            if (val.Checked)
            {
                drawingType = TypeToInt[val.Text];
            }
        }
        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            int x = e.Location.X;
            int y = e.Location.Y;
            int ow = pictureBox1.Width;
            int oh = pictureBox1.Height;
            int VX, VY;

            PropertyInfo pInfo = pictureBox1.GetType().GetProperty("ImageRectangle", BindingFlags.Instance | BindingFlags.NonPublic);
            if (pInfo == null)
            {
                return;
            }

            if (e.Delta > 0)
            {
                pictureBox1.Width += zoomstep;
                pictureBox1.Height += zoomstep;
            }
            else if (e.Delta < 0)
            {
                if (pictureBox1.Width < canvas.Width / 10)
                    return;

                pictureBox1.Width -= zoomstep;
                pictureBox1.Height -= zoomstep;
            }

            Rectangle rect = (Rectangle)pInfo.GetValue(pictureBox1, null);
            pictureBox1.Width = rect.Width;
            pictureBox1.Height = rect.Height;
            VX = (int)((double)x * (ow - pictureBox1.Width) / ow);
            VY = (int)((double)y * (oh - pictureBox1.Height) / oh);
            pictureBox1.Location = new System.Drawing.Point(pictureBox1.Location.X + VX, pictureBox1.Location.Y + VY);

        }

        private void prebtn_Click(object sender, EventArgs e)
        {
            Console.WriteLine(pre.Count());
            if (pre.Count() != 0)
            {
                nex.Push(canvas.Clone());
                tempCanvas = canvas = pre.Pop();
            }
                
            pictureBox1.Image = BitmapConverter.ToBitmap(canvas);
        }

        private void nexbtn_Click(object sender, EventArgs e)
        {
            Console.WriteLine(nex.Count());
            if (nex.Count() != 0)
            {
                pre.Push(canvas.Clone());
                tempCanvas = canvas = nex.Pop();
            }

            pictureBox1.Image = BitmapConverter.ToBitmap(canvas);
        }
    }
}
