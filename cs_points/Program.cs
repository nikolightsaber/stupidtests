using System;
using System.Collections.Generic;

namespace cs_tests
{
    public class GeoCoordinate : IEquatable<GeoCoordinate>
    {
        public static readonly GeoCoordinate Unknown = new GeoCoordinate();

        public GeoCoordinate()
        {
        }

        public GeoCoordinate(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public GeoCoordinate(double latitude, double longitude, double altitude = 0.0, double pdop = 0.0, double hdop = 0.0, double vdop = 0.0f, string fixType = "Unknown")
            : this(latitude, longitude)
        {
            Altitude = altitude;
            Vdop = vdop;
            Hdop = hdop;
            Pdop = pdop;

            switch (fixType)
            {
                case "2D": Fix = FixType.TwoDimension; break;
                case "3D": Fix = FixType.ThreeDimension; break;
                case "FLOAT": Fix = FixType.Float; break;
                case "RTK": Fix = FixType.RTK; break;
                default: Fix = FixType.Invalid; break;
            }
        }

        public bool IsValid
        {
            get
            {
                return Hdop < 2.0f && Fix > FixType.Invalid;
            }
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public double Vdop { get; set; }
        public double Hdop { get; set; }
        public double Pdop { get; set; }
        public FixType Fix { get; set; }

        public enum FixType
        {
            Invalid,
            TwoDimension,
            ThreeDimension,
            Float,
            RTK
        }

        public bool Equals(GeoCoordinate other)
        {
            return other != null && Latitude == other.Latitude && Longitude == other.Longitude;
        }

        public static bool operator ==(GeoCoordinate left, GeoCoordinate right)
        {
            if (Object.ReferenceEquals(left, null))
                return Object.ReferenceEquals(right, null);
            return left.Equals(right);
        }

        public static bool operator !=(GeoCoordinate left, GeoCoordinate right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            var g = obj as GeoCoordinate;
            return g != null && Equals(g);
        }

        public double GetDistanceTo(GeoCoordinate other)
        {
            if (double.IsNaN(this.Latitude) || double.IsNaN(this.Longitude) || double.IsNaN(other.Latitude) || double.IsNaN(other.Longitude))
            {
                throw new ArgumentException("Argument Latitude or Longitude is not a number");
            }
            else
            {
                double latitude = this.Latitude * 0.0174532925199433;
                double longitude = this.Longitude * 0.0174532925199433;
                double num = other.Latitude * 0.0174532925199433;
                double longitude1 = other.Longitude * 0.0174532925199433;
                double num1 = longitude1 - longitude;
                double num2 = num - latitude;
                double num3 = Math.Pow(Math.Sin(num2 / 2), 2) + Math.Cos(latitude) * Math.Cos(num) * Math.Pow(Math.Sin(num1 / 2), 2);
                double num4 = 2 * Math.Atan2(Math.Sqrt(num3), Math.Sqrt(1 - num3));
                double num5 = 6376500 * num4;
                return num5;
            }
        }

        public override int GetHashCode()
        {
            return (Latitude * 100 + Longitude).GetHashCode();
        }
    }
    public class Point
    {
        public double X;
        public double Y;

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double Norm()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        public double Dot(Point p)
        {
            return X * p.X + Y * p.Y;
        }

        public double Cross(Point p)
        {
            return X * p.Y - Y * p.X;
        }

        public double DistanceSquared(Point p)
        {
            return (p.X - X) * (p.X - X) + (p.Y - Y) * (p.Y - Y);
        }

        public double GetDist(Point p)
        {
            return Math.Sqrt(DistanceSquared(p));
        }

        // slope of the vector from Point 'this' to Point 'p'
        public double Angle(Point p)
        {
            return Math.Atan2(p.Y - Y, p.X - X);
        }

        public static Point operator +(Point p1, Point p2)
        {
            Point p = new Point(p1.X + p2.X, p1.Y + p2.Y);
            return p;
        }

        public static Point operator -(Point p1, Point p2)
        {
            Point p = new Point(p1.X - p2.X, p1.Y - p2.Y);
            return p;
        }

        public static Point operator *(double k, Point p1)
        {
            Point p = new Point(k * p1.X, k * p1.Y);
            return p;
        }

        public static Point operator /(Point p1, double k)
        {
            Point p = new Point(p1.X / k, p1.Y / k);
            return p;
        }

        public override string ToString()
        {
            return "(" + X + " , " + Y + ")";
        }
    }

    public class Line
    {
        public double A;
        public double B;
        public double C;

        public Line(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
        }

        public Line(double angle, Point v)
        {
            A = -Math.Sin(angle);
            B = Math.Cos(angle);
            C = -(A * v.X + B * v.Y);
        }

        public Line(Point a, Point b):this(Math.Atan2(b.Y - a.Y, b.X - a.X), b)
        {
        }

        public Line Orthogonal(Point p)
        {
            double A_ortho = B;
            double B_ortho = A;
            if (Math.Abs(A) > Math.Abs(B))
            {
                B_ortho *= -1;
            }
            else
            {
                A_ortho *= -1;
            }
            return new Line(A_ortho, B_ortho, -A_ortho*p.X - B_ortho*p.Y);
        }

        public Line Parallel(Point p)
        {
            return new Line(A, B, -A * p.X - B * p.Y);
        }

        /// <summary>
        /// Returns the intersection point between two lines
        /// If the lines are parallel, returns null
        /// </summary>
        public Point Intersect(Line l)
        {
            double xp = 0;
            double yp = 0;

            double denom = B * l.A - A * l.B;

            if (Math.Abs(denom) < 1e-9)
            {
                // lines are parallel, no intersection
                return null;
            }

            if (Math.Abs(A) > Math.Abs(B))
            {
                yp = (l.C * A - C * l.A) / denom;
                xp = (-C - B * yp) / A;
            }
            else
            {
                xp = (C * l.B - l.C * B) / denom;
                yp = (-C - A * xp) / B;
            }

            return new Point(xp, yp);
        }

        public Point Projection(Point p)
        {
            double xp = 0;
            double yp = 0;

            if (Math.Abs(B) > Math.Abs(A))
            {
                xp = (-A * C - A * B * p.Y + B * B * p.X) / (A * A + B * B);
                yp = p.Y;
                if (B != 0)
                {
                    yp = (-C - A * xp) / B;
                }
            }
            else
            {
                yp = (-B * C - A * B * p.X + A * A * p.Y) / (A * A + B * B);
                xp = p.X;
                if (A != 0)
                {
                    xp = (-C - B * yp) / A;
                }
            }
            return new Point(xp, yp);
        }

        /// <summary>
        /// Return the distance from p to the line
        /// The sign is positive if 'target' is reachable CCW from the projection point on the line
        /// </summary>
        public double SignedDist(Point p, Point target)
        {
            Point proj = Projection(p);
            Point x = p - proj;
            Point t = target - proj;

            double err_dist = Math.Sign(x.Cross(t)) * x.Norm();

            return err_dist;
        }

        public override string ToString()
        {
            return string.Format("Line A={0} B={1} C={2}", A, B, C);
        }
    }

    public class Edge
    {
        public Point a;
        public Point b;

        public Edge(Point a, Point b)
        {
            this.a = a;
            this.b = b;
        }

        public bool RayIntersects(Point p)
        {
            if (a.Y > b.Y)
            {
                Edge ne = new Edge(b, a);
                return ne.RayIntersects(p);
            }

            if (p.Y == a.Y || p.Y == b.Y)
            {
                return RayIntersects(new Point(p.X, p.Y + 0.0000001));
            }
            if (p.Y > b.Y || p.Y < a.Y || p.X > Math.Max(a.X, b.X))
            {
                return false;
            }

            if (p.X < Math.Min(a.X, b.X))
            {
                return true;
            }

            double blue = Math.Abs(a.X - p.X) > double.NegativeInfinity ? (p.Y - a.Y) / (p.X - a.X) : double.PositiveInfinity;
            double red = Math.Abs(a.X - b.X) > double.NegativeInfinity ? (b.Y - a.Y) / (b.X - a.X) : double.PositiveInfinity;
            return blue >= red;
        }

        public Point RayIntersects(Point p, float angle, out double dist)
        {
            var v1 = p - a;
            var v2 = b - a;
            var v3 = new Point(-Math.Sin(angle), Math.Cos(angle));
            dist = double.NegativeInfinity;

            var dot = v2.Dot(v3);
            if (Math.Abs(dot) < 0.000001)
            {
                Console.WriteLine("intersect {0} {1} {2}", v2, v3, dot);
                return null; // parallel
            }

            var t1 = v2.Cross(v1) / dot;
            var t2 = v1.Dot(v3) / dot;

            if (t1 >= 0.0 && (t2 >= 0.0 && t2 <= 1.0))
            {
                dist = t1;
                return p + t1 * new Point(Math.Cos(angle), Math.Sin(angle));
            }

            return null;
        }


        /// <summary>
        /// Returns the intersection point between this segment and a line.
        /// If the segment and the line are parallel, or the intersection would be
        /// outside of the segment, returns null.
        /// </summary>
        public Point Intersect(Line l)
        {
            var my_line = new Line(a, b);
            var p = my_line.Intersect(l);

            if (p == null)
            {
                return null;
            }

            if ( p.X <= Math.Max(a.X, b.X) && p.X >= Math.Min(a.X, b.X) &&
                 p.Y <= Math.Max(a.Y, b.Y) && p.Y >= Math.Min(a.Y, b.Y))
            {
                return p;
            }
            else
            {
                // the intersection is not on the segment
                return null;
            }
        }

        public Point Intersect(Edge e)
        {
            // get the intersection between this and the line of 'e'
            var p = Intersect(new Line(e.a, e.b));

            if (p != null)
            {
                // check that p is on 'e' 
                if (!( p.X <= Math.Max(e.a.X, e.b.X) && p.X >= Math.Min(e.a.X, e.b.X) &&
                       p.Y <= Math.Max(e.a.Y, e.b.Y) && p.Y >= Math.Min(e.a.Y, e.b.Y)) )
                {
                    p = null;
                }
            }

            return p;
        }

        public Point Projection(Point p)
        {
            var my_line = new Line(a, b);
            var proj = my_line.Projection(p);

            if (proj.X <= Math.Max(a.X, b.X) && proj.X >= Math.Min(a.X, b.X) &&
                proj.Y <= Math.Max(a.Y, b.Y) && proj.Y >= Math.Min(a.Y, b.Y))
            {
                return proj;
            }
            else
            {
                // the projection is not on the segment
                return null;
            }
        }

        public double DistanceSquared(Point p, out float dir)
        {
            double l2 = a.DistanceSquared(b);

            if (l2 == 0.0)
            {
                dir = (float)p.Angle(a);
                return p.DistanceSquared(a);
            }

            double t = Math.Max(0, Math.Min(1, (p - a).Dot(b - a) / l2));
            Point projection = a + t * (b - a);

            dir = (float)p.Angle(projection);

            return p.DistanceSquared(projection);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Edge edge = new Edge(new Point(0, 0), new Point(1, 1));
            Point p = new Point(1, 1);
            float angle = (float) (Math.PI / 4.0);


            if (edge.RayIntersects(p, angle, out double dist) != null && dist <= double.MaxValue)
                Console.WriteLine("hello");
        }
    }
}
