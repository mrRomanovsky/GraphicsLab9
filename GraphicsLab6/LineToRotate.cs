using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicsLab6
{
    public partial class LineToRotate : Form
    {
        public LineToRotate()
        {
            InitializeComponent();
        }

        public Point3D Point1 { get; private set; }

        public Point3D Point2 { get; private set; }

        public double AngleRad { get; private set; }

        private void LineToRotate_Load(object sender, EventArgs e)
        {

        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Point1 = new Point3D(double.Parse(x1TextBox.Text), double.Parse(y1TextBox.Text), double.Parse(z1TextBox.Text));
            Point2 = new Point3D(double.Parse(x2TextBox.Text), double.Parse(y2TextBox.Text), double.Parse(z2TextBox.Text));
            AngleRad = double.Parse(angleTextBox.Text) * Math.PI / 180;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
