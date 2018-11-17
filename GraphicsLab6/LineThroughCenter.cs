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
    public partial class LineThroughCenter : Form
    {
        public string AxisChosen { get; private set; }
        public double RotationAngleRadians { get; private set; }

        public LineThroughCenter()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.AxisChosen = this.axisTextBox.Text;
            this.RotationAngleRadians = double.Parse(this.rotationAngleTextBox.Text) * Math.PI / 180;
            this.Close();
        }
    }
}
