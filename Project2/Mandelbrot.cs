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
        private TextBox middenx , middeny , schaal , max;

        public Scherm()
        {
            Label tekst1,tekst2,tekst3,tekst4;
            Button knop;

            tekst1 = new Label();
            tekst2 = new Label();
            tekst3 = new Label();
            tekst4 = new Label();
            this.middenx = new TextBox();
            this.middeny = new TextBox();
            this.schaal = new TextBox();
            this.max = new TextBox();
            knop = new Button();

            tekst1.Location = new Point(20, 20);
            tekst2.Location = new Point(20, 45);
            tekst3.Location = new Point(260, 20);
            tekst4.Location = new Point(260, 45);
            this.middenx.Location = new Point(80, 17);
            this.middeny.Location = new Point(80, 42);
            this.schaal.Location = new Point(310, 17);
            this.max.Location = new Point(310, 42);
            knop.Location = new Point(360, 42);
            tekst1.Size = new Size(60, 15);
            tekst2.Size = new Size(60, 15);
            tekst3.Size = new Size(40, 15);
            tekst4.Size = new Size(40, 15);
            this.middenx.Size = new Size(160, 15);
            this.middeny.Size = new Size(160, 15);
            this.schaal.Size = new Size(90, 15);
            this.max.Size = new Size(40, 15);
            knop.Size = new Size(50, 20);
            tekst1.Text = "Midden X:";
            tekst2.Text = "Midden Y:";
            tekst3.Text = "Schaal:";
            tekst4.Text = "Max:";
            knop.Text = "OK";

            this.Controls.Add(tekst1);
            this.Controls.Add(tekst2);
            this.Controls.Add(tekst3);
            this.Controls.Add(tekst4);
            this.Controls.Add(middenx);
            this.Controls.Add(middeny);
            this.Controls.Add(schaal);
            this.Controls.Add(max);
            this.Controls.Add(knop);
            this.Text = "Mandelbrot";
            this.ClientSize = new Size(500, 500);
            this.BackColor = System.Drawing.Color.LightGray;
        }
    }
}

