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
    public partial class Lab8Task1Vector : Form
    {
        public Lab8Task1Vector()
        {
            InitializeComponent();
        }

        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        private void okButton_Click(object sender, EventArgs e)
        {
            X = double.Parse(xTextBox.Text);
            Y = double.Parse(yTextBox.Text);
            Z = double.Parse(zTextBox.Text);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
