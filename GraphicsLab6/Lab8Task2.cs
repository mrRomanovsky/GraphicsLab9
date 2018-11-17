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
    public partial class Lab8Task2 : Form
    {
        private List<Polyhedron> figures;

        public Lab8Task2(List<Polyhedron> figures)
        {
            InitializeComponent();
            this.figures = figures;
        }

        

        private void buttonClear_Click(object sender, EventArgs e)
        {

        }

        private void buttonDraw_Click(object sender, EventArgs e)
        {

        }
    }
}
