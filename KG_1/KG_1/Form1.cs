
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace KG_1
{
    public partial class Form1 : Form
    {
        private int X;
        private int Y;
        private Graphics graphics;
        private Bitmap bitmap;
        private Bitmap texture;
        private List<string> list;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            bitmap = (Bitmap)pictureBox1.Image;
            texture = new Bitmap("D:\\Учеба\\3 курс\\2 сем\\КГ\\KG_1\\KG_1\\Images\\Дерево2.jpg");
            pictureBox2.Image = texture;
            list = new List<string>();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var graphics = Graphics.FromImage(pictureBox1.Image);
                graphics.DrawLine(new Pen(Color.Red), e.X, e.Y, X, Y);
                X = e.X;
                Y = e.Y;
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            X = e.X;
            Y = e.Y;
            if (e.Button == MouseButtons.Right)
            {
                Fill(X, Y);
            }
        }

        private void Fill(int x, int y)
        {
            if (list.Contains($"{x}{y}"))
                return;

            if (x >= pictureBox1.Width - 1) return;
            if (x < 1) return;
            if (y >= pictureBox1.Height - 1) return;
            if (y < 1) return;

            var color = texture.GetPixel(x, y);
            var pen = new Pen(color);
            graphics.DrawLine(pen, x, y, x, y + 0.5f);
            list.Add($"{x}{y}");

            if (bitmap.GetPixel(x + 1, y).ToArgb() != Color.Red.ToArgb()) 
                Fill(x + 1, y);
            if (bitmap.GetPixel(x - 1, y).ToArgb() != Color.Red.ToArgb())
                Fill(x - 1, y);
            if (bitmap.GetPixel(x, y + 1).ToArgb() != Color.Red.ToArgb())
                Fill(x, y + 1);
            if (bitmap.GetPixel(x, y - 1).ToArgb() != Color.Red.ToArgb())
                Fill(x, y - 1);

            pictureBox1.Invalidate();
            pen.Dispose();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png",
                Title = "Select an Image"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                texture = new Bitmap(openFileDialog.FileName);
                pictureBox2.Image = texture;
            }
        }
    }
}
