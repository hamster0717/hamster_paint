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
using System.Threading;
using System.Net.Mail;

namespace hamster_paint
{
    public partial class Mainform : Form
    {
        private double ratio = 1;
        
        private System.Drawing.Size pic_size;
        private Stack<Mat> pre = new Stack<Mat>();
        private Stack<Mat> nex = new Stack<Mat>();
        private Mat canvas;
        private Mat tempCanvas;
        private OpenCvSharp.Point previousPoint;
        private OpenCvSharp.Point startPoint;
        private bool isDrawing = false;
        private int drawingType = 0;
        private Scalar MyScalar = Scalar.FromRgb(0, 0, 0);
        private OpenCvSharp.Point dragStartPoint;
        public Mainform()
        {
            InitializeComponent();
            pic_size = this.pictureBox1.Size;
            canvas = new Mat(new OpenCvSharp.Size(pictureBox1.Width, pictureBox1.Height), MatType.CV_8UC3, Scalar.FromRgb(255, 255, 255));
            pictureBox1.Image = BitmapConverter.ToBitmap(canvas);
            pictureBox2.Image = BitmapConverter.ToBitmap(new Mat(new OpenCvSharp.Size(pictureBox2.Width, pictureBox2.Height), MatType.CV_8UC3, Scalar.FromRgb(0, 0, 0)));

        }
        private OpenCvSharp.Point GetPos(int X, int Y)
        {
            int originalWidth = this.pictureBox1.Image.Width;
            int originalHeight = this.pictureBox1.Image.Height;

            PropertyInfo rectangleProperty = this.pictureBox1.GetType().GetProperty("ImageRectangle", BindingFlags.Instance | BindingFlags.NonPublic);
            Rectangle rectangle = (Rectangle)rectangleProperty.GetValue(this.pictureBox1, null);

            int currentWidth = rectangle.Width;
            int currentHeight = rectangle.Height;

            double rate = (double)currentHeight / (double)originalHeight;

            int black_left_width = (currentWidth == this.pictureBox1.Width) ? 0 : (this.pictureBox1.Width - currentWidth) / 2;
            int black_top_height = (currentHeight == this.pictureBox1.Height) ? 0 : (this.pictureBox1.Height - currentHeight) / 2;

            int zoom_x = X - black_left_width;
            int zoom_y = Y - black_top_height;
            return new OpenCvSharp.Point((double)zoom_x / rate, (double)zoom_y / rate);
        }



        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                pre.Push(canvas.Clone());
                nex.Clear();
                isDrawing = true;
                startPoint = GetPos(e.X, e.Y);
                previousPoint = startPoint;
            }

