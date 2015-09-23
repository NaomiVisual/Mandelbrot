﻿using System;
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
        private TextBox middenx, middeny, schaal, max;

        public Scherm()
        {
            Label MiddenX, MiddenY, Schaal, Max;
            Button knop;
            Panel panel;

            MiddenX = new Label();
            MiddenY = new Label();
            Schaal = new Label();
            Max = new Label();
            this.middenx = new TextBox();
            this.middeny = new TextBox();
            this.schaal = new TextBox();
            this.max = new TextBox();
            knop = new Button();
            panel = new Panel();

            MiddenX.Location = new Point(20, 20);
            MiddenY.Location = new Point(20, 45);
            Schaal.Location = new Point(250, 20);
            Max.Location = new Point(250, 45);
            this.middenx.Location = new Point(80, 17);
            this.middeny.Location = new Point(80, 42);
            this.schaal.Location = new Point(300, 17);
            this.max.Location = new Point(300, 42);
            knop.Location = new Point(350, 42);
            panel.Location = new Point(20, 85);

            panel.BackColor = System.Drawing.Color.Black; 

            MiddenX.Size = new Size(60, 15);
            MiddenY.Size = new Size(60, 15);
            Schaal.Size = new Size(40, 15);
            Max.Size = new Size(40, 15);
            this.middenx.Size = new Size(150, 15);
            this.middeny.Size = new Size(150, 15);
            this.schaal.Size = new Size(90, 15);
            this.max.Size = new Size(40, 15);
            knop.Size = new Size(50, 20);
            panel.Size = new Size(400, 400);

            MiddenX.Text = "Midden X:";
            MiddenY.Text = "Midden Y:";
            Schaal.Text = "Schaal:";
            Max.Text = "Max:";
            knop.Text = "OK";

            this.Controls.Add(MiddenX);
            this.Controls.Add(MiddenY);
            this.Controls.Add(Schaal);
            this.Controls.Add(Max);
            this.Controls.Add(middenx);
            this.Controls.Add(middeny);
            this.Controls.Add(schaal);
            this.Controls.Add(max);
            this.Controls.Add(knop);
            this.Controls.Add(panel);

            this.Text = "Mandelbrot";
            this.ClientSize = new Size(475, 500);
            this.BackColor = System.Drawing.Color.LightGray;
        }

        public static double Mandelgetal(double x, double y)
        {

            // mandelbrotgetal = t
            double t;

            double a = 0;
            double b = 0;
            double Pythagoras = 0;

            for (t = 0; Pythagoras <= 2 && t < 100; t += 1)
            {
                double c = a * a - b * b + x;
                double d = 2 * a * b + y;
                Pythagoras = Math.Sqrt((c * c) + (d * d));
                a = c;
                b = d;
            }
            return t;

        }
    }
}