using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsLab6
{
    public class Edge
    {
        //private int countVertex;
        public List<Point3D> Vertexes;
        public double A, B, C, D;
        public Edge()
        {
            Vertexes = new List<Point3D>();
        }

        public Edge(List<Point3D> points)
        {
            Vertexes = points;
            //countVertex = points.Count;
        }

        public Point3D FindLeftUpVertex(string plans, int order = 1)
        {
            if (plans == "xoy")
            {
                return Vertexes.OrderBy(p => order * p.X).ThenBy(p => order * p.Y).First();
            }
            if (plans == "yoz")
            {
                return Vertexes.OrderBy(p => order * p.Y).ThenBy(p => order * p.Z).First();
            }
            return Vertexes.OrderBy(p => order * p.X).ThenBy(p => order * p.Z).First();
        }
        public void GetEquationPlans()
        {
            var t1 = Vertexes[0];
            var t2 = Vertexes[1];
            var t3 = Vertexes[2];
            A = t1.Y * (t2.Z - t3.Z) + t2.Y * (t3.Z - t1.Z) + t3.Y * (t1.Z - t2.Z);
            B = t1.Z * (t2.X - t3.X) + t2.Z * (t3.X - t1.X) + t3.Z * (t1.X - t2.X);
            C = t1.X * (t2.Y - t3.Y) + t2.X * (t3.Y - t1.Y) + t2.X * (t1.Y - t2.Y);
            D = -(t1.X * (t2.Y * t3.Z - t3.Y * t2.Z) + t2.X * (t3.Y * t1.Z - t1.Y * t3.Z) + t3.X * (t1.Y * t2.Z - t2.Y * t1.Z));
        }

        internal Point3D FindRightDownVertex(string v)
        {
            return FindLeftUpVertex(v, -1);
        }
    }

    //class EquilateralEdge : Edge
    //{
    //    readonly double Angle;
    //    public EquilateralEdge(int countVert, double length, PointF startPoint)
    //    {
    //        Angle = Math.PI * 2 / countVert;
    //        Vertexes = Enumerable.Range(0, countVert)
    //              .Select(i => 
    //                PointF.Add(startPoint, new SizeF((float)(Math.Sin(i * Angle) * length), (float)(Math.Cos(i * Angle) * length))))
    //              .ToList();
    //    }

    //    public EquilateralEdge(int countVert, PointF centrePoint, double radius)
    //    {
    //        Angle = Math.PI * 2 / countVert;
    //        Vertexes = Enumerable.Range(0, countVert)
    //              .Select(i => 
    //                PointF.Add(centrePoint, new SizeF((float)(Math.Sin(i * Angle) * radius), (float)(Math.Cos(i * Angle) * radius))))
    //              .ToList();
    //    }
    //}

}
