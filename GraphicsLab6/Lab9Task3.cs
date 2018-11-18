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
    public partial class Lab9Task3 : Form
    {
        private Form1 parent;
        private Polyhedron figure;
        public Lab9Task3()
        {
            InitializeComponent();
        }

        public Lab9Task3(Form1 parent)
        {
            this.parent = parent;
            InitializeComponent();
            figure = new Polyhedron();
        }

        public double[,] matrix_multiplication(double[,] m1, double[,] m2)
        {
            double[,] res = new double[m1.GetLength(0), m2.GetLength(1)];
            for (int i = 0; i < m1.GetLength(0); ++i)
                for (int j = 0; j < m2.GetLength(1); ++j)
                    for (int k = 0; k < m2.GetLength(0); k++)
                        res[i, j] += m1[i, k] * m2[k, j];
            return res;
        }

        private void make_curve(ref List<Point3D> l, double x, double y0, double y1, double step)
        {
            for (double y = y0; y <= y1; y += step)
            {
                Point3D p = new Point3D(x, y, x*x + y*y);
                l.Add(p);
            }
        }
        private double currentFunction(double x, double y)
        {
            if (checkBoxXXYY.Checked)
                return x * x + y * y;
            else if (checkBoxSinCos.Checked)
                return Math.Sin(x) * Math.Cos(y);
            else if (checkBoxAbs.Checked)
                return Math.Abs(x) + Math.Abs(y);
            else
                return 5* (Math.Cos(x*x + y*y + 1)/(x*x + y*y + 1) + 0.1);

        }

        Dictionary<double, double> YMin = new Dictionary<double, double>();
        Dictionary<double, double> YMax = new Dictionary<double, double>();
         private Point pointOnLine(Point p1, Point p2, int x)
        {
            double y = p1.X != p2.X ? ((x - p1.X) * (p2.Y- p1.Y) * 1.0) / (p2.X - p1.X) + p1.Y : (p2.Y + p1.Y) / 2;
            return new Point(x, (int)Math.Round(y));
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<double, List<Point>> res = new Dictionary<double, List<Point>>();

            figure.vertexes.Clear();

            int x1 = int.Parse(textBoxX1.Text);
            int x2 = int.Parse(textBoxX2.Text);

            int y1 = int.Parse(textBoxY1.Text);
            int y2 = int.Parse(textBoxY2.Text);

            double step = double.Parse(textBoxStep.Text);

            //Build polyhedron
            for (double i = x1; i <= x2; i += step)
            {
                for (double j = y1; j <= y2; j += step)
                {
                    var p = new Point3D(i, j, currentFunction(i, j));
                    figure.vertexes.Add(p);
                }   
            }

            foreach (var vertex in figure.vertexes)
            {
                    var ind1 = figure.vertexes.Where(x => (x.X == vertex.X + step) && (x.Y == vertex.Y)).ToList();
                    if (ind1.Count > 0)
                        vertex.AddNeighbour(ind1.First());
                    var ind2 = figure.vertexes.Where(x => (x.X == vertex.X - step) && (x.Y == vertex.Y)).ToList();
                    if (ind2.Count > 0)
                        vertex.AddNeighbour(ind2.First());
                    //var ind3 = figure.vertexes.Where(y => (y.X == vertex.X) && (y.Y == vertex.Y + step)).ToList();
                    //if (ind3.Count > 0)
                    //    vertex.AddNeighbour(ind3.First());
                    //var ind4 = figure.vertexes.Where(y => (y.X == vertex.X) && (y.Y == vertex.Y - step)).ToList();
                    //if (ind4.Count > 0)
                    //    vertex.AddNeighbour(ind4.First());
            }


            var isometric = new List<List<double>> { new List<double> { Math.Sqrt(0.5), -1 / Math.Sqrt(6), 0, 0 }, new List<double> { 0, Math.Sqrt(2) / Math.Sqrt(3), 0, 0 }, new List<double> { -1 / Math.Sqrt(2), -1 / Math.Sqrt(6), 0, 0 }, new List<double> { 0, 0, 0, 1 } };

            foreach (var item in figure.vertexes)
                item.MultiplyByMatrix(isometric);
            figure.Centre = new Point3D(0, 0, 0);
            figure.CountVertex = figure.vertexes.Count();
            parent.DrawPolyhedron(figure, parent.pictureBox1Size);
            parent.getFigureFromChild(figure);
        }

        private void Lab9Task3_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}