using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GraphicsLab6
{
    public class Point3D
    {
        public double X;
        public double Y;
        public double Z;
        public List<Point3D> Neighbours = new List<Point3D>();

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }


        public Point3D()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        public void AddNeighbour(Point3D neighbour)
        {
            Neighbours.Add(neighbour);
        }

        public void MultiplyByMatrix(List<List<double>> matrix)
        {
            var homogeneousCoords = new List<double> { X, Y, Z, 1 };
            var newHomogeneousCoords = new List<double> { 0, 0, 0, 0 };
            double col_res = 0;
            for (int j = 0; j < 4; ++j)
            {
                for (int i = 0; i < 4; ++i)
                    col_res += homogeneousCoords[i] * matrix[i][j]; //matrix[i][j] * homogeneousCoords[j];
                newHomogeneousCoords[j] = col_res;
                col_res = 0;
            }
            double divideBy = newHomogeneousCoords.Last() == 0 ? 1 : newHomogeneousCoords.Last();
            X = newHomogeneousCoords[0] / divideBy;
            Y = newHomogeneousCoords[1] / divideBy;
            Z = newHomogeneousCoords[2] / divideBy;
            //return new Point3D(newHomogeneousCoords[0] / divideBy, newHomogeneousCoords[1] / divideBy, newHomogeneousCoords[2] / divideBy);
        }

    }
}
