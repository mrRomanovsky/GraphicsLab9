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
        public double xN;
        public double yN;
        public double zN;
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
      
        public void MultiplyByMatrixTask8(List<List<double>> matrix)
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
            xN = newHomogeneousCoords[0] / divideBy;
            yN = newHomogeneousCoords[1] / divideBy;
            zN = newHomogeneousCoords[2] / divideBy;
            //return new Point3D(newHomogeneousCoords[0] / divideBy, newHomogeneousCoords[1] / divideBy, newHomogeneousCoords[2] / divideBy);
        }
      
        internal Point3D Copy()
        {
            return new Point3D(X, Y, Z);
        }

        public static Point3D operator + (Point3D point1, Point3D point2)
        {
            return new Point3D(point1.X + point2.X, point1.Y + point2.Y, point1.Z + point2.Z);
        }

        public static Point3D operator -(Point3D point1, Point3D point2)
        {
            return new Point3D(point1.X - point2.X, point1.Y - point2.Y, point1.Z - point2.Z);
        }

        public static Point3D operator / (Point3D point1, double divisor)
        {
            return new Point3D(point1.X / divisor, point1.Y / divisor, point1.Z / divisor);
        }

        internal static Point3D TransformCoordinate(Point3D coordinates, double[,] transMat)
        {
            throw new NotImplementedException();
        }

        public double Magnitude()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        internal void Normalize()
        {
            var lenght = Magnitude();
            if (lenght != 0)
            {
                X /= lenght;
                Y /= lenght;
                Z /= lenght;
            }
        }

        public static double Dot(Point3D point1, Point3D point2)
        {
            var div = ((Math.Sqrt(point1.X * point1.X + point1.Y * point1.Y + point1.Z * point1.Z) * Math.Sqrt(point2.X * point2.X + point2.Y * point2.Y + point2.Z * point2.Z)));
            double angle = Math.Acos((point1.X * point2.X + point1.Y * point2.Y + point1.Z * point2.Z));
            if (div != 0)
                angle /= div;
            var cos = Math.Cos(angle);
            return cos * point1.Magnitude() * point2.Magnitude();
        }
    }
}
