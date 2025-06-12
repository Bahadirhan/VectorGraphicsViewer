using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace VectorGraphicsViewer.Domain.Shapes
{
    /// <summary>
    /// Represents a polygon shape with three corners (a triangle).
    /// Implements the IShape interface.
    /// </summary>
    public class TriangleShape : IShape
    {
        /// <summary>
        /// First corner of the triangle.
        /// </summary>
        public Point A { get; }

        /// <summary>
        /// Second corner of the triangle.
        /// </summary>
        public Point B { get; }

        /// <summary>
        /// Third corner of the triangle.
        /// </summary>
        public Point C { get; }

        /// <summary>
        /// Shape color in ARGB format.
        /// </summary>
        public Color Color { get; }

        /// <summary>
        /// Indicates whether the triangle is filled or only its border is drawn.
        /// </summary>
        public bool Filled { get; }

        public TriangleShape(Point a, Point b, Point c, Color color, bool filled)
        {
            A = a;
            B = b;
            C = c;
            Color = color;
            Filled = filled;
        }

        /// <summary>
        /// Draws the triangle onto the given DrawingContext.
        /// </summary>
        public void Draw(DrawingContext context, double scale, double offsetX, double offsetY)
        {
            // Convert virtual coordinates to screen coordinates (Y axis flipped)
            var pA = new Point(A.X * scale + offsetX, -A.Y * scale + offsetY);
            var pB = new Point(B.X * scale + offsetX, -B.Y * scale + offsetY);
            var pC = new Point(C.X * scale + offsetX, -C.Y * scale + offsetY);

            // Create a frozen brush for the border for performance
            var penBrush = new SolidColorBrush(Color);
            penBrush.Freeze();
            var pen = new Pen(penBrush, 1.0); // 1px thickness

            SolidColorBrush fillBrush = null;
            if (Filled)
            {
                fillBrush = new SolidColorBrush(Color);
                fillBrush.Freeze(); // Optimize fill brush as well
            }

            // Create the triangle geometry
            var geometry = new StreamGeometry();
            using (var ctx = geometry.Open())
            {
                ctx.BeginFigure(pA, Filled, true); // Start point
                ctx.LineTo(pB, true, false);
                ctx.LineTo(pC, true, false);
            }

            // Draw the shape
            context.DrawGeometry(fillBrush, pen, geometry);
        }

        /// <summary>
        /// Determines whether a point on the screen is inside the triangle.
        /// </summary>
        public bool Contains(Point point, double scale, double offsetX, double offsetY)
        {
            // Convert triangle points to screen coordinates
            var pA = new Point(A.X * scale + offsetX, -A.Y * scale + offsetY);
            var pB = new Point(B.X * scale + offsetX, -B.Y * scale + offsetY);
            var pC = new Point(C.X * scale + offsetX, -C.Y * scale + offsetY);

            return PointInTriangle(point, pA, pB, pC);
        }

        /// <summary>
        /// Algorithm that determines whether a point lies inside the triangle.
        /// This version uses a deterministic barycentric method.
        /// </summary>
        private bool PointInTriangle(Point p, Point a, Point b, Point c)
        {
            double s = a.Y * c.X - a.X * c.Y + (c.Y - a.Y) * p.X + (a.X - c.X) * p.Y;
            double t = a.X * b.Y - a.Y * b.X + (a.Y - b.Y) * p.X + (b.X - a.X) * p.Y;

            if ((s < 0) != (t < 0))
                return false;

            double A = -b.Y * c.X + a.Y * (c.X - b.X) + a.X * (b.Y - c.Y) + b.X * c.Y;
            if (A < 0.0)
            {
                s = -s; t = -t; A = -A;
            }

            return s > 0 && t > 0 && (s + t) < A;
        }

        /// <summary>
        /// Returns the bounding rectangle that contains the triangle (in virtual space).
        /// </summary>
        public Rect GetBounds()
        {
            var minX = Math.Min(Math.Min(A.X, B.X), C.X);
            var minY = Math.Min(Math.Min(A.Y, B.Y), C.Y);
            var maxX = Math.Max(Math.Max(A.X, B.X), C.X);
            var maxY = Math.Max(Math.Max(A.Y, B.Y), C.Y);

            return new Rect(new Point(minX, minY), new Point(maxX, maxY));
        }
    }
}

