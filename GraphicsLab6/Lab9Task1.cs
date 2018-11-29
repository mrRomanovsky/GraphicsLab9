using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicsLab6
{
    class Lab9Task1
    {
        private Form1 form;
        private Point3D lightPos;
        private Color startColor;
        private List<Polyhedron> figures;
        private List<Vertex> vertices;
        public class Mesh
        {
            public string Name { get; set; }
            public Vertex[] Vertices { get; private set; }
            public Edge[] Faces { get; set; }
            public Point3D Position { get; set; }
            public Point3D Rotation { get; set; }

            public Mesh(string name, int verticesCount, int facesCount)
            {
                Vertices = new Vertex[verticesCount];
                Faces = new Edge[facesCount];
                Name = name;
            }
        }
        double addedX = 0;
        double addedY = 0;
        PictureBox pictureBox;

        public struct Vertex
        {
            public Point3D Normal;
            public Point3D Coordinates;
            public Point3D WorldCoordinates;
        }

        public struct ScanLineData
        {
            public int currentY;
            public double ndotla;
            public double ndotlb;
            public double ndotlc;
            public double ndotld;
        }

        public Lab9Task1(Form1 form1, Point3D lightSource, int colorNum, List<Polyhedron> figures, PictureBox pb)
        {
            this.pictureBox = pb;
            form = form1;
            this.lightPos = lightSource;
            ReadColor(colorNum);
            this.figures = figures;
            vertices = new List<Vertex>();
        }

        private void ReadColor(int colorNum)
        {
            switch (colorNum)
            {
                case 0:
                    startColor = Color.Red;
                    break;
                case 1:
                    startColor = Color.Orange;
                    break;
                case 2:
                    startColor = Color.Yellow;
                    break;
                case 3:
                    startColor = Color.Green;
                    break;
                case 4:
                    startColor = Color.LightBlue;
                    break;
                case 5:
                    startColor = Color.Blue;
                    break;
                case 6:
                    startColor = Color.Violet;
                    break;
                default:
                    startColor = Color.Black;
                    break;
            }
        }

        #region удаление невидимых граней
        private Point3D VectorFromZeroCoords(Point3D vectorStart, Point3D vectorEnd) =>
           new Point3D(vectorEnd.X - vectorStart.X, vectorEnd.Y - vectorStart.Y, vectorEnd.Z - vectorStart.Z);

        private Point3D VectorProduct(Point3D vector1, Point3D vector2)
        {
            double newX = vector1.Y * vector2.Z - vector1.Z * vector2.Y;
            double newY = vector1.X * vector2.Z - vector1.Z * vector2.X;
            double newZ = vector1.X * vector2.Y - vector1.Y * vector2.X;
            return new Point3D(newX, newY, newZ);
        }

        private Point3D GetNormal(List<int> edge, Polyhedron polyhedron)
        {
            var edgeVectors = new List<Point3D>();
            var normalVector = VectorFromZeroCoords(polyhedron.vertexes[edge[0]], polyhedron.vertexes[edge[1]]);
            var edgeVector = new Point3D();
            edgeVectors.Add(normalVector);
            bool shouldBeAdded = true; ;
            for (int i = 1; i < edge.Count - 1; ++i)
            {
                edgeVector = VectorFromZeroCoords(polyhedron.vertexes[edge[i]], polyhedron.vertexes[edge[i + 1]]);
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
            edgeVector = VectorFromZeroCoords(polyhedron.vertexes[edge[edge.Count - 1]], polyhedron.vertexes[edge[0]]);
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
            normalVector = FixNormal(edge, polyhedron, normalVector);
            return normalVector;
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

        private double VectorAngle(Point3D vector1, Point3D vector2)
        {
            double v1Length = Math.Sqrt(vector1.X * vector1.X + vector1.Y * vector1.Y + vector1.Z * vector1.Z);
            double v2Length = Math.Sqrt(vector2.X * vector2.X + vector2.Y * vector2.Y + vector2.Z * vector2.Z);
            double cos = (vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z) / (v1Length * v2Length);
            return Math.Acos(cos);
        }
        #endregion


        private List<Point3D> GetNormalsForVertices(Polyhedron figure, string plan, ref bool[] deletedEdges)
        {
            var viewVector = new Point3D(0, 0, -1);//new Point3D(vectorDialog.X, vectorDialog.Y, vectorDialog.Z);

            //#region удаление невидимых граней
            //var normalVector = new Point3D();
            //var edgesToRestore = new List<int>();
            //// if (figure.Type != PolyhedronType.Octahedron)
            //for (int i = 0; i < figure.edges.Count; ++i)
            //{
            //    var edge = figure.edges[i];
            //    normalVector = GetNormal(edge, figure);
            //    normalVector = FixNormal(edge, figure, normalVector);
            //    var angle = VectorAngle(viewVector, normalVector) * 180 / Math.PI;
            //    deletedEdges[i] = angle >= 90 && angle <= 270;
            //}
            //#endregion
            pictureBox.BackColor = Color.Gray;
            var dicAdgancentEdgeWithVertex = figure.GetDictionaryAdgacentEdgeForvertexesClassEdge();
            var listNormalsVertices = new List<Point3D>();
            foreach (var item in dicAdgancentEdgeWithVertex)
            {
                // var edgesNormals = new List<Point3D>();
                var sumNormalsEdges = new Point3D(0, 0, 0);
                foreach (var edge in item.Value)
                {
                    //if (deletedEdges[edge.Number])
                    //    continue;
                    // edgesNormals.Add(form.GetNormal(edge, figure));
                    sumNormalsEdges += form.GetNormal(figure.edges[edge.Number], figure);
                }
                listNormalsVertices.Add(sumNormalsEdges / item.Value.Count);
                Point3D wordCoord = new Point3D();
                if (plan == "xoy" || plan == "8")
                {
                    var z = 1.0;
                    if (item.Key.Z != 0)
                        z = item.Key.Z;
                    wordCoord = new Point3D(item.Key.X / z, item.Key.Y / z, item.Key.Z);
                 }
                if (plan == "xoz")
                {
                    var z = 1.0;
                    if (item.Key.Y != 0)
                        z = item.Key.Y;
                    wordCoord = new Point3D(item.Key.X / z, item.Key.Z / z, item.Key.Y);
                }
                if (plan == "yoz")
                {
                    var z = 1.0;
                    if (item.Key.X != 0)
                        z = item.Key.X;
                    wordCoord = new Point3D(item.Key.Y / z, item.Key.Z / z, item.Key.X);
                }
                vertices.Add(new Vertex { Coordinates = wordCoord, Normal = sumNormalsEdges / item.Value.Count , WorldCoordinates = item.Key });
            }
            return listNormalsVertices;
        }

        public void GouraudShading(string plan)//Polyhedron phd)
        {
            Device();
            foreach (var figure in figures)
            {
                vertices = new List<Vertex>();
                bool [] deletedEdges  = new bool[figure.edges.Count];
                GetNormalsForVertices(figure, plan, ref deletedEdges);
                addedX = renderWidth/ 2 - figure.SegmentLength / 2;
                addedY = renderHeight / 2 - figure.SegmentLength / 2;
                var i = 0;
                foreach (var edge in figure.Edges)
                {

                    //if (deletedEdges[edge.Number])
                    //    continue;
                    if (figure.Type == PolyhedronType.Hexahedron)
                    {
                        //if (plan == "8" && i > 4)
                        //    break;
                        DrawTriangle(vertices.Where(v => v.WorldCoordinates == edge.Vertexes[0]).First(),
                           vertices.Where(v => v.WorldCoordinates == edge.Vertexes[1]).First(),
                           vertices.Where(v => v.WorldCoordinates == edge.Vertexes[2]).First(),
                           startColor); 
                        DrawTriangle(vertices.Where(v => v.WorldCoordinates == edge.Vertexes[2]).First(),
                           vertices.Where(v => v.WorldCoordinates == edge.Vertexes[3]).First(),
                           vertices.Where(v => v.WorldCoordinates == edge.Vertexes[0]).First(),
                           startColor);
                    }
                    else if (figure.Type != PolyhedronType.Hexahedron)
                    {
                        if (figure.Type == PolyhedronType.Octahedron && plan == "8" && (i == 0 || i == 4 || i == 5 || i == 7))
                            DrawTriangle(vertices.Where(v => v.WorldCoordinates == edge.Vertexes[0]).First(),
                               vertices.Where(v => v.WorldCoordinates == edge.Vertexes[1]).First(),
                               vertices.Where(v => v.WorldCoordinates == edge.Vertexes[2]).First(),
                               startColor);
                        else if (plan != "8" || figure.Type == PolyhedronType.Tetrahedron)
                            DrawTriangle(vertices.Where(v => v.WorldCoordinates == edge.Vertexes[0]).First(),
                           vertices.Where(v => v.WorldCoordinates == edge.Vertexes[1]).First(),
                           vertices.Where(v => v.WorldCoordinates == edge.Vertexes[2]).First(),
                           startColor);
                    }
                    ++i;
                    pictureBox.Image = bmp;
                 //   pictureBox.Refresh();
                }
            }
            pictureBox.Image = bmp;
            pictureBox.Invalidate();
        }

        private byte[] backBuffer;
        private  double[] depthBuffer;
        private object[] lockBuffer;
        private Bitmap bmp;
        private  int renderWidth;
        private  int renderHeight;

        public void Device()
        {
            this.bmp = new Bitmap(pictureBox.Width,  pictureBox.Height);
            renderWidth = bmp.Width;
            renderHeight = bmp.Height;

            // the back buffer size is equal to the number of pixels to draw
            // on screen (width*height) * 4 (R,G,B & Alpha values). 
            backBuffer = new byte[renderWidth * renderHeight * 4];
            depthBuffer = new double[renderWidth * renderHeight];
            //lockBuffer = new object[renderWidth * renderHeight];
            for (var i = 0; i < depthBuffer.Length; i++)
            {
                depthBuffer[i] = double.MaxValue;
            }
        }

        public void PutPixel(int x, int y, double z, Color color)
        {
            var index = (x + y * renderWidth);
            if (depthBuffer[index] < z)
            {
                return; // Discard
            }

            depthBuffer[index] = z;

            bmp.SetPixel(x, y, Color.FromArgb(color.A, color.R, color.G, color.B ));

            // As we have a 1-D Array for our back buffer
            // we need to know the equivalent cell in 1-D based
            // on the 2D coordinates on screen
            var index4 = index * 4;

            // Protecting our buffer against threads concurrencies
            //lock (lockBuffer[index])
            //{
            

            //    backBuffer[index4] = (byte)(color.B * 255);
            //    backBuffer[index4 + 1] = (byte)(color.G * 255);
            //    backBuffer[index4 + 2] = (byte)(color.R * 255);
            //    backBuffer[index4 + 3] = (byte)(color.A * 255);
          //  }
        }

        public void Clear(byte r, byte g, byte b, byte a)
        {
            // Clearing Back Buffer
            for (var index = 0; index < backBuffer.Length; index += 4)
            {
                // BGRA is used by Windows instead by RGBA in HTML5
                backBuffer[index] = b;
                backBuffer[index + 1] = g;
                backBuffer[index + 2] = r;
                backBuffer[index + 3] = a;
            }

            // Clearing Depth Buffer
            for (var index = 0; index < depthBuffer.Length; index++)
            {
                depthBuffer[index] = double.MaxValue;
            }
        }

        public void DrawPoint(Point3D point, Color color)
        {
            // Clipping what's visible on screen
            if (point.X + addedX >= 0 && point.Y + addedY >= 0 && point.X + addedX < renderWidth && point.Y + addedY < renderHeight)
            {
                // Drawing a point
                PutPixel((int)(point.X + addedX), (int)(point.Y + addedY), (double)point.Z, color);
            }
        }

        // Once everything is ready, we can flush the back buffer
        // into the front buffer. 
        //public void Present()
        //{
        //    using (var stream = bmp.PixelBuffer.AsStream())
        //    {
        //        // writing our byte[] back buffer into our WriteableBitmap stream
        //        stream.Write(backBuffer, 0, backBuffer.Length);
        //    }
        //    // request a redraw of the entire bitmap
        //    bmp.Invalidate();
        //}

        // Clamping values to keep them between 0 and 1
        double Clamp(double value, double min = 0, double max = 1)
        {
            return Math.Max(min, Math.Min(value, max));
        }

        double Interpolate(double min, double max, double gradient)
        {
            return (double)(min + (max - min) * Clamp(gradient));
        }

        public Vertex Project(Vertex vertex, double [,] transMat, double[,] world)
        {
            var point2d = Point3D.TransformCoordinate(vertex.Coordinates, transMat);
            var point3dWorld = Point3D.TransformCoordinate(vertex.Coordinates, world);
            var normal3dWorld = Point3D.TransformCoordinate(vertex.Normal, world);

            var x = point2d.X * renderWidth + renderWidth / 2.0f;
            var y = -point2d.Y * renderHeight + renderHeight / 2.0f;

            return new Vertex
            {
                Coordinates = new Point3D(x, y, point2d.Z),
                Normal = normal3dWorld,
                WorldCoordinates = point3dWorld
            };
        }

        double ComputeNDotL(Point3D vertex, Point3D normal, Point3D lightPosition)
        {
            var lightDirection = lightPosition - vertex;

            normal.Normalize();
            lightDirection.Normalize();
            return Math.Max(0, (double)Point3D.Dot(normal, lightDirection));
        }

        void ProcessScanLine(ScanLineData data, Vertex va, Vertex vb, Vertex vc, Vertex vd, Color color)
        {
            Point3D pa = va.Coordinates;
            Point3D pb = vb.Coordinates;
            Point3D pc = vc.Coordinates;
            Point3D pd = vd.Coordinates;

            var gradient1 = pa.Y != pb.Y ? (data.currentY - pa.Y) / (pb.Y - pa.Y) : 1;
            var gradient2 = pc.Y != pd.Y ? (data.currentY - pc.Y) / (pd.Y - pc.Y) : 1;

            int sx = (int)Interpolate(pa.X, pb.X, gradient1);
            int ex = (int)Interpolate(pc.X, pd.X, gradient2);

            double z1 = Interpolate(pa.Z, pb.Z, gradient1);
            double z2 = Interpolate(pc.Z, pd.Z, gradient2);

            var snl = Interpolate(data.ndotla, data.ndotlb, gradient1);
            var enl = Interpolate(data.ndotlc, data.ndotld, gradient2);

            if (sx > ex)
            {
                form.Swap(ref sx, ref ex);
                form.Swap(ref z1, ref z2);
                form.Swap(ref snl, ref enl);
            }
            for (var x = sx; x < ex; x++)
            {
                double gradient = (x - sx) / (double)(ex - sx);

                var z = Interpolate(z1, z2, gradient);
                var ndotl = Interpolate(snl, enl, gradient);
                DrawPoint(new Point3D(x, data.currentY, z), MultColorOnNumb(color, ndotl)); 
            }
        }

        private Color MultColorOnNumb(Color color, double num)
        {
            return Color.FromArgb((int)(color.R * num), (int)(color.G * num), (int)(color.B * num));
        }

        public void DrawTriangle(Vertex v1, Vertex v2, Vertex v3, Color color)
        {
            if (v1.Coordinates.Y > v2.Coordinates.Y)
            {
                var temp = v2;
                v2 = v1;
                v1 = temp;
            }

            if (v2.Coordinates.Y > v3.Coordinates.Y)
            {
                var temp = v2;
                v2 = v3;
                v3 = temp;
            }

            if (v1.Coordinates.Y > v2.Coordinates.Y)
            {
                var temp = v2;
                v2 = v1;
                v1 = temp;
            }

            Point3D p1 = v1.Coordinates;
            Point3D p2 = v2.Coordinates;
            Point3D p3 = v3.Coordinates;

            // Light position 
            Point3D lightPos = this.lightPos;
            double nl1 = ComputeNDotL(v1.WorldCoordinates, v1.Normal, lightPos);
            double nl2 = ComputeNDotL(v2.WorldCoordinates, v2.Normal, lightPos);
            double nl3 = ComputeNDotL(v3.WorldCoordinates, v3.Normal, lightPos);

            var data = new ScanLineData { };

            double dP1P2, dP1P3;

            if (p2.Y - p1.Y > 0)
                dP1P2 = (p2.X - p1.X) / (p2.Y - p1.Y);
            else
                dP1P2 = 0;

            if (p3.Y - p1.Y > 0)
                dP1P3 = (p3.X - p1.X) / (p3.Y - p1.Y);
            else
                dP1P3 = 0;

            if (dP1P2 > dP1P3)
            {
                for (var y = (int)p1.Y; y <= (int)p3.Y; y++)
                {
                    data.currentY = y;

                    if (y < p2.Y)
                    {
                        data.ndotla = nl1;
                        data.ndotlb = nl3;
                        data.ndotlc = nl1;
                        data.ndotld = nl2;
                        ProcessScanLine(data, v1, v3, v1, v2, color);
                    }
                    else
                    {
                        data.ndotla = nl1;
                        data.ndotlb = nl3;
                        data.ndotlc = nl2;
                        data.ndotld = nl3;
                        ProcessScanLine(data, v1, v3, v2, v3, color);
                    }
                }
            }
            else
            {
                for (var y = (int)p1.Y; y <= (int)p3.Y; y++)
                {
                    data.currentY = y;

                    if (y < p2.Y)
                    {
                        data.ndotla = nl1;
                        data.ndotlb = nl2;
                        data.ndotlc = nl1;
                        data.ndotld = nl3;
                        ProcessScanLine(data, v1, v2, v1, v3, color);
                    }
                    else
                    {
                        data.ndotla = nl2;
                        data.ndotlb = nl3;
                        data.ndotlc = nl1;
                        data.ndotld = nl3;
                        ProcessScanLine(data, v2, v3, v1, v3, color);
                    }
                }
            }
        }

        
    }
}
