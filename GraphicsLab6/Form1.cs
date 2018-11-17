using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using AdjacentPoints = System.Tuple<int, System.Collections.Generic.List<System.Tuple<int, int>>>;

namespace GraphicsLab6
{
    public partial class Form1 : Form
    {
        private string figType = "";
        private string mode = "";
        private Polyhedron figure;
        private Pen redPen = new Pen(Color.Red);
        private List<System.Windows.Forms.Panel> tasksPanels;
        private Edge polyhrdron2D;
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            tasksPanels = new List<Panel>(7);
            InitializeTasksPanels();
        }

        private void InitializeTasksPanels()
        {
            Initialize2TaskPanel();
            Initialize3TaskPanel();
            Initialize4TaskPanel();
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            figType = (string)((ListBox)sender).SelectedItem;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            if (figType != "" && textBox1.Text != "")//&& mode == "")
            {
                switch (figType)
                {
                    case "Тетраэдр":
                        figure = new Polyhedron(PolyhedronType.Tetrahedron, int.Parse(textBox1.Text));
                        break;
                    case "Гексаэдр":
                        figure = new Polyhedron(PolyhedronType.Hexahedron, int.Parse(textBox1.Text));
                        break;
                    case "Октаэдр":
                        figure = new Polyhedron(PolyhedronType.Octahedron, int.Parse(textBox1.Text));
                        break;
                }
                DrawPolyhedron(figure, pictureBox1.Size);
                pictureBox1.Invalidate();
            }
        }

        private void DrawPolyhedron(Polyhedron polyhedron, Size size)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            var res = new List<PointF>();
            var x = size.Width / 2 - polyhedron.SegmentLength / 2;
            var y = size.Height / 2 - polyhedron.SegmentLength / 2;
            var z = 1.0;

