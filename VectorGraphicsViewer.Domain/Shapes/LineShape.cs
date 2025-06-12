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
    /// Shape class representing a straight line between two points.
    /// Implements the IShape interface.
    /// </summary>
    public class LineShape : IShape
    {
        /// <summary>
        /// The starting point of the line (in virtual coordinate space).
        /// </summary>
        public Point Start { get; }

        /// <summary>
        /// The ending point of the line (in virtual coordinate space).
        /// </summary>
        public Point End { get; }

        /// <summary>
        /// Color of the line (supports ARGB).
        /// </summary>
        public Color Color { get; }

        /// <summary>
        /// Always returns false since lines are not filled shapes.
        /// </summary>
        public bool Filled => false;

        public LineShape(Point start, Point end, Color color)
        {
            Start = start;
            End = end;
            Color = color;
        }

        /// <summary>
        /// Draws the line on the given DrawingContext.
        /// </summary>
        public void Draw(DrawingContext context, double scale, double offsetX, double offsetY)
        {
            // Freeze brush for performance (optimized for single-use)
            var penBrush = new SolidColorBrush(Color);
            penBrush.Freeze();  // Improve performance by making it immutable
            var pen = new Pen(penBrush, 1.0);

            // Convert virtual coordinates to screen coordinates (Y axis is flipped)
            context.DrawLine(pen,
                new Point(Start.X * scale + offsetX, -Start.Y * scale + offsetY),
                new Point(End.X * scale + offsetX, -End.Y * scale + offsetY));
        }

        /// <summary>
        /// Determines whether a given screen point is close enough to this line
        /// to be considered selected.
        /// </summary>
        public bool Contains(Point point, double scale, double offsetX, double offsetY)
        {
            // Convert to screen coordinates
            var p1 = new Point(Start.X * scale + offsetX, -Start.Y * scale + offsetY);
            var p2 = new Point(End.X * scale + offsetX, -End.Y * scale + offsetY);

            double distance = DistancePointToSegment(point, p1, p2);
            return distance < 3.0; // Considered clicked if within 3 pixels
        }

        /// <summary>
        /// Calculates the shortest distance from a point to a line segment.
        /// Uses vector geometry.
        /// </summary>
        private double DistancePointToSegment(Point p, Point a, Point b)
        {
            var ab = b - a;
            var ap = p - a;
            double t = (ab.X * ap.X + ab.Y * ap.Y) / (ab.X * ab.X + ab.Y * ab.Y);
            t = Math.Max(0, Math.Min(1, t)); // Clamp to [0,1]
            var closest = new Point(a.X + ab.X * t, a.Y + ab.Y * t);
            return (closest - p).Length;
        }

        /// <summary>
        /// Returns the bounding box (rectangular area) that contains the shape.
        /// This is in virtual coordinates (scale is not applied).
        /// </summary>
        public Rect GetBounds()
        {
            var minX = Math.Min(Start.X, End.X);
            var minY = Math.Min(Start.Y, End.Y);
            var maxX = Math.Max(Start.X, End.X);
            var maxY = Math.Max(Start.Y, End.Y);
            return new Rect(new Point(minX, minY), new Point(maxX, maxY));
        }

    }

}