            else if (e.Button == MouseButtons.Right)
            {
                Console.WriteLine("owo");
                isDrawing = true;
                dragStartPoint.X = Cursor.Position.X;
                dragStartPoint.Y = Cursor.Position.Y;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                OpenCvSharp.Point currentPoint = GetPos(e.X, e.Y);
                if (e.Button == MouseButtons.Left )
                {
                    bool holdshift = Control.ModifierKeys == Keys.Shift;
                    if (drawingType == 0)
                    {
                        Cv2.Line(canvas, previousPoint, currentPoint, MyScalar, 2);
                        previousPoint = currentPoint;
                        pictureBox1.Image.Dispose();
                        pictureBox1.Image = BitmapConverter.ToBitmap(canvas);
                        return;
                    }
                    if (tempCanvas != null)
                        tempCanvas.Dispose();
                    tempCanvas = canvas.Clone();
                    switch (drawingType)
                    {

                        case 1:
                            DrawEllipse(tempCanvas, startPoint, currentPoint, holdshift);
                            break;
                        case 2:
                            DrawRectangle(tempCanvas, startPoint, currentPoint);
                            break;
                        case 3:
                            Cv2.Line(tempCanvas, startPoint, currentPoint, MyScalar, 2);
                            break;
                        default:
                            return;
                    }

                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = BitmapConverter.ToBitmap(tempCanvas);

                }

                else if (e.Button == MouseButtons.Right)
                {
                    Console.WriteLine("owo");
                    pictureBox1.Location = new System.Drawing.Point
                    (
                        pictureBox1.Location.X + Cursor.Position.X - dragStartPoint.X,
                        pictureBox1.Location.Y + Cursor.Position.Y - dragStartPoint.Y
                    );
                    dragStartPoint.X = Cursor.Position.X;
                    dragStartPoint.Y = Cursor.Position.Y;

                }
            }
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                bool holdshift = Control.ModifierKeys == Keys.Shift;
                OpenCvSharp.Point currentPoint = GetPos(e.X, e.Y);
                switch (drawingType)
                {
                    case 0:
                        Cv2.Line(canvas, previousPoint, currentPoint, MyScalar, 2);
                        previousPoint = currentPoint;
                        break;
                    case 1:
                        DrawEllipse(canvas, startPoint, currentPoint, holdshift);
                        break;
                    case 2:
                        DrawRectangle(canvas, startPoint, currentPoint);
                        break;
                    case 3:
                        DrawStrline(canvas, startPoint, currentPoint, holdshift);
                        break;
                    default:
                        break;
                }
                pictureBox1.Image.Dispose();
                pictureBox1.Image = BitmapConverter.ToBitmap(canvas);
                if(drawingType != 3)
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
                    canvas = Cv2.ImRead(filePath);
                    pre.Clear();
                    nex.Clear();
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = BitmapConverter.ToBitmap(canvas);
                }
            }
        }
        private void DrawStrline(Mat img, OpenCvSharp.Point start, OpenCvSharp.Point end, bool holdshift)
        {
            if (!holdshift)
            {
                Cv2.Line(canvas, start, end, MyScalar, 2);
            }
            else
            {

            }
        }
        private void DrawEllipse(Mat img, OpenCvSharp.Point start, OpenCvSharp.Point end,bool holdshift)
        {
            int centerX = (start.X + end.X) / 2;
            int centerY = (start.Y + end.Y) / 2;
            int axisX = Math.Abs(start.X - end.X) / 2;
            int axisY = Math.Abs(start.Y - end.Y) / 2;
            if(!holdshift)
                Cv2.Ellipse(img, new OpenCvSharp.Point(centerX, centerY), new OpenCvSharp.Size(axisX, axisY), 0, 0, 360, MyScalar, 2);
            else if (Math.Abs(start.X - end.X) <= Math.Abs(start.Y - end.Y))
            {
                centerX = (start.X + end.X) / 2;
                centerY = (start.Y - end.Y) <= 0 ? (Math.Abs(start.X - end.X) / 2 + start.Y) : (start.Y - Math.Abs(start.X - end.X) / 2);
                Cv2.Ellipse(img, new OpenCvSharp.Point(centerX, centerY), new OpenCvSharp.Size(axisX, axisX), 0, 0, 360, MyScalar, 2);
            }
            else
            {
                centerY = (start.Y + end.Y) / 2;
                centerX = (start.X - end.X) <= 0 ? (Math.Abs(start.Y - end.Y) / 2 + start.X) : (start.X - Math.Abs(start.Y - end.Y) / 2);
                Cv2.Ellipse(img, new OpenCvSharp.Point(centerX, centerY), new OpenCvSharp.Size(axisY, axisY), 0, 0, 360, MyScalar, 2);
            }

        }
        private void DrawRectangle(Mat img, OpenCvSharp.Point start, OpenCvSharp.Point end)
        {
            Cv2.Rectangle(img, start, end, MyScalar, 2, LineTypes.Link8, 0);
        }
        private readonly Dictionary<String, int> TypeToInt = new Dictionary<String, int>
        {
            {"line",0},{"circle",1},{"square",2},{"str_line",3}
        };

        private void TypeChange(object sender, EventArgs e)
        {
            RadioButton val = (RadioButton)sender;
            if (val.Checked)
            {
                drawingType = TypeToInt[val.Text];
            }
        }
        private void prebtn_Click(object sender, EventArgs e)
        {
            if (pre.Count() != 0)
            {
                nex.Push(canvas.Clone());
                canvas.Dispose();
                canvas = pre.Pop();
                pictureBox1.Image.Dispose();
                pictureBox1.Image = BitmapConverter.ToBitmap(canvas);
            }


        }

        private void nexbtn_Click(object sender, EventArgs e)
        {
            if (nex.Count() != 0)
            {
                pre.Push(canvas.Clone());
                canvas.Dispose();
                canvas = nex.Pop();
                
                pictureBox1.Image.Dispose();
                pictureBox1.Image = BitmapConverter.ToBitmap(canvas);
            }


        }

        ColorDialog ColorDialog = new ColorDialog();
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
            if (ColorDialog.ShowDialog() == DialogResult.OK)
            {
                Color color = ColorDialog.Color;
                MyScalar=new Scalar(color.B,color.G,color.R);
                pictureBox2.Image = BitmapConverter.ToBitmap(new Mat(new OpenCvSharp.Size(pictureBox2.Width, pictureBox2.Height), MatType.CV_8UC3, MyScalar));
            }

        }



        private void pictureBox1_MouseWheeltest(object sender, MouseEventArgs e)
        {
            double ratioStep = 0.1;
            if (e.Delta > 0)
            {
                ratio += ratioStep;
                if (ratio > 3) // 放大上限
                    ratio = 3;
                else
                {
                    changePictureBoxSize(ratio, e);
                }
            }
            else
            {
                ratio -= ratioStep;
                if (ratio < 0.5)  // 放大下限
                    ratio = 0.5;
                else
                {
                    changePictureBoxSize(ratio, e);
                }
            }
        }

        private void changePictureBoxSize(double ratio, MouseEventArgs e)
        {
            int ow = pictureBox1.Width;
            int oh = pictureBox1.Height;
            int VX, VY;
            int x = e.X;
            int y = e.Y;
            System.Drawing.Size t = pictureBox1.Size;
            t.Width = Convert.ToInt32(pic_size.Width * ratio);
            t.Height = Convert.ToInt32(pic_size.Height * ratio);
            pictureBox1.Size = t;

            VX = (int)((double)x * (ow - pictureBox1.Width) / ow);
            VY = (int)((double)y * (oh - pictureBox1.Height) / oh);
            pictureBox1.Location = new System.Drawing.Point(pictureBox1.Location.X + VX, pictureBox1.Location.Y + VY);


            //location.Y = (this.Height - this.pictureBox1.Height) / 2;
            //location.X = (this.Width - this.pictureBox1.Width) / 2;
            //this.pictureBox1.Location = location;
        }

        public void newbtn_Click(object sender, EventArgs e)
        {
            Subform MyDialog = new Subform();
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                canvas = new Mat(new OpenCvSharp.Size(Convert.ToInt32(MyDialog.textBox1.Text), Convert.ToInt32(MyDialog.textBox2.Text)), MatType.CV_8UC3, Scalar.FromRgb(255, 255, 255));
                pictureBox1.Image = BitmapConverter.ToBitmap(canvas);
            }
            
        }
    }
}
