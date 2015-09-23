using System.Windows.Forms;
using System.Drawing;
using System;

class Mandelbrot : Form
{

    public Mandelbrot()
    {
        this.Text = "Mandelbrot";
        this.Size = new Size(450, 400);
        this.Paint += this.tekenScherm;
        this.MouseClick += this.muis;

    }


    public void tekenScherm(object o, PaintEventArgs pea)
    {
        this.Size = new Size(400, 400);

    }

    public void muis(object o, MouseEventArgs mea)
    {

        this.middelx = mea.X;
        this.middely = mea.Y;
        this.Invalidate();

    }


    public static double Mandelgetal(double x, double y)
    {

        // mandelbrotgetal = t
        double t;
 
        double a = 0;
        double b = 0;
        double Pythagoras = 0;

        for(t=0; Pythagoras <= 2 && t<100; t += 1)
        {
            double c = a * a - b * b + x;
            double d = 2 * a * b + y;
            Pythagoras = Math.Sqrt((c * c) + (d * d));
            a = c;
            b = d; 
        }
        return t;

    }


    static void Main()
    {
        Mandelbrot scherm;
        scherm = new Mandelbrot();
        double result = Mandelgetal(0.4, 0.4);
        Application.Run(scherm);
    }
}