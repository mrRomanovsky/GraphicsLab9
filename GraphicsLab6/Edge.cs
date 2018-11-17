using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsLab6
{
    class Edge
    {
        //private int countVertex;
        public List<PointF> Vertexes;

        public Edge()
        {
            Vertexes = new List<PointF>();
        }

        public Edge(List<PointF> points)
        {
            Vertexes = points;
            //countVertex = points.Count;
        }
    }

    class EquilateralEdge : Edge
    {
        readonly double Angle;
        public EquilateralEdge(int countVert, double length, PointF startPoint)
        {
            Angle = Math.PI * 2 / countVert;
            Vertexes = Enumerable.Range(0, countVert)
                  .Select(i => 
                    PointF.Add(startPoint, new SizeF((float)(Math.Sin(i * Angle) * length), (float)(Math.Cos(i * Angle) * length))))
                  .ToList();
        }

        public EquilateralEdge(int countVert, PointF centrePoint, double radius)
        {
            Angle = Math.PI * 2 / countVert;
            Vertexes = Enumerable.Range(0, countVert)
                  .Select(i => 
                    PointF.Add(centrePoint, new SizeF((float)(Math.Sin(i * Angle) * radius), (float)(Math.Cos(i * Angle) * radius))))
                  .ToList();
        }
    }

}
