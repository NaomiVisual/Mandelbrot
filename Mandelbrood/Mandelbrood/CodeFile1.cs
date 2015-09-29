using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mandelbrot
{
    class Program
    {
        static void Main()
        {
            Application.Run(new Scherm());
        }
    }

    class Scherm : Form
    {
        //De standaardwaardes
        double midX = 0;
        double midY = 0;
        double schaal = 0.01;
        int max = 100;

        //De GUI
        TextLabel boxMidX, boxMidY, boxSchaal, boxMax;
        Button knop;
        PictureBox panel;
        
        public Scherm()
        {
            //Scherm
            this.Text = "Mandelbrot";
            this.ClientSize = new Size(500, 500);
            this.BackColor = Color.FromArgb(235, 250, 255);

            //Text-Label setup
            this.boxMidX = new TextLabel(this.Controls,
                new Point(80, 17), new Size(150, 15), midX.ToString(),
                new Point(20, 20), new Size(60, 15), "Midden X");
            this.boxMidY = new TextLabel(this.Controls,
                new Point(80, 42), new Size(150, 15), midY.ToString(),
                new Point(20, 45), new Size(60, 15), "Midden Y");
            this.boxSchaal = new TextLabel(this.Controls,
                new Point(300, 17), new Size(90, 15), schaal.ToString(),
                new Point(250, 20), new Size(40, 15), "Schaal");
            this.boxMax = new TextLabel(this.Controls,
                new Point(300, 42), new Size(40, 15), max.ToString(),
                new Point(250, 45), new Size(40, 15), "Max");
            
            //OK-knop setup
            this.knop = new Button();
            this.knop.Location = new Point(350, 42);
            this.knop.Size = new Size(50, 20);
            this.Controls.Add(knop);
            this.knop.Click += this.Klik;
            this.knop.BackColor = Color.LightBlue;
            this.knop.Text = "Ok";

            //Panel setup
            this.panel = new PictureBox();
            this.panel.Location = new Point(20, 85);
            this.panel.Size = new Size(400, 400);
            this.Controls.Add(panel);
            this.panel.Click += this.Muis;


            DrawMandelbrot();
        }

        public void DrawMandelbrot()
        {
            Bitmap image = new Bitmap(400, 400);

            for (int i = 0; i < image.Width; i++)
                for (int j = 0; j < image.Height; j++)
                {
                    Color newcolor;

                    double x = i * this.schaal - image.Width * this.schaal / 2 + this.midX;
                    double y = j * this.schaal - image.Height * this.schaal / 2 - this.midY;

                    double t = Mandelgetal(x, y);
                    if (t % 2 == 0)
                        newcolor = Color.Black;
                    else
                        newcolor = Color.White;

                    image.SetPixel(i, j, newcolor);
                }

            this.panel.Image = image;
        }

        public void Muis(object o, EventArgs ea)
        {
            double k = double.Parse(this.boxSchaal.Text);
            k /= 2;
            this.schaal = k;
            this.boxSchaal.Text = k.ToString();

            double x = double.Parse(this.boxSchaal.Text);
            x *= Cursor.Position.X;
            this.boxMidX.Text = x.ToString();

            double y = double.Parse(this.boxSchaal.Text);
            y *= Cursor.Position.Y;
            this.boxMidY.Text = x.ToString();
            

            this.Invalidate();
            //DrawMandelbrot();
        }
        
        public void Klik(object o, EventArgs e)
        {
            this.midX = double.Parse(this.boxMidX.Text);
            this.midY = double.Parse(this.boxMidY.Text);
            this.schaal = double.Parse(this.boxSchaal.Text);
            this.max = int.Parse(this.boxMax.Text);

            DrawMandelbrot();
        }
        
        public double Mandelgetal(double x, double y)
        {
            //Het mandelbrotgetal
            double t;

            double a = 0;
            double b = 0;
            double pythagoras = 0;

            //Mandelbrot loop
            for (t = 0; pythagoras <= 2 && t < this.max; t ++)
            {
                double c = a * a - b * b + x;
                double d = 2 * a * b + y;
                pythagoras = Math.Sqrt((c * c) + (d * d));

                a = c;
                b = d;
            }
            return t;
        }
    }
}
