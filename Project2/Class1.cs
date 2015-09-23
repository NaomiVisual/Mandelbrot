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
        public Scherm()
        {
            Label tekst1; 
            Button knop1;
            Label tekst2;
            tekst1 = new Label(); ; ; ; ;
            knop1 = new Button();

            this.Text = "Mandelbrot";
            this.ClientSize = new Size(400, 500);
            this.BackColor = System.Drawing.Color.LightGray;
        }
    }
}

