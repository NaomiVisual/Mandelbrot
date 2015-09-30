using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Mandelbrot
{
    class TextLabel
    {
        TextBox box;
        Label label;

        //Met de onderstaande klasse kunnen de verschillende textboxen met hun bijbehorende labels worden aangeroepen.
        public TextLabel(Control.ControlCollection controls,
            Point boxLocation, Size boxSize, string boxText,
            Point labelLocation, Size labelSize, string labelText)
        {
            box = new TextBox();
            label = new Label();

            this.box.Location = boxLocation;
            this.box.Size = boxSize;
            this.box.Text = boxText;

            this.label.Location = labelLocation;
            this.label.Size = labelSize;
            this.label.Text = labelText;

            controls.Add(box);
            controls.Add(label);
        }

        //Met behulp van deze methode kunnen de waarden uit de textboxen worden opgeslagen of opgevraagd.
        public string Text
        {
            get { return this.box.Text; }
            set { this.box.Text = value; }
        }
    }
}