            using (var g = Graphics.FromImage(pictureBox1.Image))
            {
                foreach (var item in polyhedron.vertexes)
                {
                    if (item.Z != 0)
                        z = item.Z;
                    var scaledPoint = new PointF((float)(item.X / z + x), (float)(item.Y / z) + y);
                    foreach (var neighbour in item.Neighbours)
                    {
                        var scaledNeighbour = new PointF((float)((float)neighbour.X / (float)z + x), (float)((float)neighbour.Y / (float)z) + y);
                        g.DrawLine(redPen, scaledPoint, scaledNeighbour);
                    }
                    res.Add(new PointF((float)(item.X / z + x), (float)(item.Y / z) + y));
                }
            }
        }

        private void DrawPolyhedronYOZ(Polyhedron polyhedron, Size size)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            var res = new List<PointF>();
            var y = size.Width / 2 - polyhedron.SegmentLength / 2;
            var z = size.Height / 2 - polyhedron.SegmentLength / 2;
            var x = 1.0;

            using (var g = Graphics.FromImage(pictureBox1.Image))
            {
                foreach (var item in polyhedron.vertexes)
                {
                    if (item.X != 0)
                        x = item.X;
                    var scaledPoint = new PointF((float)(item.Y / x + y), (float)(item.Z / x) + z);
                    foreach (var neighbour in item.Neighbours)
                    {
                        var scaledNeighbour = new PointF((float)((float)neighbour.Y / (float)x + y), (float)((float)neighbour.Z / (float)x) + z);
                        g.DrawLine(redPen, scaledPoint, scaledNeighbour);
                    }
                    res.Add(new PointF((float)(item.Y / x + y), (float)(item.Z / x) + z));
                }
            }
        }

        private void DrawPolyhedronXOZ(Polyhedron polyhedron, Size size)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            var res = new List<PointF>();
            var x = size.Width / 2 - polyhedron.SegmentLength / 2;
            var z = size.Height / 2 - polyhedron.SegmentLength / 2;
            var y = 1.0;

            using (var g = Graphics.FromImage(pictureBox1.Image))
            {
                foreach (var item in polyhedron.vertexes)
                {
                    if (item.Y != 0)
                        y = item.Y;
                    var scaledPoint = new PointF((float)(item.X / y + x), (float)(item.Z / y) + z);
                    foreach (var neighbour in item.Neighbours)
                    {
                        var scaledNeighbour = new PointF((float)((float)neighbour.X / (float)y + x), (float)((float)neighbour.Z / (float)y) + z);
                        g.DrawLine(redPen, scaledPoint, scaledNeighbour);
                    }
                    res.Add(new PointF((float)(item.X / y + x), (float)(item.Z / y) + z));
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            figType = "";
            mode = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var item in tasksPanels)
            {
                item.Enabled = false;
                item.Visible = false;
            }
            switch (listBox2.Text)
            {
                case "Задание 1":
                    mode = "";
                    break;
                case "Задание 2":
                    mode = "move";
                    tasksPanels[((ListBox)sender).SelectedIndex].Enabled = true;
                    tasksPanels[((ListBox)sender).SelectedIndex].Visible = true;
                    break;
                case "Задание 3":
                    mode = "reflection";
                    tasksPanels[((ListBox)sender).SelectedIndex].Enabled = true;
                    tasksPanels[((ListBox)sender).SelectedIndex].Visible = true;
                    break;
                case "Задание 4":
                    mode = "centrescale";
                    tasksPanels[((ListBox)sender).SelectedIndex].Enabled = true;
                    tasksPanels[((ListBox)sender).SelectedIndex].Visible = true;
                    break;
                case "Задание 7":
                    var c = pictureBox1.Height / 2;
                    var perspective = new List<List<double>> { new List<double> { 1, 0, 0, 0 }, new List<double> { 0, 1, 0, 0 }, new List<double> { 0, 0, 0, -1 / c }, new List<double> { 0, 0, 0, 1 } };

                    foreach (var item in figure.vertexes)
                        item.MultiplyByMatrix(perspective);
                    DrawPolyhedron(figure, pictureBox1.Size);
                    pictureBox1.Invalidate();
                    break;
                case "Задание 8":
                    var isometric = new List<List<double>> { new List<double> { Math.Sqrt(0.5), -1 / Math.Sqrt(6), 0, 0 }, new List<double> { 0, Math.Sqrt(2) / Math.Sqrt(3), 0, 0 }, new List<double> { -1 / Math.Sqrt(2), -1 / Math.Sqrt(6), 0, 0 }, new List<double> { 0, 0, 0, 1 } };

                    foreach (var item in figure.vertexes)
                        item.MultiplyByMatrix(isometric);
                    DrawPolyhedron(figure, pictureBox1.Size);
                    pictureBox1.Invalidate();
                    break;
                case "Задание 9 XOY":
                    var ortXOY = new List<List<double>> { new List<double> { 1, 0, 0, 0 }, new List<double> { 0, 1, 0, 0 }, new List<double> { 0, 0, 0, 0}, new List<double> { 0, 0, 0, 1 } };

                    foreach (var item in figure.vertexes)
                        item.MultiplyByMatrix(ortXOY);
                    DrawPolyhedron(figure, pictureBox1.Size);
                    pictureBox1.Invalidate();
                    break;
                case "Задание 9 XOZ":
                    var ortXOZ = new List<List<double>> { new List<double> { 1, 0, 0, 0 }, new List<double> { 0, 0, 0, 0 }, new List<double> { 0, 0, 1, 0 }, new List<double> { 0, 0, 0, 1 } };

                    foreach (var item in figure.vertexes)
                        item.MultiplyByMatrix(ortXOZ);
                    DrawPolyhedronXOZ(figure, pictureBox1.Size);
                    pictureBox1.Invalidate();
                    break;
                case "Задание 9 YOZ":
                    var ortYOZ = new List<List<double>> { new List<double> { 0, 0, 0, 0 }, new List<double> { 0, 1, 0, 0 }, new List<double> { 0, 0, 1, 0 }, new List<double> { 0, 0, 0, 1 } };

                    foreach (var item in figure.vertexes)
                        item.MultiplyByMatrix(ortYOZ);
                    DrawPolyhedronYOZ(figure, pictureBox1.Size);
                    pictureBox1.Invalidate();
                    break;
                case "Задание 5":
                    using (var centreLineDialog = new LineThroughCenter())
                    {
                        var dialogRes = centreLineDialog.ShowDialog();
                        if (dialogRes == DialogResult.OK)
                        {
                            var axes = centreLineDialog.AxisChosen;
                            Point3D parallelUnitVector;
                            switch (axes)
                            {
                                case "x":
                                    parallelUnitVector = new Point3D(1, 0, 0);
                                    break;
                                case "y":
                                    parallelUnitVector = new Point3D(0, 1, 0);
                                    break;
                                default:
                                    parallelUnitVector = new Point3D(0, 0, 1);
                                    break;
                            }
                            figure.RotateAroundLine(figure.Centre, parallelUnitVector, centreLineDialog.RotationAngleRadians);
                            DrawPolyhedron(figure, pictureBox1.Size);
                            pictureBox1.Invalidate();
                        }
                    }
                    break;
                case "Задание 6":
                    using (var lineRotateDialog = new LineToRotate())
                    {
                        var dialogRes = lineRotateDialog.ShowDialog();
                        if (dialogRes == DialogResult.OK)
                        {
                            var point1 = lineRotateDialog.Point1;
                            var point2 = lineRotateDialog.Point2;
                            var parallelVector = new Point3D(point2.X - point1.X, point2.Y - point1.Y, point2.Z - point1.Z);
                            var vectorLength = Math.Sqrt(parallelVector.X * parallelVector.X + parallelVector.Y * parallelVector.Y + parallelVector.Z * parallelVector.Z);
                            parallelVector.X /= vectorLength;
                            parallelVector.Y /= vectorLength;
                            parallelVector.Z /= vectorLength;
                            figure.RotateAroundLine(point2, parallelVector, lineRotateDialog.AngleRad);
                            DrawPolyhedron(figure, pictureBox1.Size);
                            pictureBox1.Invalidate();
                        }
                    }
                    break;

            }
        }

        #region task234
        private List<string> activeItems = new List<string>();

        private int[,] MultMatrix(int[,] a, int[,] b)
        {
            if (a.GetLength(1) != b.GetLength(0)) throw new Exception("Матрицы нельзя перемножить");
            int[,] r = new int[a.GetLength(0), b.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    for (int k = 0; k < b.GetLength(0); k++)
                    {
                        r[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            return r;
        }

        private double[,] MultMatrix(double[,] a, double[,] b)
        {
            if (a.GetLength(1) != b.GetLength(0)) throw new Exception("Матрицы нельзя перемножить");
            double[,] r = new double[a.GetLength(0), b.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    for (int k = 0; k < b.GetLength(0); k++)
                    {
                        r[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            return r;
        }

        private bool CheckCorrectPoint(double w, double h) =>
             w > 0 && w < pictureBox1.Width && 0 < h && h < pictureBox1.Height;

        private void Initialize2TaskPanel()
        {
            var task2Panel = new Panel();
            task2Panel.Visible = false;
            task2Panel.Enabled = false;
            task2Panel.Location = new Point(600, 250);
            task2Panel.Size = new Size(190, 180);
            this.Controls.Add(task2Panel);
            tasksPanels.Add(task2Panel);
            var task2LabelAbout = new Label();
            task2LabelAbout.Location = new Point(22, 2);
            task2LabelAbout.Size = new Size(180, 50);
            task2LabelAbout.Text
                = "Применение аффинных преобразований к примитиву: смещение, поворот, масштаб.";
            var task2ButtonMove = new Button();
            task2ButtonMove.Text = "C";
            task2ButtonMove.Location = new Point(27, 47);
            task2ButtonMove.Size = new Size(40, 22);
            task2ButtonMove.FlatStyle = FlatStyle.Flat;
            task2ButtonMove.Click += moveButton_Click;
            var task2ButtonTurn = new Button();
            task2ButtonTurn.Text = "П";
            task2ButtonTurn.Click += turnButton_Click;
            task2ButtonTurn.Location = new Point(85, 47);
            task2ButtonTurn.Size = new Size(40, 22);
            task2ButtonTurn.FlatStyle = FlatStyle.Flat;
            var task2ButtonScale = new Button();
            task2ButtonScale.Text = "М";
            task2ButtonScale.Location = new Point(140, 47);
            task2ButtonScale.Click += scaleButton_Click;
            task2ButtonScale.Size = new Size(40, 22);
            task2ButtonScale.FlatStyle = FlatStyle.Flat;
            task2Panel.Controls.Add(task2ButtonScale);
            task2Panel.Controls.Add(task2ButtonTurn);
            task2Panel.Controls.Add(task2ButtonMove);
            task2Panel.Controls.Add(task2LabelAbout);
            var task2LabelDirection = new Label();
            task2LabelDirection.Text = "Задайте смещение: ";
            task2LabelDirection.Name = "task2LabelDirection";
            task2LabelDirection.Location = new Point(22, 80);
            task2LabelDirection.Size = new Size(160, 30);
            task2LabelDirection.Enabled = false;
            task2LabelDirection.Visible = false;
            task2Panel.Controls.Add(task2LabelDirection);

            var task2XLabel = new Label();
            task2XLabel.Text = "X: ";
            task2XLabel.Name = "task2XLabel";
            task2XLabel.Location = new Point(10, 110);
            task2XLabel.Size = new Size(20, 20);
            task2XLabel.Enabled = false;
            task2XLabel.Visible = false;
            task2Panel.Controls.Add(task2XLabel);
            var task2XTextBox = new TextBox();
            task2XTextBox.Text = "0";
            task2XTextBox.Name = "task2XTextBox";
            task2XTextBox.Location = new Point(32, 110);
            task2XTextBox.Size = new Size(30, 20);
            task2XTextBox.Enabled = false;
            task2XTextBox.Visible = false;
            task2Panel.Controls.Add(task2XTextBox);

            var task2YLabel = new Label();
            task2YLabel.Text = "Y: ";
            task2YLabel.Name = "task2YLabel";
            task2YLabel.Location = new Point(72, 110);
            task2YLabel.Size = new Size(20, 20);
            task2YLabel.Enabled = false;
            task2YLabel.Visible = false;
            task2Panel.Controls.Add(task2YLabel);
            var task2YTextBox = new TextBox();
            task2YTextBox.Text = "0";
            task2YTextBox.Name = "task2YTextBox";
            task2YTextBox.Location = new Point(94, 110);
            task2YTextBox.Size = new Size(30, 20);
            task2YTextBox.Enabled = false;
            task2YTextBox.Visible = false;
            task2Panel.Controls.Add(task2YTextBox);

            var task2ZLabel = new Label();
            task2ZLabel.Text = "Z: ";
            task2ZLabel.Name = "task2ZLabel";
            task2ZLabel.Location = new Point(137, 110);
            task2ZLabel.Size = new Size(20, 20);
            task2ZLabel.Enabled = false;
            task2ZLabel.Visible = false;
            task2Panel.Controls.Add(task2ZLabel);
            var task2ZTextBox = new TextBox();
            task2ZTextBox.Text = "0";
            task2ZTextBox.Name = "task2ZTextBox";
            task2ZTextBox.Location = new Point(159, 110);
            task2ZTextBox.Size = new Size(30, 20);
            task2ZTextBox.Enabled = false;
            task2ZTextBox.Visible = false;
            task2Panel.Controls.Add(task2ZTextBox);


            var task2AngleLabel = new Label();
            task2AngleLabel.Text = "Угол: ";
            task2AngleLabel.Name = "task2AngleLabel";
            task2AngleLabel.Location = new Point(50, 110);
            task2AngleLabel.Size = new Size(40, 20);
            task2AngleLabel.Enabled = false;
            task2AngleLabel.Visible = false;
            task2Panel.Controls.Add(task2AngleLabel);
            var task2AngleTextBox = new TextBox();
            task2AngleTextBox.Text = "0";
            task2AngleTextBox.Name = "task2AngleTextBox";
            task2AngleTextBox.Location = new Point(95, 110);
            task2AngleTextBox.Size = new Size(30, 20);
            task2AngleTextBox.Enabled = false;
            task2AngleTextBox.Visible = false;
            task2Panel.Controls.Add(task2AngleTextBox);

            var task2ButtonOk = new Button();
            task2ButtonOk.Text = "Сместить";
            task2ButtonOk.Location = new Point(60, 150);
            task2ButtonOk.Name = "task2ButtonOk";
            task2ButtonOk.Click += task2ButtonOk_Click;
            task2ButtonOk.Size = new Size(80, 23);
            task2ButtonOk.Enabled = false;
            task2ButtonOk.Visible = false; task2ButtonOk.FlatStyle = FlatStyle.Flat;
            task2Panel.Controls.Add(task2ButtonOk);
        }

        private void Initialize3TaskPanel()
        {
            var task3Panel = new Panel();
            task3Panel.Visible = false;
            task3Panel.Enabled = false;
            task3Panel.Location = new Point(600, 250);
            task3Panel.Size = new Size(190, 180);
            this.Controls.Add(task3Panel);
            tasksPanels.Add(task3Panel);
            var task3LabelAbout = new Label();
            task3LabelAbout.Location = new Point(22, 2);
            task3LabelAbout.Size = new Size(180, 50);
            task3LabelAbout.Text
                = "Отражение относительно выбранной координатной плоскости";
            task3Panel.Controls.Add(task3LabelAbout);
            var task3LabelDirection = new Label();
            task3LabelDirection.Text = "Введите координатную плоскость: ";
            task3LabelDirection.Name = "task3LabelDirection";
            task3LabelDirection.Location = new Point(22, 80);
            task3LabelDirection.Size = new Size(160, 30);
            task3Panel.Controls.Add(task3LabelDirection);

            var task3RefLabel = new Label();
            task3RefLabel.Text = "Значение: ";
            task3RefLabel.Name = "task3ScaleLabel";
            task3RefLabel.Location = new Point(50, 110);
            task3RefLabel.Size = new Size(40, 20);
            task3Panel.Controls.Add(task3RefLabel);
            var task3RefTextBox = new TextBox();
            task3RefTextBox.Text = "xOy";
            task3RefTextBox.Name = "task3RefTextBox";
            task3RefTextBox.Location = new Point(95, 110);
            task3RefTextBox.Size = new Size(30, 20);
            task3Panel.Controls.Add(task3RefTextBox);

            var task3ButtonOk = new Button();
            task3ButtonOk.Text = "Отразить";
            task3ButtonOk.Location = new Point(60, 150);
            task3ButtonOk.Name = "task3ButtonOk";
            task3ButtonOk.Click += reflectionButton_Click;
            task3ButtonOk.Size = new Size(80, 23);
            task3ButtonOk.FlatStyle = FlatStyle.Flat;
            task3Panel.Controls.Add(task3ButtonOk);
        }

        private void Initialize4TaskPanel()
        {
            var task4Panel = new Panel();
            task4Panel.Visible = false;
            task4Panel.Enabled = false;
            task4Panel.Location = new Point(600, 250);
            task4Panel.Size = new Size(190, 180);
            this.Controls.Add(task4Panel);
            tasksPanels.Add(task4Panel);
            var task4LabelAbout = new Label();
            task4LabelAbout.Location = new Point(22, 2);
            task4LabelAbout.Size = new Size(180, 50);
            task4LabelAbout.Text
                = "Масштабирование многогранника относительно своего центра.";
            task4Panel.Controls.Add(task4LabelAbout);
            var task4LabelDirection = new Label();
            task4LabelDirection.Text = "Задайте масштаб: ";
            task4LabelDirection.Name = "task4LabelDirection";
            task4LabelDirection.Location = new Point(22, 80);
            task4LabelDirection.Size = new Size(160, 30);
            task4Panel.Controls.Add(task4LabelDirection);

            var task4ScaleLabel = new Label();
            task4ScaleLabel.Text = "Значение: ";
            task4ScaleLabel.Name = "task4ScaleLabel";
            task4ScaleLabel.Location = new Point(50, 110);
            task4ScaleLabel.Size = new Size(40, 20);
            task4Panel.Controls.Add(task4ScaleLabel);
            var task4ScaleTextBox = new TextBox();
            task4ScaleTextBox.Text = "0";
            task4ScaleTextBox.Name = "task4ScaleTextBox";
            task4ScaleTextBox.Location = new Point(95, 110);
            task4ScaleTextBox.Size = new Size(30, 20);
            task4Panel.Controls.Add(task4ScaleTextBox);

            var task4ButtonOk = new Button();
            task4ButtonOk.Text = "Масштабировать";
            task4ButtonOk.Location = new Point(60, 150);
            task4ButtonOk.Name = "task4ButtonOk";
            task4ButtonOk.Click += centreScaleButton_Click;
            task4ButtonOk.Size = new Size(80, 23);
            task4ButtonOk.FlatStyle = FlatStyle.Flat;
            task4Panel.Controls.Add(task4ButtonOk);
        }

        private void reflectionButton_Click(object sender, EventArgs e)
        {
            var t1 = tasksPanels[1].Controls.Find("task3RefTextBox", false).First().Text.ToLower();
            var matrix = new double[4, 4];
            if (t1 == "yox" || t1 == "xoy")
                matrix = new double[,] {
                    { 1, 0, 0, 0 },
                    { 0, 1, 0, 0 },
                    { 0, 0, -1, 0 },
                    { 0, 0, 0, 1 } };
            else if (t1 == "yoz" || t1 == "zoy")
                matrix = new double[,] {
                    { -1, 0, 0, 0 },
                    { 0, 1, 0, 0 },
                    { 0, 0, 1, 0 },
                    { 0, 0, 0 , 1 } };
            else if (t1 == "zox" || t1 == "xoz")
                matrix = new double[,] {
                    { 1, 0, 0, 0 },
                    { 0, -1, 0, 0 },
                    { 0, 0, 1, 0 },
                    { 0, 0, 0, 1 } };
            else
                return;
            var matrix2 = new double[figure.CountVertex, 4];
            for (int i = 0; i < figure.CountVertex; ++i)
            {
                matrix2[i, 0] = figure.vertexes[i].X;
                matrix2[i, 1] = figure.vertexes[i].Y;
                matrix2[i, 2] = figure.vertexes[i].Z;
                matrix2[i, 3] = 1;
            }
            var res2 = MultMatrix(matrix2, matrix);
            for (int i = 0; i < figure.CountVertex; ++i)
            {
                figure.vertexes[i].X = Math.Round(res2[i, 0], 3);
                figure.vertexes[i].Y = Math.Round(res2[i, 1], 3);
                figure.vertexes[i].Z = Math.Round(res2[i, 2], 3);
            }
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            DrawPolyhedron(figure, pictureBox1.Size);
            pictureBox1.Invalidate();
        }

        private void centreScaleButton_Click(object sender, EventArgs e)
        {
            var t1 = double.Parse(tasksPanels[2].Controls.Find("task4ScaleTextBox", false).First().Text);
            if (t1 == 0)
                return;
            var matrix = new double[,] {
                    { t1, 0, 0, 0 },
                    { 0, t1, 0, 0 },
                    { 0, 0, t1, 0 },
                    //{ 0, 0, 0, 1 } };

                  //{figure.Centre.X * t1, figure.Centre.Y * t1, t1, 1 } };
                    { figure.Centre.X - figure.Centre.X * t1, figure.Centre.Y - figure.Centre.Y * t1, figure.Centre.Z - figure.Centre.Z * t1, 1 } };
            var matrix2 = new double[figure.CountVertex, 4];
            for (int i = 0; i < figure.CountVertex; ++i)
            {
                matrix2[i, 0] = figure.vertexes[i].X;
                matrix2[i, 1] = figure.vertexes[i].Y;
                matrix2[i, 2] = figure.vertexes[i].Z;
                matrix2[i, 3] = 1;
            }
            var res2 = MultMatrix(matrix2, matrix);
            for (int i = 0; i < figure.CountVertex; ++i)
            {
                figure.vertexes[i].X = Math.Round(res2[i, 0], 3);
                figure.vertexes[i].Y = Math.Round(res2[i, 1], 3);
                figure.vertexes[i].Z = Math.Round(res2[i, 2], 3);
            }
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            DrawPolyhedron(figure, pictureBox1.Size);
            pictureBox1.Invalidate();
        }

        private void task2ButtonOk_Click(object sender, EventArgs e)
        {
            if (mode == "move")
                MoveFigure();
            else if (mode == "turn")
                TurnFigure();
            else if (mode == "scale")
                ScaleFigure();
        }


        private void moveButton_Click(object sender, EventArgs e)
        {
            mode = "move";
            foreach (var ai in activeItems)
            {
                var item = tasksPanels[0].Controls.Find(ai, false).First();
                item.Visible = false;
                item.Enabled = false;
            }
            var strs = new List<string> { "task2LabelDirection", "task2XLabel", "task2YLabel", "task2ZLabel", "task2XTextBox", "task2YTextBox", "task2ZTextBox", "task2ButtonOk" };
            foreach (var s in strs)
            {
                var item = tasksPanels[0].Controls.Find(s, false).First();
                if (item.Name == "task2LabelDirection")
                    item.Text = "Задайте смещение: ";
                else if (item.Name == "task2ButtonOk")
                    item.Text = "Сместить";
                item.Visible = true;
                item.Enabled = true;
            }
            activeItems = strs;
        }

        private void turnButton_Click(object sender, EventArgs e)
        {
            mode = "turn";
            foreach (var ai in activeItems)
            {
                var item = tasksPanels[0].Controls.Find(ai, false).First();
                item.Visible = false;
                item.Enabled = false;
            }
            var strs = new List<string> { "task2LabelDirection", "task2AngleLabel", "task2AngleTextBox", "task2ButtonOk" };
            foreach (var s in strs)
            {
                var item = tasksPanels[0].Controls.Find(s, false).First();
                if (item.Name == "task2LabelDirection")
                    item.Text = "Задайте поворот: ";
                else if (item.Name == "task2ButtonOk")
                    item.Text = "Повернуть";
                item.Visible = true;
                item.Enabled = true;
            }
            activeItems = strs;
        }

        private void scaleButton_Click(object sender, EventArgs e)
        {
            mode = "scale";
            foreach (var ai in activeItems)
            {
                var item = tasksPanels[0].Controls.Find(ai, false).First();
                item.Visible = false;
                item.Enabled = false;
            }
            var strs = new List<string> { "task2LabelDirection", "task2AngleLabel", "task2AngleTextBox", "task2ButtonOk" };
            foreach (var s in strs)
            {
                var item = tasksPanels[0].Controls.Find(s, false).First();
                if (item.Name == "task2AngleLabel")
                    item.Text = "Значение: ";
                else if (item.Name == "task2LabelDirection")
                    item.Text = "Задайте масштаб: ";
                else if (item.Name == "task2ButtonOk")
                    item.Text = "Масштабировать";
                item.Visible = true;
                item.Enabled = true;
            }
            activeItems = strs;
        }

        private void ScaleFigure()
        {
            var t1 = double.Parse(tasksPanels[0].Controls.Find("task2AngleTextBox", false).First().Text);
            if (t1 == 0)
                return;
            var matrix = new double[,] {
                    { 1, 0, 0, 0 },
                    { 0, 1, 0, 0 },
                    { 0, 0, 1, 0 },
                    { 0, 0, 0, t1 } };
            var matrix2 = new double[figure.CountVertex, 4];
            for (int i = 0; i < figure.CountVertex; ++i)
            {
                matrix2[i, 0] = figure.vertexes[i].X;
                matrix2[i, 1] = figure.vertexes[i].Y;
                matrix2[i, 2] = figure.vertexes[i].Z;
                matrix2[i, 3] = 1;
            }
            var res2 = MultMatrix(matrix2, matrix);
            for (int i = 0; i < figure.CountVertex; ++i)
            {
                figure.vertexes[i].X = Math.Round(res2[i, 0] / t1, 3);
                figure.vertexes[i].Y = Math.Round(res2[i, 1] / t1, 3);
                figure.vertexes[i].Z = Math.Round(res2[i, 2] / t1, 3);
            }
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            DrawPolyhedron(figure, pictureBox1.Size);
            pictureBox1.Invalidate();
        }

        private void TurnFigure()
        {
            var t1 = double.Parse(tasksPanels[0].Controls.Find("task2AngleTextBox", false).First().Text);
            var matrix = new double[,] { { Math.Cos(Math.PI * t1 / 180), Math.Sin(Math.PI * t1 / 180), 0, 0 },
                                     { -Math.Sin(Math.PI * t1 / 180), Math.Cos(Math.PI * t1 / 180), 0, 0 },
                                     { 0, 0, 1, 0 },
                                        {0,0,0,1 } };
            double[,] matrix2 = new double[figure.CountVertex, 4];
            for (int i = 0; i < figure.CountVertex; ++i)
            {
                matrix2[i, 0] = figure.vertexes[i].X;
                matrix2[i, 1] = figure.vertexes[i].Y;
                matrix2[i, 2] = figure.vertexes[i].Z;
                matrix2[i, 3] = 1;
            }
            var res2 = MultMatrix(matrix2, matrix);
            for (int i = 0; i < figure.CountVertex; ++i)
            {
                figure.vertexes[i].X = Math.Round(res2[i, 0], 3);
                figure.vertexes[i].Y = Math.Round(res2[i, 1], 3);
                figure.vertexes[i].Z = Math.Round(res2[i, 2], 3);
            }
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            DrawPolyhedron(figure, pictureBox1.Size);
            pictureBox1.Invalidate();
        }

        void MoveFigure()
        {
            var t1 = tasksPanels[0].Controls.Find("task2XTextBox", false).First().Text;
            var t2 = tasksPanels[0].Controls.Find("task2YTextBox", false).First().Text;
            var t3 = tasksPanels[0].Controls.Find("task2ZTextBox", false).First().Text;
            var matrix = new double[,] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { int.Parse(t1), int.Parse(t2), int.Parse(t3), 1 } };
            double[,] matrix2 = new double[figure.CountVertex, 4];
            for (int i = 0; i < figure.CountVertex; ++i)
            {
                matrix2[i, 0] = figure.vertexes[i].X;
                matrix2[i, 1] = figure.vertexes[i].Y;
                matrix2[i, 2] = figure.vertexes[i].Z;
                matrix2[i, 3] = 1;
            }
            var res2 = MultMatrix(matrix2, matrix);
            for (int i = 0; i < figure.CountVertex; ++i)
            {
                figure.vertexes[i].X = Math.Round(res2[i, 0], 3);
                figure.vertexes[i].Y = Math.Round(res2[i, 1], 3);
                figure.vertexes[i].Z = Math.Round(res2[i, 2], 3);
            }
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            DrawPolyhedron(figure, pictureBox1.Size);
            pictureBox1.Invalidate();
        }

        #endregion

        private Point3D VectorProduct(Point3D vector1, Point3D vector2)
        {
            double newX = vector1.Y * vector2.Z - vector1.Z * vector2.Y;
            double newY = vector1.X * vector2.Z - vector1.Z * vector2.X;
            double newZ = vector1.X * vector2.Y - vector1.Y * vector2.X;
            return new Point3D(newX, newY, newZ);
        }

        private void ReadPolyhedronFromFile(string filepath)
        {
            var edgesLines = File.ReadAllLines(filepath);
            int edgeVertexCount = edgesLines[0].Split('|').Length - 1;
            var edgesPoints = new Point3D[edgesLines.Length, edgeVertexCount - 1];
            var edgesAdjacentPoints = new List<AdjacentPoints>[edgesLines.Length];
            for (int i = 0; i < edgesLines.Length; ++i)
            {
                var edgeInfo = SplitEdgeInfo(edgesLines[i]);
                ParseEdge(edgeInfo.Item1, i, edgesPoints);
                foreach (var adjacentEdgeInfo in edgeInfo.Item2.Split('E'))
                    edgesAdjacentPoints[i].Add(ParseAdjacentPoints(adjacentEdgeInfo, i));
            }
            AddAdjacentNeighbours(edgesPoints, edgesAdjacentPoints);
            figure = BuildPolyhedronFromPoints(edgesPoints);
        }

        private Polyhedron BuildPolyhedronFromPoints(Point3D[,] points)
        {
            //TODO: build polyhedron from points
            var polyhedron = new Polyhedron(PolyhedronType.Dodecahedron, -20);
            polyhedron.vertexes.Clear();
            foreach (var point in points)
                polyhedron.vertexes.Add(point);
            return polyhedron;
        }

        private void AddAdjacentNeighbours(Point3D[,] edgesPoints, List<AdjacentPoints>[] adjacentPoints)
        {
            for (int i = 0; i < edgesPoints.GetLength(0); ++i)
                AddEdgeNeighbours(edgesPoints, i, adjacentPoints[i]);
        }


        private void AddEdgeNeighbours(Point3D[,] edgesPoints, int edgeNumber, List<AdjacentPoints> adjacentPointsLst)
        {
            foreach (var adjacentPoints in adjacentPointsLst)
                foreach (var adjIdxs in adjacentPoints.Item2)
                    edgesPoints[edgeNumber, adjIdxs.Item1].AddNeighbour(edgesPoints[adjacentPoints.Item1, adjIdxs.Item2]);
        }

        private AdjacentPoints ParseAdjacentPoints(string adjacentString, int edgeNumber)
        {
            var adjacentStrSplitted = adjacentString.Split(':');
            int adjacentEdgeIdx = int.Parse(adjacentStrSplitted[0]);
            var adjacentList = adjacentStrSplitted[1];
            int firstIdx = 0;
            int lastIdx = adjacentList.IndexOf(')', firstIdx + 1);
            var adjacentVertices = new List<Tuple<int, int>>();
            while (true)
            {
                var numbersStrs = adjacentList.Substring(firstIdx + 1, lastIdx - firstIdx - 1).Split(',');
                adjacentVertices.Add(new Tuple<int, int>(int.Parse(numbersStrs[0]), int.Parse(numbersStrs[1])));
                if (lastIdx == adjacentList.Length - 1)
                    break;
                firstIdx = lastIdx + 2;
                lastIdx = adjacentList.IndexOf(')', firstIdx + 1);
            }
            //adjacentPoints[edgeNumber] = new AdjacentPoints(adjacentEdgeIdx, adjacentVertices);
            return new AdjacentPoints(adjacentEdgeIdx, adjacentVertices);
        }

        private Tuple<String, String> SplitEdgeInfo(string edgeInfo)
        {
            int lastSepIdx = edgeInfo.Length - 1;
            while (edgeInfo[lastSepIdx] != '|')
                --lastSepIdx;
            return new Tuple<string, string>(edgeInfo.Substring(0, lastSepIdx), edgeInfo.Substring(lastSepIdx + 1));
        }

        private void ParseEdge(string edgeStr, int edgeNumber, Point3D[,] pointsList)
        {
            var pointsAndAdjacentVertexStr = edgeStr.Split('|').ToArray();
            for (int i = 0; i < pointsAndAdjacentVertexStr.Length - 1; ++i)
            {
                pointsList[edgeNumber, i] = ParsePoint(pointsAndAdjacentVertexStr[i]);
                if (i > 0)
                {
                    pointsList[edgeNumber, i].AddNeighbour(pointsList[edgeNumber, i - 1]);
                    pointsList[edgeNumber, i - 1].AddNeighbour(pointsList[edgeNumber, i]);
                }
            }
            pointsList[edgeNumber, 0].AddNeighbour(pointsList[edgeNumber, pointsList.GetLength(1) - 1]);
            pointsList[edgeNumber, pointsList.GetLength(1) - 1].AddNeighbour(pointsList[edgeNumber, 0]);
        }


        private Point3D ParsePoint(string pointStr)
        {
            var pointCoords = pointStr.Split(';');
            var xCoord = double.Parse(pointCoords[0]);
            var yCoord = double.Parse(pointCoords[1]);
            var zCoord = double.Parse(pointCoords[2]);
            return new Point3D(xCoord, yCoord, zCoord);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var f = new Lab7Task2();
            f.Show();
        }

        private void task1Button_Click(object sender, EventArgs e)
        {
            using (var pathForm = new Lab7Task1Path())
            {
                var dialogRes = pathForm.ShowDialog();
                if (dialogRes == DialogResult.OK)
                {
                    var filePath = pathForm.FilePath;
                    ReadPolyhedronFromFile(filePath);
                    DrawPolyhedron(figure, pictureBox1.Size);
                    pictureBox1.Invalidate();
                }
            }
        }

        private Point3D VectorFromZeroCoords(Point3D vectorStart, Point3D vectorEnd) =>
            new Point3D(vectorEnd.X - vectorStart.X, vectorEnd.Y - vectorStart.Y, vectorEnd.Z - vectorStart.Z);

        private void lab8Task1Button_Click(object sender, EventArgs e)
        {
            using (var vectorDialog = new Lab8Task1Vector())
            {
                var dialogRes = vectorDialog.ShowDialog();
                var edgesToRestore = new List<int>();

                if (dialogRes == DialogResult.OK)
                {
                    var viewVector = new Point3D(vectorDialog.X, vectorDialog.Y, vectorDialog.Z);
                    var normalVector = new Point3D();
                    var edgeVector = new Point3D();
                    bool shouldBeAdded = true;
                    int idx = 0;
                    foreach (var edge in figure.edges)
                    {
                        var edgeVectors = new List<Point3D>();
                        normalVector = VectorFromZeroCoords(figure.vertexes[edge[0]], figure.vertexes[edge[1]]);
                        edgeVectors.Add(normalVector);
                        for (int i = 1; i < edge.Count - 1; ++i)
                        {
                            edgeVector = VectorFromZeroCoords(figure.vertexes[edge[i]], figure.vertexes[edge[i + 1]]);
                            shouldBeAdded = true;
                            foreach (var v in edgeVectors)
                            {
                                var oldAngle = VectorAngle(v, edgeVector) * 180 / Math.PI;
                                if (Math.Abs(oldAngle - 180) < 0.1 || Math.Abs(oldAngle - 180) < 0.1)
                                {
                                    shouldBeAdded = false;
                                    break;
                                }
                            }
                            if (shouldBeAdded)
                            {
                                normalVector = VectorProduct(normalVector, edgeVector);
                                edgeVectors.Add(new Point3D(edgeVector.X, edgeVector.Y, edgeVector.Z));
                            }
                        }
                        edgeVector = VectorFromZeroCoords(figure.vertexes[edge[edge.Count - 1]], figure.vertexes[edge[0]]);
                        shouldBeAdded = true;
                        foreach (var v in edgeVectors)
                        {
                            var oldAngle = VectorAngle(v, edgeVector) * 180 / Math.PI;
                            if (Math.Abs(oldAngle - 180) < 0.1 || Math.Abs(oldAngle - 180) < 0.1)
                            {
                                shouldBeAdded = false;
                                break;
                            }
                        }
                        if (shouldBeAdded)
                        {
                            normalVector = VectorProduct(normalVector, edgeVector);
                            edgeVectors.Add(new Point3D(edgeVector.X, edgeVector.Y, edgeVector.Z));
                        }
                        normalVector = FixNormal(edge, figure, normalVector);
                        if (!RemoveEdgeIfAngleIsBig(edge, figure, normalVector, viewVector))
                            edgesToRestore.Add(idx);
                        ++idx;
                    }
                }
                foreach (var edgeIdx in edgesToRestore)
                    RestoreEdge(figure.edges[edgeIdx], figure);
            }
        }

        private Point3D FixNormal(List<int> edge, Polyhedron polyhedron, Point3D normal)
        {
            var newNormalStart = polyhedron.vertexes[edge[0]];
            var newNormalEnd = new Point3D(normal.X + newNormalStart.X, normal.Y + newNormalStart.Y, normal.Z + newNormalStart.Z);
            var normalDx = newNormalEnd.X - newNormalStart.X;
            var normalDy = newNormalEnd.Y - newNormalStart.Y;
            var normalDz = newNormalEnd.Z - newNormalStart.Z;
            var normalLength = Math.Sqrt(normalDx * normalDx + normalDy * normalDy + normalDz * normalDz);
            var stepLength = 1.0 / normalLength;
            var nextNormalPoint = new Point3D(newNormalStart.X + normalDx * stepLength, newNormalStart.Y + normalDy * stepLength,
                newNormalStart.Z + normalDz * stepLength);
            if (SecondPointIsCloser(polyhedron.Centre, newNormalStart, nextNormalPoint))
                return new Point3D(-1 * normal.X, -1 * normal.Y, -1 * normal.Z);
            return normal;
        }

        private bool SecondPointIsCloser(Point3D point1, Point3D point2, Point3D point3)
        {
            var p2ToP1Dx = point2.X - point1.X;
            var p2ToP1Dy = point2.Y - point1.Y;
            var p2ToP1Dz = point2.Z - point1.Z;
            var p2ToP1Dist = Math.Sqrt(p2ToP1Dx * p2ToP1Dx + p2ToP1Dy * p2ToP1Dy + p2ToP1Dz * p2ToP1Dz);

            var p3ToP1Dx = point3.X - point1.X;
            var p3ToP1Dy = point3.Y - point1.Y;
            var p3ToP1Dz = point3.Z - point1.Z;
            var p3ToP1Dist = Math.Sqrt(p3ToP1Dx * p3ToP1Dx + p3ToP1Dy * p3ToP1Dy + p3ToP1Dz * p3ToP1Dz);

            return p3ToP1Dist < p2ToP1Dist;
        }

        /*
         * full_len = |B - A| // длина вектора, соединяющего две точки == длина отрезка
C = A + (B - A) * (len / full_len)
         * */

        private void RestoreEdge(List<int> edge, Polyhedron polyhedron)
        {
            for (int i = 0; i < edge.Count - 1; ++i)
            {
                polyhedron.vertexes[edge[i]].Neighbours.Add(polyhedron.vertexes[edge[i + 1]]);
                polyhedron.vertexes[edge[i + 1]].Neighbours.Add(polyhedron.vertexes[edge[i]]);
            }
            polyhedron.vertexes[edge[0]].Neighbours.Add(polyhedron.vertexes[edge[edge.Count - 1]]);
            polyhedron.vertexes[edge[edge.Count - 1]].Neighbours.Add(polyhedron.vertexes[edge[0]]);
        }

        private bool RemoveEdgeIfAngleIsBig(List<int> edge, Polyhedron polyhedron, Point3D normalVector, Point3D pointOfViewVector)
        {
            var angle = VectorAngle(pointOfViewVector, normalVector) * 180 / Math.PI;
            if (angle >= 90 && angle <= 270)
            {
                for (int i = 0; i < edge.Count - 1; ++i)
                {
                    polyhedron.vertexes[edge[i]].Neighbours = polyhedron.vertexes[edge[i]].Neighbours
                        .Where(p => p.X != polyhedron.vertexes[edge[i + 1]].X || p.Y != polyhedron.vertexes[edge[i + 1]].Y
                        || p.Z != polyhedron.vertexes[edge[i + 1]].Z).ToList();
                    polyhedron.vertexes[edge[i + 1]].Neighbours = polyhedron.vertexes[edge[i + 1]].Neighbours
                        .Where(p => p.X != polyhedron.vertexes[edge[i]].X || p.Y != polyhedron.vertexes[edge[i]].Y
                        || p.Z != polyhedron.vertexes[edge[i]].Z).ToList();
                }
                polyhedron.vertexes[edge[0]].Neighbours = polyhedron.vertexes[edge[0]].Neighbours
                        .Where(p => p.X != polyhedron.vertexes[edge[edge.Count - 1]].X || p.Y != polyhedron.vertexes[edge[edge.Count - 1]].Y
                        || p.Z != polyhedron.vertexes[edge[edge.Count - 1]].Z).ToList();
                polyhedron.vertexes[edge[edge.Count - 1]].Neighbours = polyhedron.vertexes[edge[edge.Count - 1]].Neighbours
                    .Where(p => p.X != polyhedron.vertexes[edge[0]].X || p.Y != polyhedron.vertexes[edge[0]].Y
                    || p.Z != polyhedron.vertexes[edge[0]].Z).ToList();
                return true;
            }
            else
                return false;
        }

        private double VectorAngle(Point3D vector1, Point3D vector2)
        {
            double v1Length = Math.Sqrt(vector1.X * vector1.X + vector1.Y * vector1.Y + vector1.Z * vector1.Z);
            double v2Length = Math.Sqrt(vector2.X * vector2.X + vector2.Y * vector2.Y + vector2.Z * vector2.Z);
            double cos = (vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z) / (v1Length * v2Length);
            return Math.Acos(cos);
        }
    }
}
