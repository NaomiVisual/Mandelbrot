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
        PictureBox figuur;
        ComboBox kleurenlijst, voorbeeldenlijst;


        // In de methode Scherm wordt de userinterface gemaakt.
        public Scherm()
        {
            //Scherm
            this.Text = "Mandelbrot";
            this.ClientSize = new Size(440, 500);
            this.BackColor = Color.FromArgb(235, 250, 255);

            //TextBox-Label setup
            this.boxMidX = new TextLabel(this.Controls,
                new Point(80, 17), new Size(60, 15), midX.ToString(),
                new Point(20, 20), new Size(50, 15), "MiddenX");
            this.boxMidY = new TextLabel(this.Controls,
                new Point(80, 42), new Size(60, 15), midY.ToString(),
                new Point(20, 45), new Size(50, 15), "MiddenY");
            this.boxSchaal = new TextLabel(this.Controls,
                new Point(210, 17), new Size(90, 15), schaal.ToString(),
                new Point(160, 20), new Size(40, 15), "Schaal");
            this.boxMax = new TextLabel(this.Controls,
                new Point(210, 42), new Size(40, 15), max.ToString(),
                new Point(160, 45), new Size(40, 15), "Max");

            //OK-knop setup
            this.knop = new Button();
            this.knop.Location = new Point(260, 42);
            this.knop.Size = new Size(40, 20);
            this.Controls.Add(knop);
            this.knop.Click += this.Klik;
            this.knop.BackColor = Color.LightBlue;
            this.knop.Text = "Ok";

            //PictureBox setup
            this.figuur = new PictureBox();
            this.figuur.Location = new Point(20, 85);
            this.figuur.Size = new Size(400, 400);
            this.Controls.Add(figuur);
            this.figuur.MouseClick += this.Muis;

            //Kleurenlijst setup
            this.kleurenlijst = new ComboBox();
            this.kleurenlijst.Location = new Point(330, 17);
            this.kleurenlijst.Size = new Size(90, 20);
            this.Controls.Add(kleurenlijst);
            this.kleurenlijst.Items.Add("Standaard");
            this.kleurenlijst.Items.Add("Grijstinten");
            this.kleurenlijst.Items.Add("Paarstinten");
            this.kleurenlijst.Items.Add("Vuur");
            this.kleurenlijst.Items.Add("Nationalisme");
            this.kleurenlijst.Text = "Kleuren";

            //Voorbeelden setup
            this.voorbeeldenlijst = new ComboBox();
            this.voorbeeldenlijst.Location = new Point(330, 42);
            this.voorbeeldenlijst.Size = new Size(90, 20);
            this.Controls.Add(voorbeeldenlijst);
            this.voorbeeldenlijst.Items.Add("Zebra");
            this.voorbeeldenlijst.Items.Add("Sneeuwvlok");
            this.voorbeeldenlijst.Items.Add("Kruis+");
            this.voorbeeldenlijst.Items.Add("Vlammen");
            this.voorbeeldenlijst.Items.Add("Vlaggen");
            this.voorbeeldenlijst.Items.Add("Neuronen");
            this.voorbeeldenlijst.Text = "Voorbeelden";
            this.voorbeeldenlijst.SelectedIndexChanged += Voorbeeldenschema;

            DrawMandelbrot();
        }


        /* De onderstaande methode transleert de coordinaten van de PictureBox naar coordinaten in
           een assenstelsel waarbij de x- en y-waarden van -2 tot 2 lopen. Vervolgens roept hij
           de methode Kleurenschema aan die kleurwaarden toekent aan de mandelgetallen. */
        public void DrawMandelbrot()
        {
            Bitmap image = new Bitmap(400, 400);

            /* De variablen i en j staan voor de coordinaten van de PictureBox.
               Onze PictureBox is 400 bij 400, dus i en j lopen van 0 tot 400. */

            for (int i = 0; i < image.Width; i++)
                for (int j = 0; j < image.Height; j++)
                {
                    double x = i * this.schaal - image.Width * this.schaal / 2 + this.midX;
                    double y = j * this.schaal - image.Height * this.schaal / 2 - this.midY;

                    int t = Mandelgetal(x, y);

                    image.SetPixel(i, j, this.Kleurenschema(t));
                }
            this.figuur.Image = image; 
        }


        /* Met de methode Muis wordt het assenstelsel getransleerd. Het nieuwe middelpunt is het punt
           waar je met de muis in het panel klikt; de schaal wordt twee keer zo klein. Daarna worden
           de nieuwe waarden ingevuld in de TextBoxen en wordt het Mandelbrotfiguur opnieuw getekend. */
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

            DrawMandelbrot();

            this.Invalidate();
        }


        /* Wanneer op de OK-knop gedrukt wordt, wordt deze methode aangeroepen. Hierdoor wordt het
           Mandelbrotfiguur opnieuw getekend met de ingevoerde waarden. */
        public void Klik(object o, EventArgs e)
        {
            this.midX = double.Parse(this.boxMidX.Text);
            this.midY = double.Parse(this.boxMidY.Text);
            this.schaal = double.Parse(this.boxSchaal.Text);
            this.max = int.Parse(this.boxMax.Text);

            DrawMandelbrot();
        }


        /* In de volgende methode wordt het mandelgetal berekend van de coordinaten die hij meekrijgt
           wanneer hij wordt aangeroepen. Vervolgens geeft hij deze berekende waarde terug. */
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


        // Door deze methode is het mogelijk om uit vijf verschillende kleurschema's te kiezen.
        public Color Kleurenschema(int mandelgetal)
        {
            this.kleurnummer = this.kleurenlijst.SelectedIndex;

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


        // Deze methode geeft de optie om voorbeelden te laten zien.
        public void Voorbeeldenschema(object o, EventArgs ea)
        {
            int voorbeeldnummer = this.voorbeeldenlijst.SelectedIndex;

            switch (voorbeeldnummer)
            {
                default:
                case 0:
                    this.boxMidX.Text = "-0.15";
                    this.boxMidY.Text = "0.9";
                    this.boxSchaal.Text = "0.00001";
                    this.boxMax.Text = "100";
                    this.kleurenlijst.SelectedIndex = 0;
                    break;
                case 1:
                    this.boxMidX.Text = "-0.108440699577332";
                    this.boxMidY.Text = "0.901937417984009";
                    this.boxSchaal.Text = "1.19209289550782E-09";
                    this.boxMax.Text = "250";
                    this.kleurenlijst.SelectedIndex = 1;
                    break;
                case 2:
                    this.boxMidX.Text = "0.362169662421875";
                    this.boxMidY.Text = "0.560195757734375";
                    this.boxSchaal.Text = "3E-08";
                    this.boxMax.Text = "100";
                    this.kleurenlijst.SelectedIndex = 2;
                    break;
                case 3:
                    this.boxMidX.Text = "-1.5";
                    this.boxMidY.Text = "0";
                    this.boxSchaal.Text = "0.0009";
                    this.boxMax.Text = "200";
                    this.kleurenlijst.SelectedIndex = 3;
                    break;
                case 4:
                    this.boxMidX.Text = "0.250671005249023";
                    this.boxMidY.Text = "0";
                    this.boxSchaal.Text = "1.9073486328125E-07";
                    this.boxMax.Text = "500";
                    this.kleurenlijst.SelectedIndex = 4;
                    break;
                case 5:
                    this.boxMidX.Text = "-0.17611328125";
                    this.boxMidY.Text = "1.0855";
                    this.boxSchaal.Text = "4.8828125E-05";
                    this.boxMax.Text = "500";
                    this.kleurenlijst.SelectedIndex = 2;
                    break;
            }
            this.knop.PerformClick();
            this.DrawMandelbrot();
        }
    }
}
