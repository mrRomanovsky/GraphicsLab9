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

    public partial class Lab8Task3 : Form
    {
        Camera cam;
        private Form1 parent;
        private bool stop;
        public Lab8Task3()
        {
            InitializeComponent();
        }

        public Lab8Task3(Form1 parent)
        {
            this.parent = parent;
            cam = new Camera(new Point3D(0, 0, 1));
            InitializeComponent();
        }

        private void Lab8Task3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double ud = double.Parse(textBox1.Text);
            double lr = double.Parse(textBox2.Text);

            cam.UpDownLeftRight(ud, lr);

            foreach (var x in parent.figure.vertexes)
                x.MultiplyByMatrixTask8(cam.projection);

            parent.DrawPolyhedronLab8(parent.figure, parent.pictureBox1Size);
        }


        private async void button2_Click(object sender, EventArgs e)
        {
            stop = false;
            while (!stop)
            {
                cam.UpDownLeftRight(2, 2);
                foreach (var x in parent.figure.vertexes)
                    x.MultiplyByMatrixTask8(cam.projection);

                parent.DrawPolyhedronLab8(parent.figure, parent.pictureBox1Size);
                await Task.Delay(50);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            stop = true;
        }
    }

    public class Camera
    {
        public Point3D coords;
        public Point3D viewVector;
        public List<List<double>> projection;
        public double upDown;
        public double leftRight;
        public Camera(Point3D coords)
        {
            viewVector = new Point3D(coords.X, coords.Y, coords.Z);
            upDown = 0;
            leftRight = 0;
            this.coords = coords;
        }

        private List<List<double>> MultMatrix(List<List<double>> m1, List<List<double>> m2)
        {
            var res = new List<List<double>>() { };
            for (int i = 0; i < m1.Count; i++)
            {
                res.Add(new List<double>() { });
                for (int j = 0; j < m2[0].Count; j++)
                    res[i].Add(0);
            }

            for (int i = 0; i < m1.Count; ++i)
                for (int j = 0; j < m2.First().Count; ++j)
                    for (int k = 0; k < m2.Count; k++)
                        res[i][j] += m1[i][k] * m2[k][j];
            return res;
        }

        public void GetMatrix()
        {
            double ud = Math.PI / 180 * upDown;
            double lr = Math.PI / 180 * leftRight;
            var d1 = new List<List<double>> {
                new List<double> { 1, 0, 0, 0 },
                new List<double> { 0, Math.Cos(ud), Math.Sin(ud), 0 },
                new List<double> { 0, -Math.Sin(ud), Math.Cos(ud), 0 },
                new List<double> { 0, 0, 0, 1 } };
            var d2 = new List<List<double>> {
                new List<double> { Math.Cos(lr), 0, -Math.Sin(lr), 0 },
                new List<double> { 0, 1, 0, 0 },
                new List<double> { Math.Sin(lr), 0, Math.Cos(lr), 0 },
                new List<double> { 0, 0, 0, 1 } };
            var d3 = new List<List<double>>
            {
                new List<double> {1, 0, 0, 0 },
                new List<double> {0, 1, 0, 0 },
                new List<double> {0, 0, 0, 0 },
                new List<double> {0, 0, 0, 1 }
            };
            projection = MultMatrix(MultMatrix(d2, d1), d3);

            var temp = MultMatrix(projection, new List < List<double> > {
                new List<double> { coords.X },
                new List<double>  { coords.Y },
                new List<double>  { coords.Z },
                new List<double>  { 1 } });
            coords = new Point3D(temp[0][0], temp[1][0], temp[2][0]);
            viewVector = new Point3D(coords.X, coords.Y , coords.Z );
        }

        public void UpDownLeftRight(double ud, double lr)
        {
            upDown += ud;
            leftRight += lr;
            GetMatrix();
        }
    }
}
