﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsLab6
{
    public enum PolyhedronType
    {
        Tetrahedron,
        Hexahedron,
        Octahedron,
        Icosahedron,
        Dodecahedron
    };

   public static class SchlafliSymbol
    {
        static public Dictionary<string, Tuple<int, int>> schlafliSymbol = new Dictionary<string, Tuple<int, int>>
        {
            ["Tetrahedron"] = Tuple.Create<int, int>(3, 3),
            ["Hexahedron"] = Tuple.Create<int, int>(4, 3),
            ["Octahedron"] = Tuple.Create<int, int>(3, 4),
            ["Icosahedron"] = Tuple.Create<int, int>(3, 5),
            ["Dodecahedron"] = Tuple.Create<int, int>(5, 3)
        };
    }
    

    public class Polyhedron
    {
        public int CountVertex;
        public int CountSegment;
        public int CountEdge;
        public PolyhedronType Type;
        public PointF CentrePoint;
        public int SegmentLength;
        public List<Point3D> vertexes;
        private Polyhedron figure;

        public Point3D Centre { get; set; }
        public List<Edge> Edges;
        public List<List<int>> edges;

        public Polyhedron(PolyhedronType type, int len)
        {
            edges = new List<List<int>>();
            Edges = new List<Edge>();
            var ss = SchlafliSymbol.schlafliSymbol[type.ToString()];
            CountVertex = 4 * ss.Item1 / (4 - (ss.Item1 - 2) * (ss.Item2 - 2));
            CountSegment = ss.Item2 * CountVertex / 2;
            CountEdge = ss.Item2 * CountVertex / ss.Item1;
            Type = type;
            SegmentLength = len;
            vertexes = new List<Point3D>();
            switch (type)
            {
                case PolyhedronType.Tetrahedron:
                    BuildTetrahedron(len);
                    break;
                case PolyhedronType.Hexahedron:
                    BuildHexahedron(len);
                    break;
                case PolyhedronType.Octahedron:
                    BuildOctahedron(len);
                    break;
            }
        }

        public Polyhedron(Polyhedron figure)
        {
            
        }

        //angle - в радианах!
        public void RotateAroundLine(Point3D linePoint, Point3D parallelVector, double angle)
        {
            var l = parallelVector.X;
            var m = parallelVector.Y;
            var n = parallelVector.Z;
            var lSquared = l * l;
            var mSquared = m * m;
            var nSquared = n * n;
            var cosAngle = Math.Round(Math.Cos(angle), 3);
            var sinAngle = Math.Round(Math.Sin(angle), 3);

            var rotateMatrix = new List<List<double>>
            {
                new List<double>{lSquared + cosAngle * (1 - lSquared), l*(1 - cosAngle)*m + n*sinAngle, l*(1 - cosAngle)*n - m*sinAngle, 0},
                new List<double>{ l * (1 - cosAngle) * m - n * sinAngle, mSquared + cosAngle*(1 - mSquared), m*(1 - cosAngle)*n + l*sinAngle, 0},
                new List<double>{ l * (1 - cosAngle) * n + m * sinAngle, m*(1 - cosAngle)*n - l*sinAngle, nSquared + cosAngle*(1 - nSquared), 0},
                new List<double>{ 0, 0, 0, 1}
            };

            var shiftTo0Matrix = new List<List<double>>
            {
                new List<double>{1, 0, 0, 0 },
                new List<double>{0, 1, 0, 0},
                new List<double>{0, 0, 1, 0 },
                new List<double>{-linePoint.X, -linePoint.Y, -linePoint.Z, 1 }
            };

            var shiftBackMatrix = new List<List<double>>
            {
                new List<double>{1, 0, 0, 0 },
                new List<double>{0, 1, 0, 0},
                new List<double>{0, 0, 1, 0 },
                new List<double>{linePoint.X, linePoint.Y, linePoint.Z, 1 }
            };

            foreach (var vertex in vertexes)
            {
                vertex.MultiplyByMatrix(shiftTo0Matrix);
                vertex.MultiplyByMatrix(rotateMatrix);
                vertex.MultiplyByMatrix(shiftBackMatrix);
                vertex.X = Math.Round(vertex.X, 3);
                vertex.Y = Math.Round(vertex.Y, 3);
                vertex.Z = Math.Round(vertex.Z, 3);
            }
        }


        #region tetrahedron
        private void BuildTetrahedron(int len)
        {
            edges.Add(new List<int>() { 0, 1, 2 });
            edges.Add(new List<int>() { 0, 1, 3 });
            edges.Add(new List<int>() { 0, 2, 3 });
            edges.Add(new List<int>() { 1, 3, 2 });
            vertexes.Add(new Point3D(0, 0, 0));
            vertexes.Add(new Point3D(len, 0, 0));
            vertexes.Add(new Point3D(len / 2, len * Math.Sqrt(3) / 2, 0));
            vertexes.Add(new Point3D(len / 2, len * Math.Sqrt(3) / 6, len * Math.Sqrt(6) / 3));
            for (int i = 0; i < 4; ++i)
                for (int j = 0; j < 4; ++j)
                    if (i != j)
                    {
                        vertexes[i].AddNeighbour(vertexes[j]);
                        vertexes[j].AddNeighbour(vertexes[i]);
                    }
            Centre = new Point3D
                (
                (vertexes[0].X + vertexes[1].X + vertexes[2].X + vertexes[3].X) / 4,
                (vertexes[0].Y + vertexes[1].Y + vertexes[2].Y + vertexes[3].Y) / 4,
                (vertexes[0].Z + vertexes[1].Z + vertexes[2].Z + vertexes[3].Z) / 4
                );
            Edges = new List<Edge>
            {
                new Edge(new List<Point3D>{vertexes[0], vertexes[1], vertexes[2]}),
                new Edge(new List<Point3D>{vertexes[0], vertexes[1], vertexes[3]}),
                new Edge(new List<Point3D>{vertexes[0], vertexes[2], vertexes[3]}),
                new Edge(new List<Point3D>{vertexes[1], vertexes[3], vertexes[2] })
            };
        }
        #endregion

        #region hexahedron
        private void BuildHexahedron(int len)
        {
            edges.Add(new List<int>() { 0, 1, 3, 2 });
            edges.Add(new List<int>() { 0, 1, 5, 4 });
            edges.Add(new List<int>() { 0, 4, 6, 2 });
            edges.Add(new List<int>() { 2, 6, 7, 3 });
            edges.Add(new List<int>() { 1, 5, 7, 3 });
            edges.Add(new List<int>() { 5, 4, 6, 7 });

            vertexes.Add(new Point3D(0, 0, 0));
            vertexes.Add(new Point3D(len, 0, 0));
            vertexes.Add(new Point3D(0, len, 0));
            vertexes.Add(new Point3D(len, len, 0));
            vertexes.Add(new Point3D(0, 0, len));
            vertexes.Add(new Point3D(len, 0, len));
            vertexes.Add(new Point3D(0, len, len));
            vertexes.Add(new Point3D(len, len, len));

            vertexes[0].AddNeighbour(vertexes[1]);
            vertexes[0].AddNeighbour(vertexes[2]);

            vertexes[1].AddNeighbour(vertexes[0]);
            vertexes[1].AddNeighbour(vertexes[3]);

            vertexes[2].AddNeighbour(vertexes[0]);
            vertexes[2].AddNeighbour(vertexes[3]);

            vertexes[4].AddNeighbour(vertexes[5]);
            vertexes[4].AddNeighbour(vertexes[6]);

            vertexes[5].AddNeighbour(vertexes[4]);
            vertexes[5].AddNeighbour(vertexes[7]);

            vertexes[6].AddNeighbour(vertexes[4]);
            vertexes[6].AddNeighbour(vertexes[7]);

            for (int i = 0; i < 4; ++i)
            {
                vertexes[i].AddNeighbour(vertexes[i + 4]);
                vertexes[i + 4].AddNeighbour(vertexes[i]);
            }

            Centre = new Point3D(len / 2, len / 2, len / 2);
            Edges = new List<Edge>
            {
                new Edge(new List<Point3D>{vertexes[0], vertexes[1], vertexes[3], vertexes[2]}),
                new Edge(new List<Point3D>{vertexes[0], vertexes[1], vertexes[5], vertexes[4]}),
                new Edge(new List<Point3D>{vertexes[0], vertexes[4], vertexes[6], vertexes[2]}),
                new Edge(new List<Point3D>{vertexes[2], vertexes[6], vertexes[7], vertexes[3]}),
                new Edge(new List<Point3D>{vertexes[1], vertexes[5], vertexes[7], vertexes[3]}),
                new Edge(new List<Point3D>{vertexes[5], vertexes[4], vertexes[6], vertexes[7]}),
            };
        }
        #endregion

        #region octahedron
        private void BuildOctahedron(int len)
        {
            edges.Add(new List<int>() { 0, 2, 5 });
            edges.Add(new List<int>() { 0, 3, 5 });
            edges.Add(new List<int>() { 3, 1, 5 });
            edges.Add(new List<int>() { 1, 2, 5 });
            edges.Add(new List<int>() { 0, 2, 4 });
            edges.Add(new List<int>() { 0, 3, 4 });
            edges.Add(new List<int>() { 3, 1, 4 });
            edges.Add(new List<int>() { 1, 2, 4 });

            float shift = (float)(len * Math.Sqrt(2) / 2);
            vertexes.Add(new Point3D(shift, 0, 0));
            vertexes.Add(new Point3D(-shift, 0, 0));
            vertexes.Add(new Point3D(0, shift, 0));
            vertexes.Add(new Point3D(0, -shift, 0));
            vertexes.Add(new Point3D(0, 0, shift));
            vertexes.Add(new Point3D(0, 0, -shift));
            foreach (var vertex1 in vertexes)
                foreach (var vertex2 in vertexes)
                    if (   vertex1.X == 0 && vertex2.X != 0
                        || vertex1.Y == 0 && vertex2.Y != 0
                        || vertex1.Z == 0 && vertex2.Z != 0  )
                        vertex1.AddNeighbour(vertex2);
            Centre = new Point3D(0, 0, 0);
            Edges = new List<Edge>
            {
                new Edge(new List<Point3D>{vertexes[0], vertexes[2], vertexes[5]}),
                new Edge(new List<Point3D>{vertexes[0], vertexes[3], vertexes[5]}),
                new Edge(new List<Point3D>{vertexes[3], vertexes[1], vertexes[5]}),
                new Edge(new List<Point3D>{vertexes[1], vertexes[2], vertexes[5]}),
                new Edge(new List<Point3D>{vertexes[0], vertexes[2], vertexes[4]}),
                new Edge(new List<Point3D>{vertexes[0], vertexes[3], vertexes[4]}),
                new Edge(new List<Point3D>{vertexes[3], vertexes[1], vertexes[4]}),
                new Edge(new List<Point3D>{vertexes[1], vertexes[2], vertexes[4]}),
            };
        }
        #endregion

        //edge connections: 0 <-> 1 <-> 2 <-> .. <-> n - 1 <-> 0
        public List<PointF> Rasterize(List<PointF> edge)
        {
            var minMaxYs = GetMinMaxY(edge);
            int minY = (int)Math.Floor(minMaxYs.Item1);
            int maxY = (int)Math.Ceiling(minMaxYs.Item2);
            var segments = GetSegment2Ds(edge);
            var intersectionPoints = new List<PointF>();
            for (int horizY = minY; horizY <= maxY; ++horizY)
            {
                foreach (var seg in segments)
                {
                    var intersectionPoint = GetSegmentHorizIntersection(seg, (float)(horizY + 0.5));
                    if (intersectionPoint.X > -1000)
                        intersectionPoints.Add(intersectionPoint);
                }
            }


            intersectionPoints = intersectionPoints.OrderByDescending(p => p.Y).ThenBy(p => p.X).ToList();
            var resultingPixels = new List<PointF>();
            float lastY = intersectionPoints[0].Y;
            int lastYStart = 0;
            for (int i = 1; i < intersectionPoints.Count; ++i)
            {
                if (intersectionPoints[i].Y != lastY)
                {
                    ProcessIntersections(intersectionPoints, lastYStart, i, resultingPixels);
                    lastYStart = i;
                    lastY = intersectionPoints[i].Y;
                }
            }

            return resultingPixels;
        }

        private void ProcessIntersections(List<PointF> intersectionPoints, int startIdx, int endIdx, List<PointF> resultingPixels)
        {
            int currIdx = startIdx;
            int y = (int)Math.Floor(intersectionPoints[startIdx].Y);
            while (currIdx < endIdx)
            {
                var point1 = intersectionPoints[currIdx];
                var point2 = intersectionPoints[currIdx + 1];
                int x1 = (int)Math.Floor(point1.X);
                int x2 = (int)Math.Floor(point2.X);
                int x = x1;
                while (x1 <= x + 0.5 && x + 0.5 <= x2)
                {
                    resultingPixels.Add(new PointF(x, y));
                    ++x;
                }
                currIdx += 2;
            } //x1 <= x + 0.5 <= x2
        }

        private Tuple<double, double> GetMinMaxY(List<PointF> edge)
        {
            double minY = int.MaxValue;
            double maxY = int.MinValue;
            foreach (var v in edge)
            {
                if (v.Y < minY)
                    minY = v.Y;
                if (v.Y > maxY)
                    maxY = v.Y;
            }
            return new Tuple<double, double>(minY, maxY);
        }

        private List<Segment2D> GetSegment2Ds(List<PointF> edge)
        {
            var result = new List<Segment2D>();
            for (int i = 0; i < edge.Count - 1; ++i)
                result.Add(new Segment2D(edge[i], edge[i + 1]));
            result.Add(new Segment2D(edge[edge.Count - 1], edge[0]));
            return result;
        }

        private PointF GetSegmentHorizIntersection(Segment2D segment, float horizY)
        {
            PointF minYPoint;
            PointF maxYPoint;
            if (segment.a.Y < segment.b.Y)
            {
                minYPoint = segment.a;
                maxYPoint = segment.b;
            }
            else
            {
                minYPoint = segment.b;
                maxYPoint = segment.a;
            }
            if (minYPoint.Y <= horizY && maxYPoint.Y > horizY)
            {
                var newPointX = (maxYPoint.X - minYPoint.X) * (horizY - minYPoint.Y) / (maxYPoint.Y - minYPoint.Y) + minYPoint.X;
                var newPointY = horizY;
                return new PointF(newPointX, newPointY);
            }
            return new PointF(-float.MaxValue, -float.MaxValue);
        }
    }
}
