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
        int kleurnummer;

        //De GUI
        TextLabel boxMidX, boxMidY, boxSchaal, boxMax;
        Button knop;
        PictureBox panel;
        ComboBox kleurlijst, voorbeelden;


        // In de methode Scherm wordt de user-interface gemaakt.
        public Scherm()
        {
            //Scherm
            this.Text = "Mandelbrot";
            this.ClientSize = new Size(440, 500);
            this.BackColor = Color.FromArgb(235, 250, 255);

            //TextBox-Label setup
            this.boxMidX = new TextLabel(this.Controls,
                new Point(70, 17), new Size(60, 15), midX.ToString(),
                new Point(20, 20), new Size(50, 15), "Midden X");
            this.boxMidY = new TextLabel(this.Controls,
                new Point(70, 42), new Size(60, 15), midY.ToString(),
                new Point(20, 45), new Size(50, 15), "Midden Y");
            this.boxSchaal = new TextLabel(this.Controls,
                new Point(200, 17), new Size(90, 15), schaal.ToString(),
                new Point(150, 20), new Size(40, 15), "Schaal");
            this.boxMax = new TextLabel(this.Controls,
                new Point(200, 42), new Size(40, 15), max.ToString(),
                new Point(150, 45), new Size(40, 15), "Max");

            //OK-knop setup
            this.knop = new Button();
            this.knop.Location = new Point(250, 42);
            this.knop.Size = new Size(40, 20);
            this.Controls.Add(knop);
            this.knop.Click += this.Klik;
            this.knop.BackColor = Color.LightBlue;
            this.knop.Text = "Ok";

            //Panel setup
            this.panel = new PictureBox();
            this.panel.Location = new Point(20, 85);
            this.panel.Size = new Size(400, 400);
            this.Controls.Add(panel);
            this.panel.MouseClick += this.Muis;

            //Kleurenlijst setup
            this.kleurlijst = new ComboBox();
            this.kleurlijst.Location = new Point(320, 17);
            this.kleurlijst.Size = new Size(100, 20);
            this.Controls.Add(kleurlijst);
            this.kleurlijst.Items.Add("Standaard");
            this.kleurlijst.Items.Add("Grijstinten");
            this.kleurlijst.Items.Add("Paarstinten");
            this.kleurlijst.Items.Add("Vuur");
            this.kleurlijst.Items.Add("Nationalisme");
            this.kleurlijst.Text = "Kleuren";

            //Voorbeelden setup
            this.voorbeelden = new ComboBox();
            this.voorbeelden.Location = new Point(320, 42);
            this.voorbeelden.Size = new Size(100, 20);
            this.Controls.Add(voorbeelden);
            this.voorbeelden.Items.Add("Zebra");
            this.voorbeelden.Items.Add("ehh");
            this.voorbeelden.Items.Add("ehhh");
            this.voorbeelden.Items.Add("ehhhh");
            this.voorbeelden.Text = "Voorbeelden";

            DrawMandelbrot();
        }


        /* De onderstaande methode transleert de coordinaten van de panel naar coordinaten in
           een assenstelsel waarbij de x- en y-waarden van -2 tot 2 lopen.Vervolgens roept hij
           de methode Kleurenschema aan die kleurwaardes toekent aan de mandelgetallen. */
        public void DrawMandelbrot()
        {
            Bitmap image = new Bitmap(400, 400);

            for (int i = 0; i < image.Width; i++)
                for (int j = 0; j < image.Height; j++)
                {
                    double x = i * this.schaal - image.Width * this.schaal / 2 + this.midX;
                    double y = j * this.schaal - image.Height * this.schaal / 2 - this.midY;

                    int t = Mandelgetal(x, y);

                    image.SetPixel(i, j, this.Kleurenschema(t));
                }
            this.panel.Image = image; 
        }


        /* Met de mehode Muis wordt het assenstelsel getransleert naar een assenstelsel.
           Het nieuwe middelpunt is het punt waar je met de muis in het panel klikt;
           de schaal wordt twee keer zo klein. 
           blablablablabla*/
        public void Muis(object o, MouseEventArgs mea)
        {
            double x = double.Parse(this.boxMidX.Text);
            x = (mea.X - 400 / 2) * this.schaal + midX;
            this.boxMidX.Text = x.ToString();

            double y = double.Parse(this.boxMidY.Text);
            y = (-(mea.Y - 400 / 2) * this.schaal) + midY;
            this.boxMidY.Text = y.ToString();

            double k = double.Parse(this.boxSchaal.Text);
            k /= 2;
            this.schaal = k;
            this.boxSchaal.Text = k.ToString();


            this.midX = double.Parse(this.boxMidX.Text);
            this.midY = double.Parse(this.boxMidY.Text);
            this.schaal = double.Parse(this.boxSchaal.Text);
            this.max = int.Parse(this.boxMax.Text);

            DrawMandelbrot();


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

        public int Mandelgetal(double x, double y)
        {
            //Het mandelbrotgetal
            int t;

            double a = 0;
            double b = 0;
            double pythagoras = 0;

            //Mandelbrot loop
            for (t = 0; pythagoras <= 2 && t < this.max; t++)
            {
                double c = a * a - b * b + x;
                double d = 2 * a * b + y;
                pythagoras = Math.Sqrt((c * c) + (d * d));

                a = c;
                b = d;
            }
            return t;
        }

        public Color Kleurenschema(int mandelgetal)
        {
            this.kleurnummer = this.kleurlijst.SelectedIndex;

            switch (this.kleurnummer)
            {
                default:
                case 0:
                    if (mandelgetal % 2 == 0)
                        return Color.Black;
                    else return Color.White;
                case 1:
                    if (mandelgetal != this.max)
                        return Color.FromArgb(mandelgetal % 128 * 2, mandelgetal % 128 * 2, mandelgetal % 128 * 2);
                    else return Color.Black;
                case 2:
                    if (mandelgetal != this.max)
                        return Color.FromArgb(mandelgetal % 25 * 10, mandelgetal % 1, mandelgetal % 70 * 3);
                    else return Color.Black;
                case 3:
                    if (mandelgetal != this.max)
                        return Color.FromArgb(255, mandelgetal % 10 * 20, 0);
                    else return Color.Red;
                case 4:
                    if (mandelgetal != this.max)
                    {
                        switch (mandelgetal % 4)
                        {
                            case 1:
                                return Color.FromArgb(255, 90, 20);
                            case 2:
                                return Color.Blue;
                            case 3:
                                return Color.White;
                            default:
                                return Color.Red;
                        }
                    }
                    else
                        return Color.FromArgb(255, 90, 20);
            }

        }
    }
}
