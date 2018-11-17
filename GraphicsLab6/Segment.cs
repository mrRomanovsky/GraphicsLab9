using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsLab6
{
    class Segment2D
    {
        readonly PointF a;
        readonly PointF b;

        public Segment2D(PointF a, PointF b)
        {
            this.a = a;
            this.b = b;
        }

        public double Lenght()
        {
            float dx = a.X - b.X;
            float dy = a.Y - b.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }

    class Segment3D
    {
        readonly Point3D a;
        readonly Point3D b;

        public Segment3D(Point3D a, Point3D b)
        {
            this.a = a;
            this.b = b;
        }

        public double Lenght()
        {
            return Math.Sqrt(Math.Pow((a.X - b.X),2) + Math.Pow((a.Y - b.Y),2) + Math.Pow((a.Z - b.Z),2));
        }
    }
}
