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
        Camera cam;
        private bool stop;
        public Lab9Task3()
        {
            InitializeComponent();
        }

        public Lab9Task3(Form1 parent)
        {
            this.parent = parent;
            InitializeComponent();
            figure = new Polyhedron();
            cam = new Camera(new Point3D(0, 0, 1));
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

        private double currentFunction(double x, double y)
        {
            if (checkBoxXXYY.Checked)
                return Math.Cos(x * x + y * y) / (x * x + y * y + 1);
            else if (checkBoxSinCos.Checked)
                return Math.Sin(x) * Math.Cos(y);
            else if (checkBoxAbs.Checked)
                return Math.Abs(x) + Math.Abs(y);
            else
                return 5* (Math.Cos(x*x + y*y + 1)/(x*x + y*y + 1) + 0.1);

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

            Dictionary<double, double> maxY = new Dictionary<double, double>();
            Dictionary<double, double> minY = new Dictionary<double, double>();

            //Build polyhedron
            for (double i = x1; i <= x2; i += step)
            {
                maxY[i] = double.MinValue;
                minY[i] = double.MaxValue;
                for (double j = y1; j <= y2; j += step)
                {
                    var p = new Point3D(i, currentFunction(i, j), j);
                    figure.vertexes.Add(p);
                }
            }

            figure.vertexes.Sort(delegate (Point3D a, Point3D b)
            {
                int xdiff = a.Z.CompareTo(b.Z);
                if (xdiff != 0) return xdiff;
                else return a.X.CompareTo(b.X);
            });

            var groupedVertexes = figure.vertexes.GroupBy(x => x.Z);
            var newFigure = new Polyhedron();
            var counts = 1;
            var preSize = 0;
            foreach (var Zs in groupedVertexes)
            {
                foreach (var xs in Zs)
                {
                    newFigure.vertexes.Add(xs);
                }

                for (int i = counts; i < newFigure.vertexes.Count - 1; i++)
                {
                    if(newFigure.vertexes[i].Y > maxY[newFigure.vertexes[i].X]) { 
                        newFigure.vertexes[i].AddNeighbour(newFigure.vertexes[i - 1]);
                        newFigure.vertexes[i].AddNeighbour(newFigure.vertexes[i + 1]);

                        if (preSize != 0 && checkBox1.Checked)
                        {
                            newFigure.vertexes[i].AddNeighbour(newFigure.vertexes[i - preSize]);
                            newFigure.vertexes[i - preSize].AddNeighbour(newFigure.vertexes[i + 1]);
                        }

                        maxY[newFigure.vertexes[i].X] = newFigure.vertexes[i].Y;
                    }
                    else if(newFigure.vertexes[i].Y < minY[newFigure.vertexes[i].X])
                    {
                        newFigure.vertexes[i].AddNeighbour(newFigure.vertexes[i - 1]);
                        newFigure.vertexes[i].AddNeighbour(newFigure.vertexes[i + 1]);
                        if (preSize != 0 && checkBox1.Checked)
                        {
                            newFigure.vertexes[i].AddNeighbour(newFigure.vertexes[i - preSize]);
                            newFigure.vertexes[i - preSize].AddNeighbour(newFigure.vertexes[i + 1]);
                        }

                        minY[newFigure.vertexes[i].X] = newFigure.vertexes[i].Y;
                    }
                }

                preSize = Zs.Count();
                counts = newFigure.vertexes.Count();
            }

            newFigure.Centre = new Point3D(0, 0, 0);
            newFigure.CountVertex = newFigure.vertexes.Count();
            parent.DrawPolyhedron(newFigure, parent.pictureBox1Size);
            parent.getFigureFromChild(newFigure);
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

        private void button2_Click(object sender, EventArgs e)
        {
            double ud = double.Parse(textBox1.Text);
            double lr = double.Parse(textBox2.Text);

            cam.UpDownLeftRight(ud, lr);

            foreach (var x in figure.vertexes)
                x.MultiplyByMatrixTask8(cam.projection);

            parent.DrawPolyhedronLab8(figure, parent.pictureBox1Size);
            parent.getFigureFromChild(figure);
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            stop = false;
            while (!stop)
            {
                cam.UpDownLeftRight(2, 2);
                foreach (var x in figure.vertexes)
                    x.MultiplyByMatrixTask8(cam.projection);

                parent.DrawPolyhedronLab8(figure, parent.pictureBox1Size);
                parent.getFigureFromChild(figure);
                await Task.Delay(50);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            stop = true;
        }
    }


}