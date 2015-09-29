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

        public string Text
        {
            get { return this.box.Text; }
            set { this.box.Text = value; }
        }
    }
}
