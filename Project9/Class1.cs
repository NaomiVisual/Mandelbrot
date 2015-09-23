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

    }

    public void tekenScherm(object obj, PaintEventArgs pea)
    {


    }

    public static double Mandelgetal(double x, double y)
    {

        // mandelbrotgetal = t
        double t;
        t = 0;

        double a = 0;
        double b = 0;
        double Pythagoras = 0;
        double e;
        double f;

        while (Pythagoras <= 2)
        {

            double c = a * a - b * b + x;
            double d = 2 * a * b + y;
            Pythagoras = Math.Sqrt((c * c) + (d * d));
            a = c;
            b = d;

            t = t + 1;
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